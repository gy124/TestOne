using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketHelper
{
    public class UdpLibrary :IDisposable
    {
        #region 构造函数
        public UdpLibrary(int port)
        {
            Port = port;
        }
        public UdpLibrary()
        {
            //默认监听端口1234
            Port = 1234;
        }
        #endregion

        #region 变量
        private UdpClient _udpClient;
        /// <summary>
        /// UDP监听端口
        /// </summary>
        private int _port = 1234;
        private bool _started;
        #endregion

        #region 属性
        [Description("UDP监听端口")]
        [Category("UDP服务端")]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        [Description("UDP客户端")]
        [Category("UDP服务端")]
        internal UdpClient UdpClient
        {
            get
            {
                if (_udpClient == null)
                {
                    bool success = false;
                    while (!success)
                    {
                        try
                        {
                            _udpClient = new UdpClient(_port);
                            success = true;
                        }
                        catch (SocketException ex)
                        {
                            _port++;
                            if (_port > 65535)
                            {
                                success = true;
                                throw ex;
                            }
                        }
                    }

                    uint IOC_IN = 0x80000000;
                    uint IOC_VENDOR = 0x18000000;
                    uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                    _udpClient.Client.IOControl(
                        (int)SIO_UDP_CONNRESET,
                        new byte[] { Convert.ToByte(false) },
                        null);
                }
                return _udpClient;
            }
        }
        #endregion

        #region 方法
        public void Start()
        {
            if (!_started)
            {
                _started = true;
                ReceiveInternal();
            }
        }

        public void Stop()
        {
            try
            {
                _started = false;
                UdpClient.Close();
                _udpClient = null;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 发送内部协议封装数据
        /// </summary>
        /// <param name="cell">消息数据单元</param>
        /// <param name="remoteIP"></param>
        public void Send(IDataCell cell, IPEndPoint remoteIP)
        {
            byte[] buffer = cell.ToBuffer();
            SendInternal(buffer, remoteIP);
        }
        /// <summary>
        /// 发送原始数据
        /// </summary>
        /// <param name="buffer">原始Byte数据</param>
        /// <param name="remoteIP"></param>
        public void Send(byte[] buffer, IPEndPoint remoteIP)
        {
            SendInternal(buffer, remoteIP);
        }
        protected void SendInternal(byte[] buffer, IPEndPoint remoteIP)
        {
            if (!_started)
            {
                throw new ApplicationException("UDP Closed.");
            }
            try
            {
                UdpClient.BeginSend(
                   buffer,
                   buffer.Length,
                   remoteIP,
                   new AsyncCallback(SendCallback),
                   null);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }

        protected void ReceiveInternal()
        {
            if (!_started)
            {
                return;
            }
            try
            {
                UdpClient.BeginReceive(
                   new AsyncCallback(ReceiveCallback),
                   null);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }

        private void SendCallback(IAsyncResult result)
        {
            try
            {
                UdpClient.EndSend(result);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            if (!_started)
            {
                return;
            }
            IPEndPoint remoteIP = new IPEndPoint(IPAddress.Any, 0);
            byte[] buffer = null;
            try
            {
                buffer = UdpClient.EndReceive(result, ref remoteIP);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            finally
            {
                ReceiveInternal();
            }

            OnReceiveData(new ReceiveDataEventArgs(buffer, remoteIP));
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _started = false;
            if (_udpClient != null)
            {
                _udpClient.Close();
                _udpClient = null;
            }
        }

        #endregion

        #region 事件
        public event ReceiveDataEventHandler ReceiveData;
        [Description("UDP服务端接收数据事件")]
        [Category("UDPServer事件")]
        protected virtual void OnReceiveData(ReceiveDataEventArgs e)
        {
            if (ReceiveData != null)
            {
                ReceiveData(this, e);
            }
        }
        #endregion
    }
}
