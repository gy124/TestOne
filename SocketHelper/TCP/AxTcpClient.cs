/********************************************************************
 * *
 * * Copyright (C) 2013-? Corporation All rights reserved.
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：Socket客户端封装组件
 * *
********************************************************************/
#region 说明
/* 简介：Socket通讯客户端实现网络通讯
 * 功能介绍：Socket通讯客户端实现网络通讯，支持断开重连。
 * socket客户端封装组件的使用：
 * 1、属性设置：
 * ServerIp
 * ServerPort
 * 
 * 2、启动和关闭方法：
 * TCPCliet.StartConnection();
 * TCPCliet.StopConnection();
 * 
 * 3、组件提供三个主要事件
 * ①、OnRecevice(byte[] date)；接收数据事件
 * ②、OnErrorMsg(string msg)；返回错误消息事件
 * ③、OnStateInfo(string msg, SocketState state)；连接状态改变时返回连接状态事件
  */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketHelper
{
    public partial class AxTcpClient : Component
    {
        #region 构造函数
        public AxTcpClient()
        {
            InitializeComponent();
        }

        public AxTcpClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        } 
        #endregion

        #region 属性
        /// <summary>
        /// 服务端IP
        /// </summary>
        private string _serverip;
        [Description("服务端IP")]
        [Category("TcpClient属性")]
        public string ServerIp
        {
            set { _serverip = value; }
            get { return _serverip; }
        }
        /// <summary>
        /// 服务端监听端口
        /// </summary>
        private int _serverport;
        [Description("服务端监听端口")]
        [Category("TcpClient属性")]
        public int ServerPort
        {
            set { _serverport = value; }
            get { return _serverport; }
        }
        /// <summary>
        /// TcpClient客户端
        /// </summary>
        private TcpClient _tcpclient = null;
        [Description("TcpClient操作类")]
        [Category("TcpClient隐藏属性")]
        [Browsable(false)]
        public TcpClient Tcpclient
        {
            set { _tcpclient = value; }
            get { return _tcpclient; }
        }
        /// <summary>
        /// Tcp客户端连接线程
        /// </summary>
        private Thread _tcpthread = null;
        [Description("TcpClient连接服务端线程")]
        [Category("TcpClient隐藏属性")]
        [Browsable(false)]
        public Thread Tcpthread
        {
            set { _tcpthread = value; }
            get { return _tcpthread; }
        } 
        /// <summary>
        /// 是否启动Tcp连接线程
        /// </summary>
        private bool _isStarttcpthreading = false;
        [Description("是否启动Tcp连接线程")]
        [Category("TcpClient隐藏属性")]
        [Browsable(false)]
        public bool IsStartTcpthreading
        {
            set { _isStarttcpthreading = value; }
            get { return _isStarttcpthreading; }
        }
        /// <summary>
        /// 连接是否关闭（用来断开重连）
        /// </summary>
        private bool _isclosed = false;
        [Description("连接是否关闭（用来断开重连）")]
        [Category("TcpClient属性")]
        public bool Isclosed
        {
            set { _isclosed = value; }
            get { return _isclosed; }
        }

        private int _reConnectionTime = 3000;
        /// <summary>
        /// 设置断开重连时间间隔单位（毫秒）（默认3000毫秒）
        /// </summary>
        [Description("设置断开重连时间间隔单位（毫秒）（默认3000毫秒）")]
        [Category("TcpClient属性")]
        public int ReConnectionTime
        {
            get { return _reConnectionTime; }
            set { _reConnectionTime = value; }
        }
        private string _receivestr;
        /// <summary>
        ///  接收Socket数据包 缓存字符串
        /// </summary>
        [Description("接收Socket数据包 缓存字符串")]
        [Category("TcpClient隐藏属性"),Browsable(false)]
        public string Receivestr
        {
            set { _receivestr = value; }
            get { return _receivestr; }
        }
        /// <summary>
        /// 重连次数
        /// </summary>
        private int _reConectedCount = 0;
        [Description("重连次数")]
        [Category("TcpClient隐藏属性"), Browsable(false)]
        public int ReConectedCount
        {
            get { return _reConectedCount; }
            set { _reConectedCount = value; }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 启动连接Socket服务器
        /// </summary>
        public void StartConnection()
        {
            try
            {
                //Isclosed = false;
                CreateTcpClient();
            }
            catch (Exception ex)
            {
                OnTcpClientErrorMsgEnterHead("错误信息：" + ex.Message);
            }
        }
        /// <summary>
        /// 创建线程连接
        /// </summary>
        private void CreateTcpClient()
        {

            if (Isclosed)
                return;
            //标示已启动连接，防止重复启动线程
            Isclosed = true;
            Tcpclient = new TcpClient();
            Tcpthread = new Thread(StartTcpThread);
            IsStartTcpthreading = true;
            Tcpthread.Start();
        }
        /// <summary>
        ///  线程接收Socket上传的数据
        /// </summary>
        private void StartTcpThread()
        {
            byte[] receivebyte = new byte[1024];
            int bytelen;
            try
            {
                while (IsStartTcpthreading)
                #region
                {
                    if (!Tcpclient.Connected)
                    {
                        try
                        {
                            if (ReConectedCount != 0)
                            {
                                //返回状态信息
                                OnTcpClientStateInfoEnterHead(string.Format("正在第{0}次重新连接服务器... ...", ReConectedCount), SocketState.Reconnection);
                            }
                            else
                            {
                                //SocketStateInfo
                                OnTcpClientStateInfoEnterHead("正在连接服务器... ...", SocketState.Connecting);
                            }
                            Tcpclient.Connect(IPAddress.Parse(ServerIp), ServerPort);
                            OnTcpClientStateInfoEnterHead("已连接服务器", SocketState.Connected);
                            //Tcpclient.Client.Send(Encoding.Default.GetBytes("login"));
                        }
                        catch
                        {
                            //连接失败
                            ReConectedCount++;
                            //强制重新连接
                            Isclosed = false;
                            IsStartTcpthreading = false;
                            //每三秒重连一次
                            Thread.Sleep(ReConnectionTime);
                            continue;
                        }
                    }
                    //Tcpclient.Client.Send(Encoding.Default.GetBytes("login"));
                    bytelen = Tcpclient.Client.Receive(receivebyte);
                    // 连接断开
                    if (bytelen == 0)
                    {
                        //返回状态信息
                        OnTcpClientStateInfoEnterHead("与服务器断开连接... ...", SocketState.Disconnect);
                        // 异常退出、强制重新连接
                        Isclosed = false;
                        ReConectedCount = 1;
                        IsStartTcpthreading = false;
                        continue;
                    }
                    
                    Receivestr = ASCIIEncoding.Default.GetString(receivebyte, 0, bytelen);
                    if (Receivestr.Trim() != "")
                    {
                        byte[] bytes = new byte[bytelen];
                        Array.Copy(receivebyte, 0, bytes, 0, bytelen);
                        //接收Byte原始数据
                        OnTcpClientReceviceByte(bytes);
                        //接收数据
                        //OnTcpClientReceviceEnterHead(Receivestr);
                    }
                }
                #endregion
                //此时线程将结束，人为结束，自动判断是否重连
                CreateTcpClient();
            }
            catch (Exception ex)
            {

                //CreateTcpClient();
                //返回错误信息
                OnTcpClientErrorMsgEnterHead("错误信息：" + ex.Message);
                OnTcpClientStateInfoEnterHead("错误：" + ex.Message, SocketState.Connecting);


            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void StopConnection()
        {

            IsStartTcpthreading = false;
            Isclosed = false;
            if (Tcpclient != null)
            {
                //关闭连接
                Tcpclient.Close();
            }
            if (Tcpthread != null)
            {
                Tcpthread.Interrupt();
                //关闭线程
                Tcpthread.Abort();
                //Tcpthread = null;
            }

            OnTcpClientStateInfoEnterHead("断开连接", SocketState.Disconnect);
            //标示线程已关闭可以重新连接
        }

        /// <summary>
        /// 发送Socket文本消息
        /// </summary>
        /// <param name="cmdstr"></param>
        public void SendCommand(string cmdstr)
        {
            try
            {
                //byte[] _out=Encoding.GetEncoding("GBK").GetBytes(cmdstr);
                byte[] _out = Encoding.Default.GetBytes(cmdstr);
                Tcpclient.Client.Send(_out);
            }
            catch (Exception ex)
            {
                //返回错误信息
                OnTcpClientErrorMsgEnterHead(ex.Message);
            }
        }

        public void SendFile(string filename)
        {
            Tcpclient.Client.BeginSendFile(filename,
                   new AsyncCallback(SendCallback), Tcpclient);
            //Tcpclient.Client.SendFile(filename);
        }
        private void SendCallback(IAsyncResult result)
        {
            try
            {
                TcpClient  tc= (TcpClient)result.AsyncState;

                // Complete sending the data to the remote device.
                tc.Client.EndSendFile(result);

            }
            catch (SocketException ex)
            {
            }
        }
        /// <summary>
        /// 发送Socket消息
        /// </summary>
        /// <param name="byteMsg"></param>
        public void SendCommand(byte[] byteMsg)
        {
            try
            {
                Tcpclient.Client.Send(byteMsg);
            }
            catch (Exception ex)
            {
                //返回错误信息
                OnTcpClientErrorMsgEnterHead("错误信息：" + ex.Message);
            }
        }
        #endregion

        #region 事件
        #region OnRecevice接收数据事件
        //public delegate void ReceviceEventHandler(string msg);
        //[Description("接收数据事件")]
        //[Category("TcpClient事件")]
        //public event ReceviceEventHandler OnRecevice;
        //protected virtual void OnTcpClientRecevice(string msg)
        //{
        //    if (OnRecevice != null)
        //        OnRecevice(msg);
        //}
        public delegate void ReceviceByteEventHandler(byte[] date);
        [Description("接收Byte数据事件")]
        [Category("TcpClient事件")]
        public event ReceviceByteEventHandler OnReceviceByte;
        protected virtual void OnTcpClientReceviceByte(byte[] date)
        {
            if (OnReceviceByte != null)
                OnReceviceByte(date);
        }
        #endregion

        #region OnErrorMsg返回错误消息事件
        public delegate void ErrorMsgEventHandler(string msg);
        [Description("返回错误消息事件")]
        [Category("TcpClient事件")]
        public event ErrorMsgEventHandler OnErrorMsg;
        protected virtual void OnTcpClientErrorMsgEnterHead(string msg)
        {
            if (OnErrorMsg != null)
                OnErrorMsg(msg);
        }
        #endregion

        #region OnStateInfo连接状态改变时返回连接状态事件
        public delegate void StateInfoEventHandler(string msg, SocketState state);
        [Description("连接状态改变时返回连接状态事件")]
        [Category("TcpClient事件")]
        public event StateInfoEventHandler OnStateInfo;
        protected virtual void OnTcpClientStateInfoEnterHead(string msg, SocketState state)
        {
            if (OnStateInfo != null)
                OnStateInfo(msg, state);
        }
        #endregion
        #endregion
    }
}
