using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SocketHelper
{
    /// <summary>
    /// 消息单元类（可序列化）
    /// </summary>
    [Serializable]
    public class MsgCell : IDataCell
    {
       
        private int _messageId;
        /// <summary>
        /// 消息标识
        /// </summary>
        public int MessageId
        {
            get { return _messageId; }
            set { _messageId = value; }
        }

        private object _data;
        /// <summary>
        /// 消息序列化数据
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #region 构造函数
        public MsgCell() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageId">消息标识</param>
        /// <param name="data">序列化数据</param>
        public MsgCell(
            int messageId,
            object data)
        {
            _messageId = messageId;
            _data = data;
        } 
        #endregion
        /// <summary>
        /// 将数据序列化成Byte[]数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBuffer()
        {
            byte[] data = SerHelper.Serialize(_data);
            byte[] id = BitConverter.GetBytes(MessageId);

            byte[] buffer = new byte[data.Length + id.Length];
            Buffer.BlockCopy(id, 0, buffer, 0, id.Length);
            Buffer.BlockCopy(data, 0, buffer, id.Length, data.Length);
            return buffer;
        }
        /// <summary>
        /// 将Byte[]数组反序列化成数据结构
        /// </summary>
        /// <param name="buffer"></param>
        public void FromBuffer(byte[] buffer)
        {
            _messageId = BitConverter.ToInt32(buffer, 0);
            _data = SerHelper.Deserialize(buffer, 4);
        }
    }
}
