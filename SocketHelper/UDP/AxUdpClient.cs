/********************************************************************
 * *
 * * Copyright (C) 2013-? Corporation All rights reserved.
 * * 作者： BinGoo QQ：315567586 
 * * 参考程序员之窗的作者Start2000的UPD代码，稍加修改并封装而成。
 * *
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：UDP客户端点对点通讯组件封装
 * *
********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SocketHelper
{
    public partial class AxUdpClient : Component
    {
        #region 构造函数
        public AxUdpClient()
        {
            InitializeComponent();
        }

        public AxUdpClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            
        } 
        #endregion

        #region 变量

        private bool _isAxUdpClientAgreement = true;
        private UdpLibrary _udpLibrary;
        /// <summary>
        /// 远程主机IP
        /// </summary>
        private string _remoteIp = "127.0.0.1";
        /// <summary>
        /// 远程主机端口
        /// </summary>
        private int _remotePort = 8900;
        /// <summary>
        /// 本地监听端口
        /// </summary>
        private int _localPort = 8899;
        /// <summary>
        /// 文件发送列表管理器
        /// </summary>
        private Dictionary<string, SendFileManager> _sendFileManagerList;
        /// <summary>
        /// 文件接收列表管理器
        /// </summary>
        private Dictionary<string, ReceiveFileManager> _receiveFileManagerList;

        private object _sendsyncLock = new object();
        private object _receivesyncLock = new object();
        #endregion

        #region 属性
        [Description("是否开启AxUdpClient内部数据传输协议。\r\n如果是false则接收原始数据\r\n如果是true则开启内部文件传输协议")]
        [Category("UDP客户端属性")]
        public bool IsAxUdpClientAgreement
        {
            get { return _isAxUdpClientAgreement; }
            set { _isAxUdpClientAgreement = value; }
        }
        [Description("UDP客户端基类")]
        [Category("UDP客户端属性")]
        public UdpLibrary UdpLibrary
        {
            get
            {
                if (_udpLibrary == null)
                {
                    _udpLibrary = new UdpLibrary(_localPort);
                    _udpLibrary.ReceiveData += new ReceiveDataEventHandler(UdpLibraryReceiveData);
                }
                return _udpLibrary;
            }
        }
        [Description("文件发送列表管理器")]
        [Category("UDP客户端属性")]
        public Dictionary<string, SendFileManager> SendFileManagerList
        {
            get
            {
                if (_sendFileManagerList == null)
                {
                    _sendFileManagerList = new Dictionary<string, SendFileManager>(10);
                }
                return _sendFileManagerList;
            }
        }
        [Description("文件接收列表管理器")]
        [Category("UDP客户端属性")]
        public Dictionary<string, ReceiveFileManager> ReceiveFileManagerList
        {
            get
            {
                if (_receiveFileManagerList == null)
                {
                    _receiveFileManagerList = new Dictionary<string, ReceiveFileManager>(10);
                }
                return _receiveFileManagerList;
            }
        }
        [Description("远程监听IP")]
        [Category("UDP客户端属性")]
        public string RemoteIp
        {
            get { return _remoteIp; }
            set { _remoteIp = value; }
        }
        [Description("远程监听端口")]
        [Category("UDP客户端属性")]
        public int RemotePort
        {
            get { return _remotePort; }
            set { _remotePort = value; }
        }
        [Description("本地监听IP")]
        [Category("UDP客户端属性")]
        public int LocalPort
        {
            get { return _localPort; }
            set { _localPort = value; }
        }
         [Description("远程主机网络端点")]
        [Category("UDP客户端属性")]
        public IPEndPoint RemoteEp
        {         
            get { return new IPEndPoint(IPAddress.Parse(_remoteIp), _remotePort); }
        }
         public FileTansfersContainer FileTansfersControl
         {
             get
             {
                 if (fileTansfersContainer == null)
                 {
                     fileTansfersContainer=new FileTansfersContainer();
                 }
                 return fileTansfersContainer;
             }
             set
             {
                 if(value==null)
                     fileTansfersContainer = new FileTansfersContainer();
                 else
                 {
                     fileTansfersContainer = value;
                 }
                     
             }
         }
        #endregion

        #region 方法
        /// <summary>
        /// 启动监听
        /// </summary>
        public void Start()
        {
            UdpLibrary.Start();
        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        public void Stop()
        {
            UdpLibrary.Stop();
        }
        /// <summary>
        /// 继承Udp基类接收数据方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UdpLibraryReceiveData(object sender, ReceiveDataEventArgs e)
        {
            //如果不是内部传输协议则接收原始数据
            if (!IsAxUdpClientAgreement)
            {
                OnReceiveByte(e);
                return;
            }
            //如果是内部接收协议，则软件双方必须按照协议来传输文件，否则会出错
            MsgCell cell = new MsgCell();
            cell.FromBuffer(e.Buffer);
            switch (cell.MessageId)
            {
                case (int)Command.RequestSendTextMSg:
                    OnReceiveTextMsg((MsgTypeCell)cell.Data);
                    break;
                case (int)Command.ResponeSendFile:
                    OnResponeSendFile((ResponeTraFransfersFile)cell.Data);
                    break;
                case (int)Command.ResponeSendFilePack:
                    OnResponeSendFilePack((ResponeTraFransfersFile)cell.Data);
                    break;
                case (int)Command.RequestCancelReceiveFile:
                    OnRequestCancelReceiveFile(cell.Data.ToString());
                    break;
                case (int)Command.RequestSendFile:
                    OnStartRecieve((TraFransfersFileStart)cell.Data, e.RemoteIP);
                    break;
                case (int)Command.RequestSendFilePack:
                    OnRecieveBuffer((TraFransfersFile)cell.Data, e.RemoteIP);
                    break;
                case (int)Command.RequestCancelSendFile:
                    OnRequestCancelSendFile(cell.Data.ToString(), e.RemoteIP);
                    break;
            }
        }

        /// <summary>
        /// 返回是否允许发送，在发送列表中的文件不能重复发送（避免文件被占用导致错误）
        /// </summary>
        /// <param name="sendFileManager"></param>
        /// <returns></returns>
        public bool CanSend(SendFileManager sendFileManager)
        {
            return !SendFileManagerList.ContainsKey(sendFileManager.MD5);
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileName">文件路径（包含完整的文件名）</param>
        public void SendFile(string fileName)
        {
            SendFileManager sendFileManager = new SendFileManager(fileName);
            Image img = Icon.ExtractAssociatedIcon(fileName).ToBitmap();
            SendFile(sendFileManager, img);
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sendFileManager">需要发送的文件类</param>
        /// <param name="image">文件ICO图标</param>
        public void SendFile(SendFileManager sendFileManager, Image image)
        {
            if (SendFileManagerList.ContainsKey(sendFileManager.MD5))
            {
                throw new Exception(string.Format(
                    "文件 {0} 正在发送，不能发送重复的文件。",
                    sendFileManager.FileName));
            }
            else
            {
                SendFileManagerList.Add(sendFileManager.MD5, sendFileManager);
                sendFileManager.ReadFileBuffer += new ReadFileBufferEventHandler(
                    SendFileManageReadFileBuffer);
                TraFransfersFileStart ts = new TraFransfersFileStart(
                    sendFileManager.MD5,
                    sendFileManager.Name,
                    image,
                    sendFileManager.Length,
                    sendFileManager.PartCount,
                    sendFileManager.PartSize);
                //添加
                AddSendItems(sendFileManager, image);
                Send((int)Command.RequestSendFile, ts);
            }
        }

        /// <summary>
        /// 取消发送
        /// </summary>
        /// <param name="md5">MD5校验文件</param>
        public void CancelSend(string md5)
        {
            SendFileManager sendFileManager;
            if (SendFileManagerList.TryGetValue(
                md5,
                out sendFileManager))
            {
                Send(
                    (int)Command.RequestCancelSendFile,
                    md5);
                lock (_sendsyncLock)
                {
                    SendFileManagerList.Remove(md5);
                    sendFileManager.Dispose();
                    sendFileManager = null;
                }
            }
        }
        /// <summary>
        /// 读取文件并发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendFileManageReadFileBuffer(
            object sender, ReadFileBufferEventArgs e)
        {
            SendFileManager sendFileManager = sender as SendFileManager;
            TraFransfersFile ts = new TraFransfersFile(
                sendFileManager.MD5, e.Index, e.Buffer);
            Send((int)Command.RequestSendFilePack, ts);
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="messageId">消息标识</param>
        /// <param name="data">序列化数据</param>
        public void Send(int messageId, object data)
        {
            Send(messageId, data, RemoteEp);
        }

        public void SendText(string strmsg)
        {
            if (!_isAxUdpClientAgreement)
            {
                byte[] dataBytes = Encoding.Default.GetBytes(strmsg);
                UdpLibrary.Send(dataBytes, RemoteEp);
            }
            else
            {
                byte[] dataBytes = Encoding.Default.GetBytes(strmsg);
                MsgTypeCell msgTypeCell = new MsgTypeCell(MsgType.TxtMsg, dataBytes);
                MsgCell cell = new MsgCell(0x000010, msgTypeCell);
                UdpLibrary.Send(cell, RemoteEp);
            }
            
        }
        public void SendText(byte[] strmsgBytes)
        {
            if (!_isAxUdpClientAgreement)
            {
                UdpLibrary.Send(strmsgBytes, RemoteEp);
            }
            else
            {
                MsgTypeCell msgTypeCell = new MsgTypeCell(MsgType.TxtMsg, strmsgBytes);
                MsgCell cell = new MsgCell(0x000010, msgTypeCell);
                UdpLibrary.Send(cell, RemoteEp);
            }
            
        }
        public void SendImage(Image img)
        {
            MsgTypeCell msgTypeCell = new MsgTypeCell(MsgType.Pic, ImageHelper.ImageToBytes(img));
            MsgCell cell = new MsgCell(0x000010, msgTypeCell);
            UdpLibrary.Send(cell, RemoteEp);
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="messageId">消息标识</param>
        /// <param name="data">序列化数据</param>
        /// <param name="remoteIp">远程主机IP</param>
        public void Send(int messageId, object data, IPEndPoint remoteIp)
        {
            MsgCell cell = new MsgCell(messageId, data);
            UdpLibrary.Send(cell, remoteIp);
        }
        /// <summary>
        /// 响应发送文件方法
        /// </summary>
        /// <param name="data"></param>
        private void OnResponeSendFile(ResponeTraFransfersFile data)
        {
            SendFileManager sendFileManager;
            if (!SendFileManagerList.TryGetValue(
                data.MD5,
                out sendFileManager))
            {
                return;
            }
            if (data.Size > 0)
            {
                OnFileSendBuffer(new FileSendBufferEventArgs(
                    sendFileManager,
                    data.Size));
            }
            if (data.Index == 0)
            {
                if (sendFileManager != null)
                {
                    OnFileSendAccept(new FileSendEventArgs(sendFileManager));
                    sendFileManager.Read(data.Index);
                }
            }
            else
            {
                if (data.Index == -1)
                {
                    OnFileSendRefuse(new FileSendEventArgs(sendFileManager));
                }
                SendFileManagerList.Remove(data.MD5);
                sendFileManager.Dispose();
            }
        }
        /// <summary>
        /// 响应发送文件包方法
        /// </summary>
        /// <param name="data"></param>
        private void OnResponeSendFilePack(ResponeTraFransfersFile data)
        {
            SendFileManager sendFileManager;
            if (!SendFileManagerList.TryGetValue(
                data.MD5,
                out sendFileManager))
            {
                return;
            }
            if (data.Size > 0)
            {
                OnFileSendBuffer(new FileSendBufferEventArgs(
                    sendFileManager,
                    data.Size));
            }
            if (data.Index >= 0)
            {
                if (sendFileManager != null)
                {
                    sendFileManager.Read(data.Index);
                }
            }
            else
            {
                if (data.Index == -1)
                {
                    OnFileSendRefuse(new FileSendEventArgs(sendFileManager));
                }
                else if (data.Index == -2)
                {
                    OnFileSendComplete(new FileSendEventArgs(sendFileManager));
                }
                SendFileManagerList.Remove(data.MD5);
                sendFileManager.Dispose();
            }
        }
        /// <summary>
        /// 请求取消接收文件方法
        /// </summary>
        /// <param name="md5"></param>
        private void OnRequestCancelReceiveFile(string md5)
        {
            SendFileManager sendFileManager;
            if (SendFileManagerList.TryGetValue(
                md5,
                out sendFileManager))
            {
                OnFileSendCancel(
                    new FileSendEventArgs(sendFileManager));
                lock (_sendsyncLock)
                {
                    SendFileManagerList.Remove(md5);
                    sendFileManager.Dispose();
                    sendFileManager = null;
                }
            }

            Send(
                (int)Command.ResponeCancelReceiveFile,
                "OK");
        }

        #region 接收方法
        /// <summary>
        /// 允许接收
        /// </summary>
        /// <param name="e"></param>
        public void AcceptReceive(RequestSendFileEventArgs e)
        {
            TraFransfersFileStart traFransfersFileStart = e.TraFransfersFileStart;
            IPEndPoint remoteIP = e.RemoteIP;
            ResponeTraFransfersFile responeTraFransfersFile;
            if (e.Cancel)
            {
                responeTraFransfersFile = new ResponeTraFransfersFile(
                    traFransfersFileStart.MD5, 0, -1);
                Send((int)Command.ResponeSendFile, responeTraFransfersFile, remoteIP);
            }
            else
            {
                ReceiveFileManager receiveFileManager;
                if (!ReceiveFileManagerList.TryGetValue(
                    traFransfersFileStart.MD5,
                    out receiveFileManager))
                {
                    receiveFileManager = new ReceiveFileManager(
                       traFransfersFileStart.MD5,
                       e.Path,
                       traFransfersFileStart.FileName,
                       traFransfersFileStart.PartCount,
                       traFransfersFileStart.PartSize,
                       traFransfersFileStart.Length,
                       remoteIP);
                    receiveFileManager.ReceiveFileComplete +=
                        new FileReceiveCompleteEventHandler(
                        ReceiveFileManagerReceiveFileComplete);
                    receiveFileManager.ReceiveFileTimeout +=
                        new EventHandler(ReceiveFileManagerReceiveFileTimeout);
                    ReceiveFileManagerList.Add(
                        traFransfersFileStart.MD5,
                        receiveFileManager);
                    receiveFileManager.Start();
                }
                responeTraFransfersFile = new ResponeTraFransfersFile(
                    traFransfersFileStart.MD5, 0, 0);
                Send((int)Command.ResponeSendFile, responeTraFransfersFile, remoteIP);
            }
        }
        /// <summary>
        /// 取消接收
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="remoteIP"></param>
        public void CancelReceive(string md5, IPEndPoint remoteIP)
        {
            ReceiveFileManager receiveFileManager;
            if (ReceiveFileManagerList.TryGetValue(
                md5,
                out receiveFileManager))
            {
                Send(
                    (int)Command.RequestCancelReceiveFile,
                    md5,
                    remoteIP);
                lock (_receivesyncLock)
                {
                    ReceiveFileManagerList.Remove(md5);
                    receiveFileManager.Dispose();
                    receiveFileManager = null;
                }
            }
        }
        /// <summary>
        /// 完成接收文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiveFileManagerReceiveFileComplete(
            object sender, FileReceiveCompleteEventArgs e)
        {
            ReceiveFileManager receiveFileManager =
                sender as ReceiveFileManager;
            OnFileReceiveComplete(new FileReceiveEventArgs(receiveFileManager));
            ReceiveFileManagerList.Remove(receiveFileManager.MD5);
        }

        private void ReceiveFileManagerReceiveFileTimeout(
            object sender, EventArgs e)
        {
            ReceiveFileManager receiveFileManager =
                sender as ReceiveFileManager;
            ResponeTraFransfersFile responeTraFransfersFile =
                new ResponeTraFransfersFile(
                receiveFileManager.MD5,
                0,
                receiveFileManager.GetNextReceiveIndex());
            Send(
                (int)Command.ResponeSendFilePack,
                responeTraFransfersFile,
                receiveFileManager.RemoteIP);
        }
        #endregion
       
        #endregion

        #region 事件

        #region 发送文件事件

        #region 文件发送被取消事件(当发送文件正在被接收中时对方取消接收)
        [Description("文件发送被取消事件\r\n(当发送文件正在被接收中时对方取消接收)")]
        public event FileSendEventHandler FileSendCancel;
        /// <summary>
        /// 文件发送时被取消时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileSendCancel(FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            if (FileSendCancel != null)
            {
                FileSendCancel(this, e);
            }
        }
        #endregion

        #region 发送文件被允许接收时触发事件
        [Description("发送文件被允许接收时触发事件")]
        public event FileSendEventHandler FileSendAccept;
        /// <summary>
        /// 文件被接收时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileSendAccept(FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
                {
                    item.Start();
                }));
            }
            if (FileSendAccept != null)
            {
                FileSendAccept(this, e);
            }
        }
        #endregion

        #region 文件正在发送时触发事件
        [Description("文件正在发送时触发事件")]
        public event FileSendBufferEventHandler FileSendBuffer;
        /// <summary>
        /// 文件正在发送时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileSendBuffer(FileSendBufferEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += e.Size;
                }));
            }
            if (FileSendBuffer != null)
            {
                FileSendBuffer(this, e);
            }
        }  
        #endregion

        #region 发送文件被拒绝接收时触发事件
        [Description("发送文件被拒绝接收时触发事件")]
        public event FileSendEventHandler FileSendRefuse;
        /// <summary>
        /// 发送文件被拒绝接收时触发事件
        /// </summary>Refuse to receive file
        /// <param name="e"></param>
        protected virtual void OnFileSendRefuse(FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            if (FileSendRefuse != null)
            {
                FileSendRefuse(this, e);
            }
        }
        #endregion

        #region 文件发送完成时触发事件
        [Description("文件发送完成时触发事件")]
        public event FileSendEventHandler FileSendComplete;
        /// <summary>
        /// 文件发送完成时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileSendComplete(FileSendEventArgs e)
        {
            FileTransfersItem item =
                e.SendFileManager.Tag as FileTransfersItem;
            if (item != null)
            {
                Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
                {
                    fileTansfersContainer.RemoveItem(item);
                    item.Dispose();
                }));
            }
            if (FileSendComplete != null)
            {
                FileSendComplete(this, e);
            }
        }
        #endregion

        #region 取消发送文件事件方法
        /// <summary>
        /// 取消发送文件事件方法
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="remoteIP"></param>
        private void OnRequestCancelSendFile(string md5, IPEndPoint remoteIP)
        {
            ReceiveFileManager receiveFileManager;
            if (ReceiveFileManagerList.TryGetValue(
                md5,
                out receiveFileManager))
            {
                OnFileReceiveCancel(
                    new FileReceiveEventArgs(receiveFileManager));
                lock (_receivesyncLock)
                {
                    ReceiveFileManagerList.Remove(md5);
                    receiveFileManager.Dispose();
                    receiveFileManager = null;
                }
            }
            else
            {
                FileReceiveEventArgs fe = new FileReceiveEventArgs();
                fe.Tag = md5;
                OnFileReceiveCancel(fe);
            }
            Send(
                (int)Command.ResponeCancelSendFile,
                "OK",
                remoteIP);
        } 
        #endregion
        #endregion

        #region 接收文件事件

        #region 接收文本数据事件
        public delegate void ReceiveTextMsgEventHandler(MsgTypeCell msgTypeCell);
        [Description("接收文本数据事件")]
        public event ReceiveTextMsgEventHandler ReceiveTextMsg;
        /// <summary>
        /// 文件被接收时触发事件
        /// </summary>
        /// <param name="bytes"></param>
        protected virtual void OnReceiveTextMsg(MsgTypeCell msgTypeCell)
        {
            if (ReceiveTextMsg != null)
            {
                ReceiveTextMsg(msgTypeCell);
            }
        } 
        #endregion

        #region 请求接收文件响应时触发事件
        [Description("请求接收文件响应时触发事件")]
        public event RequestSendFileEventHandler FileRecieveRequest;
        /// <summary>
        /// 请求接收文件响应时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileRecieveRequest(RequestSendFileEventArgs e)
        {
            AddReadyReceiveItem(e);
            if (FileRecieveRequest != null)
            {
                FileRecieveRequest(this, e);
                
            }
        }
        #endregion

        #region 文件被读取时（正在读取）触发事件
        [Description("文件被读取时（正在读取）触发事件")]
        public event FileReceiveBufferEventHandler FileReceiveBuffer;
        /// <summary>
        /// 文件被读取时（正在读取）触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileReceiveBuffer(FileReceiveBufferEventArgs e)
        {
            FileTransfersItem item = fileTansfersContainer.Search(
                e.ReceiveFileManager.MD5);
            if (item != null)
            {
                Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
                {
                    item.TotalTransfersSize += e.Size;
                }));
            }
            if (FileReceiveBuffer != null)
            {
                FileReceiveBuffer(this, e);
            }
        } 
        #endregion

        #region 文件接收完成时触发事件
        [Description("文件接收完成时触发事件")]
        public event FileReceiveEventHandler FileReceiveComplete;
        /// <summary>
        /// 文件接收完成时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileReceiveComplete(FileReceiveEventArgs e)
        {
            Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
            {
                fileTansfersContainer.RemoveItem(e.ReceiveFileManager.MD5);
            }));
            if (FileReceiveComplete != null)
            {
                FileReceiveComplete(this, e);
            }
        } 
        #endregion

        #region 文件接收时被取消发送触发事件
        [Description("接收文件被取消时触发事件\r\n(当正在接收对方文件时对方取消发送)")]
        public event FileReceiveEventHandler FileReceiveCancel;
        /// <summary>
        /// 文件接收时被取消发送触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFileReceiveCancel(FileReceiveEventArgs e)
        {
            string md5 = string.Empty;
            if (e.ReceiveFileManager != null)
            {
                md5 = e.ReceiveFileManager.MD5;
            }
            else
            {
                md5 = e.Tag.ToString();
            }

            FileTransfersItem item = fileTansfersContainer.Search(md5);

            Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
            {
                fileTansfersContainer.RemoveItem(item);
            }));
            if (FileReceiveCancel != null)
            {
                FileReceiveCancel(this, e);
            }
        } 
        #endregion

        #region 接收文件事件方法
        /// <summary>
        /// 接收文件事件方法
        /// </summary>
        /// <param name="traFransfersFile"></param>
        /// <param name="remoteEp"></param>
        private void OnRecieveBuffer(
            TraFransfersFile traFransfersFile,
            IPEndPoint remoteEp)
        {
            ReceiveFileManager receiveFileManager;

            if (!ReceiveFileManagerList.TryGetValue(
                traFransfersFile.MD5,
                out receiveFileManager))
            {
                return;
            }
            if (receiveFileManager != null)
            {
                ResponeTraFransfersFile responeTraFransfersFile;
                int size = receiveFileManager.ReceiveBuffer(
                    traFransfersFile.Index,
                    traFransfersFile.Buffer);

                if (receiveFileManager.Completed)
                {
                    responeTraFransfersFile = new ResponeTraFransfersFile(
                        traFransfersFile.MD5,
                        size,
                        -2);
                    Send(
                        (int)Command.ResponeSendFilePack,
                        responeTraFransfersFile,
                        remoteEp);
                }
                else
                {
                    responeTraFransfersFile = new ResponeTraFransfersFile(
                        traFransfersFile.MD5,
                        size,
                        receiveFileManager.GetNextReceiveIndex());
                    Send(
                        (int)Command.ResponeSendFilePack,
                        responeTraFransfersFile,
                        remoteEp);
                }
                OnFileReceiveBuffer(
                    new FileReceiveBufferEventArgs(
                    receiveFileManager, traFransfersFile.Buffer.Length));
            }
        } 
        #endregion

        #region 开始接收文件事件方法（尚未开始接收文件）
        /// <summary>
        /// 开始接收文件事件方法（尚未开始接收文件）
        /// </summary>
        /// <param name="traFransfersFileStart"></param>
        /// <param name="remoteEp"></param>
        private void OnStartRecieve(
            TraFransfersFileStart traFransfersFileStart,
            IPEndPoint remoteEp)
        {
            OnFileRecieveRequest(
                new RequestSendFileEventArgs(
                traFransfersFileStart,
                remoteEp));
        } 
        #endregion

        
        #endregion

        #region 接收原始数据事件
        public delegate void ReceiveByteEventHandler(ReceiveDataEventArgs e);
        [Description("接收文本数据事件")]
        public event ReceiveByteEventHandler ReceiveByte;
        /// <summary>
        /// 文件被接收时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReceiveByte(ReceiveDataEventArgs e)
        {
            if (ReceiveByte != null)
            {
                ReceiveByte(e);
            }
        } 
        #endregion
        #endregion

        #region FileTransfersItem子项

        #region Items变量
        private Color _baseColor = Color.FromArgb(255, 192, 128);
        private Color _borderColor = Color.FromArgb(224, 224, 224);
        private Color _progressBarBarColor = Color.SteelBlue;
        private Color _progressBarBorderColor = Color.LightGray;
        private Color _progressBarTextColor = Color.White;
        #endregion

        #region 接收文件子项控件

        /// <summary>
        /// 添加准备接收文件Item
        /// </summary>
        /// <param name="e"></param>
        public void AddReadyReceiveItem(RequestSendFileEventArgs e)
        {
            TraFransfersFileStart traFransfersFileStart = e.TraFransfersFileStart;

            Form.ActiveForm.BeginInvoke(new MethodInvoker(delegate()
            {
                FileTransfersItem item = fileTansfersContainer.AddItem(
                    traFransfersFileStart.MD5,
                    "接收文件",
                    traFransfersFileStart.FileName,
                    traFransfersFileStart.Image,
                    traFransfersFileStart.Length,
                    FileTransfersItemStyle.ReadyReceive);

                item.BaseColor = _baseColor;
                item.BorderColor = _borderColor;
                item.ProgressBarBarColor = _progressBarBarColor;
                item.ProgressBarBorderColor = _progressBarBorderColor;
                item.ProgressBarTextColor = _progressBarTextColor;
                item.Tag = e;
                item.SaveButtonClick += new EventHandler(ItemSaveButtonClick);
                item.SaveToButtonClick += new EventHandler(ItemSaveToButtonClick);
                item.RefuseButtonClick += new EventHandler(ItemRefuseButtonClick);
                fileTansfersContainer.ResumeLayout(true);
            }));

        }

        #region 按钮事件
        /// <summary>
        /// 文件另存为按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSaveToButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                rse.Path = fbd.SelectedPath;
                ControlTag tag = new ControlTag(
                    rse.TraFransfersFileStart.MD5,
                    rse.TraFransfersFileStart.FileName,
                    rse.RemoteIP);
                item.Tag = tag;
                item.Style = FileTransfersItemStyle.Receive;
                item.CancelButtonClick += new EventHandler(ItemCancelButtonClick);
                item.Start();

                this.AcceptReceive(rse);
            }
        }

        /// <summary>
        /// 保存文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSaveButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            //自动保存在程序根目录下
            rse.Path = Application.StartupPath;
            ControlTag tag = new ControlTag(
                rse.TraFransfersFileStart.MD5,
                rse.TraFransfersFileStart.FileName,
                rse.RemoteIP);
            item.Tag = tag;
            item.Style = FileTransfersItemStyle.Receive;
            item.CancelButtonClick += new EventHandler(ItemCancelButtonClick);
            item.Start();
            this.AcceptReceive(rse);
        }

        /// <summary>
        /// 拒绝接收文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemRefuseButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            RequestSendFileEventArgs rse = item.Tag as RequestSendFileEventArgs;
            rse.Cancel = true;
            fileTansfersContainer.RemoveItem(item);
            item.Dispose();
            AcceptReceive(rse);
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCancelButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item = sender as FileTransfersItem;
            ControlTag tag = item.Tag as ControlTag;
            CancelReceive(tag.MD5, tag.RemoteIP);
            fileTansfersContainer.RemoveItem(item);
            item.Dispose();
        }  
        #endregion
        #endregion

        #region 发送文件子项控件
        /// <summary>
        /// 添加发送文件控件Item
        /// </summary>
        /// <param name="sendFileManager"></param>
        /// <param name="image"></param>
        public void AddSendItems(SendFileManager sendFileManager, Image image)
        {

            FileTransfersItem item = fileTansfersContainer.AddItem(
                sendFileManager.MD5,
                "发送文件",
                sendFileManager.Name,
                image,
                sendFileManager.Length,
                FileTransfersItemStyle.Send);
            item.BaseColor = Color.FromArgb(224, 224, 224);
            item.BorderColor = _borderColor;
            item.ProgressBarBarColor = _progressBarBarColor;
            item.ProgressBarBorderColor = _progressBarBorderColor;
            item.ProgressBarTextColor = _progressBarTextColor;
            item.CancelButtonClick += new EventHandler(ItemSendCancelButtonClick);
            item.Tag = sendFileManager;
            sendFileManager.Tag = item;
        }
        #region 按钮事件

        private void ItemSendCancelButtonClick(object sender, EventArgs e)
        {
            FileTransfersItem item =
                sender as FileTransfersItem;
            SendFileManager sendFileManager =
                item.Tag as SendFileManager;
            this.CancelSend(sendFileManager.MD5);

            fileTansfersContainer.RemoveItem(item);
        }

        #endregion
        #endregion
        #endregion
    }
}
