/********************************************************************
 * *
 * * Copyright (C) 2013-? Corporation All rights reserved.
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：Socket服务端封装组件
 * *
********************************************************************/
#region 说明
/* 简介：基于底层socket的服务端监听，非TcpListener
 * 功能介绍：基于底层的Socket服务端监听，监听客户端连接，接收客户端发送的数据，发送数据给客户端，心跳包(代码已注释，根据需要将代码取消注释)
 * socket服务端监听封装组件使用方法
 * 1、设置属性：
 * ServerIp  本机IP（可以是本地IP、局域网IP、外网IP）
 * ServerPort 监听端口
 * 
 * 2、启动监听和关闭监听
 * _tcpServer.Start();
 *  _tcpServer.Stop(); 
 *  
 * 3、组件提供三个主要事件
 * ①、OnReceviceByte(Socket temp, byte[] dataBytes)；接收数据事件
 * ②、OnErrorMsg(string msg)；返回错误消息事件
 * ③、OnReturnClientCount(int count)；用户上线下线时更新客户端在线数量事件
 * ④、OnStateInfo(string msg, SocketState state)；连接状态改变时返回连接状态事件
 * ⑤、OnAddClient(Socket temp)；新客户端上线时返回客户端事件
 * ⑥、OnOfflineClient(Socket temp)；客户端下线时返回客户端事件
  */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketHelper
{
    public partial class AxTcpServer : Component
    {

        #region 构造函数
        public AxTcpServer()
        {
            InitializeComponent();
            ClientSocketList = new List<Socket>();
            ClientSocketList.Clear();
        }

        public AxTcpServer(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            #region 初始化委托方法
            #endregion
            ClientSocketList = new List<Socket>();
            ClientSocketList.Clear();
        } 
        #endregion

        #region 变量属性

        /// <summary>
        /// 监听Socket
        /// </summary>
        public Socket ServerSocket;
        /// <summary>
        /// 监听线程
        /// </summary>
        public Thread StartSockst;
        /// <summary>
        /// 本机监听IP,默认是本地ip
        /// </summary>
        private string _serverIp = "127.0.0.1";
        [Description("本机监听IP,默认是本地IP")]
        [Category("TCP服务端")]
        public string ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }
        /// <summary>
        /// 监听端口
        /// </summary>
        private int _serverPort = 5000;
        
        [Description("本机监听端口,默认是5000")]
        [Category("TCP服务端")]
        public int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }
        /// <summary>
        /// 是否已启动监听
        /// </summary>
        public bool IsStartListening = false;
        /// <summary>
        /// 客户端列表
        /// </summary>
        public List<Socket> ClientSocketList = new List<Socket>();
        #endregion

        #region 方法
        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start()
        {
            try
            {
                //若已开始监听，则不在开启线程监听，直至关闭监听后才能再次开启监听
                if (IsStartListening)
                    return;
                //启动线程打开监听
                StartSockst = new Thread(new ThreadStart(StartSocketListening));
                StartSockst.Start();
            }
            catch (SocketException ex)
            {
                OnTcpServerErrorMsgEnterHead(ex.Message);
            }
        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        public void Stop()
        {
            try
            {
                IsStartListening = false;
                if (ServerSocket != null)
                {
                    ServerSocket.Close();
                }

                //if (StartSockst != null)
                //{
                //    StartSockst.Interrupt();
                //    StartSockst.Abort();
                //}
                
                OnTcpServerStateInfoEnterHead(string.Format("服务端Ip:{0},端口:{1}已停止监听", ServerIp, ServerPort), SocketState.StopListening);
                for (int i = 0; i < ClientSocketList.Count; i++)
                {
                    OnTcpServerOfflineClientEnterHead(ClientSocketList[i]);
                    ClientSocketList[i].Shutdown(SocketShutdown.Both);
                }
                GC.Collect();

            }
            catch 
            {
            }
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void StartSocketListening()
        {
            try
            {

                //获取本机IP:
                //string strip = //Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

                //ServerIp = strip;
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //绑定监听
                ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(ServerIp), ServerPort));
                ServerSocket.Listen(10000);
                //标记准备就绪，开始监听
                IsStartListening = true;
                OnTcpServerStateInfoEnterHead(string.Format("服务端Ip:{0},端口:{1}已启动监听", ServerIp, ServerPort), SocketState.StartListening);
                while (IsStartListening)
                {
                    //阻塞挂起直至有客户端连接
                    Socket clientSocket = ServerSocket.Accept();
                    try
                    {
                        Thread.Sleep(10);
                        //添加客户端用户
                        ClientSocketList.Add(clientSocket);
                        string ip = ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
                        string port = ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString();
                        OnTcpServerStateInfoEnterHead("<" + ip + "：" + port + ">---上线", SocketState.ClientOnline);
                        OnTcpServerOnlineClientEnterHead(clientSocket);
                       OnTcpServerReturnClientCountEnterHead(ClientSocketList.Count);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ClientSocketCallBack), clientSocket);
                    }
                    catch (Exception ex)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                      OnTcpServerOfflineClientEnterHead(clientSocket);
                        ClientSocketList.Remove(clientSocket);
                        //DelegateHelper.TcpServerErrorMsg("网络通讯异常，异常原因：" + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                //其他错误原因
                OnTcpServerErrorMsgEnterHead(ex.Message);
            }
        }

        /// <summary>
        /// 线程池回调
        /// </summary>
        /// <param name="obj"></param>

        public void ClientSocketCallBack(Object obj)
        {

            Socket temp = (Socket) obj;
            while (IsStartListening)
            {
                Thread.Sleep(10);
                byte[] receivebyte = new byte[1024];
                MemoryStream mStream = new MemoryStream();
                mStream.Position = 0;
                //接收数据长度
                int bytelen;
                try
                {
                    //可自定心跳包数据
                    //temp.Send(System.Text.Encoding.Default.GetBytes("&conn&")); //心跳检测socket连接
                    bytelen = temp.Receive(receivebyte);
                    if (bytelen > 0)
                    {
                        //接收客户端数据
                        byte[] bytes = new byte[bytelen];
                        Array.Copy(receivebyte, 0, bytes, 0, bytelen);
                        OnTcpServerReceviceByte(temp, bytes);
                        ////接收客户端数据
                        //string clientRecevieStr = ASCIIEncoding.Default.GetString(receivebyte, 0, bytelen);
                        //OnTcpServerRecevice(temp, clientRecevieStr);
                        //DelegateHelper.TcpServerReceive(temp, clientRecevieStr);
                    }
                    else if (bytelen == 0)
                    {
                        //接收到数据时数据长度一定是>0，若为0则表示客户端断线
                        ClientSocketList.Remove(temp);
                        string ip = ((IPEndPoint) temp.RemoteEndPoint).Address.ToString();
                        string port = ((IPEndPoint) temp.RemoteEndPoint).Port.ToString();
                        OnTcpServerStateInfoEnterHead("<" + ip + "：" + port + ">---下线",
                            SocketState.ClientOnOff);
                        OnTcpServerOfflineClientEnterHead(temp);
                        OnTcpServerReturnClientCountEnterHead(ClientSocketList.Count);
                        try
                        {
                            temp.Shutdown(SocketShutdown.Both);
                        }
                        catch
                        {
                        }
                        break;
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="ip">客户端IP</param>
        /// <param name="port">客户端端口</param>
        /// <param name="strData">发送的文本字符串</param>
        public void SendData(string ip, int port, string strData)
        {
            Socket socket = ResoultSocket(ip, port);
            try
            {

                if (socket != null)
                    socket.Send((System.Text.Encoding.Default.GetBytes(strData)));

            }
            catch (SocketException ex)
            {
                if (socket != null)
                    socket.Shutdown(SocketShutdown.Both);
                OnTcpServerErrorMsgEnterHead(ex.Message);
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="ip">客户端IP</param>
        /// <param name="port">客户端端口</param>
        /// <param name="dataBytes">发送的字节数组数据</param>
        public void SendData(string ip, int port, byte[] dataBytes)
        {
            Socket socket = ResoultSocket(ip, port);
            try
            {

                if (socket != null)
                    socket.Send(dataBytes);

            }
            catch (SocketException ex)
            {
                if (socket != null)
                    socket.Shutdown(SocketShutdown.Both);
                OnTcpServerErrorMsgEnterHead(ex.Message);
            }
        }
        /// <summary>
        /// 根据IP,端口查找Socket客户端
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Socket ResoultSocket(string ip, int port)
        {
            Socket sk = null;
            try
            {
                foreach (Socket socket in ClientSocketList)
                {
                    if (((IPEndPoint)socket.RemoteEndPoint).Address.ToString().Equals(ip)
                        && port == ((IPEndPoint)socket.RemoteEndPoint).Port)
                    {
                        sk = socket;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                OnTcpServerErrorMsgEnterHead(ex.Message);
            }
            return sk;
        }
        #endregion

        #region 事件
        #region OnRecevice接收数据事件
        //public delegate void ReceviceEventHandler(Socket temp, string msg);
        //[Description("接收数据事件")]
        //[Category("TcpServer事件")]
        //public event ReceviceEventHandler OnRecevice;
        //protected virtual void OnTcpServerRecevice(Socket temp, string msg)
        //{
        //    if (OnRecevice != null)
        //        OnRecevice(temp, msg);
        //}
        public delegate void ReceviceByteEventHandler(Socket temp, byte[] dataBytes);
        [Description("接收原始Byte数组数据事件")]
        [Category("TcpServer事件")]
        public event ReceviceByteEventHandler OnReceviceByte;
        protected virtual void OnTcpServerReceviceByte(Socket temp, byte[] dataBytes)
        {
            if (OnReceviceByte != null)
                OnReceviceByte(temp, dataBytes);
        } 
        #endregion

        #region OnErrorMsg返回错误消息事件
        public delegate void ErrorMsgEventHandler(string msg);
        [Description("错误消息")]
        [Category("TcpServer事件")]
        public event ErrorMsgEventHandler OnErrorMsg;
        protected virtual void OnTcpServerErrorMsgEnterHead(string msg)
        {
            if (OnErrorMsg != null)
                OnErrorMsg(msg);
        } 
        #endregion

        #region OnReturnClientCount用户上线下线时更新客户端在线数量事件
        public delegate void ReturnClientCountEventHandler(int count);
        [Description("用户上线下线时更新客户端在线数量事件")]
        [Category("TcpServer事件")]
        public event ReturnClientCountEventHandler OnReturnClientCount;
        protected virtual void OnTcpServerReturnClientCountEnterHead(int count)
        {
            if (OnReturnClientCount != null)
                OnReturnClientCount(count);
        } 
        #endregion

        #region OnStateInfo监听状态改变时返回监听状态事件
        public delegate void StateInfoEventHandler(string msg, SocketState state);
        [Description("监听状态改变时返回监听状态事件")]
        [Category("TcpServer事件")]
        public event StateInfoEventHandler OnStateInfo;
        protected virtual void OnTcpServerStateInfoEnterHead(string msg, SocketState state)
        {
            if (OnStateInfo != null)
                OnStateInfo(msg, state);
        } 
        #endregion

        #region OnAddClient新客户端上线时返回客户端事件
        public delegate void AddClientEventHandler(Socket temp);
        [Description("新客户端上线时返回客户端事件")]
        [Category("TcpServer事件")]
        public event AddClientEventHandler OnOnlineClient;
        protected virtual void OnTcpServerOnlineClientEnterHead(Socket temp)
        {
            if (OnOnlineClient != null)
                OnOnlineClient(temp);
        } 
        #endregion

        #region OnOfflineClient客户端下线时返回客户端事件
        public delegate void OfflineClientEventHandler(Socket temp);
        [Description("客户端下线时返回客户端事件")]
        [Category("TcpServer事件")]
        public event AddClientEventHandler OnOfflineClient;
        protected virtual void OnTcpServerOfflineClientEnterHead(Socket temp)
        {
            if (OnOfflineClient != null)
                OnOfflineClient(temp);
        }
        #endregion
        #endregion
    }
}
