using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionCtrl;

namespace UI
{
    public partial class FrRst : Form
    {
        bool bflag;
        bool breset = false;
        int j ;

        #region 委托弹框
        delegate DialogResult MessageBoxShow(string text,string caption, MessageBoxButtons buttons,MessageBoxIcon icon);
        DialogResult MessageBoxShowF(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var dr = MessageBox.Show(text,caption,buttons,icon);
            return dr;
        }
        public DialogResult ShowMessage(string text, string caption="提示", MessageBoxButtons buttons= MessageBoxButtons.OKCancel, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            var dr = new DialogResult();
            try
            {
                var tmp = this.Invoke(new MessageBoxShow(MessageBoxShowF), new object[] { text, caption, buttons, icon });
                if (tmp != null) dr = (DialogResult)tmp;
            }
            catch
            {

            }
            return dr;
        }
        #endregion
        #region 委托操作按键
        delegate void EnableObject(object sender,bool ben = true);
        void EnableObj(object sender, bool ben = true)
        {
            ((dynamic)sender).Enabled = ben;
        }
        public void Enablebtn(object sender, bool ben = true)
        {
            try
            {
                this.Invoke(new EnableObject(EnableObj), new object[] { sender, ben });
            }
            catch
            {

            }
        }
        #endregion
        public FrRst()
        {
            InitializeComponent();
        }

        public bool bupdate
        {
            get { return timer_update.Enabled; }
            set { timer_update.Enabled = value; }
        }

        private void FrUser_Load(object sender, EventArgs e)
        {
            COM.traybox_get.SetSta(TrayBox.EM_STA.FULL);//所有料盘满料
            COM.traybox_get.NewBox(Product.EM_CM_RES.OK);//所有模组ok
            tbox_fd.box = COM.traybox_get;

            COM.traybox_back.SetSta(TrayBox.EM_STA.EMPTY);//所有料盘满料
            COM.traybox_back.NewBox(Product.EM_CM_RES.OK);//所有模组ok
            tbox_back.box = COM.traybox_back;
        
        }      
        delegate void UpdateCallback();
        public void UpdateDisplay()
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            int n = 0;
            while (!IsHandleCreated)
            {
                //解决窗体关闭时出现“访问已释放句柄“的异常
                if (Disposing || IsDisposed)
                    return;
                Application.DoEvents();
                Thread.Sleep(1);
                if (n++ > 100) return;
            }
            if (InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {

                UpdateCallback d = new UpdateCallback(UpdateDisplay);
                BeginInvoke(d, new object[] {});
            }
            else
            {
                tbox_back.UpdateShow();
                tbox_fd.UpdateShow();
                tray_ws_get.tray_dat =WSGet.tray_now    ;
                tray_bk_ok.tray_dat = WSBack.tray_now;
                tray_ws_get.UpdateShow();
                tray_bk_ok.UpdateShow();
                //状态信息
                lb_get_sta_inf.Text =WSGet.GetStaString;
                lb_bk_sta_inf.Text = WSBack.GetStaString;
                lb_fd_sta_inf.Text = WSFeed.GetStaString;
                label_move_inf.Text = WsTrayMove.GetStaString;
                lb_bbk_sta.Text = WsBuBK.GetStaString;
                lb_bulltFed_sta_inf.Text = WsBuFD.GetStaString;


                j++;
                if ((j % 2) == 1)//闪烁背景
                {
                    if (VAR.gsys_set.status != EM_SYS_STA.RUN)
                    {
                        lb_get_sta_inf.BackColor = Color.Yellow;
                        lb_bk_sta_inf.BackColor = Color.Yellow;
                        lb_fd_sta_inf.BackColor = Color.Yellow;
                        lb_bulltFed_sta_inf.BackColor = Color.Yellow;
                        lb_bbk_sta.BackColor = Color.Yellow;
                        label_move_inf.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lb_get_sta_inf.BackColor = Color.Green;
                        lb_fd_sta_inf.BackColor = Color.Green;
                        lb_bk_sta_inf.BackColor = Color.Green;
                        lb_bulltFed_sta_inf.BackColor = Color.Green;
                        lb_bbk_sta.BackColor = Color.Green;
                        label_move_inf.BackColor = Color.Green;
                    }
                }
                else
                {
                    lb_get_sta_inf.BackColor = Color.White;
                    lb_bk_sta_inf.BackColor = Color.White;
                    lb_fd_sta_inf.BackColor = Color.White;
                    lb_bulltFed_sta_inf.BackColor = Color.White;
                    lb_bbk_sta.BackColor = Color.White;
                    label_move_inf.BackColor = Color.White;
                }
                //变量标志
                if (WSGet.bLPutOK)
                    checkBox_wsget_LPutOK.Checked = true;
                else
                    checkBox_wsget_LPutOK.Checked = false;


                if (WSGet.bRPutOK)
                    checkBox_wsget_RPutOK.Checked = true;
                else
                    checkBox_wsget_RPutOK.Checked = false;

                if (WSBack.bLGet)
                    checkBox_wsback_LPutOK.Checked = true;
                else
                    checkBox_wsback_LPutOK.Checked = false;


                if (WSBack.bRGet)
                    checkBox_wsback_RPutOK.Checked = true;
                else
                    checkBox_wsback_RPutOK.Checked = false;

                if (WSFeed.bLput)
                    checkBox_wsfeed_LPutOK.Checked = true;
                else
                    checkBox_wsfeed_LPutOK.Checked = false;

                if (WSFeed.bRput)
                    checkBox_wsfeed_RPutOK.Checked = true;
                else
                    checkBox_wsfeed_RPutOK.Checked = false;

                if (WSFeed.bAAcmd)
                    checkBox_wsfeed_bCMD.Checked = true;
                else
                    checkBox_wsfeed_bCMD.Checked = false;

                if (WSROLL.bRollOK)
                    checkBox_wsrol_RollOK.Checked = true;
                else
                    checkBox_wsrol_RollOK.Checked = false;
                
                
            }
        }
        void SetBtnStatusColor(Button btn, AXIS.HOME_STA sta, bool bflag)
        {
            if (sta == AXIS.HOME_STA.OK) btn.BackColor = Color.DodgerBlue;
            else if (sta == AXIS.HOME_STA.HOMING) btn.BackColor = bflag ? Color.DodgerBlue : SystemColors.ButtonFace;
            else if (sta == AXIS.HOME_STA.ERROR || sta == AXIS.HOME_STA.UNKOWN) btn.BackColor = Color.Orange;
            else btn.BackColor = SystemColors.ButtonFace;
        }        
        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            VAR.gsys_set.bquit = true;
            MT.AllAxStop();
        }

        private void timer_update_Tick(object sender, EventArgs e)
        {
            bflag = !bflag;
            Task show = new Task(() =>
            {
                UpdateDisplay();
            }
           );
            show.Start();
           
            //上料
            SetBtnStatusColor(bt_BFD_z, MT.AXIS_bullet_feed.home_status, bflag);

            SetBtnStatusColor(bt_MT_x, MT.AXIS_bullet_move.home_status, bflag);

            SetBtnStatusColor(bt_BBK_z, MT.AXIS_bullet_back.home_status, bflag);
            SetBtnStatusColor(btn_ws_get_x_sta, MT.AXIS_GET_X.home_status, bflag);
            SetBtnStatusColor(btn_ws_get_y_sta, MT.AXIS_GET_Y.home_status, bflag);
            SetBtnStatusColor(btn_ws_get_z_sta, MT.AXIS_GET_Z.home_status, bflag);
            SetBtnStatusColor(btn_ws_get_a_sta, MT.AXIS_GET_A.home_status, bflag);
            SetBtnStatusColor(bt_BK_x, MT.AXIS_BACK_X.home_status, bflag);
            SetBtnStatusColor(bt_BK_y, MT.AXIS_BACK_Y.home_status, bflag);
            SetBtnStatusColor(bt_BK_z, MT.AXIS_BACK_Z.home_status, bflag);
            SetBtnStatusColor(bt_BK_a, MT.AXIS_BACK_A.home_status, bflag);
            SetBtnStatusColor(bt_FD_x, MT.AXIS_FEED_X.home_status, bflag);
            SetBtnStatusColor(bt_FD_y, MT.AXIS_FEED_Y.home_status, bflag);
            SetBtnStatusColor(bt_FD_z, MT.AXIS_FEED_Z.home_status, bflag);
            SetBtnStatusColor(bt_FD_a, MT.AXIS_FEED_A.home_status, bflag);
            if (VAR.gsys_set.status!=EM_SYS_STA.STANDBY)
            {
                bt_ws_bufeed_tray_ready.Enabled = false;
                //bt_bullfeed_BOXCH.Enabled = false;
                //bt_bullfeed_tray_out.Enabled = false;
                //bt_bullfeed_BOXIN.Enabled = false;
                //bt_bullfeed_BOXOUT.Enabled = false;
                //button3.Enabled = false;
                
            }
            else
            {
                bt_ws_bufeed_tray_ready.Enabled = true;
                bt_bullfeed_BOXCH.Enabled = true;
                bt_bullfeed_tray_out.Enabled = true;
                bt_bullfeed_BOXIN.Enabled = true;
                bt_bullfeed_BOXOUT.Enabled = true;
                button3.Enabled = true;
            }

            lb_wsGT.Text = string.Format("X: {0:000.000}  Y: {1:000.000}\nZ: {2:000.000}  A: {3:000.000}", MT.AXIS_GET_X.fcmd_pos, MT.AXIS_GET_Y.fcmd_pos, MT.AXIS_GET_Z.fcmd_pos, MT.AXIS_GET_A.fcmd_pos);
            lb_wsFD.Text = string.Format("X: {0:000.000}  Y: {1:000.000}\nZ: {2:000.000}  A: {3:000.000}", MT.AXIS_FEED_X.fcmd_pos, MT.AXIS_FEED_Y.fcmd_pos, MT.AXIS_FEED_Z.fcmd_pos, MT.AXIS_FEED_A.fcmd_pos);
            lb_wsBK.Text = string.Format("X: {0:000.000}  Y: {1:000.000}\nZ: {2:000.000}  A: {3:000.000}",  MT.AXIS_BACK_X .fcmd_pos, MT.AXIS_BACK_Y.fcmd_pos, MT.AXIS_BACK_Z.fcmd_pos, MT.AXIS_BACK_A.fcmd_pos);
   
        }


        #region 各个工站复位和停止按键事件

        private void btn__all_stop_Click(object sender, EventArgs e)
        {
            COM.Stop();
        }

        private void btn_all_home_Click(object sender, EventArgs e)
        {
            if (COM.bhomeing) return;
            EM_RES ret=EM_RES.OK;
            if (DialogResult.OK != MessageBox.Show("确定要系统复位 ？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                    
                {
                    Enablebtn(ws_bull_feed_home, false);
                    Enablebtn(btn_ws_get_home, false);
                    Enablebtn(ws_bull_back_home, false);
                    Enablebtn(btn_ws_back_home, false);
                    Enablebtn(btn_ws_feed_home, false);
                    Enablebtn(btn_ws_move_home, false);
                    Enablebtn(sender, false);

                    EM_RES res = EM_RES.OK;
                     res = COM.Home(ref VAR.gsys_set.bquit);
                    ShowMessage(string.Format("【{0}】 复位 {1}", "系统", res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
                    if (res == EM_RES.OK)
                        VAR.gsys_set.status = EM_SYS_STA.STANDBY;
                   VAR.sys_inf.Set(EM_ALM_STA.NOR_BLUE,   "就绪", 0);
                }
                finally
                {
                    Enablebtn(ws_bull_feed_home, true);
                    Enablebtn(btn_ws_get_home, true);
                    Enablebtn(ws_bull_back_home, true);
                    Enablebtn(btn_ws_back_home, true);
                    Enablebtn(btn_ws_feed_home, true);
                    Enablebtn(btn_ws_move_home, true);
                    Enablebtn(sender, true);
                }
            }
            );
            task_home.Start();
        }

        private void btn_ws_get_home_Click(object sender, EventArgs e)
        {
            if (WSGet.status == WSGet.EM_STA.HOME) return;

            
            if (DialogResult.OK != MessageBox.Show("确定要复位 【取料工站】？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                {
                    Enablebtn(sender, false);
                    Enablebtn(btn_all_home, false);
                    VAR.gsys_set.bquit = false;
                    EM_RES res = WSGet.Home(ref VAR.gsys_set.bquit);
                    ShowMessage(string.Format("【{0}】 复位 {1}！", WSGet.disc, res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
                }
                finally
                {
                    Enablebtn(sender, true);
                    Enablebtn(btn_all_home, true);
                }
            }
            );
            task_home.Start();
        }

        private void btn_ws_get_stop_Click(object sender, EventArgs e)
        {
             MT.AxisHomeQuit(MT.AxList_WS_GET);
        }

        private void ws_bull_feed_home_Click(object sender, EventArgs e)
        {
            if (WsBuFD.status == WsBuFD.EM_STA.HOME) return;         
            if (DialogResult.OK != MessageBox.Show("确定要复位 【弹夹上料工站】？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                {
                      Enablebtn(sender, false);
                      Enablebtn(btn_all_home, false);
                    
                      EM_RES res =  WsBuFD.home(ref VAR.gsys_set.bquit);
                      ShowMessage(string.Format("【{0}】 复位 {1}！",WsBuFD.disc , res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
              }
                finally
                {
                    Enablebtn(sender, true);
                    Enablebtn(btn_all_home, true);
                }
            }
            );

            task_home.Start();
        }

        private void ws_bull_back_home_Click(object sender, EventArgs e)
        {
            if (WsBuBK.status == WsBuBK.EM_STA.HOME) return;
           
            if (DialogResult.OK != MessageBox.Show("确定要复位 【弹夹收料工站】？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                {
                    Enablebtn(sender, false);
                    Enablebtn(btn_all_home, false);
                    VAR.gsys_set.bquit = false;
                    EM_RES res = WsBuBK.home(ref VAR.gsys_set.bquit);
                    ShowMessage(string.Format("【{0}】 复位 {1}！", WsBuBK.disc, res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
                }
                finally
                {
                    Enablebtn(sender, true);
                    Enablebtn(btn_all_home, true);
                }
            }
            );
            task_home.Start();
        }

        private void ws_bull_back_stop_Click(object sender, EventArgs e)
        {
           MT.AxisHomeQuit(MT.AXIS_bullet_back);
        }

        private void ws_bull_fed_stop_Click(object sender, EventArgs e)
        {
            MT.AxisHomeQuit(MT.AXIS_bullet_feed,MT.AXIS_bullet_move);
            MT.AxisHomeQuit(MT.AXIS_bullet_move);
        }

        private void btn_ws_back_home_Click(object sender, EventArgs e)
        {

            if (WSBack.status == WSBack.EM_STA.HOME) return;

            if (DialogResult.OK != MessageBox.Show("确定要复位 【弹夹收料工站】？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                {
                    Enablebtn(sender, false);
                    Enablebtn(btn_all_home, false);
                    VAR.gsys_set.bquit = false;
                    EM_RES res = WSBack.home(ref VAR.gsys_set.bquit);
                    ShowMessage(string.Format("【{0}】 复位 {1}！", WSBack.disc, res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
                }
                finally
                {
                    Enablebtn(sender, true);
                    Enablebtn(btn_all_home, true);
                }
            }
            );
            task_home.Start();
        }

        private void button37_Click(object sender, EventArgs e)
        {

            if (WSFeed.status == WSFeed.EM_STA.HOME) return;

            if (DialogResult.OK != MessageBox.Show("确定要复位 【上料工站】？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                {
                    Enablebtn(sender, false);
                    Enablebtn(btn_all_home, false);
                    VAR.gsys_set.bquit = false;
                    EM_RES res = WSFeed.home(ref VAR.gsys_set.bquit);
                    ShowMessage(string.Format("【{0}】 复位 {1}！", WSFeed.disc, res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
                }
                finally
                {
                    Enablebtn(sender, true);
                    Enablebtn(btn_all_home, true);
                }
            }
            );
            task_home.Start();
        }
        private void btn_ws_move_home_Click(object sender, EventArgs e)
        {
            if (WsTrayMove.status == WsTrayMove.EM_STA.HOME) return;
         
            if (DialogResult.OK != MessageBox.Show("确定要复位 【运料工站】？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)) return;
            Task task_home = new Task(() =>
            {
                try
                {
                    Enablebtn(sender, false);
                    Enablebtn(btn_all_home, false);
                    VAR.gsys_set.bquit = false;
                    EM_RES res = WsTrayMove.home(ref VAR.gsys_set.bquit);
                    ShowMessage(string.Format("【{0}】 复位 {1}！", WsTrayMove.disc, res == EM_RES.OK ? "成功" : "失败"), "提示", MessageBoxButtons.OK);
                }
                finally
                {
                    Enablebtn(sender, true);
                    Enablebtn(btn_all_home, true);
                }
            });
            task_home.Start();
        }

        #endregion

        #region 手动操作按钮事件
        private void button28_Click(object sender, EventArgs e)
        {
            WsTrayMove.task_run();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            EM_RES ret;
          ret=  WsBuFD.trayReady(ref VAR.gsys_set.bquit);     
                if (ret != EM_RES.OK) MessageBox.Show( "找盘失败!");
                else    MessageBox.Show("找盘成功!");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            EM_RES ret=   WsTrayMove.m_PUT  (ref VAR.gsys_set.bquit);
         if (ret == EM_RES.OK) MessageBox.Show("移栽上料成功");
         else MessageBox.Show("移栽上料失败");
        
        }
        private void button23_Click(object sender, EventArgs e)
        {
         EM_RES ret=   WsTrayMove.m_GET(ref VAR.gsys_set.bquit);
         if (ret == EM_RES.OK) MessageBox.Show("移栽取料成功");
         else MessageBox.Show("移栽取料失败");
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            EM_RES ret = EM_RES.OK;
            ret = WSROLL.mHome(ref  VAR.gsys_set.bquit);
            if (ret == EM_RES.OK) MessageBox.Show("取料成功");
            else
                MessageBox.Show("取料失败");
        }


        private void ws_get_act_get_Click(object sender, EventArgs e)
        {

            EM_RES ret = EM_RES.OK;     
             ret=  WSGet.m_ActGet(ref  VAR.gsys_set.bquit);
            if (ret == EM_RES.OK) MessageBox.Show("取料成功");
           else
               MessageBox.Show("取料失败");
             
        }

        private void button16_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            ret = WsBuFD.boxIN(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("进料盒失败!");
            else MessageBox.Show("进料盒成功!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            ret = WsBuFD.boxOUT(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("出料盒失败!");
            else MessageBox.Show("出料盒成功!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            ret = WsBuFD.boxChange(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("换料盒失败!");
            else MessageBox.Show("换料盒成功!");
        }

        private void ws_get_photo_L_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            bool sta = WSGet.bLPutOK;//临时记录状态
            WSGet. bLPutOK = false;//
            ret = WSGet.m_ToPhoto (ref VAR.gsys_set.bquit);
            WSGet.bLPutOK = sta;//状态恢复
            if (ret != EM_RES.OK) MessageBox.Show("拍照失败!");
            else MessageBox.Show("拍照成功!");
        }

        private void ws_get_photo_R_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            bool sta = WSGet.bLPutOK;//临时记录状态
            bool staR = WSGet.bRPutOK;//临时记录状态
            WSGet.bRPutOK = false;//
            WSGet.bLPutOK = true;//
            ret = WSGet.m_ToPhoto(ref VAR.gsys_set.bquit);
            WSGet.bRPutOK = staR;//状态恢复
            WSGet.bLPutOK = sta;//状态恢复
            if (ret != EM_RES.OK) MessageBox.Show("拍照失败!");
            else MessageBox.Show("拍照成功!");
        }

        private void ws_get_put_L_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            bool sta = WSGet.bLPutOK;//临时记录状态
            WSGet.bLPutOK = false;//
            ret = WSGet.m_ToPut(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) { MessageBox.Show("放料失败!");WSGet.bLPutOK = sta; return ; }           
            WSGet.bLPutOK = sta;//状态恢复
            if (ret != EM_RES.OK) MessageBox.Show("放料失败!");
            else MessageBox.Show("放料成功!");
        }

        private void ws_get_put_R_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            bool sta = WSGet.bLPutOK;//临时记录状态
            bool staR = WSGet.bRPutOK;//临时记录状态
            WSGet.bRPutOK = false;//
            WSGet.bLPutOK = true;//
            ret = WSGet.m_ToPut(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) { MessageBox.Show("放料失败!"); WSGet.bLPutOK = sta; WSGet.bRPutOK = staR; return; }
    
            WSGet.bRPutOK = staR;//状态恢复
            WSGet.bLPutOK = sta;//状态恢复
            
            if (ret != EM_RES.OK) MessageBox.Show("拍照失败!");
            else MessageBox.Show("拍照成功!");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            EM_RES ret = WsBuFD.trayOut(ref VAR.gsys_set.bquit);
                if (ret != EM_RES.OK) MessageBox.Show("失败!");
                else MessageBox.Show("成功!");
        }

        private void button30_Click(object sender, EventArgs e)
        {

            EM_RES ret = WsBuBK.trayIN(ref VAR.gsys_set.bquit);
                if (ret != EM_RES.OK) MessageBox.Show("失败!");
                else MessageBox.Show("成功!");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EM_RES ret = WsBuBK.trayReady(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            EM_RES ret = WsBuBK.boxOUT(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EM_RES ret = WsBuBK.boxIN(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            bool sta = WSBack.bLGet;//临时记录状态
            WSBack.bLGet = false;//
            ret = WSBack.m_ToGet(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) { MessageBox.Show("取料失败!"); WSBack.bLGet = sta; return; }
            ret = WSBack.m_ActGet(ref VAR.gsys_set.bquit);
            WSBack.bLGet = sta;//状态恢复
            if (ret != EM_RES.OK) MessageBox.Show("放料失败!");
            else MessageBox.Show("放料成功!");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            EM_RES ret;
            bool sta = WSBack.bLGet;//临时记录状态
            bool staR = WSBack.bRGet;//临时记录状态
            WSBack.bRGet = false;//
            WSBack.bLGet = true;//
            ret = WSBack.m_ToGet(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) { MessageBox.Show("取料失败!"); WSBack.bLGet = sta; WSBack.bRGet = staR; return; }
            ret = WSBack.m_ActGet(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("取料失败!");
            WSBack.bRGet = staR;//状态恢复
            WSBack.bLGet = sta;//状态恢复

            if (ret != EM_RES.OK) MessageBox.Show("取料失败!");
            else MessageBox.Show("取料成功!");
        }


        private void button5_Click(object sender, EventArgs e)
        {
          
                EM_RES ret =  WSFeed.m_ActGet_L(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            EM_RES ret = WSFeed.m_ActGet_R(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EM_RES ret = WSFeed.m_ActPut_L(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            EM_RES ret = WSFeed.m_ActPut_R(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }



        private void button43_Click(object sender, EventArgs e)
        {

            EM_RES ret = WSROLL.mclose(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            EM_RES ret = WSROLL.mopen(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }
        private void button41_Click(object sender, EventArgs e)
        {
            WSFeed.task_run();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
           WsBuFD.task_run();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            WSGet.task_run();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            WsBuBK.task_run();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            WSBack.task_run();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            WSFeed.task_run();
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
            EM_RES ret = WSROLL.mact_roll(ref VAR.gsys_set.bquit);
            if (ret != EM_RES.OK) MessageBox.Show("失败!");
            else MessageBox.Show("成功!");
        }


        private void bt_ws_move_feed_Click(object sender, EventArgs e)
        {

        }


        private void checkBox1_Click(object sender, EventArgs e)
        {

            WSGet.bLPutOK = !WSGet.bLPutOK;
          
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            WSGet.bRPutOK = !WSGet.bRPutOK;
        }

        private void checkBox1_Click_1(object sender, EventArgs e)
        {
            WSBack.bLGet = !WSBack.bLGet;
        }

        private void checkBox2_Click_1(object sender, EventArgs e)
        {
            WSBack.bRGet = !WSBack.bRGet;
        }

        private void checkBox1_Click_2(object sender, EventArgs e)
        {
            WSFeed.bLput = !WSFeed.bLput;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            WSFeed.bRput = !WSFeed.bRput;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            WSFeed.bAAcmd = !WSFeed.bAAcmd;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            WSROLL.bRollOK = !WSROLL.bRollOK;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UI.COM.MVS.SaveInf();
        }
        #endregion
       
    }
}
