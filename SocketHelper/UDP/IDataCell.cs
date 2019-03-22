namespace SocketHelper
{
    /// <summary>
    /// 消息数据单元接口
    /// </summary>
    public interface IDataCell
    {
        byte[] ToBuffer();

        void FromBuffer(byte[] buffer);
    }
}
