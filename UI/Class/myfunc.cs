using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Text;
using MotionCtrl;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;
using System.ComponentModel;


namespace UI
{
    public delegate EM_RES Mact(ref bool bquit);
    public delegate void MsgShow(string msg = "");
    public delegate EM_RES CAMERA(out ST_XYZ res, int camera_id = 1);

    #region COM
    public static class COM
    {
           
        public static TrayBox traybox_get = new TrayBox("弹夹供料仓", ref MT.pos_bullet_feed_check_low, ref MT.pos_bullet_feed_check_top, 1, TrayBox.EM_DIR.ONLY_OUT, MT.CYL_bullet_feed_plate, 10, MT.AXIS_bullet_move, MT.AXIS_bullet_feed,
            MT.pos_bullet_feed_boxOUT, MT.pos_bullet_feed_boxIN, MT.CKPOS_bull_feed_box, MT.CKPOS_bull_feed_plate, MT.VACUM_move_plate);
        public static TrayBox traybox_back = new TrayBox("弹夹收料OK仓", ref  MT.pos_bullet_back_check_low, ref MT.pos_bullet_back_check_top, 2, TrayBox.EM_DIR.ONLY_IN, MT.CYL_bullet_back_plate, 10, MT.AXIS_bullet_move, MT.AXIS_bullet_back,
           MT.pos_bullet_back_boxOUT, MT.pos_bullet_back_boxIN, MT.CKPOS_bull_back_box, MT.CKPOS_bull_back_plate);
        
        //产品
        public static Product product = new Product();      
       //视觉
        public static VS MVS = new VS();
        #region 设备复位
        public static bool bhomeing = false;
        public static EM_RES Home(ref bool bquit)
        {
            try
            {
                bquit = false;
                bhomeing = true;               
                EM_RES resLB = EM_RES.OK;
                EM_RES resRB = EM_RES.OK;
                EM_RES resUL = EM_RES.OK;
                EM_RES resTM = EM_RES.OK;
                EM_RES resBFD = EM_RES.OK;
                EM_RES resBBK = EM_RES.OK;
                EM_RES resFD = EM_RES.OK;
                EM_RES resBK = EM_RES.OK;
                EM_RES resGT = EM_RES.OK;
                EM_RES res = EM_RES.OK;


                Task taskBBK = new Task(() => { resBBK = WsBuBK.home(ref VAR.gsys_set.bquit); });
                Task taskBFD = new Task(() => { resBFD = WsBuFD.home(ref VAR.gsys_set.bquit); });
                Task taskBK = new Task(() => { resBK = WSBack.home(ref VAR.gsys_set.bquit); });
                Task taskFD = new Task(() => { resFD = WSFeed.home(ref VAR.gsys_set.bquit); });
                Task taskGT = new Task(() => { resGT = WSGet.home(ref VAR.gsys_set.bquit); });
                Task taskTM = new Task(() => { resTM = WsTrayMove.home(ref VAR.gsys_set.bquit); });
                //等待退出
                while (!(WsBuBK.TaskRun.IsCompleted && WsBuFD.TaskRun.IsCompleted && WsTrayMove.TaskRun.IsCompleted))
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                    if (bquit)
                        break;
                }

                taskTM.Start();
                taskFD.Start();
                taskBBK.Start();
                taskBFD.Start();
                while (!bquit)
                {
                    if (taskTM.IsCompleted) break;
                    Application.DoEvents();
                    Thread.Sleep(10);
                }
                if (!bquit && resTM != EM_RES.OK)
                {
                    // 停止所有
                    return EM_RES.ERR;
                }
                if (!bquit)
                {
                    taskGT.Start();
                    taskBK.Start();
                }
                while (!bquit)
                {
                    if (taskGT.IsCompleted && taskBK.IsCompleted && taskBFD.IsCompleted && taskBBK.IsCompleted
                        && taskFD.IsCompleted) break;
                    Application.DoEvents();
                    Thread.Sleep(10);
                }
                if (resBFD == EM_RES.OK && resBBK == EM_RES.OK && resFD == EM_RES.OK && resBK == EM_RES.OK
                    && resGT == EM_RES.OK && resTM == EM_RES.OK) 
                    return EM_RES.OK;
                else
                   
                    if (bquit)
                        return EM_RES.QUIT;
                return EM_RES.ERR;
            }
            catch
            {
                VAR.ErrMsg("全部回原中发生未知错误！");
                bquit = true;
                return EM_RES.ERR;
            }
            finally
            {
                bhomeing = false;
                Stop();
            }
        }
        /// <summary>
        /// 停止轴运动，停止Home动作
        /// </summary>
        public static void Stop()
        {
            WsBuBK.Stop();
            WsBuFD.Stop();
            WSBack.Stop();
            WSFeed.Stop();
            WSGet.Stop();
            WsTrayMove.Stop();
        }
        #endregion
    }
    #endregion
    #region 运动控制
    public static class MT
    {
     
        public enum WS_ID { get = 5, feed = 4, back = 3, bull_back = 2, bull_feed = 1, bull_mov = 6, tray = 7 }
        #region 板卡定义
        //public static CARD CARD_ECI2400_1 = new CARD(1, "192.168.0.101", 4, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2400", "左光箱");
        //public static CARD CARD_ECI2400_2 = new CARD(2, "192.168.0.102", 4, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2400", "右光箱");
        //public static CARD CARD_ECI2600_3 = new CARD(3, "192.168.0.103", 6, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2600", "下料");
        //public static CARD CARD_ECI0064_4 = new CARD(4, "192.168.0.104", 0, 32, 32, CARD.BRAND.ZMOTION, CARD.TYPE.IO, "ECI0064", "下料");
        //public static CARD CARD_DMC3800_5 = new CARD(5, 0, 8, 16, 16, CARD.BRAND.LEADSHINE, CARD.TYPE.MOTION, "DMC3800", "上料");
        public static CARD CARD_GD_6 = new CARD(6, 0, 8, 16, 16, CARD.BRAND.GOOGOLTECH, CARD.TYPE.MOTION, "固高卡1", "固高测试");
        public static CARD CARD_GD_7 = new CARD(7, 1, 8, 16, 16, CARD.BRAND.GOOGOLTECH, CARD.TYPE.MOTION, "固高卡0", "固高测试");
        public static CARD CARD_GD_EX_0 = new CARD(8, 0, 8, 16, 16, CARD.BRAND.GOOGOLTECH, CARD.TYPE.CAN_IO, "固高卡扩展0", "固高测试", 0);
        public static CARD CARD_GD_EX_1 = new CARD(9, 1, 8, 16, 16, CARD.BRAND.GOOGOLTECH, CARD.TYPE.CAN_IO, "固高卡扩展1", "固高测试", 0);

        public static List<CARD> CardList = new List<CARD> { CARD_GD_6, CARD_GD_7, CARD_GD_EX_0, CARD_GD_EX_1 };
        #endregion
        #region IO 定义
        public static GPIO GPIO_OUT_ON_NULL = new GPIO(0, null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "OUT_ON_NULL", GPIO.IO_STA.OUT_ON);
        public static GPIO GPIO_IN_ON_NULL = new GPIO(0, null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "IN_ON_NULL", GPIO.IO_STA.IN_ON);
        public static GPIO GPIO_OUT_OFF_NULL = new GPIO(0, null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "OUT_OFF_NULL", GPIO.IO_STA.OUT_OFF);
        public static GPIO GPIO_IN_OFF_NULL = new GPIO(0, null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "IN_OFF_NULL", GPIO.IO_STA.IN_OFF);
        #region OUT
        //固高测试
        //弹夹气缸
        public static GPIO GO_CYL_bullet_up = new GPIO(0, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹顶升气缸");
        public static GPIO CKB_CYL_bullet_up = new GPIO(2, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "弹夹顶升气缸原位");
        public static GPIO OUT_CYL_bullet_up = new GPIO(3, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "弹夹顶升气缸动位");
        public static Cylinder CYL_bullet_up = new Cylinder(GO_CYL_bullet_up, OUT_CYL_bullet_up, CKB_CYL_bullet_up);

        public static GPIO GO_bullet_back_plate_in = new GPIO(3, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹收料载盘气缸进弹夹");
        public static GPIO OUT_CYL_bullet_back_plate = new GPIO(9, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "弹夹收料载盘气缸动位");
        public static GPIO CKB_CYL_bullet_back_plate = new GPIO(8, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "弹夹收料载盘气缸原位");
        public static GPIO GO_bullet_back_plate_out = new GPIO(13, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹收料载盘气缸（出弹夹");
        public static Cylinder CYL_bullet_back_plate = new Cylinder(2, GO_bullet_back_plate_out, OUT_CYL_bullet_back_plate, CKB_CYL_bullet_back_plate, GO_bullet_back_plate_in);

        public static GPIO GO_bullet_feed_plate_in = new GPIO(12, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹上料载盘拉出气缸进弹");
        public static GPIO OUT_CYL_bullet_feed_plate = new GPIO(6, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "弹夹上料载盘拉出气缸原位");
        public static GPIO CKB_CYL_bullet_feed_plate = new GPIO(7, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "弹夹上料载盘拉出气缸动位");
        public static GPIO GO_bullet_feed_plate_out = new GPIO(2, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹上料载盘拉出气缸（出弹夹）");
        public static Cylinder CYL_bullet_feed_plate = new Cylinder(2, GO_bullet_feed_plate_out, OUT_CYL_bullet_feed_plate, CKB_CYL_bullet_feed_plate, GO_bullet_feed_plate_in);

        public static GPIO GO_CYL_bullet_move_plate_up = new GPIO(4, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "载盘移位升降气缸");
        public static GPIO CKB_CYL_bullet_move_plate_up = new GPIO(10, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "载盘移位升降气缸原位");
        public static GPIO OUT_CYL_bullet_move_plate_up = new GPIO(11, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "载盘移位升降气缸动位");
        public static Cylinder CYL_bullet_move_plate_up = new Cylinder(GO_CYL_bullet_move_plate_up, OUT_CYL_bullet_move_plate_up, CKB_CYL_bullet_move_plate_up);
        //上料气缸
        public static GPIO GO_CYL_feed_left_up = new GPIO(3, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料左升降气缸");
        public static GPIO CKB_CYL_feed_left_up = new GPIO(5, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料左升降气缸原位");
        public static GPIO OUT_CYL_feed_left_up = new GPIO(6, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料左升降气缸动位");
        public static Cylinder CYL_feed_left_up = new Cylinder(GO_CYL_feed_left_up, OUT_CYL_feed_left_up, CKB_CYL_feed_left_up);

        public static GPIO GO_CYL_feed_right_up = new GPIO(5, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料右升降气缸");
        public static GPIO CKB_CYL_feed_right_up = new GPIO(9, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料右升降气缸原位");
        public static GPIO OUT_CYL_feed_right_up = new GPIO(10, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料右升降气缸动位");
        public static Cylinder CYL_feed_right_up = new Cylinder(GO_CYL_feed_right_up, OUT_CYL_feed_right_up, CKB_CYL_feed_right_up);

        public static GPIO GO_feed_left_clip_tight = new GPIO(12, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料左夹紧气缸（夹紧）");
        public static GPIO GO_feed_left_clip_loosen = new GPIO(4, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料左夹紧气缸（松开）");
        public static GPIO CKB_CYL_feed_left_clip = new GPIO(7, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料左夹紧气缸原位");
        public static GPIO OUT_CYL_feed_left_clip = new GPIO(8, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料左夹紧气缸动位");
        public static Cylinder CYL_feed_left_clip = new Cylinder(2, GO_feed_left_clip_tight, OUT_CYL_feed_left_clip, CKB_CYL_feed_left_clip, GO_feed_left_clip_loosen);

        public static GPIO GO_feed_right_clip_tight = new GPIO(13, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料右夹紧气缸（夹紧）");
        public static GPIO GO_feed_right_clip_loosen = new GPIO(6, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料右夹紧气缸（松开）");
        public static GPIO CKB_CYL_feed_right_clip = new GPIO(11, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料右夹紧气缸原位");
        public static GPIO OUT_CYL_feed_right_clip = new GPIO(12, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料右夹紧气缸动位");
        public static Cylinder CYL_feed_right_clip = new Cylinder(2, GO_feed_right_clip_tight, OUT_CYL_feed_right_clip, CKB_CYL_feed_right_clip, GO_feed_right_clip_loosen);

        public static GPIO OUT_VACUM_feed_hand_L = new GPIO(0, CARD_GD_EX_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "左夹爪真空0");
        public static GPIO OUT_VACUM_feed_hand_R = new GPIO(1, CARD_GD_EX_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "右夹爪真空1");
        public static GPIO OUT_RED_light = new GPIO(2, CARD_GD_EX_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "报警灯鸣1");
        public static GPIO OUT_Bee = new GPIO(3, CARD_GD_EX_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "报警灯鸣2");
        public static GPIO VACUM_feed_hand_L_check = new GPIO(6, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "左夹爪真空检测");
        public static GPIO VACUM_feed_hand_R_check = new GPIO(7, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "右夹爪真空检测");
        public static Cylinder VACUM_feed_hand_L = new Cylinder(OUT_VACUM_feed_hand_L, VACUM_feed_hand_L_check);
        public static Cylinder VACUM_feed_hand_R = new Cylinder(OUT_VACUM_feed_hand_R, VACUM_feed_hand_R_check);

        public static List<Cylinder> List_cyl_feed = new List<Cylinder> { CYL_feed_left_up, CYL_feed_right_up, CYL_feed_left_clip, CYL_feed_right_clip };
        public static List<Cylinder> List_vacu_feed = new List<Cylinder> { VACUM_feed_hand_L, VACUM_feed_hand_R };
        //取料气缸真空

        public static GPIO GO_CYL_get_up = new GPIO(2, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "取料旋转升降气缸");
        public static GPIO CKB_CYL_get_up = new GPIO(2, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "取料升降气缸原位22");
        public static GPIO OUT_CYL_get_up = new GPIO(3, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "取料升降气缸动位23");
        public static Cylinder CYL_get_up = new Cylinder(GO_CYL_get_up, OUT_CYL_get_up, CKB_CYL_get_up);

        public static GPIO OUT_VACUM_get_mouth = new GPIO(1, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "取料旋转真空");
        public static GPIO VACUM_get_mouth_check = new GPIO(4, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "取料旋转真空检测");
        public static Cylinder VACUM_get_mouth = new Cylinder(OUT_VACUM_get_mouth, VACUM_get_mouth_check);

        public static List<Cylinder> List_cyl_get = new List<Cylinder> { CYL_get_up };
        public static List<Cylinder> List_vacu_get = new List<Cylinder> { VACUM_get_mouth };
        //收料气缸真空
        public static GPIO GO_CYL_back_up = new GPIO(8, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "收料旋转升降气缸");
        public static GPIO CKB_CYL_back_up = new GPIO(0, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "收料升降气缸原位");
        public static GPIO OUT_CYL_back_up = new GPIO(1, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "收料升降气缸动位");
        public static Cylinder CYL_back_up = new Cylinder(GO_CYL_back_up, OUT_CYL_back_up, CKB_CYL_back_up);

        public static GPIO OUT_VACUM_back_mouth = new GPIO(7, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "收料旋转真空");
        public static GPIO VACUM_back_mouth_check = new GPIO(4, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "收料旋转真空检测");
        public static Cylinder VACUM_back_mouth = new Cylinder(OUT_VACUM_back_mouth, VACUM_back_mouth_check);

        public static List<Cylinder> List_cyl_back = new List<Cylinder> { CYL_back_up };
        public static List<Cylinder> List_vacu_back = new List<Cylinder> { VACUM_back_mouth };
        //检测工站
        public static GPIO GO_CYL_check_up = new GPIO(9, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "检测升降气缸");
        public static GPIO CKB_CYL_check_up = new GPIO(0, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "检测升降气缸原位20");
        public static GPIO OUT_CYL_check_up = new GPIO(1, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "检测升降气缸动位21");
        public static Cylinder CYL_check_up = new Cylinder(GO_CYL_check_up, OUT_CYL_check_up, CKB_CYL_check_up);

        public static GPIO GO_CYL_cover_open = new GPIO(1, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "推出开盖气缸");
        public static GPIO CKB_CYL_cover_open = new GPIO(2, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "推出开盖气缸原位");
        public static GPIO OUT_CYL_cover_open = new GPIO(3, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "推出开盖气缸动位");
        public static Cylinder CYL_cover_open = new Cylinder(GO_CYL_cover_open, OUT_CYL_cover_open, CKB_CYL_cover_open);

        public static GPIO GO_CYL_cover_close = new GPIO(0, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "推出合盖气缸");
        public static GPIO CKB_CYL_cover_close = new GPIO(4, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "推出扣盖气缸原位24");
        public static GPIO OUT_CYL_cover_close = new GPIO(5, CARD_GD_EX_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "推出扣盖气缸动位25");
        public static Cylinder CYL_cover_close = new Cylinder(GO_CYL_cover_close, OUT_CYL_cover_close, CKB_CYL_cover_close);





        public static GPIO OUT_VACUM_move_plate = new GPIO(5, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "载盘真空");
        public static GPIO VACUM_move_plate_check = new GPIO(12, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "载盘真空检测");
        public static Cylinder VACUM_move_plate = new Cylinder(OUT_VACUM_move_plate, VACUM_move_plate_check);




        public static GPIO OUT_VACUM_roll_plate1 = new GPIO(6, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘工位1负压");
        public static GPIO OUT_VACUM_roll_plate2 = new GPIO(7, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘工位2负压");
        public static GPIO OUT_VACUM_roll_plate3 = new GPIO(8, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘工位3负压");
        public static GPIO OUT_VACUM_roll_plate4 = new GPIO(9, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘工位4负压");
        public static GPIO OUT_VACUM_roll_plate5 = new GPIO(10, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘工位5负压");
        public static GPIO OUT_VACUM_roll_plate6 = new GPIO(11, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘工位6负压");
        public static GPIO VACUM_roll_plate1_check = new GPIO(2, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘工位1负压检测");
        public static GPIO VACUM_roll_plate2_check = new GPIO(3, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘工位2负压检测");
        public static GPIO VACUM_roll_plate3_check = new GPIO(4, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘工位3负压检测");
        public static GPIO VACUM_roll_plate4_check = new GPIO(5, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘工位4负压检测");
        public static GPIO VACUM_roll_plate5_check = new GPIO(6, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘工位5负压检测");
        public static GPIO VACUM_roll_plate6_check = new GPIO(7, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘工位6负压检测");
        public static Cylinder VACUM_roll_plate1 = new Cylinder(OUT_VACUM_roll_plate1, VACUM_roll_plate1_check);
        public static Cylinder VACUM_roll_plate2 = new Cylinder(OUT_VACUM_roll_plate2, VACUM_roll_plate2_check);
        public static Cylinder VACUM_roll_plate3 = new Cylinder(OUT_VACUM_roll_plate3, VACUM_roll_plate3_check);
        public static Cylinder VACUM_roll_plate4 = new Cylinder(OUT_VACUM_roll_plate4, VACUM_roll_plate4_check);
        public static Cylinder VACUM_roll_plate5 = new Cylinder(OUT_VACUM_roll_plate5, VACUM_roll_plate5_check);
        public static Cylinder VACUM_roll_plate6 = new Cylinder(OUT_VACUM_roll_plate6, VACUM_roll_plate6_check);

        public static List<Cylinder> List_vacu_roll = new List<Cylinder> { VACUM_roll_plate1, VACUM_roll_plate2,
            VACUM_roll_plate3, VACUM_roll_plate4, VACUM_roll_plate5, VACUM_roll_plate6 };
        public static GPIO GPIO_OUT_belet = new GPIO(14, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹皮带");
        public static GPIO GPIO_OUT_brake_back_z = new GPIO(15, CARD_GD_7, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "收料弹夹z刹车");
        public static GPIO GPIO_OUT_gugao_test114 = new GPIO(14, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "固高出口1.14");


        public static GPIO CKPOS_bull_feed_box = new GPIO(0, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料弹夹感应");
        public static GPIO CKPOS_bull_feed_plate = new GPIO(1, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料弹夹料盘感应");
        public static GPIO GPIO_IN_code_closed = new GPIO(13, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "治具扣盖检测");
        public static GPIO CKPOS_MOVE_get_plate = new GPIO(14, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "移动上料位载盘感应");
        public static GPIO GPIO_IN_gugao_test115 = new GPIO(15, CARD_GD_6, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "固高入口1.15");
        public static GPIO GPIO_IN_code_open = new GPIO(5, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "治具开盖检测");
        public static GPIO 
            
            GPIO_IN_emg_key1 = new GPIO(13, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停1按钮");
        public static GPIO CKPOS_MOVE_middle_plate = new GPIO(14, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "移动中间位载盘感应");
        public static GPIO CKPOS_MOVE_back_plate = new GPIO(15, CARD_GD_7, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "移动收料位载盘感应");

        public static GPIO CKPOS_bull_back_plate = new GPIO(0, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "收料弹夹感应");
        public static GPIO CKPOS_bull_back_box = new GPIO(1, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "收料料盒感应");
        public static GPIO CKPOS_bull_belt_come = new GPIO(8, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "皮带弹盒进料感应");
        public static GPIO CKPOS_bull_belt_topos = new GPIO(9, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "皮带弹盒到位感应9");
        public static GPIO CKPOS_bull_back_up_out = new GPIO(10, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "收弹夹顶出感应10");
        public static GPIO CKPOS_roll_plate_topos = new GPIO(11, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘到位感应11");
        public static GPIO CKPOS_roll_plate_home_point = new GPIO(14, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转盘原点位13");

        //按键
        public static GPIO GPIO_IN_key_start = new GPIO(12, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "启动按钮12");
        public static GPIO GPIO_IN_emg_key2 = new GPIO(13, CARD_GD_EX_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停2按钮13");

        public static GPIO GPIO_OUT_roll_plate = new GPIO(10, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转盘启动");
        public static GPIO GPIO_OUT_light = new GPIO(11, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "照明灯");
        public static GPIO GPIO_OUT_brake_feed_z = new GPIO(15, CARD_GD_6, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "弹夹上料Z刹车");

        //上料
        // public static GPIO GPIO_OUT_UL_ZK_FD_TRAY = new GPIO(3, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "供料料盘真空");

        //按键
        // public static GPIO GPIO_OUT_KL_START = new GPIO(6, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "开始按键灯");
        // public static GPIO GPIO_OUT_KL_STOP = new GPIO(7, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "停止按键灯");
        // public static GPIO GPIO_OUT_KL_RESET = new GPIO(8, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "复位按键灯");
        //警报
        //     public static GPIO GPIO_OUT_ALM_RED = new GPIO(9, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "红色塔灯");
        //      public static GPIO GPIO_OUT_ALM_YELLOW = new GPIO(10, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "黄色塔灯");
        //     public static GPIO GPIO_OUT_ALM_GREEN = new GPIO(11, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "绿色塔灯");
        //      public static GPIO GPIO_OUT_ALM_BEEPER = new GPIO(12, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "蜂鸣器");
        //相机
        //       public static GPIO GPIO_OUT_UL_CAM_FR = new GPIO(13, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "外相机触发");
        //       public static GPIO GPIO_OUT_UL_CAM_DW = new GPIO(14, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "下相机触发");
        //      public static GPIO GPIO_OUT_UL_CAM_BK = new GPIO(15, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "内相机触发");
        //料盘真空
        //    public static GPIO GPIO_OUT_DL_ZK_NG_TRAY = new GPIO(0, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "NG料盘真空");
        //     public static GPIO GPIO_OUT_DL_ZK_OK_TRAY = new GPIO(1, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "OK料盘真空");

        //工站压盖

        #endregion
        #region IN
        //急停
        //public static GPIO GPIO_IN_EMG = new GPIO(0, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停键");
        ////上料
        //public static GPIO GPIO_IN_UL_ZK_N1 = new GPIO(1, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料吸头真空感应1");
        //public static GPIO GPIO_IN_UL_ZK_N2 = new GPIO(2, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料吸头真空感应2");
        //public static GPIO GPIO_IN_UL_INP_FD_TRAYBOX = new GPIO(3, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料仓在位感应");
        //public static GPIO GPIO_IN_UL_RDY_FD_TRAY = new GPIO(4, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料仓有料感应");
        //public static GPIO GPIO_IN_UL_ZK_FD_TRAY = new GPIO(5, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料盘真空感应");
        ////按键
        //public static GPIO GPIO_IN_KEY_START = new GPIO(6, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "开始键");
        //public static GPIO GPIO_IN_KEY_STOP = new GPIO(7, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "停止键");
        //public static GPIO GPIO_IN_KEY_RESET = new GPIO(8, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "复位键");
        ////安全门
        //public static GPIO GPIO_IN_FR_DOOR = new GPIO(9, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "前门光栅");
        //public static GPIO GPIO_IN_BK_DOOR = new GPIO(10, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "后门感应");

        //下料
        //夹爪夹位感应
        //public static GPIO GPIO_IN_DL_HD_HD1 = new GPIO(0, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应1");
        //public static GPIO GPIO_IN_DL_HD_HD2 = new GPIO(1, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应2");
        //public static GPIO GPIO_IN_DL_HD_HD3 = new GPIO(2, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应3");
        //public static GPIO GPIO_IN_DL_HD_HD4 = new GPIO(3, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应4");
        //public static GPIO GPIO_IN_DL_HD_HD5 = new GPIO(4, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应5");
        //public static GPIO GPIO_IN_DL_HD_HD6 = new GPIO(5, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应6");
        //public static GPIO GPIO_IN_DL_HD_HD7 = new GPIO(6, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应7");
        //public static GPIO GPIO_IN_DL_HD_HD8 = new GPIO(7, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应8");
        //public static GPIO GPIO_IN_DL_HD_HD9 = new GPIO(8, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应9");
        //public static GPIO GPIO_IN_DL_HD_HD10 = new GPIO(9, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应10");
        //public static GPIO GPIO_IN_DL_HD_HD11 = new GPIO(10, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应11");
        //public static GPIO GPIO_IN_DL_HD_HD12 = new GPIO(11, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应12");
        //public static GPIO GPIO_IN_DL_HD_HD13 = new GPIO(12, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应13");
        //public static GPIO GPIO_IN_DL_HD_HD14 = new GPIO(13, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应14");
        //public static GPIO GPIO_IN_DL_HD_HD15 = new GPIO(14, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应15");
        //public static GPIO GPIO_IN_DL_HD_HD16 = new GPIO(15, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪夹位感应16");
        ////夹爪下位感应
        //public static GPIO GPIO_IN_DL_DW_HD1 = new GPIO(16, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应1");
        //public static GPIO GPIO_IN_DL_DW_HD2 = new GPIO(17, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应2");
        //public static GPIO GPIO_IN_DL_DW_HD3 = new GPIO(18, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应3");
        //public static GPIO GPIO_IN_DL_DW_HD4 = new GPIO(19, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应4");
        //public static GPIO GPIO_IN_DL_DW_HD5 = new GPIO(20, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应5");
        //public static GPIO GPIO_IN_DL_DW_HD6 = new GPIO(21, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应6");
        //public static GPIO GPIO_IN_DL_DW_HD7 = new GPIO(22, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应7");
        //public static GPIO GPIO_IN_DL_DW_HD8 = new GPIO(23, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应8");
        //public static GPIO GPIO_IN_DL_DW_HD9 = new GPIO(24, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应9");
        //public static GPIO GPIO_IN_DL_DW_HD10 = new GPIO(25, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应10");
        //public static GPIO GPIO_IN_DL_DW_HD11 = new GPIO(26, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应11");
        //public static GPIO GPIO_IN_DL_DW_HD12 = new GPIO(27, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应12");
        //public static GPIO GPIO_IN_DL_DW_HD13 = new GPIO(28, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应13");
        //public static GPIO GPIO_IN_DL_DW_HD14 = new GPIO(29, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应14");
        //public static GPIO GPIO_IN_DL_DW_HD15 = new GPIO(30, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应15");
        //public static GPIO GPIO_IN_DL_DW_HD16 = new GPIO(31, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应16");
        ////料夹在位
        //public static GPIO GPIO_IN_DL_INP_NG_TRAYBOX = new GPIO(18, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料仓在位感应");
        //public static GPIO GPIO_IN_DL_INP_OK_TRAYBOX = new GPIO(19, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料仓在位感应");
        ////料夹有料
        //public static GPIO GPIO_IN_DL_RDY_NG_TRAY = new GPIO(20, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料仓有料感应");
        //public static GPIO GPIO_IN_DL_RDY_OK_TRAY = new GPIO(21, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料仓有料感应");
        ////料盘真空
        //public static GPIO GPIO_IN_DL_ZK_NG_TRAY = new GPIO(22, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料盘真空感应");
        //public static GPIO GPIO_IN_DL_ZK_OK_TRAY = new GPIO(23, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料盘真空感应");

        //工站压盖
        #endregion
        #region 气缸定义
        //料盘
        //  public static Cylinder CLD_UL_ZK_TRAY_FD = new Cylinder(GPIO_OUT_UL_ZK_FD_TRAY, GPIO_IN_UL_ZK_FD_TRAY);
        //夹爪     
        //  public static Cylinder CLD_DL_ZK_TRAY_NG = new Cylinder(GPIO_OUT_DL_ZK_NG_TRAY, GPIO_IN_DL_ZK_NG_TRAY);
        //   public static Cylinder CLD_DL_ZK_TRAY_OK = new Cylinder(GPIO_OUT_DL_ZK_OK_TRAY, GPIO_IN_DL_ZK_OK_TRAY);

        #endregion

        #endregion
        #region 轴定义
        //固高工位1取料
        public static AXIS AXIS_GET_X = new AXIS(1, CARD_GD_6, "取料X", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_GET_Y = new AXIS(0, CARD_GD_6, "取料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_GET_Z = new AXIS(2, CARD_GD_6, "取料Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_GET_A = new AXIS(3, CARD_GD_6, "取料转角A", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //固高工位1收料
        public static AXIS AXIS_BACK_X = new AXIS(1, CARD_GD_7, "收料X", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BACK_Y = new AXIS(0, CARD_GD_7, "收料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BACK_Z = new AXIS(2, CARD_GD_7, "收料Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BACK_A = new AXIS(3, CARD_GD_7, "收料转角A", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //固高工位1收料
        public static AXIS AXIS_FEED_X = new AXIS(5, CARD_GD_6, "上料X", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_FEED_Y = new AXIS(4, CARD_GD_6, "上料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_FEED_Z = new AXIS(6, CARD_GD_6, "上料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_FEED_A = new AXIS(7, CARD_GD_6, "上料转角A", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //固高工位1收料
        public static AXIS AXIS_bullet_back = new AXIS(4, CARD_GD_7, "弹夹收料", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_bullet_feed = new AXIS(6, CARD_GD_7, "弹夹上料", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_bullet_move = new AXIS(5, CARD_GD_7, "载盘移动", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //工位1
        //OTP光箱

        ////上料
        //public static AXIS AXIS_UL_X = new AXIS(0, CARD_DMC3800_5, "上料X", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_UL_Y = new AXIS(1, CARD_DMC3800_5, "上料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_UL_Z = new AXIS(2, CARD_DMC3800_5, "上料Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_UL_U1 = new AXIS(3, CARD_DMC3800_5, "上料U1", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_UL_U2 = new AXIS(4, CARD_DMC3800_5, "上料U2", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_UL_FD_X = new AXIS(5, CARD_DMC3800_5, "供料X", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_UL_FD_Z = new AXIS(6, CARD_DMC3800_5, "供料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);

        ////下料
        //public static AXIS AXIS_DL_Y = new AXIS(0, CARD_ECI2600_3, "下料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_DL_Z = new AXIS(1, CARD_ECI2600_3, "下料Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_DL_OK_X = new AXIS(2, CARD_ECI2600_3, "OK料X", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_DL_OK_Z = new AXIS(3, CARD_ECI2600_3, "OK料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_DL_NG_X = new AXIS(4, CARD_ECI2600_3, "NG料X", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_DL_NG_Z = new AXIS(5, CARD_ECI2600_3, "NG料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);

        //左光箱
        //public static AXIS AXIS_BOX_L_Y_FAF = new AXIS(0, CARD_ECI2400_1, "左光箱远焦Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_BOX_L_Y_NAF = new AXIS(1, CARD_ECI2400_1, "左光箱近焦Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_BOX_L_Z_NAF = new AXIS(2, CARD_ECI2400_1, "左光箱近焦Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_BOX_L_Z_DUST = new AXIS(3, CARD_ECI2400_1, "左光箱污坏点Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);

        //右光箱
        //public static AXIS AXIS_BOX_R_Y_FAF = new AXIS(0, CARD_ECI2400_2, "右光箱远焦Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_BOX_R_Y_NAF = new AXIS(1, CARD_ECI2400_2, "右光箱近焦Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_BOX_R_Z_NAF = new AXIS(2, CARD_ECI2400_2, "右光箱近焦Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        //public static AXIS AXIS_BOX_R_Z_DUST = new AXIS(3, CARD_ECI2400_2, "右光箱污坏点Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);

        //工站
        public static List<AXIS> AxList_bullet = new List<AXIS> { AXIS_bullet_back };

        public static List<AXIS> AxList_ALL = new List<AXIS> { AXIS_BACK_X, AXIS_BACK_Y, AXIS_BACK_Z,AXIS_BACK_A, AXIS_FEED_X, AXIS_FEED_Y,AXIS_FEED_A, AXIS_FEED_Z,
            AXIS_GET_X , AXIS_GET_Y, AXIS_GET_Z,AXIS_GET_A ,AXIS_bullet_back,AXIS_bullet_feed,AXIS_bullet_move};
        public static List<AXIS> AxList_WS_BACK = new List<AXIS> { AXIS_BACK_X, AXIS_BACK_Y, AXIS_BACK_Z, AXIS_BACK_A };
        public static List<AXIS> AxList_WS_FEED = new List<AXIS> { AXIS_FEED_X, AXIS_FEED_Y, AXIS_FEED_Z, AXIS_FEED_A };
        public static List<AXIS> AxList_WS_GET = new List<AXIS> { AXIS_GET_X, AXIS_GET_Y, AXIS_GET_Z, AXIS_GET_A };
        public static List<AXIS> AxList_WS_GET_XY = new List<AXIS> { AXIS_GET_X, AXIS_GET_Y, null, null };
        //光箱

        // public static List<AXIS> AxList_BOX_LEFT = new List<AXIS> { AXIS_BOX_L_Y_FAF, AXIS_BOX_L_Y_NAF, AXIS_BOX_L_Z_NAF, AXIS_BOX_L_Z_DUST };
        // public static List<AXIS> AxList_BOX_RIGHT = new List<AXIS> { AXIS_BOX_R_Y_FAF, AXIS_BOX_R_Y_NAF, AXIS_BOX_R_Z_NAF, AXIS_BOX_R_Z_DUST };

        ////上料
        //public static List<AXIS> AxList_UL = new List<AXIS> { AXIS_UL_X, AXIS_UL_Y, AXIS_UL_Z, AXIS_UL_U1, AXIS_UL_U2, AXIS_UL_FD_X, AXIS_UL_FD_Z };
        ////下料
        //public static List<AXIS> AxList_DL = new List<AXIS> { AXIS_DL_Y, AXIS_DL_Z, AXIS_DL_OK_X, AXIS_DL_OK_Z, AXIS_DL_NG_X, AXIS_DL_NG_Z };

        #endregion
        #region 位置

        public static POS pos_get_plate_star = new POS(AXIS_GET_X, AXIS_GET_Y, null, null, "取料盘起点", 1, (ushort)WS_ID.get);
        public static POS pos_get_plate_row = new POS(AXIS_GET_X, AXIS_GET_Y, null, null, "取料盘行点", 2, (ushort)WS_ID.get);
        public static POS pos_get_plate_line = new POS(AXIS_GET_X, AXIS_GET_Y, null, null, "取料盘列点", 3, (ushort)WS_ID.get);      
        public static List<POS> pos_tray_get = new List<POS> { pos_get_plate_star, pos_get_plate_row, pos_get_plate_line };
        public static POS pos_get_photo_L = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "取料左拍照位", 3, (ushort)WS_ID.get);
        public static POS pos_get_photo_R = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "取料右拍照位", 8, (ushort)WS_ID.get);
        public static POS pos_get_put_L = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "左放料位", 4, (ushort)WS_ID.get);
        public static POS pos_get_to_put_L = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "缓左放料位", 5, (ushort)WS_ID.get);
        public static POS pos_get_to_put_R = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "缓右放料位", 6, (ushort)WS_ID.get);
        public static POS pos_get_put_R = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "右放料位", 8, (ushort)WS_ID.get);
        public static POS pos_get_safe = new POS(AXIS_GET_X, AXIS_GET_Y, null, AXIS_GET_A, "取料安全位位", 7, (ushort)WS_ID.get);
        public static POS pos_get_z_dwn = new POS(null, null, AXIS_GET_Z, null, "取料z下降位", 8, (ushort)WS_ID.get);
        public static POS pos_get_z_up = new POS(null, null, AXIS_GET_Z, null, "取料z抬升位", 9, (ushort)WS_ID.get);

        public static List<POS> pos_list_get = new List<POS> {
                pos_get_photo_R, pos_get_photo_L, pos_get_put_L, pos_get_to_put_L, pos_get_to_put_R,pos_get_put_R,
                   pos_get_safe,pos_get_z_dwn, pos_get_z_up };
        public static POS pos_back_plate_star = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "收料ok料盘起点", 0, (ushort)WS_ID.back);
        public static POS pos_back_plate_row = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "收料ok料盘行终点", 1, (ushort)WS_ID.back);
        public static POS pos_back_plate_line = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "收料ok料盘列终点", 2, (ushort)WS_ID.back);
        public static POS pos_back_NG_star = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "开图NG料盘起点", 0, (ushort)WS_ID.back);
        public static POS pos_back_NG_row = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "开图NG料盘行终点", 1, (ushort)WS_ID.back);
        public static POS pos_back_NG_line = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "开图NG料盘列终点", 2, (ushort)WS_ID.back);
        public static List<POS> pos_tray_bk_ng = new List<POS> { pos_back_NG_star, pos_back_NG_row, pos_back_NG_line };
        public static List<POS> pos_tray_bk_ok = new List<POS> { pos_back_plate_star, pos_back_plate_row, pos_back_plate_line };

        public static POS pos_back_AANG_star = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "AANG料盘起点", 0, (ushort)WS_ID.back);
        public static POS pos_back_AANG_row = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "AANG料盘行终点", 1, (ushort)WS_ID.back);
        public static POS pos_back_AANG_line = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "AANG料盘列终点", 2, (ushort)WS_ID.back);
        public static List<POS> pos_tray_bk_AANG = new List<POS> { pos_back_AANG_star, pos_back_AANG_row, pos_back_AANG_line };

        public static POS pos_back_safe = new POS(AXIS_BACK_X, AXIS_BACK_Y, AXIS_BACK_Z, AXIS_BACK_A, "取料安全位位", 3, (ushort)WS_ID.back);
        public static POS pos_back_L = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, AXIS_BACK_A, "收料左位", 4, (ushort)WS_ID.back);
        public static POS pos_back_R = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, AXIS_BACK_A, "收料右位", 5, (ushort)WS_ID.back);
        public static POS pos_back_z_dwn = new POS(null, null, AXIS_BACK_Z, null, " 收料Z下降位", 6, (ushort)WS_ID.back);
        public static POS pos_back_z_up = new POS(null, null, AXIS_BACK_Z, null, "取料Z上升位", 7, (ushort)WS_ID.back);
        public static POS pos_back_to_L = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, AXIS_BACK_A, "收料缓冲左位", 8, (ushort)WS_ID.back);
        public static POS pos_back_to_R = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, AXIS_BACK_A, "收料缓冲右位", 9, (ushort)WS_ID.back);
        public static POS pos_back_ng_plate_star = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "收料NG料盘起点", 8, (ushort)WS_ID.back);
        public static POS pos_back_ng_plate_row = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "收料NG料盘行终点", 9, (ushort)WS_ID.back);
        public static POS pos_back_ng_plate_line = new POS(AXIS_BACK_X, AXIS_BACK_Y, null, null, "收料NG料盘列终点", 10, (ushort)WS_ID.back);
        public static List<POS> pos_list_back = new List<POS> {pos_back_safe,pos_back_L,pos_back_R,pos_back_z_dwn,pos_back_z_up,pos_back_to_L,pos_back_to_R,
         };

        public static POS pos_feed_left_get = new POS(AXIS_FEED_X, AXIS_FEED_Y, null, null, "上料左爪取料位", 0, (ushort)WS_ID.feed);
        public static POS pos_feed_left_feed = new POS(AXIS_FEED_X, AXIS_FEED_Y, null, null, "上料左爪上料位", 1, (ushort)WS_ID.feed);
        public static POS pos_feed_right_back = new POS(AXIS_FEED_X, AXIS_FEED_Y, null, null, "上料右爪放回位", 2, (ushort)WS_ID.feed);
        public static POS pos_feed_z_dwn = new POS(null, null, AXIS_FEED_Z, null, "上料升高位", 3, (ushort)WS_ID.feed);
        public static POS pos_feed_z_up = new POS(null, null, AXIS_FEED_Z, null, "上料下降位", 4, (ushort)WS_ID.feed);
        public static POS pos_feed_safe = new POS(AXIS_FEED_X, AXIS_FEED_Y, null, AXIS_FEED_A, "上料安全位", 5, (ushort)WS_ID.feed);
        public static POS pos_feed_right_get = new POS(AXIS_FEED_X, AXIS_FEED_Y, AXIS_FEED_Z, AXIS_FEED_A, "上料右爪取料位", 6, (ushort)WS_ID.feed);

        public static POS pos_feed_photo = new POS(AXIS_FEED_X, AXIS_FEED_Y, AXIS_FEED_Z, AXIS_FEED_A, "上料拍照位", 7, (ushort)WS_ID.feed);
        public static List<POS> pos_list_feed = new List<POS> { pos_feed_left_get, pos_feed_left_feed, pos_feed_right_back,
            pos_feed_z_dwn, pos_feed_z_up ,pos_feed_safe,pos_feed_right_get,pos_feed_photo};

        public static POS pos_bullet_back_boxIN = new POS(null, null, AXIS_bullet_back, null, "弹收向下料盒进料位", 0, (ushort)WS_ID.bull_back);
        public static POS pos_bullet_back_boxOUT = new POS(null, null, AXIS_bullet_back, null, "弹收向上顶升出盒位", 1, (ushort)WS_ID.bull_back);
        public static POS pos_bullet_back_check_low = new POS(null, null, AXIS_bullet_back, null, "弹收检测最低点", 2, (ushort)WS_ID.bull_back);
        public static POS pos_bullet_back_check_top = new POS(null, null, AXIS_bullet_back, null, "弹收检测最高点", 3, (ushort)WS_ID.bull_back);
        public static POS pos_bullet_back_safe = new POS(null, null, AXIS_bullet_back, null, "弹收安全位", 4, (ushort)WS_ID.bull_back);
        public static List<POS> pos_list_bull_back = new List<POS> { pos_bullet_back_boxIN, pos_bullet_back_boxOUT, pos_bullet_back_check_low, pos_bullet_back_check_top, pos_bullet_back_safe };

        public static POS pos_bullet_feed_boxOUT = new POS(null, null, AXIS_bullet_feed, null, "上弹出料盒位置", 0, (ushort)WS_ID.bull_feed);
        public static POS pos_bullet_feed_safe = new POS(null, null, AXIS_bullet_feed, null, "上弹安全位", 1, (ushort)WS_ID.bull_feed);
        public static POS pos_bullet_feed_get_box = new POS(null, null, AXIS_bullet_feed, null, "上弹接空料盒位", 2, (ushort)WS_ID.bull_feed);
        public static POS pos_bullet_feed_check_low = new POS(null, null, AXIS_bullet_feed, null, "上弹检测最低点", 3, (ushort)WS_ID.bull_feed);
        public static POS pos_bullet_feed_check_top = new POS(null, null, AXIS_bullet_feed, null, "上弹检测最高点", 4, (ushort)WS_ID.bull_feed);
        public static POS pos_bullet_feed_boxIN = new POS(null, null, AXIS_bullet_feed, null, "上弹检测备料盒位置", 5, (ushort)WS_ID.bull_feed);
        public static List<POS> pos_list_bull_feed = new List<POS> { pos_bullet_feed_boxOUT, pos_bullet_feed_safe, pos_bullet_feed_boxIN, pos_bullet_feed_get_box, pos_bullet_feed_check_low, pos_bullet_feed_check_top };

        public static POS pos_bullet_move_wait = new POS(AXIS_bullet_move, null, null, null, "移弹缓冲位", 0, (ushort)WS_ID.bull_mov);
        public static POS pos_bullet_move_safe = new POS(AXIS_bullet_move, null, null, null, "移弹安全位", 1, (ushort)WS_ID.bull_mov);
        public static POS pos_bullet_move_get_tray = new POS(AXIS_bullet_move, null, null, null, "移弹取盘位", 2, (ushort)WS_ID.bull_mov);
        public static POS pos_bullet_move_put = new POS(AXIS_bullet_move, null, null, null, "移弹放盘位", 3, (ushort)WS_ID.bull_mov);
        public static POS pos_bullet_move_center = new POS(AXIS_bullet_move, null, null, null, "移弹中间位", 4, (ushort)WS_ID.bull_mov);
        public static List<POS> pos_list_bull_move = new List<POS> { pos_bullet_move_wait, pos_bullet_move_safe, pos_bullet_move_get_tray, pos_bullet_move_put, pos_bullet_move_center };

        #endregion
        #region 板卡初始化
        /// <summary>
        /// 轴卡初始化
        /// </summary>
        public static bool bCardInit
        {
            get
            {
                foreach (CARD card in CardList)
                {
                    if (!card.isReady) return false;
                }
                return true;
            }
        }
        public static EM_RES card_Init(String filename)
        {
            if (bCardInit) return EM_RES.OK;
            EM_RES res = EM_RES.OK;
            bool bok = true;
            foreach (CARD card in CardList)
            {
                if (!card.isReady)
                {
                    res = card.Init();
                    if (res != EM_RES.OK)
                        bok = false;
                    Application.DoEvents();
                    Thread.Sleep(10);
                }
            }
            if (bok)
                return EM_RES.OK;
            else
                return EM_RES.ERR;

        }
        #endregion
        #region 检查/重连
        /// <summary>
        /// 检查所有板卡是否在线，否在重连
        /// </summary>
        /// <returns></returns>
        public static EM_RES ChkAndReConnect(string filename = "")
        {
            EM_RES res = EM_RES.OK;
            bool bok = true;
            foreach (CARD card in CardList)
            {
                res = card.ChkOnline(filename);
                if (res != EM_RES.OK) bok = false;
            }
            if (bok) return EM_RES.OK;
            else return EM_RES.ERR;
        }
        #endregion
        #region 关闭控制卡
        public static EM_RES Close()
        {
            EM_RES res = EM_RES.OK;
            bool bok = true;
            foreach (CARD card in CardList)
            {
                res = card.Close();
                if (res != EM_RES.OK) bok = false;
            }
            if (bok) return EM_RES.OK;
            else return EM_RES.ERR;
        }
        #endregion
        #region 设置轴到工作速度
        public static EM_RES SetAllAxToWorkSpd(double persent = 1.0)
        {
            EM_RES ret = EM_RES.OK;
            foreach (CARD card in CardList)
            {
                if (card.AxList == null) continue;
                foreach (AXIS ax in card.AxList)
                {
                    ret = ax.SetToWorkSpd(persent);
                    if (ret != EM_RES.OK) return ret;
                }
            }
            return ret;
        }
        public static EM_RES SetAllAxToManualSpd()
        {
            EM_RES ret = EM_RES.OK;
            foreach (CARD card in CardList)
            {
                if (card.AxList == null) continue;
                foreach (AXIS ax in card.AxList)
                {
                    ret = ax.SetToManualHighSpd();
                    if (ret != EM_RES.OK) return ret;
                }
            }
            return ret;
        }
        //public static EM_RES SetMainAxToCaliSpd()
        //{
        //    EM_RES ret = EM_RES.OK;
        //    foreach (AXIS ax in AxisListMain)
        //    {
        //        ret = ax.SetSpeed(ax.spd_start, ax.spd_work / 3, ax.spd_stop, 1, 1);
        //        if (ret != EM_RES.OK) return ret;
        //    }
        //    return ret;
        //}
        #endregion
        #region 所有轴停止
        public static void AllAxStop(List<AXIS> list_ax = null)
        {
            if (list_ax == null)
            {
                foreach (CARD card in CardList)
                {
                    if (card.AxList == null) continue;
                    card.AllCardStop();
                }
            }
            else
            {
                foreach (AXIS ax in list_ax)
                {
                    ax.Stop();
                }
                foreach (CARD card in CardList)
                {
                    if (card.AxList == null) continue;
                    card.AllCardStop();
                }
            }


        }

        #endregion    
        public static EM_RES PosInit()
        {
            try
            {
                bool ret = true;
                List<List<POS>> AllPos = new List<List<POS>> { pos_list_back, pos_list_bull_back, pos_list_bull_move, pos_list_feed, pos_list_get };
                foreach (List<POS> mlist in AllPos)
                {
                    foreach (POS e in mlist)
                    {
                        ret = e.ReadUpdatePos();
                        if (!ret)
                            return EM_RES.ERR;

                    }


                }
                return EM_RES.OK;

            }
            catch
            {
                VAR.ErrMsg("位置加载异常");
                return EM_RES.ERR;
            }

        }
      
    #region 轴复位
        public static EM_RES AxisHome(ref bool bquit, params AXIS[] axs)
        {
            //home task start
            foreach (AXIS ax in axs)
            {
                if (ax != null) ax.HomeTask(10000);
            }

            //wait end
            bool bok = true;
            while (true)
            {
                bok = true;
                foreach (AXIS ax in axs)
                {
                    if (ax != null && !ax.HomeTaskisEnd)
                        bok = false;
                }
                if (bok)
                    break;
                Application.DoEvents();
                Thread.Sleep(10);
                //quit
                if (bquit)
                {
                    foreach (AXIS ax in axs)
                    {
                        if (ax != null && !ax.HomeTaskisEnd)
                            ax.Stop();
                    }
                    return EM_RES.ERR;
                }
            }

            //check result
            bok = true;
            foreach (AXIS ax in axs)
            {
                if (ax != null && ax.home_status != AXIS.HOME_STA.OK)
                    bok = false;
            }
            if (bok == false)
            {
                foreach (AXIS ax in axs)
                {
                    if (ax != null && !ax.HomeTaskisEnd)
                        ax.Stop();
                }
                return EM_RES.ERR;
            }

            return EM_RES.OK;
        }
        public static void AxisHomeQuit(params AXIS[] axs)
        {
            foreach (AXIS ax in axs)
            {
                if (ax != null)
                {
                    ax.bhomequit = true;
                    ax.Stop();
                }
            }
        }
        public static void AxisHomeQuit(List<AXIS> axs)
        {
            foreach (AXIS ax in axs)
            {
                if (ax != null)
                {
                    ax.bhomequit = true;
                    ax.Stop();
                }
            }
        }
        #endregion    
    }
    #endregion
    #region 料仓
    public class TrayBox
    {
        //料盘       
        public List<EM_STA> list_sta = new List<EM_STA>();
        public Product.Tray tray_cur = null;
        int m_tray_cnt; //格数、盘数
        public POS pos_low;
        public POS pos_high;
        public POS pos_out;
        public POS pos_in;

        public int tray_cnt
        {
            get { return VAR.gsys_set.box_tray_cnt; }
        }
        //当前格
        int m_tray_idx;
        public int tray_idx
        {
            get
            {

                for (int n = 0; n < tray_cnt; n++)
                {
                    //search down
                    if ((n < tray_cnt) && list_sta[n] != EM_STA.EMPTY)

                        return n;
                }
                return 99;
            }
            set
            {
                if (value < 0 || value > tray_cnt) m_tray_idx = 0;
                else m_tray_idx = value;
            }
        }
        public int out_id
        {
            get
            {

                for (int n = 0; n < tray_cnt; n++)
                {
                    //search down
                    if ((n < tray_cnt) && list_sta[n] != EM_STA.EMPTY)

                        return n;
                }
                return 0;
            }
            set
            {
                if (value < 0 || value > tray_cnt) value = 0;

            }
        }
        public int in_id
        {
            get
            {

                for (int n = 0; n < tray_cnt; n++)
                {
                    //search down
                    if ((n < tray_cnt) && list_sta[n] == EM_STA.EMPTY)

                        return n;
                }
                return 0;
            }
            set
            {
                if (value < 0 || value > tray_cnt) value = 0;

            }
        }
        string disc;
        //料仓参数

        //第一格位置
        public double pos_tray_top;
        //最后一格
        public double pos_tray_low;
        //当前格

        public bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
                return true;
            }
        }
        //每格高度
        public double tray_height
        {
            get { return Math.Abs((pos_tray_top - pos_tray_low) / (tray_cnt - 1)); }
        }
        //取放料脱离高度
        public double tray_feed_ofs_h;
        //安区X位置
        public double fd_safe_x;
        public EM_DIR direction;
        //硬件
        public AXIS ax_z;
        public AXIS ax_x;
        //料夹感应
        public GPIO in_box_sen;
        //料盘拉出到位感应
        public GPIO in_tray_sen;
        //料盘真空吸组件
        public Cylinder vacu_mov;
        //料盘进出气缸
        public Cylinder tray_out_in_cyl;
        //料盘运料抬升气缸
        public Cylinder tray_mov_up_cyl;
        //料盘真空吸out
        public GPIO out_tray_zk;
        //料盘真空吸感应
        public GPIO in_tray_zk;
        public string warn_msg;

        public int boxID;
        //料仓的盘状态
        public EM_STA status;
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOWN,
            [Description("就绪")]
            STANBY,
            [Description("空仓")]
            EMPTY,
            [Description("满仓")]
            FULL,
            [Description("待测")]
            UNTEST,
            [Description("完成")]
            DONE,
            [Description("复位中")]
            HOME,
            [Description("无料仓")]
            NOBOX,
            [Description("错误")]
            ERR
        }

        public string GetStaStr
        {
            get
            {
                return status.GetDescription();
            }
        }
        public enum EM_DIR
        {
            [Description("只进")]
            ONLY_IN,
            [Description("只出")]
            ONLY_OUT,
            [Description("进/出")]
            IN_OUT,
        }

        //初始化
        public void TrayBox_init(string disc = "料仓", EM_DIR dir = EM_DIR.IN_OUT, int cnt = 10, AXIS ax_x = null, AXIS ax_z = null, GPIO in_box_sen = null, GPIO in_tray_sen = null, GPIO out_tray_zk = null, GPIO in_tray_zk = null)
        {
            this.direction = dir;
            this.disc = disc;
            this.m_tray_cnt = cnt;
            this.ax_x = ax_x;
            this.ax_z = ax_z;
            this.in_box_sen = in_box_sen;
            this.in_tray_sen = in_tray_sen;
            this.out_tray_zk = out_tray_zk;
            this.in_tray_zk = in_tray_zk;
            list_sta.Clear();
            //list_tray.Clear();
            Random rdm = new Random();
            for (int n = 0; n < cnt; n++)
            {
                Thread.Sleep(1);
                list_sta.Add((EM_STA)rdm.Next(0, 5));
                //Product.Tray tray = new Product.Tray(tray_row, tray_col, (Product.EM_CM_RES)rdm.Next(0, 5));
                //list_tray.Add(tray);
            }
        }
        //public TrayBox(string disc = "料仓", AXIS ax_x = null, EM_DIR dir = EM_DIR.IN_OUT, int cnt = 10, AXIS ax_z = null, GPIO in_box_sen = null, GPIO in_tray_sen = null, Cylinder tray_mouth=null)
        //{

        //    TrayBox_init(disc, dir, cnt, ax_x, ax_z, in_box_sen, in_tray_sen, tray_mouth.io_out, tray_mouth.io_sen_on);
        //}
        public TrayBox(string disc = "料仓", EM_DIR dir = EM_DIR.IN_OUT, int cnt = 10, AXIS ax_x = null, AXIS ax_z = null, GPIO in_box_sen = null, GPIO in_tray_sen = null, GPIO out_tray_zk = null, GPIO in_tray_zk = null)
        {
            TrayBox_init(disc, dir, cnt, ax_x, ax_z, in_box_sen, in_tray_sen, out_tray_zk, in_tray_zk);
        }
        public TrayBox(string disc, EM_DIR dir, Cylinder tray_out_in_cyl, int cnt = 10, AXIS ax_x = null, AXIS ax_z = null, GPIO in_box_sen = null, GPIO in_tray_sen = null, Cylinder tray_mov_up_cyl = null)
        {
            TrayBox_init(disc, dir, cnt, ax_x, ax_z, in_box_sen, in_tray_sen);
            this.tray_out_in_cyl = tray_out_in_cyl;
            this.tray_mov_up_cyl = tray_mov_up_cyl;
        }
        public TrayBox(string disc, ref POS pos_low, ref POS pos_high, int boxID = 1, EM_DIR dir = EM_DIR.ONLY_IN, Cylinder tray_out_in_cyl = null, int cnt = 10, AXIS ax_x = null, AXIS ax_z = null,
             POS pos_out = null, POS pos_in = null, GPIO in_box_sen = null, GPIO in_tray_sen = null, Cylinder tray_mov_up_cyl = null)
        {
            TrayBox_init(disc, dir, cnt, ax_x, ax_z, in_box_sen, in_tray_sen);
            this.boxID = boxID;
            this.tray_out_in_cyl = tray_out_in_cyl;
            this.tray_mov_up_cyl = tray_mov_up_cyl;
            if (pos_high != null) pos_high.UpdatePos();
            if (pos_low != null) pos_low.UpdatePos();
            if (pos_out != null) pos_out.UpdatePos();
            if (pos_in != null) pos_in.UpdatePos();
            this.pos_low = pos_low;//-gy-1204-
            this.pos_high = pos_high;
            this.pos_out = pos_out;
            this.pos_in = pos_in;
            pos_tray_top = pos_high.pos_z;
            pos_tray_low = pos_low.pos_z;
            for (int n = 0; n < tray_cnt; n++)
            {

                if (direction == EM_DIR.ONLY_IN)

                    list_sta.Add(EM_STA.EMPTY);
                else
                    list_sta.Add(EM_STA.FULL);
            }

            //if (direction == EM_DIR.ONLY_IN)

            //    tray_cur = list_tray[out_id];
            //else
            //    tray_cur = list_tray[in_id];

        }
        public TrayBox(string disc, int boxID = 1, EM_DIR dir = EM_DIR.ONLY_IN, int cnt = 10)
        {
            this.direction = dir;
            this.disc = disc;
            this.m_tray_cnt = cnt;
            this.boxID = boxID;
        }
        /// <summary>
        /// 加载参数
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// 
        // EM_RES LoadCfg(string filename = "")
        //{
        //    if (filename.Length < 3)
        //        filename = Path.GetFullPath("..") + "\\product\\" + VAR.gsys_set.cur_product_name + "\\TrayBox.inf";
        //    if (!File.Exists(filename)) File.Create(filename);
        //    IniFile inf = new IniFile(filename);
        //    string str_section = "TRAY_BOX" + boxID;
        //  //  tray_cnt = inf.ReadInteger(str_section, "TRAY_CNT", tray_cnt);
        //    return EM_RES.OK;
        //}
        ///// <summary>
        ///// 保存参数
        ///// </summary>
        ///// <param name="filename"></param>
        ///// <returns></returns>
        // EM_RES SaveCfg(string filename = "")
        //{
        //    if (filename.Length < 3)
        //        filename = Path.GetFullPath("..") + "\\product\\" + VAR.gsys_set.cur_product_name + "\\feedcfg.inf";
        //    if (!File.Exists(filename)) return EM_RES.PARA_ERR;

        //    IniFile inf = new IniFile(filename);

        //    string str_section = "TRAY_BOX" + boxID;
        //    inf.WriteInteger(str_section, "TRAY_CNT", tray_cnt);
        //    return EM_RES.OK;
        //}
        public bool isSafe
        {
            get
            {
                if (ax_x == null) return true;
                if (ax_x.status == AXIS.AX_STA.ALM || ax_x.status == AXIS.AX_STA.UNKOWN || ax_x.status == AXIS.AX_STA.HOMEING) return false;
                return true;
            }
        }

        public EM_RES NewBox(Product.EM_CM_RES cm_res)
        {
            list_sta.Clear();
            for (int n = 0; n < tray_cnt; n++)
            {
                list_sta.Add(EM_STA.FULL);
            }
            return EM_RES.OK;
        }

        /// <summary>
        /// 移动料仓到指定位置编号
        /// </summary>
        /// <param name="bquit"></param>
        /// <param name="idx">指定位置编号</param>
        /// <param name="btrayin">True：后续动作为TRAY盘入仓，定位自动降低ofs_z。</param>
        /// <returns></returns>
        public EM_RES BoxMoveToPosIdx(ref bool bquit, int idx = -1)
        {
            //bquit
            if (bquit) return EM_RES.QUIT;

            //current idx
            if (idx < 0) idx = tray_idx;
            //check idx
            if (idx >= tray_cnt)
                return EM_RES.PARA_OUTOFRANG;


            //calc pos
            //double pos = pos_tray_low + idx * tray_height - (btrayin ? tray_feed_ofs_h : 0);

            double pos = pos_tray_low + idx * tray_height;
            //  move
            if (ax_z != null)
            {
                EM_RES res = ax_z.MoveTo(ref bquit, pos, 10000, true);
                if (res != EM_RES.OK)
                    return res;

            }
            else
                return EM_RES.ERR;
            return EM_RES.OK;
        }
        public EM_RES SetSta(EM_STA sta)
        {
            for (int n = 0; n < list_sta.Count; n++)
            {
                list_sta[n] = sta;
            }
            return EM_RES.OK;
        }
        public EM_RES TrayOut(ref bool bquit, int idx = -2, EM_STA sta = EM_STA.UNTEST)
        {
            EM_RES ret;
            if (bquit) return EM_RES.QUIT;
            //ret = TrayReadyOUT(ref bquit, idx, sta);
            //if (ret != EM_RES.OK) goto END;
            ret = ax_z.MoveTo(ref bquit, ax_z.fcmd_pos - 4, 10000, true);
            if (ret != EM_RES.OK) goto END;
            //cyl_work
            if (tray_out_in_cyl == null)
            { ret = EM_RES.ERR; goto END; }

            ret = tray_out_in_cyl.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto END;
            Thread.Sleep(800);
            Application.DoEvents();
            //  下降8再拉出
            WsBuFD.bsafe = true;
            ret = ax_z.MoveTo(ref bquit, ax_z.fcmd_pos - 4, 10000, true);
            WsBuFD.bsafe = false;

            if (ret != EM_RES.OK)
                return ret;
            Thread.Sleep(200);
            Application.DoEvents();
            Thread.Sleep(200);
            Application.DoEvents();
            ret = tray_out_in_cyl.SetOn(ref bquit, 10000);
            if (ret != EM_RES.OK)
                return ret;
            Thread.Sleep(800);
            Application.DoEvents();

            Application.DoEvents();
            //tray_cur = list_tray[out_id];
            list_sta[out_id] = EM_STA.EMPTY;
        END:
            tray_out_in_cyl.SetOn(ref bquit, 3000);
            return ret;

        }
        public EM_RES TrayReadyOUT(ref bool bquit, int idx = -1, EM_STA sta = EM_STA.UNTEST)
        {
            try
            {
                EM_RES res = EM_RES.OK;
                if (bquit) return EM_RES.QUIT;
                //check idx

                if (idx < -1 || idx >= tray_cnt) return EM_RES.QUIT;
                if (idx == -1)
                    idx = out_id;
                res = BoxMoveToPosIdx(ref bquit, idx);
                if (res != EM_RES.OK) return res;
                if (in_tray_sen.isOFF)
                {
                    list_sta[idx] = EM_STA.EMPTY;//标记当前位置空盘
                }
                else
                    return EM_RES.OK;
                //search


                if (list_sta[idx] == EM_STA.EMPTY)
                {

                    for (int n = 1; n < tray_cnt; n++)
                    {
                        //search down
                        if ((idx + n < tray_cnt) && list_sta[idx + n] != EM_STA.EMPTY)
                        {
                            out_id = idx + n;
                            res = BoxMoveToPosIdx(ref bquit, tray_idx);
                            if (res != EM_RES.OK)
                                return res;
                            if (in_tray_sen.isOFF)
                            {
                                list_sta[idx + n] = EM_STA.EMPTY;
                            }
                            else
                                break;
                        }

                        //search up
                        if ((idx - n >= 0) && (idx - n) < tray_cnt && list_sta[idx - n] != EM_STA.EMPTY)
                        {

                            out_id = idx - n;
                            res = BoxMoveToPosIdx(ref bquit, tray_idx);
                            if (res != EM_RES.OK)
                                return res;
                            if (in_tray_sen.isOFF)
                            {
                                list_sta[idx - n] = EM_STA.EMPTY;
                            }
                            else
                                break;
                        }
                    }

                }
                //check_result
                if (!in_tray_sen.isOFF) return EM_RES.OK;
                //检测有料盒子
                res = pos_low.MoveTo(ref bquit, true);
                if (res != EM_RES.OK) return res;
                res = ax_z.MoveTo(ref bquit, ax_z.fcmd_pos - tray_height, 9000);
                if (res != EM_RES.OK) return res;
                if (in_tray_sen.isON) status = EM_STA.EMPTY;
                else status = EM_STA.NOBOX;
                return EM_RES.END;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--找料盘-" + "未知系统异常", disc));
                return EM_RES.ERR;
            }




        }
        public EM_RES TrayReadyIN(ref bool bquit, int idx = -2, EM_STA sta = EM_STA.UNTEST)
        {
            try
            {
                EM_RES res = EM_RES.OK;
                //check box
                if (bquit) return EM_RES.QUIT;
                if (list_sta[idx] == EM_STA.FULL) return EM_RES.QUIT;
                if (idx < 0 || idx >= list_sta.Count)
                    idx = in_id;
                res = BoxMoveToPosIdx(ref bquit, idx);
                if (res != EM_RES.OK) return res;
                if (in_tray_sen.isON)
                {
                    list_sta[idx] = EM_STA.FULL;//set statue
                }
                else
                    return EM_RES.OK;
                //check idx

                //search
                if (list_sta[idx] == EM_STA.FULL)
                {

                    for (int n = 0; n < list_sta.Count; n++)
                    {
                        //search down
                        if ((idx + n < list_sta.Count) && list_sta[idx + n] != EM_STA.FULL)
                        {
                            idx = idx + n;
                            res = BoxMoveToPosIdx(ref bquit, idx);
                            if (res != EM_RES.OK) return res;
                            if (in_tray_sen.isON)
                            {
                                list_sta[idx] = EM_STA.FULL;
                            }
                            else
                                break;
                        }

                        //search up
                        if ((idx - n >= 0) && list_sta[idx - n] != EM_STA.FULL)
                        {
                            idx = idx - n;
                            res = BoxMoveToPosIdx(ref bquit, idx);
                            if (res != EM_RES.OK) return res;
                            if (in_tray_sen.isON)
                            {
                                list_sta[idx] = EM_STA.FULL;
                            }
                            else
                                break;
                        }
                    }
                }
                //check_result
                if (in_tray_sen.isON)
                {
                    status = EM_STA.FULL;
                    return EM_RES.ERR;
                }
                else
                    return EM_RES.OK;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--找料盘-" + "未知系统异常", disc));
                return EM_RES.ERR;
            }

        }
        public EM_RES TrayIn(ref bool bquit, int idx = -2, EM_STA sta = EM_STA.UNTEST)
        {
            try
            {

                EM_RES ret;
                if (bquit) return EM_RES.QUIT;
                if (isReady) return EM_RES.QUIT;
                ret = TrayReadyIN(ref bquit, idx, sta);
                if (ret != EM_RES.OK) return ret;
                //cyl_work
                if (tray_out_in_cyl != null)
                {
                    ret = tray_out_in_cyl.SetOn(30);
                    if (ret != EM_RES.OK) return ret;
                    ret = tray_out_in_cyl.SetOff(30);
                    if (ret != EM_RES.OK) return ret;

                }
                else
                    return EM_RES.ERR;
                //update status
                list_sta[idx] = EM_STA.FULL;
                tray_cur = null;
                //check status
                //ret = TrayReadyIN(ref bquit, idx, sta);
                //if (ret == EM_RES.OK)
                //{
                //    status = EM_STA.STANBY;
                //    return EM_RES.OK;
                //}
                //else
                //    status = EM_STA.FULL;


                return EM_RES.OK;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--找料盘-" + "未知系统异常", disc));
                return EM_RES.ERR;
            }
        }


        /// <summary>
        /// 抬升到指定位置编号
        /// </summary>
        /// <param name="bquit"></param>
        /// <param name="idx">指定位置编号，-1为当前位置的上一位置</param>
        /// <returns></returns>
        public EM_RES Up(ref bool bquit)
        {
            return BoxMoveToPosIdx(ref bquit, tray_idx++);
        }
        public EM_RES Down(ref bool bquit)
        {
            return BoxMoveToPosIdx(ref bquit, tray_idx--);
        }

    }
    #endregion
    #region 工站
   
    #region 弹夹上下料
    public  class WsBuFD
    {
        public static TrayBox tbox_get = COM.traybox_get;
        #region 执行气缸io轴
        static AXIS ax_Z = COM.traybox_get.ax_z;
        static Cylinder cyl_lock_up = MT.CYL_bullet_up;//上料盒二挂起气缸
        static Cylinder cyl_tray_out = MT.CYL_bullet_feed_plate;//上料拉盘气缸
        static GPIO sen_box_in = MT.CKPOS_bull_feed_box;
        static GPIO sen_tray_in = MT.CKPOS_bull_feed_plate;
        static GPIO sen_box_out = MT.CKPOS_bull_belt_come;
        static GPIO belet_move = MT.GPIO_OUT_belet;
        static GPIO sen_plate_at_get = MT.CKPOS_MOVE_get_plate;
        #endregion
        #region 位置
        static POS PosBoxOUT = MT.pos_bullet_feed_boxOUT;
        static POS PosSafe = MT.pos_bullet_feed_safe;
        static POS PosGetBox = MT.pos_bullet_feed_get_box;
        static POS PosTrayLow = MT.pos_bullet_feed_check_low;
        static POS PosTrayTop = MT.pos_bullet_feed_check_top;
        static POS PosBoxIn = MT.pos_bullet_feed_boxIN;
        static POS PosTrayStar = MT. pos_get_plate_star;
        static POS PosTrayRow = MT. pos_get_plate_row;
        static POS PosTrayLine = MT. pos_get_plate_line;
        #endregion
        public static EM_STA status = EM_STA.UNKNOW;
        public static string disc = "弹夹出料仓工站";
        public static bool bsafe;//取消防护true
        public  static string GetStaString
        {
            get
            {
                return string.Format("{0}+{1}", disc, status.GetDescription());
            }
        }
        /// <summary>
        /// 运行条件
        /// </summary>
        public static bool isReady
        {
            get
            {               
                if (VAR.gsys_set.bquit) return false;            
                if (!(WSGet.TaskRun == null || WSGet.TaskRun.IsCompleted))
                {
                    status = EM_STA.WAIT;
                    return false;
                }
                if (!(WsTrayMove.TaskRun == null || WsTrayMove.TaskRun.IsCompleted))
                {
                    status = EM_STA.WAIT;
                    return false;
                }
                //if (isErr) return false;
                //if (VAR.gsys_set.status != EM_SYS_STA.RUN) return false;
                return true;
            }
        }
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("盘拉出上料")]
            TRAYOUT,
            [Description("准备料盘")]
            TRAYREADY,
            [Description("上料盒")]
            BOXIN,
            [Description("出料盒")]
            BOXOUT,
            [Description("换料盒二")]
            BOXCHG,
            [Description("错误")]
            ERR,
            [Description("安全位")]
            SAFE,
            [Description("等待")]
            STANDBY,
            [Description("等待上料")]
            WAIT


        }
    

        #region 动作函数
        public static EM_RES trayReady(ref bool bquit)
        {
            if (!isReady) return EM_RES.QUIT;
            if (tbox_get == null) return EM_RES.QUIT;
            EM_RES ret = tbox_get.TrayReadyOUT(ref  bquit);
            return ret;
        }
        public static EM_RES trayOut(ref bool bquit)
        {

            if (!isReady) return EM_RES.QUIT;
            if (tbox_get == null) return EM_RES.QUIT;
            if (sen_plate_at_get.AssertON())
            {                 
                return EM_RES.ERR;
            }
            EM_RES ret = EM_RES.OK;
            ret = tbox_get.TrayOut(ref  bquit);
            if (ret == EM_RES.OK)
            {
                if (sen_plate_at_get.AssertON())
                {
                    COM.product.TrayGet.NewTray(false, Product.EM_CM_RES.UNTEST);
                    WSGet.tray_now = COM.product.TrayGet;
                    return EM_RES.OK;
                }
                else
                    return EM_RES.ERR;
            }

            return ret;
        }
        public static EM_RES boxIN(ref bool bquit)
        {
            try
            {
                EM_RES ret = EM_RES.OK;
                status = EM_STA.BOXIN;
                if (sen_box_in.AssertON())
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}" + "有料盘在", GetStaString));
                    return EM_RES.ERR;
                }

                ret = cyl_lock_up.SetOff(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;


                //接料盒位高位一个也有传感反应
                ret = MT.pos_bullet_feed_get_box.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                while (isReady)
                {
                    Action.MsgShow("请上料盒");
                    if (sen_box_in.AssertON())
                        break;
                }

                //check box在
                if (sen_box_in.AssertOFF())
                {
                    return EM_RES.ERR;
                }
                //到到检测备料盒位置，检测有两个，再挂起，否则一直不挂
                ret = MT.pos_bullet_feed_boxIN.MoveTo(ref bquit, true); //安全
                if (ret != EM_RES.OK) return ret;
                if (sen_box_in.AssertON())
                {
                    ret = cyl_lock_up.SetOn(ref bquit, 3000);
                    if (ret != EM_RES.OK) return ret;
                }

                COM.traybox_get.NewBox(Product.EM_CM_RES.OK);
                COM.traybox_get.SetSta(TrayBox.EM_STA.FULL);
                return ret;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--" + "异常", GetStaString));
                return EM_RES.ERR;
            }

        }
        public static EM_RES boxOUT(ref bool bquit)
        {

            EM_RES ret = EM_RES.OK;
            //下降到出料位
            status = EM_STA.BOXOUT;
            ret = PosBoxOUT.MoveTo(ref bquit, true); //安全
            if (ret != EM_RES.OK) return ret;
            //检测到达出料位
            //皮带运动
            ret = belet_move.SetOn();
            if (ret != EM_RES.OK) return ret;
            ret = sen_box_out.WaitON(ref bquit);
            if (ret != EM_RES.OK) { belet_move.SetOff(); return ret; }
            ret = sen_box_out.WaitOFF(ref bquit);
            if (ret != EM_RES.OK) { belet_move.SetOff(); return ret; }
            //皮带停止
            ret = belet_move.SetOff();
            if (ret != EM_RES.OK) return ret;
            //如果成功启动下一动作
            COM.traybox_get.SetSta(TrayBox.EM_STA.EMPTY);//所有料盘满料
            COM.traybox_get.NewBox(Product.EM_CM_RES.NONE);//所有模组ok
            return ret;
        }
        public static EM_RES boxChange(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (sen_box_in.AssertOFF())
            {
                return EM_RES.ERR;
            }
            //到获取料盒位置

            ret = PosGetBox.MoveTo(ref bquit, true); //安全
            if (ret != EM_RES.OK) return ret;
            //关锁
            ret = cyl_lock_up.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            //下降到料盘位
            ret = PosTrayTop.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) return ret;
            COM.traybox_get.NewBox(Product.EM_CM_RES.OK);
            COM.traybox_get.SetSta(UI.TrayBox.EM_STA.FULL);
            return ret;
        }
        #endregion
        /// <summary>
        /// 委托轴安全防护
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>

        public static EM_RES axZ_Safe(int id, double pos = 0)
        {
            EM_RES ret = EM_RES.OK;
            if (bsafe) return ret;
            if (sen_box_in.AssertOFF())
            {
                ret = cyl_lock_up.SetOff(ref VAR.gsys_set.bquit, 3000);
                if (ret != EM_RES.OK) return ret;
            }
            ret = cyl_tray_out.SetOn(ref VAR.gsys_set.bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            return ret;
        }
        #region 运行
        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        static EM_RES WSRun(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (!isReady) return EM_RES.QUIT;
            if (sen_plate_at_get.AssertON()) return EM_RES.QUIT;//已经有料盘
            ret = trayReady(ref bquit);//自动检索料盘并拉出
            if (ret == EM_RES.OK)
            {
                ret = trayOut(ref bquit); return ret;
            }
            if (tbox_get.status == TrayBox.EM_STA.EMPTY)  //盒子空的话出料盒
            {
                ret = boxOUT(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = MT.pos_bullet_feed_boxIN.MoveTo(ref bquit, true); //安全
                if (ret != EM_RES.OK) return ret;
                if (sen_box_in.AssertON())
                {
                    ret = boxChange(ref bquit); if (ret != EM_RES.OK) return ret;
                }
                else
                {
                    ret = boxIN(ref bquit); if (ret != EM_RES.OK) return ret;
                }
                ret = trayReady(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = trayOut(ref bquit); return ret;

            }
            else
            if (tbox_get.status == TrayBox.EM_STA.NOBOX) //盒子空的话进料盒
            {
                //进料盒命令
                ret = boxIN(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = trayReady(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = trayOut(ref bquit); return ret;
            }

            VAR.ErrMsg(disc + GetStaString + "逻辑异常");
            return EM_RES.ERR;

        }
        public static Task TaskRun;
        public static void task_run()
        {
            if (TaskRun == null || (TaskRun != null && TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建出料仓线程");
                if (TaskRun != null)
                    TaskRun.Dispose();
                TaskRun = new Task(act_run);
                TaskRun.Start();
            }
        }
        #endregion
        #region 回原
        public static EM_RES home(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            status = EM_STA.HOME;
            if (!(TaskRun == null || TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 工站工作中，回原失败!", disc));
                return EM_RES.ERR;
            }
            if (sen_box_in.AssertON())
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 检测到有料盒，回原失败!", disc));
                return EM_RES.ERR;
            }
            res = cyl_lock_up.SetOff(1000);
            if (res != EM_RES.OK)
                return res;
            res = MT.AxisHome(ref bquit, ax_Z);
            status = EM_STA.STANDBY;
            return res;
        }
        #endregion
        #region 停止
        public static void Stop()
        {
            ax_Z.bhomequit = true;
            ax_Z.Stop();
        }
        #endregion

    }
    public  class WsBuBK
    {
        #region 硬件和位置
        public static TrayBox tbox_get = COM.traybox_back;
        static Product.Tray tray_out = COM.traybox_back.tray_cur;
        static AXIS ax_Z = COM.traybox_back.ax_z;
  
        static Cylinder cyl_tray_in = MT.CYL_bullet_back_plate;//拉盘气缸
        static GPIO sen_box_in = MT.CKPOS_bull_belt_topos;//皮带进料处感应料盒来
        static GPIO sen_tray_in = MT.CKPOS_bull_back_plate;
        static GPIO sen_box_out = MT.CKPOS_bull_back_box;//料盒外出感应
        static GPIO belet_move = MT.GPIO_OUT_belet;
        static POS PosBoxOUT = MT.pos_bullet_back_boxOUT;
        static POS PosSafe = MT.pos_bullet_back_safe;
        static POS PosTrayLow = MT.pos_bullet_back_check_low;
        static POS PosTrayTop = MT.pos_bullet_back_check_top;
        static POS PosBoxIn = MT.pos_bullet_back_boxIN;
        #endregion
        public static string warn_msg = "";
        public static bool isShow = true;//弹窗
        public static bool isErr;
        public static EM_STA status = EM_STA.UNKNOW;
        public static string disc = "料盘移栽工站";
        public static string GetStaString
        {
            get
            {
                return status.GetDescription();
            }
        }
        /// <summary>
        /// 运行条件
        /// </summary>
        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
          
                if (!(WSBack.TaskRun == null || WSBack.TaskRun.IsCompleted))
                    return false;
                if (!(WsTrayMove.TaskRun == null || WsTrayMove.TaskRun.IsCompleted))
                    return false;
                return true;
            }
        }
        public static bool mbOK;
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("盘拉进")]
            TRAYIN,
            [Description("准备收料位")]
            TRAYREADY,
            [Description("进料盒")]
            BOXIN,
            [Description("出料盒")]
            BOXOUT,
            [Description("错误")]
            ERR,
            [Description("等待")]
            STANDBY,
            [Description("等待取料盒")]
            WAIT
        }
        #region 动作函数
        public static EM_RES trayReady(ref bool bquit)
        {
            if (isReady) return EM_RES.QUIT;
            if (WSBack.tbox_back == null) return EM_RES.QUIT;
            EM_RES ret = WSBack.tbox_back.TrayReadyIN(ref  bquit);
            return ret;
        }
        public static EM_RES trayIN(ref bool bquit)
        {
            status = EM_STA.TRAYIN;
            if (WSBack.tbox_back == null) return EM_RES.QUIT;
            EM_RES ret = WSBack.tbox_back.TrayIn(ref  bquit);
            return ret;
        }
        public static EM_RES boxIN(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            try
            {
                status = EM_STA.BOXIN;
                int i = 0;
                //到进料位
                res = PosBoxIn.MoveTo(ref bquit, true);
                if (res != EM_RES.OK) return res;
                //check box在
                res = belet_move.SetOn();
                if (res != EM_RES.OK) return res;
                while (sen_box_in.AssertOFF() && isReady)
                {
                    Thread.Sleep(20);
                    Application.DoEvents();
                    i++;
                    if (i > 1000)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}等待超时", GetStaString));
                        res = belet_move.SetOff();
                        return EM_RES.ERR;
                    }
                }
                for (int j = 0; j < 100; j++)
                {
                    Thread.Sleep(30);
                    Application.DoEvents();
                    if (!isReady) break;
                }
                res = belet_move.SetOff();
                if (res != EM_RES.OK) return res;
                return res;
            }
            catch(Exception ee)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}-{1}", disc,ee.ToString() ));
                return EM_RES.ERR;
            }

        }
        public static EM_RES boxOUT(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            try
            {
                status = EM_STA.BOXOUT;
                //到出料位
                res = PosBoxOUT.MoveTo(ref bquit, true);
                if (res != EM_RES.OK) return res;
                if (!sen_box_out.AssertON())
                {
                    VAR.ErrMsg(string.Format("{0}+{1}无料盒", disc, status.GetDescription()));
                    return EM_RES.ERR;
                }
                //检测到达出料位等待拿走
                int i = 0;
                while (sen_box_out.AssertON() && isReady)
                {
                    Thread.Sleep(30);
                    Application.DoEvents();
                    i++;
                    if (i > 1000)
                    {
                        VAR.ErrMsg(string.Format("{0}+{1}等待拿料盒超时", disc, status.GetDescription()));
                        return EM_RES.ERR;
                    }
                }
                return res;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}-{1}未知错误", disc, GetStaString));
                return EM_RES.ERR;
            }

        }
        #endregion
        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        public static EM_RES WSRun(ref bool bquit)
        {
            try
            {
                EM_RES ret = EM_RES.OK;
                if (!isReady) return EM_RES.QUIT;
                ret = trayReady(ref bquit );
                if (ret == EM_RES.OK)//找到空位就拉一下进去
                {
                    ret = trayIN(ref bquit);
                    return ret;
                }
                if (tbox_get.status == TrayBox.EM_STA.FULL)//如果满了就出料盒
                {
                    ret = boxOUT(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = boxIN(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = trayReady(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = trayIN(ref bquit);
                    return ret;

                }
                if (tbox_get.status == TrayBox.EM_STA.EMPTY)//如果没料盒了就进料盒
                {
                    ret = boxIN(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = trayReady(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = trayIN(ref bquit);
                    return ret;
                }

                Stop();
                VAR.ErrMsg(disc + "料盒异常");
                return EM_RES.ERR;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}-{1}未知错误", disc, GetStaString));
                return EM_RES.ERR;
            }

        }
        public static Task TaskRun;
        public static void task_run()
        {
            if (TaskRun == null || (TaskRun != null && TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建料仓线程");
                if (TaskRun != null)
                    TaskRun.Dispose();
                TaskRun = new Task(act_run);
                TaskRun.Start();
            }
        }
        public static EM_RES home(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            if (!(TaskRun == null || TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 工站工作中，回原失败!", disc));
                return EM_RES.ERR;
            }
            if (sen_box_out.AssertON())
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 检测到有料盒，回原失败!", disc));
                return EM_RES.ERR;
            }
            res = MT.AxisHome(ref bquit, ax_Z);
            return res;
        }
        public static void Stop()
        {
            ax_Z.bhomequit = true;
            ax_Z.Stop();
        }
        public static EM_RES axZ_Safe(int id, double pos = 0)
        {
            EM_RES ret = EM_RES.OK;
            ret = cyl_tray_in.SetOff(ref VAR.gsys_set.bquit, 3000);
            return ret;
        }
    }
    public  class WsTrayMove 
    {
       
        public static GPIO sen_tray_get = MT.CKPOS_MOVE_get_plate;
        public static GPIO sen_tray_store = MT.CKPOS_MOVE_middle_plate;
        public static GPIO sen_tray_back = MT.CKPOS_MOVE_back_plate;
        public static Cylinder vacu_move = MT.VACUM_move_plate;
        public static AXIS ax_Z = MT.AXIS_bullet_move;

        public static EM_STA status = EM_STA.UNKNOW;
        public static bool bhome = false;
        public static string disc = "料盘移栽工站";
        public static string GetStaString
        {
            get
            {
                return status.GetDescription();
            }
        }
        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
       
                if (!(WSBack.TaskRun == null || WSBack.TaskRun.IsCompleted))
                {
                    status = EM_STA.BUSY;
                    return false;
                }
                if (!(WSGet.TaskRun == null || WSGet.TaskRun.IsCompleted))
                {
                    status = EM_STA.BUSY;
                    return false;
                }
                // if (VAR.gsys_set.status != EM_SYS_STA.RUN) return false;
                return true;
            }
        }
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("空盘上料")]
            PLACE,
            [Description("空盘存料")]
            PICK,
            [Description("错误")]
            ERR,
            [Description("安全位")]
            SAFE,
            [Description("等待")]
            STANDBY
        }
        //下降放料
        static EM_RES m_down_put(ref bool bquit)
        {
            try
            {
                EM_RES ret = EM_RES.OK;
                //检测真空
                if (!vacu_move.io_sen_on.AssertON())
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "吸嘴无料", disc, GetStaString));
                    return EM_RES.ERR;

                }

                //气缸下降
                ret = MT.CYL_bullet_move_plate_up.SetOn(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = MT.VACUM_move_plate.SetOff(ref bquit);
                if (ret != EM_RES.OK) return ret;
                Thread.Sleep(500);
                Application.DoEvents();
                Thread.Sleep(500);
                Application.DoEvents();
                Thread.Sleep(500);
                Application.DoEvents();
                Thread.Sleep(500);
                Application.DoEvents();
                Thread.Sleep(500);
                Application.DoEvents();
                //气缸上升
                ret = MT.CYL_bullet_move_plate_up.SetOff(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                //检测真空
                if (!vacu_move.io_sen_on.AssertOFF())
                    return EM_RES.ERR;
                else
                    return EM_RES.OK;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "未知错误", disc, GetStaString));
                return EM_RES.ERR;
            }
        }
        public static EM_RES ck_ax_safe(int id, double pos = 0)
        {
            EM_RES ret = EM_RES.OK;
            if (MT.CYL_bullet_move_plate_up == null) return EM_RES.ERR;
            if (MT.CYL_bullet_move_plate_up.io_sen_off.AssertON()) return EM_RES.OK;
            else
                ret = MT.CYL_bullet_move_plate_up.SetOff(ref VAR.gsys_set.bquit, 3000);
            return ret;

        }
        //移栽下降取料
        static EM_RES m_down_get(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            try
            {
                ret = MT.VACUM_move_plate.SetOn(ref bquit);
                if (ret != EM_RES.OK) return ret;
                //气缸下降
                ret = MT.CYL_bullet_move_plate_up.SetOn(ref bquit, 3000);
                //打开真空
                if (ret != EM_RES.OK) return ret;
                ret = MT.VACUM_move_plate.SetOn(ref bquit, 6000);
                if (ret != EM_RES.OK) return ret;
                //气缸升
                ret = MT.CYL_bullet_move_plate_up.SetOff(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                //检测真空
                if (MT.VACUM_move_plate.io_sen_on.AssertOFF())
                    return EM_RES.ERR;
                else
                    return EM_RES.OK;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "未知错误", disc, GetStaString));
                return EM_RES.ERR;
            }
            finally
            {
                ret = MT.CYL_bullet_move_plate_up.SetOff(ref bquit, 3000);
            }

        }
        //移栽取料
        public static EM_RES m_GET(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            try
            {
                status = EM_STA.PICK;
                //检测料盘在位
                if (MT.CKPOS_MOVE_get_plate.isOFF) return EM_RES.OK;
                ret = MT.CYL_bullet_move_plate_up.SetOff(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                //轴运动                    

                ret = MT.pos_bullet_move_get_tray.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = m_down_get(ref bquit);
                if (ret != EM_RES.OK) return ret;
                //检测料盘不在位

                if (!MT.CKPOS_MOVE_get_plate.AssertON())
                    return EM_RES.ERR;
                ret = MT.pos_bullet_move_center.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = m_down_put(ref bquit);
                if (ret != EM_RES.OK) return ret;
                //检测料盘在位
                if (MT.CKPOS_MOVE_get_plate.AssertON())
                    return EM_RES.ERR;
                else

                    return EM_RES.OK;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "未知错误", disc, GetStaString));
                return EM_RES.ERR;
            }



        }
        public static EM_RES m_PUT(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            try
            {
                //检测有无料盘            
                ret = MT.pos_bullet_move_center.MoveTo(ref bquit);
                if (ret != EM_RES.OK) return ret;
                //检测料盘在位                    
                if (!MT.CKPOS_MOVE_middle_plate.AssertON())
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "中间位无料盘请上料盘从新开始", disc, GetStaString));
                    return EM_RES.ERR;
                }
                ret = m_down_get(ref bquit);
                if (ret != EM_RES.OK) return ret;
                ret = MT.pos_bullet_move_put.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = m_down_put(ref bquit);
                if (ret != EM_RES.OK) return ret;
                //检测料盘在位
                if (!MT.CKPOS_MOVE_back_plate.AssertON())
                    return EM_RES.ERR;
                return ret;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "未知错误", disc, GetStaString));
                return EM_RES.ERR;

            }

        }
        //移栽安全位
        public static EM_RES m_SAFE(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;

            try
            {

                //轴运动       
                ret = MT.pos_bullet_move_safe.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                if (!ax_Z.isORG)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "移栽轴不在原点", disc, GetStaString));
                    return EM_RES.ERR;
                }
                else
                    return EM_RES.OK;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "未知错误", disc, GetStaString));
                return EM_RES.ERR;
            }
        }
        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        public static EM_RES WSRun(ref bool bquit)
        {

            try
            {
                EM_RES ret = EM_RES.OK;
                if (!isReady)  return ret;
                if (sen_tray_get.AssertON())//有料盘在把盘收起来
                {
                    ret = m_GET(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    WSGet.tray_now = null;
                }
                if (!sen_tray_back.AssertON())//收料盘处无盘，进行上盘
                {
                    if (sen_tray_store.AssertON())
                    {
                        ret = m_PUT(ref bquit);
                        if (ret != EM_RES.OK) return ret;
                        WSBack.tray_now = COM.product.TrayBackOK;
                        
                    }
                    else
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "中间无空盘", disc, GetStaString));
                        status = EM_STA.ERR;
                        return EM_RES.ERR;
                    }
                }
                if (!ax_Z.isORG)
                {
                    ret = m_SAFE(ref bquit);
                     return ret;
                }
                return ret;

            }
            catch (Exception ee)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "未知错误", disc, ee.ToString()));
                return EM_RES.ERR;
            }


        }
        public static Task TaskRun;
        public static void task_run()
        {
            if (TaskRun == null || (TaskRun != null && TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建料仓线程");
                if (TaskRun != null)
                    TaskRun.Dispose();
                TaskRun = new Task(act_run);
                TaskRun.Start();

            }
        }
        public static EM_RES home(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (bquit) return EM_RES.QUIT;
            status = EM_STA.HOME;
            if (!((WSGet.TaskRun == null || WSGet.TaskRun.IsCompleted) &&
               (WSBack.TaskRun == null || WSBack.TaskRun.IsCompleted)))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 取料和收料工站未退出!", disc));
                return EM_RES.ERR;
            }
            if (!WSGet.ax_x.isORG)
            {
                ret = WSGet.ax_z.MoveTo(ref bquit, 99999, 5000);
                if (ret != EM_RES.OK) return ret;
                ret = WSGet.ax_x.MoveTo(ref bquit, 99999, 5000);
                if (ret != EM_RES.OK) return ret;
                if (!WSGet.ax_x.isELP)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} q取料工站到正限位异常!", disc));
                    return EM_RES.ERR;
                }

            }
            if (!WSBack.ax_x.isORG)
            {
                ret = WSBack.ax_z.MoveTo(ref bquit, 99999, 5000);
                if (ret != EM_RES.OK) return ret;
                ret = WSBack.ax_x.MoveTo(ref bquit, 99999, 5000);
                if (ret != EM_RES.OK) return ret;
                if (!WSBack.ax_x.isELP)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 取料工站到正限位异常!", disc));
                    return EM_RES.ERR;
                }

            }
            ret = MT.AxisHome(ref bquit, ax_Z);
            if (ret != EM_RES.OK) return ret;
            else
                status = EM_STA.STANDBY;
            return ret;
        }
        public static void Stop()
        {
            ax_Z.bhomequit = true;
            ax_Z.Stop();
        }

    }
    #endregion

   
    #region 取料
    public class WSGet
    {
       
        public static TrayBox tbox_get = COM.traybox_get;
        public static Product.Tray tray_now=new Product.Tray(9);        
        public static AXIS ax_x = MT.AXIS_GET_X;
        public static AXIS ax_y = MT.AXIS_GET_Y;
        public static AXIS ax_z = MT.AXIS_GET_Z;
        public static AXIS ax_a = MT.AXIS_GET_A;
        static Cylinder cyl_z = MT.CYL_get_up;
        static Cylinder zk_z = MT.VACUM_get_mouth;
        static GPIO sen_plate_at_get = MT.CKPOS_MOVE_get_plate;
        #region 位置
        static POS ps_sf = MT.pos_get_safe;
      public  static POS ps_pho_L = MT.pos_get_photo_L;
      public  static POS ps_pho_R = MT.pos_get_photo_R;
        static POS ps_put_L = MT.pos_get_put_L;
        static POS ps_put_R = MT.pos_get_put_R;
        static POS ps_put_to_L = MT.pos_get_to_put_L;
        static POS ps_put_to_R = MT.pos_get_to_put_R;
        static POS ps_zDown = MT.pos_get_z_dwn;
        static POS ps_zUP = MT.pos_get_z_up;

        static List<POS> ps_list = MT.pos_list_get;
        #endregion

        public static bool bOK
        {
            get 
            {
                if (bLPutOK && bRPutOK)
                    return true;
                else return false;
            }
            set 
            {
                if (value )
                {
                    bLPutOK = true;
                    bRPutOK = true;
                }
                else
                {
                    bLPutOK = false;
                    bRPutOK = false;
                }
            }
        }
        //单点取料次数 

        //连续取料失败次数要报警
        public static int modu_id;
        public static string disc = "取料工站-";
        //拍照选择左右
        public static bool bLPutOK;
        public static bool bRPutOK;
        static ST_XYZ Move_L;//左放料视觉偏移量
        static ST_XYZ Move_R;//左放料视觉偏移量
        //运动函数委托

        public static Task TaskRun = null;
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("拍照")]
            PHOTO,
            [Description("左放料")]
            PLACE,
            [Description("右放料")]
            PLACE_R,
            [Description("取料")]
            PICK,
            [Description("等待料盘")]
            WAIT,
            [Description("错误")]
            ERR,
            [Description("退出")]
            QUIET
        }

        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
             
                if (!(WsBuFD.TaskRun == null || WsBuFD.TaskRun.IsCompleted))
                {
                    status = EM_STA.WAIT;
                    return false;
                }
                if (!(WsTrayMove.TaskRun == null || WsTrayMove.TaskRun.IsCompleted))
                {
                    status = EM_STA.WAIT;
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 轴安全
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static EM_RES ck_ax_safe(int id, double pos = 0)
        {
            bool bquit=false;
            int i = 0;
            double max = 9999;
            try
            {
                if (!ax_z.isELP)
                {
                    ax_z.MoveTo(ref bquit, max);
                    while (!ax_z.isELP)
                    {
                        i++;
                        Thread.Sleep(100);
                        Application.DoEvents();
                        if (i > 100)
                        {
                            VAR.ErrMsg(ax_z.disc + "到正限位异常");
                            return EM_RES.ERR;
                        }
                    }
                }

                return EM_RES.OK;
            }
            catch(Exception e)
            {
                VAR.ErrMsg(e.ToString());
                return EM_RES.ERR;
            }
           
           

        }
        public static string GetStaString
        {
            get
            {
                return status.GetDescription() + "过程";

            }
        }//获取状态翻译
     
        //需要保存，当前流程状态
        public static EM_STA status = EM_STA.UNKNOW;
        /// <summary>
        /// 取料料模块复位
        /// </summary>
        /// <param name="bquit"></param>
        /// <returns></returns>
        /// 
        public static EM_RES Home(ref bool bquit)
        {
            //status = EM_STA.READY;
            //return EM_RES.OK;
            try
            {
                EM_RES res = EM_RES.OK;
                if (bquit)
                    return EM_RES.QUIT;
                status = EM_STA.HOME;
                //气缸回位
                res = cyl_z.SetOff(ref bquit, 1000);
                if (res != EM_RES.OK) return res;
                //先抬升
                if (!ax_z.isELP)
                {
                    ax_z.MoveTo(ref bquit, ax_z.fcmd_pos + 9999, 1000);
                    Thread.Sleep(20);
                    if (!ax_z.isELP)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}到正限位异常!", disc, ax_z.disc));
                        return EM_RES.ERR; ;
                    }
                }
                //other axis
                res = MT.AxisHome(ref bquit, ax_x, ax_y, ax_a);
                if (res != EM_RES.OK) return res;
                res = MT.AxisHome(ref bquit, ax_z);
                if (res != EM_RES.OK) return res;
                status = EM_STA.READY;
                return res;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR; ;
            }
        }
        /// <summary>
        /// 停止轴运动，停止Home动作
        /// </summary>   
        public static void Stop()
        {
            ax_x.bhomequit = true;
            ax_x.Stop();
            ax_y.bhomequit = true;
            ax_y.Stop();
            ax_z.bhomequit = true;
            ax_z.Stop();
            ax_a.bhomequit = true;
            ax_a.Stop();
        }
        /// <summary>
        /// 准备取料
        /// </summary>   
        /// <summary>
        /// 从安全位开始到取料动作
        /// </summary>     
        public static EM_RES m_ActGet(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;        
            try
            {                
                tray_now = COM.product.TrayGet;
                if (tray_now == null)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}料盘空!", disc, GetStaString));
                    return EM_RES.PARA_ERR;
                }
                if (tray_now.bEmpty)
                    return EM_RES.QUIT;
                status = EM_STA.PICK;
                //检测吸嘴有料
                ret = zk_z.SetOn(ref bquit);
                if (ret != EM_RES.OK) return ret;
                if (zk_z.io_sen_on.AssertON(10, 100))
                    return EM_RES.OK;
                ret = zk_z.SetOff(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = ps_sf.MoveTo(ref bquit, true); //安全
                if (ret != EM_RES.OK) return ret;
                ret = tray_now.ToPosId( MT.pos_get_plate_star,ref bquit);   //到取料位置
                if (ret != EM_RES.OK) return ret;
                ret = ps_zDown.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = cyl_z.SetOn(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                if (!Action.bNullRun)
                {
                    ret = zk_z.SetOn(ref bquit, 4000);
                    if (ret != EM_RES.OK) return ret;
                }
                    ret = ps_zUP.MoveTo(ref bquit, true);
                    if (ret != EM_RES.OK) return ret;

                if (zk_z.isONByChkSen || Action.bNullRun)
                {
                    tray_now.list_mask[tray_now.get_id] = false;
                    return EM_RES.OK;
                }
                else
                    return EM_RES.ERR;
            }
            catch
            {

                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }
            finally
            {
                 ps_zUP.MoveTo(ref bquit, true);
               
              

            }
        }
        public static EM_RES m_ToPhoto(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            status = EM_STA.PHOTO;
            try
            {
                status = EM_STA.PHOTO;
                if (!bLPutOK)
                {
                    ret = ps_pho_L.MoveTo(ref bquit, true); //安全
                    if (ret != EM_RES.OK) return ret;
                    ret = COM.MVS.m_camera_action(1, out Move_L);
                   if (ret != EM_RES.OK) return ret;                
                   return ret;
                }
                else if (!bRPutOK)
                {
                   
                    ret = ps_pho_R.MoveTo(ref bquit, true); //安全
                    if (ret != EM_RES.OK) return ret;
                    ret = COM.MVS.m_camera_action(2, out Move_R);
                    if (ret != EM_RES.OK) return ret;
                    return ret;
                }
                else
                {
                    return EM_RES.OK;
                }

            }

            catch
            {

                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }
        }
        public static EM_RES m_ToPut(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            status = EM_STA.PLACE;
            try
            {
                if (!zk_z.isONByChkSen&&!Action.bNullRun)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}吸嘴无料!", disc, GetStaString));
                    return EM_RES.ERR;
                }
                //ret = ps_sf.MoveTo(ref bquit, true); //安全
                //if (ret != EM_RES.OK) return ret;
                if (!bLPutOK)
                {
                    ret = ps_put_to_L.MoveTo(ref bquit, true); //安全
                    if (ret != EM_RES.OK) return ret;
                    ret = ps_put_L.MoveTo(ref bquit, true); //安全
                    if (ret != EM_RES.OK) return ret;
                    //POS mPos = new POS(MT.AXIS_GET_X, MT.AXIS_GET_Y, MT.AXIS_GET_A,null, "视觉偏移", 0, 0, Move_L);
                    ret = MT.AXIS_GET_X.MoveTo(ref bquit, MT.AXIS_GET_X.fcmd_pos + Move_L.x, 2000);
                    if (ret != EM_RES.OK) return ret;
                    ret = MT.AXIS_GET_Y.MoveTo(ref bquit, MT.AXIS_GET_X.fcmd_pos + Move_L.y, 2000);
                    if (ret != EM_RES.OK) return ret;
                    ret = MT.AXIS_GET_A.MoveTo(ref bquit, MT.AXIS_GET_A.fcmd_pos + Move_L.z, 2000);
                    if (ret != EM_RES.OK) return ret;
                    ret = m_ActPut(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = ps_put_L.MoveTo(ref bquit, true); //安全
                    if (ret != EM_RES.OK) return ret;
                    ret = ps_put_to_L.MoveTo(ref bquit, true); //安全
                    if (ret != EM_RES.OK) return ret;

                }
                else
                    if (!bRPutOK)
                    {
                        ret = ps_put_to_R.MoveTo(ref bquit, true); //安全
                        if (ret != EM_RES.OK) return ret;
                        ret = ps_put_R.MoveTo(ref bquit, true); //安全
                        if (ret != EM_RES.OK) return ret;
                        ret = MT.AXIS_GET_X.MoveTo(ref bquit, MT.AXIS_GET_X.fcmd_pos + Move_R.x, 2000);
                        if (ret != EM_RES.OK) return ret;
                        ret = MT.AXIS_GET_Y.MoveTo(ref bquit, MT.AXIS_GET_X.fcmd_pos + Move_R.y, 2000);
                        if (ret != EM_RES.OK) return ret;
                        ret = MT.AXIS_GET_A.MoveTo(ref bquit, MT.AXIS_GET_A.fcmd_pos + Move_R.z, 2000);
                        if (ret != EM_RES.OK) return ret;
                        ret = m_ActPut(ref bquit);
                        if (ret != EM_RES.OK) return ret;
                        ret = ps_put_R.MoveTo(ref bquit, true); //安全
                        if (ret != EM_RES.OK) return ret;
                        ret = ps_put_to_R.MoveTo(ref bquit, true); //安全
                        if (ret != EM_RES.OK) return ret;
                    }
                    else
                        return EM_RES.OK;
                return ret;
            }
            catch
            {

                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }
        }
        public static EM_RES m_ActPut(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            try
            {
                status = EM_STA.PLACE;
                if (!isReady) return EM_RES.QUIT;
                if (!zk_z.isONByChkSen && !Action.bNullRun)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}吸嘴无料!", disc, GetStaString));
                    return EM_RES.ERR;
                }
                ret = ps_zDown.MoveTo(ref bquit, true); //安全
                if (ret != EM_RES.OK) return ret;
                zk_z.SetOff(ref bquit);
                ret = ps_zUP.MoveTo(ref bquit, true); //安全
                if (ret != EM_RES.OK) return ret;
                if (!bLPutOK)
                    bLPutOK = true;
                else if (!bRPutOK)
                    bRPutOK = true;
                return ret;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}吸嘴无料!", disc, GetStaString));
                return EM_RES.ERR;
            }

        }


        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        public static EM_RES WSRun(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            int i = 0;
            try
            {
                if (bOK)
                {
                    status = EM_STA.READY;
                    return EM_RES.OK;
                }
                if (!isReady) return EM_RES.QUIT;
                if (!sen_plate_at_get.AssertON())//检测盘
                {
                   // tray_now = null;
                    WsBuFD.task_run();//出料盘
                    status = EM_STA.WAIT;
                    Stop();
                    return EM_RES.WAIT;
                }
                if (tray_now == null)
                {
                    // tray_now = COM.product.TrayGet;
                    return EM_RES.ERR;
                }
                else if (tray_now.bEmpty)  //空盘收料
                {
                    WsTrayMove.task_run();
                    status = EM_STA.WAIT;
                    Stop();
                    Thread.Sleep(100);
                    Application.DoEvents();
                    return EM_RES.WAIT;
                }
                if (!WsTrayMove.ax_Z.isORG)
                {
                    WsTrayMove.task_run();
                    status = EM_STA.WAIT;
                    Stop();
                    Thread.Sleep(100);
                    Application.DoEvents();
                    return EM_RES.WAIT;
                }
                ret = m_ActGet(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = m_ToPhoto(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = m_ToPut(ref bquit);  return ret;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }
        }
        public static void task_run()
        {
            if (TaskRun == null || (TaskRun != null && TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建取料线程");
                if (TaskRun != null)
                    TaskRun.Dispose();
                TaskRun = new Task(act_run);
                TaskRun.Start();

            }
        }
        public static EM_RES home(ref bool bquit)
        {
            try
            {
                EM_RES res = EM_RES.OK;
                if (bquit) return EM_RES.QUIT;
                if (!(WSGet.TaskRun == null || WSGet.TaskRun.IsCompleted))
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}运行线程未退出!", disc));
                    return EM_RES.ERR;
                }
                res = cyl_z.SetOff(3000);
                if (res != EM_RES.OK) return res;
                if (bquit) return EM_RES.QUIT;
                res = ax_z.MoveTo(ref bquit, 9999, 5000);
                if (res != EM_RES.OK && res != EM_RES.PARA_OUTOFRANG) return res;
                if (bquit) return EM_RES.QUIT;
                res = MT.AxisHome(ref bquit, ax_y, ax_x, ax_a);
                if (res != EM_RES.OK) return res;
                res = MT.AxisHome(ref bquit, ax_z);
                return res;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--回原-" + "未知系统异常", disc));
                return EM_RES.ERR;
            }
        }



    }
    #endregion
    #region   收料
    public  class WSBack 
    {
        public static TrayBox tbox_back = COM.traybox_back;
        public static Product.Tray tray_now =COM.product.TrayBackOK;
        public static Product.Tray tray_AANG = COM.product.TrayBackAANG;
        public static Product.Tray tray_PPNG = COM.product.TrayBackPPNG;
        public static Product.Tray tray_TTNG = COM.product.TrayBackTTNG;
        public static AXIS ax_x = MT.AXIS_BACK_X;
        public static AXIS ax_y = MT.AXIS_BACK_Y;
        public static AXIS ax_z = MT.AXIS_BACK_Z;
        public static AXIS ax_a = MT.AXIS_BACK_A;
        static Cylinder cyl_z = MT.CYL_back_up;
        static Cylinder zk_z = MT.VACUM_back_mouth;
       public static POS ps_sf = MT.pos_back_safe;
        static POS ps_pho_R = MT.pos_get_photo_R;

        static POS ps_get_L = MT.pos_back_L;
        static POS ps_get_R = MT.pos_back_R;
        static POS ps_get_to_L = MT.pos_back_to_L;
        static POS ps_get_to_R = MT.pos_back_to_R;
        static POS ps_zDown = MT.pos_back_z_dwn;
        static POS ps_zUP = MT.pos_back_z_up;
        static POS ps_row = MT.pos_back_plate_row;
        static POS ps_col = MT.pos_back_plate_line;
        static POS ps_star = MT.pos_back_plate_star;
        public static List<POS> ps_list = MT.pos_list_back;
        public static ST_XYZA pos_now;
        public static bool bOK
        {
            get
            {
                if (bLGet && bRGet)
                    return true;
                else return false;
            }
            set
            {
                if (value)
                {
                    bLGet = true;
                    bRGet = true;
                }
                else
                {
                    bLGet = false;
                    bRGet = false;
                }
            }
        }
        public static string disc = "收料工站-";
        public static EM_STA status = EM_STA.UNKNOW;
        public static string GetStaString
        {
            get
            {
                return status.GetDescription() + "-";

            }
        }

        //取料选择左右
        public static bool bLGet;
        public static bool bRGet;
        //收料盘是否放好
        public static bool b_tray_OK;

        public static Task TaskRun;
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("放料")]
            PLACE,
            [Description("取料")]
            PICK,
            [Description("错误")]
            ERR,
            [Description("等待料盘")]
            WAIT,
            [Description("退出")]
            QUIET
        }       
        //需要保存，当前状态     
        /// <summary>
        /// 取料料模块复位
        /// </summary>
        /// <param name="bquit"></param>
        /// <returns></returns>
        public static EM_RES Home(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            if (bquit)
                return EM_RES.QUIT;
            status = EM_STA.HOME;
            //气缸回位
            res = cyl_z.SetOff(ref bquit, 1000);
            if (res != EM_RES.OK) return res;
            //先抬升
            if (!ax_z.isELP)
            {
                ax_z.MoveTo(ref bquit, ax_z.fcmd_pos + 9999, 1000);
                Thread.Sleep(20);
                if (!ax_z.isELP)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}到正限位异常!", disc, ax_z.disc));
                    return EM_RES.ERR; ;
                }
            }

            //other axis
            res = MT.AxisHome(ref bquit, ax_x, ax_y, ax_a);
            if (res != EM_RES.OK) return res;

            //AXIS[] ax_xy = new AXIS[] { ax_x, ax_y, ax_a };
            //res = MT.AxisHome(ref bquit, ax_xy);
            //if (res != EM_RES.OK) return res;

            res = MT.AxisHome(ref bquit, ax_z);
            if (res != EM_RES.OK) return res;

            return res;
        }
        /// <summary>
        /// 停止轴运动，停止Home动作
        /// </summary>   
        public static void Stop()
        {
            ax_x.bhomequit = true;
            ax_x.Stop();
            ax_y.bhomequit = true;
            ax_y.Stop();
            ax_z.bhomequit = true;
            ax_z.Stop();
            ax_a.bhomequit = true;
            ax_a.Stop();
        }
        public static EM_RES ck_ax_safe(int id, double pos = 0)
        {
            bool bquit = false;
            int i = 0;
            double max = 9999;
            try
            {
                if (!ax_z.isELP)
                {
                    ax_z.MoveTo(ref bquit, max);
                    while (!ax_z.isELP)
                    {
                        i++;
                        Thread.Sleep(100);
                        Application.DoEvents();
                        if (i > 100)
                        {
                            VAR.ErrMsg(ax_z.disc + "到正限位异常");
                            return EM_RES.ERR;
                        }
                    }
                }

                return EM_RES.OK;
            }
            catch (Exception e)
            {
                VAR.ErrMsg(e.ToString());
                return EM_RES.ERR;
            }



        }
        /// <summary>
        /// 准备取料
        /// </summary>   
        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
                if (!(WsBuBK.TaskRun == null || WsBuBK.TaskRun.IsCompleted))
                    return false;
                if (!(WsTrayMove.TaskRun == null || WsTrayMove.TaskRun.IsCompleted))
                    return false;
                //   if (VAR.gsys_set.status != EM_SYS_STA.RUN) return false;
                return true;
            }
        }
        /// <summary>
        /// 从安全位开始到取料动作
        /// </summary>     
        public static EM_RES m_ActGet(ref bool bquit)
        {
            try
            {
                status = EM_STA.PICK;
                EM_RES ret = EM_RES.OK;
                if (!isReady) return EM_RES.QUIT;
                //检测到吸嘴有料
                if (zk_z.isONByChkSen)
                {
                    return EM_RES.OK;
                }
                //Z轴下降到位          
                if (bquit) return EM_RES.QUIT;
                ret = ps_zDown.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                //气缸下降
                ret = cyl_z.SetOff(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;
                //真空吸
                ret = zk_z.SetOn(ref bquit, 3000);
                if (ret != EM_RES.OK) return ret;

                if (!bLGet)
                {
                    ret = ax_a.MoveTo(ref bquit, ax_a.fcmd_pos + 20, 3000);
                    if (ret != EM_RES.OK) return ret;
                   
                }            
                else if (!bRGet)
                {
                    ret = ax_a.MoveTo(ref bquit, ax_a.fcmd_pos - 20, 3000);
                    if (ret != EM_RES.OK) return ret;
                   
                }        
                return ret;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }
        }
        public static EM_RES m_ToGet(ref bool bquit)
        {
            try
            {
                status = EM_STA.PICK;
                EM_RES ret = EM_RES.OK;
                if (zk_z.isONByChkSen)
                {
                    return EM_RES.OK;
                }

                if (!bLGet)
                {
                    ret = ps_get_to_L.MoveTo(ref bquit, true);
                    if (ret != EM_RES.OK) return ret;
                    ret = ps_get_L.MoveTo(ref bquit, true);
                    if (ret != EM_RES.OK) return ret;
                }
                else
                    if (!bRGet)
                    {

                        ret = ps_get_to_R.MoveTo(ref bquit, true);
                        if (ret != EM_RES.OK) return ret;
                        ret = ps_get_R.MoveTo(ref bquit, true);
                        if (ret != EM_RES.OK) return ret;
                    }
                return ret;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }

        }
        public static EM_RES m_ActPut(ref bool bquit)
        {
            try
            {
                status = EM_STA.PLACE;
                EM_RES ret = EM_RES.OK;
                if (bLGet && bRGet) return ret;
                if (tray_now == null) return EM_RES.QUIT;
                if (tray_now.bFull) return EM_RES.QUIT;
                if (!zk_z.isONByChkSen)
                {

                    VAR.ErrMsg(string.Format("{0}{1} 吸嘴无料", disc, GetStaString));
                    return EM_RES.ERR;
                }
   
                //需要根据状态放不同料盘-gy-1227
                ret = tray_now.ToPosId(MT.pos_back_plate_star,ref bquit, tray_now.put_id);
                if (ret != EM_RES.OK) return ret;
                ret = ps_zDown.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = zk_z.SetOff(ref bquit, 2000);
                if (ret != EM_RES.OK) return ret;
                ret = ps_zUP.MoveTo(ref bquit, true);
                if (ret != EM_RES.OK) return ret;

                if (!bLGet)
                    bLGet = true;
                else
                    if (!bRGet)
                        bRGet = true;

                tray_now.list_mask[tray_now.put_id] = true;
                return ret;
            }

            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }

        }

        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        public static EM_RES WSRun(ref bool bquit)
        {
            try
            {
                EM_RES ret = EM_RES.OK;
                if (bOK)
                {
                    status = EM_STA.READY;
                    Stop();
                    return ret;
                }
                if (!isReady)
                {
                    Stop();
                    return EM_RES.QUIT;
                }

                if (!WsTrayMove.sen_tray_back.AssertON()|| tray_now == null)//检测盘不在位
                {
                  //  tray_now = null;
                    WsBuFD.task_run();//出料盘
                    status = EM_STA.WAIT;
                    Stop();
                    return EM_RES.WAIT;
                }
             //   tray_now = COM.product.TrayBackOK;
            
                if (tray_now.bFull && WsTrayMove.sen_tray_back.AssertON())  //满盘进料
                {
                    WsTrayMove.task_run();
                    Stop();
                    status = EM_STA.WAIT;
                    return EM_RES.WAIT;
                }
                if (!WsTrayMove.ax_Z.isORG)
                {
                    WsTrayMove.task_run();
                    status = EM_STA.WAIT;
                    Stop();
                    return EM_RES.WAIT;
                }               
                ret = m_ToGet(ref bquit);if (ret != EM_RES.OK) return ret;
                ret = m_ActGet(ref bquit); if (ret != EM_RES.OK) return ret;
                ret = m_ActPut(ref bquit); if (ret != EM_RES.OK) return ret;
                if (!ax_y.isORG)
                    ret = ps_sf.MoveTo(ref VAR.gsys_set.bquit, true);
                return ret;

            }
            catch (Exception ee)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "运行失败", disc, ee.ToString()));
                Action.stop();
                status = EM_STA.ERR;
                return EM_RES.ERR;
            }

        }
        public static void task_run()
        {
            if (TaskRun == null || (TaskRun != null && TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建收料线程");
                if (TaskRun != null)
                    TaskRun.Dispose();
                TaskRun = new Task(act_run);
                TaskRun.Start();

            }
        }
        public static EM_RES home(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            try
            {
                if (bquit) return EM_RES.QUIT;
                if (!(TaskRun == null || TaskRun.IsCompleted))
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 工站工作中，回原失败!", disc));
                    return EM_RES.ERR;
                }

                res = cyl_z.SetOff(3000);
                if (res != EM_RES.OK) return res;
                if (bquit) return EM_RES.QUIT;
                res = ax_z.MoveTo(ref bquit, 99999, 5000);
                if (res != EM_RES.OK && res != EM_RES.PARA_OUTOFRANG) return res;
                if (!ax_z.isELP) return EM_RES.ERR;
                if (bquit) return EM_RES.QUIT;
                //res = MT.AxisHome(ref bquit, ax_y, ax_x, ax_a);//-gy-1201-旋转轴失败
                res = MT.AxisHome(ref bquit, ax_y, ax_x);
                if (res != EM_RES.OK) return res;
                res = MT.AxisHome(ref bquit, ax_z);
                return res;

            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}未知错误!", disc, GetStaString));
                return EM_RES.ERR;
            }

        }
        
    }
    #endregion
    #region AA上料
    public class WSFeed
    {
        #region 硬件执行
        static AXIS ax_x = MT.AXIS_FEED_X;
        static AXIS ax_y = MT.AXIS_FEED_Y;
        static AXIS ax_z = MT.AXIS_FEED_Z;
        static AXIS ax_a = MT.AXIS_FEED_A;
        static Cylinder cyl_up_L = MT.CYL_feed_left_up;
        static Cylinder cyl_up_R = MT.CYL_feed_right_up;    
        //夹紧气缸
        static Cylinder cyl_clip_L = MT.CYL_feed_left_clip;
        static Cylinder cyl_clip_R = MT.CYL_feed_right_clip;
        //夹爪真空
        static Cylinder vacu_clip_L = MT.VACUM_feed_hand_L;
        static Cylinder vacu_clip_R = MT.VACUM_feed_hand_R;
        #endregion
        #region 位置
        static POS ps_sf = MT.pos_feed_safe;
        static POS ps_get_L = MT.pos_feed_left_get;
        static POS ps_put_L = MT.pos_feed_left_feed;
        static POS ps_get_R = MT.pos_feed_right_get;
        static POS ps_put_R = MT.pos_feed_right_back;      
        static POS ps_zDwn = MT.pos_feed_z_dwn;
        static POS ps_zUP = MT.pos_feed_z_up;
        static List<POS> ps_list = MT.pos_list_feed;
        public static POS ps_photo_L = MT.pos_feed_photo;
        #endregion
        public static string disc = "上料工站-";
        public static bool bOK
        {
            get
            {
               
                if ((bLput && bRput)) return true;
                else
                return false;
            }
            set
            {

                bLput = value;
                bRput = value;

            }
        }              
        static bool is_get_L
        {
            get
            {
                
                if (cyl_clip_L.io_out.isON && cyl_clip_L.io_sen_on.AssertON())
                    return true;//取料成功
                else return false;
            }

        }  //是否左取料
        public static bool bLput;//左放料完成,重要变量    
        static bool is_get_R
        {
            get
            {
                
                if (cyl_clip_R.io_out.isON && cyl_clip_R.io_sen_on.AssertON())
                    return true;//取料成功
                else return false;
            }

        }
        /// <summary>
        /// 收到AA上料命令
        /// </summary>
        public static bool bAAcmd;
        /// <summary>
        /// 右放料完成
        /// </summary>
        public static bool bRput;

        static ST_XYZ Move_L;
        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("安全位")]
            SAFE,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("左放料")]
            PLACE_L,
            [Description("右放料")]
            PLACE_R,
            [Description("左取料")]
            PICK_L,
            [Description("右取料")]
            PICK_R,
            [Description("错误")]
            ERR,
            [Description("拍照")]
            PHOTO,
            [Description("退出")]
            QUIET
        }
        public static bool isErr;
        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
                //if (VAR.gsys_set.status != EM_SYS_STA.RUN) return false;
                return true;
            }
        }
        public static string GetStaString
        {
            get
            {
                return status.GetDescription() + "过程";

            }
        }   
        public static EM_STA status = EM_STA.UNKNOW;
    
        public static EM_RES ck_ax_safe(int id, double pos = 0)
        {

            if (cyl_up_L == null || cyl_up_R == null) return EM_RES.ERR;
            if (cyl_up_L.io_sen_off == null || cyl_up_R.io_sen_off == null) return EM_RES.ERR;
         
            if (!cyl_up_L.io_sen_off.AssertON())
                cyl_up_L.SetOff(3000);
            if (!cyl_up_L.io_sen_off.AssertON())
            {
                VAR.ErrMsg("夹爪抬升到位异常");
                return EM_RES.ERR;
            }
            if (!cyl_up_R.io_sen_off.AssertON())
                cyl_up_R.SetOff(3000);
            if (!cyl_up_R.io_sen_off.AssertON())
            {
                VAR.ErrMsg("夹爪抬升到位异常");
                return EM_RES.ERR;
            }
            if (!MT.AXIS_FEED_Z.isELP)
                MT.AXIS_FEED_Z.MoveTo(ref VAR.gsys_set.bquit, MT.AXIS_FEED_Z.fcmd_pos+9999, 3000);
            if (!MT.AXIS_FEED_Z.isELP)
            {
                VAR.ErrMsg("Z轴到正限位异常");
                return EM_RES.ERR;
            }

            if (cyl_up_L.io_sen_off.AssertON() && cyl_up_R.io_sen_off.AssertON())
                return EM_RES.OK;
            else
                return EM_RES.ERR;

        }
        /// <summary>
        /// 取料料模块复位
        /// </summary>
        /// <param name="bquit"></param>
        /// <returns></returns>
        public static EM_RES Home(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            if (bquit)
                return EM_RES.QUIT;

            //先抬升
            if (!ax_z.isELP)
            {
                ax_z.MoveTo(ref bquit, ax_z.fcmd_pos + 9999, 1000);
                Thread.Sleep(20);
                if (!ax_z.isELP)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} {1}到正限位异常!", disc, ax_z.disc));
                    return EM_RES.ERR; ;
                }
            }

            //other axis
            res = MT.AxisHome(ref bquit, ax_x, ax_y, ax_a);
            if (res != EM_RES.OK) return res;

            //AXIS[] ax_xy = new AXIS[] { ax_x, ax_y, ax_a };
            //res = MT.AxisHome(ref bquit, ax_xy);
            //if (res != EM_RES.OK) return res;

            res = MT.AxisHome(ref bquit, ax_z);
            if (res != EM_RES.OK) return res;

            return res;
        }
        /// <summary>
        /// 停止轴运动，停止Home动作
        /// </summary>   
        public static void Stop()
        {
            ax_x.bhomequit = true;
            ax_x.Stop();
            ax_y.bhomequit = true;
            ax_y.Stop();
            ax_z.bhomequit = true;
            ax_z.Stop();
            ax_a.bhomequit = true;
            ax_a.Stop();
        }
        /// <summary>
        /// 从安全位开始到取料动作
        /// </summary>     
        public static EM_RES m_ActGet_L(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (!isReady) return EM_RES.QUIT;
            if (is_get_L) return EM_RES.OK;
            if (WSROLL.VacumRoll[WSROLL.aa_id].io_sen_off.isON)
            {

                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "无夹具在位", disc, GetStaString));            
                return EM_RES.ERR;
            }
            ret = ps_get_L.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) return ret;
            //打开夹爪
            ret = cyl_clip_L.SetOff(ref bquit, 3000);
            //用感应信号判断有没有夹到重要
            if (ret != EM_RES.OK) return ret;
            //气缸上升
            ret = cyl_up_L.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            //Z轴下降到位
            ret = ps_zDwn.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) return ret;
            //气缸下降
            ret = cyl_up_L.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            //夹爪收紧
            ret = cyl_clip_L.SetOn(ref bquit, 3000);
               if (ret != EM_RES.OK) return ret;
            //夹具真空关闭
               ret = WSROLL.VacumRoll[WSROLL.aa_id].SetOff();
               if (ret != EM_RES.OK) return ret;
            //夹爪真空打开
             ret=  vacu_clip_L.SetOn();
             if (ret != EM_RES.OK) return ret;
            //气缸上升
            ret = cyl_up_L.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            //Z轴回升        
            ret = ps_zUP.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) return ret;

            if (UI.Action.bNullRun)//空料运行
                return EM_RES.OK;
            if (is_get_L)
                return EM_RES.OK;
            else return EM_RES.ERR;
        }
        public static EM_RES m_ActGet_R(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (!isReady) return EM_RES.QUIT;
            if (is_get_R) return EM_RES.OK;
          
            ret = ps_get_R.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            //打开夹爪
            ret = cyl_clip_R.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //气缸上升
            ret = cyl_up_R.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //Z轴下降到位
            ret = ps_zDwn.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            //气缸下降
            ret = cyl_up_R.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //夹爪收紧
            ret = cyl_clip_R.SetOn(ref bquit, 3000);
              if (ret != EM_RES.OK) goto MSTOP;
              //夹具真空关闭
              //ret = WSROLL.VacumRoll[WSROLL.aa_id].SetOff();
              //if (ret != EM_RES.OK) return ret;
              //夹爪真空打开
              ret = vacu_clip_R.SetOn();
              if (ret != EM_RES.OK) return ret;
            //气缸上升
            ret = cyl_up_R.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //Z轴回升        
            ret = ps_zUP.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            if (is_get_R)
                ret = EM_RES.OK;
            else ret = EM_RES.ERR;
        MSTOP:
            m_ZSafe(ref  bquit);
            Stop();
            return ret;

        }
        /// <summary>
        /// 运动到取料位
        /// </summary> 
        public static EM_RES m_ZSafe(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (bquit) return EM_RES.QUIT;
            if (!isReady) return EM_RES.QUIT;

            ret = ps_zUP.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) return ret;
            //打开夹爪
            ret = cyl_up_L.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            ret = cyl_up_R.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            return ret;
        }
        public static EM_RES m_ToPhoto(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            status = EM_STA.PHOTO;
            if (!isReady) return EM_RES.QUIT;
            ret = m_ZSafe(ref  bquit);
            if (ret != EM_RES.OK) return ret;

            ret = ps_photo_L.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) return ret;
            ret = COM.MVS.m_camera_action(3, out Move_L);
            if (ret != EM_RES.OK) return ret; 
            return ret;

        }
        public static EM_RES m_ActPut_L(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (!isReady) return EM_RES.QUIT;
            if (!is_get_L&&!UI.Action.bNullRun) return EM_RES.QUIT;
            status = EM_STA.PLACE_L;
            ret = ps_put_L.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = MT.AXIS_FEED_X.MoveTo(ref bquit, MT.AXIS_FEED_X.fcmd_pos + Move_L.x, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = MT.AXIS_FEED_Y.MoveTo(ref bquit, MT.AXIS_FEED_Y.fcmd_pos + Move_L.y, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = MT.AXIS_FEED_A.MoveTo(ref bquit, MT.AXIS_FEED_A.fcmd_pos + Move_L.z, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = ps_zDwn.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            //气缸下降
            ret = cyl_up_L.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //打开夹爪
            ret = cyl_clip_L.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //夹具真空关闭
            //ret = WSROLL.VacumRoll[WSROLL.aa_id].SetOn();
            //if (ret != EM_RES.OK) return ret;
            //夹爪真空打开
            ret = vacu_clip_L.SetOff();
            if (ret != EM_RES.OK) return ret;
            //气缸回升
            ret = cyl_up_L.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = ps_zUP.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
        
            bLput = true;
        MSTOP:
            Stop();
            cyl_up_L.SetOn(ref bquit, 3000);
            ps_zUP.MoveTo(ref bquit, true);
            return ret;


        }
        public static EM_RES m_ActPut_R(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (!isReady) return EM_RES.QUIT;
            if (!is_get_R && !UI.Action.bNullRun) return EM_RES.QUIT;
            status = EM_STA.PLACE_R;
            ret = ps_put_R.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = ps_zDwn.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK) goto MSTOP;
            //气缸下降
            ret = cyl_up_R.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            //打开夹爪
            ret = cyl_clip_R.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;

            //转盘真空打开
            ret = WSROLL.VacumRoll[WSROLL.aa_id].SetOn();
            if (ret != EM_RES.OK) return ret;
            //夹爪真空关闭
            ret = vacu_clip_R.SetOff();
            if (ret != EM_RES.OK) return ret;
            //气缸回升
            ret = cyl_up_R.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) goto MSTOP;
            ret = ps_zUP.MoveTo(ref bquit, true);
            if (ret != EM_RES.OK)  goto MSTOP;
            bRput = true;
        MSTOP:
            Stop();
            cyl_up_L.SetOn(ref bquit, 3000);
            ps_zUP.MoveTo(ref bquit, true);
            return ret;
        }
        public static EM_RES m_ToSafe(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            if (!isReady) return EM_RES.QUIT;
            //Z轴回升
            ret = m_ZSafe(ref  bquit);
            if (ret != EM_RES.OK) return ret;
            ret = ps_sf.MoveTo(ref bquit, true);

            return ret;
        }
        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        public static EM_RES WSRun(ref bool bquit)
        {
            try
            {
                EM_RES ret = EM_RES.OK;
                if (!isReady || bOK) return EM_RES.QUIT;
                if (bLput && bRput) return EM_RES.QUIT;
                if (!is_get_L && !bLput)    //左取料
                {
                    ret = m_ActGet_L(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                }
                if (!bAAcmd && !UI.Action.bNullRun)//如果没有收到命令就反复在此等待
                   return EM_RES.WAIT;
                if (!is_get_R && !bRput)//右边取料
                {
                    ret = m_ActGet_R(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                }
                if (!bLput)
                {
                    ret = m_ToPhoto(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                    ret = m_ActPut_L(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                }
                if (!bRput)
                {
                    ret = m_ActPut_R(ref bquit);
                    
                        return ret;
                }
                return EM_RES.ERR;
            }
            catch (Exception ee)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "运行失败", disc, ee.ToString()));
                return EM_RES.ERR;
            }
        }
        public static Task TaskRun;
        public static void task_run()
        {
            if (TaskRun == null || (TaskRun != null && TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建收料线程");
                if (TaskRun != null)
                    TaskRun.Dispose();
                TaskRun = new Task(act_run);
                TaskRun.Start();

            }
        }
        public static EM_RES home(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            if (bquit) return EM_RES.QUIT;
            if (!(TaskRun == null || TaskRun.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 工站工作中，回原失败!", disc));
                return EM_RES.ERR;
            }
            res = cyl_up_L.SetOff(3000);
            if (res != EM_RES.OK) return res;
            res = cyl_up_R.SetOff(3000);
            if (res != EM_RES.OK) return res;
            if (bquit) return EM_RES.QUIT;
            res = ax_z.MoveTo(ref bquit, 99999, 5000);
            if (res != EM_RES.OK && res != EM_RES.PARA_OUTOFRANG) return res;
            if (!ax_z.isELP) return EM_RES.ERR;
            if (bquit) return EM_RES.QUIT;
            res = MT.AxisHome(ref bquit, ax_y, ax_x, ax_a);
            if (res != EM_RES.OK) return res;
            res = MT.AxisHome(ref bquit, ax_z);
            return res;
        }
    }
    #endregion
    #region 转盘
    public  class WSROLL
    {
        public static Cylinder cyl_ck_up = MT.CYL_check_up;
        public static Cylinder cyl_open = MT.CYL_cover_open;
        public static Cylinder cyl_close = MT.CYL_cover_close;
        public static GPIO sen_open = MT.GPIO_IN_code_open;
        public static GPIO sen_closed = MT.GPIO_IN_code_closed;
        public static List<Cylinder> VacumRoll = MT.List_vacu_roll;
        public static GPIO sen_roll_topos = MT.CKPOS_roll_plate_topos;
        public static GPIO sen_home = MT.CKPOS_roll_plate_home_point;
        public static GPIO roll_star = MT.GPIO_OUT_roll_plate;
        public static GPIO roll_topos = MT.CKPOS_roll_plate_topos;     
        public static string disc = "上料工站-";
  
        public static bool bRollOK;
        public static UI.form Mcom = new form();
        public static bool bOtherOK 
        {
            get
            {
                if (sen_closed.isON) return false;

                if (sen_open.isOFF) return false;

                if (ModSta[ck_id].LSta == STA_MOD.UNTEST || ModSta[ck_id].RSta == STA_MOD.UNTEST)
                    return false;
                return true;
            }
        
        }

        /// <summary>
        /// 工站数量
        /// </summary>
        public static int pos_num = 6;

        public struct RO_STA
        {
            public STA_MOD LSta ;
            public STA_MOD RSta ;
        }
        //    public  static List<RO_STA> RollSta = new List<RO_STA>();

        public static int get_id;//当前转盘取料位模组块编号
        //检测位置编号
        public static int ck_id
        {
            get
            {
                return (get_id + 1) % pos_num;
            }
        }
        //AA交换位置
        public static int aa_id
        {
            get
            {

                return (get_id + 2) % pos_num;
            }
        }
        //收料位置编号
        public static int bk_id
        {
            get
            {

                return (get_id + 4) % pos_num;
            }
        }
        //开盖位置
        public static int op_id
        {
            get
            {

                return (get_id + 3) % pos_num;
            }
        }
      
        public enum STA_MOD
        {
            [Description("未知")]
            UNKNOW,
            [Description("空")]
            NULL,
            [Description("开图NG")]
            PPNG,
            [Description("AANG")]
            AANG,
            [Description("OK")]
            OK,
            [Description("待测")]
            UNTEST,
            [Description("错误")]
            ERR,
            [Description("点亮NG")]
            POINTNG
        }
        // public static List<RO_STA> ModSta = new List<RO_STA>();
        public static RO_STA[] ModSta;
        public static void CK_cmd(string data)
        {
            string OKOK = "站号3命令上料产品OKOK";

            if (data.Equals(OKOK))
            {
                ModSta[aa_id].LSta = STA_MOD.OK;
                ModSta[aa_id].RSta = STA_MOD.OK;
            }

            string NGOKAA = "站号3命令上料产品NGOKAA";
            if (data.Equals(NGOKAA))
            {
                ModSta[aa_id].LSta = STA_MOD.AANG;
                ModSta[aa_id].RSta = STA_MOD.OK;
            }
            string OKNGAA = "站号3命令上料产品OKNGAA";
            if (data.Equals(OKNGAA))
            {
                ModSta[aa_id].LSta = STA_MOD.OK;
                ModSta[aa_id].RSta = STA_MOD.AANG;
            }
            string NGNGAAAA = "站号3命令上料产品NGNGAAAA";
            if (data.Equals(NGNGAAAA))
            {
                ModSta[aa_id].LSta = STA_MOD.AANG;
                ModSta[aa_id].RSta = STA_MOD.AANG;
            }
            string NGNGPP = "站号3命令上料产品NGNG开图开图";
            if (data.Equals(NGNGPP))
            {
                ModSta[aa_id].LSta = STA_MOD.PPNG;
                ModSta[aa_id].RSta = STA_MOD.PPNG;
            }
            string STANDY = "站号3命令待机";
            if (data.Equals(STANDY))
            {
                ;
            }
            String STOPSTA = "站号3命令暂停恢复";
            if (data.Equals(STOPSTA))
            {
                
            }
            String CLEAR = "站号3命令清料";
            if (data.Equals(CLEAR))
            {
                VAR.WarnMsg("请手动清料");
                Action.stop(); 
                
            }




        }
        //public enum STA_AA
        //{
        //    [Description("待机")]
        //    STANDAY = "站号3命令待机",
        //    [Description("暂停开始")]
        //    TSTOP = "站号3命令暂停恢复",
        //    [Description("清料")]
        //    RESTAR = "站号3命令清料",
        //    [Description("上料产品OK")]
        //    OK = "站号3命令上料产品OKOK",
        //    [Description("左AANG")]
        //    LAANG = "站号3命令上料产品NGOKAA",
        //    [Description("右AANG")]
        //    RAANG = "站号3命令上料产品OKNGAA",
        //    [Description("AANG")]
        //    AANG = "站号3命令上料产品NGNGAAAA",
        //    [Description("开图NG")]
        //    PPNG = "站号3命令上料产品NGNG开图开图",
        //    [Description("错误")]
        //    ERR,
        //}
        //public static STA_AA Client_Sta;
        /// <summary>
        /// 转盘安全检测
        /// </summary>
        /// <param name="id">委托格式未定义</param>
        /// <returns></returns>
        public static EM_RES ck_roll_safe(int id)
        {
            EM_RES ret = EM_RES.OK;
            if (cyl_ck_up == null || cyl_ck_up.io_sen_off == null)
                return EM_RES.ERR;
            if (cyl_open == null || cyl_open.io_sen_off == null)
                return EM_RES.ERR;
            if (cyl_close == null || cyl_close.io_sen_off == null)
                return EM_RES.ERR;
            if (!cyl_ck_up.io_sen_off.AssertON())
            {
                ret = cyl_ck_up.SetOff(2000);
                if (ret != EM_RES.OK)
                    return ret;
            }
            if (!cyl_open.io_sen_off.AssertON())
            {
                ret = cyl_open.SetOff(2000);
                if (ret != EM_RES.OK)
                    return ret;
            }
            if (!cyl_close.io_sen_off.AssertON())
            {
                ret = cyl_close.SetOff(2000);
                if (ret != EM_RES.OK)
                    return ret;
            }
            //扩展卡读取异常
            if (!(cyl_ck_up.io_sen_off.AssertON() && cyl_open.io_sen_off.AssertON() && cyl_close.io_sen_off.AssertON()))
                //if (! cyl_open.io_sen_off.AssertON() )
                return EM_RES.ERR;
            ret = WSFeed.ck_ax_safe(0);
            if (ret != EM_RES.OK)
                return ret;
            else
                return EM_RES.OK;
        }
        public static EM_RES mopen(ref bool bquit)
        {
            EM_RES ret;
            ret = cyl_open.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            ret = cyl_open.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            ret = sen_open.WaitON(ref bquit);
            if (ret != EM_RES.OK) return ret;
            return ret;
        }
        public static EM_RES mclose(ref bool bquit)
        {
            EM_RES ret;
            ret = cyl_close.SetOn(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            ret = cyl_close.SetOff(ref bquit, 3000);
            if (ret != EM_RES.OK) return ret;
            ret = sen_closed.WaitON(ref bquit);
            if (ret != EM_RES.OK) return ret;
            return ret;
        }
        //点亮测试
        public static EM_RES mPoint(ref bool bquit)
        {
            EM_RES ret;
            status = EM_STA.POINT;
            try
            {
               ret= cyl_ck_up.SetOn(ref bquit, 3000);
               //发送测试命令，等待测试结果
               Mcom.SendData("BBB0401");
                //等待收到消息
               return ret;
            }
              
            
            finally
            {
               ret= cyl_ck_up.SetOff(ref bquit, 3000);              
            }
           

        }
        public static EM_RES mact_roll(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            try
            {
                status = EM_STA.ROLL;
                int i, j;
                if (!isReady) return EM_RES.QUIT;
                res = roll_star.SetOff();
                if (res != EM_RES.OK) return res;
                res = ck_roll_safe(0);
                if (res != EM_RES.OK) return res;
                res = cyl_ck_up.SetOff(ref bquit, 3000);
                if (res != EM_RES.OK) return res;
                res = roll_star.SetOn();
                if (res != EM_RES.OK) return res;
                i = 0; j = 0;
                while (roll_topos.isOFF && isReady)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    i++;
                    if (i > 1000)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "转盘运动等待出位超时", disc, GetStaString));
                        return EM_RES.ERR;
                    }

                }
                while (roll_topos.isON && isReady)
                {

                    Thread.Sleep(5);
                    Application.DoEvents();
                    j++;
                    if (j > 1000)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "转盘运动等待到位超时", disc, GetStaString));
                        return EM_RES.ERR;
                    }

                }

                res = roll_star.SetOff();
                if (res != EM_RES.OK) return res;
                res = VacumRoll[ck_id].SetOn();
                if (res != EM_RES.OK) return res;
                if (!(ModSta[get_id].LSta == STA_MOD.UNTEST && ModSta[get_id].RSta == STA_MOD.UNTEST))
                {
                    res = VacumRoll[get_id].SetOff();
                    if (res != EM_RES.OK) return res;
                }
                res = VacumRoll[op_id].SetOn();
                if (res != EM_RES.OK) return res;
                if (!(ModSta[bk_id].LSta == STA_MOD.UNTEST && ModSta[bk_id].RSta == STA_MOD.UNTEST))
                {
                    res = VacumRoll[bk_id].SetOff();
                    if (res != EM_RES.OK) return res;
                }
                bRollOK = true;
                return EM_RES.OK;
            }
            catch
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "位置错误", disc, GetStaString));
                return EM_RES.ERR;
            }
            finally
            {
                roll_star.SetOff();

            }
        }

        public enum EM_STA
        {
            [Description("未知")]
            UNKNOW,
            [Description("忙")]
            BUSY,
            [Description("回零中")]
            HOME,
            [Description("就绪")]
            READY,
            [Description("转动中")]
            ROLL,
            [Description("点亮中")]
            POINT,
            [Description("错误")]
            ERR,
           [Description("退出")]
            QUIET
        }      
        public static bool isErr;
        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;
                // if (VAR.gsys_set.status != EM_SYS_STA.RUN) return false;
                return true;
            }
        }
        /// <summary>
        /// 获取状态
        /// </summary>
        public static string GetStaString
        {
            get
            {
                
                return status.GetDescription() + "过程";

            }
        }

        /// <summary>
        /// 执行委托动作函数
        /// </summary>
        /// <param name="act">委托动作</param>
        /// <returns></returns>
   
        public static EM_STA status = EM_STA.UNKNOW;
        public static EM_RES mHome(ref bool bquit)
        {
            EM_RES res = EM_RES.OK;
            status = EM_STA.HOME;
            if (!isReady) return EM_RES.QUIT;
            res = ck_roll_safe(0);
            if (res != EM_RES.OK) return res;
            res = cyl_ck_up.SetOff(ref bquit, 3000);
            if (res != EM_RES.OK) return res;
            for (int i = 0; i < 12; i++)
            {
                if (!isReady) return EM_RES.QUIT;
                res = mact_roll(ref bquit);
                if (res != EM_RES.OK) return res;
                Thread.Sleep(10);
                Application.DoEvents();
                if (sen_home.AssertON())
                {
                    
                    return EM_RES.OK;
                }
            }
            status = EM_STA.ERR;
            return EM_RES.ERR;
            //other axis

        }
        public static EM_RES Home(ref bool bquit)
        {
            EM_RES ret = EM_RES.OK;
            ret = mHome( ref bquit);
            return ret;
        }
        public static void act_run()
        {
            try
            {
                if (VAR.gsys_set.bquit) return;
                EM_RES ret = EM_RES.OK;
                ret = WSRun(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, disc + "完成");
                }
                else
                if (VAR.gsys_set.bquit == false && ret == EM_RES.ERR)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    status = EM_STA.ERR;
                }

            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + GetStaString + "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return;
            }

        }
        public static EM_RES WSRun(ref bool bquit)
        {
            try
            {

                EM_RES ret = EM_RES.OK;
                status = EM_STA.ROLL;
                if (!bRollOK)
                {
                    ret = mact_roll(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                }
                else
                if (!bOtherOK)
                {
                    if (sen_closed.isON)
                    {
                        ret = mopen(ref bquit);
                        if (ret != EM_RES.OK) return ret;
                    }
                    if (sen_open.isOFF)
                    {
                        ret = mclose(ref bquit);
                        if (ret != EM_RES.OK) return ret;
                    }
                    if (ModSta[ck_id].LSta == STA_MOD.UNTEST || ModSta[ck_id].RSta == STA_MOD.UNTEST)
                    {
                        ret = mPoint(ref bquit);
                        if (ret != EM_RES.OK) return ret;
                    }

                    return EM_RES.ERR;

                }
                return EM_RES.ERR;
            }
            catch (Exception ee)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}--{1}--" + "运行失败", disc, ee.ToString()));
                return EM_RES.ERR;
            }

        }
        
        public static Task TaskRoll;
        public static void task_star()
        {
            if (TaskRoll == null || (TaskRoll != null && TaskRoll.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建转盘运动线程");
                if (TaskRoll != null)
                    TaskRoll.Dispose();
                TaskRoll = new Task(act_run);
                TaskRoll.Start();
            }
        }


    }
    #endregion
    #region 上料
    public static class UploadModle
    {
        //public static TrayBox traybox_fd = COM.traybox_fd;
        //public static AXIS ax_x = MT.AXIS_UL_X;
        //public static AXIS ax_y = MT.AXIS_UL_Y;
        //public static AXIS ax_z = MT.AXIS_UL_Z;
        //public static AXIS axis_z1 = MT.AXIS_UL_U1;
        //public static AXIS axis_z2 = MT.AXIS_UL_U2;
        //public static readonly object xlock = new object();
        //public enum EM_STA
        //{
        //    [Description("未知")]
        //    UNKNOW,
        //    [Description("忙")]
        //    BUSY,
        //    [Description("回零中")]
        //    HOME,
        //    [Description("就绪")]
        //    READY,
        //    [Description("放料")]
        //    PLACE,
        //    [Description("取料")]
        //    PICK,
        //    [Description("错误")]
        //    ERR
        //}
        //public static EM_STA status = EM_STA.UNKNOW;
        ////取料
        ////飞拍
        ////放料
        ///// <summary>
        ///// 下料模块复位
        ///// </summary>
        ///// <param name="bquit"></param>
        ///// <returns></returns>
        //public static EM_RES Home(ref bool bquit)
        //{
        //    EM_RES res = EM_RES.OK;

        //    if (bquit) return EM_RES.QUIT;

        //    //先抬升
        //    res = MT.AxisHome(ref bquit, ax_z);
        //    if (res != EM_RES.OK) return res;

        //    //确保Z原点感应
        //    if (!ax_z.isORG)
        //    {
        //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未在原点处，有撞机风险!", ax_z.disc));
        //        return EM_RES.ERR;
        //    }

        //    //检查下料模块是否已经抬起
        ////    if (!DownloadModle.isUp) return EM_RES.MOVE_PROTECT;

        //    //other axis
        //    res = MT.AxisHome(ref bquit, ax_x, ax_y, axis_z1, axis_z2, traybox_fd.ax_x);
        //    if (res != EM_RES.OK) return res;

        //    res = MT.AxisHome(ref bquit, traybox_fd.ax_z);
        //    if (res != EM_RES.OK) return res;

        //    return res;
        //}
        ///// <summary>
        ///// 停止轴运动，停止Home动作
        ///// </summary>
        //public static void Stop()
        //{
        //    ax_x.bhomequit = true;
        //    ax_x.Stop();

        //    ax_y.bhomequit = true;
        //    ax_y.Stop();

        //    axis_z1.bhomequit = true;
        //    axis_z1.Stop();

        //    axis_z2.bhomequit = true;
        //    axis_z2.Stop();

        //    traybox_fd.ax_x.bhomequit = true;
        //    traybox_fd.ax_x.Stop();

        //    traybox_fd.ax_z.bhomequit = true;
        //    traybox_fd.ax_z.Stop();
        //}
    }
    #endregion
    #region 下料
    public static class DownloadModle
    {
        //public static TrayBox traybox_ok = COM.traybox_ok;
        //public static TrayBox traybox_ng = COM.traybox_ng;
        //public static List<Cylinder> List_CLD_UD_HD =MT. List_cyl_get;
        //public static List<Cylinder> List_CLD_HD_HD = MT.List_cyl_back;
        //public static Cylinder CLD_DL_ZK_TRAY_OK = MT.CLD_DL_ZK_TRAY_OK;
        //public static AXIS ax_y = MT.AXIS_DL_Y;
        //public static AXIS ax_z = MT.AXIS_DL_Z;
        //public enum EM_STA
        //{
        //    [Description("未知")]
        //    UNKNOW,
        //    [Description("忙")]
        //    BUSY,
        //    [Description("回零中")]
        //    HOME,
        //    [Description("就绪")]
        //    READY,
        //    [Description("放料")]
        //    PLACE,
        //    [Description("取料")]
        //    PICK,
        //    [Description("错误")]
        //    ERR
        //}
        //public static EM_STA status = EM_STA.UNKNOW;
        ////取料
        ////放料
        ////检查是否抬起
        //public static bool isUp
        //{
        //    get
        //    {
        //        //check Cylinder
        //        foreach (Cylinder cy in List_CLD_UD_HD)
        //        {
        //            if (cy.isOFFByChkSen)
        //            {
        //                Thread.Sleep(300);
        //                if (cy.isOFFByChkSen)
        //                {
        //                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 气缸未抬起!", cy.io_out.disc));
        //                    return false;
        //                }
        //            }
        //        }

        //        //确保Z原点感应
        //        if (!ax_z.isORG)
        //        {
        //            Thread.Sleep(300);
        //            if (!ax_z.isORG)
        //            {
        //                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未在原点处，有撞机风险!", ax_z.disc));
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //}
        ///// <summary>
        ///// 下料模块复位
        ///// </summary>
        ///// <param name="bquit"></param>
        ///// <returns></returns>
        //public static EM_RES Home(ref bool bquit)
        //{
        //    EM_RES res = EM_RES.OK;

        //    if (bquit) return EM_RES.QUIT;

        //    //确保上料X轴已复位，且位置安全
        //    if (UploadModle.ax_x.home_status != AXIS.HOME_STA.OK)
        //    {
        //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未复位，有撞机风险!", UploadModle.ax_x.disc));
        //        return EM_RES.ERR;
        //    }
        //    if (Math.Abs(UploadModle.ax_x.fenc_pos) > 10)
        //    {
        //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未在原点附近(<10),X={1:F3}，有撞机风险!", UploadModle.ax_x.disc, UploadModle.ax_x.fenc_pos));
        //        return EM_RES.ERR;
        //    }

        //    //气缸抬升
        //    foreach (Cylinder cy in List_CLD_UD_HD) cy.SetOff();

        //    //先抬升
        //    res = MT.AxisHome(ref bquit, ax_z);
        //    if (res != EM_RES.OK) return res;

        //    //检查是否已经抬起
        //    if (!isUp) return EM_RES.MOVE_PROTECT;

        //    //Y复位
        //    res = MT.AxisHome(ref bquit, ax_y, traybox_ok.ax_x, traybox_ng.ax_x);
        //    if (res != EM_RES.OK) return res;


        //    res = MT.AxisHome(ref bquit, traybox_ok.ax_z, traybox_ng.ax_z);
        //    if (res != EM_RES.OK) return res;

        //    return EM_RES.OK;
        //}
        ///// <summary>
        ///// 停止轴运动，停止Home动作
        ///// </summary>
        //public static void Stop()
        //{
        //    ax_z.bhomequit = true;
        //    ax_z.Stop();

        //    ax_y.bhomequit = true;
        //    ax_y.Stop();

        //    traybox_ok.ax_x.bhomequit = true;
        //    traybox_ok.ax_x.Stop();

        //    traybox_ok.ax_z.bhomequit = true;
        //    traybox_ok.ax_z.Stop();

        //    traybox_ng.ax_x.bhomequit = true;
        //    traybox_ng.ax_x.Stop();

        //    traybox_ng.ax_z.bhomequit = true;
        //    traybox_ng.ax_z.Stop();
        //}
    }
    #endregion
    #endregion
    #region 基本动作
    public static class Action
    {
        public static bool isReady
        {
            get
            {
                if (VAR.gsys_set.bquit) return false;             
                return true;
            }
        }
        static Task run_task = null;
       static Task show_task = null;
        /// <summary>
        /// 空料运行
        /// </summary>
       public static bool bNullRun;
        public static EM_RES DoAct(Mact Act)  //执行委托
        {
            EM_RES ret;
            try
            {
                if (VAR.gsys_set.bquit) return EM_RES.QUIT;
                ret = Act(ref VAR.gsys_set.bquit);
                if (ret == EM_RES.OK)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS,   "成功");
                   
                    return ret;
                }
                //不成功的话检测退出变量
                if (VAR.gsys_set.bquit == false)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "异常!");
                    for (int n = 0; n < 30; n++)
                    {
                        VAR.gsys_set.bquit = true;
                        Thread.Sleep(10);
                        Application.DoEvents();
                    }
                    
                    return EM_RES.ERR;
                }
               
                return EM_RES.QUIT;
            }
            catch (Exception e)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "异常" + e.ToString());
                VAR.gsys_set.bquit = true;
                return EM_RES.ERR;
            }

        }
        #region 弹窗报警
        public static string ErrMsg
        {
            set
            {
                if (isReady)
                    mMsg = value;
            }
            get
            {

                if (mMsg == "")
                    return "未定义错误！设备停止";
                return mMsg;
            }

        }
        static string mMsg;
        public static MsgShow EShow;//错误显示委托
        public static MsgShow WShow;//警告显示委托

        public static void ErrShowTask()
        {
            if (show_task == null || (show_task != null && show_task.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "显示线程");
                if (show_task != null)
                    show_task.Dispose();
                show_task = new Task(ActErrShow);
                show_task.Start();
                Thread.Sleep(100);
                Application.DoEvents();

            }
        }
        public static void ActErrShow()
        {
            EShow = new UI.MsgShow(ErrShow);
            EShow(ErrMsg);

        }
        public static bool task_isOUT(params Task[] mtask)
        {
            for (int i = 0; i < 5; i++)//检测五遍
            {
                foreach (Task tk in mtask)
                {
                    if (!(tk == null || tk.IsCompleted))

                    { return false; }
                }
            }
            return true;
        }
        public static void ErrShow(string msg = "")
        {
            //设备暂停，蜂鸣开始
            //   VAR.gsys_set.beep_en = true;

            if (msg == "")
                msg = "未定义错误！设备停止";
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}", msg));
            warning fr_warning = new warning();//错误窗体
            fr_warning.TopMost = true;
            fr_warning.BackColor = Color.Red;
            fr_warning.lb_msg.Text = msg;
            fr_warning.ShowDialog();
        }
        public static void MsgShow(string msg = "")
        {
            //设备暂停，蜂鸣开始
            //   VAR.gsys_set.beep_en = true;
            if (msg == "")
                msg = "未定义操作提示";
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SAVE_WAR, string.Format("{0}", msg));
            warning fr_warning = new warning();//错误窗体
            fr_warning.TopMost = true;
            fr_warning.BackColor = Color.Yellow;
            fr_warning.lb_msg.Text = msg;
            fr_warning.ShowDialog();

        }
        public static DialogResult WarningShow(string msg = "", bool EnCancel = true)
        {
            //设备暂停，蜂鸣开始
            VAR.gsys_set.beep_en = true;
            if (msg == "")
                msg = "未定义警告！";
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.WAR, string.Format("{0}", msg));
            warning fr_warning = new warning();//警告窗体
            if (EnCancel)
                fr_warning.btn_cancle.Visible = true;
            fr_warning.TopMost = true;
            fr_warning.BackColor = Color.Yellow;
            fr_warning.lb_msg.Text = msg;
            fr_warning.ShowDialog();
            VAR.gsys_set.beep_en = false;
            return fr_warning.DialogResult;
        }
        public static EM_RES WarnShow(EM_ALM_STA type = EM_ALM_STA.WAR_YELLOW, string str = "")
        {
            DialogResult res = DialogResult.OK;
            if (!isReady) return EM_RES.QUIT;
            //设备暂停，蜂鸣开始
            VAR.gsys_set.beep_en = true;
            EM_RES ret = EM_RES.OK;
            switch (type)
            {
                case EM_ALM_STA.WAR_YELLOW:
                    res = Action.WarningShow(str + "异常,是否停止？?");
                    if (res == DialogResult.Cancel)
                        ret = EM_RES.OK;
                    else
                        ret = EM_RES.ERR;
                    break;
                case EM_ALM_STA.WAR_RED:
                    Action.ErrShow(str + "设备停止");
                    ret = EM_RES.ERR;
                    break;
                case EM_ALM_STA.NOR_BLUE:
                    Action.MsgShow(str + "提示");
                    ret = EM_RES.OK;
                    break;
                case EM_ALM_STA.NOR_GREEN:
                    ret = EM_RES.OK;
                    break;
                default:
                    ret = EM_RES.OK;
                    break;
            }
            VAR.gsys_set.beep_en = false;
            if (ret == EM_RES.ERR)
                VAR.gsys_set.status = EM_SYS_STA.ERR;
            return ret;
        }
        #endregion
        #region 运行
        /// <summary>
        /// 产品更换数据初始化更新
        /// </summary>
        /// <returns></returns>
       public  static EM_RES Update_pro()
        {
            VAR.gsys_set.SaveSysCfg();//产品名称保存 
            EM_RES ret = MT.PosInit();//位置数据加载初始化
            if (ret != EM_RES.OK) return ret;                     
            ret = COM.product.LoadDat(VAR.gsys_set.cur_product_name);//料盘加载数据
            if (ret != EM_RES.OK) return ret;
            ret = COM.MVS.LoadInf();//视觉数据加载
            if (ret != EM_RES.OK) return ret;
            //加载产品列表
            bool bOK=  COM.product.LoadProductList();
            if (!bOK) return EM_RES.ERR;
            return ret;
        }

        public static void th_run()
        {
            if (run_task == null || (run_task != null && run_task.IsCompleted))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "创建运行线程");
                if (run_task != null)
                    run_task.Dispose();
                run_task = new Task(run_th);
                run_task.Start();

                MT.GPIO_OUT_belet.SetOff();//停止皮带
            }
        }
        public static void run_get()
        {
            EM_RES ret = EM_RES.OK;
            VAR.gsys_set.bquit = false;
            bool bhomeerr = false;

            while (true)
            {
                //if ((StationGet.status == StationGet.EM_STA.UNKNOW) && (!bhomeerr))
                //{
                //   StationGet. ErrShow("请先取料复位");
                //    bhomeerr = true;

                //}


            }
        }
        public static void run_th()
        {
            int n;
            EM_RES ret;
            bool bsafe = false;//安全门检测
            bool bkeystart = false;//按键开始
            bool bkeystop = false;//按键停止
            bool bemg = false;//急停按键

            int t_temp = System.Environment.TickCount;//系统毫秒时间
            int tmr_wl = System.Environment.TickCount;//计时
            bool brun = true;//运行控制

            bool bnew = false;//未定义
            bool bstandy = false;//就绪进入
            bool bHome = true;
            int standby_cnt;//就绪次数

            //初始化运行条件
       
            VAR.gsys_set.bquit = false;     
            VAR.gsys_set.status = EM_SYS_STA.RUN;
            COUNT_DATA.ct_pause = 0;
            COUNT_DATA.tmr_wl = 0;
            MT.GPIO_OUT_light.SetOn();//开灯
            //检测所有轴已经回原
            //gy0114
            //foreach (AXIS ax in MT.AxList_ALL)
            //{
            //    if (ax.home_status != AXIS.HOME_STA.OK)
            //        bHome = false;
            //}

            //if (!bHome)
            //{
            //    VAR.sys_inf.Set(EM_ALM_STA.NOR_BLUE, "请复位", -1);
            //    return;
            //}
            //检测所有线程已经退出
            //停止所有线程
            while ( VAR.gsys_set.bquit == false)
            {
                Thread.Sleep(10);
                Application.DoEvents();
                if (!brun) goto RUN_STAGE;
                    VAR.gsys_set.status = EM_SYS_STA.RUN;
                    VAR.sys_inf.Set(EM_ALM_STA.NOR_BLUE, "正在运行", -1);
                    tmr_wl = System.Environment.TickCount;
                    if (!WSROLL.bRollOK && (WSROLL.TaskRoll == null || WSROLL.TaskRoll.IsCompleted))
                    {
                        WSROLL.task_star();
                        WSBack.bOK = false;
                        WSFeed.bOK = false;
                        WSGet.bOK = false;
                        Thread.Sleep(500);
                        Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    else
                    {

                        if (!WSGet.bOK && WSGet.TaskRun == null || WSGet.TaskRun.IsCompleted)
                            WSGet.task_run();
                        Thread.Sleep(500);
                        Thread.Sleep(500);
                        Application.DoEvents();
                        if (!WSBack.bOK && WSBack.TaskRun == null || WSBack.TaskRun.IsCompleted)
                            WSBack.task_run();
                        if (!WSFeed.bOK && WSFeed.TaskRun == null || WSFeed.TaskRun.IsCompleted)
                            WSFeed.task_run();
                        if (WSGet.bOK && WSBack.bOK && WSFeed.bOK)
                            WSROLL.bRollOK = false;
                        Thread.Sleep(500);
                        Thread.Sleep(500);
                        Application.DoEvents();
                    }
              

                #region 状态更新
                if ((WsBuFD.TaskRun == null || WsBuFD.TaskRun.IsCompleted) && (WsBuBK.TaskRun == null || WsBuBK.TaskRun.IsCompleted))
                {

                }
                #endregion
                #region  按键处理

            //if (MT.GPIO_IN_key_start.AssertON())
            //{
            //    if (bkeystart == false)
            //    {
            //        //MT.GPIO_OUT_KL_RESET.SetOff();
            //        bkeystart = true;
            //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "开始按键按下");
            //        if (MT.isSafeSen)
            //        {

                //            //TD.Task_Start();
            //            //非就绪，暂停 则继续，不能从新开始运行
            //            if (VAR.gsys_set.status != EM_SYS_STA.STANDBY && VAR.gsys_set.status != EM_SYS_STA.PAUSE) continue;
            //            if (bnew == false && VAR.gsys_set.status != EM_SYS_STA.RUN && VAR.gsys_set.status != EM_SYS_STA.PAUSE)
            //            {
            //                //初始化速度等
            //                MT.SetAllAxToWorkSpd();
            //                VAR.gsys_set.bpause = false;
            //                brun = true;
            //            }
            //            else if (VAR.gsys_set.status == EM_SYS_STA.PAUSE)
            //            {
            //                //初始化速度等
            //                MT.SetAllAxToWorkSpd();
            //                //暂停时，重新启动
            //                VAR.gsys_set.bpause = false;
            //                //等待线程处理异常
            //                Thread.Sleep(500);

                //                //如果已经退出，则重新启动
            //                //TD.Task_Start();
            //            }
            //        }
            //    }
            //}
            //else bkeystart = false;
            //if (false)
            //{
            //    if (bkeypause == false)
            //    {
            //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "暂停按键按下");
            //        bpause_kl = true;
            //        bkeypause = true;
            //        VAR.sys_inf.Set(CONST.EM_ALM_STA.NOR_BLUE, "暂停", 100, true);
            //        VAR.gsys_set.bpause = true;
            //    }
            //}
            //else bkeypause = false;

                //if (MT.GPIO_IN_emg_key2.AssertON())
            //{
            //    if (bkeystop == false)
            //    {
            //        bkeystop = true;
            //        //光栅保护
            //        if (!MT.isSafeSen) continue;

                //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "停止按键按下");
            //        if (VAR.gsys_set.status != EM_SYS_STA.RUN)
            //        {
            //            VAR.sys_inf.Set(EM_ALM_STA.NOR_GREEN, "就绪", 100, true);
            //            MT.GPIO_OUT_KL_STOP.SetOn();
            //        }
            //        else
            //        {
            //            VAR.gsys_set.bquit = true;
            //            //TD.Task_Stop();
            //        }
            //    }
            //}
            //else bkeystop = false;

                //if (!MT.isSafeSen)
            //{
            //    if (bsafe == false) VAR.msg.AddMsg(Msg.EM_MSGTYPE.WAR, "安全光栅/门锁触发1");
            //    bsafe = true;
            //    if (VAR.gsys_set.status == EM_SYS_STA.RUN)
            //    {
            //        VAR.sys_inf.Set(EM_ALM_STA.WAR_YELLOW_FLASH, "安全防护");
            //        VAR.gsys_set.bpause = true;
            //    }
            //}
            //else bsafe = false;            
            //if (MT.GPIO_IN_EMG.AssertOFF())
            //{
            //    VAR.gsys_set.status = EM_SYS_STA.EMG;
            //    VAR.gsys_set.bpause = true;
            //    VAR.gsys_set.bquit = true;
            //    brun = false;
            //    VAR.sys_inf.Set(EM_ALM_STA.WAR_RED_FLASH, "急停");
            //    if (bemg == false) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "急停按键按下");
            //    bemg = true;
            //}
            //else bemg = false;
                #endregion
            RUN_STAGE:
                Thread.Sleep(500);
                
            }

            //int t = Environment.TickCount;          
            //t = Environment.TickCount - t;
            //VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "U T=" + t.ToString());
            Thread.Sleep(100);
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "运行线程结束");
        }

        #endregion
        #region 保存读取
        //public static EM_RES LoadCfg(string filePath, string Section, string Ident, string Default)
        //{
        //    //产品参数
        //    if(filePath.Length<3)
        //        filePath = string.Format("{0}\\syscfg\\syscfg.ini", Path.GetFullPath(".."));
        //    else
        //        filePath = string.Format("{0}+filePath", Path.GetFullPath(".."));
        //    if (!File.Exists(filePath))
        //    {
        //        File.Create(filePath);
        //    }
        //    IniFile inf = new IniFile(filePath);
        //    string section = string.Format("RUNcfg");
        //  //   inf.ReadString(section, Ident, 1);
        //    return EM_RES.OK;
        //}

        public static EM_RES SavCfg()
        {
     
            //产品参数
            string filename = string.Format("{0}\\syscfg\\syscfg.ini", Path.GetFullPath(".."));

            if (!File.Exists(filename))
            {
                File.Create(filename);
                //   VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                // return EM_RES.PARA_ERR;
            }

            IniFile inf = new IniFile(filename);
            string section = string.Format("RUNcfg");
           // inf.WriteInteger(section, "TARY_ROW", row);
            return EM_RES.OK;
        }
        #endregion
        #region 停止
        public static void stop()
        {                      
            for (int n = 0; n < 50; n++)
            {
                VAR.gsys_set.bquit = true;
                Thread.Sleep(10);
                Application.DoEvents();
            }
            
            if (VAR.gsys_set.status == EM_SYS_STA.EMG || VAR.gsys_set.status == EM_SYS_STA.ERR || VAR.gsys_set.status == EM_SYS_STA.UNKOWN)
            {
                MT.AllAxStop();                
            }
            if (VAR.gsys_set.status != EM_SYS_STA.EMG && VAR.gsys_set.status != EM_SYS_STA.ERR && VAR.gsys_set.status != EM_SYS_STA.UNKOWN)
                    VAR.gsys_set.status = EM_SYS_STA.STANDBY;
            
        }
        #endregion
        #region 暂停
        //public static bool pause(ref EM_SYS_STA status, ref bool bquit, bool bquit2 = false)
        //{
        //    bool bpause = false;
        //    //if (VAR.gsys_set.bpause == true || MT.GPIO_IN_FR_DOOR.isOFF)
        //    //{
        //    //    bpause = true;
        //    //    VAR.gsys_set.bpause = true;
        //    //    if (VAR.gsys_set.status != EM_SYS_STA.PAUSE) MT.Beeper(100);
        //    //}

        //    //while ((VAR.gsys_set.mode & EM_SYS_MODE.STEP) == EM_SYS_MODE.STEP && VAR.gsys_set.status == EM_SYS_STA.RUN || (VAR.gsys_set.bpause == true || MT.GPIO_IN_FR_DOOR.isOFF))
        //    //{
        //        //if (VAR.gsys_set.bpause == true || MT.GPIO_IN_FR_DOOR.isOFF)
        //        //{
        //        //    status = EM_SYS_STA.PAUSE;
        //        //    VAR.gsys_set.status = EM_SYS_STA.PAUSE;
        //        //}
        //        ////继续运行
        //        //if (MT.GPIO_IN_key_start.AssertON())
        //        //{
        //        //    VAR.sys_inf.Set(EM_ALM_STA.NOR_GREEN, "运行", 0, true);
        //        //    break;
        //        //}
        //        ////复位键退出
        //        //if (MT.GPIO_IN_KEY_STOP.AssertON())
        //        //{
        //        //    VAR.sys_inf.Set(EM_ALM_STA.NOR_GREEN, "运行", 0, true);
        //        //    //if (!VAR.isStepMode)
        //        //    bquit = true;
        //        //    break;
        //        //}
        //    //    if (bquit || bquit2) break;

        //    //    //发生错误
        //    //    if (VAR.gsys_set.status == EM_SYS_STA.EMG || VAR.gsys_set.status == EM_SYS_STA.ERR || VAR.gsys_set.status == EM_SYS_STA.UNKOWN)
        //    //    {
        //    //        bquit = true;
        //    //        break;
        //    //    }
        //    //    Thread.Sleep(10);
        //    //    Application.DoEvents();
        //    //}
        //    //检查系统状态
        //    //if (VAR.gsys_set.status == EM_SYS_STA.RUN || VAR.gsys_set.status == EM_SYS_STA.PAUSE)
        //    //{
        //    //    status = EM_SYS_STA.RUN;
        //    //    VAR.gsys_set.status = EM_SYS_STA.RUN;
        //    //    VAR.sys_inf.Set(EM_ALM_STA.NOR_GREEN, "运行", 0, true);
        //    //}
        //    //else
        //    //{
        //    //    //bquit = true;
        //    //}

        //    //return bpause;
        //}
        #endregion
        #region 退出关闭软件
        public static EM_RES close()
        {
            //stop
            stop();
           
            Thread.Sleep(100);
            Application.DoEvents();

            for (int n = 0; n < 100; n++)
            {
                VAR.gsys_set.bquit = true;
         
              
                Thread.Sleep(10);
                Application.DoEvents();
            }

            VAR.gsys_set.bquit = false;
            MT.Close();
            ////close vison
            //CogFrameGrabbers FG_List = new CogFrameGrabbers();
            //if (FG_List.Count > 0)
            //{
            //    for (int i = 0; i < FG_List.Count; i++)
            //    {
            //        FG_List[i].Disconnect(true);
            //    }
            //}
            return EM_RES.OK;
        }
        #endregion       
    }
    #endregion
    #region 按键HOOK钩子
    public class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13; //键盘 

        private const int WM_KEYDOWN = 0x100;//KEYDOWN

        private const int WM_KEYUP = 0x101;  //KEYUP

        private const int WM_SYSKEYDOWN = 0x104; //SYSKEYDOWN

        private const int WM_SYSKEYUP = 0x105;  //SYSKEYUP




        public event KeyEventHandler KeyDownEvent;
        public event KeyPressEventHandler KeyPressEvent;
        public event KeyEventHandler KeyUpEvent;

        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        static int hKeyboardHook = 0; //声明键盘钩子处理的初始值
        //值在Microsoft SDK的Winuser.h里查询
        HookProc KeyboardHookProcedure; //声明KeyboardHookProcedure作为HookProc类型
        //键盘结构 
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;  //定一个虚拟键码。该代码必须有一个价值的范围1至254
            public int scanCode; // 指定的硬件扫描码的关键
            public int flags;  // 键标志
            public int time; // 指定的时间戳记的这个讯息
            public int dwExtraInfo; // 指定额外信息相关的信息
        }
        //使用此功能，安装了一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);


        //调用此函数卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);


        //使用此功能，通过信息钩子继续下一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        // 取得当前线程编号（线程钩子需要用到） 
        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();

        //使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public void Start()
        {
            // 安装键盘钩子
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
                //hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                //************************************ 
                //键盘线程钩子 
                //SetWindowsHookEx( 2,KeyboardHookProcedure, IntPtr.Zero, GetCurrentThreadId());//指定要监听的线程idGetCurrentThreadId(),
                //键盘全局钩子,需要引用空间(using System.Reflection;) 
                //SetWindowsHookEx( 13,MouseHookProcedure,Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),0); 
                // 
                //关于SetWindowsHookEx (int idHook, HookProc lpfn, IntPtr hInstance, int threadId)函数将钩子加入到钩子链表中，说明一下四个参数： 
                //idHook 钩子类型，即确定钩子监听何种消息，上面的代码中设为2，即监听键盘消息并且是线程钩子，如果是全局钩子监听键盘消息应设为13， 
                //线程钩子监听鼠标消息设为7，全局钩子监听鼠标消息设为14。lpfn 钩子子程的地址指针。如果dwThreadId参数为0 或是一个由别的进程创建的 
                //线程的标识，lpfn必须指向DLL中的钩子子程。 除此以外，lpfn可以指向当前进程的一段钩子子程代码。钩子函数的入口地址，当钩子钩到任何 
                //消息后便调用这个函数。hInstance应用程序实例的句柄。标识包含lpfn所指的子程的DLL。如果threadId 标识当前进程创建的一个线程，而且子 
                //程代码位于当前进程，hInstance必须为NULL。可以很简单的设定其为本应用程序的实例句柄。threaded 与安装的钩子子程相关联的线程的标识符
                //如果为0，钩子子程与所有的线程关联，即为全局钩子
                //************************************ 
                //如果SetWindowsHookEx失败
                if (hKeyboardHook == 0)
                {
                    Stop();
                    throw new Exception("安装键盘钩子失败");
                }
            }
        }
        public void Stop()
        {
            bool retKeyboard = true;


            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }

            if (!(retKeyboard)) throw new Exception("卸载钩子失败！");
        }
        //ToAscii职能的转换指定的虚拟键码和键盘状态的相应字符或字符
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, //[in] 指定虚拟关键代码进行翻译。 
                                         int uScanCode, // [in] 指定的硬件扫描码的关键须翻译成英文。高阶位的这个值设定的关键，如果是（不压）
                                         byte[] lpbKeyState, // [in] 指针，以256字节数组，包含当前键盘的状态。每个元素（字节）的数组包含状态的一个关键。如果高阶位的字节是一套，关键是下跌（按下）。在低比特，如果设置表明，关键是对切换。在此功能，只有肘位的CAPS LOCK键是相关的。在切换状态的NUM个锁和滚动锁定键被忽略。
                                         byte[] lpwTransKey, // [out] 指针的缓冲区收到翻译字符或字符。 
                                         int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 

        //获取按键的状态
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // 侦听键盘事件
            if ((nCode >= 0) && (KeyDownEvent != null || KeyUpEvent != null || KeyPressEvent != null))
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                // raise KeyDown
                if (KeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDownEvent.Invoke(this, e);
                }

                //键盘按下
                if (KeyPressEvent != null && wParam == WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);

                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer, MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        KeyPressEvent.Invoke(this, e);
                    }
                }

                // 键盘抬起 
                if (KeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUpEvent.Invoke(this, e);
                }

            }
            //如果返回1，则结束消息，这个消息到此为止，不再传递。
            //如果返回0或调用CallNextHookEx函数则消息出了这个钩子继续往下传递，也就是传给消息真正的接受者 
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
       
        }
        ~KeyboardHook()
        {
            Stop();
        }
    }
    public class KeyboardTest
    {
        private KeyEventHandler myKeyEventHandeler = null;//按键钩子
        private KeyboardHook k_hook = new KeyboardHook();


        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            //  这里写具体实现
            MessageBox.Show("按下按键" + e.KeyValue);
        }
        public void startListen()
        {
            myKeyEventHandeler = new KeyEventHandler(hook_KeyDown);
            k_hook.KeyDownEvent += myKeyEventHandeler;//钩住键按下
            k_hook.Start();//安装键盘钩子
        }
        public void stopListen()
        {
            if (myKeyEventHandeler != null)
            {
                k_hook.KeyDownEvent -= myKeyEventHandeler;//取消按键事件
                myKeyEventHandeler = null;
                k_hook.Stop();//关闭键盘钩子
            }
        }


    }
    #endregion
    #region 统计数据
    public static class COUNT_DATA
    {
        public static bool bct_page_start = false;  //单张贴补时间计时开始
        public static int cnt_page_ttl;     //总贴补张数
        public static int cnt_pcs_ttl;      //总贴补次数
        public static int cnt_page;        //今日贴补张数
        public static int cnt_pcs;         //今日贴补次数
        public static int cnt_pcs_pl;      //今日抛料次数

        public static int ct_fdh;           //机械手进料用时
        public static int ct_fly;           //飞拍用时
        public static int ct_mark;          //目标定位用时
        public static int ct_tf;            //贴附用时
        public static int ct_pcs;           //单次贴补时间
        public static int ct_page;          //单张贴补时间 
        public static int ct_pause;         //单张暂停时间 
        public static int cnt_per_page;     //单张贴补次数

        public static int cnt_vs_ng;       //视觉识别失败次数 
        public static int cnt_fd_ng;       //供料失败次数    
        public static int cnt_tf_ng;       //贴付失败次数          

        public static int runtime;         //运行时间sec
        public static int waittime;        //运行时间sec
        public static int tmr_wl;          //涡轮空闲计时
        public static int tmr_no_op;       //空闲计时
        public static DateTime dt;         //日期

        private static int cnt_pcs_temp;
        private static int timer_temp;

        #region  加载
        public static void LoadDat(string productname)
        {
            string fileroad = Path.GetFullPath("..") + "\\product\\" + productname + "\\cfg.inf";
            IniFile inf = new IniFile(fileroad);

            cnt_pcs_ttl = inf.ReadInteger("CNT", "CNT_PCS_TTL", cnt_pcs_ttl);
            cnt_page_ttl = inf.ReadInteger("CNT", "CNT_PAGE_TTL", cnt_page_ttl);

            cnt_pcs = inf.ReadInteger("CNT", "CNT_PCS", cnt_pcs);
            cnt_page = inf.ReadInteger("CNT", "CNT_PAGE", cnt_page);

            cnt_pcs_pl = inf.ReadInteger("CNT", "CNT_PCS_PL", cnt_pcs_pl);

            ct_pcs = inf.ReadInteger("CNT", "CT_PCS", ct_pcs);
            ct_page = inf.ReadInteger("CNT", "CT_PAGE", ct_page);
            runtime = inf.ReadInteger("CNT", "RUNTIME", runtime);
            waittime = inf.ReadInteger("CNT", "WAITTIME", waittime);
            dt = Convert.ToDateTime(inf.ReadString("CNT", "DT", dt.ToString()));

            cnt_pcs_temp = cnt_pcs;
        }
        #endregion
        #region  保存
        public static void SaveDat(string productname, bool bsave = false)
        {
            if (!bsave && (cnt_pcs_temp == cnt_pcs) && (runtime - timer_temp) < 60) return;
            cnt_pcs_temp = cnt_pcs;
            timer_temp = runtime;

            string fileroad = Path.GetFullPath("..") + "\\product\\" + productname + "\\cfg.inf";
            IniFile inf = new IniFile(fileroad);

            inf.WriteInteger("CNT", "CNT_PCS_TTL", cnt_pcs_ttl);
            inf.WriteInteger("CNT", "CNT_PAGE_TTL", cnt_page_ttl);

            inf.WriteInteger("CNT", "CNT_PCS", cnt_pcs);
            inf.WriteInteger("CNT", "CNT_PAGE", cnt_page);

            inf.WriteInteger("CNT", "CNT_PCS_PL", cnt_pcs_pl);

            inf.WriteInteger("CNT", "CT_PCS", ct_pcs);
            inf.WriteInteger("CNT", "CT_PAGE", ct_page);
            inf.WriteInteger("CNT", "RUNTIME", runtime);
            inf.WriteInteger("CNT", "WAITTIME", waittime);
            inf.WriteString("CNT", "DT", dt.ToString());
        }
        #endregion
        #region 清零
        public static void Clear()
        {
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}pcs,{1}张，抛料{2}，运行时间{3:0.0}h,待机时间{4:0.0}h", cnt_pcs, cnt_page, cnt_pcs_pl, (double)(runtime / 3600), (double)(waittime / 3600)));
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("上次清零时间:{0}", dt.ToString()));
            cnt_page = 0;
            cnt_pcs = 0;
            cnt_pcs_pl = 0;
            cnt_vs_ng = 0;
            cnt_fd_ng = 0;
            cnt_tf_ng = 0;
            runtime = 0;
            waittime = 0;
            dt = System.DateTime.Now;
        }

        #endregion
    }
    #endregion
    #region 通讯
    public static class COMM
    {
        //网口通讯连接

        public static bool bCnnect;//连接标志
        public  static string mip = "";
        public  static int mport ;
        public static int isServe;//是服务器还是客户端
        public static EM_RES Client_conn(SocketHelper.AxTcpClient qxTcpClient, string ip = "", int port =0)
        {
            if (!qxTcpClient.IsStartTcpthreading)
            {
                try
                {
                    qxTcpClient.ReConectedCount = 0;
                    qxTcpClient.ServerIp = ip;
                    qxTcpClient.ServerPort = port;
                    qxTcpClient.StartConnection();
                    System.Threading.Thread.Sleep(50);
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, "连接 " + qxTcpClient.ServerIp + ": " + qxTcpClient.ServerPort + "...!");
                }
                catch (Exception)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "连接 " + qxTcpClient.ServerIp + ": " + qxTcpClient.ServerPort + "...失败!");
                    return EM_RES.ERR;
                }
            }
            return EM_RES.OK;
        }
        public static void Client_send(SocketHelper.AxTcpClient qxTcpClient, string cmd)
        {
            if (qxTcpClient == null) return;
            qxTcpClient.SendCommand(cmd);
        }
        public static void Client_stop(SocketHelper.AxTcpClient qxTcpClient, string cmd)
        {
            if (qxTcpClient == null) return;
            qxTcpClient.StopConnection();
        }

        public static void server_send(SocketHelper.AxTcpServer qxTcpClient, string cmd)
        {
            if (qxTcpClient == null) return;
            LoadCfg();
            string client_ip = mip;
            int client_port = mport;
            string data = cmd;

            qxTcpClient.SendData(client_ip, client_port, data);

        }
        //其他状态，收到数据等在控件事件中，使用方便

        #region 保存读取
        public static EM_RES LoadCfg()
        {
            //产品参数
            string filename = string.Format("{0}\\syscfg\\syscfg.ini", Path.GetFullPath(".."));
            if (!File.Exists(filename))
            {
                File.Create(filename);
                //VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                //return EM_RES.PARA_ERR;
            }
            IniFile inf = new IniFile(filename);
            string section = string.Format("COMMcfg");
            mip = inf.ReadString(section, "IP", mip.ToString());
            mport = inf.ReadInteger(section, "PORT", 1);
            isServe = inf.ReadInteger(section, "isServe", 1);
            return EM_RES.OK;
        }

        public static EM_RES SavCfg()
        {

            //产品参数
            string filename = string.Format("{0}\\syscfg\\syscfg.ini", Path.GetFullPath(".."));

            if (!File.Exists(filename))
            {
                File.Create(filename);
                //   VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                // return EM_RES.PARA_ERR;
            }

            IniFile inf = new IniFile(filename);
            string section = string.Format("COMMcfg");
  
            inf.WriteString(section, "IP", mip);
            inf.WriteInteger(section, "PORT", mport);
            inf.WriteInteger(section, "isServe", isServe);
            return EM_RES.OK;
        }
        #endregion



        //}
    }


    #endregion
    #region 视觉
  
    public  class VS
    {
        //网口通讯连接
        public static bool bCam;//有无相机
        public  CAMERA CamGet;//拍照获取
  
        public struct ST_SET
        {
         
            //相机1 模板信息

            public ST_XYZ L_mode_modu_pos;   // 左工位—模组           
            public ST_XYZ R_mode_modu_pos;  // 右工位—模组
            //相机2模板
            public ST_XYZ Feed_mode_pos;    // 左工位—连接

            //相机1像素比例
            public ST_XYZ modu_x_scale;     //左工位 模组-x轴
            public ST_XYZ modu_y_scale;      //左工位模组-y轴
            //相机2像素比例
            public ST_XYZ Feed_x_scale;     //左工位连接-x轴
            public ST_XYZ Feed_y_scale;     //左工位连接-y轴
            // 通用旋转中心
            public ST_XYZ L_mouth1_center;         //吸嘴旋转中心，视觉
            public ST_XYZ L_mouth2_center;

            public ST_XYZ Feed_mouth_center;
            public double Ang_min;
            public double Ang_max;
        }
        public static ST_SET st_set;
        //List<ST_XYZ> list_xyz = new List<ST_XYZ> { st_set.Feed_mode_pos, st_set.Feed_mouth_center,
        //st_set.Feed_x_scale,  st_set.Feed_y_scale,  st_set.L_mode_modu_pos,   st_set.L_mouth1_center,
        //st_set.modu_x_scale,  st_set.modu_y_scale,  st_set.R_mode_modu_pos ,st_set.L_mouth2_center
        //};

        public EM_RES m_camera_action(int photo_id, out ST_XYZ m_result)
        {
            try
            {
               
                ST_XYZ m_camera_get;
              
                String camera_name;
                short m_camera_id;    // 四个相机编号
                ST_XYZ m_mouth_home;    ////在拍照位置吸嘴旋转中心位置
                ST_XYZ m_x_cli, m_y_cli;  //旋转偏移计算xy modu相机视觉比例           
                ST_XYZ m_mod_pos;         //  模板位置  或者连接板或者模组模板
                int i;
                m_result.x = 0;
                m_result.y = 0;
                m_result.z = 0;
                if(UI.Action.bNullRun)  //空跑直接成功            
                    return EM_RES.OK;                              
                switch (photo_id)
                {
                    case 1:
                        m_camera_id = 1;
                        camera_name = "左工位mod1拍照";
                        m_mouth_home = st_set.L_mouth1_center;
                        m_x_cli = st_set.modu_x_scale;
                        m_y_cli = st_set.modu_y_scale;
                        m_mod_pos = st_set.L_mode_modu_pos;
                        break;
                    case 2:
                        m_camera_id = 1;
                        camera_name = "左工位mod2拍照";
                        m_mouth_home = st_set.L_mouth2_center;
                        m_x_cli = st_set.modu_x_scale;
                        m_y_cli = st_set.modu_y_scale;
                        m_mod_pos = st_set.R_mode_modu_pos;
                        break;
                    case 3:
                        m_camera_id = 2;
                        camera_name = "上料夹爪拍照";
                        m_mouth_home = st_set.Feed_mouth_center;
                        m_x_cli = st_set.Feed_x_scale;
                        m_y_cli = st_set.Feed_y_scale;
                        m_mod_pos = st_set.Feed_mode_pos;
                        break;
                    default:
                        return EM_RES.ERR;
                }
                EM_RES ret;
                double d_a, d_x, d_y;
                double x_1;     //如果模组拍照 m_camera_modu= m_camera_get，连接板拍照 m_camera_modu等于与之扣合的模组数据
                double y_1;
                if (Action.bNullRun)
                    return EM_RES.OK;
                ret = get_vs(out m_camera_get, m_camera_id);  
                if (ret != EM_RES.OK) return ret;
                //判断角度合格
                x_1 = m_camera_get.x;
                y_1 = m_camera_get.y;

                double x_0 = m_mouth_home.x; //旋转中心，吸嘴位置
                double y_0 = m_mouth_home.y;

                d_a = -(m_camera_get.z - m_mod_pos.z);
                d_a = d_a * 3.1415926 / 180.0;

                d_x = (x_1 - x_0) * Math.Cos(d_a) - (y_1 - y_0) * Math.Sin(d_a) + x_0 - x_1;
                d_y = (x_1 - x_0) * Math.Sin(d_a) + (y_1 - y_0) * Math.Cos(d_a) + y_0 - y_1;
                double x_a = m_x_cli.x;
                double y_a = m_x_cli.y;
                double x_b = m_y_cli.x;
                double y_b = m_y_cli.y;

                double length_x1 = (d_x * y_b - d_y * x_b) / (x_a * y_b - x_b * y_a);
                double length_y1 = (d_x * y_a - d_y * x_a) / (x_b * y_a - x_a * y_b);
                double d_x2;
                double d_y2;
                d_x2 = m_mod_pos.x - x_1;
                d_y2 = m_mod_pos.y - y_1;

                double length_x2 = (d_x2 * y_b - d_y2 * x_b) / (x_a * y_b - x_b * y_a);
                double length_y2 = (d_x2 * y_a - d_y2 * x_a) / (x_b * y_a - x_a * y_b);
                double length_a = d_a;
                //  弧度变成角度
                length_a = length_a / 3.1415926 * 180.0;

                m_result.x = length_x1 + length_x2;
                m_result.y = length_y1 + length_y2;
                m_result.z = length_a;
                return EM_RES.OK;
            }
            catch(Exception e)
            {
                VAR.ErrMsg(e.ToString());
                m_result.x = 0;
                m_result.y = 0;
                m_result.z = 0;
                return EM_RES.ERR;
            }
        }
        public EM_RES get_vs(out ST_XYZ mres , int camera_id = 1,     int sch_id = 0)
        {
            EM_RES ret = EM_RES.OK;
            if (CamGet == null)
            {
                VAR.ErrMsg("拍照未定义");
                mres.x = 0;
                mres.y = 0;
                mres.z = 0;
                return EM_RES.ERR;
            }
            ret = CamGet(out mres, camera_id);   //0是非标定参数
            return ret;
        }
        #region 加载与保存参数
        EM_RES LoadInfXYZ(out ST_XYZ mxyz, string section, string filename = "")
        {
            try
            {

                if (filename.Length < 3)
                    filename = Path.GetFullPath("..") + "\\product\\"+ VAR.gsys_set.cur_product_name+"\\vscfg.ini";

                IniFile inf = new IniFile(filename);
                string Section = string.Format(section);
                mxyz.x = inf.ReadDouble(Section, "x", 0);
                mxyz.y = inf.ReadDouble(Section, "y", 0);
                mxyz.z = inf.ReadDouble(Section, "z", 0);
            }
            catch
            {
                //  VAR.msg.AddMsg();
                mxyz.x = 0;
                mxyz.y = 0;
                mxyz.z = 0;
                return EM_RES.ERR;
            }
            return EM_RES.OK;
        }
        EM_RES SaveInfXYZ(ST_XYZ mxyz, string section, string filename = "")
        {
            try
            {
                if (filename.Length < 3) 
                filename = Path.GetFullPath("..") + "\\product\\" + VAR.gsys_set.cur_product_name + "\\vscfg.ini";
                if (!File.Exists(filename))
                    File.Create(filename);
                IniFile inf = new IniFile(filename);
                string Section = string.Format(section);
                inf.WriteDouble(Section, "x", mxyz.x);
                inf.WriteDouble(Section, "y", mxyz.y);
                inf.WriteDouble(Section, "z", mxyz.z);
                return EM_RES.OK;
            }
            catch
            {
                return EM_RES.ERR;
            }
        }
        public EM_RES LoadInf()
        {
            EM_RES ret = EM_RES.OK;

            ret = LoadInfXYZ(out st_set.Feed_mode_pos, "Feed_mode_pos");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.Feed_mouth_center, "Feed_mouth_center");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.Feed_x_scale, "Feed_x_scale");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.Feed_y_scale, "Feed_y_scale");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.L_mode_modu_pos, "L_mode_modu_pos");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.L_mouth1_center, "L_mouth1_center");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.L_mouth2_center, "L_mouth2_center");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.modu_x_scale, "modu_x_scale");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.modu_y_scale, "modu_y_scale");
            if (ret != EM_RES.OK) return ret;
            ret = LoadInfXYZ(out st_set.R_mode_modu_pos, "R_mode_modu_pos");
            if (ret != EM_RES.OK) return ret;
            return ret;
        }
        public EM_RES SaveInf()
        {
            EM_RES ret = EM_RES.OK;
            ret = SaveInfXYZ(st_set.Feed_mode_pos, "Feed_mode_pos");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.Feed_mouth_center, "Feed_mouth_center");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.Feed_x_scale, "Feed_x_scale");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.Feed_y_scale, "Feed_y_scale");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.L_mode_modu_pos, "L_mode_modu_pos");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.L_mouth1_center, "L_mouth1_center");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.L_mouth2_center, "L_mouth2_center");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.modu_x_scale, "modu_x_scale");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.modu_y_scale, "modu_y_scale");
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(st_set.R_mode_modu_pos, "R_mode_modu_pos");
            if (ret != EM_RES.OK) return ret;
            return ret;
        }
        /// <summary>
        /// 拍照获取模板数据
        /// </summary>
        /// <param name="mxyz"></param>模板
        /// <param name="section"></param>模板名字
        /// <returns></returns>
        public EM_RES save_mode_pos(ST_XYZ mxyz, int cam_id = 2, string section = "Feed_mode_pos")
        {
            EM_RES ret = EM_RES.OK;
            ret = get_vs(out mxyz, cam_id);
            if (ret != EM_RES.OK) return ret;
            ret = SaveInfXYZ(mxyz, "section");
            if (ret != EM_RES.OK) return ret;
            return ret;
        }

        public EM_RES cam_get_scle(ref bool bquit, int cam_id = 1)
        {
            EM_RES ret=EM_RES.OK;
            double scale_length=2;//偏移距离2mm
            ST_XYZ start_pos,xmove_pos,ymove_pos;
            if(cam_id==1)
            {
                ret = WSGet.ps_pho_L.MoveTo(ref  bquit,true);
                ret = get_vs(out start_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSGet.ps_pho_L.AxisX.MoveTo(ref bquit, WSGet.ps_pho_L.AxisX.fcmd_pos + scale_length,3000);
                if (ret != EM_RES.OK) return ret; 
                ret = get_vs(out xmove_pos, cam_id);
                if (ret != EM_RES.OK) return ret; 
                st_set.modu_x_scale.x = (xmove_pos.x - start_pos.x) / scale_length;
                st_set.modu_x_scale.y = (xmove_pos.y - start_pos.y) / scale_length;
              
                ret = WSGet.ps_pho_L.MoveTo(ref  bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out start_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSGet.ps_pho_L.AxisY.MoveTo(ref bquit, WSGet.ps_pho_L.AxisY.fcmd_pos + scale_length, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out ymove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                st_set.modu_y_scale.x = (ymove_pos.x - start_pos.x) / scale_length;
                st_set.modu_y_scale.y = (ymove_pos.y - start_pos.y) / scale_length;             
            }
            else
            {
                ret = WSFeed.ps_photo_L.MoveTo(ref  bquit, true);       
                ret = get_vs(out start_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSFeed.ps_photo_L.AxisX.MoveTo(ref bquit, WSGet.ps_pho_L.AxisX.fcmd_pos + scale_length, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out xmove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                st_set.Feed_x_scale.x = (xmove_pos.x - start_pos.x) / scale_length;
                st_set.Feed_x_scale.y = (xmove_pos.y - start_pos.y) / scale_length;

                ret = WSFeed.ps_photo_L.MoveTo(ref  bquit, true);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out start_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSFeed.ps_photo_L.AxisY.MoveTo(ref bquit, WSGet.ps_pho_L.AxisY.fcmd_pos + scale_length, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out ymove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                st_set.Feed_y_scale.x = (ymove_pos.x - start_pos.x) / scale_length;
                st_set.Feed_y_scale.y = (ymove_pos.y - start_pos.y) / scale_length;  
            }
            SaveInf();
            return ret;
        }
        public EM_RES cam_get_center(ref bool bquit, int cam_id = 1)
        {
            EM_RES ret = EM_RES.OK;
            double scale_angle = 2;//偏移距离2度
            ST_XYZ start_pos, xmove_pos, ymove_pos;
            if (cam_id == 1)
            {
                ret = WSGet.ps_pho_L.MoveTo(ref  bquit, true);
                ret = get_vs(out start_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSGet.ps_pho_L.AxisA.MoveTo(ref bquit, WSGet.ps_pho_L.AxisA.fcmd_pos + scale_angle, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out xmove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSGet.ps_pho_L.AxisA.MoveTo(ref bquit, WSGet.ps_pho_L.AxisA.fcmd_pos + scale_angle, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out ymove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;

                double mpos_u, mpos_v, mpos_m, mpos_k;
                mpos_u = (start_pos.x * start_pos.x - xmove_pos.x * xmove_pos.x + start_pos.y * start_pos.y - xmove_pos.y * xmove_pos.y) / (2 * start_pos.x - 2 * xmove_pos.x);
                mpos_m = (start_pos.y - xmove_pos.y) / (start_pos.x - xmove_pos.x);
                mpos_v = (start_pos.x * start_pos.x - ymove_pos.x * ymove_pos.x + start_pos.y * start_pos.y - ymove_pos.y * ymove_pos.y) / (2 * start_pos.x - 2 * ymove_pos.x);
                mpos_k = (start_pos.y - ymove_pos.y) / (start_pos.x - ymove_pos.x);
                st_set.L_mouth1_center.x = (mpos_u - mpos_v) / (mpos_m - mpos_k);
                st_set.L_mouth1_center.y = mpos_v - (mpos_u - mpos_v) * mpos_k / (mpos_m - mpos_k);

            }
            else
            {

                ret = WSFeed.ps_photo_L.MoveTo(ref  bquit, true);
                ret = get_vs(out start_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSFeed.ps_photo_L.AxisA.MoveTo(ref bquit, WSFeed.ps_photo_L.AxisA.fcmd_pos + scale_angle, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out xmove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                ret = WSFeed.ps_photo_L.AxisA.MoveTo(ref bquit, WSFeed.ps_photo_L.AxisA.fcmd_pos + scale_angle, 3000);
                if (ret != EM_RES.OK) return ret;
                ret = get_vs(out ymove_pos, cam_id);
                if (ret != EM_RES.OK) return ret;
                //三点确定圆心

                double mpos_u, mpos_v, mpos_m, mpos_k;
                mpos_u = (start_pos.x * start_pos.x - xmove_pos.x * xmove_pos.x + start_pos.y * start_pos.y - xmove_pos.y * xmove_pos.y) / (2 * start_pos.x - 2 * xmove_pos.x);
                mpos_m = (start_pos.y - xmove_pos.y) / (start_pos.x - xmove_pos.x);
                mpos_v = (start_pos.x * start_pos.x - ymove_pos.x * ymove_pos.x + start_pos.y * start_pos.y - ymove_pos.y * ymove_pos.y) / (2 * start_pos.x - 2 * ymove_pos.x);
                mpos_k = (start_pos.y - ymove_pos.y) / (start_pos.x - ymove_pos.x);
                st_set.Feed_mouth_center.x = (mpos_u - mpos_v) / (mpos_m - mpos_k);
                st_set.Feed_mouth_center.y = mpos_v - (mpos_u - mpos_v) * mpos_k / (mpos_m - mpos_k);
            }
            SaveInf();
            return ret;
          
        }
        #endregion

    }

   
    #endregion

}
