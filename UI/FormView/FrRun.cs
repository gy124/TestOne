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
    public partial class FrRun : Form
    {
       public  delegate bool CamControl();
       public  CamControl CamOpen;
       public CamControl CamClose;
       delegate void UpdateCallback();//刷新
        public bool bupdate
        {
            get { return timer_500ms.Enabled; }
            set { timer_500ms.Enabled = value; }
        }

        public FrRun()
        {
            InitializeComponent();
           
        }       
        public void product_list_update()
        {
            int itm_id = 0;
            int j=0;
            cb_product_list.Items.Clear();

            foreach (string str in UI.COM.product.product_list)
            {
                 cb_product_list.Items.Add(str);
                if (!str.Equals(VAR.gsys_set.cur_product_name))
                    itm_id++;
                else
                    cb_product_list.Text = str;
            }
            
        }     
        private void FrRun_Load(object sender, EventArgs e)
        {
            bool ret;
            UI.COM.MVS.CamGet = new CAMERA(cam_get);
            VAR.sys_inf.Init(lb_war_inf, MT.OUT_Bee,VAR.gsys_set.beep_tmr);//lb_war_inf
            VAR.msg.StartUpdate(dvg_msg);//委托信息显示到控件     
            Thread.Sleep(2000);
            Application.DoEvents();
            product_list_update();
            CamOpen = new CamControl(cam_open);
            CamClose = new CamControl(cam_close);
            if (CamOpen != null)
            {
              ret=  CamOpen();
              if (!ret)
              {
                  VAR.ErrMsg("打开相机失败");
                  VAR.sys_inf.Set(EM_ALM_STA.WAR_RED_FLASH, "打开相机失败", 0);
              }
            }
            switch (cTabControl1.SelectedIndex)
            {                    
                case 1:
                    ax_pable.axis_x = MT.AxList_WS_FEED[0];
                    ax_pable.axis_y = MT.AxList_WS_FEED[1];
                    ax_pable.axis_z = MT.AxList_WS_FEED[2];
                    ax_pable.axis_a = MT.AxList_WS_FEED[3];
                    ax_pable.update_show();
                    break;
                case 0:
                    ax_pable.axis_x = MT.AxList_WS_GET[0];
                    ax_pable.axis_y = MT.AxList_WS_GET[1];
                    ax_pable.axis_z = MT.AxList_WS_GET[2];
                    ax_pable.axis_a = MT.AxList_WS_GET[3];
                    ax_pable.update_show();
                    break;
                default: break;
            }

        }
        private void btn_run_Click(object sender, EventArgs e)
        {
            VAR.SysMsg("运行按钮按下");
            //检测复位状态
            foreach (CARD card in MT.CardList)
            {
                if (card.isReady == false)
                {
                    MessageBox.Show(string.Format("{0}未初始化!", card.disc), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (AXIS ax in card.AxList)
                {
                    if (ax.home_status != AXIS.HOME_STA.OK)
                    {
                        MessageBox.Show(string.Format("{0} 状态异常，{1}!\r\n请先复位", ax.disc, ax.home_status), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            //检测运行状态
            if (VAR.gsys_set.status == EM_SYS_STA.RUN) return;
            //if (VAR.gsys_set.status != EM_SYS_STA.STANDBY)
            //{
            //    MessageBox.Show("系统有错误，请复位！");
            //    return;
            //}
            VAR.gsys_set.bquit = false;
            Action.th_run();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {

            VAR.SysMsg("停止按钮按下");
            if (VAR.gsys_set.status == EM_SYS_STA.RUN)
            {
                if (MessageBox.Show("运行中，是否要停止?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Action.stop();                 
                    return;
                }
            }
            else
            if (MessageBox.Show("是确定要停止?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //k_hook.KeyDownEvent -= hook_KeyDown;//钩住键按下
                //k_hook.KeyUpEvent -= hook_KeyUp;//钩住键按下
                //k_hook.Stop();//安装键盘钩子
                //Acquistion.AllCameraDisconnect();
            }
          
            Action.stop();
          
        }   
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

                product_list_update();

                //traybox_fd.UpdateShow();
                //traybox_ok.UpdateShow();
                //traybox_ng.UpdateShow();

                //tray_fd.tray_dat = traybox_fd.box.tray_cur;
                //tray_ok.tray_dat = traybox_ok.box.tray_cur;
                //tray_ng.tray_dat = traybox_ng.box.tray_cur;

                //tray_fd.UpdateShow();                
                //tray_ok.UpdateShow();
                //tray_ng.UpdateShow();
               // ws1.UpdateShow();
              //  ws2.UpdateShow();

              //ws5.UpdateShow();
               // Turnplate.UpdateShow();
                if (UI.Action.bNullRun)
                    checkBox_null_run.Checked = true;
            }
        }
        private void timer_500ms_Tick(object sender, EventArgs e)
        {
            Task show = new Task(() =>
                {
                    UpdateDisplay();
                }
            );
            show.Start();
        }
  
        #region 相机相关按钮事件
        bool cam_open()
        {
            short ret = 0;
            try
            {
                ret = axGeneralVisionControl1.OpenCamera();
                if (ret != 0)
                    return false;
                else

                    return true;
            }

            catch (Exception e)
            {
                VAR.ErrMsg(e.ToString());
                return false;
            }

            ;
        }
        bool cam_close()
        {
            try
            {
                axGeneralVisionControl1.CloseVision();
                return true;
            }

            catch (Exception e)
            {
                VAR.ErrMsg(e.ToString());
                return false;
            }
        }

        public EM_RES cam_get(out ST_XYZ res, int cam_id)
        {
            short cam = (short)(cam_id);
            short re = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            res.x = 0;
            res.y = 0;
            res.z = 0;
            try
            {

            //   re = axGeneralVisionControl1.TriggerCam(cam, 0, ref x, ref y, ref z);
                if (re != 0)
                {
                    VAR.ErrMsg("拍照失败");
                    return EM_RES.ERR;
                }
                res.x = x;
                res.y = y;
                res.z = z;
                return EM_RES.OK;
            }
            catch (Exception e)
            {
                VAR.ErrMsg(e.ToString());
                return EM_RES.ERR;
            }
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            CamOpen();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EM_RES ret = EM_RES.OK;
            ST_XYZ res=new ST_XYZ();
            int cam_id = 2;
            ret = UI.COM.MVS.get_vs(out res, cam_id);
            if (ret != EM_RES.OK)
            {
                MessageBox.Show("拍照失败");
                return;
            }
            MessageBox.Show(res.x.ToString() + "\n" + res.y.ToString() + "\n" + res.z.ToString());
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            EM_RES ret = EM_RES.OK;
            ST_XYZ res=new ST_XYZ();
            int cam_id=1;
            ret = UI.COM.MVS.get_vs(out res, cam_id);
            if(ret!=EM_RES.OK)
            {
                MessageBox.Show("拍照失败");
                return;
            }
            MessageBox.Show(res.x.ToString() + "\n" + res.y.ToString() + "\n" + res.z.ToString());
        }

        private void bt_mark_cam1_Click(object sender, EventArgs e)
        {
          int cam_id = 1;
          EM_RES ret=  UI.COM.MVS.cam_get_scle(ref VAR.gsys_set.bquit, cam_id);
          if (ret == EM_RES.OK)
              MessageBox.Show("获取比例成功");
          else
              MessageBox.Show("获取比例失败");
        }
        private void bt_mark_cam2_Click(object sender, EventArgs e)
        {
            int cam_id =2;
            EM_RES ret = UI.COM.MVS.cam_get_scle(ref VAR.gsys_set.bquit, cam_id);
            if (ret == EM_RES.OK)
                MessageBox.Show("获取比例成功");
            else
                MessageBox.Show("获取比例失败");
        }

        private void bt_close_cam_Click(object sender, EventArgs e)
        {            
            CamClose();
        }

        #endregion


        private void cTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
                   ax_pable.clear();
                   switch (((CTabControl)sender).SelectedIndex)
                   {
                       case 1:
                       
                           ax_pable.axis_x = MT.AxList_WS_FEED[0];
                           ax_pable.axis_y = MT.AxList_WS_FEED[1];
                           ax_pable.axis_z = MT.AxList_WS_FEED[2];
                           ax_pable.axis_a = MT.AxList_WS_FEED[3];
                           ax_pable.update_show();
                           break;

                       case 0:
                       
                           ax_pable.axis_x = MT.AxList_WS_GET[0];
                           ax_pable.axis_y = MT.AxList_WS_GET[1];
                           ax_pable.axis_z = MT.AxList_WS_GET[2];
                           ax_pable.axis_a = MT.AxList_WS_GET[3];
                           ax_pable.update_show();
                           break;
                       default: break;
                   }
        }

        private void cTabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ax_pable.clear();
            switch (((CTabControl)sender).SelectedIndex)
            {
                case 1:

                    ax_pable.axis_x = MT.AxList_WS_FEED[0];
                    ax_pable.axis_y = MT.AxList_WS_FEED[1];
                    ax_pable.axis_z = MT.AxList_WS_FEED[2];
                    ax_pable.axis_a = MT.AxList_WS_FEED[3];
                    ax_pable.update_show();
                    break;
                case 0:
                    ax_pable.axis_x = MT.AxList_WS_GET[0];
                    ax_pable.axis_y = MT.AxList_WS_GET[1];
                    ax_pable.axis_z = MT.AxList_WS_GET[2];
                    ax_pable.axis_a = MT.AxList_WS_GET[3];
                    ax_pable.update_show();
                    break;
                default: break;
            }
        } 

        private void checkBox_null_run_CheckedChanged(object sender, EventArgs e)
        {
            UI.Action.bNullRun = !UI.Action.bNullRun;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VAR.SysMsg("暂停按钮按下");
            if (VAR.gsys_set.status == EM_SYS_STA.RUN)
            {
                if (MessageBox.Show("运行中，是否要暂停?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Action.stop();
                    return;
                }
            }
            else

            Action.stop();

        }
    }
}
