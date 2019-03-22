using System;
namespace SocketHelper
{
	public interface IFileTransfersItemText
	{
		string Save
		{
			get;
		}
		string SaveTo
		{
			get;
		}
		string RefuseReceive
		{
			get;
		}
		string CancelTransfers
		{
			get;
		}
	}
}
