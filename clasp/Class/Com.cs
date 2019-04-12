using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasp.Class
{
    class Com
    {
    }
    public static class COM
    {
        //NG代码
        //public static NGCodeDef NGDef = new NGCodeDef();
        ////工站
        //public static WS ws1 = new WS(0, MT.AXIS_WS1_F, MT.AXIS_WS1_B, MT.AXIS_WS1_U, MT.List_CLD_WS1_FR,
        //    MT.List_CLD_WS1_BK);

        //public static WS ws2 = new WS(1, MT.AXIS_WS2_F, MT.AXIS_WS2_B, MT.AXIS_WS2_U, MT.List_CLD_WS2_FR,
        //    MT.List_CLD_WS2_BK);

        //public static WS ws3 = new WS(2, MT.AXIS_WS3_F, MT.AXIS_WS3_B, MT.AXIS_WS3_U, MT.List_CLD_WS3_FR,
        //    MT.List_CLD_WS3_BK);

        //public static WS ws4 = new WS(3, MT.AXIS_WS4_F, MT.AXIS_WS4_B, MT.AXIS_WS4_U, MT.List_CLD_WS4_FR,
        //    MT.List_CLD_WS4_BK);

        //public static List<WS> list_ws = new List<WS>() { ws1, ws2, ws3, ws4 };

        //public static TrayBox traybox_fd = new TrayBox("TrayBox_FD", "供料仓", TrayBox.EM_DIR.IN_OUT, 10, MT.AXIS_UL_FD_X,
        //    MT.AXIS_UL_FD_Z, MT.GPIO_IN_UL_INP_FD_TRAYBOX, MT.GPIO_IN_UL_RDY_FD_TRAY, MT.GPIO_OUT_UL_FD_TRAY,
        //    MT.GPIO_IN_UL_FD_TRAY, MT.CLD_UL_TRAY_FD);

        //public static TrayBox traybox_ok = new TrayBox("TrayBox_OK", "OK料仓", TrayBox.EM_DIR.IN_OUT, 10, MT.AXIS_DL_OK_X,
        //    MT.AXIS_DL_OK_Z, MT.GPIO_IN_DL_INP_OK_TRAYBOX, MT.GPIO_IN_DL_RDY_OK_TRAY, MT.GPIO_OUT_DL_OK_TRAY,
        //    MT.GPIO_IN_DL_OK_TRAY, MT.CLD_DL_OKTRAY_HD);

        //public static TrayBox traybox_ng = new TrayBox("TrayBox_NG", "NG料仓", TrayBox.EM_DIR.IN_OUT, 10, MT.AXIS_DL_NG_X,
        //    MT.AXIS_DL_NG_Z, MT.GPIO_IN_DL_INP_NG_TRAYBOX, MT.GPIO_IN_DL_RDY_NG_TRAY, MT.GPIO_OUT_DL_NG_TRAY,
        //    MT.GPIO_IN_DL_NG_TRAY, MT.CLD_DL_NGTRAY_HD);

        //public static List<TrayBox> List_traybox = new List<TrayBox>() { traybox_fd, traybox_ok, traybox_ng };

        ////相机
        //public static Cam CamUp1 = new Cam("CamUp1", "上相机1", true, false, false, MT.CamUp1Triger, MT.MoveHandle,
        //    MT.GPIO_OUT_UL_CAM_BK);

        //public static Cam CamUp2 = new Cam("CamUp2", "上相机2", false, false, false, MT.CamUp2Triger, MT.MoveHandle,
        //    MT.GPIO_OUT_UL_CAM_FR);

        //public static Cam CamDown = new Cam("CamDown", "下相机", false, false, true, MT.CamDownTriger, MT.MoveHandle,
        //    MT.GPIO_OUT_UL_CAM_DW);

        //public static List<Cam> ListCam = new List<Cam>() { CamUp1, CamUp2, CamDown };

        ////目标
        //public static Product product = new Product();

        ////左光箱
        //public static LightBox LeftLightBox = new LightBox("LeftLightBox", "左光箱", MT.AXIS_BOX_L_X1, MT.AXIS_BOX_L_X2,
        //    MT.AXIS_BOX_L_Z1, MT.AXIS_BOX_L_Z2);

        ////右光箱
        //public static LightBox RightLightBox = new LightBox("RightLightBox", "右光箱", MT.AXIS_BOX_R_X1, MT.AXIS_BOX_R_X2,
        //    MT.AXIS_BOX_R_Z1, MT.AXIS_BOX_R_Z2);

        ////OTP光箱
        //public static LightBox OTPLightBox =
        //    new LightBox("OTPLightBox", "OTP光箱", null, null, null, null, MT.AXIS_BOX_OTP_Z);

        ////吸头
        //public static XT xt1 = new XT(0, ref MT.AXIS_UL_X, ref MT.AXIS_UL_Y, ref MT.AXIS_UL_Z, ref MT.AXIS_UL_U1,
        //    ref MT.AXIS_UL_FD_X, ref traybox_fd, MT.CLD_UL_N1, MT.GPIO_OUT_UL_PZK_N1);

        //public static XT xt2 = new XT(1, ref MT.AXIS_UL_X, ref MT.AXIS_UL_Y, ref MT.AXIS_UL_Z, ref MT.AXIS_UL_U2,
        //    ref MT.AXIS_UL_FD_X, ref traybox_fd, MT.CLD_UL_N2, MT.GPIO_OUT_UL_PZK_N2);

        //public static List<XT> ListXT = new List<XT> { xt1, xt2 };

        //public static EM_RES XtInit(string productname)
        //{

        //    foreach (XT xt in COM.ListXT)
        //    {
        //        EM_RES ret = xt.LoadCfg(productname);
        //        if (ret != EM_RES.OK)
        //        {
        //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, xt.disc + "加载参数出错!");
        //            MessageBox.Show(xt.disc + "加载参数出错!");
        //            return ret;
        //        }
        //    }

        //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "加载吸头参数成功！");
        //    return EM_RES.OK;
        //}

        //public static EM_RES TrayBoxInit(string productname = "")
        //{
        //    foreach (TrayBox traybox in COM.List_traybox)
        //    {
        //        EM_RES ret = traybox.LoadCfg(productname);
        //        if (ret != EM_RES.OK)
        //        {
        //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, traybox.disc + "加载参数出错!");
        //            MessageBox.Show(traybox.disc + "加载参数出错!");
        //            return ret;
        //        }
        //    }

        //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "仓储参数成功！");
        //    return EM_RES.OK;
        //}

        //视觉
       // public static Msg vs_msg = new Msg(200, Msg.EM_MSGTYPE.NOR | Msg.EM_MSGTYPE.ERR);

        #region 设备复位

        public static bool bhomeing = false;

        //public static EM_RES Home()
        //{
        //    try
        //    {
        //        VAR.gsys_set.bquit = false;
        //        bhomeing = true;
        //        //光箱/上料
        //        EM_RES resLB = EM_RES.OK;
        //        EM_RES resRB = EM_RES.OK;
        //        EM_RES resUL = EM_RES.OK;
        //        EM_RES res = EM_RES.OK;

        //        Task taskLeftLbHome = new Task(() => { resLB = LeftLightBox.Home(ref VAR.gsys_set.bquit); });
        //        Task taskRightLbHome = new Task(() => { resRB = RightLightBox.Home(ref VAR.gsys_set.bquit); });
        //        Task taskUploadHome = new Task(() => { resUL = UploadModle.Home(ref VAR.gsys_set.bquit); });
        //        taskLeftLbHome.Start();
        //        taskRightLbHome.Start();
        //        taskUploadHome.Start();

        //        //wait
        //        while (!VAR.gsys_set.bquit)
        //        {
        //            if (taskLeftLbHome.IsCompleted && taskLeftLbHome.IsCompleted && taskLeftLbHome.IsCompleted) break;
        //            Application.DoEvents();
        //            Thread.Sleep(10);
        //        }

        //        if (resLB != EM_RES.OK || resRB != EM_RES.OK || resUL != EM_RES.OK) res = EM_RES.ERR;

        //        if (VAR.gsys_set.bquit || res != EM_RES.OK)
        //        {
        //            LeftLightBox.Stop();
        //            RightLightBox.Stop();
        //            UploadModle.Stop();
        //            return VAR.gsys_set.bquit ? EM_RES.QUIT : res;
        //        }



        //        //下料

        //        EM_RES resDL = EM_RES.OK;

        //        Task taskDownloadHome = new Task(() => { resDL = DownloadModle.Home(ref VAR.gsys_set.bquit); });

        //        taskDownloadHome.Start();

        //        //wait
        //        while (!VAR.gsys_set.bquit)
        //        {
        //            if (taskDownloadHome.IsCompleted) break;
        //            Application.DoEvents();
        //            Thread.Sleep(10);
        //        }

        //        if (resDL != EM_RES.OK) res = EM_RES.ERR;

        //        if (VAR.gsys_set.bquit || res != EM_RES.OK)
        //        {
        //            //taskTurnplateHome.Stop();
        //            DownloadModle.Stop();
        //            return VAR.gsys_set.bquit ? EM_RES.QUIT : res;
        //        }
        //        //转盘
        //        EM_RES resTP = EM_RES.OK;
        //        Task taskTurnplateHome = new Task(() => { resTP = EM_RES.OK; });
        //        taskTurnplateHome.Start();
        //        //wait
        //        while (!VAR.gsys_set.bquit)
        //        {
        //            if (taskTurnplateHome.IsCompleted) break;
        //            Application.DoEvents();
        //            Thread.Sleep(10);
        //        }
        //        if (resTP != EM_RES.OK) res = EM_RES.ERR;
        //        return EM_RES.OK;
        //    }
        //    finally
        //    {
        //        bhomeing = false;
        //    }
        //}

        /// <summary>
        /// 停止轴运动，停止Home动作
        /// </summary>
        public static void Stop()
        {
          //  VAR.gsys_set.bquit = true;
            //LeftLightBox.Stop();
            //RightLightBox.Stop();
         //   UploadModle.Stop();
            //taskTurnplateHome.Stop();
           // DownloadModle.Stop();

            //foreach (WS ws in COM.list_ws)
            //{
            //    ws.ax_u.Stop();
            //    ws.ax_fr.Stop();
            //    ws.ax_bk.Stop();
            //}
        }

        #endregion

        #region 相机初始化

        //public static EM_RES CamInit(int timeout = 1000)
        //{
        //    //Task TaskCamInit = new Task(() =>
        //    //{
        //    try
        //    {

        //        foreach (Cam cam in ListCam)
        //        {
        //            string path = string.Format("{0}\\product\\{1}\\Camera\\{2}\\", Path.GetFullPath(".."),
        //                VAR.gsys_set.cur_product_name, cam.mName);
        //            cam.status = Cam.CAM_STA.INIT;
        //            cam.Init(path);
        //            cam.LoadTask(path);
        //            path = string.Format("{0}\\syscfg\\Calibration\\{1}\\", Path.GetFullPath(".."), cam.mName);
        //            cam.LoadCaliTool(path);
        //            if (cam.isInit) cam.status = Cam.CAM_STA.READY;
        //            else cam.status = Cam.CAM_STA.DISCONNECT;
        //        }

        //        //affTransTool use pix space               
        //        //Cam.VisionTask task = CamUp1.List_vs_task.Find(s => s.TaskName.Equals("AffTransTool"));
        //        //if (task != null) task.ListCaliTool = new List<Cam.CaliTool>();

        //        //task = CamUp2.List_vs_task.Find(s => s.TaskName.Equals("AffTransTool"));
        //        //if (task != null) task.ListCaliTool = new List<Cam.CaliTool>();

        //        //task = CamDown.List_vs_task.Find(s => s.TaskName.Equals("AffTransTool"));
        //        //if (task != null) task.ListCaliTool = new List<Cam.CaliTool>();


        //        //CamDown to CamUp1 transform
        //        foreach (Cam.CaliTool tool in CamUp1.ListCaliTool)
        //            CamDown.ListCaliTool.Add(tool);

        //        //CamUp2 to CamUp1 transform
        //        foreach (Cam.CaliTool tool in CamUp1.ListCaliTool)
        //            CamUp2.ListCaliTool.Add(tool);

        //        foreach (Cam cam in COM.ListCam)
        //        {
        //            cam.ListCaliTool.Sort((a, b) => { return a.name.CompareTo(b.name); });
        //        }

        //        CogVisionToolMultiThreading.Enable = true;
        //        CogVisionToolMultiThreading.ThreadCountMode =
        //            CogVisionToolMultiThreadingThreadCountModeConstants.HardwareDefined;
        //    }
        //    catch (Exception ex)
        //    {
        //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化异常", ex.Message));
        //        return EM_RES.ERR;
        //    }
        //    // }
        //    //);
        //    // TaskCamInit.Start();
        //    // await TaskCamInit;

        //    //if (timeout == 0) return EM_RES.OK;

        //    ////wait
        //    //int t = 0;
        //    //do
        //    //{
        //    //    t += 10;
        //    //    if (t >= timeout) return EM_RES.TIMEOUT;

        //    //    if (TaskCamInit.IsCompleted || VAR.gsys_set.bclose) break;
        //    //    Thread.Sleep(10);
        //    //    Application.DoEvents();

        //    //} while (true);

        //    return EM_RES.OK;
        //}

        //public static EM_RES CamLoadCailTool()
        //{
        //    try
        //    {
        //        foreach (Cam cam in ListCam)
        //        {
        //            string path = string.Format("{0}\\syscfg\\Calibration\\{1}\\", Path.GetFullPath(".."), cam.mName);
        //            cam.LoadCaliTool(path);
        //        }

        //        //CamDown to CamUp1 transform
        //        foreach (Cam.CaliTool tool in CamUp1.ListCaliTool)
        //            CamDown.ListCaliTool.Add(tool);

        //        //CamUp2 to CamUp1 transform
        //        foreach (Cam.CaliTool tool in CamUp1.ListCaliTool)
        //            CamUp2.ListCaliTool.Add(tool);

        //        foreach (Cam cam in COM.ListCam)
        //        {
        //            cam.ListCaliTool.Sort((a, b) => { return a.name.CompareTo(b.name); });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}加载校正关系异常", ex.Message));
        //        return EM_RES.ERR;
        //    }

        //    return EM_RES.OK;
        //}

        //public static bool CamisOnInitStatus
        //{
        //    get
        //    {
        //        foreach (Cam cam in ListCam)
        //        {
        //            if (cam.status == Cam.CAM_STA.INIT) return true;
        //        }

        //        return false;
        //    }
        //}

        #endregion

        #region 板卡初始化

        //public EM_RES HwInit()
        //{
        //    //硬件初始化
        //    Task TaskHWInit = new Task(() =>
        //    {
        //        EM_RES res = MT.Init(Path.GetFullPath("..") + "\\syscfg\\");
        //        if (res != EM_RES.OK) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "板卡始化失败!");
        //        else VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "板卡始化成功!");
        //    }
        //    );
        //    TaskHWInit.Start();

        //    //for (int n = 0; n < 3000; n++)
        //    //{
        //    //    if (TaskHWInit.IsCompleted || VAR.gsys_set.bclose) break;
        //    //    Thread.Sleep(10);
        //    //    Application.DoEvents();
        //    //}

        //    return EM_RES.OK;
        //}

        #endregion

        #region 测试转台复位

        //public static EM_RES TurntableHome(ref bool bquit)
        //{
        //    EM_RES res = EM_RES.OK;

        //    try
        //    {
        //        //start u axis home
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.ax_u.HomeTask(30000);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        //wait home end
        //        while (true)
        //        {
        //            Thread.Sleep(200);
        //            Application.DoEvents();
        //            foreach (WS ws in list_ws)
        //            {
        //                if (!ws.ax_u.HomeTaskisEnd) continue;
        //            }

        //            break;
        //        }

        //        //check result
        //        foreach (WS ws in list_ws)
        //        {
        //            if (ws.ax_u.HomeTaskRet != EM_RES.OK) return EM_RES.ERR;
        //        }

        //        //u up
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.ax_u.JOG_Step(ref bquit, AXIS.AX_DIR.P);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        //all up
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.FrCyUp(ref bquit);
        //            if (res != EM_RES.OK) return res;
        //            res = ws.BkCyUp(ref bquit);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        //start fr/bk axis home
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.ax_fr.HomeTask(30000);
        //            if (res != EM_RES.OK) return res;
        //            res = ws.ax_bk.HomeTask(30000);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        //wait home end
        //        while (true)
        //        {
        //            Thread.Sleep(200);
        //            Application.DoEvents();
        //            foreach (WS ws in list_ws)
        //            {
        //                if (!ws.ax_fr.HomeTaskisEnd || !ws.ax_bk.HomeTaskisEnd) continue;
        //            }

        //            break;
        //        }

        //        //check result
        //        foreach (WS ws in list_ws)
        //        {
        //            if (ws.ax_fr.HomeTaskRet != EM_RES.OK || ws.ax_bk.HomeTaskRet != EM_RES.OK) return EM_RES.ERR;
        //        }

        //        //fr/bk axis
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.ax_fr.JOG_Step(ref bquit, AXIS.AX_DIR.P);
        //            if (res != EM_RES.OK) return res;
        //            res = ws.ax_bk.JOG_Step(ref bquit, AXIS.AX_DIR.P);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        //all down
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.FrCyDown(ref bquit);
        //            if (res != EM_RES.OK) return res;
        //            res = ws.BkCyDown(ref bquit);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        //u down
        //        foreach (WS ws in list_ws)
        //        {
        //            res = ws.ax_u.JOG_Step(ref bquit, AXIS.AX_DIR.N);
        //            if (res != EM_RES.OK) return res;
        //        }

        //        return EM_RES.OK;
        //    }
        //    finally
        //    {
        //        //foreach (WS ws in list_ws)
        //        //{
        //        //    ws.ax_u.Stop();
        //        //    ws.ax_fr.Stop();
        //        //    ws.ax_bk.Stop();
        //        //}
        //    }
        //}

        //public static EM_RES TurntableHome2(ref bool bquit)
        //{
        //    EM_RES res = EM_RES.OK;

        //    try
        //    {
        //        //check
        //        if (LeftLightBox.ax_x2.home_status != AXIS.HOME_STA.OK ||
        //            LeftLightBox.ax_x2.fcmd_pos > LeftLightBox.pos_safe_xz)
        //        {
        //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR,
        //                string.Format("转台可能与{0}有干涉，请先回零{1}!", LeftLightBox.ax_x2.disc, LeftLightBox.ax_x2.disc));
        //            return EM_RES.MOVE_PROTECT;
        //        }

        //        if (RightLightBox.ax_x2.home_status != AXIS.HOME_STA.OK ||
        //            RightLightBox.ax_x2.fcmd_pos > RightLightBox.pos_safe_xz)
        //        {
        //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR,
        //                string.Format("转台可能与{0}有干涉，请先回零{1}!", RightLightBox.ax_x2.disc, RightLightBox.ax_x2.disc));
        //            return EM_RES.MOVE_PROTECT;
        //        }

        //        if (!OTPLightBox.ax_z_otp.isORG)
        //        {
        //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR,
        //                string.Format("转台可能与{0}有干涉，请先回零{1}!", OTPLightBox.ax_z_otp.disc, OTPLightBox.ax_z_otp.disc));
        //            return EM_RES.MOVE_PROTECT;
        //        }

        //        //link
        //        res = MT.CARD_ORIENTAL485_6.Init();
        //        if (res != EM_RES.OK) return res;

        //        //fr/bk
        //        foreach (WS ws in list_ws)
        //        {
        //            ws.ax_fr.SVRON = true;
        //            ws.ax_bk.SVRON = true;
        //            ws.ax_u.SVRON = true;
        //            if (ws.isFrUp)
        //            {
        //                res = ws.ax_fr.JOG_Step(ref bquit, AXIS.AX_DIR.P);
        //                if (res != EM_RES.OK) return res;
        //                ws.FrCyDown(ref bquit);
        //            }

        //            if (ws.isBkUp)
        //            {
        //                res = ws.ax_bk.JOG_Step(ref bquit, AXIS.AX_DIR.P);
        //                if (res != EM_RES.OK) return res;
        //                ws.BkCyDown(ref bquit);
        //            }

        //            if (ws.ax_fr.SVRON)
        //                ws.ax_fr.home_status = AXIS.HOME_STA.OK;
        //            if (ws.ax_bk.SVRON)
        //                ws.ax_bk.home_status = AXIS.HOME_STA.OK;

        //            res = ws.ax_u.JOG_Step(ref bquit, AXIS.AX_DIR.N);
        //            if (res != EM_RES.OK) return res;

        //            if (ws.ax_u.SVRON && Math.Abs(ws.ax_u.fenc_pos) < 1)
        //                ws.ax_u.home_status = AXIS.HOME_STA.OK;
        //        }

        //        return EM_RES.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ex.Message);
        //        return EM_RES.ERR;
        //    }
        //    finally
        //    {
        //        //foreach (WS ws in list_ws)
        //        //{
        //        //    ws.ax_u.Stop();
        //        //    ws.ax_fr.Stop();
        //        //    ws.ax_bk.Stop();
        //        //}
        //    }
        //}

        #endregion

        #region 获取当前工站对应的光箱

        //public static EM_RES GetLightBox(int ws_num, ref LightBox lb)
        //{
        //    //获取当前转盘位置
        //    Turntable.EM_STA pos = Turntable.GetWSSta(ws_num);
        //    switch (pos)
        //    {
        //        //转盘位置未知
        //        default:
        //            lb = null;
        //            return EM_RES.ERR;
        //        //上料位
        //        case Turntable.EM_STA.POS0:
        //            lb = null;
        //            break;
        //        //左光箱
        //        case Turntable.EM_STA.POS1:
        //            lb = LeftLightBox;
        //            break;
        //        //OTP光箱
        //        case Turntable.EM_STA.POS2:
        //            lb = OTPLightBox;
        //            break;
        //        //右光箱
        //        case Turntable.EM_STA.POS3:
        //            lb = RightLightBox;
        //            break;
        //    }

        //    return EM_RES.OK;
        //}

        #endregion

        public static readonly Object UDLockObj = new object();
        public static readonly Object TrunTableLockObj = new object();
    }
}
