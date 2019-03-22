using System;
namespace SocketHelper
{
	internal class FileTransfersItemText : IFileTransfersItemText
	{
		public string Save
		{
			get
			{
				return "接收";
			}
		}
		public string SaveTo
		{
			get
			{
				return "另存为...";
			}
		}
		public string RefuseReceive
		{
			get
			{
				return "拒绝";
			}
		}
		public string CancelTransfers
		{
			get
			{
				return "取消";
			}
		}
	}
}
