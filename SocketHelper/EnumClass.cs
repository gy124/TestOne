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
using System;
using System.Collections.Generic;
using System.Text;

namespace SocketHelper
{
    /// <summary>
    /// Socket状态枚举
    /// </summary>
    public enum SocketState
    {
        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting = 0,

        /// <summary>
        /// 已连接
        /// </summary>
        Connected = 1,

        /// <summary>
        /// 重新连接
        /// </summary>
        Reconnection = 2,

        /// <summary>
        /// 断开连接
        /// </summary>
        Disconnect = 3,

        /// <summary>
        /// 正在监听
        /// </summary>
        StartListening = 4,

        /// <summary>
        /// 停止监听
        /// </summary>
        StopListening = 5,

        /// <summary>
        /// 客户端上线
        /// </summary>
        ClientOnline = 6,

        /// <summary>
        /// 客户端下线
        /// </summary>
        ClientOnOff = 7
    }

    public enum SocketError
    {

    }

    /// <summary>
    /// 发送接收命令枚举
    /// </summary>
    public enum Command
    {
        
        /// <summary>
        /// 发送请求接收文件
        /// </summary>
        RequestSendFile = 0x000001,
        /// <summary>
        /// 响应发送请求接收文件
        /// </summary>
        ResponeSendFile = 0x100001,

        /// <summary>
        /// 请求发送文件包
        /// </summary>
        RequestSendFilePack = 0x000002,
        /// <summary>
        /// 响应发送文件包
        /// </summary>
        ResponeSendFilePack = 0x100002,

        /// <summary>
        /// 请求取消发送文件包
        /// </summary>
        RequestCancelSendFile = 0x000003,
        /// <summary>
        /// 响应取消发送文件包
        /// </summary>
        ResponeCancelSendFile = 0x100003,

        /// <summary>
        /// 请求取消接收发送文件
        /// </summary>
        RequestCancelReceiveFile = 0x000004,
        /// <summary>
        /// 响应取消接收发送文件
        /// </summary>
        ResponeCancelReceiveFile = 0x100004,
        /// <summary>
        /// 请求发送文本消息
        /// </summary>
        RequestSendTextMSg = 0x000010,
    }

    public enum MsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        TxtMsg=0,
        /// <summary>
        /// 抖动窗体
        /// </summary>
        Shake= 1,
        /// <summary>
        /// 表情
        /// </summary>
        Face=2,
        /// <summary>
        /// 图片
        /// </summary>
        Pic=3
    }
}
