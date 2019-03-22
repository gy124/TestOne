using System;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace MotionCtrl
{
    public static class Utility
    {
        #region 阵列
        /// <summary>
        /// 根据矩形的三个顶点生成阵列,返回二维数组
        /// </summary>
        /// <param name="st_pos_tl">左上点</param>
        /// <param name="st_pos_tl_x">右上点</param>
        /// <param name="st_pos_tl_y">左下点</param>
        /// <param name="col_x">列数</param>
        /// <param name="row_y">行数</param>
        /// <param name="brolback">排序方式，设为True时，编号从左上到右上，再从右到左。False时，编号从左上到右上，返回再从左到右。</param>
        /// <returns>返回二维数组</returns>
        public static ST_XYZA[][] Array(ST_XYZA st_pos_tl, ST_XYZA st_pos_tl_x, ST_XYZA st_pos_tl_y, int col_x, int row_y, bool brolback = false)
        {
            //check
            
           
            if (col_x <= 0 && row_y <= 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "阵列数据异常!\r\n行数或列数小于1!");
                return null;
            }

            ST_XYZA[][] st_pos = new ST_XYZA[row_y][];
            for (int n = 0; n < row_y; n++)
            {
                st_pos[n] = new ST_XYZA[col_x];
            }
            double col_w = 0;
            double col_h = 0;
            double row_w = 0;
            double row_h = 0;

            if (col_x > 0)
            {
                col_w = (st_pos_tl_x.x - st_pos_tl.x) / (col_x-1);
                col_h = (st_pos_tl_x.y - st_pos_tl.y) / (col_x-1);
            }
            if (row_y > 0)
            {
                row_w = (st_pos_tl_y.x - st_pos_tl.x)/(row_y-1);
                row_h = (st_pos_tl_y.y - st_pos_tl.y)/(row_y-1);
            }
            int c = 0;
            int r = 0;
            //bool dir = true;
            for (c = 0; c < col_x; c++)
            {
                //if (dir)
                //{
                    for (r = 0; r < row_y; r++)
                    {
                        st_pos[r][c].x = st_pos_tl.x + c * col_w + r * row_w;
                        st_pos[r][c].y = st_pos_tl.y + r * row_h + c * col_h;
                    }
                //}
                //else
                //{
                //    for (r = row_y - 1; r >= 0; r--)
                //    {
                //        st_pos[r][c].x = st_pos_tl.x + c * col_w + r * row_w;
                //        st_pos[r][c].y = st_pos_tl.y + r * row_h + c * col_h;
                //    }
                //}
                //if (brolback) dir = !dir;
            }

            return st_pos;
        }
        #endregion
        #region 字符串提取数值
        /// <summary>
        /// 以指定字符ch分割字符串，搜索其后的数字字符，然后转换为数值,成功返回true
        /// </summary>
        /// <param name="str">要提取数值的字符串</param>
        /// <param name="ch">指定分割字符</param>
        /// <param name="dat">返回数值</param>
        /// <returns>成功返回true</returns>
        public static bool GetDoubleFrStr(string str, char ch, ref double dat)
        {
            if (str.Length < 2) return false;

            //避免数字开始时分隔提取出错
            str = "*" + str;

            string[] str_sub = str.Split(ch);
            double temp = 0;
            foreach (string s in str_sub)
            {
                if (s.Length < 1) continue;
                string str_num = "";
                string ss=s.Trim();
                for (int i = 0; i < ss.Length; i++)
                {
                    //提前知道字符后的数字字符
                    if (ss[i] == '.' || ss[i] >= '0' && ss[i] <= '9' || (str_num.Length == 0 && ss[i] == '-' || ss[i] == '+'))
                    {
                        str_num = str_num + ss[i];
                    }
                    //数字字符转换为数值
                    else
                    {
                        if (double.TryParse(str_num, out temp))
                        {
                            str_num = "";
                            dat = temp;
                            return true;
                        }
                        str_num = "";
                        break;
                    }
                }                
                if (double.TryParse(str_num, out temp))
                {
                    dat = temp;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 十六进制字字符串转为节数组
        /// </summary>
        /// <param name="s">待转换的字符串</param>
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
        /// <param name="data">十六进制字节数组</param>
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
        #endregion
        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstend">当枚举没有定义DescriptionAttribute,是否用枚举名代替，默认使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this Enum value, bool nameInstend = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstend == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }        
    }
}
