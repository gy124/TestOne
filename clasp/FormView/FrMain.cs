using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionCtrl;
using System.IO;
using System.Threading;

namespace clasp
{
    public partial class FrMain : Form
    {
        public static FrCount frcount = null;// = new FrSys();
         public static FrSys frsys = null;// = new FrSys();
         public static FrRun frrun = null;// = new FrRun();
         public static FrProduct frproduct = null;// = new FrProduct();
        //public static FrUser frsuser = null;// = new FrUser();
        public static FrRst frrst = null;// = new FrRst();
        public static FrMain frmain = null;
        public FrMain()
        {
            InitializeComponent();
            frmain = this;
        }

        private void FrMain_Load(object sender, EventArgs e)
        {
            EM_RES ret;

            VAR.msg.ShowMsgCfg(1000, (Msg.EM_MSGTYPE)0xffff);
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "系统启动...");
            //load sys config
            VAR.gsys_set.LoadSysCfg();
            VAR.gsys_set.status = EM_SYS_STA.UNKOWN;
            VAR.gsys_set.bclose = false;

            VAR.sys_inf.Set(EM_ALM_STA.WAR_YELLOW_FLASH, "正在加载", 2, true);

            //加载产品
            try
            {
                //if (COM.NGDef == null)
                //    COM.NGDef = new NGCodeDef();
                //COM.NGDef.LoadCfg();

                //if (COM.product == null) COM.product = new Product();
                //ret = COM.product.LoadDat(VAR.gsys_set.cur_product_name);
                //if (ret != EM_RES.OK) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "产品数据加载失败!");
                //else VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "产品数据加载成功!");

                //foreach (WS ws in COM.list_ws)
                //{
                //    ws.LoadCfg();
                //}

                ////加载吸头
                //COM.XtInit(VAR.gsys_set.cur_product_name);
                ////加载仓储
                //COM.TrayBoxInit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //硬件初始化
            Task TaskHWInit = new Task(() =>
            {
                ret = MT.Init(Path.GetFullPath("..") + "\\syscfg\\");
                if (ret != EM_RES.OK) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "板卡初始化失败!");
                else VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "板卡初始化成功!");
            }
            );
            TaskHWInit.Start();

            //相机初始化
            //Task TaskCamInit = new Task(() =>
            //{
            //    ret = MT.Init(Path.GetFullPath("..") + "\\syscfg\\");
            //    if (ret != EM_RES.OK) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "板卡始化失败!");
            //    else VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "板卡始化成功!");
            //}
            //ret = COM.CamInit();
            //if (ret != EM_RES.OK) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "相机初始化失败!");
            //else VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "相机初始化成功!");
            //);
            //TaskHWInit.Start();
            //COM.CamInit();

            //create form   
            //Task TaskFormInit = new Task(() =>
            //{
            //    if (frsys == null) frsys = new FrSys();
            //    frsys.bupdate = false;
            //    if (frrst == null) frrst = new FrRst();
            //    frrst.bupdate = false;
            //    if (frproduct == null) frproduct = new FrProduct();
            //    frproduct.bupdate = false;
            //    if (frrun == null) frrun = new FrRun();
            //    frrun.bupdate = false;

            //    pnl_sub.Controls.Clear();
            //    frrun.TopLevel = false;
            //    frrun.FormBorderStyle = FormBorderStyle.None;                
            //}
            //);
            //TaskFormInit.Start();
            form_sel("rbtn_run");//显示运行界面
            Application.DoEvents();
            Thread.Sleep(10);

         //   timer_reconnect.Enabled = true;
        //    if (frrun != null) frrun.bupdate = true;

            VAR.sys_inf.Set(EM_ALM_STA.WAR_YELLOW_FLASH, "待回零", 10, true);

            if (MT.isReady)
            {
                ////钩子侦测按键           
                //k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);//钩住键按下
                //k_hook.KeyUpEvent += new KeyEventHandler(hook_KeyUp);//钩住键按下
                //k_hook.Start();//安装键盘钩子
            }
            MT.SetSafeFunc();
            //MT.GPIO_OUT_TT_REV.ChkSafe = Turntable.ChkSafe;
            //MT.GPIO_OUT_TT_FWD.ChkSafe = Turntable.ChkSafe;
        }
        public void form_sel(string btn_name, string page_name = "", string page_name2 = "")
        {
            //    if (frrun != null) frrun.timer_update.Enabled = false;
            //    if (frsys != null) frsys.timer_update.Enabled = false;
           
            Font ft = new Font("Microsoft Sans Serif", 18, FontStyle.Bold);
            rbtn_run.Font = ft;
            rbtn_product.Font = ft;
            rbtn_sys.Font = ft;
          //  rbtn_count.Font = ft;

            rbtn_run.ForeColor = Color.DarkGray;
            rbtn_product.ForeColor = Color.DarkGray;
            rbtn_sys.ForeColor = Color.DarkGray;
          //  rbtn_count.ForeColor = Color.DarkGray;

            rbtn_run.BackColor = Color.Transparent;
            rbtn_product.BackColor = Color.Transparent;
            rbtn_sys.BackColor = Color.Transparent;
        //    rbtn_count.BackColor = Color.Transparent;

            Form form = null;
            ft = new Font("Microsoft Sans Serif", 22, FontStyle.Bold);

            //if (frsys == null) frsys = new FrSys();
            if (frsys != null) frsys.bupdate = false;
            //if (frrst == null) frrst = new FrRst();
            if (frrst != null) frrst.bupdate = false;
            //if (frproduct == null) frproduct = new FrProduct();
            if (frproduct != null) frproduct.bupdate = false;
            //if (frrun == null) frrun = new FrRun();
            if (frrun != null) frrun.bupdate = false;

            switch (btn_name)
            {
                
                case "rbtn_run":
                  //  rbtn_run.Checked = true;
                    rbtn_run.ForeColor = Color.WhiteSmoke;
                    rbtn_run.Font = ft;
                    if (frrun == null) frrun = new FrRun();
                    form = frrun;
                    frrun.bupdate = true;
                //    foreach (Cam cam in COM.ListCam) cam.mCogRecDisplay = frrun.cogDisplayer_run.cogRecordDisplay;
                    break;
                case "rbtn_product":
                  //  rbtn_product.Checked = true;
                    rbtn_product.ForeColor = Color.WhiteSmoke;
                    rbtn_product.Font = ft;
                    if (frproduct == null) frproduct = new FrProduct();
                    form = frproduct;
                    frproduct.bupdate = true;
                 //   foreach (Cam cam in COM.ListCam) cam.mCogRecDisplay = frproduct.cogDisplayer_product.cogRecordDisplay;
                    ////page select
                    //if (frproduct.ctb_prodcut.TabPages[page_name] != null) frproduct.ctb_prodcut.TabPages[page_name].Select();
                    //if (page_name == "tb_tg_cfg")
                    //{
                    //    //VisionRun.Display = new VisionDisplay(frproduct.cogRecordDisplay_live, "");
                    //    if (frproduct.ctb_tg_view.TabPages[page_name2] != null) frproduct.ctb_tg_view.TabPages[page_name2].Select();
                    //}
                    //else if (page_name == "tb_tg_vs")
                    //{
                    //    //VisionRun.Display = new VisionDisplay(frproduct.DisPlayAndImageMask1.CogRecordDisplay, "");
                    //    if (frproduct.ctb_vs_cfg.TabPages[page_name2] != null) frproduct.ctb_vs_cfg.TabPages[page_name2].Select();
                    //}
                    //else if (page_name == "tb_ofs")
                    //{
                    //    //VisionRun.Display = new VisionDisplay(frproduct.cogRecordDisplay_ofs, "");
                    //    if (frproduct.ctb_ofs.TabPages[page_name2] != null) frproduct.ctb_ofs.TabPages[page_name2].Select();
                    //}
                    break;
                case "rbtn_count":
                    //rbtn_count.Checked = true;
                    rbtn_count.ForeColor = Color.WhiteSmoke;
                    rbtn_count.Font = ft;
                    if (frcount == null) frcount = new FrCount();
                    form = frcount;
                    break;
                case "rbtn_sys":
                  //  rbtn_sys.Checked = true;
                    rbtn_sys.ForeColor = Color.WhiteSmoke;
                    rbtn_sys.Font = ft;
                    if (frsys == null) frsys = new FrSys();
                    form = frsys;
                    frsys.bupdate = true;
                 //   foreach (Cam cam in COM.ListCam) cam.mCogRecDisplay = frsys.CogRecordDisplay_sys.cogRecordDisplay;
                    ////page select
                    //if (frsys.ctb_sys.TabPages[page_name] != null) frsys.ctb_sys.TabPages[page_name].Select();
                    //if (page_name == "tb_cali")
                    //{
                    //    if (frsys.ctb_cali.TabPages[page_name2] != null) frsys.ctb_cali.TabPages[page_name2].Select();
                    //}
                    //frsys.timer_update.Enabled = true;
                    //if (VisionRun.Display.m_strName != "frsysCogRecordDisplay")
                    //    VisionRun.Display = new VisionDisplay(frsys.CogRecordDisplay, "frsysCogRecordDisplay");
                    break;
                //case "rbtn_user":
                //    rbtn_user.Checked = true;
                //    rbtn_user.ForeColor = Color.WhiteSmoke;
                //    rbtn_user.Font = ft;
                //    if (frsuser == null) frsuser = new FrUser();
                //    form = frsuser;
                //    break;

                //case "rbtn_rst":
                //    rbtn_rst.Checked = true;
                //    rbtn_rst.ForeColor = Color.WhiteSmoke;
                //    rbtn_rst.Font = ft;
                //    if (frrst == null) frrst = new FrRst();
                //    form = frrst;
                //    frrst.bupdate = true;
                //    break;
                default:
                    break;
            }

            if (form == null) return;
          //  pnl_sub.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
           //  this.panel1.Controls.Add(form);
            form.Parent = this.panel1;
  


            //    pnl_sub.Controls.Add(form);

            //form.Width = pnl_sub.Width;
            //form.Height = pnl_sub.Height - 8;
            form.Show();
        }

        private void rbtn_run_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();//清除面板
            switch (((Button)sender).Name)
            {
                case "rbtn_run":
                    form_sel(((Button)sender).Name);
                    break;
                case "rbtn_product":
                    if (frproduct == null) frproduct = new FrProduct();
                    //switch (frproduct.ctb_product.SelectedTab != null ? frproduct.ctb_product.SelectedTab.Name : "")
                    //{
                    //    case "tb_tg_cfg":
                    //        form_sel(((RadioButton)sender).Name, frproduct.ctb_product.SelectedTab.Name);
                    //        break;
                    //    case "tb_tg_vs":
                    //        form_sel(((RadioButton)sender).Name, frproduct.ctb_product.SelectedTab.Name);
                    //        break;
                    //    case "tb_ofs":
                    //        form_sel(((RadioButton)sender).Name, frproduct.ctb_product.SelectedTab.Name);
                    //        break;
                    //    default:
                    //        form_sel(((RadioButton)sender).Name, frproduct.ctb_product.SelectedTab.Name);
                    //        break;
                    //}
                    form_sel(((Button)sender).Name);
                    break;
                case "rbtn_sys":
                    if (frsys == null) frsys = new FrSys();
                    //form_sel(((RadioButton)sender).Name, frsys.ctb_sys.SelectedTab.Name);
                    form_sel(((Button)sender).Name);
                    break;
                case "rbtn_count":
                    form_sel(((Button)sender).Name);
                    break;
                default:
                    form_sel(((Button)sender).Name);
                    break;
            }
        }

   
    }
}
