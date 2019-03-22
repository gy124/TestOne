using System;
using System.Collections.Generic;
using System.Text;

namespace SocketHelper
{
    /// <summary>
    /// 字符串与整数互转
    /// </summary>
    public class IntegerOrString
    {
        /// <summary>
        /// 十六进制字字符串转为节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] StringToHexByteArray(string s)
        {
            s = s.Replace(" ", "");
            if ((s.Length % 2) != 0)
                s += " ";
            byte[] returnBytes = new byte[s.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 十六进制字节数组转为字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HexByteArrayToString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
            return sb.ToString().ToUpper();//将得到的字符全部以字母大写形式输出
        }
    }
}
