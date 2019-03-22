using System;
namespace SocketHelper
{
    /// <summary>
    /// 文件状态
    /// </summary>
	public enum FileTransfersItemStyle
	{
        /// <summary>
        /// 发送文件
        /// </summary>
		Send,
        /// <summary>
        /// 准备接收文件
        /// </summary>
		ReadyReceive,
        /// <summary>
        /// 接收文件
        /// </summary>
		Receive
	}
}
