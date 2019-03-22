using System;
using System.Collections.Generic;
using System.Text;

namespace SocketHelper
{
    [Serializable]
    public class MsgTypeCell
    {
        private MsgType _msgType;
        private string _imageSuffix="";
        private byte[] _bufferBytes;

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType Msgtype {
            get { return _msgType; }
            set { _msgType = value; }
        }
        /// <summary>
        /// 图片后缀格式
        /// </summary>
        public string ImageSuffix
        {
            get { return _imageSuffix; }
            set { _imageSuffix = value; }
        }
        public byte[] BufferBytes
        {
            get { return _bufferBytes; }
            set { _bufferBytes = value; }
        }
        public MsgTypeCell(MsgType msgType, byte[] buffer)
        {
            Msgtype = msgType;
            BufferBytes = buffer;
        }
    }
}
