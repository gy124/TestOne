using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionCtrl;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AZD;

namespace MotionCtrl
{
    /// <summary>
    /// 板卡类，统一板卡的API
    /// 创建：李大源 @2018/08/02
    /// </summary>
    public class CARD
    {
        #region 参数
        /// <summary>
        /// 是否已初始化
        /// </summary>
        bool isInit;
        /// <summary>
        /// 描述
        /// </summary>
        public string str_disc;
        public string ps;
        /// <summary>
        /// 板块是否初始化就绪
        /// </summary>
        public bool isReady
        {
            get
            {
                return (isInit && ((long)handle) != 0);
            }
        }
        /// <summary>
        /// 轴数
        /// </summary>
        public int ax_num;
        /// <summary>
        /// 输入口数量
        /// </summary>
        public int input_num;
        /// <summary>
        /// 输出口数量
        /// </summary>
        public int output_num;
        /// <summary>
        /// 卡ID,不能重复
        /// </summary>
        public int id { get { return m_id; } }
        public int m_id;
        /// <summary>
        /// 主卡ID
        /// </summary>
        public int maincard_id = -1;
        /// <summary>
        /// 卡ID
        /// </summary>
        public int card_id = -1;
        /// <summary>
        /// 卡IP地址
        /// </summary>
        public string ip = "";
        /// <summary>
        /// 卡句柄
        /// </summary>
        public IntPtr handle;
        /// <summary>
        /// 卡描述
        /// </summary>
        public string disc
        {
            get
            {
                if (PortName.Length > 3 && Baudrate != 0)
                {
                    return string.Format("{0}({1}/{2})", str_disc, PortName, Baudrate);
                }
                else
                {
                    return string.Format("{0}({1})", str_disc, ip.Length > 0 ? ip : card_id.ToString());
                }
            }
        }
        /// <summary>
        /// 品牌定义 雷赛，正运动，研华
        /// </summary>
        public enum BRAND { LEADSHINE, ZMOTION, ADVANTTECH, GOOGOLTECH, ORIENTALMOTOR, NULL };
        /// <summary>
        /// 板卡类型定义 运动板卡，IO板卡，CANIO扩展卡
        /// </summary>
        public enum TYPE { MOTION, IO, CAN_IO, RS485, NULL };
        /// <summary>
        /// 板卡品牌
        /// </summary>
        public BRAND brand = BRAND.NULL;
        /// <summary>
        /// 板卡类型
        /// </summary>
        public TYPE type = TYPE.NULL;
        /// <summary>
        /// RS485 COM口名称
        /// </summary>
        public string PortName = "";
        /// <summary>
        /// RS485 COM波特率
        /// </summary>
        public int Baudrate;
        public List<AXIS> AxList = new List<AXIS>();
        public List<GPIO> GPIOList = new List<GPIO>();

        public class IOSTA
        {
            /// <summary>
            /// 输入IO
            /// </summary>
            public int[] input;
            /// <summary>
            /// 输出IO
            /// </summary>
            public int[] output;
            /// <summary>
            /// 时间戳
            /// </summary>
            public long tickcnt;
            public IOSTA()
            {
                input = new int[2];
                output = new int[2];
            }
        }
        /// <summary>
        /// 板卡状态Buffer
        /// </summary>
        public IOSTA CardIOBuf = new IOSTA();
        public EM_RES UpdateIOBuff(bool bforceupdate = true)
        {
            if (!isReady || CardIOBuf == null) return EM_RES.ERR;

            //check time
            if (!bforceupdate && Math.Abs(VAR.msg.sw.ElapsedMilliseconds - CardIOBuf.tickcnt) < 20)
                return EM_RES.OK;

#if ZMOTION
            long t = VAR.msg.sw.ElapsedMilliseconds;
            List<int> list_temp = new List<int>();
            int ret = 0;

            ret = zmcaux.ZAux_Direct_GetIOSta(handle, ref list_temp);
            if (ret == 0 && list_temp.Count == 4)
            {
                CardIOBuf.input[0] = list_temp.ElementAt(0);
                CardIOBuf.output[0] = list_temp.ElementAt(1);
                CardIOBuf.input[1] = list_temp.ElementAt(2);
                CardIOBuf.output[1] = list_temp.ElementAt(3);
                CardIOBuf.tickcnt = VAR.msg.sw.ElapsedMilliseconds;
                //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} update IO,T{1}ms", disc, CardIOBuf.tickcnt - t));
            }
            else
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} update IO ,Err:{1},Cnt:{2},T{3}ms", disc, ret, list_temp.Count, CardIOBuf.tickcnt - t));
                return EM_RES.ERR;
            }
#endif
            return EM_RES.OK;
        }
        #region 从缓存提取IO状态
        public GPIO.IO_STA GetIOFrBuff(int num, GPIO.IO_DIR dir)
        {
            if (CardIOBuf == null) return GPIO.IO_STA.ERR;

            int wnum = num / 32;
            int bitofs = num % 32;
            if (wnum < 0 || wnum > CardIOBuf.input.Length) return GPIO.IO_STA.ERR;

            if (dir == GPIO.IO_DIR.IN)
            {

                return (CardIOBuf.input[wnum] >> bitofs & 1) == 1 ? GPIO.IO_STA.IN_ON : GPIO.IO_STA.IN_OFF;
            }
            else
            {
                return (CardIOBuf.output[wnum] >> bitofs & 1) == 1 ? GPIO.IO_STA.OUT_ON : GPIO.IO_STA.OUT_OFF;
            }
        }
        #endregion

        //避免连续显示错误信息
        bool bshowmsg = false;
        #endregion
        #region 构造
        public CARD(int id, int card_id, int ax_num, int input_num, int output_num, BRAND brand, TYPE type, string disc, string ps = "")
        {
            this.m_id = id;
            this.card_id = card_id;
            this.ax_num = ax_num;
            this.input_num = input_num;
            this.output_num = output_num;
            this.brand = brand;
            this.type = type;
            this.ps = ps;
            isInit = false;
            handle = (IntPtr)0;
            str_disc = disc;
        }
        public CARD(int id, string ip, int ax_num, int input_num, int output_num, BRAND brand, TYPE type, string disc, string ps = "")
        {
            this.m_id = id;
            this.ip = ip;
            this.ax_num = ax_num;
            this.input_num = input_num;
            this.output_num = output_num;
            this.brand = brand;
            this.type = type;
            this.ps = ps;
            isInit = false;
            handle = (IntPtr)0;
            str_disc = disc;
        }
        public CARD(int id, string port_name, int baudrate, BRAND brand, TYPE type, string disc, string ps = "")
        {
            this.m_id = id;
            this.ax_num = 10000;
            this.input_num = 10000;
            this.output_num = 10000;
            this.brand = brand;
            this.type = type;
            this.ps = ps;
            isInit = false;
            handle = (IntPtr)0;
            str_disc = disc;
            PortName = port_name;
            Baudrate = baudrate;
        }
        #endregion
        #region 初始化
        /// <summary>
        /// 初始化板卡。
        /// 如有主次卡之分，请先初始化主卡。关闭则相反，先关从卡再关主卡。
        /// </summary>
        /// <param name="id">卡ID</param>
        /// <param name="maincard_id">主卡ID</param>
        /// <param name="ip">卡IP</param>
        /// <param name="filename">配置文件路径</param>
        /// <returns></returns>
        public EM_RES Init(int card_id = -1, int maincard_id = -1, string ip = "", string filename = "")
        {
            if (isReady) return EM_RES.OK;
            int ret = 0;
            EM_RES res = EM_RES.OK;
            //update id
            if (card_id >= 0) this.card_id = card_id;
            else card_id = this.card_id;
            //update ip
            if (ip.Length > 7) this.ip = ip;
            else ip = this.ip;
            //update maincard_id
            if (maincard_id >= 0) this.maincard_id = maincard_id;
            else maincard_id = this.maincard_id;

#if LEADSHINE
            if (brand == BRAND.LEADSHINE)
            {
                if (type == TYPE.MOTION)
                {
                    //init card
                    ret = LTDMC.dmc_board_init();
                    if (ret < 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}ID有重复，请检查确认!0x{1:X8}", disc, ret));
                        return EM_RES.ERR;
                    }

                    //get card list
                    ushort cardnum = 0;
                    ushort[] cardid = new ushort[64];
                    uint[] cardtype = new uint[64];
                    ret = LTDMC.dmc_get_CardInfList(ref cardnum, cardtype, cardid);
                    if (ret != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("获取{0}列表错误，0x{1:X8}", disc, ret));
                        return EM_RES.ERR;
                    }

                    //search card list
                    bool bfound = false;
                    for (int n = 0; n < cardnum; n++)
                    {
                        if (card_id == cardid[n])
                        {
                            bfound = true;
                            break;
                        }
                    }
                    if (!bfound)
                    {
                        isInit = false;
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}列表找不到ID为{1}的板卡!", disc, card_id));
                        return EM_RES.ERR;
                    }

                    //download config
                    res = DownLoadFile(filename);
                    if (res != EM_RES.OK) return res;

                    isInit = true;
                    handle = (IntPtr)1;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}初始化成功!", disc));
                    return EM_RES.OK;
                }
                else if (type == TYPE.CAN_IO)
                {
                    //连接IO卡
                    ret = LTDMC.dmc_set_can_state((ushort)maincard_id, (ushort)card_id, 1, 0);
                    if (ret != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}连接失败,Err:0x{1:X8},重新连接...", disc, ret));
                        ret = LTDMC.dmc_set_can_state((ushort)maincard_id, (ushort)card_id, 1, 0);
                        if (ret != 0)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}重新连接失败,请确认卡连接是否正确!Err:0x{1:X8}", disc, ret));
                            return EM_RES.ERR;
                        }
                    }
                    isInit = true;
                    handle = (IntPtr)1;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}初始化成功!", disc));
                    return EM_RES.OK;
                }
#if LEADSHINE_IO
                else if (type == TYPE.IO)
                {
                    ret = IOC0640.ioc_board_init();
                    if (ret > 0)
                    {
                        isInit = true;
                        handle = (IntPtr)1;
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}初始化成功!", disc));
                        return EM_RES.OK;
                    }
                    else
                    {
                        IOC0640.ioc_board_close();
                        isInit = false;
                        handle = (IntPtr)0;
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化失败!Err:0x{1:X8}", disc, ret));
                        return EM_RES.ERR;
                    }
                }
#endif
            }
#endif
#if ZMOTION
            if (brand == BRAND.ZMOTION)
            {
                //check ip
                if (ip.Length < 7)
                {
                    isInit = false;
                    handle = (IntPtr)0;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0} IP异常,{1}", disc, ip));
                    return EM_RES.CAM_PARA_ERR;
                }

                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0} 连接...", disc));
                ////search and open,多网卡时，搜索异常
                //for (int n = 0; n < 10; n++)
                //{
                //    ret = zmcaux.ZAux_SearchEth(ip, 100);
                //    if (ret == 0) break;
                //    Application.DoEvents();
                //}
                ret = 0;
                if (ret == 0)
                {
                    ret = zmcaux.ZAux_OpenEth(ip, out handle);
                    if (ret != 0)
                    {
                        isInit = false;
                        handle = (IntPtr)0;
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化失败,IP={1},Err:{2}", disc, ip, ret));
                        return EM_RES.ERR;
                    }
                    else
                    {
                        //check param
                        int ftemp = 0;
                        bool bdownload = false;
                        foreach (AXIS ax in AxList)
                        {
                            zmcaux.ZAux_Direct_GetMaxSpeed(handle, ax.num, ref ftemp);
                            if (Math.Abs((ax.max_spd * ax.pul_per_mm) - ftemp) > 100)
                            {
                                bdownload = true;
                                break;
                            }
                        }
                        //download config
                        if (bdownload)
                        {
                            res = DownLoadFile(filename);
                            if (res != EM_RES.OK) return res;
                        }

                        isInit = true;
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}初始化成功!", disc));
                        foreach (AXIS ax in AxList)
                        {
                            ax.Init();
                        }
                        return EM_RES.OK;
                    }
                }
                else
                {
                    isInit = false;
                    handle = (IntPtr)0;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}找不到，IP={1}", disc, ip));
                    return EM_RES.ERR;
                }
            }
#endif
#if ADVANTTECH
            if (brand == BRAND.ADVANTTECH)
            {
                isInit = false;
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "研华库未添加!");
                return EM_RES.ERR;
            }
#endif
#if ORIENTALMOTOR
            if (brand == BRAND.ORIENTALMOTOR)
            {
                //get config
                if (filename.Length < 3) filename = Path.GetFullPath("..") + "\\syscfg\\ORIENTALMOTOR.ini";
                if (File.Exists(filename))
                {
                    IniFile inf = new IniFile(filename);
                    int temp = inf.ReadInteger("COM", "PORT", -1);
                    if (temp >= 0 && temp < 100) PortName = string.Format("COM{0}",temp);
                    temp = inf.ReadInteger("COM", "BAUD", -1);
                    if (temp > 0) Baudrate = temp;
                }

                if (0 == AZD.Motor.InitRs485(PortName, Baudrate))
                {
                    isInit = true;
                    handle = (IntPtr)1;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}初始化成功!", disc));
                    foreach (AXIS ax in AxList)
                    {
                        ax.Init();
                    }
                    return EM_RES.OK;
                }
                else
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("InitRs485异常，BRAND={0}，TYPE={1},Port={2}/{3}", brand, type, PortName, Baudrate));
                    return EM_RES.ERR;
                }
            }
#endif
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("未定义异常，BRAND={0}，TYPE={1}", brand, type));
            return EM_RES.ERR;
        }
        /// <summary>
        /// 初始化板卡。
        /// 如有主次卡之分，请先初始化主卡。关闭则相反，先关从卡再关主卡。
        /// </summary>
        /// <param name="id">卡ID</param>
        /// <param name="filename">配置文件路径</param>
        /// <returns></returns>
        public EM_RES Init(int card_id, string filename = "")
        {
            return Init(card_id, -1, "", filename);
        }
        /// <summary>
        /// 初始化板卡。
        /// 如有主次卡之分，请先初始化主卡。关闭则相反，先关从卡再关主卡。
        /// </summary>
        /// <param name="ip">卡IP地址</param>
        /// <param name="filename">配置文件路径</param>
        /// <returns></returns>
        public EM_RES Init(string ip, string filename = "")
        {
            return Init(-1, -1, ip, filename);
        }
        /// <summary>
        /// 初始化板卡。
        /// 如有主次卡之分，请先初始化主卡。关闭则相反，先关从卡再关主卡。
        /// </summary>
        /// <param name="id">卡ID</param>
        /// <param name="maincard_id">主卡ID</param>
        /// <param name="filename">配置文件路径</param>
        /// <returns></returns>
        public EM_RES Init(int card_id, int maincard_id, string filename = "")
        {
            return Init(card_id, maincard_id, "", filename);
        }
        #endregion
        #region 下载参数
        public EM_RES DownLoadFile(string filename = "")
        {
            int ret = 0;
            //if (!isReady) return EM_RES.ERR;
#if LEADSHINE
            if (brand == BRAND.LEADSHINE)
            {
                //download config
                if (filename == "") filename = string.Format("{0}DMC3800_{1}.ini", Path.GetFullPath("..") + "\\syscfg\\", card_id);
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, String.Format("{0}下载配置,{1}", str_disc, filename));
                ret = LTDMC.dmc_download_configfile((ushort)card_id, filename);
                if (ret != 0)
                {
                    LTDMC.dmc_board_close();
                    isInit = false;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0}/id{1},配置失败！Err:0x{2:X8}", disc, card_id, ret));
                    return EM_RES.ERR;
                }
                else VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, String.Format("{0}/id{1},更新配置成功！", disc, card_id));
            }
#endif
#if ZMOTION
            if (brand == BRAND.ZMOTION)
            {
			    //下载时，容易引起程序丢失
                //if (filename == "") filename = string.Format("{0}{1}_{2}.bas", Path.GetFullPath("..") + "\\syscfg\\", str_disc, id);
                //VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, String.Format("{0}下载配置,{1}", str_disc, filename));
                //ret = zmcaux.ZAux_BasDown(handle, filename);
                //if (ret != 0)
                //{
                //    isInit = false;
                //    zmcaux.ZAux_Close(handle);
                //    handle = (IntPtr)0;
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 下载配置失败,Err:{1},{2}", disc, ret, filename));
                //    return EM_RES.ERR;
                //}
                //else
                //{
                //    //set max_spd
                //    foreach (AXIS ax in AxList)
                //    {
                //        zmcaux.ZAux_Direct_SetMaxSpeed(handle, ax.num, (int)(ax.max_spd * ax.pul_per_mm));
                //        if (ret != 0)
                //        {
                //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 配置MAX_SPD失败,Err:{1}", disc, ret));
                //        }
                //    }
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, String.Format("{0}/id{1},更新配置成功！", disc, id));
                //}
            }
#endif
#if ORIENTALMOTOR
            if (brand == BRAND.ORIENTALMOTOR)
            {
                if (!Motor.isRs485Ready)
                {
                    EM_RES res = Init();
                    if (res != EM_RES.OK) return res;
                }
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, String.Format("{0}下载配置", str_disc));
                foreach (AXIS ax in AxList)
                {
                    UInt32 temp = 0;
                    UInt32 temp2 = 0;
                    ushort[] dat;
                    bool bres = true;

                    ////(ZHOME)•运行速度 688/689
                    ////(ZHOME)•加减速 690/691
                    //temp = (ushort)(ax.home_spd * ax.pul_per_mm);
                    //temp2 = (ushort)(ax.tacc / 0.001);
                    //dat = new ushort[4] { (ushort)(temp >> 16), (ushort)(temp & 0XFFFF), (ushort)(temp2 >> 16), (ushort)(temp2 & 0XFFFF) };
                    //bres = ax.AzdMotor.WriteReg(688, dat);
                    //if(!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置运行速度/加速出错!",ax.disc));
                    //    return EM_RES.ERR;
                    //}
                    ////SLN作为 原点/预置位置 908/909
                    //temp = (ushort)(ax.sln * ax.pul_per_mm);
                    //dat = new ushort[2] { (ushort)(temp >> 16), (ushort)(temp & 0XFFFF) };
                    //bres = ax.AzdMotor.WriteReg(908, dat);
                    //if (!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置原点出错!", ax.disc));
                    //    return EM_RES.ERR;
                    //}

                    //POS0点数据 1024/1025
                    //POS1点数据 1026/1027
                    temp = (ushort)(ax.pos0 * ax.pul_per_mm);
                    temp2 = (ushort)(ax.pos1 * ax.pul_per_mm);
                    dat = new ushort[4] { (ushort)(temp >> 16), (ushort)(temp & 0XFFFF), (ushort)(temp2 >> 16), (ushort)(temp2 & 0XFFFF) };
                    bres = ax.AzdMotor.WriteReg(1024, dat);
                    if (!bres)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置POS0/POS1出错!", ax.disc));
                        return EM_RES.ERR;
                    }                    

                    ////速度No.0 1152/1153
                    ////速度No.1 1154/1155
                    //temp = (ushort)(ax.spd_work * ax.pul_per_mm);
                    //dat = new ushort[4] { (ushort)(temp >> 16), (ushort)(temp & 0XFFFF), (ushort)(temp >> 16), (ushort)(temp & 0XFFFF) };
                    //bres = ax.AzdMotor.WriteReg(1152,dat);
                    //if (!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置速度0/速度1出错!", ax.disc));
                    //    return EM_RES.ERR;
                    //}

                    ////方式No.0 1280/1281 1-绝对定位
                    ////方式No.1 1282/1283 1-绝对定位
                    //dat = new ushort[4] { 0, 1, 0, 1 };
                    //bres = ax.AzdMotor.WriteReg(1280, dat);
                    //if (!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置定位方式0/1出错!", ax.disc));
                    //    return EM_RES.ERR;
                    //}

                    ////加减速单位 654/655 0：•kHz/s• 1：•s• 2：•ms / kHz
                    //dat = new ushort[2] { 0,1 };
                    //bres = ax.AzdMotor.WriteReg(654, dat);
                    //if (!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置加减速单位出错!", ax.disc));
                    //    return EM_RES.ERR;
                    //}

                    ////起动/变速No.0 1536/1537
                    ////起动/变速No.1 1538/1539
                    //temp = (ushort)(ax.tacc/0.001);
                    //dat = new ushort[4] { (ushort)(temp >> 16), (ushort)(temp & 0XFFFF), (ushort)(temp >> 16), (ushort)(temp & 0XFFFF) };
                    //bres = ax.AzdMotor.WriteReg(1536, dat);
                    //if (!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置加速度出错!", ax.disc));
                    //    return EM_RES.ERR;
                    //}

                    ////停止No.0 1664/1665
                    ////停止No.1 1666/1667
                    //temp = (ushort)(ax.tdec / 0.001);
                    //dat = new ushort[4] { (ushort)(temp >> 16), (ushort)(temp & 0XFFFF), (ushort)(temp >> 16), (ushort)(temp & 0XFFFF) };
                    //bres = ax.AzdMotor.WriteReg(1664, dat);
                    //if (!bres)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置减速度出错!", ax.disc));
                    //    return EM_RES.ERR;
                    //}
                    return EM_RES.OK;
                }
            }
#endif
            return EM_RES.OK;
        }
        #endregion
        #region 检查连接
        /// <summary>
        /// 检查板卡是否在线，否在重连
        /// </summary>
        /// <returns></returns>
        public EM_RES ChkOnline(string filename = "")
        {
            EM_RES res = EM_RES.OK;
#if LEADSHINE
            if (brand == BRAND.LEADSHINE)
            {
                if (isReady) return EM_RES.OK;
            }
#endif
#if ZMOTION
            if (brand == BRAND.ZMOTION)
            {
                //check on line
                if (((long)handle) == 0)
                {
                    if (bshowmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 掉线!", disc));
                    bshowmsg = false;
                    return res = EM_RES.ERR;                     
                }
                uint val = 0;
                int ret = zmcaux.ZAux_Direct_GetIn(handle, 0,ref val);
                if (ret != 0)
                {
                    if (bshowmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 掉线!", disc));
                    if (((long)handle) != 0) zmcaux.ZAux_Close(handle);
                    handle = (IntPtr)0;
                    bshowmsg = false;
                    res = EM_RES.ERR;
                }
                else if (((long)handle) == 0)
                {
                    bshowmsg = true;
                    res = Init(ip, filename);
                }
            }
#endif
#if ORIENTALMOTOR
            if (brand == BRAND.ORIENTALMOTOR)
            {
                foreach (AXIS ax in AxList)
                {
                    ax.AzdMotor.ErrorCount = 0;
                }
                if (AZD.Motor.isRs485Ready) return res = EM_RES.OK;
                else
                {
                    res = Init();
                    return res;
                    //res = EM_RES.ERR;
                    //return res;
                }
            }
#endif
            return res;
        }
        #endregion
        #region 关闭
        /// <summary>
        /// 关闭板卡。先关从卡再关主卡。
        /// </summary>
        /// <returns></returns>
        public EM_RES Close()
        {
            if (!isInit) return EM_RES.OK;

            int ret;
#if LEADSHINE
            if (brand == BRAND.LEADSHINE)
            {
                if (type == TYPE.MOTION)
                {
                    //axis dec stop
                    for (int n = 0; n < ax_num; n++)
                    {
                        LTDMC.dmc_stop((ushort)card_id, (ushort)n, 0);
                        Thread.Sleep(10);
                    }

                    ret = LTDMC.dmc_board_close();
                    if (ret != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}关闭失败,Err:0x{1:X8}", disc, ret));
                        return EM_RES.ERR;
                    }
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}关闭成功", disc));
                    isInit = false;
                    return EM_RES.OK;
                }
                else if (type == TYPE.CAN_IO)
                {
                    //连接IO卡
                    ret = LTDMC.dmc_set_can_state((ushort)maincard_id, (ushort)card_id, 0, 0);
                    if (ret != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}断开失败,Err:0x{1:X8}", disc, ret));
                        return EM_RES.ERR;
                    }
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}断开成功", disc));
                    isInit = false;
                    return EM_RES.OK;
                }
#if LEADSHINE_IO
                else if (type == TYPE.IO)
                {
                    IOC0640.ioc_board_close();
                    isInit = false;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}关闭成功", disc));
                    return EM_RES.OK;
                }
#endif
            }
#endif
#if ZMOTION
            if (brand == BRAND.ZMOTION)
            {
                if (handle != (IntPtr)0)
                {
                    ret = zmcaux.ZAux_Close(handle);
                    if (ret != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("{0}关闭失败,Err:0x{1:X8}", disc, ret));
                        return EM_RES.ERR;
                    }
                    handle = (IntPtr)0;
                    isInit = false;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}关闭成功", disc));
                    return EM_RES.OK;
                }
                return EM_RES.OK;
            }
#endif
#if ADVANTTECK
            if (brand == BRAND.ADVANTTECH)
            {
                isInit = false;
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "研华库未添加!");
                return EM_RES.ERR;
            }
#endif
#if ORIENTALMOTOR
            if (brand == BRAND.ORIENTALMOTOR)
            {
                if (AZD.Motor.Close())
                {
                    isInit = false;
                    return EM_RES.OK;
                }
                else return EM_RES.ERR;
            }
#endif
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("未定义异常，BRAND={0}，TYPE={1}", brand, type));
            return EM_RES.ERR;
        }
        #endregion
        #region 设置轴到工作速度
        public static EM_RES SetAxToWorkSpd(List<AXIS> ax_list, double persent = 1.0)
        {
            EM_RES ret = EM_RES.OK;
            foreach (AXIS ax in ax_list)
            {
                ret = ax.SetToWorkSpd(persent);
                if (ret != EM_RES.OK) return ret;
            }
            return ret;
        }
        #endregion
        #region 设置轴到手动速度
        public static EM_RES SetAxToManualSpd(List<AXIS> ax_list)
        {
            EM_RES ret = EM_RES.OK;
            foreach (AXIS ax in ax_list)
            {
                ret = ax.SetToManualHighSpd();
                if (ret != EM_RES.OK) return ret;
            }
            return ret;
        }
        #endregion
        #region 轴停止
        public static void AxStop(List<AXIS> ax_list)
        {
            foreach (AXIS ax in ax_list) ax.Stop();
        }
        #endregion
        #region 卡内所有轴停止
        /// <summary>
        /// 停止卡内所有轴
        /// </summary>
        /// <param name="mode">0：取消当前运动，1：取消缓存动作，2：当前运动/缓存运动，3：急停</param>
        /// <returns></returns>
        public EM_RES AllCardStop(int mode = 2)
        {
            if (!isReady) return EM_RES.ERR;
#if ZMOTION
            if (brand == BRAND.ZMOTION)
            {
                int ret = zmcaux.ZAux_Direct_Rapidstop(handle, mode);
                if (ret != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 停止出错，ERR:{1}", disc, ret));
                    return EM_RES.ERR;
                }
            }
#endif
            return EM_RES.OK;
        }
        #endregion
        #region 轴使能
        public static bool AxSvrOn(List<AXIS> ax_list)
        {
            bool all_on = true;
            foreach (AXIS ax in ax_list)
            {
                if (ax.mt_type != AXIS.MT_TYPE.VIRTUAL && !ax.isSVRON)
                {
                    all_on = false;
                    ax.SVRON = true;
                }
            }
            return all_on;
        }
        #endregion
        #region 定位
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
    }
}
