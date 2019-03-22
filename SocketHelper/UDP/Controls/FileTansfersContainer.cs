using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace SocketHelper
{
    /// <summary>
    /// 发送接收文件管理器
    /// </summary>
	public class FileTansfersContainer : Panel
	{
		private IFileTransfersItemText _fileTransfersItemText;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IFileTransfersItemText FileTransfersItemText
		{
			get
			{
				if (this._fileTransfersItemText == null)
				{
					this._fileTransfersItemText = new FileTransfersItemText();
				}
				return this._fileTransfersItemText;
			}
			set
			{
				this._fileTransfersItemText = value;
				foreach (FileTransfersItem item in base.Controls)
				{
					item.FileTransfersText = this._fileTransfersItemText;
				}
			}
		}
		public FileTansfersContainer()
		{
			this.AutoScroll = true;
		}

        private bool _isAutomaticShowHide = true;
        [Description("是否开启自动显示隐藏控件\r\n(true为开启，有接收文件或发送文件时自动显示隐藏控件)")]
        public bool IsAutomaticShowHide {
            get { return _isAutomaticShowHide; }
            set { _isAutomaticShowHide = value; }
        }

        public FileTransfersItem AddItem(string text, string fileName, Image image, long fileSize, FileTransfersItemStyle style)
		{
			FileTransfersItem item = new FileTransfersItem();
			item.Text = text;
			item.FileName = fileName;
			item.Image = image;
			item.FileSize = fileSize;
			item.Style = style;
			item.FileTransfersText = this.FileTransfersItemText;
			item.Dock = DockStyle.Top;
			base.SuspendLayout();
			base.Controls.Add(item);
			item.BringToFront();
			base.ResumeLayout(true);
            if (IsAutomaticShowHide)
            {
                base.Visible = base.Controls.Count > 0 ? true : false;
            }
			return item;
		}
        /// <summary>
        /// 添加FileTransfersItem文件项
        /// </summary>
        /// <param name="md5">名称（MD5校验值）</param>
        /// <param name="typetext">文件类型（发送文件/接收文件）</param>
        /// <param name="fileName">文件路径</param>
        /// <param name="image">文件图标</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="style">文件状态（详情查看：FileTransfersItemStyle）</param>
        /// <returns></returns>
		public FileTransfersItem AddItem(string md5, string typetext, string fileName, Image image, long fileSize, FileTransfersItemStyle style)
		{
			FileTransfersItem item = new FileTransfersItem();
            item.Name = md5;
            item.Text = typetext;
			item.FileName = fileName;
			item.Image = image;
			item.FileSize = fileSize;
			item.Style = style;
			item.FileTransfersText = this.FileTransfersItemText;
			item.Dock = DockStyle.Top;
			base.SuspendLayout();
			base.Controls.Add(item);
            item.BringToFront();
			base.ResumeLayout(true);
            if (IsAutomaticShowHide)
            {
                base.Visible = base.Controls.Count > 0 ? true : false;
            }
			return item;
		}
        /// <summary>
        /// 移除对应FileTransfersItem
        /// </summary>
        /// <param name="item">FileTransfersItem</param>
		public void RemoveItem(FileTransfersItem item)
		{
			base.Controls.Remove(item);
            if (IsAutomaticShowHide)
            {
                base.Visible = base.Controls.Count > 0 ? true : false;
            }
		}
        /// <summary>
        /// 根据MD5键值移除对应控件
        /// </summary>
        /// <param name="md5"></param>
		public void RemoveItem(string md5)
		{
            base.Controls.RemoveByKey(md5);
            if (IsAutomaticShowHide)
            {
                base.Visible = base.Controls.Count > 0 ? true : false;
            }
		}
        
		public void RemoveItem(Predicate<FileTransfersItem> match)
		{
			FileTransfersItem itemRemove = null;
			foreach (FileTransfersItem item in base.Controls)
			{
				if (match(item))
				{
					itemRemove = item;
				}
			}
			base.Controls.Remove(itemRemove);
            if (IsAutomaticShowHide)
            {
                base.Visible = base.Controls.Count > 0 ? true : false;
            }
		}
        /// <summary>
        /// 根据MD5校验值查找FileTransfersItem
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
		public FileTransfersItem Search(string md5)
		{
            return base.Controls[md5] as FileTransfersItem;
		}
		public FileTransfersItem Search(Predicate<FileTransfersItem> match)
		{
			FileTransfersItem result;
			foreach (FileTransfersItem item in base.Controls)
			{
				if (match(item))
				{
					result = item;
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
