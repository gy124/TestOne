/********************************************************************
 * *
 * * Copyright (C) 2013-? Corporation All rights reserved.
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：
 * *
********************************************************************/
#region 说明
/* 简介：基于底层socket的服务端监听，非TcpListener
 * 功能介绍：基于底层的Socket服务端监听，监听客户端连接，接收客户端发送的数据，发送数据给客户端，心跳包(代码已注释，根据需要将代码取消注释)
 * socket服务端监听封装类的调用三步：
 * 1、初始化：
 * int port=5100
 * TCPServer _tcpServer=new TCPServer(port);
 * 
 * 2、创建委托接收数据方法并绑定（可根据需求定义），此类暂时定义了四种接收数据的委托：返回接收客户端的数据，返回客户端连接状态和监听状态，返回错误信息，返回客户端数量的委托
 * 
 * ①申明返回接收数据信息的委托方法
 * DelegateHelper.TcpServerReceive= 自定义方法;
 * 
 * ②申明返回状态信息的委托方法
 * DelegateHelper.TcpServerStateInfo= 自定义方法;
 * 
 * ③申明放回错误信息的委托方法
 * DelegateHelper.TcpServerErrorMsg = 自定义方法;
 * 
 * ④申明返回客户端数量档位委托方法
 * DelegateHelper.ReturnClientCountCallBack = 自定义方法;
 * 
 * 3、启动监听和关闭监听
 * _tcpServer.Start();
 *  _tcpServer.Stop(); 
	
  */
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketHelper
{
    public class TCPServer
    {
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
        /// 本机监听IP
        /// </summary>
        public string ServerIp = "127.0.0.1";
        /// <summary>
        /// 监听端口
        /// </summary>
        public int ServerPort = 5100;
        /// <summary>
        /// 是否已启动监听
        /// </summary>
        public bool IsStartListening = false;
        /// <summary>
        /// 客户端列表
        /// </summary>
        public List< Socket>ClientSocketList=new List<Socket>();
        #endregion

        #region 构造函数
        public TCPServer(int port)
        {
            #region 初始化委托方法
            //接收数据
            if (DelegateHelper.TcpServerReceive == null)
            {
                DelegateHelper.TcpServerReceive = DelegateHelper.BaseVoid;
            }
            //接收错误信息
            if (DelegateHelper.TcpServerErrorMsg == null)
            {
                DelegateHelper.TcpServerErrorMsg = DelegateHelper.BaseVoid;
            }
            //接收状态信息
            if (DelegateHelper.TcpServerStateInfo == null)
            {
                DelegateHelper.TcpServerStateInfo = DelegateHelper.BaseVoid;
            }
            //添加客户端委托
            if (DelegateHelper.TcpServerAddClient == null)
            {
                DelegateHelper.TcpServerAddClient = DelegateHelper.BaseVoid;
            }
            //删除客户端委托
            if (DelegateHelper.TcpServerDelClient == null)
            {
                DelegateHelper.TcpServerDelClient = DelegateHelper.BaseVoid;
            } 
            #endregion
            //ServerIp = ip;
            ServerPort = port;
            ClientSocketList = new List<Socket>();
            ClientSocketList.Clear();
        } 
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
                DelegateHelper.TcpServerErrorMsg(ex.Message);
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
                StartSockst.Interrupt();
                StartSockst.Abort();
                ServerSocket.Close();
                DelegateHelper.TcpServerStateInfo(string.Format("服务端Ip:{0},端口:{1}已停止监听", ServerIp, ServerPort),EnumClass.SocketState.StopListening);
                for (int i = 0; i < ClientSocketList.Count; i++)
                {
                    DelegateHelper.TcpServerDelClient(ClientSocketList[i]);
                    ClientSocketList[i].Shutdown(SocketShutdown.Both);
                }
                GC.Collect();
                
            }
            catch (SocketException ex)
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
                string strip = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

                ServerIp = strip;
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //绑定监听
                ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(ServerIp), ServerPort));
                ServerSocket.Listen(10000);
                //标记准备就绪，开始监听
                IsStartListening = true;
                DelegateHelper.TcpServerStateInfo(string.Format("服务端Ip:{0},端口:{1}已启动监听", ServerIp, ServerPort), EnumClass.SocketState.StartListening);
                while (IsStartListening)
                {
                    //阻塞挂起直至有客户端连接
                    Socket clientSocket = ServerSocket.Accept();
                    try
                    {
                        Thread.Sleep(10);
                        //添加客户端用户
                        ClientSocketList.Add(clientSocket);
                        string ip = ((IPEndPoint) clientSocket.RemoteEndPoint).Address.ToString();
                        string port = ((IPEndPoint) clientSocket.RemoteEndPoint).Port.ToString();
                        DelegateHelper.TcpServerStateInfo("<" + ip + "：" + port + ">---上线",EnumClass.SocketState.ClientOnline);
                        DelegateHelper.TcpServerAddClient(clientSocket);
                        DelegateHelper.ReturnClientCountCallBack(ClientSocketList.Count.ToString());
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ClientSocketCallBack), clientSocket);
                    }
                    catch (Exception ex)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        DelegateHelper.TcpServerDelClient(clientSocket);
                        ClientSocketList.Remove(clientSocket);
                        //DelegateHelper.TcpServerErrorMsg("网络通讯异常，异常原因：" + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                //其他错误原因
                DelegateHelper.TcpServerErrorMsg(ex.Message);
            }
        }

        /// <summary>
        /// 线程池回调
        /// </summary>
        /// <param name="obj"></param>

        public void ClientSocketCallBack(Object obj)
        {
            Socket temp = (Socket)obj;
            while (IsStartListening)
            {
                Thread.Sleep(10);
                byte[] recvMessage = new byte[1024];
                int bytes;
                try
                {
                    //可自定心跳包数据
                    //temp.Send(System.Text.Encoding.Default.GetBytes("&conn&")); //心跳检测socket连接
                    bytes = temp.Receive(recvMessage);
                    if (bytes > 0)
                    {
                        //接收客户端数据
                        string clientRecevieStr = ASCIIEncoding.Default.GetString(recvMessage, 0, bytes);
                        DelegateHelper.TcpServerReceive(temp, clientRecevieStr);
                    }
                    else if (bytes == 0)
                    {
                        //接收到数据时数据长度一定是>0，若为0则表示客户端断线
                        ClientSocketList.Remove(temp);
                        string ip = ((IPEndPoint)temp.RemoteEndPoint).Address.ToString();
                        string port = ((IPEndPoint)temp.RemoteEndPoint).Port.ToString();
                        DelegateHelper.TcpServerStateInfo("<" + ip + "：" + port + ">---下线",EnumClass.SocketState.ClientOnOff);
                        DelegateHelper.TcpServerDelClient(temp);
                        DelegateHelper.ReturnClientCountCallBack(ClientSocketList.Count.ToString());
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
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="strData"></param>
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
                DelegateHelper.TcpServerErrorMsg(ex.Message);
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
                DelegateHelper.TcpServerErrorMsg(ex.Message);
            }
            return sk;
        } 
        #endregion
    }
}
