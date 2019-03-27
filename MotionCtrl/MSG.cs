using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Timers;
using System.Drawing;

namespace MotionCtrl
{
    public class Msg
    {
        /// <summary>
        /// 显示信息类型  错误/警告/运行/
        /// </summary>
        public enum EM_MSGTYPE
        {
            ERR = 0x01,
            WAR = 0x02,
            NOR = 0x04,
            DBG = 0x08,
            SYS = 0x10,
            SAVE_ERR = (ERR << 8),
            SAVE_WAR = (WAR << 8),
            SAVE_NOR = (NOR << 8),
            SAVE_DBG = (DBG << 8),
            SAVE_SYS = (SYS << 8)
        };

        public struct MsgData
        {
            public EM_MSGTYPE msg_type;
            public DateTime dt;
            public string msg;
            public override string ToString()
            {
                return string.Format("[{0}] [{1:HH:mm:ss.fff}] {2}", msg_type.ToString(), dt, msg);
            }
        }

        FileStream Log;
        string str_date;
        private static readonly Object LockObj = new object();
        /// <summary>
        /// 信息显示列表
        /// </summary>
        LinkedList<MsgData> list_msgdat = new LinkedList<MsgData>();
        object displayer;

        public EM_MSGTYPE MsgCfg = (EM_MSGTYPE)0xFFFF;
        //public StringBuilder gMsgStrList = new StringBuilder();
        public int MsgMaxLine = 200;
        /// <summary>
        /// 系统时间
        /// </summary>
        public System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        System.Timers.Timer timer;
        #region 初始化
        public Msg(int MaxLine = 500, EM_MSGTYPE Cfg = (EM_MSGTYPE)0xFFFF)
        {
            MsgCfg = Cfg;
            MsgMaxLine = MaxLine;            

            //时间戳
            sw.Reset();
            sw.Start();
        }

        //~Msg()
        //{
        //    if (Log != null)
        //    {
        //        Log.Close();
        //        Log = null;
        //    }
        //}
        public void StartUpdate(object displayer)
        {
            if (displayer == null) return;
            this.displayer = displayer;
            //显示刷新
            if (timer == null)
            {
                timer = new System.Timers.Timer(200);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.AutoReset = true;
                timer.Enabled = true;
            }            
        }
       
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (list_msgdat.Count == 0 || displayer == null) return;
            ShowMsgCallback(displayer);
        }
        #endregion
        #region  保存Message为log文档
        public void SaveMsg(string MsgStr)
        {
            byte[] byData;
            if (Log == null || 0 != string.Compare(str_date, DateTime.Now.ToString("yyyy-MM-dd")))
            {                
                String str;
                str = Path.GetFullPath("..") + "\\log\\";
                if (!Directory.Exists(str))
                {
                    //文件夹不存在则创建
                    Directory.CreateDirectory(str);
                }

                try
                {
                    if (Log != null)
                    {
                        Log.Close();
                        Log = null;
                    }
                    str += DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                    if (!File.Exists(str))
                    {
                        Log = new FileStream(str, FileMode.Create);
                    }
                    else
                    {
                        Log = new FileStream(str, FileMode.Open);

                    }
                    str_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    return;
                }
            }
            if (Log != null)
            {
                MsgStr = MsgStr + Environment.NewLine;
                Log.Seek(0, SeekOrigin.End);
                byData = System.Text.Encoding.Default.GetBytes(MsgStr);
                if (Log.CanWrite == true) Log.Write(byData, 0, byData.Length);
            }
        }
        #endregion
        #region 委托显示
        delegate void DisplayCallback(object displayer);
        public void ShowMsgCallback(object displayer)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 

            int n = 0;
            Type type = displayer.GetType();
            dynamic obj = Convert.ChangeType(displayer, type);

            while (obj.IsHandleCreated == false)
            {
                //解决窗体关闭时出现“访问已释放句柄“的异常
                if (obj.Disposing || obj.IsDisposed)
                    return;
                Application.DoEvents();
                Thread.Sleep(1);
                if (n++ > 100) return;
            }
            //如果调用控件的线程和创建创建控件的线程不是同一个则为True
            if (obj.InvokeRequired)
            {
                DisplayCallback d = new DisplayCallback(ShowMsgCallback);
                obj.BeginInvoke(d, new object[] { displayer });
            }
            else
            {
                showmsg(obj);
            }
        }
        #endregion
        #region 显示到指定控件       
        public void showmsg(TextBox tb)
        {
            if (list_msgdat.Count == 0 || tb == null) return;

            while (list_msgdat.Count > 0)
            {
                MsgData msg = list_msgdat.First();
                if ((msg.msg_type & MsgCfg) != 0)
                {
                    tb.AppendText(msg.ToString() + "\r\n");//显示信息
                }

                if (((int)msg.msg_type & ((int)MsgCfg) >> 8) != 0)
                {
                    SaveMsg(msg.ToString());//保存信息
                }
                if (tb.Lines.Count() > MsgMaxLine / 2)//信息行数大于最大值的一半
                {
                    List<string> str_list = tb.Lines.ToList();
                    str_list.RemoveAt(0);//去掉第一个
                    tb.Lines = str_list.ToArray();
                }

                tb.SelectionStart = tb.Text.Length;
                tb.ScrollToCaret();

                lock (LockObj)
                {
                    list_msgdat.RemoveFirst();
                }
            }
        }
        /// <summary>
        /// 在RichTextBox中显示信息
        /// </summary>
        /// <param name="rtb"></param>
        public void showmsg(RichTextBox rtb)
        {
            if (list_msgdat.Count == 0 || rtb == null) return;

            while (list_msgdat.Count > 0)
            {
                MsgData msg = list_msgdat.First();
                if ((msg.msg_type & MsgCfg) != 0)
                {
                    rtb.AppendText(msg.ToString() + "\r\n");
                    rtb.SelectedText = msg.ToString() + "\r\n";

                    if (msg.msg_type == EM_MSGTYPE.DBG) rtb.SelectionColor = System.Drawing.Color.DarkGray;
                    else if (msg.msg_type == EM_MSGTYPE.NOR) rtb.SelectionColor = System.Drawing.Color.Blue;
                    else if (msg.msg_type == EM_MSGTYPE.WAR) rtb.SelectionColor = System.Drawing.Color.DarkOrange;
                    else if (msg.msg_type == EM_MSGTYPE.ERR) rtb.SelectionColor = System.Drawing.Color.Red;
                }

                if (((int)msg.msg_type & ((int)MsgCfg) >> 8) != 0)
                {
                    SaveMsg(msg.ToString());//保存到log
                }
                if (rtb.Lines.Count() > MsgMaxLine / 2) rtb.Lines[0].Remove(0);

                rtb.SelectionStart = rtb.Text.Length;
                rtb.ScrollToCaret();

                lock (LockObj)
                {
                    list_msgdat.RemoveFirst();
                }
            }
        }

        /// <summary>
        /// 在表格中显示信息
        /// </summary>
        /// <param name="dgv"></param>
        public void showmsg(DataGridView dgv)
        {
            if (list_msgdat.Count==0 || dgv == null) return;
            //bupdate = false;

            //create col
            if (dgv.ColumnCount < 3)
            {
                dgv.Columns.Clear();
                dgv.Columns.Add("dt", "时间");
                dgv.Columns.Add("type", "类型");
                dgv.Columns.Add("disc", "内容");
                dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[2].FillWeight = 100;
                dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

                dgv.ColumnHeadersHeight = 18;
                dgv.RowHeadersVisible = false;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToResizeColumns = false;
                dgv.AllowUserToResizeRows = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ScrollBars = ScrollBars.Vertical;
                dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(showtext);
                dgv.Rows.Clear();
            }


            while (list_msgdat.Count > 0)
            {
                try
                {
                    MsgData msg = list_msgdat.First();
                    if ((msg.msg_type & MsgCfg) != 0)
                    {
                        int idx = dgv.Rows.Add();
                        DataGridViewRow row = dgv.Rows[idx];

                        row.Cells[0].Value = msg.dt.ToString("HH:mm:ss.fff");
                        row.Cells[1].Value = msg.msg_type.ToString();
                        row.Cells[2].Value = msg.msg;

                        if (msg.msg_type == EM_MSGTYPE.DBG) row.DefaultCellStyle.BackColor = dgv.BackgroundColor;
                        else if (msg.msg_type == EM_MSGTYPE.NOR) row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCyan;
                        else if (msg.msg_type == EM_MSGTYPE.WAR) row.DefaultCellStyle.BackColor = System.Drawing.Color.Wheat;
                        else if (msg.msg_type == EM_MSGTYPE.ERR) row.DefaultCellStyle.BackColor = System.Drawing.Color.Pink;
                        else if (msg.msg_type == EM_MSGTYPE.SYS) row.DefaultCellStyle.BackColor = dgv.BackgroundColor;

                        dgv.FirstDisplayedScrollingRowIndex = idx;
                        if (dgv.RowCount > MsgMaxLine) dgv.Rows.RemoveAt(0);
                    }

                    if (((int)msg.msg_type & ((int)MsgCfg) >> 8) != 0)
                    {
                        SaveMsg(msg.ToString());//保存到log
                    }
                    //if (list_msgdat.Count % 10 == 0)
                    //{
                    //    Application.DoEvents();
                    //    Thread.Sleep(1);
                    //}
                }
                catch(Exception ex)
                {

                }             

                lock (LockObj)
                {
                    list_msgdat.RemoveFirst();
                }
            }
        }

        void showtext(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            MessageBox.Show(((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
        #endregion
        #region 添加显示信息到缓存
        public void AddMsg(EM_MSGTYPE MsgType, string MsgStr)
        {
            if (MsgStr.Length == 0) return;

            try
            {
                //if (pLockMsg == null) pLockMsg = new Mutex(true);
              //  pLockMsg.WaitOne();

                String str="";
                ////show config
                //if ((MsgType == EM_MSGTYPE.ERR) && ((MsgCfg & EM_MSGTYPE.ERR) != 0)) HandStr = "[ERR] ";
                //else if ((MsgType == EM_MSGTYPE.WAR) && ((MsgCfg & EM_MSGTYPE.WAR) != 0)) HandStr = "[WAR] ";
                //else if ((MsgType == EM_MSGTYPE.NOR) && ((MsgCfg & EM_MSGTYPE.NOR) != 0)) HandStr = "[NOR] ";
                //else if ((MsgType == EM_MSGTYPE.DBG) && ((MsgCfg & EM_MSGTYPE.DBG) != 0)) HandStr = "[DBG] ";
                //else if ((MsgType == EM_MSGTYPE.SYS) && ((MsgCfg & EM_MSGTYPE.SYS) != 0)) HandStr = "[SYS] ";
                //else return;

                ////show data
                //str = DateTime.Now.ToString("[HH:mm:ss.fff] ") + HandStr + MsgStr;
            //   if ((MsgType == EM_MSGTYPE.ERR) && ((MsgCfg & EM_MSGTYPE.ERR) != 0)) addstr(str, true);
                //else if ((MsgType == EM_MSGTYPE.WAR) && ((MsgCfg & EM_MSGTYPE.WAR) != 0)) addstr(str, true);
                //else if ((MsgType == EM_MSGTYPE.NOR) && ((MsgCfg & EM_MSGTYPE.NOR) != 0)) addstr(str);
                //else if ((MsgType == EM_MSGTYPE.DBG) && ((MsgCfg & EM_MSGTYPE.DBG) != 0)) addstr(str);
                //else if ((MsgType == EM_MSGTYPE.SYS) && ((MsgCfg & EM_MSGTYPE.SYS) != 0)) addstr(str);

                //save
                //if ((MsgType == EM_MSGTYPE.ERR) && ((MsgCfg & EM_MSGTYPE.SAVE_ERR) != 0)) SaveMsg(str);
                //else if ((MsgType == EM_MSGTYPE.WAR) && ((MsgCfg & EM_MSGTYPE.SAVE_WAR) != 0)) SaveMsg(str);
                //else if ((MsgType == EM_MSGTYPE.NOR) && ((MsgCfg & EM_MSGTYPE.SAVE_NOR) != 0)) SaveMsg(str);
                //else if ((MsgType == EM_MSGTYPE.DBG) && ((MsgCfg & EM_MSGTYPE.SAVE_DBG) != 0)) SaveMsg(str);
                //else if ((MsgType == EM_MSGTYPE.SYS) && ((MsgCfg & EM_MSGTYPE.SAVE_SYS) != 0)) SaveMsg(str);

                if(MsgType == EM_MSGTYPE.ERR)
                {
                   // VAR.gsys_set.bquit = true;
                }


                MsgData msg = new MsgData();
                msg.msg_type = MsgType;
                msg.msg = MsgStr;
                msg.dt = DateTime.Now;
                lock (LockObj)//lock list单独操作锁
                {
                    list_msgdat.AddLast(msg);
                    if (list_msgdat.Count > MsgMaxLine) list_msgdat.RemoveFirst();
                }
            }
            finally
            {
                //pLockMsg.ReleaseMutex();
            }
        }
        #endregion
    }
    public class SYS_INF
    {
        /// <summary>
        /// 报警状态
        /// </summary>
        public EM_ALM_STA alm_sta;
        /// <summary>
        /// 状态显示
        /// </summary>
        public string info;
        /// <summary>
        /// 操作提示
        /// </summary>
        public string hit;
        int beeptime;
        int maxbeeptime;
        int beeptimer;
        int showinftimer;
        bool bflash;
        System.Timers.Timer timer;
        object displayer;
        GPIO out_beer;

        public SYS_INF()
        {            
        }
        /// <summary>
        /// 初始化，并启动刷新
        /// </summary>
        /// <param name="displayer">用于显示信息的控件，须带Text,BackColor属性</param>
        /// <param name="out_beer">蜂鸣器IO</param>
        /// <param name="maxbeeptime">最大蜂鸣时间</param>
        /// <returns></returns>
        public EM_RES Init(object displayer, GPIO out_beer,  int maxbeeptime = 3000)
        {
            if (displayer == null || out_beer == null) return EM_RES.PARA_ERR;
            this.displayer = displayer;
            this.out_beer = out_beer;
            this.maxbeeptime = maxbeeptime;

            timer = new System.Timers.Timer(100);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = true;
            timer.Enabled = true;
            return EM_RES.OK;
        }
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //show infor
            if (++showinftimer == 7)
            {
                ShowSysInforCallBack(displayer);
                showinftimer = 0;
            }
            //beeper
            if (beeptimer < Math.Min(beeptime, maxbeeptime)) beeptimer++;
            if (beeptimer == Math.Min(beeptime, maxbeeptime))
            {
                //避免打开软件初始化时，IO报警
                if (out_beer != null && out_beer.card.isReady) out_beer.SetOff();
                beeptimer ++;
            }
        }

        delegate void DisplayCallback(object displayer);
        /// <summary>
        /// 设置状态/信息
        /// </summary>
        /// <param name="alm_sta">状态</param>
        /// <param name="info">信息</param>
        /// <param name="beep">蜂鸣时间</param>
        /// <param name="bset">强制更新</param>
        /// <param name="hit">提示</param>
        public void Set(EM_ALM_STA alm_sta, string info, int beepTime = 100, bool bset = false,string hit ="")
        {
            //仅仅设置状态
            if (bset)
            {
                this.alm_sta = alm_sta;
                this.info = info;
                beepTime = 0;
            }
            //等级更高的报警
             if (alm_sta > EM_ALM_STA.WAR_YELLOW)
            {
                if (this.info == info)
                {
                    // out_beer.SetOff();
                    return;
                }

                this.alm_sta = alm_sta;
                this.info = info;
            }
            //非报警状态
            else if (alm_sta < EM_ALM_STA.WAR_YELLOW)
            {
                this.alm_sta = alm_sta;
                this.info = info;
            }

            //蜂鸣
            if (beepTime > 0)
            {
                //避免打开软件初始化时，IO报警
                if (out_beer != null && out_beer.card.isReady)
                {
                    out_beer.SetOn();
                    beeptime = beepTime;
                    beeptimer = 0;
                }
                return;
            }

            if (this.alm_sta >= EM_ALM_STA.WAR_YELLOW)//&& !VAR.inf_string.Contains("回零") && VAR.inf_string != "伺服失电")
            {
                if (beeptimer < VAR.gsys_set.beep_tmr)
                    out_beer.SetOn();
            }
            else if (alm_sta < EM_ALM_STA.WAR_YELLOW)
            {
                if (out_beer != null )
                out_beer.SetOff();
            }
        }
        /// <summary>
        /// 设置系统状态显示
        /// </summary>
        /// <param name="displayer">有Text，BackColor属性的控件</param>
        void ShowSysInforCallBack(object displayer)
        {
            if (displayer == null) return;
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 

            int n = 0;
            Type type = displayer.GetType();
            dynamic obj = Convert.ChangeType(displayer, type);

            while (obj.IsHandleCreated == false)
            {
                //解决窗体关闭时出现“访问已释放句柄“的异常
                if (obj.Disposing || obj.IsDisposed)
                    return;
                Application.DoEvents();
                Thread.Sleep(1);
                if (n++ > 100) return;
            }
            //如果调用控件的线程和创建创建控件的线程不是同一个则为True
            if (obj.InvokeRequired)
            {
                DisplayCallback d = new DisplayCallback(ShowSysInforCallBack);
                obj.BeginInvoke(d, new object[] { displayer });
            }
            else
            {
                showinf(obj);
            }
        }

        void showinf(object displayer)
        {
            Color cl = Color.GreenYellow;
            switch (alm_sta)
            {
                case EM_ALM_STA.NOR_GREEN:
                case EM_ALM_STA.NOR_BLUE:
                    cl = Color.GreenYellow;
                    break;
                case EM_ALM_STA.NOR_GREEN_FLASH:
                case EM_ALM_STA.NOR_BLUE_FLASH:
                    cl = Color.GreenYellow;
                    bflash = true;
                    break;
                case EM_ALM_STA.WAR_RED_FLASH:
                    bflash = true;
                    cl = Color.DarkOrange;
                    break;
                case EM_ALM_STA.WAR_RED:
                    cl = Color.DarkOrange;
                    break;
                case EM_ALM_STA.WAR_YELLOW_FLASH:
                    bflash = true;
                    cl = Color.Yellow;
                    break;
                case EM_ALM_STA.WAR_YELLOW:
                    cl = Color.Yellow;
                    break;
                default:
                    cl = Color.GreenYellow;
                    break;
            }
            Type type = displayer.GetType();
            dynamic obj = Convert.ChangeType(displayer, type);
            obj.Text = info;
            if (bflash)
            {
                if (obj.BackColor == Color.Gainsboro) obj.BackColor = cl;
                else obj.BackColor = Color.Gainsboro;
            }
            else
            {
                obj.BackColor = cl;
            }
        }
    }
}
