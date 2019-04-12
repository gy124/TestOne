using System;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MotionCtrl
{
    public static class Utility
    {
        private static readonly object objCSV = new object();
        #region 文件/文件夹操作
        /// <summary>
        /// 文件夹复制
        /// </summary>
        /// <param name="sourceFolderName">需要复制的文件夹</param>
        /// <param name="destFolderName">目标路径</param>
        /// <param name="overwrite">是否覆盖,true为覆盖，默认true</param>
        public static void CopyDir(string sourceFolderName, string destFolderName, bool overwrite = true)
        {
            var sourceFilesPath = Directory.GetFileSystemEntries(sourceFolderName);

            for (int i = 0; i < sourceFilesPath.Length; i++)
            {
                var sourceFilePath = sourceFilesPath[i];
                var directoryName = Path.GetDirectoryName(sourceFilePath);
                var forlders = directoryName.Split('\\');
                var lastDirectory = forlders[forlders.Length - 1];
                var dest = Path.Combine(destFolderName, lastDirectory);

                if (File.Exists(sourceFilePath))
                {
                    var sourceFileName = Path.GetFileName(sourceFilePath);
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }
                    File.Copy(sourceFilePath, Path.Combine(dest, sourceFileName), overwrite);
                }
                else
                {
                    CopyDir(sourceFilePath, dest, overwrite);
                }
            }
        }
        /// <summary>
        /// 获取磁盘空间并删除指定日期前的文件
        /// </summary>
        /// <param name="HardDiskName"  >要指定索检的硬盘名，例如"C"为C盘</param>
        /// <param name="DeletePath" >要删除的文件夹路径</param>
        /// <param name="DeleteDay" >要指定删除的日期（负数为时间更早，单位为天），例如：-7，表示只保留此前7天数据，更早的会删除</param>
        /// <param name="freeSpace" >保留空间(G)，小于此值才进行删除动作,默认10G</param>
        public static void CatchDiskAndDeleteFile(string HardDiskName, string DeletePath, int DeleteDay, int freeSpace = 10)
        {
            long FreeSpace = new long();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            DirectoryInfo DyInfo = new DirectoryInfo(DeletePath);
            if (HardDiskName != null && HardDiskName.Length > 0)
            {
                foreach (System.IO.DriveInfo drive in drives)
                {
                    if (drive.Name == HardDiskName)
                    {
                        FreeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                        break;
                    }

                }
            }

            if (System.IO.Directory.Exists(DeletePath))
            {
                if (FreeSpace < freeSpace)
                {
                    foreach (FileInfo feInfo in DyInfo.GetFiles())
                    {
                        if (feInfo.CreationTime < DateTime.Now.AddDays(DeleteDay))
                        {
                            feInfo.Delete();
                        }
                    }
                    //System.IO.Directory.Delete(DeletePath,true);     
                    //System.IO.Directory.CreateDirectory(DeletePath);
                }
            }
        }
        #endregion
        #region XML与DataTable互导
        /// <summary>
        /// DataTable从XML文件读取数据
        /// </summary>
        /// <param name="filename">XML文件路径</param>
        /// <returns>返回 DataTable</returns>
        public static DataTable ReadFromXml(string filename)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!File.Exists(filename))
                {
                    throw new Exception("文件不存在!");
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);
                XmlNode root = xmlDoc.SelectSingleNode("DataTable");
                //读取表名   
                dt.TableName = ((XmlElement)root).GetAttribute("TableName");
                //读取行数  
                int CountOfRows = 0;
                if (!int.TryParse(((XmlElement)root).GetAttribute("CountOfRows").ToString(), out CountOfRows))
                {
                    throw new Exception("行数转换失败");
                }
                //读取列数   
                int CountOfColumns = 0;
                if (!int.TryParse(((XmlElement)root).GetAttribute("CountOfColumns").ToString(), out CountOfColumns))
                {
                    throw new Exception("列数转换失败");
                }

                //检查列名与列类型数目
                int colcnt = 0;
                if(root.ChildNodes[0].Attributes.Count == root.ChildNodes[1].Attributes.Count)
                {
                    colcnt = root.ChildNodes[0].Attributes.Count;
                }
                else
                {
                    throw new Exception("列名与列类型数目不一致");
                }

                for(int n=0;n< colcnt;n++)
                {
                    //从第1行中读取记录的列名,从第2行中读取记录类型 
                    dt.Columns.Add(root.ChildNodes[0].Attributes[n].Value, Type.GetType(root.ChildNodes[1].Attributes[n].Value,true,true));
                }

                //从后面的行中读取行信息   
                for (int i = 2; i < root.ChildNodes.Count; i++)
                {
                    string[] array = new string[colcnt];
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = root.ChildNodes[i].Attributes[j].Value.ToString();
                    }
                    dt.Rows.Add(array);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
            return dt;
        }
        /// <summary>
        /// DataTable数据写入XML文件
        /// </summary>
        /// <param name="dt">需要保存的DataTable</param>
        /// <param name="filename">XML文件路径</param>
        /// <returns>成功返回true,否在false</returns>
        public static bool WriteToXml(DataTable dt, string filename)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(filename, Encoding.GetEncoding("UTF-8"));//gb2312
                writer.Formatting = Formatting.Indented;
                //XML文档创建开始   
                writer.WriteStartDocument();
                writer.WriteComment("DataTable: " + dt.TableName);
                writer.WriteStartElement("DataTable"); //DataTable开始  
                writer.WriteAttributeString("TableName", dt.TableName);
                writer.WriteAttributeString("CountOfRows", dt.Rows.Count.ToString());
                writer.WriteAttributeString("CountOfColumns", dt.Columns.Count.ToString());
                //ColumnName开始   
                writer.WriteStartElement("ClomunName", ""); 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.WriteAttributeString("Column" + i.ToString(), dt.Columns[i].ColumnName);
                }
                writer.WriteEndElement();
                //ColumnName结束   

                //ColType开始   
                writer.WriteStartElement("ColType", "");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.WriteAttributeString("ColType" + i.ToString(), dt.Columns[i].DataType.FullName);
                }
                writer.WriteEndElement();
                //ColType结束   

                //各行   
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    writer.WriteStartElement("Row" + j.ToString(), "");
                    //各列    
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        string ss = dt.Rows[j][k].ToString();
                        //if (dt.Columns[k].GetType() == typeof(bool))
                        //{
                        //    if(ss != "True"|| ss != "true") ss = "false";
                        //}
                            
                        writer.WriteAttributeString(dt.Columns[k].ColumnName, ss);//"Column" + k.ToString()
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement(); //DataTable结束  
                writer.WriteEndDocument();
                writer.Close();   //XML文档创建结束 
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        #endregion      
        #region CSV与DataTable互导
        /// 
        /// <summary>
        /// 从CSV文件到数据到DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="title">表示第title行是字段,默认-1则自动生成字段[0...n]</param>
        /// <param name="from">起始行，默认文件开始</param>
        /// <param name="to">结束行，默认为文件结尾</param>
        /// <returns></returns>
        public static DataTable CsvToDataTable(string filePath, int title = -1, int from = 0,int to = int.MaxValue)
        {
            //check file
            if(!File.Exists(filePath))return null;

            String csvSplitBy = "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default, false);
            DataTable dt = new DataTable();

            int i = 0, m = 0;
            reader.Peek();
            int line = 0;
            for (; reader.Peek() > 0 && line < to; line++)
            {
                string str = reader.ReadLine();  
                if (line < from) continue;

                MatchCollection mcs = Regex.Matches(str, csvSplitBy);
                if (line == title)
                {
                    //字段行                    
                    foreach (Match mc in mcs)
                    {
                        dt.Columns.Add(mc.Value);
                    }
                }
                else
                {
                    //检查字段
                    for (int col = dt.Columns.Count; col < mcs.Count; col++)
                    {
                        dt.Columns.Add(col.ToString());
                    }
                    //添加到row
                    i = 0;
                    System.Data.DataRow row = dt.NewRow();
                    foreach (Match mc in mcs)
                    {
                        row[i] = mc.Value;
                        i++;
                    }
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        #endregion
        #region 存字符串到CSV文件
        /// <summary>
        /// 存字符串到CSV文件
        /// </summary>
        /// <param name="filename">csv文件路径</param>
        /// <param name="str">要存的字符串</param>
        /// <returns></returns>
        public static bool WriteStrToCSV(string filename, string str)
        {
            lock (objCSV)
            {
                StreamWriter writer1 = new StreamWriter(filename, true, System.Text.Encoding.GetEncoding("UTF-8"));
                writer1.WriteLine(str);
                writer1.Close();
                writer1.Dispose();
                return true;
            }
           
        }
        #endregion
        #region 阵列

        /// <summary>
        /// 根据矩形的三个顶点生成阵列,返回二维数组
        /// </summary>
        /// <param name="st_pos_tl">左上点</param>
        /// <param name="st_pos_tl_x">右上点</param>
        /// <param name="st_pos_tl_y">左下点</param>
        /// <param name="col_x">列数</param>
        /// <param name="row_y">行数</param>
        /// <param name="brow_inc">true按row增长，false按col增长</param>
        /// <param name="brolback">true为折返增长方式，false为行/列到头则返回行/列头增长</param>
        /// <param name="bshort_step_mode">true：表示tl_x/tl_y为第2/2个目标位置，false:表示tl_x/tl_y为第col_x/row_y个目标位置</param>
        /// <returns>返回二维数组</returns>
        public static ST_XYZA[][] Array(ST_XYZA st_pos_tl, ST_XYZA st_pos_tl_x, ST_XYZA st_pos_tl_y, int col_x,
            int row_y, bool brow_inc = false, bool brolback = false, bool bshort_step_mode = false)
        {
            //check
            if (col_x <= 0 && row_y <= 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "阵列数据异常!\r\n行数或列数小于1!");
                return null;
            }

            ST_XYZA[][] st_pos; 
            if (brow_inc)
            {
                st_pos = new ST_XYZA[col_x][];
                for (int n = 0; n < col_x; n++)
                {
                    st_pos[n] = new ST_XYZA[row_y];
                }
            }
            else
            {
                st_pos = new ST_XYZA[row_y][];
                for (int n = 0; n < row_y; n++)
                {
                    st_pos[n] = new ST_XYZA[col_x];
                }
            }

            double col_w = 0;
            double col_h = 0;
            double row_w = 0;
            double row_h = 0;

            if (col_x > 1)
            {
                col_w = bshort_step_mode ? (st_pos_tl_x.x - st_pos_tl.x) : (st_pos_tl_x.x - st_pos_tl.x) / (col_x - 1);
                col_h = bshort_step_mode ? (st_pos_tl_x.y - st_pos_tl.y) : (st_pos_tl_x.y - st_pos_tl.y) / (col_x - 1);
            }

            if (row_y > 1)
            {
                row_w = bshort_step_mode ? (st_pos_tl_y.x - st_pos_tl.x) : (st_pos_tl_y.x - st_pos_tl.x) / (row_y - 1);
                row_h = bshort_step_mode ? (st_pos_tl_y.y - st_pos_tl.y) : (st_pos_tl_y.y - st_pos_tl.y) / (row_y - 1);
            }

            int c = 0;
            int r = 0;
            bool dir = true;
            for (c = 0; c < col_x; c++)
            {
                if (dir)
                {
                    for (r = 0; r < row_y; r++)
                    {
                        if (brow_inc)
                        {
                            st_pos[c][r].x = st_pos_tl.x + c * col_w + r * row_w;
                            st_pos[c][r].y = st_pos_tl.y + r * row_h + c * col_h;
                        }
                        else
                        {
                            st_pos[r][c].x = st_pos_tl.x + c * col_w + r * row_w;
                            st_pos[r][c].y = st_pos_tl.y + r * row_h + c * col_h;
                        }
                    }
                }
                else
                {
                    for (r = row_y - 1; r >= 0; r--)
                    {
                        if (brow_inc)
                        {
                            st_pos[c][r].x = st_pos_tl.x + c * col_w + r * row_w;
                            st_pos[c][r].y = st_pos_tl.y + r * row_h + c * col_h;
                        }
                        else
                        {
                            st_pos[r][c].x = st_pos_tl.x + c * col_w + r * row_w;
                            st_pos[r][c].y = st_pos_tl.y + r * row_h + c * col_h;
                        }
                    }
                }

                if (brolback) dir = !dir;
            }

            return st_pos;
        }

        /// <summary>
        /// 根据矩形的三个顶点生成阵列,返回List,XY为坐标，Z为row编号，A为col编号
        /// </summary>
        /// <param name="st_pos_tl">左上点</param>
        /// <param name="st_pos_tl_x">右上点</param>
        /// <param name="st_pos_tl_y">左下点</param>
        /// <param name="col_x">列数</param>
        /// <param name="row_y">行数</param>
        /// <param name="brolback">排序方式，设为True时，编号从左上到右上，再从右到左。False时，编号从左上到右上，返回再从左到右。</param>
        /// <returns>返回List</returns>
        public static List<ST_XYZA> ArrayToList(ST_XYZA st_pos_tl, ST_XYZA st_pos_tl_x, ST_XYZA st_pos_tl_y, int col_x, int row_y, bool brolback = false)
        {
            List<ST_XYZA> ls = new List<ST_XYZA>();
            //check
            if (col_x <= 0 && row_y <= 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "阵列数据异常!\r\n行数或列数小于1!");
                return ls;
            }

            double col_w = 0;
            double col_h = 0;
            double row_w = 0;
            double row_h = 0;

            if (col_x > 1)
            {
                col_w = (st_pos_tl_x.x - st_pos_tl.x);
                col_h = (st_pos_tl_x.y - st_pos_tl.y);
            }
            if (row_y > 1)
            {
                row_w = (st_pos_tl_y.x - st_pos_tl.x);
                row_h = (st_pos_tl_y.y - st_pos_tl.y);
            }
            int c = 0;
            int r = 0;
            bool dir = true;
            for (c = 0; c < col_x; c++)
            {
                if (dir)
                {
                    for (r = 0; r < row_y; r++)
                    {
                        ST_XYZA pos = new ST_XYZA();
                        pos.x = st_pos_tl.x + c * col_w + r * row_w;
                        pos.y = st_pos_tl.y + r * row_h + c * col_h;
                        pos.z = r;
                        pos.a = c;
                        ls.Add(pos);
                    }
                }
                else
                {
                    for (r = row_y - 1; r >= 0; r--)
                    {
                        ST_XYZA pos = new ST_XYZA();
                        pos.x = st_pos_tl.x + c * col_w + r * row_w;
                        pos.y = st_pos_tl.y + r * row_h + c * col_h;
                        pos.z = r;
                        pos.a = c;
                        ls.Add(pos);
                    }
                }
                if (brolback) dir = !dir;
            }

            return ls;
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
                string ss = s.Trim();
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
        #region 获得枚举的Description
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
        #endregion
        /// <summary>
        /// 清除挂接的事件
        /// </summary>
        /// <param name="evt">事件</param>
        /// <param name="eventname">指定名称</param>
        public static void clearEvent(EventHandler evt, string eventname = "")
        {
            if (evt == null) return;
            Delegate[] dels = evt.GetInvocationList();
            foreach (Delegate d in dels)
            {
                object delObj = d.GetType().GetProperty("Method").GetValue(d, null);
                string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);
                if (eventname == "" || funcName == eventname)
                {
                    evt -= d as EventHandler;
                }
            }
        }
        #region 旋转偏差计算
        /// <summary>
        /// 获取以指定点为中心旋转指定角度后的新坐标
        /// </summary>
        /// <param name="CurPos">需要旋转的点</param>
        /// <param name="RolCenter">旋转中心</param>
        /// <param name="Angle">旋转角度</param>
        public static void PointRotToNewPoint(ST_XYA CurPos, ST_XY RotCenter, double Angle, ref ST_XYA NewPos)
        {
            //转为弧度
            Angle = Angle / 180.0 * Math.PI;
            //计算旋转后XY偏差
            NewPos.x = Math.Cos(Angle) * (CurPos.x - RotCenter.x) - Math.Sin(Angle) * (CurPos.y - RotCenter.y) + RotCenter.x;
            NewPos.y = Math.Cos(Angle) * (CurPos.y - RotCenter.y) + Math.Sin(Angle) * (CurPos.x - RotCenter.x) + RotCenter.y;
            NewPos.a = CurPos.a + Angle;
        }
        #region 根据上下相机数据与校准数据，计算放置位置
        /// <summary>
        /// 根据上下相机数据与校准数据，计算放置位置
        /// </summary>
        /// <param name="PosUpCam">上相机拍照位置</param>
        /// <param name="VsDatUpCam">上相机视觉数据</param>
        /// <param name="PosDwCam">下相机拍照位置</param>
        /// <param name="VsDatDwCam">下相机视觉数据</param>
        /// <param name="CaliOfs">校准值=放料位置-下拍照位置-上拍照位置</param>
        /// <param name="RotCent">旋转中心</param>
        /// <param name="RotCap">对应旋转中心的拍照位置</param>
        /// <param name="VsOfs">校准值=下相机识别点与放置识别点的偏差</param>
        /// <param name="VsUpCamAglRef">校准VsOfs时上相机的视觉角度</param>
        /// <param name="VsDwCamAglRef">校准VsOfs时下相机的视觉角度</param>
        /// <returns></returns>
        public static ST_XYA CalcPlacePos(ST_XY PosUpCam, ST_XYA VsDatUpCam, ST_XY PosDwCam, ST_XYA VsDatDwCam, ST_XYA CaliOfs, ST_XY RotCent, ST_XYZ RotCap, ST_XYA VsOfs, double VsUpCamAglRef, double VsDwCamAglRef)
        {
            //cali_ofs = st_pos_affine_place - st_pos_affine_downcam_cap - st_pos_affine_upcam_cap

            //点对点
            ST_XYA StPlacePos = CaliOfs + PosDwCam.ToXYA() + PosUpCam.ToXYA() + (VsDatDwCam - VsDatUpCam);

            //偏差旋转
            double a = VsDatUpCam.a - VsUpCamAglRef;
            ST_XYA NewOfs = new ST_XYA();
            Utility.PointRotToNewPoint(VsOfs, new ST_XY(), a, ref NewOfs);
            StPlacePos.x += NewOfs.x;
            StPlacePos.y += NewOfs.y;

            //吸头旋转            
            ST_XY NewRot = RotCent - (PosDwCam - RotCap.ToXY());
            ST_XYA NewPos = new ST_XYA();
            a =- VsOfs.a + (VsDatDwCam.a - VsDwCamAglRef) - (VsDatUpCam.a - VsUpCamAglRef);
            Utility.PointRotToNewPoint(VsDatDwCam, NewRot, -a, ref NewPos);
            NewPos = NewPos - VsDatDwCam;
            StPlacePos = StPlacePos + NewPos;
            StPlacePos.a = a;

            return StPlacePos;
        }
        #endregion
        #region 根据目标位置，计算相机位置
        /// <summary>
        /// 根据上下相机数据与校准数据，计算放置位置
        /// </summary>
        /// <param name="PosUpCam">上相机拍照位置</param>
        /// <param name="VsDatUpCam">上相机视觉数据</param>
        /// <param name="PosDwCam">下相机拍照位置</param>
        /// <param name="VsDatDwCam">下相机视觉数据</param>
        /// <param name="CaliOfs">校准值=放料位置-下拍照位置-上拍照位置</param>
        /// <param name="RotCent">旋转中心</param>
        /// <param name="RotCap">对应旋转中心的拍照位置</param>
        /// <param name="VsOfs">校准值=下相机识别点与放置识别点的偏差</param>
        /// <param name="VsUpCamAglRef">校准VsOfs时上相机的视觉角度</param>
        /// <param name="VsDwCamAglRef">校准VsOfs时下相机的视觉角度</param>
        /// <returns></returns>
        public static ST_XYA CalcCamPos(ST_XY PosUpCam, ST_XYA VsDatUpCam, ST_XY PosDwCam, ST_XYA VsDatDwCam, ST_XYA CaliOfs, ST_XY RotCent, ST_XYZ RotCap, ST_XYA VsOfs, double VsUpCamAglRef, double VsDwCamAglRef)
        {
            //cali_ofs = st_pos_affine_place - st_pos_affine_downcam_cap - st_pos_affine_upcam_cap

            //点对点
            ST_XYA StPlacePos = CaliOfs + PosDwCam.ToXYA() + PosUpCam.ToXYA() + (VsDatDwCam - VsDatUpCam);

            //偏差旋转
            double a = VsDatUpCam.a - VsUpCamAglRef;
            ST_XYA NewOfs = new ST_XYA();
            Utility.PointRotToNewPoint(VsOfs, new ST_XY(), a, ref NewOfs);
            StPlacePos.x += NewOfs.x;
            StPlacePos.y += NewOfs.y;

            //吸头旋转            
            ST_XY NewRot = RotCent - (PosDwCam - RotCap.ToXY());
            ST_XYA NewPos = new ST_XYA();
            a =-VsOfs.a + (VsDatDwCam.a - VsDwCamAglRef) - (VsDatUpCam.a - VsUpCamAglRef);
            Utility.PointRotToNewPoint(VsDatDwCam, NewRot, -a, ref NewPos);
            NewPos = NewPos - VsDatDwCam;
            StPlacePos = StPlacePos + NewPos;
            StPlacePos.a = a;

            return StPlacePos;
        }
        #endregion

        #region 计算轮廓与识别点偏差
        // VsTg 目标识别点坐标（上相机），st_VsShapeOrgOffset轮廓原点坐标（上相机），st_VsShapeInit轮廓初始识别点（下相机），返回偏差Offset
        public static EM_RES CalcOffset(ST_XYA st_VsTg, ST_XYA st_VsShapeInit, ST_XYA st_VsShapeOrgOffset, ref ST_XYA st_Offset)
        {
            //目标点(0,0)绕偏移点(原点偏移)旋转
            ST_XYA st_new = new ST_XYA();
            ST_XY st_org = new ST_XY();
           // st_Offset.a = st_VsShapeOrgOffset.a;// -st_VsShapeInit.z;

            PointRotToNewPoint(st_VsShapeInit, st_org, st_VsShapeOrgOffset.a, ref st_new);
            st_Offset.x = st_VsTg.x - (st_VsShapeOrgOffset.x + st_new.x);
            st_Offset.y = st_VsTg.y - (st_VsShapeOrgOffset.y + st_new.y);
            st_Offset.a = st_VsShapeOrgOffset.a;// -st_VsShapeInit.z;
            return EM_RES.OK;
        }
        #endregion
        #endregion
    }
}
