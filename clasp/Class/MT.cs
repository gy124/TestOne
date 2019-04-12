using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionCtrl;

namespace clasp
{
    public static class MT
    {
        #region 板卡定义
        public static CARD CARD_ECI2400_0 = new CARD(0, "192.168.0.100", 4, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2400", "转台");
        public static CARD CARD_ECI2400_1 = new CARD(1, "192.168.0.101", 4, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2400", "左光箱");
        public static CARD CARD_ECI2400_2 = new CARD(2, "192.168.0.102", 4, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2400", "右光箱");
        public static CARD CARD_ECI2600_3 = new CARD(3, "192.168.0.103", 6, 24, 8, CARD.BRAND.ZMOTION, CARD.TYPE.MOTION, "ECI2600", "下料");
        public static CARD CARD_ECI0064_4 = new CARD(4, "192.168.0.104", 0, 32, 32, CARD.BRAND.ZMOTION, CARD.TYPE.IO, "ECI0064", "下料");
        public static CARD CARD_DMC3800_5 = new CARD(5, 0, 8, 16, 16, CARD.BRAND.LEADSHINE, CARD.TYPE.MOTION, "DMC3800", "上料");
        public static CARD CARD_ORIENTAL485_6 = new CARD(6, "COM3", 115200, CARD.BRAND.ORIENTALMOTOR, CARD.TYPE.MOTION, "ORIENTAL", "工装平台");
       // public static List<CARD> CardList = new List<CARD> { CARD_ECI2400_0, CARD_ECI2400_1, CARD_ECI2400_2, CARD_ECI2600_3, CARD_ECI0064_4, CARD_DMC3800_5, CARD_ORIENTAL485_6 };
        public static List<CARD> CardList = new List<CARD> {  CARD_DMC3800_5 };
        //public static List<CARD> CardList = new List<CARD> { CARD_ECI2600_3, CARD_ECI0064_4, CARD_DMC3800_5 };
        #endregion
        #region 轴定义 
        //工位1
        public static AXIS AXIS_WS1_F = new AXIS(1, CARD_ORIENTAL485_6, "工站1前排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS1_B = new AXIS(2, CARD_ORIENTAL485_6, "工站1后排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS1_U = new AXIS(3, CARD_ORIENTAL485_6, "工站1旋转", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //工位1
        public static AXIS AXIS_WS2_F = new AXIS(4, CARD_ORIENTAL485_6, "工站2前排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS2_B = new AXIS(5, CARD_ORIENTAL485_6, "工站2后排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS2_U = new AXIS(6, CARD_ORIENTAL485_6, "工站2旋转", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //工位1
        public static AXIS AXIS_WS3_F = new AXIS(7, CARD_ORIENTAL485_6, "工站3前排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS3_B = new AXIS(8, CARD_ORIENTAL485_6, "工站3后排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS3_U = new AXIS(9, CARD_ORIENTAL485_6, "工站3旋转", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //工位1
        public static AXIS AXIS_WS4_F = new AXIS(10, CARD_ORIENTAL485_6, "工站4前排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS4_B = new AXIS(11, CARD_ORIENTAL485_6, "工站4后排", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_WS4_U = new AXIS(12, CARD_ORIENTAL485_6, "工站4旋转", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        //OTP光箱
        public static AXIS AXIS_BOX_OTP_Z = new AXIS(0, CARD_ECI2400_0, "OTP_Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);

        //上料
        public static AXIS AXIS_UL_X = new AXIS(0, CARD_DMC3800_5, "上料X", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_UL_Y = new AXIS(1, CARD_DMC3800_5, "上料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_UL_Z = new AXIS(2, CARD_DMC3800_5, "上料Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_UL_U2 = new AXIS(3, CARD_DMC3800_5, "上料U2", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_UL_U1 = new AXIS(4, CARD_DMC3800_5, "上料U1", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_UL_FD_X = new AXIS(5, CARD_DMC3800_5, "供料X", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_UL_FD_Z = new AXIS(6, CARD_DMC3800_5, "供料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);

        //下料
        public static AXIS AXIS_DL_Y = new AXIS(0, CARD_ECI2600_3, "下料Y", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_DL_Z = new AXIS(1, CARD_ECI2600_3, "下料Z", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_DL_OK_X = new AXIS(2, CARD_ECI2600_3, "OK料X", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_DL_OK_Z = new AXIS(3, CARD_ECI2600_3, "OK料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_DL_NG_X = new AXIS(4, CARD_ECI2600_3, "NG料X", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_DL_NG_Z = new AXIS(5, CARD_ECI2600_3, "NG料Z", AXIS.MT_TYPE.STEP, AXIS.ENC_TYPE.NO, GPIO.IO_STA.OUT_ON);

        //左光箱
        public static AXIS AXIS_BOX_L_X1 = new AXIS(0, CARD_ECI2400_1, "左光箱X1", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BOX_L_X2 = new AXIS(1, CARD_ECI2400_1, "左光箱X2", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BOX_L_Z1 = new AXIS(2, CARD_ECI2400_1, "左光箱Z1", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BOX_L_Z2 = new AXIS(3, CARD_ECI2400_1, "左光箱Z2", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);

        //右光箱
        public static AXIS AXIS_BOX_R_X1 = new AXIS(0, CARD_ECI2400_2, "右光箱X1", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BOX_R_X2 = new AXIS(1, CARD_ECI2400_2, "右光箱X2", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BOX_R_Z1 = new AXIS(2, CARD_ECI2400_2, "右光箱Z1", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);
        public static AXIS AXIS_BOX_R_Z2 = new AXIS(3, CARD_ECI2400_2, "右光箱Z2", AXIS.MT_TYPE.SEVER, AXIS.ENC_TYPE.YES, GPIO.IO_STA.OUT_ON);

        //工站
        public static List<AXIS> AxList_WS = new List<AXIS> { AXIS_WS1_F, AXIS_WS1_B, AXIS_WS1_U, AXIS_WS2_F, AXIS_WS2_B, AXIS_WS2_U, AXIS_WS3_F, AXIS_WS3_B, AXIS_WS3_U, AXIS_WS4_F, AXIS_WS4_B, AXIS_WS4_U };
        public static List<AXIS> AxList_WS1 = new List<AXIS> { AXIS_WS1_F, AXIS_WS1_B, AXIS_WS1_U };
        public static List<AXIS> AxList_WS2 = new List<AXIS> { AXIS_WS2_F, AXIS_WS2_B, AXIS_WS2_U };
        public static List<AXIS> AxList_WS3 = new List<AXIS> { AXIS_WS3_F, AXIS_WS3_B, AXIS_WS3_U };
        public static List<AXIS> AxList_WS4 = new List<AXIS> { AXIS_WS4_F, AXIS_WS4_B, AXIS_WS4_U };
        //光箱
        public static List<AXIS> AxList_BOX_OPT = new List<AXIS> { AXIS_BOX_OTP_Z };
        public static List<AXIS> AxList_BOX_LEFT = new List<AXIS> { AXIS_BOX_L_X1, AXIS_BOX_L_X2, AXIS_BOX_L_Z1, AXIS_BOX_L_Z2 };
        public static List<AXIS> AxList_BOX_RIGHT = new List<AXIS> { AXIS_BOX_R_X1, AXIS_BOX_R_X2, AXIS_BOX_R_Z1, AXIS_BOX_R_Z2 };
        //上料
        public static List<AXIS> AxList_UL = new List<AXIS> { AXIS_UL_X, AXIS_UL_Y, AXIS_UL_Z, AXIS_UL_U1, AXIS_UL_U2, AXIS_UL_FD_X, AXIS_UL_FD_Z };
        //下料
        public static List<AXIS> AxList_DL = new List<AXIS> { AXIS_DL_Y, AXIS_DL_Z, AXIS_DL_OK_X, AXIS_DL_OK_Z, AXIS_DL_NG_X, AXIS_DL_NG_Z };

        #endregion
        #region IO 定义
        public static GPIO GPIO_OUT_ON_NULL = new GPIO(0, (CARD)null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "OUT_ON_NULL", GPIO.IO_STA.OUT_ON);
        public static GPIO GPIO_IN_ON_NULL = new GPIO(0, (CARD)null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "IN_ON_NULL", GPIO.IO_STA.IN_ON);
        public static GPIO GPIO_OUT_OFF_NULL = new GPIO(0, (CARD)null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "OUT_OFF_NULL", GPIO.IO_STA.OUT_OFF);
        public static GPIO GPIO_IN_OFF_NULL = new GPIO(0, (CARD)null, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.NULL, "IN_OFF_NULL", GPIO.IO_STA.IN_OFF);
        #region OUT  
        //上料
        public static GPIO GPIO_OUT_UL_Z_RESET = new GPIO(0, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料Z轴复位");
        public static GPIO GPIO_OUT_UL_ZK_N1 = new GPIO(5, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料吸头真空1");
        public static GPIO GPIO_OUT_UL_ZK_N2 = new GPIO(4, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料吸头真空2");

        public static GPIO GPIO_OUT_UL_PZK_N1 = new GPIO(7, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料吸头破真空1");
        public static GPIO GPIO_OUT_UL_PZK_N2 = new GPIO(6, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "上料吸头破真空2");

        public static GPIO GPIO_OUT_UL_FD_TRAY = new GPIO(1, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "供料料盘夹紧");

        //按键
        public static GPIO GPIO_OUT_KL_START = new GPIO(0, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "开始按键灯");
        public static GPIO GPIO_OUT_KL_STOP = new GPIO(1, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "停止按键灯");
        public static GPIO GPIO_OUT_KL_RESET = new GPIO(2, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "复位按键灯");
        //警报
        public static GPIO GPIO_OUT_ALM_RED = new GPIO(8, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "红色塔灯");
        public static GPIO GPIO_OUT_ALM_YELLOW = new GPIO(9, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "黄色塔灯");
        public static GPIO GPIO_OUT_ALM_GREEN = new GPIO(10, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "绿色塔灯");
        public static GPIO GPIO_OUT_ALM_BEEPER = new GPIO(8, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "蜂鸣器");
        //相机
        public static GPIO GPIO_OUT_UL_CAM_FR = new GPIO(13, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "相机2触发");
        public static GPIO GPIO_OUT_UL_CAM_DW = new GPIO(15, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "下相机触发");
        public static GPIO GPIO_OUT_UL_CAM_BK = new GPIO(14, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "相机1触发");

        //下料
        public static GPIO GPIO_OUT_DL_OK_TRAY = new GPIO(2, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "OK料盘夹紧");
        public static GPIO GPIO_OUT_DL_NG_TRAY = new GPIO(3, CARD_DMC3800_5, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "NG料盘夹紧");
        //夹爪
        public static GPIO GPIO_OUT_DL_HD_HD1 = new GPIO(0, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪1");
        public static GPIO GPIO_OUT_DL_HD_HD2 = new GPIO(1, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪2");
        public static GPIO GPIO_OUT_DL_HD_HD3 = new GPIO(2, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪3");
        public static GPIO GPIO_OUT_DL_HD_HD4 = new GPIO(3, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪4");
        public static GPIO GPIO_OUT_DL_HD_HD5 = new GPIO(4, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪5");
        public static GPIO GPIO_OUT_DL_HD_HD6 = new GPIO(5, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪6");
        public static GPIO GPIO_OUT_DL_HD_HD7 = new GPIO(6, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪7");
        public static GPIO GPIO_OUT_DL_HD_HD8 = new GPIO(7, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪8");
        public static GPIO GPIO_OUT_DL_HD_HD9 = new GPIO(8, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪9");
        public static GPIO GPIO_OUT_DL_HD_HD10 = new GPIO(9, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪10");
        public static GPIO GPIO_OUT_DL_HD_HD11 = new GPIO(10, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪11");
        public static GPIO GPIO_OUT_DL_HD_HD12 = new GPIO(11, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪12");
        public static GPIO GPIO_OUT_DL_HD_HD13 = new GPIO(12, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪13");
        public static GPIO GPIO_OUT_DL_HD_HD14 = new GPIO(13, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪14");
        public static GPIO GPIO_OUT_DL_HD_HD15 = new GPIO(14, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪15");
        public static GPIO GPIO_OUT_DL_HD_HD16 = new GPIO(15, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪16");
        //夹爪上下
        public static GPIO GPIO_OUT_DL_UD_HD1 = new GPIO(16, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下1");
        public static GPIO GPIO_OUT_DL_UD_HD2 = new GPIO(17, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下2");
        public static GPIO GPIO_OUT_DL_UD_HD3 = new GPIO(18, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下3");
        public static GPIO GPIO_OUT_DL_UD_HD4 = new GPIO(19, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下4");
        public static GPIO GPIO_OUT_DL_UD_HD5 = new GPIO(20, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下5");
        public static GPIO GPIO_OUT_DL_UD_HD6 = new GPIO(21, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下6");
        public static GPIO GPIO_OUT_DL_UD_HD7 = new GPIO(22, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下7");
        public static GPIO GPIO_OUT_DL_UD_HD8 = new GPIO(23, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下8");
        public static GPIO GPIO_OUT_DL_UD_HD9 = new GPIO(24, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下9");
        public static GPIO GPIO_OUT_DL_UD_HD10 = new GPIO(25, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下10");
        public static GPIO GPIO_OUT_DL_UD_HD11 = new GPIO(26, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下11");
        public static GPIO GPIO_OUT_DL_UD_HD12 = new GPIO(27, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下12");
        public static GPIO GPIO_OUT_DL_UD_HD13 = new GPIO(28, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下13");
        public static GPIO GPIO_OUT_DL_UD_HD14 = new GPIO(29, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下14");
        public static GPIO GPIO_OUT_DL_UD_HD15 = new GPIO(30, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下15");
        public static GPIO GPIO_OUT_DL_UD_HD16 = new GPIO(31, CARD_ECI0064_4, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "下料夹爪上下16");
        //料盘
        //public static GPIO GPIO_OUT_DL_ZK_NG_TRAY = new GPIO(0, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "NG料盘真空");
        //public static GPIO GPIO_OUT_DL_ZK_OK_TRAY = new GPIO(1, CARD_ECI2600_3, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.IO_CARD, "OK料盘真空");

        //转台
        public static GPIO GPIO_OUT_TT_FWD = new GPIO(0, CARD_ECI2400_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转台顺时针(FWD)信号");
        public static GPIO GPIO_OUT_TT_REV = new GPIO(1, CARD_ECI2400_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转台逆时针(REV)信号");
        public static GPIO GPIO_OUT_TT_RESET = new GPIO(2, CARD_ECI2400_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转台复位(X1)信号");
        public static GPIO GPIO_OUT_TT_STOP = new GPIO(3, CARD_ECI2400_0, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "转台停止(X3)信号");

        //工站压盖WS1
        public static GPIO GPIO_OUT_WS1_OPEN_FR1 = new GPIO(0, AXIS_WS1_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR2 = new GPIO(1, AXIS_WS1_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR3 = new GPIO(2, AXIS_WS1_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR4 = new GPIO(3, AXIS_WS1_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR5 = new GPIO(4, AXIS_WS1_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR6 = new GPIO(5, AXIS_WS1_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR7 = new GPIO(0, AXIS_WS1_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_FR8 = new GPIO(1, AXIS_WS1_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1前排压盖8", GPIO.IO_STA.NULL, true);

        public static GPIO GPIO_OUT_WS1_OPEN_BK1 = new GPIO(0, AXIS_WS1_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK2 = new GPIO(1, AXIS_WS1_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK3 = new GPIO(2, AXIS_WS1_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK4 = new GPIO(3, AXIS_WS1_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK5 = new GPIO(4, AXIS_WS1_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK6 = new GPIO(5, AXIS_WS1_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK7 = new GPIO(2, AXIS_WS1_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS1_OPEN_BK8 = new GPIO(3, AXIS_WS1_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站1后排压盖8", GPIO.IO_STA.NULL, true);
        //工站压盖WS2
        public static GPIO GPIO_OUT_WS2_OPEN_FR1 = new GPIO(0, AXIS_WS2_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR2 = new GPIO(1, AXIS_WS2_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR3 = new GPIO(2, AXIS_WS2_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR4 = new GPIO(3, AXIS_WS2_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR5 = new GPIO(4, AXIS_WS2_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR6 = new GPIO(5, AXIS_WS2_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR7 = new GPIO(0, AXIS_WS2_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_FR8 = new GPIO(1, AXIS_WS2_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2前排压盖8", GPIO.IO_STA.NULL, true);

        public static GPIO GPIO_OUT_WS2_OPEN_BK1 = new GPIO(0, AXIS_WS2_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK2 = new GPIO(1, AXIS_WS2_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK3 = new GPIO(2, AXIS_WS2_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK4 = new GPIO(3, AXIS_WS2_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK5 = new GPIO(4, AXIS_WS2_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK6 = new GPIO(5, AXIS_WS2_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK7 = new GPIO(2, AXIS_WS2_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS2_OPEN_BK8 = new GPIO(3, AXIS_WS2_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站2后排压盖8", GPIO.IO_STA.NULL, true);

        //工站压盖WS3
        public static GPIO GPIO_OUT_WS3_OPEN_FR1 = new GPIO(0, AXIS_WS3_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR2 = new GPIO(1, AXIS_WS3_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR3 = new GPIO(2, AXIS_WS3_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR4 = new GPIO(3, AXIS_WS3_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR5 = new GPIO(4, AXIS_WS3_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR6 = new GPIO(5, AXIS_WS3_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR7 = new GPIO(0, AXIS_WS3_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_FR8 = new GPIO(1, AXIS_WS3_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3前排压盖8", GPIO.IO_STA.NULL, true);

        public static GPIO GPIO_OUT_WS3_OPEN_BK1 = new GPIO(0, AXIS_WS3_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK2 = new GPIO(1, AXIS_WS3_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK3 = new GPIO(2, AXIS_WS3_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK4 = new GPIO(3, AXIS_WS3_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK5 = new GPIO(4, AXIS_WS3_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK6 = new GPIO(5, AXIS_WS3_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK7 = new GPIO(2, AXIS_WS3_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS3_OPEN_BK8 = new GPIO(3, AXIS_WS3_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站3后排压盖8", GPIO.IO_STA.NULL, true);

        //工站压盖WS4
        public static GPIO GPIO_OUT_WS4_OPEN_FR1 = new GPIO(0, AXIS_WS4_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR2 = new GPIO(1, AXIS_WS4_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR3 = new GPIO(2, AXIS_WS4_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR4 = new GPIO(3, AXIS_WS4_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR5 = new GPIO(4, AXIS_WS4_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR6 = new GPIO(5, AXIS_WS4_F, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR7 = new GPIO(0, AXIS_WS4_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_FR8 = new GPIO(1, AXIS_WS4_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4前排压盖8", GPIO.IO_STA.NULL, true);

        public static GPIO GPIO_OUT_WS4_OPEN_BK1 = new GPIO(0, AXIS_WS4_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖1", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK2 = new GPIO(1, AXIS_WS4_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖2", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK3 = new GPIO(2, AXIS_WS4_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖3", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK4 = new GPIO(3, AXIS_WS4_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖4", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK5 = new GPIO(4, AXIS_WS4_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖5", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK6 = new GPIO(5, AXIS_WS4_B, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖6", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK7 = new GPIO(2, AXIS_WS4_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖7", GPIO.IO_STA.NULL, true);
        public static GPIO GPIO_OUT_WS4_OPEN_BK8 = new GPIO(3, AXIS_WS4_U, GPIO.IO_DIR.OUT, GPIO.IO_TYPE.MT_CARD, "工站4后排压盖8", GPIO.IO_STA.NULL, true);
        #endregion
        #region IN
        //急停
        public static GPIO GPIO_IN_EMG0 = new GPIO(0, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停键(转台)");
        public static GPIO GPIO_IN_EMG1 = new GPIO(0, CARD_ECI2400_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停键(左光箱)");
        public static GPIO GPIO_IN_EMG2 = new GPIO(0, CARD_ECI2400_2, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停键(右光箱)");
        public static GPIO GPIO_IN_EMG3 = new GPIO(0, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停键(下料)");
        public static GPIO GPIO_IN_EMG5 = new GPIO(0, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "急停键（上料）");
        //上料
        public static GPIO GPIO_IN_UL_Z_RSTOK = new GPIO(1, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料Z轴复位完成");
        public static GPIO GPIO_IN_UL_ZK_N1 = new GPIO(11, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料吸头真空感应1");
        public static GPIO GPIO_IN_UL_ZK_N2 = new GPIO(12, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上料吸头真空感应2");
        //public static GPIO GPIO_IN_UL_INP_FD_TRAYBOX = new GPIO(13,CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料仓在位感应");
        //public static GPIO GPIO_IN_UL_RDY_FD_TRAY = new GPIO(13,CARD_ECI2600_3 , GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料仓有料感应");
        public static GPIO GPIO_IN_UL_FD_TRAY = new GPIO(6, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料盘夹紧感应");
        //按键
        public static GPIO GPIO_IN_KEY_START = new GPIO(2, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "开始键");
        public static GPIO GPIO_IN_KEY_STOP = new GPIO(4, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "停止键");
        public static GPIO GPIO_IN_KEY_RESET = new GPIO(3, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "复位键");
        //安全门        
        //public static GPIO GPIO_IN_BK_DOOR = new GPIO(10, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "后门感应");

        //下料
        public static GPIO GPIO_IN_DL_OK_TRAY = new GPIO(8, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料盘夹紧感应");
        public static GPIO GPIO_IN_DL_NG_TRAY = new GPIO(10, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料盘夹紧感应");
        //夹爪夹位感应
        public static GPIO GPIO_IN_DL_HD_HD1 = new GPIO(0, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应1");
        public static GPIO GPIO_IN_DL_HD_HD2 = new GPIO(1, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应2");
        public static GPIO GPIO_IN_DL_HD_HD3 = new GPIO(2, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应3");
        public static GPIO GPIO_IN_DL_HD_HD4 = new GPIO(3, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应4");
        public static GPIO GPIO_IN_DL_HD_HD5 = new GPIO(4, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应5");
        public static GPIO GPIO_IN_DL_HD_HD6 = new GPIO(5, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应6");
        public static GPIO GPIO_IN_DL_HD_HD7 = new GPIO(6, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应7");
        public static GPIO GPIO_IN_DL_HD_HD8 = new GPIO(7, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应8");
        public static GPIO GPIO_IN_DL_HD_HD9 = new GPIO(8, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应9");
        public static GPIO GPIO_IN_DL_HD_HD10 = new GPIO(9, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应10");
        public static GPIO GPIO_IN_DL_HD_HD11 = new GPIO(10, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应11");
        public static GPIO GPIO_IN_DL_HD_HD12 = new GPIO(11, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应12");
        public static GPIO GPIO_IN_DL_HD_HD13 = new GPIO(12, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应13");
        public static GPIO GPIO_IN_DL_HD_HD14 = new GPIO(13, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应14");
        public static GPIO GPIO_IN_DL_HD_HD15 = new GPIO(14, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应15");
        public static GPIO GPIO_IN_DL_HD_HD16 = new GPIO(15, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料真空感应16");
        //夹爪下位感应
        public static GPIO GPIO_IN_DL_DW_HD1 = new GPIO(16, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应1");
        public static GPIO GPIO_IN_DL_DW_HD2 = new GPIO(17, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应2");
        public static GPIO GPIO_IN_DL_DW_HD3 = new GPIO(18, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应3");
        public static GPIO GPIO_IN_DL_DW_HD4 = new GPIO(19, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应4");
        public static GPIO GPIO_IN_DL_DW_HD5 = new GPIO(20, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应5");
        public static GPIO GPIO_IN_DL_DW_HD6 = new GPIO(21, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应6");
        public static GPIO GPIO_IN_DL_DW_HD7 = new GPIO(22, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应7");
        public static GPIO GPIO_IN_DL_DW_HD8 = new GPIO(23, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应8");
        public static GPIO GPIO_IN_DL_DW_HD9 = new GPIO(24, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应9");
        public static GPIO GPIO_IN_DL_DW_HD10 = new GPIO(25, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应10");
        public static GPIO GPIO_IN_DL_DW_HD11 = new GPIO(26, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应11");
        public static GPIO GPIO_IN_DL_DW_HD12 = new GPIO(27, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应12");
        public static GPIO GPIO_IN_DL_DW_HD13 = new GPIO(28, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应13");
        public static GPIO GPIO_IN_DL_DW_HD14 = new GPIO(29, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应14");
        public static GPIO GPIO_IN_DL_DW_HD15 = new GPIO(30, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应15");
        public static GPIO GPIO_IN_DL_DW_HD16 = new GPIO(31, CARD_ECI0064_4, GPIO.IO_DIR.IN, GPIO.IO_TYPE.IO_CARD, "下料夹爪下位感应16");
        //料夹在位
        public static GPIO GPIO_IN_UL_INP_FD_TRAYBOX = new GPIO(13, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料仓在位感应");
        public static GPIO GPIO_IN_DL_INP_OK_TRAYBOX = new GPIO(14, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料仓在位感应");
        public static GPIO GPIO_IN_DL_INP_NG_TRAYBOX = new GPIO(15, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料仓在位感应");        
        //料夹有料
        public static GPIO GPIO_IN_UL_RDY_FD_TRAY = new GPIO(13, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "供料仓有料感应");
        public static GPIO GPIO_IN_DL_RDY_OK_TRAY = new GPIO(14, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料仓有料感应");
        public static GPIO GPIO_IN_DL_RDY_NG_TRAY = new GPIO(15, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料仓有料感应");
        //料盘真空
        //public static GPIO GPIO_IN_DL_ZK_NG_TRAY = new GPIO(15, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "NG料盘真空感应");
        //public static GPIO GPIO_IN_DL_ZK_OK_TRAY = new GPIO(14, CARD_DMC3800_5, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "OK料盘真空感应");
        public static GPIO GPIO_IN_UD_DOOR = new GPIO(16, CARD_ECI2600_3, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "上下料门感应");

        //转台        
        public static GPIO GPIO_IN_TT_ALM = new GPIO(3, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台报警(30B)信号");
        public static GPIO GPIO_IN_TT_INP = new GPIO(4, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台到位(X2)信号");
        public static GPIO GPIO_IN_TT_SEN90 = new GPIO(5, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台90°信号");
        public static GPIO GPIO_IN_TT_SEN0 = new GPIO(6, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台0°信号");
        public static GPIO GPIO_IN_TT_SEN270 = new GPIO(7, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台270°信号");
        public static GPIO GPIO_IN_TT_MOVE = new GPIO(8, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台转动(Y1)信号");
        public static GPIO GPIO_IN_TT_DOOR = new GPIO(10, CARD_ECI2400_0, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "转台门感应");

        //工站开盖WS1
        public static GPIO GPIO_IN_WS1_OPEN_FR1 = new GPIO(0, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖1");
        public static GPIO GPIO_IN_WS1_OPEN_FR2 = new GPIO(1, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖2");
        public static GPIO GPIO_IN_WS1_OPEN_FR3 = new GPIO(2, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖3");
        public static GPIO GPIO_IN_WS1_OPEN_FR4 = new GPIO(3, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖4");
        public static GPIO GPIO_IN_WS1_OPEN_FR5 = new GPIO(4, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖5");
        public static GPIO GPIO_IN_WS1_OPEN_FR6 = new GPIO(5, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖6");
        public static GPIO GPIO_IN_WS1_OPEN_FR7 = new GPIO(6, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖7");
        public static GPIO GPIO_IN_WS1_OPEN_FR8 = new GPIO(7, AXIS_WS1_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1前排开盖8");

        public static GPIO GPIO_IN_WS1_OPEN_BK1 = new GPIO(0, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖1");
        public static GPIO GPIO_IN_WS1_OPEN_BK2 = new GPIO(1, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖2");
        public static GPIO GPIO_IN_WS1_OPEN_BK3 = new GPIO(2, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖3");
        public static GPIO GPIO_IN_WS1_OPEN_BK4 = new GPIO(3, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖4");
        public static GPIO GPIO_IN_WS1_OPEN_BK5 = new GPIO(4, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖5");
        public static GPIO GPIO_IN_WS1_OPEN_BK6 = new GPIO(5, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖6");
        public static GPIO GPIO_IN_WS1_OPEN_BK7 = new GPIO(6, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖7");
        public static GPIO GPIO_IN_WS1_OPEN_BK8 = new GPIO(7, AXIS_WS1_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站1后排开盖8");
        //工站开盖WS2
        public static GPIO GPIO_IN_WS2_OPEN_FR1 = new GPIO(0, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖1");
        public static GPIO GPIO_IN_WS2_OPEN_FR2 = new GPIO(1, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖2");
        public static GPIO GPIO_IN_WS2_OPEN_FR3 = new GPIO(2, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖3");
        public static GPIO GPIO_IN_WS2_OPEN_FR4 = new GPIO(3, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖4");
        public static GPIO GPIO_IN_WS2_OPEN_FR5 = new GPIO(4, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖5");
        public static GPIO GPIO_IN_WS2_OPEN_FR6 = new GPIO(5, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖6");
        public static GPIO GPIO_IN_WS2_OPEN_FR7 = new GPIO(6, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖7");
        public static GPIO GPIO_IN_WS2_OPEN_FR8 = new GPIO(7, AXIS_WS2_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2前排开盖8");

        public static GPIO GPIO_IN_WS2_OPEN_BK1 = new GPIO(0, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖1");
        public static GPIO GPIO_IN_WS2_OPEN_BK2 = new GPIO(1, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖2");
        public static GPIO GPIO_IN_WS2_OPEN_BK3 = new GPIO(2, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖3");
        public static GPIO GPIO_IN_WS2_OPEN_BK4 = new GPIO(3, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖4");
        public static GPIO GPIO_IN_WS2_OPEN_BK5 = new GPIO(4, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖5");
        public static GPIO GPIO_IN_WS2_OPEN_BK6 = new GPIO(5, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖6");
        public static GPIO GPIO_IN_WS2_OPEN_BK7 = new GPIO(6, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖7");
        public static GPIO GPIO_IN_WS2_OPEN_BK8 = new GPIO(7, AXIS_WS2_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站2后排开盖8");

        //工站开盖WS3
        public static GPIO GPIO_IN_WS3_OPEN_FR1 = new GPIO(0, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖1");
        public static GPIO GPIO_IN_WS3_OPEN_FR2 = new GPIO(1, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖2");
        public static GPIO GPIO_IN_WS3_OPEN_FR3 = new GPIO(2, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖3");
        public static GPIO GPIO_IN_WS3_OPEN_FR4 = new GPIO(3, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖4");
        public static GPIO GPIO_IN_WS3_OPEN_FR5 = new GPIO(4, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖5");
        public static GPIO GPIO_IN_WS3_OPEN_FR6 = new GPIO(5, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖6");
        public static GPIO GPIO_IN_WS3_OPEN_FR7 = new GPIO(6, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖7");
        public static GPIO GPIO_IN_WS3_OPEN_FR8 = new GPIO(7, AXIS_WS3_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3前排开盖8");

        public static GPIO GPIO_IN_WS3_OPEN_BK1 = new GPIO(0, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖1");
        public static GPIO GPIO_IN_WS3_OPEN_BK2 = new GPIO(1, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖2");
        public static GPIO GPIO_IN_WS3_OPEN_BK3 = new GPIO(2, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖3");
        public static GPIO GPIO_IN_WS3_OPEN_BK4 = new GPIO(3, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖4");
        public static GPIO GPIO_IN_WS3_OPEN_BK5 = new GPIO(4, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖5");
        public static GPIO GPIO_IN_WS3_OPEN_BK6 = new GPIO(5, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖6");
        public static GPIO GPIO_IN_WS3_OPEN_BK7 = new GPIO(6, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖7");
        public static GPIO GPIO_IN_WS3_OPEN_BK8 = new GPIO(7, AXIS_WS3_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站3后排开盖8");

        //工站开盖WS4
        public static GPIO GPIO_IN_WS4_OPEN_FR1 = new GPIO(0, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖1");
        public static GPIO GPIO_IN_WS4_OPEN_FR2 = new GPIO(1, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖2");
        public static GPIO GPIO_IN_WS4_OPEN_FR3 = new GPIO(2, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖3");
        public static GPIO GPIO_IN_WS4_OPEN_FR4 = new GPIO(3, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖4");
        public static GPIO GPIO_IN_WS4_OPEN_FR5 = new GPIO(4, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖5");
        public static GPIO GPIO_IN_WS4_OPEN_FR6 = new GPIO(5, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖6");
        public static GPIO GPIO_IN_WS4_OPEN_FR7 = new GPIO(6, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖7");
        public static GPIO GPIO_IN_WS4_OPEN_FR8 = new GPIO(7, AXIS_WS4_F, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4前排开盖8");

        public static GPIO GPIO_IN_WS4_OPEN_BK1 = new GPIO(0, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖1");
        public static GPIO GPIO_IN_WS4_OPEN_BK2 = new GPIO(1, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖2");
        public static GPIO GPIO_IN_WS4_OPEN_BK3 = new GPIO(2, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖3");
        public static GPIO GPIO_IN_WS4_OPEN_BK4 = new GPIO(3, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖4");
        public static GPIO GPIO_IN_WS4_OPEN_BK5 = new GPIO(4, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖5");
        public static GPIO GPIO_IN_WS4_OPEN_BK6 = new GPIO(5, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖6");
        public static GPIO GPIO_IN_WS4_OPEN_BK7 = new GPIO(6, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖7");
        public static GPIO GPIO_IN_WS4_OPEN_BK8 = new GPIO(7, AXIS_WS4_B, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "工站4后排开盖8");

        //左光箱
        public static GPIO GPIO_IN_LLB_LEFT_DOOR = new GPIO(10, CARD_ECI2400_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "左光箱左门感应");
        public static GPIO GPIO_IN_LLB_BACK_DOOR = new GPIO(9, CARD_ECI2400_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "左光箱后门感应");
        public static GPIO GPIO_IN_LLB_FRONT_DOOR = new GPIO(11, CARD_ECI2400_1, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "左光箱前门感应");

        //右光箱
        public static GPIO GPIO_IN_RLB_LEFT_DOOR = new GPIO(10, CARD_ECI2400_2, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "右光箱右门感应");
        public static GPIO GPIO_IN_RLB_BACK_DOOR = new GPIO(11, CARD_ECI2400_2, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "右光箱后门感应");
        public static GPIO GPIO_IN_RLB_FRONT_DOOR = new GPIO(9, CARD_ECI2400_2, GPIO.IO_DIR.IN, GPIO.IO_TYPE.MT_CARD, "右光箱前门感应");
        #endregion
        #region 气缸定义
        //上料
        public static Cylinder CLD_UL_N1 = new Cylinder(GPIO_OUT_UL_ZK_N1, GPIO_IN_UL_ZK_N1);
        public static Cylinder CLD_UL_N2 = new Cylinder(GPIO_OUT_UL_ZK_N2, GPIO_IN_UL_ZK_N2);
        //料盘
        public static Cylinder CLD_UL_TRAY_FD = new Cylinder(GPIO_OUT_UL_FD_TRAY, GPIO_IN_UL_FD_TRAY);

        //下料
        public static Cylinder CLD_DL_OKTRAY_HD = new Cylinder(GPIO_OUT_DL_OK_TRAY, GPIO_IN_DL_OK_TRAY);
        public static Cylinder CLD_DL_NGTRAY_HD = new Cylinder(GPIO_OUT_DL_NG_TRAY, GPIO_IN_DL_NG_TRAY);
        //夹爪
        public static Cylinder CLD_DL_HD_HD1 = new Cylinder(GPIO_OUT_DL_HD_HD1, null, GPIO_IN_DL_HD_HD1);
        public static Cylinder CLD_DL_HD_HD2 = new Cylinder(GPIO_OUT_DL_HD_HD2, null, GPIO_IN_DL_HD_HD2);
        public static Cylinder CLD_DL_HD_HD3 = new Cylinder(GPIO_OUT_DL_HD_HD3, null, GPIO_IN_DL_HD_HD3);
        public static Cylinder CLD_DL_HD_HD4 = new Cylinder(GPIO_OUT_DL_HD_HD4, null, GPIO_IN_DL_HD_HD4);
        public static Cylinder CLD_DL_HD_HD5 = new Cylinder(GPIO_OUT_DL_HD_HD5, null, GPIO_IN_DL_HD_HD5);
        public static Cylinder CLD_DL_HD_HD6 = new Cylinder(GPIO_OUT_DL_HD_HD6, null, GPIO_IN_DL_HD_HD6);
        public static Cylinder CLD_DL_HD_HD7 = new Cylinder(GPIO_OUT_DL_HD_HD7, null, GPIO_IN_DL_HD_HD7);
        public static Cylinder CLD_DL_HD_HD8 = new Cylinder(GPIO_OUT_DL_HD_HD8, null, GPIO_IN_DL_HD_HD8);
        public static Cylinder CLD_DL_HD_HD9 = new Cylinder(GPIO_OUT_DL_HD_HD9, null, GPIO_IN_DL_HD_HD9);
        public static Cylinder CLD_DL_HD_HD10 = new Cylinder(GPIO_OUT_DL_HD_HD10, null, GPIO_IN_DL_HD_HD10);
        public static Cylinder CLD_DL_HD_HD11 = new Cylinder(GPIO_OUT_DL_HD_HD11, null, GPIO_IN_DL_HD_HD11);
        public static Cylinder CLD_DL_HD_HD12 = new Cylinder(GPIO_OUT_DL_HD_HD12, null, GPIO_IN_DL_HD_HD12);
        public static Cylinder CLD_DL_HD_HD13 = new Cylinder(GPIO_OUT_DL_HD_HD13, null, GPIO_IN_DL_HD_HD13);
        public static Cylinder CLD_DL_HD_HD14 = new Cylinder(GPIO_OUT_DL_HD_HD14, null, GPIO_IN_DL_HD_HD14);
        public static Cylinder CLD_DL_HD_HD15 = new Cylinder(GPIO_OUT_DL_HD_HD15, null, GPIO_IN_DL_HD_HD15);
        public static Cylinder CLD_DL_HD_HD16 = new Cylinder(GPIO_OUT_DL_HD_HD16, null, GPIO_IN_DL_HD_HD16);
        public static List<Cylinder> List_CLD_HD_HD = new List<Cylinder> { CLD_DL_HD_HD1, CLD_DL_HD_HD2, CLD_DL_HD_HD3, CLD_DL_HD_HD4, CLD_DL_HD_HD5, CLD_DL_HD_HD6, CLD_DL_HD_HD7, CLD_DL_HD_HD8,
            CLD_DL_HD_HD9, CLD_DL_HD_HD10, CLD_DL_HD_HD11, CLD_DL_HD_HD12, CLD_DL_HD_HD13, CLD_DL_HD_HD14, CLD_DL_HD_HD15, CLD_DL_HD_HD16};
        //夹爪上下
        public static Cylinder CLD_DL_UD_HD1 = new Cylinder(GPIO_OUT_DL_UD_HD1, null, GPIO_IN_DL_DW_HD1);
        public static Cylinder CLD_DL_UD_HD2 = new Cylinder(GPIO_OUT_DL_UD_HD2, null, GPIO_IN_DL_DW_HD2);
        public static Cylinder CLD_DL_UD_HD3 = new Cylinder(GPIO_OUT_DL_UD_HD3, null, GPIO_IN_DL_DW_HD3);
        public static Cylinder CLD_DL_UD_HD4 = new Cylinder(GPIO_OUT_DL_UD_HD4, null, GPIO_IN_DL_DW_HD4);
        public static Cylinder CLD_DL_UD_HD5 = new Cylinder(GPIO_OUT_DL_UD_HD5, null, GPIO_IN_DL_DW_HD5);
        public static Cylinder CLD_DL_UD_HD6 = new Cylinder(GPIO_OUT_DL_UD_HD6, null, GPIO_IN_DL_DW_HD6);
        public static Cylinder CLD_DL_UD_HD7 = new Cylinder(GPIO_OUT_DL_UD_HD7, null, GPIO_IN_DL_DW_HD7);
        public static Cylinder CLD_DL_UD_HD8 = new Cylinder(GPIO_OUT_DL_UD_HD8, null, GPIO_IN_DL_DW_HD8);
        public static Cylinder CLD_DL_UD_HD9 = new Cylinder(GPIO_OUT_DL_UD_HD9, null, GPIO_IN_DL_DW_HD9);
        public static Cylinder CLD_DL_UD_HD10 = new Cylinder(GPIO_OUT_DL_UD_HD10, null, GPIO_IN_DL_DW_HD10);
        public static Cylinder CLD_DL_UD_HD11 = new Cylinder(GPIO_OUT_DL_UD_HD11, null, GPIO_IN_DL_DW_HD11);
        public static Cylinder CLD_DL_UD_HD12 = new Cylinder(GPIO_OUT_DL_UD_HD12, null, GPIO_IN_DL_DW_HD12);
        public static Cylinder CLD_DL_UD_HD13 = new Cylinder(GPIO_OUT_DL_UD_HD13, null, GPIO_IN_DL_DW_HD13);
        public static Cylinder CLD_DL_UD_HD14 = new Cylinder(GPIO_OUT_DL_UD_HD14, null, GPIO_IN_DL_DW_HD14);
        public static Cylinder CLD_DL_UD_HD15 = new Cylinder(GPIO_OUT_DL_UD_HD15, null, GPIO_IN_DL_DW_HD15);
        public static Cylinder CLD_DL_UD_HD16 = new Cylinder(GPIO_OUT_DL_UD_HD16, null, GPIO_IN_DL_DW_HD16);
        public static List<Cylinder> List_CLD_UD_HD = new List<Cylinder> { CLD_DL_UD_HD1, CLD_DL_UD_HD2, CLD_DL_UD_HD3, CLD_DL_UD_HD4, CLD_DL_UD_HD5, CLD_DL_UD_HD6, CLD_DL_UD_HD7, CLD_DL_UD_HD8,
            CLD_DL_UD_HD9, CLD_DL_UD_HD10, CLD_DL_UD_HD11, CLD_DL_UD_HD12, CLD_DL_UD_HD13, CLD_DL_UD_HD14, CLD_DL_UD_HD15, CLD_DL_UD_HD16};
        //料盘
        public static Cylinder CLD_DL_HD_TRAY_NG = new Cylinder(GPIO_OUT_DL_NG_TRAY, GPIO_IN_DL_NG_TRAY);
        public static Cylinder CLD_DL_HD_TRAY_OK = new Cylinder(GPIO_OUT_DL_OK_TRAY, GPIO_IN_DL_OK_TRAY);

        //工站WS1
        public static Cylinder CLD_OPEN_WS1_FR1 = new Cylinder(GPIO_OUT_WS1_OPEN_FR1, null, GPIO_IN_WS1_OPEN_FR1);
        public static Cylinder CLD_OPEN_WS1_FR2 = new Cylinder(GPIO_OUT_WS1_OPEN_FR2, null, GPIO_IN_WS1_OPEN_FR2);
        public static Cylinder CLD_OPEN_WS1_FR3 = new Cylinder(GPIO_OUT_WS1_OPEN_FR3, null, GPIO_IN_WS1_OPEN_FR3);
        public static Cylinder CLD_OPEN_WS1_FR4 = new Cylinder(GPIO_OUT_WS1_OPEN_FR4, null, GPIO_IN_WS1_OPEN_FR4);
        public static Cylinder CLD_OPEN_WS1_FR5 = new Cylinder(GPIO_OUT_WS1_OPEN_FR5, null, GPIO_IN_WS1_OPEN_FR5);
        public static Cylinder CLD_OPEN_WS1_FR6 = new Cylinder(GPIO_OUT_WS1_OPEN_FR6, null, GPIO_IN_WS1_OPEN_FR6);
        public static Cylinder CLD_OPEN_WS1_FR7 = new Cylinder(GPIO_OUT_WS1_OPEN_FR7, null, GPIO_IN_WS1_OPEN_FR7);
        public static Cylinder CLD_OPEN_WS1_FR8 = new Cylinder(GPIO_OUT_WS1_OPEN_FR8, null, GPIO_IN_WS1_OPEN_FR8);

        public static Cylinder CLD_OPEN_WS1_BK1 = new Cylinder(GPIO_OUT_WS1_OPEN_BK1, null, GPIO_IN_WS1_OPEN_BK1);
        public static Cylinder CLD_OPEN_WS1_BK2 = new Cylinder(GPIO_OUT_WS1_OPEN_BK2, null, GPIO_IN_WS1_OPEN_BK2);
        public static Cylinder CLD_OPEN_WS1_BK3 = new Cylinder(GPIO_OUT_WS1_OPEN_BK3, null, GPIO_IN_WS1_OPEN_BK3);
        public static Cylinder CLD_OPEN_WS1_BK4 = new Cylinder(GPIO_OUT_WS1_OPEN_BK4, null, GPIO_IN_WS1_OPEN_BK4);
        public static Cylinder CLD_OPEN_WS1_BK5 = new Cylinder(GPIO_OUT_WS1_OPEN_BK5, null, GPIO_IN_WS1_OPEN_BK5);
        public static Cylinder CLD_OPEN_WS1_BK6 = new Cylinder(GPIO_OUT_WS1_OPEN_BK6, null, GPIO_IN_WS1_OPEN_BK6);
        public static Cylinder CLD_OPEN_WS1_BK7 = new Cylinder(GPIO_OUT_WS1_OPEN_BK7, null, GPIO_IN_WS1_OPEN_BK7);
        public static Cylinder CLD_OPEN_WS1_BK8 = new Cylinder(GPIO_OUT_WS1_OPEN_BK8, null, GPIO_IN_WS1_OPEN_BK8);

        public static List<Cylinder> List_CLD_WS1_FR = new List<Cylinder> { CLD_OPEN_WS1_FR1, CLD_OPEN_WS1_FR2, CLD_OPEN_WS1_FR3, CLD_OPEN_WS1_FR4, CLD_OPEN_WS1_FR5, CLD_OPEN_WS1_FR6, CLD_OPEN_WS1_FR7, CLD_OPEN_WS1_FR8 };
        public static List<Cylinder> List_CLD_WS1_BK = new List<Cylinder> { CLD_OPEN_WS1_BK1, CLD_OPEN_WS1_BK2, CLD_OPEN_WS1_BK3, CLD_OPEN_WS1_BK4, CLD_OPEN_WS1_BK5, CLD_OPEN_WS1_BK6, CLD_OPEN_WS1_BK7, CLD_OPEN_WS1_BK8 };

        //工站WS2
        public static Cylinder CLD_OPEN_WS2_FR1 = new Cylinder(GPIO_OUT_WS2_OPEN_FR1, null, GPIO_IN_WS2_OPEN_FR1);
        public static Cylinder CLD_OPEN_WS2_FR2 = new Cylinder(GPIO_OUT_WS2_OPEN_FR2, null, GPIO_IN_WS2_OPEN_FR2);
        public static Cylinder CLD_OPEN_WS2_FR3 = new Cylinder(GPIO_OUT_WS2_OPEN_FR3, null, GPIO_IN_WS2_OPEN_FR3);
        public static Cylinder CLD_OPEN_WS2_FR4 = new Cylinder(GPIO_OUT_WS2_OPEN_FR4, null, GPIO_IN_WS2_OPEN_FR4);
        public static Cylinder CLD_OPEN_WS2_FR5 = new Cylinder(GPIO_OUT_WS2_OPEN_FR5, null, GPIO_IN_WS2_OPEN_FR5);
        public static Cylinder CLD_OPEN_WS2_FR6 = new Cylinder(GPIO_OUT_WS2_OPEN_FR6, null, GPIO_IN_WS2_OPEN_FR6);
        public static Cylinder CLD_OPEN_WS2_FR7 = new Cylinder(GPIO_OUT_WS2_OPEN_FR7, null, GPIO_IN_WS2_OPEN_FR7);
        public static Cylinder CLD_OPEN_WS2_FR8 = new Cylinder(GPIO_OUT_WS2_OPEN_FR8, null, GPIO_IN_WS2_OPEN_FR8);

        public static Cylinder CLD_OPEN_WS2_BK1 = new Cylinder(GPIO_OUT_WS2_OPEN_BK1, null, GPIO_IN_WS2_OPEN_BK1);
        public static Cylinder CLD_OPEN_WS2_BK2 = new Cylinder(GPIO_OUT_WS2_OPEN_BK2, null, GPIO_IN_WS2_OPEN_BK2);
        public static Cylinder CLD_OPEN_WS2_BK3 = new Cylinder(GPIO_OUT_WS2_OPEN_BK3, null, GPIO_IN_WS2_OPEN_BK3);
        public static Cylinder CLD_OPEN_WS2_BK4 = new Cylinder(GPIO_OUT_WS2_OPEN_BK4, null, GPIO_IN_WS2_OPEN_BK4);
        public static Cylinder CLD_OPEN_WS2_BK5 = new Cylinder(GPIO_OUT_WS2_OPEN_BK5, null, GPIO_IN_WS2_OPEN_BK5);
        public static Cylinder CLD_OPEN_WS2_BK6 = new Cylinder(GPIO_OUT_WS2_OPEN_BK6, null, GPIO_IN_WS2_OPEN_BK6);
        public static Cylinder CLD_OPEN_WS2_BK7 = new Cylinder(GPIO_OUT_WS2_OPEN_BK7, null, GPIO_IN_WS2_OPEN_BK7);
        public static Cylinder CLD_OPEN_WS2_BK8 = new Cylinder(GPIO_OUT_WS2_OPEN_BK8, null, GPIO_IN_WS2_OPEN_BK8);

        public static List<Cylinder> List_CLD_WS2_FR = new List<Cylinder> { CLD_OPEN_WS2_FR1, CLD_OPEN_WS2_FR2, CLD_OPEN_WS2_FR3, CLD_OPEN_WS2_FR4, CLD_OPEN_WS2_FR5, CLD_OPEN_WS2_FR6, CLD_OPEN_WS2_FR7, CLD_OPEN_WS2_FR8 };
        public static List<Cylinder> List_CLD_WS2_BK = new List<Cylinder> { CLD_OPEN_WS2_BK1, CLD_OPEN_WS2_BK2, CLD_OPEN_WS2_BK3, CLD_OPEN_WS2_BK4, CLD_OPEN_WS2_BK5, CLD_OPEN_WS2_BK6, CLD_OPEN_WS2_BK7, CLD_OPEN_WS2_BK8 };

        //工站WS3
        public static Cylinder CLD_OPEN_WS3_FR1 = new Cylinder(GPIO_OUT_WS3_OPEN_FR1, null, GPIO_IN_WS3_OPEN_FR1);
        public static Cylinder CLD_OPEN_WS3_FR2 = new Cylinder(GPIO_OUT_WS3_OPEN_FR2, null, GPIO_IN_WS3_OPEN_FR2);
        public static Cylinder CLD_OPEN_WS3_FR3 = new Cylinder(GPIO_OUT_WS3_OPEN_FR3, null, GPIO_IN_WS3_OPEN_FR3);
        public static Cylinder CLD_OPEN_WS3_FR4 = new Cylinder(GPIO_OUT_WS3_OPEN_FR4, null, GPIO_IN_WS3_OPEN_FR4);
        public static Cylinder CLD_OPEN_WS3_FR5 = new Cylinder(GPIO_OUT_WS3_OPEN_FR5, null, GPIO_IN_WS3_OPEN_FR5);
        public static Cylinder CLD_OPEN_WS3_FR6 = new Cylinder(GPIO_OUT_WS3_OPEN_FR6, null, GPIO_IN_WS3_OPEN_FR6);
        public static Cylinder CLD_OPEN_WS3_FR7 = new Cylinder(GPIO_OUT_WS3_OPEN_FR7, null, GPIO_IN_WS3_OPEN_FR7);
        public static Cylinder CLD_OPEN_WS3_FR8 = new Cylinder(GPIO_OUT_WS3_OPEN_FR8, null, GPIO_IN_WS3_OPEN_FR8);

        public static Cylinder CLD_OPEN_WS3_BK1 = new Cylinder(GPIO_OUT_WS3_OPEN_BK1, null, GPIO_IN_WS3_OPEN_BK1);
        public static Cylinder CLD_OPEN_WS3_BK2 = new Cylinder(GPIO_OUT_WS3_OPEN_BK2, null, GPIO_IN_WS3_OPEN_BK2);
        public static Cylinder CLD_OPEN_WS3_BK3 = new Cylinder(GPIO_OUT_WS3_OPEN_BK3, null, GPIO_IN_WS3_OPEN_BK3);
        public static Cylinder CLD_OPEN_WS3_BK4 = new Cylinder(GPIO_OUT_WS3_OPEN_BK4, null, GPIO_IN_WS3_OPEN_BK4);
        public static Cylinder CLD_OPEN_WS3_BK5 = new Cylinder(GPIO_OUT_WS3_OPEN_BK5, null, GPIO_IN_WS3_OPEN_BK5);
        public static Cylinder CLD_OPEN_WS3_BK6 = new Cylinder(GPIO_OUT_WS3_OPEN_BK6, null, GPIO_IN_WS3_OPEN_BK6);
        public static Cylinder CLD_OPEN_WS3_BK7 = new Cylinder(GPIO_OUT_WS3_OPEN_BK7, null, GPIO_IN_WS3_OPEN_BK7);
        public static Cylinder CLD_OPEN_WS3_BK8 = new Cylinder(GPIO_OUT_WS3_OPEN_BK8, null, GPIO_IN_WS3_OPEN_BK8);

        public static List<Cylinder> List_CLD_WS3_FR = new List<Cylinder> { CLD_OPEN_WS3_FR1, CLD_OPEN_WS3_FR2, CLD_OPEN_WS3_FR3, CLD_OPEN_WS3_FR4, CLD_OPEN_WS3_FR5, CLD_OPEN_WS3_FR6, CLD_OPEN_WS3_FR7, CLD_OPEN_WS3_FR8 };
        public static List<Cylinder> List_CLD_WS3_BK = new List<Cylinder> { CLD_OPEN_WS3_BK1, CLD_OPEN_WS3_BK2, CLD_OPEN_WS3_BK3, CLD_OPEN_WS3_BK4, CLD_OPEN_WS3_BK5, CLD_OPEN_WS3_BK6, CLD_OPEN_WS3_BK7, CLD_OPEN_WS3_BK8 };

        //工站WS4
        public static Cylinder CLD_OPEN_WS4_FR1 = new Cylinder(GPIO_OUT_WS4_OPEN_FR1, null, GPIO_IN_WS4_OPEN_FR1);
        public static Cylinder CLD_OPEN_WS4_FR2 = new Cylinder(GPIO_OUT_WS4_OPEN_FR2, null, GPIO_IN_WS4_OPEN_FR2);
        public static Cylinder CLD_OPEN_WS4_FR3 = new Cylinder(GPIO_OUT_WS4_OPEN_FR3, null, GPIO_IN_WS4_OPEN_FR3);
        public static Cylinder CLD_OPEN_WS4_FR4 = new Cylinder(GPIO_OUT_WS4_OPEN_FR4, null, GPIO_IN_WS4_OPEN_FR4);
        public static Cylinder CLD_OPEN_WS4_FR5 = new Cylinder(GPIO_OUT_WS4_OPEN_FR5, null, GPIO_IN_WS4_OPEN_FR5);
        public static Cylinder CLD_OPEN_WS4_FR6 = new Cylinder(GPIO_OUT_WS4_OPEN_FR6, null, GPIO_IN_WS4_OPEN_FR6);
        public static Cylinder CLD_OPEN_WS4_FR7 = new Cylinder(GPIO_OUT_WS4_OPEN_FR7, null, GPIO_IN_WS4_OPEN_FR7);
        public static Cylinder CLD_OPEN_WS4_FR8 = new Cylinder(GPIO_OUT_WS4_OPEN_FR8, null, GPIO_IN_WS4_OPEN_FR8);

        public static Cylinder CLD_OPEN_WS4_BK1 = new Cylinder(GPIO_OUT_WS4_OPEN_BK1, null, GPIO_IN_WS4_OPEN_BK1);
        public static Cylinder CLD_OPEN_WS4_BK2 = new Cylinder(GPIO_OUT_WS4_OPEN_BK2, null, GPIO_IN_WS4_OPEN_BK2);
        public static Cylinder CLD_OPEN_WS4_BK3 = new Cylinder(GPIO_OUT_WS4_OPEN_BK3, null, GPIO_IN_WS4_OPEN_BK3);
        public static Cylinder CLD_OPEN_WS4_BK4 = new Cylinder(GPIO_OUT_WS4_OPEN_BK4, null, GPIO_IN_WS4_OPEN_BK4);
        public static Cylinder CLD_OPEN_WS4_BK5 = new Cylinder(GPIO_OUT_WS4_OPEN_BK5, null, GPIO_IN_WS4_OPEN_BK5);
        public static Cylinder CLD_OPEN_WS4_BK6 = new Cylinder(GPIO_OUT_WS4_OPEN_BK6, null, GPIO_IN_WS4_OPEN_BK6);
        public static Cylinder CLD_OPEN_WS4_BK7 = new Cylinder(GPIO_OUT_WS4_OPEN_BK7, null, GPIO_IN_WS4_OPEN_BK7);
        public static Cylinder CLD_OPEN_WS4_BK8 = new Cylinder(GPIO_OUT_WS4_OPEN_BK8, null, GPIO_IN_WS4_OPEN_BK8);

        public static List<Cylinder> List_CLD_WS4_FR = new List<Cylinder> { CLD_OPEN_WS4_FR1, CLD_OPEN_WS4_FR2, CLD_OPEN_WS4_FR3, CLD_OPEN_WS4_FR4, CLD_OPEN_WS4_FR5, CLD_OPEN_WS4_FR6, CLD_OPEN_WS4_FR7, CLD_OPEN_WS4_FR8 };
        public static List<Cylinder> List_CLD_WS4_BK = new List<Cylinder> { CLD_OPEN_WS4_BK1, CLD_OPEN_WS4_BK2, CLD_OPEN_WS4_BK3, CLD_OPEN_WS4_BK4, CLD_OPEN_WS4_BK5, CLD_OPEN_WS4_BK6, CLD_OPEN_WS4_BK7, CLD_OPEN_WS4_BK8 };


        #endregion

        #endregion

        #region 错误框显示
        public static readonly object lockdisplay = new object();
        public static warning fr_warn= new warning();
        public static void Display_frwarn(Color cl, string errmsg)
        {
            lock (lockdisplay)
            {
                fr_warn.TopMost = true;
                fr_warn.BackColor = cl;
                fr_warn.lb_msg.Text = errmsg;
                fr_warn.ShowDialog();
            }

        }


        #endregion
        #region 安全监测
        public static void SetSafeFunc()
        {
            foreach (AXIS ax in AxList_UL)
            {
                ax.ChkSafeSen = ChkSafeSen;
                ax.ChkSafePos = ChkSafePos;
            }

            foreach (AXIS ax in AxList_DL)
            {
                ax.ChkSafeSen = ChkSafeSen;
                ax.ChkSafePos = ChkSafePos;
            }
        }

        public static EM_RES ChkSafePos(int id, double targe_pos = double.MaxValue)
        {
            EM_RES ret;
         
            //安全保护
            ret = ChkSafeSen(id);
           
            if(id==MT.AXIS_UL_X.id||id==MT.AXIS_UL_Y.id)
            {
                if (Math.Abs(MT.AXIS_UL_Z.fenc_pos) > 2)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "上料Z当前坐标>2，禁止移动上料X轴或上料Y轴!");
                    return EM_RES.MOVE_PROTECT;
                }
            }

            if (id == MT.AXIS_UL_Y.id && MT.AXIS_UL_Y.home_status == AXIS.HOME_STA.OK&& VAR.gsys_set.status!=EM_SYS_STA.RUN && targe_pos > 400)
            {
                //if (!Turntable.GetWSOnFeedPos._isUInFeedPos && !Turntable.GetWSOnFeedPos.isUInFeedPos)
                //{
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "上料工位不在上料状态，禁止移动上料Y轴!");
                //    return EM_RES.MOVE_PROTECT;
                //}

            }

            if (MT.AXIS_DL_Y.home_status == AXIS.HOME_STA.OK && MT.AXIS_UL_X.home_status == AXIS.HOME_STA.OK)
            {
                //if (id == MT.AXIS_DL_Y.id)
                //{
                //    if (!DownloadModle.isUp)
                //        return EM_RES.MOVE_PROTECT;
                //    if ((targe_pos > (DownloadModle.Dwload_Ysafe+0.1)) && MT.AXIS_UL_X.fenc_pos >0.1)
                //    {
                //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("下料Y定位位置{0}大于安全位置{1},且上料X轴的当前位置不在原点,禁止下料轴移动!", targe_pos, DownloadModle.Dwload_Ysafe));
                //        return EM_RES.MOVE_PROTECT;
                //    }
                //}

                //if (id == MT.AXIS_UL_X.id)
                //{
                //    if ((MT.AXIS_DL_Y.fenc_pos > (DownloadModle.Dwload_Ysafe+0.1)) && targe_pos >0.1)
                //    {
                //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("下料Y轴当前位置大于安全位置{0}，上料X轴禁止移动!", DownloadModle.Dwload_Ysafe));
                //        return EM_RES.MOVE_PROTECT;
                //    }

                //}
            }


            //if (id == MT.AXIS_UL_FD_Z.id)
            //{
            //    if ((Math.Abs(MT.AXIS_UL_FD_X.fenc_pos) > COM.traybox_fd.fd_safe_x+1) && (Math.Abs(targe_pos - MT.AXIS_UL_FD_Z.fenc_pos) > COM.traybox_fd.tray_feed_ofs_h + 0.1))
            //    {
            //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("供料仓X轴插入OK料仓，OK料Z轴移动距离{0}超过{1}!", (Math.Abs(targe_pos - MT.AXIS_UL_FD_Z.fenc_pos)), COM.traybox_fd.tray_feed_ofs_h));
            //        return EM_RES.MOVE_PROTECT;
            //    }
            //}
          
            //if (id == MT.AXIS_DL_NG_Z.id)
            //{
            //    if ((Math.Abs(MT.AXIS_DL_NG_X.fenc_pos) > COM.traybox_ng.fd_safe_x+1) && (Math.Abs(targe_pos - MT.AXIS_DL_NG_Z.fenc_pos) > COM.traybox_ng.tray_feed_ofs_h + 0.1))
            //    {
            //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("NG料仓X轴插入NG料仓，NG料Z轴移动距离{0}大于{1}!", (Math.Abs(targe_pos - MT.AXIS_DL_NG_Z.fenc_pos)), COM.traybox_ng.tray_feed_ofs_h));
            //        return EM_RES.MOVE_PROTECT;
            //    }
            //}
         
            //if (id == MT.AXIS_DL_OK_Z.id)
            //{
            //    if ((Math.Abs(MT.AXIS_DL_OK_X.fenc_pos) > COM.traybox_ok.fd_safe_x+1) && (Math.Abs(targe_pos - MT.AXIS_DL_OK_Z.fenc_pos) > COM.traybox_ok.tray_feed_ofs_h + 0.1))
            //    {
            //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("OK料仓X轴插入OK料仓，OK料Z轴移动距离{0}大于{1}!", (Math.Abs(targe_pos - MT.AXIS_DL_OK_Z.fenc_pos)), COM.traybox_ok.tray_feed_ofs_h));
            //        return EM_RES.MOVE_PROTECT;
            //    }
            //}

            //检查料仓物料
            //if (id == MT.AXIS_UL_FD_X.id)
            //{
            //    if ((Math.Abs(MT.AXIS_UL_FD_X.fenc_pos) > COM.traybox_fd.fd_safe_x) && MT.GPIO_IN_UL_INP_FD_TRAYBOX.isON)
            //    {
            //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("供料仓X轴插入OK料仓，OK料Z轴移动距离{0}超过{1}!", (Math.Abs(targe_pos - MT.AXIS_UL_FD_Z.fenc_pos)), COM.traybox_fd.tray_feed_ofs_h));
            //        return EM_RES.MOVE_PROTECT;
            //    }
            //}
           
           
            #region old
            //if (AXIS_Z.id == id) return EM_RES.OK;

            //Z在原点且坐标接近0
            //if ((!AXIS_Z.isORG || (AXIS_Z.home_status != AXIS.HOME_STA.OK && Math.Abs(AXIS_Z.fcmd_pos) > 1)))
            //{
            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "Z不在原点，禁止移动载台!");
            //    return CONST.RES_MOVE_PROTECT;
            //}
            //if (!AXIS_Z.isORG && (AXIS_Z.home_status != AXIS.HOME_STA.OK && Math.Abs(AXIS_Z.fcmd_pos) > (TD.dt_pos_up + 0.1)))
            //{
            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "Z不在原点，禁止移动载台!");
            //    return EM_RES.MOVE_PROTECT;
            //}

            ////FDH在上位
            //if ((GPIO_IN_FDH_L_U.isOFF && GPIO_IN_FDH_L_L.isOFF) || (GPIO_IN_FDH_R_U.isOFF && GPIO_IN_FDH_R_R.isOFF))
            //{
            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "搬料未抬起，禁止移动载台!");
            //    return EM_RES.MOVE_PROTECT;
            //}

            ////R限制
            //if (AXIS_R.id == id && Math.Abs(AXIS_R.fenc_pos - targe_pos) > 3)
            //{
            //    if (AXIS_Y.fenc_pos > (AXIS_Y.slp - 50))
            //    {
            //        double[] posInf = { -80, -100, -260, -290 };
            //        for (int n = 0; n < posInf.Length; n++)
            //        {
            //            if (Math.Min(AXIS_R.fenc_pos, targe_pos) < posInf[n] && posInf[n] < Math.Max(AXIS_R.fenc_pos, targe_pos))
            //                return EM_RES.MOVE_PROTECT;
            //        }
            //    }
            //}
            #endregion
            return EM_RES.OK;
        }

        public static EM_RES ChkSafeSen(int id = 0, double target_pos = double.MaxValue)
        {
            //if (GPIO_IN_FR_DOOR.isOFF) return EM_RES.SAFE_PROTECT;

            //if (GPIO_IN_BK_DOOR_L.isOFF)
            //{
            //    GPIO_IN_BK_DOOR_L.AssertOFF();
            //    return EM_RES.SAFE_PROTECT;
            //}
            //if (GPIO_IN_BK_DOOR_R.isOFF)
            //{
            //    GPIO_IN_BK_DOOR_R.AssertOFF();
            //    return EM_RES.SAFE_PROTECT;
            //}

            //if (GPIO_IN_EMG.isOFF) return EM_RES.EMG;
            return EM_RES.OK;
        }
        public static bool isSafeSen
        {
            get
            {
                if (ChkSafeSen() == EM_RES.OK) return true;
                return false;
            }
        }

        #endregion
        #region 板卡初始化
        public static bool isReady
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
        public static EM_RES Init(String filename = "", int timeout = 15000)
        {
            if (isReady) return EM_RES.OK;

            //start init
            List<Task> ListTask = new List<Task>();
            foreach (CARD card in CardList)
            {
                if (!card.isReady)
                {
                    Task TaskHWInit = new Task(() =>
                    {
                        card.Init();
                    });
                    ListTask.Add(TaskHWInit);
                    TaskHWInit.Start();
                }
            }

            if (timeout == 0) return EM_RES.OK;

            //wait
            bool bEnd = true;
            int time = 0;
            do
            {
                //timer
                time += 10;
                if (time >= timeout) return EM_RES.TIMEOUT;

                //check task
                bEnd = true;
                foreach (Task task in ListTask)
                {
                    if (!task.IsCompleted) bEnd = false;
                }
                if (bEnd) break;

                //delay
                Thread.Sleep(10);
                Application.DoEvents();

            } while (true);

            if (isReady) return EM_RES.OK;
            else return EM_RES.ERR;
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
            }

        }
        public static bool AllAxSvrOn()
        {
            bool all_on = true;
            foreach (CARD card in CardList)
            {
                if (card.AxList == null) continue;
                foreach (AXIS ax in card.AxList)
                {
                    if (ax.mt_type != AXIS.MT_TYPE.VIRTUAL && !ax.isSVRON)
                    {
                        all_on = false;
                        ax.SVRON = true;
                    }
                }
            }
            return all_on;
        }
        #endregion
        #region 先升Z再定位
        public static EM_RES ZupMove(ref bool bquit, ref AXIS ax_x, double xpos, int time_out_ms = 10000, bool bdoevent = false)
        {
            AXIS ax_null = null;
            return ZupMove(ref bquit, ref ax_x, xpos, ref ax_null, 0, ref ax_null, 0, ref ax_null, 0, time_out_ms, bdoevent);
        }
        public static EM_RES ZupMove(ref bool bquit, ref AXIS ax_x, double xpos, ref AXIS ax_y, double ypos, int time_out_ms = 10000, bool bdoevent = false)
        {
            AXIS ax_null = null;
            return ZupMove(ref bquit, ref ax_x, xpos, ref ax_y, ypos, ref ax_null, 0, ref ax_null, 0, time_out_ms, bdoevent);
        }
        public static EM_RES ZupMove(ref bool bquit, ref AXIS ax_x, double xpos, ref AXIS ax_y, double ypos, ref AXIS ax_z, double zpos, int time_out_ms = 10000, bool bdoevent = false)
        {
            AXIS ax_null = null;
            return ZupMove(ref bquit, ref ax_x, xpos, ref ax_y, ypos, ref ax_z, zpos, ref ax_null, 0, time_out_ms, bdoevent);
        }
        public static EM_RES ZupMove(ref bool bquit, ref AXIS ax_x, double xpos, ref AXIS ax_y, double ypos, ref AXIS ax_z, double zpos, ref AXIS ax_r, double rpos, int time_out_ms = 10000, bool bdoevent = false)
        {
            EM_RES ret;
            bool bz_up = false;

            if (ax_x != null && ax_x.m_id != MT.AXIS_UL_Z.m_id)
            {
                bz_up = true;
            }

            if (ax_y != null && ax_x.m_id != MT.AXIS_UL_Z.m_id)
            {
                bz_up = true;
            }

            if (ax_z != null && ax_x.m_id != MT.AXIS_UL_Z.m_id)
            {
                bz_up = true;
            }

            if (ax_r != null && ax_x.m_id != MT.AXIS_UL_Z.m_id)
            {
                bz_up = true;
            }

            if (bz_up)
            {
                ret = Move(ref bquit, ref MT.AXIS_UL_Z, 0, time_out_ms, bdoevent);
                if (ret != EM_RES.OK) return ret;
            }

            ret = Move(ref bquit, ref ax_x, xpos, ref ax_y, ypos, ref ax_z, zpos, ref ax_r, rpos, time_out_ms, bdoevent);
            return ret;
        }
        #endregion
        #region 定位
        public static EM_RES MoveHandle(ref bool bquit, bool bEnX, bool bEnY, bool bEnZ, bool bEnA,bool bIsU1, ref ST_XYZA StPos, int Delay = 500)
        {
            EM_RES ret;
            AXIS ax_x = null, ax_y = null, ax_z = null, ax_u = null, ax_null = null;
            if (bEnX) ax_x = MT.AXIS_UL_X;
            if (bEnY) ax_y = MT.AXIS_UL_Y;
            if (bEnZ) ax_z = MT.AXIS_UL_Z;
            if (bEnA && bIsU1) ax_u = MT.AXIS_UL_U1;
            if (bEnA && !bIsU1) ax_u = MT.AXIS_UL_U2;
            //Z提起
            //if (Math.Abs(MT.AXIS_UL_Z.fenc_pos) > 0.5)
            //{
            //    ret = UploadModle.ZHome(ref bquit);
            //    if (ret != EM_RES.OK) return ret;
            //}
            ret = Move(ref bquit, ref ax_null, 0, ref ax_null, 0, ref MT.AXIS_UL_Z,0, ref ax_null, 0);
            if (ret != EM_RES.OK) return ret;
            //定位
            ret = Move(ref bquit, ref ax_x, StPos.x, ref ax_y, StPos.y, ref ax_null, 0, ref ax_u, StPos.a);
            if (ret != EM_RES.OK) return ret;
            //Z下降
            ret = Move(ref bquit, ref ax_null,0, ref ax_null,0, ref ax_z, StPos.z, ref ax_null,0);
            if (ret != EM_RES.OK) return ret;
            Thread.Sleep(Delay);
            if (ax_x != null) StPos.x = ax_x.fenc_pos;
            if (ax_y != null) StPos.y = ax_y.fenc_pos;
            if (ax_z != null) StPos.z = ax_z.fenc_pos;
            return EM_RES.OK;
        }
        public static EM_RES Move(ref bool bquit, ref AXIS ax_x, double xpos, int time_out_ms = 10000, bool bdoevent = false)
        {
            AXIS ax_null = null;
            return Move(ref bquit, ref ax_x, xpos, ref ax_null, 0, ref ax_null, 0, ref ax_null, 0, time_out_ms, bdoevent);
        }
        public static EM_RES Move(ref bool bquit, ref AXIS ax_x, double xpos, ref AXIS ax_y, double ypos, int time_out_ms = 10000, bool bdoevent = false)
        {
            AXIS ax_null = null;
            return Move(ref bquit, ref ax_x, xpos, ref ax_y, ypos, ref ax_null, 0, ref ax_null, 0, time_out_ms, bdoevent);
        }
        public static EM_RES Move(ref bool bquit, ref AXIS ax_x, double xpos, ref AXIS ax_y, double ypos, ref AXIS ax_z, double zpos, int time_out_ms = 10000, bool bdoevent = false)
        {
            AXIS ax_null = null;
            return Move(ref bquit, ref ax_x, xpos, ref ax_y, ypos, ref ax_z, zpos, ref ax_null, 0, time_out_ms, bdoevent);
        }
        public static EM_RES Move(ref bool bquit, ref AXIS ax_x, double xpos, ref AXIS ax_y, double ypos, ref AXIS ax_z, double zpos, ref AXIS ax_r, double rpos, int time_out_ms = 10000, bool bdoevent = false)
        {
            EM_RES ret = EM_RES.OK;
            //start move
            if (ax_x != null)
            {
                ret = ax_x.MoveTo(ref bquit, xpos);
                if (ret != EM_RES.OK)
                    goto MOVE_END;
            }
            if (ax_y != null)
            {
                ret = ax_y.MoveTo(ref bquit, ypos);
                if (ret != EM_RES.OK) goto MOVE_END;

            }
            if (ax_z != null)
            {
                ret = ax_z.MoveTo(ref bquit, zpos);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (ax_r != null)
            {
                ret = ax_r.MoveTo(ref bquit, rpos);
                if (ret != EM_RES.OK) goto MOVE_END;
            }

            //wait
            if (ax_x != null)
            {
                ret = ax_x.WaitForMoveDone(ref bquit, xpos, time_out_ms, bdoevent);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (ax_y != null)
            {
                ret = ax_y.WaitForMoveDone(ref bquit, ypos, time_out_ms, bdoevent);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (ax_z != null)
            {
                ret = ax_z.WaitForMoveDone(ref bquit, zpos, time_out_ms, bdoevent);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (ax_r != null)
            {
                ret = ax_r.WaitForMoveDone(ref bquit, rpos, time_out_ms, bdoevent);
                if (ret != EM_RES.OK) goto MOVE_END;
            }

            return EM_RES.OK;

            MOVE_END:
            if (ax_x != null) ax_x.Stop();
            if (ax_y != null) ax_y.Stop();
            if (ax_z != null) ax_z.Stop();
            if (ax_r != null) ax_r.Stop();
            return ret;
        }
        #endregion
        #region 蜂鸣器
        public static EM_RES Beeper(int tmr)
        {
            Task beep = new Task(() =>
                {
                    if (tmr > 0)
                    {
                        EM_RES ret = GPIO_OUT_ALM_BEEPER.SetOn();
                        if (ret == EM_RES.OK)
                        {
                            Thread.Sleep(tmr);
                            ret = GPIO_OUT_ALM_BEEPER.SetOff();
                        }
                    }
                }
            );
            beep.Start();
            return EM_RES.OK;
        }
        #endregion
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
                    if (ax != null && !ax.HomeTaskisEnd) bok = false;
                }
                if (bok) break;

                Application.DoEvents();
                Thread.Sleep(10);

                //quit
                if (bquit)
                {
                    foreach (AXIS ax in axs)
                    {
                        if (ax != null && !ax.HomeTaskisEnd) ax.Stop();
                    }
                    return EM_RES.ERR;
                }
            }

            //check result
            bok = true;
            foreach (AXIS ax in axs)
            {
                if (ax != null && ax.home_status != AXIS.HOME_STA.OK) bok = false;
            }
            if (bok == false)
            {
                foreach (AXIS ax in axs)
                {
                    if (ax != null && !ax.HomeTaskisEnd) ax.Stop();
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
        #region 归位或避让
        public static EM_RES gotopos(bool breset = true, bool brun = false)
        {
            EM_RES ret = EM_RES.OK;

            ////检查是否需要移动
            //bool bneedmove = false;
            //if (breset && ((Math.Abs(MT.AXIS_X.fenc_pos) > 0.05))) bneedmove = true;
            //if (breset && ((Math.Abs(MT.AXIS_Y.fenc_pos) > 0.05))) bneedmove = true;
            //if (breset && ((Math.Abs(MT.AXIS_R.fenc_pos) > 0.05))) bneedmove = true;
            //if (!bneedmove) return CONST.RES_OK;

            ////safe check
            //ret = ChkSafeSen();
            //if (ret != CONST.RES_OK) return ret;

            ////正在进出料，不能移动
            //if (!brun && FDH.binuse)
            //{
            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "出料中gotopos定位");
            //    Thread.Sleep(10);
            //    if (FDH.binuse) return CONST.RES_MOVE_PROTECT;
            //}

            ////运行中，不能移动
            //if (COM.MounterGetRunStatus() == CONST.SYS_STATUS_RUN)
            //{
            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "运行中gotopos定位");
            //    return CONST.RES_ERR;
            //}

            //if (brun || VAR.gsys_set.status == CONST.SYS_STATUS_STANDBY || VAR.gsys_set.status == CONST.SYS_STATUS_PAUSE)
            //{
            //    VAR.gsys_set.bquit = false;
            //    ret = MT.ChkSafeSen();
            //    if (ret != CONST.RES_OK) return ret;

            //    if (VAR.gsys_set.status == CONST.SYS_STATUS_STANDBY) VAR.sys_inf.Set(CONST.EM_ALM_STA.NOR_GREEN, "就绪", -1, true);
            //    else if (VAR.gsys_set.status == CONST.SYS_STATUS_PAUSE) VAR.sys_inf.Set(CONST.EM_ALM_STA.NOR_GREEN, "暂停", -1, true);

            //    //set to workspd
            //    foreach (AXIS ax in AxisListExceptFd) ax.SetToWorkSpd();

            //    try
            //    {
            //        bgotopos = true;
            //        if (breset)
            //        {
            //            ret = ZupMove(ref VAR.gsys_set.bquit, ref AXIS_X, 0, ref AXIS_Y, 0, ref AXIS_R, 0, 10000, true);
            //            if (ret != CONST.RES_OK) return CONST.RES_ERR;
            //        }
            //        else
            //        {
            //            ret = ZupMove(ref VAR.gsys_set.bquit, ref AXIS_X, FDH.st_pos_ready.x, ref AXIS_Y, COM.MTER1.pos_br_y, ref AXIS_R, 0, 10000, true);
            //            if (ret != CONST.RES_OK) return CONST.RES_ERR;
            //        }
            //    }
            //    finally
            //    {
            //        bgotopos = false;
            //    }
            //}
            //else
            //{
            //    if (breset) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "非待机状态禁止复位！");
            //    else VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "非待机状态禁止定位避让位!");
            //    return CONST.RES_ERR;
            //}
            return ret;
        }
        #endregion
        #region 相机触发
        public static EM_RES CamUp1Triger(int dly = 100)
        {
            EM_RES res = GPIO_OUT_UL_CAM_BK.SetOn();
            Thread.Sleep(dly);
            res = GPIO_OUT_UL_CAM_BK.SetOff();
            //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, "CamUp1 Triger");
            return res;
        }
        public static EM_RES CamUp2Triger(int dly = 100)
        {
            EM_RES res = GPIO_OUT_UL_CAM_FR.SetOn();
            Thread.Sleep(dly);
            res = GPIO_OUT_UL_CAM_FR.SetOff();
            //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, "CamUp2 Triger");
            return res;
        }
        public static EM_RES CamDownTriger(int dly = 100)
        {
            EM_RES res = GPIO_OUT_UL_CAM_DW.SetOn();
            Thread.Sleep(dly);
            res = GPIO_OUT_UL_CAM_DW.SetOff();
            //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, "CamDown Triger");
            return res;
        }
        #endregion
    }
}