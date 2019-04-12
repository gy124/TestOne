using System;
using System.IO;
using System.ComponentModel;

namespace MotionCtrl
{
    #region 基本结构
    public struct ST_XY
    {
        public double x;
        public double y;
        public ST_XY(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
        }
        public ST_XY(string str)
        {
            x = 0;
            y = 0;
            FrString(str);
        }
        public ST_XYA ToXYA(double a = 0)
        {
            ST_XYA xya = new ST_XYA();
            xya.x = x;
            xya.y = y;
            xya.a = a;
            return xya;
        }
        public ST_XYZ ToXYZ(double z = 0)
        {
            ST_XYZ xyz = new ST_XYZ();
            xyz.x = x;
            xyz.y = y;
            xyz.z = z;
            return xyz;
        }
        public ST_XYZA ToXYZA(double z = 0, double a = 0)
        {
            ST_XYZA xyza = new ST_XYZA();
            xyza.x = x;
            xyza.y = y;
            xyza.z = z;
            xyza.a = a;
            return xyza;
        }
        public override string ToString()
        {
            return string.Format(" X{0:F3},Y{1:F3} ", x, y);
        }
        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref x)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref y)) n++;
            return n;
        }
        public static ST_XY operator +(ST_XY lhs, ST_XY rhs)
        {
            return new ST_XY(lhs.x + rhs.x, lhs.y + rhs.y);
        }
        public static ST_XY operator -(ST_XY lhs, ST_XY rhs)
        {
            return new ST_XY(lhs.x - rhs.x, lhs.y - rhs.y);
        }
    }
    public struct ST_XZ
    {
        public double x;
        public double z;
        public ST_XZ(double x = 0, double z = 0)
        {
            this.z = z;
            this.x = x;
        }

        public ST_XZ(string str)
        {
            x = 0;
            z = 0;
            FrString(str);
        }
        public override string ToString()
        {
            return string.Format(" X{0:F3},Z{1:F3} ", x, z);
        }
        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref x)) n++;
            if (Utility.GetDoubleFrStr(str, 'Z', ref z)) n++;
            return n;
        }
    }
    public struct ST_YZ
    {
        public double y;
        public double z;
        public ST_YZ(double y = 0, double z = 0)
        {
            this.z = z;
            this.y = y;
        }

        public ST_YZ(string str)
        {
            z = 0;
            y = 0;
            FrString(str);
        }
        public override string ToString()
        {
            return string.Format(" Y{0:F3},Z{1:F3} ", y, z);
        }
        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'Z', ref z)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref y)) n++;
            return n;
        }
    }
    public struct ST_XYZ
    {
        public double x;
        public double y;
        public double z;
        public ST_XYZ(double x = 0, double y = 0, double z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public ST_XYZ(string str)
        {
            x = 0;
            y = 0;
            z = 0;
            FrString(str);
        }
        public ST_XY ToXY()
        {
            ST_XY xy = new ST_XY();
            xy.x = x;
            xy.y = y;
            return xy;
        }
        public ST_XYA ToXYA()
        {
            ST_XYA xya = new ST_XYA();
            xya.x = x;
            xya.y = y;
            xya.a = z;
            return xya;
        }
        public ST_XYZA ToXYZA(double a = 0)
        {
            ST_XYZA xyza = new ST_XYZA();
            xyza.x = x;
            xyza.y = y;
            xyza.z = z;
            xyza.a = a;
            return xyza;
        }
        public override string ToString()
        {
            return string.Format(" X{0:F3},Y{1:F3},Z{2:F3} ", x, y, z);
        }
        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref x)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref y)) n++;
            if (Utility.GetDoubleFrStr(str, 'Z', ref z)) n++;
            return n;
        }
        public static ST_XYZ operator +(ST_XYZ lhs, ST_XYZ rhs)
        {
            return new ST_XYZ(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }
        public static ST_XYZ operator -(ST_XYZ lhs, ST_XYZ rhs)
        {
            return new ST_XYZ(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }
    }
    public struct ST_XYA
    {
        public double x;
        public double y;
        public double a;
        public ST_XYA(double x = 0, double y = 0, double a = 0)
        {
            this.x = x;
            this.y = y;
            this.a = a;
        }
        public ST_XYA(string str)
        {
            x = 0;
            y = 0;
            a = 0;
            FrString(str);
        }
        public ST_XY ToXY()
        {
            ST_XY xy = new ST_XY();
            xy.x = x;
            xy.y = y;
            return xy;
        }
        public ST_XYZ ToXYZ()
        {
            ST_XYZ xyz = new ST_XYZ();
            xyz.x = x;
            xyz.y = y;
            xyz.z = a;
            return xyz;
        }
        public ST_XYZA ToXYZA(double z = 0)
        {
            ST_XYZA xyza = new ST_XYZA();
            xyza.x = x;
            xyza.y = y;
            xyza.z = z;
            xyza.a = a;
            return xyza;
        }
        public override string ToString()
        {
            return string.Format(" X{0:F3},Y{1:F3},A{2:F3} ", x, y, a);
        }
        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref x)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref y)) n++;
            if (Utility.GetDoubleFrStr(str, 'A', ref a)) n++;
            return n;
        }
        public static ST_XYA operator +(ST_XYA lhs, ST_XYA rhs)
        {
            return new ST_XYA(lhs.x + rhs.x, lhs.y + rhs.y, lhs.a + rhs.a);
        }
        public static ST_XYA operator -(ST_XYA lhs, ST_XYA rhs)
        {
            return new ST_XYA(lhs.x - rhs.x, lhs.y - rhs.y, lhs.a - rhs.a);
        }
    }

    //[TypeConvert(typeof(ExpandableObjectConverter))]
    public struct ST_XYZA
    {
        public double x;
        public double y;
        public double z;
        public double a;
        public ST_XYZA(double x = 0, double y = 0, double z = 0, double a = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.a = a;
        }
        public ST_XYZA(string str)
        {
            x = 0;
            y = 0;
            z = 0;
            a = 0;
            FrString(str);
        }
        public ST_XY ToXY()
        {
            ST_XY xy = new ST_XY();
            xy.x = x;
            xy.y = y;
            return xy;
        }
        public ST_XYZ ToXYZ()
        {
            ST_XYZ xyz = new ST_XYZ();
            xyz.x = x;
            xyz.y = y;
            xyz.z = z;
            return xyz;
        }
        public ST_XYA ToXYA()
        {
            ST_XYA xya = new ST_XYA();
            xya.x = x;
            xya.y = y;
            xya.a = a;
            return xya;
        }
        public override string ToString()
        {
            return string.Format(" X{0:F3},Y{1:F3},Z{2:F3},A{3:F3} ", x,y,z,a);
        }
        public int FrString(string str)
        {
            int n = 0;
            if (Utility.GetDoubleFrStr(str, 'X', ref x)) n++;
            if (Utility.GetDoubleFrStr(str, 'Y', ref y)) n++;
            if (Utility.GetDoubleFrStr(str, 'Z', ref z)) n++;
            if (Utility.GetDoubleFrStr(str, 'A', ref a)) n++;
            return n;
        }
        public static ST_XYZA operator +(ST_XYZA lhs, ST_XYZA rhs)
        {
            return new ST_XYZA(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.a + rhs.a);
        }
        public static ST_XYZA operator -(ST_XYZA lhs, ST_XYZA rhs)
        {
            return new ST_XYZA(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.a - rhs.a);
        }
        public ST_XYZA Clone()
        {
            return new ST_XYZA(x,y,z,y);
        }
    }
    #endregion
    #region 设备信息
    public class MCInf
    {
        /// <summary>
        /// 供应商信息
        /// Manufacturer information
        /// </summary>
        public class MFInf
        {
            public string CompName;//供应商名称
            public string Contact;//联系人
            public string ContactMode; //联系方式
            public string MFD; //制造日期
            public string Exp; //保修截止时间
            public string ps;  //备注
        }
        /// <summary>
        /// 基本参数
        /// NominalParam
        /// </summary>
        public class Param
        {
            public string Name;//设备名
            public string SN;//序列号
            public string AssetsCOde;//资产编号
            public double Lenght;//长(m)
            public double Width;//宽(m)
            public double Height;//高(m)
            public int Power;//额定功率(W)
            public int Voltage;//额定电压(V);
            public string ps;//其他备注;
        }
        /// <summary>
        /// 使用信息
        /// </summary>
        public class UserInf
        {
            public string AreaCode;//厂区编号
            public string WorkShopCode;//车间编号
            public string LineCode;// 产线编号
            public string WorkStationNum;//测试站编号
            public string SubWorkStationNum;//测试站子编号
            public string UPH;// 预定UPH
            public string Contact;//联系人
            public string ContactMode; //联系方式
            public string ps;//其他备注;
        }
        /// <summary>
        /// 设备状态
        /// </summary>
        public class Status
        {
            public string status;// 运行/停止/待料/警报/维护
            public string product;// 产品名称，规格
            public int run_time_min;//运行时间(min)
            public int stop_time_min;//停止时间(min)
            public int wait_time_min;//待料时间(min)
            public int error_time_min;//警报时间(min)
            public int maintenance_time_min;// 维护时间(min)
        }
        /// <summary>
        /// 设备维护
        /// maintenance, repair and operation
        /// </summary>
        public class MRO
        {
            public string Type;//维护类别
            public string Discription;// 实施内容
            public string Operator;// 实施人
            public string Datetime;// 实施时间
            public string Taketime;//损耗工时(h)
        }
        /// <summary>
        /// 设备功能
        /// </summary>
        public class MCFunc
        {
            public int StationNum;//工站编号
            public int TextBoxNum;// 工装编号
            public string TestFunc;// 测试项目
        }
        /// <summary>
        /// 测试结果
        /// </summary>
        public class TestResult
        {
            public int StationNum;//工站编号
            public int TextBoxNum;// 工装编号
            public string TestFunc;// 测试项目
            public int NGCode;// 测试结果代码
            public int CT;//测试用时(sec)
            public string ps;//附加结果信息
        }
        /// <summary>
        /// 测试命令
        /// </summary>
        public class TestCmd
        {
            public int StationNum;//工站编号
            public int TextBoxNum;//工装编号
            public string TestFunc;//测试项目
            public string Barcode;//模组二维码
        }
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public int Initrial()
        {
            //从配置文件加载参数进行初始化
            //建立一个客户端链接中控服务器，进行设备状态上传
            //建立一个服务端或客户端，进行机台间通信
            return 0;
        }

        /// <summary>
        /// 上传设备信息数据到中控
        /// </summary>
        /// <returns></returns>
        public int SendMCInfToCentraCtrl()
        {
            return 0;
        }
        /// <summary>
        /// 上传设备状态到中控
        /// </summary>
        /// <returns></returns>
        public int SendMCStatusToCentraCtrl()
        {
            return 0;
        }
        /// <summary>
        /// 上传设备维护记录到中控
        /// </summary>
        /// <returns></returns>
        public int SendMMROToCentraCtrl()
        {
            return 0;
        }
        /// <summary>
        /// 接受中控信息事件时执行
        /// </summary>
        /// <returns></returns>
        public int ReceFromCentraCtrl()
        {
            //根据命令响应
            return 0;
        }

        /// <summary>
        /// 发送数据给联机设备
        /// </summary>
        /// <returns></returns>
        public int SendTestResult()
        {
            return 0;
        }
        public int SendStatus()
        {
            return 0;
        }
        public int SendCommand()
        {
            return 0;
        }
        /// <summary>
        /// 接受联机设备信息事件时执行
        /// </summary>
        /// <returns></returns>
        public int Receve()
        {
            //根据命令响应
            return 0;
        }
    }
    #endregion
    #region 结果定义
    public enum EM_RES
    {
        //RESEULT
        [Description("成功")]
        OK,
        [Description("错误")]
        ERR,
        [Description("急停")]
        EMG,
        [Description("超时")]
        TIMEOUT,
        [Description("取消")]
        QUIT,
        [Description("退出")]
        ABORT,

        //FOLLOW
        [Description("BUSY")]
        BUSY,
        [Description("WAIT")]
        WAIT,
        [Description("NEXT")]
        NEXT,
        [Description("RETRY")]
        RETRY,
        [Description("END")]
        END,

        //PARAM
        [Description("参数异常")]
        PARA_ERR,
        [Description("参数超范围")]
        PARA_OUTOFRANG,

        //CAM
        [Description("相机错误")]
        CAM_ERR,
        [Description("相机未初始化")]
        CAM_INIT_ERR,
        [Description("相机连接错误")]
        CAM_LINK_ERR,
        [Description("相机参数错误")]
        CAM_PARA_ERR,
        [Description("相机任务加载错误")]
        CAM_TASK_LOAD_ERR,
        [Description("相机重测错误")]
        CAM_RECHK_ERR,
        [Description("CAM_REMARK")]
        CAM_REMARK,

        //FUCTION
        [Description("取料错误")]
        PICK_ERR,
        [Description("放料错误")]
        PLACE_ERR,

        //PROTECT
        [Description("移动保护")]
        MOVE_PROTECT,
        [Description("安全保护")]
        SAFE_PROTECT,
        [Description("IO保护")]
        GPIO_PROTECT,
        [Description("旋转保护")]
        ROL_PROTECT
    }
    #endregion
    #region 运行模式
    public enum EM_SYS_MODE
    {
        NORMAL = 0x01,
        DEMO = 0x02,
        CHK = 0x04,//点检模式
        STEP = 0x10,
        ONE = 0x20,
        CONTINUE = 0x40,
    }
    #endregion
    #region 系统状态
    public enum EM_SYS_STA
    {
        UNKOWN,
        STANDBY,
        RUN,
        WARNING,
        ERR,
        PAUSE,
        RESET,
        EMG,
    }
    #endregion
    #region 报警信息
    public enum EM_ALM_STA
    {
        NOR_GREEN,
        NOR_GREEN_FLASH,
        NOR_BLUE,
        NOR_BLUE_FLASH,
        WAR_YELLOW,
        WAR_YELLOW_FLASH,
        WAR_RED,
        WAR_RED_FLASH,
    }
    #endregion
    #region 视觉数据
    public class VS_DAT
    {
        public int cs;              //对应流程编号
        public int id;              //目标编号
        public ST_XYZA st_cap_pos;  //拍照位置
        public ST_XYA st_pix;	    //像素坐标
        public ST_XYA st_mm;	    //mm坐标
        public ST_XY st_center_ofs;	//与画面中心偏差
        public ST_XYZA st_temp;     //备用
        public string str_barcode;  //读取二维码数据
        public bool bOK;		    //结果
        public bool bupdate;	    //更新标志
        public int ct_ms;           //CT时间
        //public ICogImage outputImage;//输出图像
        //public CogGraphicCollection GraphicCollection;//输出界面绘图
        //public CogCompositeShape GraphicsPMAlignTool;//输出PMAlignTool匹配图形
        //public List<CogPointMarker> ListTopCamera;
    }
    #endregion
    #region 系统信息
    public class SYS_SET
    {
        //机型设置
        public int mc_sel;
        public int hw_ver;
        public String mc_name;
        public String mc_disc;

        //当前产品名
        public String cur_product_name;
        //使能蜂鸣器
        public bool beep_en;
        //蜂鸣器时间
        public int beep_tmr;
        //使能触摸屏操作
        public bool touch_en;
        //当前模式
        public EM_SYS_MODE mode;
        //当前状态
        public EM_SYS_STA status;

        //退出标记
        public bool bquit;
        //程序关闭
        public bool bclose;
        //程序暂停
        public bool bpause;
        //当前IP
        public string str_cur_ip;
        //前机IP
        public string str_pre_ip;
        //后机IP
        public string str_next_ip;

        //信息设置
        public int log_cfg;

        public bool blight_always_on;
        public bool isChkMode { get { return ((mode & EM_SYS_MODE.CHK) == EM_SYS_MODE.CHK); } }
        public bool isDemoMode { get { return ((mode & EM_SYS_MODE.DEMO) == EM_SYS_MODE.DEMO); } }
        public bool isStepMode { get { return ((mode & EM_SYS_MODE.STEP) == EM_SYS_MODE.STEP); } }
        public bool isContinueMode { get { return ((mode & EM_SYS_MODE.CONTINUE) == EM_SYS_MODE.CONTINUE); } }
        public bool isNormalMode { get { return ((mode & EM_SYS_MODE.NORMAL) == EM_SYS_MODE.NORMAL); } }

        public bool isStandby { get { return (status == EM_SYS_STA.STANDBY); } }
        
        public string GetSysCfgPath { get { return string.Format("{0}\\syscfg\\", Path.GetFullPath("..")); } }
        public string GetSysProductPath { get { return string.Format("{0}\\product\\", Path.GetFullPath("..")); } }
        public string GetCurProductPath { get { return string.Format("{0}\\product\\{1}\\", Path.GetFullPath(".."), cur_product_name); } }

        #region 系统设置
        public void LoadSysCfg(string filename = "")
        {
            if (filename.Length < 3) filename = Path.GetFullPath("..") + "\\syscfg\\syscfg.ini";
            IniFile inf = new IniFile(filename);

            //当前产品ID
            cur_product_name = inf.ReadString("PRODUCT_SET", "CUR_PRODCUT_NAME", "");

            //信息处理
            log_cfg = inf.ReadInteger("OTHER_SET", "MSG_CFG", 0);

            mc_sel = inf.ReadInteger("MC_SEL", "MC", 0);
            mc_name = inf.ReadString("MC_SEL", "NAME", "");
            mc_disc = inf.ReadString("MC_SEL", "DISC", "");
            hw_ver = inf.ReadInteger("MC_SEL", "HW_VER", 0);
        }

        public void SaveSysCfg(string filename = "")
        {
            if (filename.Length < 3) filename = Path.GetFullPath("..") + "\\syscfg\\syscfg.ini";
            IniFile inf = new IniFile(filename);

            //当前产品ID
            inf.WriteString("PRODUCT_SET", "CUR_PRODCUT_NAME", cur_product_name);
            //信息处理
            inf.WriteInteger("OTHER_SET", "MSG_CFG", log_cfg);
        }
        #endregion
    }
    #endregion
}