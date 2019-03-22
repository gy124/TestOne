using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionCtrl;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace MotionCtrl
{
    public delegate EM_RES CHK_AXIS_SAFE(int id, double target_pos = double.MaxValue);
    /// <summary>
    /// AXIS类，统一轴的API
    /// 创建：李大源 @2018/08/02
    /// </summary>
    public class AXIS
    {
        /// <summary>
        /// 对应板卡
        /// </summary>
        CARD card;

        public enum AX_DIR { N = 0, P = 1, CCW = 0, CW = 1 }
        public enum MT_TYPE { STEP, SEVER, VIRTUAL, NULL }
        public enum ENC_TYPE { YES, NO }
        public enum HOME_STA
        {
            [Description("未知")]
            UNKOWN,
            [Description("回零中")]
            HOMING,
            [Description("完成")]
            OK,
            [Description("错误")]
            ERROR
        }
        /// <summary>
        /// 限位使用类型
        /// </summary>
        public enum EL_EN
        {
            [Description("不用限位")]
            DIS,
            [Description("正负限位")]
            NP,
            [Description("只用负限位")]
            N,
            [Description("只用正限位")]
            P
        }
        public enum EL_LOGIC
        {
            [Description("正负限位低电平有效")]
            NP_L,
            [Description("正负限位高电平有效")]
            NP_H,
            [Description("正低负高电平有效")]
            NH_PL,
            [Description("正高负低电平有效")]
            NL_PH
        }

        public enum EL_STOP
        {
            [Description("正负限位处急停")]
            NP_EMG,
            [Description("正负限位减速停止")]
            NP_DEC,
            [Description("负限位减速停止")]
            N_DEC,
            [Description("正限位减速停止")]
            P_DEC
        }
        [Flags]
        public enum MTIO : uint
        {
            [Description("报警")]
            ALM = (1 << 0),
            [Description("正限位")]
            LMTP = (1 << 1),
            [Description("负限位")]
            LMTN = (1 << 2),
            [Description("急停")]
            EMG = (1 << 3),
            [Description("原点")]
            ORG = (1 << 4),
            [Description("软件正限位")]
            SLMTP = (1 << 6),
            [Description("软件负限位")]
            SLMTN = (1 << 7),
            [Description("实际到位")]
            INP = (1 << 8),
            [Description("相位")]
            EZ = (1 << 9),
            [Description("命令脉冲到位")]
            RDY = (1 << 10),
            [Description("伺服使能")]
            SVON = (1 << 11),
            [Description("回零中")]
            HOMEING = (1 << 12)
        }

        public enum AX_STA
        {
            [Description("命令脉冲到位")]
            READY,
            [Description("点到点运动中")]
            PTP,
            [Description("报警")]
            ALM,
            [Description("软件正限位")]
            ELP,
            [Description("软件负限位")]
            ELN,

            SLP,
            SLN,
            [Description("急停")]
            EMG,
            [Description("伺服未使能")]
            DIS,

            NRDY,
            HOMEING,
            UNKOWN
        }


        public int m_id;

        public int num;//从零开始轴号
        public string str_disc;
        public EM_RES res;
        public int useby = -1;  //使用id索引

        public ushort hcmp_io_num;
        private ushort hcmp_mode;

        private int m_cmd_pos;
        private int m_enc_pos;
        private uint m_mt_io;

        GPIO.IO_STA logic_svr_on;
        public ENC_TYPE encode;
        public MT_TYPE mt_type;

        private AX_DIR home_dir;
        private ushort home_mode;
        public double home_spd;
        public double home_offset;
        public int home_timeout;
        public HOME_STA home_status = HOME_STA.UNKOWN;

        public double mspd_cur;
        public double spd_start;
        public double spd_stop;
        public double spd_work;
        public double spd_cap;
        public double spd_manual_high;
        public double spd_manual_low;
        public bool bman_high_spd;
        public double manual_step;
        public double tacc;
        public double tdec;
        public double ts;

        public double max_spd;//mm/s
        public double max_acc;//mm/s^2

        public double pul_per_mm;
        public double sln;
        public double slp;
        public int enc_k = 1;

        public int cmd_k = 1;

        private bool bkey = false;
        private bool ben_eln;
        private bool ben_elp;

        public int ax_type = 0;
        public int puls_mode = 0;
        public int org_num = -1;
        public int alm_num = -1;
        public int eln_num = -1;
        public int elp_num = -1;
        public int svron_num = -1;

        public bool org_active_h = false;
        public bool alm_active_h = false;
        public bool eln_active_h = false;
        public bool elp_active_h = false;

        //运行里程
        public double dis_ttl;
        //需要保养里程
        public double dis_max;

        public CHK_AXIS_SAFE ChkSafePos;
        public CHK_AXIS_SAFE ChkSafeSen;

        AX_DIR JOGDir = AX_DIR.P;

        bool bShowErrMsg = true;
        public bool bInit = false;
        /// <summary>
        /// 固高32位轴状态
        /// </summary>
        int GT_pSts;
        /// <summary>
        /// 读取控制器时钟标志
        /// </summary>
        uint GT_axis_time = 0;



        #region 构造、初始化
        public AXIS()
        {
            num = -1;
            max_spd = 0;
            spd_work = 0;
            pul_per_mm = 0;
            home_status = HOME_STA.UNKOWN;
        }
        public AXIS(ushort ax_id, CARD card, string disc, MT_TYPE mt_type = MT_TYPE.SEVER, ENC_TYPE encode = ENC_TYPE.YES, GPIO.IO_STA logic_svr_on = GPIO.IO_STA.OUT_ON, int cmd_k = 1, int enc_k = 1)
        {
            this.card = card;
            if (card != null)
                m_id = (int)(card.id << 8) + (int)(ax_id & 0xFF);
            else m_id = -1;

            this.num = ax_id;
            this.str_disc = disc.Replace("\0", "");
            this.mt_type = mt_type;
            this.encode = encode;
            this.logic_svr_on = logic_svr_on;
            this.cmd_k = cmd_k;
            this.enc_k = enc_k;

            home_status = HOME_STA.UNKOWN;
            mspd_cur = 0;

            ben_eln = true;
            ben_elp = true;

            manual_step = 1;

            ChkSafePos = null;
            ChkSafePos = null;

            //check num
            if (num < 0 || num >= card.ax_num)
            {
                pul_per_mm = 0;
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} io_num {1} 超范围[{2},{3})", disc, num, 0, card.ax_num));

            }

            //load config
            res = LoadCfgFrInf();
            if (res != EM_RES.OK)
            {
                pul_per_mm = 0;
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 加载参数异常！", disc));
            }

            //set to home speed
            if (card.isReady) SetToHomeSpd();

            card.AxList.Add(this);
        }
        public bool isInit
        {
            get
            {
                //check card
                if (card == null || card.isReady == false) return false;

                //check num
                if (num < 0 || num >= card.ax_num) return false;

                //check id
                if (m_id < 0) return false;

                //pul_per_mm
                if (Math.Abs(pul_per_mm) < 0.1) return false;

                return true;
            }
        }
        #endregion
        #region 加载/保存参数
        public EM_RES LoadCfgFrInf(string filename = "")
        {

            if (filename.Length < 3) filename = Path.GetFullPath("..") + "\\syscfg\\axiscfg.ini";
            IniFile inf = new IniFile(filename);

            string Section = string.Format("AXIS_{0}/{1}", num, card.id);
            //string temp;

            //str_disc = inf.ReadString(Section,"DISC",str_disc);

            //temp = inf.ReadString(Section, "MT_TYPE", Enum.GetName(typeof(MT_TYPE), mt_type));
            //mt_type =(MT_TYPE) Enum.Parse(typeof(MT_TYPE), temp, false);

            //temp = inf.ReadString(Section, "ENCODE", Enum.GetName(typeof(ENC_TYPE), encode));
            //encode = (ENC_TYPE)Enum.Parse(typeof(ENC_TYPE), temp, false);


            logic_svr_on = (GPIO.IO_STA)inf.ReadInteger(Section, "LOGIC_SVR_ON", (int)logic_svr_on);
            home_dir = (AX_DIR)inf.ReadInteger(Section, "HOME_DIR", (int)home_dir);
            home_mode = (ushort)inf.ReadInteger(Section, "HOME_MODE", home_mode);
            home_offset = inf.ReadDouble(Section, "HOME_OFFSET", 0);
            home_spd = inf.ReadDouble(Section, "HOME_SPD", 0);

            spd_work = inf.ReadDouble(Section, "WORK_SPD", 0);
            //spd_cap = inf.ReadDouble(Section, "CAP_SPD", 1000);
            spd_start = inf.ReadDouble(Section, "START_SPD", 0);
            spd_stop = inf.ReadDouble(Section, "STOP_SPD", 0);
            spd_manual_high = inf.ReadDouble(Section, "MAN_SPD_H", 80);
            spd_manual_low = inf.ReadDouble(Section, "MAN_SPD_L", 5);

            pul_per_mm = inf.ReadDouble(Section, "PUL_PER_MM", 0);
            slp = inf.ReadDouble(Section, "SLP", 0);
            sln = inf.ReadDouble(Section, "SLN", 0);
            tacc = inf.ReadDouble(Section, "TACC", 0);
            tdec = inf.ReadDouble(Section, "TDEC", 0);
            ts = inf.ReadDouble(Section, "TS", 0);
            if (slp == 0 && sln == 0)
            {
                slp = 100000;
                sln = -slp;
            }
            max_acc = inf.ReadDouble(Section, "MAX_ACC", 15000);
            max_spd = inf.ReadDouble(Section, "MAX_SPD", 1500);

            inf.ReadInteger(Section, "AX_TYPE", ax_type);
            inf.ReadInteger(Section, "PULS_MODE", puls_mode);

            elp_num = inf.ReadInteger(Section, "ELP", -1);
            elp_active_h = inf.ReadBool(Section, "ELP_ACTIVE_H", false);
            eln_num = inf.ReadInteger(Section, "ELN", -1);
            eln_active_h = inf.ReadBool(Section, "ELN_ACTIVE_H", false);
            org_num = inf.ReadInteger(Section, "ORG", -1);
            org_active_h = inf.ReadBool(Section, "ORG_ACTIVE_H", false);
            alm_num = inf.ReadInteger(Section, "ALM", -1);
            alm_active_h = inf.ReadBool(Section, "ALM_ACTIVE_H", true);
            return EM_RES.OK;
        }
        public EM_RES SaveCfgToInf(string filename = "")
        {
            if (filename.Length < 3) filename = Path.GetFullPath("..") + "\\syscfg\\axiscfg.ini";
            IniFile inf = new IniFile(filename);

            string Section = string.Format("AXIS_{0}/{1}", num, card.id);

            inf.WriteString(Section, "DISC", str_disc);
            inf.WriteString(Section, "MT_TYPE", Enum.GetName(typeof(MT_TYPE), mt_type));
            inf.WriteString(Section, "ENCODE", Enum.GetName(typeof(ENC_TYPE), encode));
            inf.WriteInteger(Section, "LOGIC_SVR_ON", (int)logic_svr_on);

            inf.WriteInteger(Section, "HOME_DIR", (int)home_dir);
            inf.WriteInteger(Section, "HOME_MODE", home_mode);
            inf.WriteDouble(Section, "HOME_OFFSET", home_offset);
            inf.WriteDouble(Section, "HOME_SPD", home_spd);

            inf.WriteDouble(Section, "WORK_SPD", spd_work);
            inf.WriteDouble(Section, "START_SPD", spd_start);
            inf.WriteDouble(Section, "STOP_SPD", spd_stop);
            inf.WriteDouble(Section, "MAN_SPD_H", spd_manual_high);
            inf.WriteDouble(Section, "MAN_SPD_L", spd_manual_low);

            inf.WriteDouble(Section, "PUL_PER_MM", pul_per_mm);
            inf.WriteDouble(Section, "SLP", slp);
            inf.WriteDouble(Section, "SLN", sln);
            inf.WriteDouble(Section, "TACC", tacc);
            inf.WriteDouble(Section, "TDEC", tdec);
            inf.WriteDouble(Section, "TS", ts);

            //inf.WriteDouble(Section, "MAX_ACC", max_acc);
            //inf.WriteDouble(Section, "MAX_SPD", max_spd);

            inf.WriteInteger(Section, "AX_TYPE", ax_type);
            inf.WriteInteger(Section, "PULS_MODE", puls_mode);

            inf.WriteInteger(Section, "ELP", elp_num);
            inf.WriteBool(Section, "ELP_ACTIVE_H", elp_active_h);
            inf.WriteInteger(Section, "ELN", eln_num);
            inf.WriteBool(Section, "ELN_ACTIVE_H", eln_active_h);
            inf.WriteInteger(Section, "ORG", org_num);
            inf.WriteBool(Section, "ORG_ACTIVE_H", org_active_h);
            inf.WriteInteger(Section, "ALM", alm_num);
            inf.WriteBool(Section, "ALM_ACTIVE_H", alm_active_h);

            return EM_RES.OK;
        }
        #endregion
        #region 设定参数
        public EM_RES SetELMode(EL_EN el_en, EL_LOGIC el_logic = EL_LOGIC.NP_L, EL_STOP el_stop = EL_STOP.NP_EMG)
        {
            if (!Enum.IsDefined(typeof(EL_EN), el_en))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置限位使能异常，el_en={1}！", disc, (int)el_en));
                return EM_RES.PARA_ERR;
            }

            int err = 0;
#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                err = LTDMC.dmc_set_el_mode((ushort)card.card_id, (ushort)num, (ushort)el_en, (ushort)el_logic, (ushort)el_stop);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置限位模式出错，{1}！", disc, err));
                    return res = EM_RES.ERR;
                }
            }
#endif
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                switch (el_en)
                {
                    case EL_EN.DIS:
                        err = zmcaux.ZAux_Direct_SetFwdIn(card.handle, num, -1);
                        err += zmcaux.ZAux_Direct_SetRevIn(card.handle, num, -1);
                        break;
                    case EL_EN.NP:
                        err = zmcaux.ZAux_Direct_SetFwdIn(card.handle, num, elp_num);
                        err += zmcaux.ZAux_Direct_SetRevIn(card.handle, num, eln_num);
                        break;
                    case EL_EN.P:
                        err = zmcaux.ZAux_Direct_SetFwdIn(card.handle, num, elp_num);
                        err += zmcaux.ZAux_Direct_SetRevIn(card.handle, num, -1);
                        break;
                    case EL_EN.N:
                        err = zmcaux.ZAux_Direct_SetFwdIn(card.handle, num, -1);
                        err += zmcaux.ZAux_Direct_SetRevIn(card.handle, num, eln_num);
                        break;
                }
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置限位IO编号出错，{1}！", disc, err));
                    return res = EM_RES.ERR;
                }

            }
#endif
            return res = EM_RES.OK;
        }
        public EM_RES MtMaxParamCfg(double max_spd, double max_acc, double sln = double.MinValue, double slp = double.MaxValue)
        {
            if (max_acc <= 0) max_acc = 25000;//2.5G

            this.max_spd = max_spd;
            this.max_acc = max_acc;
            this.sln = sln;
            this.slp = slp;
            return res = EM_RES.OK;
        }
        public EM_RES MtParamCfg(double pul_per_mm, double spd_work, double spd_manual_high, double spd_manual_low, double tacc, double tdec, double spd_start = 0, double spd_stop = 0)
        {
            this.pul_per_mm = pul_per_mm;
            this.spd_work = spd_work;
            this.spd_manual_high = spd_manual_high;
            this.spd_manual_low = spd_manual_low;
            this.tacc = tacc;
            this.tdec = tdec;
            this.spd_start = spd_start;
            this.spd_stop = spd_stop;

            res = EM_RES.OK;
            double min_tacc;
            min_tacc = max_acc > 0 ? max_spd / max_acc : 0.08;
            if (tacc < min_tacc) { VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 加速时间{1:F3} 过小（<{2:F3}）", disc, tacc, min_tacc)); res = EM_RES.PARA_ERR; }
            if (tdec < min_tacc) { VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 减速时间{1:F3} 过小 (<{2:F3}）", disc, tacc, min_tacc)); res = EM_RES.PARA_ERR; }

            return res;
        }
        #region 初始化
        /// <summary>
        /// 下载初始化轴参数
        /// </summary>
        /// <returns></returns>
        public EM_RES Init()
        {
            int err = 0;
            bInit = false;
#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                //check card
                if (card == null || card.isReady == false) return EM_RES.ERR;

                bInit = true;
                return EM_RES.OK;
            }
#endif
#if GOOGOLTECH
            if (card.brand == CARD.BRAND.GOOGOLTECH)
            {
                //check card
                if (card == null || card.isReady == false) return EM_RES.ERR;

                bInit = true;
                return EM_RES.OK;
            }
#endif
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                ////ELN
                //err = zmcaux.ZAux_Direct_SetRevIn(card.handle, num, eln_num >= 0 ? eln_num : -1);
                //if (eln_num >= 0) err += zmcaux.ZAux_Direct_SetInvertIn(card.handle, eln_num, eln_active_h ? 1 : 0);
                //if (err != 0)
                //{
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置负限位（IN={1}）出错，{2}！", disc, eln_num, err));
                //    return res = EM_RES.ERR;
                //}
                ////ELP
                //err = zmcaux.ZAux_Direct_SetFwdIn(card.handle, num, elp_num >= 0 ? elp_num : -1);
                //if (elp_num >= 0) err += zmcaux.ZAux_Direct_SetInvertIn(card.handle, elp_num, elp_active_h ? 1 : 0);
                //if (err != 0)
                //{
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置正限位（IN={1}）出错，{2}！", disc, elp_num, err));
                //    return res = EM_RES.ERR;
                //}
                ////ORG
                //err = zmcaux.ZAux_Direct_SetFwdIn(card.handle, num, org_num >= 0 ? org_num : -1);
                //if (org_num >= 0) err += zmcaux.ZAux_Direct_SetInvertIn(card.handle, org_num, org_active_h ? 1 : 0);
                //if (err != 0)
                //{
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置原点（IN={1}）出错，{2}！", disc, org_num, err));
                //    return res = EM_RES.ERR;
                //}
                ////ALM
                //err = zmcaux.ZAux_Direct_SetAlmIn(card.handle, num, alm_num >= 0 ? alm_num : -1);
                //if (alm_num >= 0) err += zmcaux.ZAux_Direct_SetInvertIn(card.handle, alm_num, alm_active_h ? 1 : 0);
                //if (err != 0)
                //{
                //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 设置轴报警(IN={1}）出错，{2}！", disc, alm_num, err));
                //    return res = EM_RES.ERR;
                //}

                //ELN
                int temp = 0;
                err = zmcaux.ZAux_Direct_GetRevIn(card.handle, num, ref eln_num);
                if (eln_num >= 0) err += zmcaux.ZAux_Direct_GetInvertIn(card.handle, eln_num, ref temp);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取负限位（IN={1}）出错，{2}！", disc, eln_num, err));
                    return res = EM_RES.ERR;
                }
                if (temp == 1) eln_active_h = true;

                //ELP
                temp = 0;
                err = zmcaux.ZAux_Direct_GetFwdIn(card.handle, num, ref elp_num);
                if (elp_num >= 0) err += zmcaux.ZAux_Direct_GetInvertIn(card.handle, elp_num, ref temp);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取正限位（IN={1}）出错，{2}！", disc, elp_num, err));
                    return res = EM_RES.ERR;
                }
                if (temp == 1) elp_active_h = true;
                //ORG
                temp = 0;
                err = zmcaux.ZAux_Direct_GetDatumIn(card.handle, num, ref org_num);
                if (org_num >= 0) err += zmcaux.ZAux_Direct_GetInvertIn(card.handle, org_num, ref temp);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取原点（IN={1}）出错，{2}！", disc, org_num, err));
                    return res = EM_RES.ERR;
                }
                if (temp == 1) org_active_h = true;

                //ALM
                temp = 0;
                err = zmcaux.ZAux_Direct_GetAlmIn(card.handle, num, ref alm_num);
                if (alm_num >= 0) err += zmcaux.ZAux_Direct_GetInvertIn(card.handle, alm_num, ref temp);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取轴报警(IN={1}）出错，{2}！", disc, alm_num, err));
                    return res = EM_RES.ERR;
                }
                if (temp == 1) alm_active_h = true;

                //SVRON
                if (card.disc.Contains("ZMC41")) svron_num = 12;
                else if (card.disc.Contains("ECI2400") || card.disc.Contains("ECI2600")) svron_num = 8;

                bInit = true;
            }
#endif
            return EM_RES.OK;
        }
        #endregion
        #endregion
        #region 检查参数
        public EM_RES ChkParam()
        {
            if (m_id < 0)
            {
                if (bShowErrMsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} ID({1})异常!", disc, m_id));
                bShowErrMsg = false;
                res = EM_RES.PARA_ERR;
                return res;
            }

            if (card == null || !card.isReady)
            {
                if (bShowErrMsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 对应板卡{1}未初始化!", disc, card.disc));
                bShowErrMsg = false;
                res = EM_RES.PARA_ERR;
                return res;
            }

            //pul_per_mm
            if (Math.Abs(pul_per_mm) < 0.1)
            {
                if (bShowErrMsg)
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 轴参数未初始化!", disc));
                bShowErrMsg = false;
                res = EM_RES.PARA_ERR;
                return res;
            }

            bShowErrMsg = true;
            res = EM_RES.OK;
            return res;
        }
        #endregion
        #region 描述
        public int id
        {
            get
            {
                return m_id;
            }
        }
        public int card_num
        {
            get
            {
                if (card != null) return card.id;
                return -1;

            }
        }
        public int axis_num
        {
            get
            {
                return num;
            }
        }
        public string disc
        {
            get
            {
                return string.Format("{0}({1}/{2})", str_disc.Replace("\0", ""), num, card.id);
            }
        }
        #endregion
        public class AXSTA
        {
            /// <summary>
            /// 命令位置
            /// </summary>
            public int cmd;
            /// <summary>
            /// 编码器位置
            /// </summary>
            public int enc;
            /// <summary>
            /// 轴IO
            /// </summary>
            public int mtio;
            /// <summary>
            /// 时间戳
            /// </summary>
            public long tickcnt;
        }
        AXSTA AxStaBuf = new AXSTA();
        #region 刷新状态缓存
        public EM_RES UpdateStatusBuff(bool bforceupdate = true)
        {
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                if (card.type == CARD.TYPE.MOTION)
                {
                    //check card
                    if (card == null || !card.isReady) return EM_RES.ERR;

                    //check time
                    if (!bforceupdate && Math.Abs(VAR.msg.sw.ElapsedMilliseconds - AxStaBuf.tickcnt) < 10)
                        return EM_RES.OK;
                    long t = VAR.msg.sw.ElapsedMilliseconds;
                    List<int> list_temp = new List<int>();
                    int ret = zmcaux.ZAux_Direct_GetAxSta(card.handle, num, ref list_temp);
                    if (ret == 0 && list_temp.Count == 7)
                    {
                        AxStaBuf.mtio = list_temp.ElementAt(0);
                        AxStaBuf.cmd = list_temp.ElementAt(1);
                        AxStaBuf.enc = list_temp.ElementAt(2);
                        card.CardIOBuf.input[0] = list_temp.ElementAt(3);
                        card.CardIOBuf.output[0] = list_temp.ElementAt(4);
                        card.CardIOBuf.input[1] = list_temp.ElementAt(5);
                        card.CardIOBuf.output[1] = list_temp.ElementAt(6);
                        card.CardIOBuf.tickcnt = VAR.msg.sw.ElapsedMilliseconds;
                        AxStaBuf.tickcnt = VAR.msg.sw.ElapsedMilliseconds;
                        //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} update,T{1}ms", disc, AxStaBuf.tickcnt - t));
                    }
                    else
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} update,Err:{1},Cnt:{2},T{3}ms", disc, ret, list_temp.Count, AxStaBuf.tickcnt - t));
                        return EM_RES.ERR;
                    }
                    return EM_RES.OK;
                }
                return EM_RES.OK;
            }
            return EM_RES.OK;
#endif
        }


        #endregion
        #region 当前速度设置
        public double spd_cur
        {
            get
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK)
                {
                    res = EM_RES.PARA_ERR;
                    return mspd_cur;
                }

                //get spd
                double spd = 0;
                int err = 0;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    double spl = 0;
                    double sps = 0;
                    double ta = 0;
                    double td = 0;
                    err = LTDMC.dmc_get_profile((ushort)card.card_id, (ushort)num, ref spl, ref spd, ref ta, ref td, ref sps);
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取速度失败，Err：{1}", disc, err));
                        res = EM_RES.ERR;
                        return mspd_cur;
                    }
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    float temp = 0;
                    err = zmcaux.ZAux_Direct_GetSpeed(card.handle, num, ref temp);
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取速度失败，Err：{1}", disc, err));
                        res = EM_RES.ERR;
                        return mspd_cur;
                    }
                    spd = temp;
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                  
                    double pValue;
               
                    err = mc.GT_GetVel((short)card.card_id, (short)(num + 1), out pValue);
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取当前速度失败，Err：{1}", disc, err));
                        res = EM_RES.ERR;
                        return mspd_cur;
                    }
                    spd = pValue*1000;
                }
#endif

                mspd_cur = spd / pul_per_mm;
                res = EM_RES.OK;
                return mspd_cur;
            }
        }
        #endregion
        #region 获取、修改当前位置位置
        public int cmd_pos
        {
            get
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK) return m_cmd_pos;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //get cmd pos
                    m_cmd_pos = LTDMC.dmc_get_position((ushort)card.card_id, (ushort)num);
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //float temp = 0;
                    //int err = zmcaux.ZAux_Direct_GetDpos(card.handle, num, ref temp);
                    //if (err != 0)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取命令坐标失败，Err：{1}", disc, err));
                    //    res = EM_RES.ERR;
                    //    return m_cmd_pos;
                    //}
                    //m_cmd_pos = (int)temp;

                    res = UpdateStatusBuff(false);
                    m_cmd_pos = AxStaBuf.cmd;
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    double m_pref_pos;
                    uint m_test = 1;
                    int mret = mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    //int mret = mc.GT_GetAxisEncPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    if (mret == 0)
                    {
                        m_cmd_pos = (int)m_pref_pos;
                        return m_cmd_pos;
                    }
                }
#endif
                return m_cmd_pos * cmd_k;
            }
            set
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK) return;
                int err = 0;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //set cmd pos
                    err = LTDMC.dmc_set_position((ushort)card.card_id, (ushort)num, value * cmd_k);
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //set cmd pos
                    err = zmcaux.ZAux_Direct_SetDpos(card.handle, num, value * cmd_k);
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    //err = mc.GT_ZeroPos((short)card.card_id, (short)(num+1), 1);
                    err = mc.GT_SetPrfPos((short)card.card_id, (short)(num + 1), value * cmd_k);
                    if (mt_type == MT_TYPE.SEVER)
                    {
                        err = mc.GT_SetEncPos((short)card.card_id, (short)(num + 1), value * cmd_k);
                    }
                }
#endif
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 设置CMD坐标出错，Err:0x{1:X8}", disc, err));
                    res = EM_RES.ERR;
                    return;
                }
                else m_cmd_pos = value * cmd_k;
                res = EM_RES.OK;
            }
        }
        public double fcmd_pos
        {
            get
            {
                return cmd_pos / pul_per_mm;
            }
            set
            {
                cmd_pos = (int)(value * pul_per_mm);
            }
        }
        #endregion
        #region 编码器位置
        public int enc_pos
        {
            get
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK) return m_enc_pos;
                if (encode == ENC_TYPE.YES)
                {
#if LEADSHINE
                    if (card.brand == CARD.BRAND.LEADSHINE)
                    {
                        //get enc pos
                        m_enc_pos = LTDMC.dmc_get_encoder((ushort)card.card_id, (ushort)num);
                    }
#endif
#if ZMOTION
                    if (card.brand == CARD.BRAND.ZMOTION)
                    {
                        ////get enc pos
                        //float temp = 0;
                        //int err = zmcaux.ZAux_Direct_GetMpos(card.handle, num, ref temp);
                        //if (err != 0)
                        //{
                        //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取反馈坐标失败，Err：{1}", disc, err));
                        //    res = EM_RES.ERR;
                        //    return m_enc_pos;
                        //}
                        //m_enc_pos = (int)temp;

                        res = UpdateStatusBuff(false);
                        m_enc_pos = AxStaBuf.enc;
                    }

#endif
#if GOOGOLTECH
                    if (card.brand == CARD.BRAND.GOOGOLTECH)
                    {
                        //get enc pos
                        double m_get_pos = 0;
                        uint m_tset_time = 1;
                        if (mt_type == MT_TYPE.SEVER)
                        {
                            int mret = mc.GT_GetAxisEncPos((short)card.card_id, (short)(num + 1), out m_get_pos, 1, out m_tset_time);
                            if (mret == 0)
                            {
                                m_enc_pos = (int)m_get_pos;
                            }
                        }
                        else

                            m_enc_pos = cmd_pos;
                    }
#endif
                }
                else
                {
                    //no encoder so get cmd pos
                    m_enc_pos = cmd_pos;
                }
                return m_enc_pos * enc_k;
            }
            set
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK) return;

                if (encode == ENC_TYPE.NO) return;

                int err = 0;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //set enc pos
                    err = LTDMC.dmc_set_encoder((ushort)card.card_id, (ushort)num, value * enc_k);
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //set cmd pos
                    err = zmcaux.ZAux_Direct_SetMpos(card.handle, num, value * enc_k);
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {

                    err = mc.GT_SetEncPos((short)card.card_id, (short)(num + 1), value * enc_k);
                    //err = mc.GT_ZeroPos((short)card.card_id, (short)(num + 1), 1);                   

                }
#endif
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 设置编码位置出错，0x{1:X8}", disc, err));
                    res = EM_RES.ERR;
                    return;
                }
                //update
                m_enc_pos = value * enc_k;
            }
        }
        public double fenc_pos
        {
            get
            {
                return enc_pos / pul_per_mm;
            }
            set
            {
                enc_pos = (int)(value * pul_per_mm);
            }
        }
        #endregion
        #region 使能软限位
        public EM_RES EnSlm(bool ben)
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            int err = 0;
#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                //set param
                err = LTDMC.dmc_set_softlimit((ushort)card.card_id, (ushort)num, (ushort)(ben ? 1 : 0), 0, 0, (int)(sln * pul_per_mm), (int)(slp * pul_per_mm));
            }
#endif
#if GOOGOLTECH
            if (card.brand == CARD.BRAND.GOOGOLTECH)
            {
                err = 0;//-gy-1201-不用软限位          
            }
#endif
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                //set SELP
                err = zmcaux.ZAux_Direct_SetFsLimit(card.handle, num, (float)(ben ? slp * pul_per_mm : 100000 * pul_per_mm));
                //set SELN
                err += zmcaux.ZAux_Direct_SetRsLimit(card.handle, num, (float)(ben ? sln * pul_per_mm : -100000 * pul_per_mm));
            }
#endif
            if (err != 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 设置软限位出错，Err:{1}", disc, err));
                res = EM_RES.ERR;
                return EM_RES.ERR;
            }

            res = EM_RES.OK;
            return EM_RES.OK;
        }
        #endregion
        #region 设定速度
        public EM_RES SetSpeed(double spd_start, double spd_high, double spd_stop, double Tacc, double Tdec)
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            //check spd
            if (spd_work == 0 || home_spd == 0 || max_spd == 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未设置速度！", disc));
                return EM_RES.PARA_ERR;
            }

            //check acc
            if (max_acc == 0 || max_spd == 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未设置最大速度/最大加速度！", disc));
                return EM_RES.PARA_ERR;
            }

            if (Math.Abs(spd_start) > Math.Abs(max_spd) || Math.Abs(spd_high) > Math.Abs(max_spd) || Math.Abs(spd_stop) > Math.Abs(max_spd))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 速度设置 [{1:F3},{2:F3}] 超范围[0,{3:F3}]！", disc, spd_start, spd_high, max_spd));
                return res = EM_RES.PARA_ERR;
            }

            if (Math.Abs(spd_high * pul_per_mm) < 10)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.WAR, string.Format("{0} 脉冲速度 {1} < 10，异常", disc, spd_high * pul_per_mm));
            }

            double min_tacc;
            min_tacc = max_acc > 0 ? max_spd / max_acc : 0.05;
            if (Math.Abs(Tacc) < Math.Abs(min_tacc) || Math.Abs(Tdec) < Math.Abs(min_tacc))
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 加/减速时间 Tacc/Tdec{1:F3}/{2:F3} 过小 (<{3:F3})！", disc, Tacc, Tdec, min_tacc));
                return res = EM_RES.PARA_ERR;
            }

            //set speed
            int err = 0;
            if (ts > Tacc || ts > Tdec)
                ts = Math.Min(Tacc, Tdec);
            if (ts < 0) ts = 0;
            if (ts > 0.25) ts = 0.25;
#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                err = LTDMC.dmc_set_profile((ushort)card.card_id, (ushort)num, spd_start * pul_per_mm, spd_high * pul_per_mm, Tacc, Tdec, spd_stop * pul_per_mm);
                if (err == 4)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 速度设置,板卡忙", disc));
                    res = EM_RES.BUSY;
                    return res;
                }
            }
#endif
#if GOOGOLTECH
            if (card.brand == CARD.BRAND.GOOGOLTECH)
            {
                mc.TTrapPrm m_axis_prm;
                m_axis_prm.acc = Tacc;
                m_axis_prm.dec = Tdec;
                m_axis_prm.velStart = spd_start * pul_per_mm;
                m_axis_prm.smoothTime = 25;
                err = mc.GT_SetTrapPrm((short)card.card_id, (short)(num + 1), ref m_axis_prm);
                err = mc.GT_SetVel((short)card.card_id, (short)(num + 1), spd_high * pul_per_mm/1000);
                //err = mc.dmc_set_profile((ushort)card.card_id, (ushort)num, spd_start * pul_per_mm, spd_high * pul_per_mm, Tacc, Tdec, spd_stop * pul_per_mm);

            }
#endif
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                //set spd_start
                err = zmcaux.ZAux_Direct_SetLspeed(card.handle, num, (float)(spd_high * pul_per_mm));
                //set speed
                err += zmcaux.ZAux_Direct_SetSpeed(card.handle, num, (float)(spd_high * pul_per_mm));
                //set Acc
                err += zmcaux.ZAux_Direct_SetAccel(card.handle, num, (float)(spd_high / Tacc));
                //set Dec
                err += zmcaux.ZAux_Direct_SetDecel(card.handle, num, (float)(spd_high / Tdec));

            }
#endif
            if (err != 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 速度设置出错,Err:{1}", disc, err));
                res = EM_RES.ERR;
                return res;
            }
            mspd_cur = spd_high;

            //S曲线



#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                err = LTDMC.dmc_set_s_profile((ushort)card.card_id, (ushort)num, 0, ts);
                if (err == 4)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} S曲线设置,板卡忙", disc));
                    res = EM_RES.BUSY;
                    return res;
                }
            }
#endif
#if GOOGOLTECH
            if (card.brand == CARD.BRAND.GOOGOLTECH)
            {

                mc.TTrapPrm m_axis_prm;
                m_axis_prm.acc = Tacc;
                m_axis_prm.dec = Tdec;
                m_axis_prm.velStart = spd_start * pul_per_mm;
                m_axis_prm.smoothTime = 25;//平滑时间0-50
                err = mc.GT_SetTrapPrm((short)card.card_id, (short)(num + 1), ref m_axis_prm);
                err = mc.GT_SetVel((short)card.card_id, (short)(num + 1), spd_high * pul_per_mm/1000);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} S曲线平滑加速设置失败", disc));
                    res = EM_RES.BUSY;
                    return res;
                }
            }
#endif
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                err = zmcaux.ZAux_Direct_SetSramp(card.handle, num, (float)ts);
            }
#endif

            if (err != 0)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} S曲线设置出错,ts={1::F3}ms,Err:{2}", disc, ts, err));
                res = EM_RES.ERR;
                return res;
            }

            res = EM_RES.OK;
            return res;
        }
        public EM_RES SetSpeed(double spd)
        {
            EM_RES res = EM_RES.OK;
            if (spd == spd_cur) return res;
            res = SetSpeed(spd_start, spd, spd_stop, tacc, tdec);
            return res;
        }
        public EM_RES SetSpdRadio(double radio)
        {
            if (radio * max_spd == spd_cur) return EM_RES.OK;
            EM_RES res = SetSpeed(spd_start, radio * max_spd, spd_stop, tacc, tdec);
            return res;
        }
        public EM_RES SetToWorkSpd(double persent = 1.0)
        {
            if ((spd_work * persent) == spd_cur) return EM_RES.OK;
            EM_RES res = SetSpeed(spd_start, spd_work * persent, spd_stop, tacc, tdec);
            return res;
        }
        public EM_RES SetToManualHighSpd()
        {
            bman_high_spd = true;
            if (spd_manual_high == spd_cur) return EM_RES.OK;
            EM_RES res = SetSpeed(spd_start, spd_manual_high, spd_stop, tacc * 3, tdec * 3);
            return res;
        }
        public EM_RES SetToHomeSpd()
        {
            if (home_spd == spd_cur) return EM_RES.OK;
            EM_RES res = SetSpeed(spd_start, home_spd, spd_stop, tacc * 3, tdec * 3);
            return res;
        }
        public EM_RES SetToManualLowSpd()
        {
            bman_high_spd = false;
            if (spd_manual_low == spd_cur) return EM_RES.OK;
            EM_RES res = SetSpeed(spd_start, spd_manual_low, spd_stop, tacc * 3, tdec * 3);
            return res;
        }
        #endregion
        #region 读取轴状态
        public AX_STA status
        {
            get
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK) return AX_STA.UNKOWN;
                AX_STA sta = AX_STA.UNKOWN;
                uint iostatus = mt_io;

                if ((iostatus & (uint)MTIO.EMG) == (uint)MTIO.EMG)
                    sta = AX_STA.EMG;
                else if ((iostatus & (uint)MTIO.ALM) == (uint)MTIO.ALM)
                    sta = AX_STA.ALM;
                else if ((iostatus & (uint)MTIO.SVON) != (uint)MTIO.SVON)
                    sta = AX_STA.DIS;
                else if (ben_elp && (iostatus & (uint)MTIO.LMTP) == (uint)MTIO.LMTP)
                    sta = AX_STA.ELP;
                else if (ben_eln && (iostatus & (uint)MTIO.LMTN) == (uint)MTIO.LMTN)
                    sta = AX_STA.ELN;
                else if ((iostatus & (uint)MTIO.SLMTP) == (uint)MTIO.SLMTP)
                    sta = AX_STA.SLP;
                else if ((iostatus & (uint)MTIO.SLMTN) == (uint)MTIO.SLMTN)
                    sta = AX_STA.SLN;
                else if ((iostatus & (uint)MTIO.HOMEING) == (uint)MTIO.HOMEING)
                    sta = AX_STA.HOMEING;
#if LEADSHINE
                else if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    if (0 == LTDMC.dmc_check_done((ushort)card.card_id, (ushort)num))
                        sta = AX_STA.PTP;
                    else
                        sta = AX_STA.READY;
                }
#endif
#if GOOGOLTECH
               else if (card.brand == CARD.BRAND.GOOGOLTECH)
                {                 
                    long mm = 1;                 
                    //get ax io state
                    int ret = mc.GT_GetSts((short)card.card_id, (short)(num + 1), out GT_pSts, 1, out GT_axis_time);
                    if (ret == 0)
                    {
                        if ((GT_pSts & (1 << 10)) !=0)
                        {
                            sta = AX_STA.PTP;
                        }
                        else
                            sta = AX_STA.READY;  
                       
                    }
                }
#endif
#if ZMOTION
                else if (card.brand == CARD.BRAND.ZMOTION)
                {
                    int temp = 0;
                    int err = zmcaux.ZAux_Direct_GetIfIdle(card.handle, num, ref temp);
                    if (err == 0)
                    {
                        if (temp == 0)
                            sta = AX_STA.PTP;
                        else sta = AX_STA.READY;
                    }
                    //else sta = AX_STA.READY;
                }
#endif
                else sta = AX_STA.READY;
                res = EM_RES.OK;
                return sta;
            }
        }
        public string str_status
        {
            get
            {
                switch (status)
                {
                    case AX_STA.ALM:
                        return "报警";
                    case AX_STA.EMG:
                        return "急停";
                    case AX_STA.ELP:
                        return "正限位";
                    case AX_STA.ELN:
                        return "负限位";
                    case AX_STA.PTP:
                        return "运行";
                    case AX_STA.SLP:
                        return "正软限位";
                    case AX_STA.SLN:
                        return "负软限位";
                    case AX_STA.DIS:
                        return "失电";
                    case AX_STA.HOMEING:
                        return "回零中";
                    case AX_STA.NRDY:
                        return "未就绪";
                    case AX_STA.READY:
                        return "就绪";
                    default:
                        break;
                }
                return "未知";
            }
        }

        public bool isStop
        {
            get
            {
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    if (1 == LTDMC.dmc_check_done((ushort)card.card_id, (ushort)num)) return true;
                    else return false;
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    //get ax io state


                    int ret = mc.GT_GetSts((short)card.card_id, (short)(num + 1), out GT_pSts, 1, out GT_axis_time);
                    if (ret == 0)
                    {
                        if ((GT_pSts & (1 << 10)) == 0)  //正在运动
                        {
                            return true;
                        }
                        else
                            return false;
                    }


                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    int temp = 0;
                    int err = zmcaux.ZAux_Direct_GetIfIdle(card.handle, num, ref temp);
                    if (err == 0 && temp != 0) return true;
                    else return false;
                }
#endif
                return false;
            }
        }
        #endregion
        #region 读取轴IO
        public uint mt_io
        {
            get
            {
                //check
                res = ChkParam();
                if (res != EM_RES.OK)
                    return m_mt_io;
                uint iostatus = 0;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //get ax io state
                    iostatus = LTDMC.dmc_axis_io_status((ushort)card.card_id, (ushort)num);

                    //get svr on
                    short temp = LTDMC.dmc_read_sevon_pin((ushort)card.card_id, (ushort)num);
                    if (temp == (int)logic_svr_on)
                        iostatus |= (uint)MTIO.SVON;

                    //get inp
                    GPIO.IO_STA pbitstate = GPIO.IO_STA.IN_OFF;
                    uint inport = LTDMC.dmc_read_inport((ushort)card.card_id, 1);
                    pbitstate = ((((inport >> (24 + num)) & 0x01) > 0) ? GPIO.IO_STA.IN_OFF : GPIO.IO_STA.IN_ON);
                    if (pbitstate == GPIO.IO_STA.IN_ON)
                        iostatus |= (uint)MTIO.INP;
                    m_mt_io = iostatus;
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    MTIO iosta = 0;
                    int temp = 0;
                    //int err = zmcaux.ZAux_Direct_GetAxisStatus(card.handle, num, ref temp);
                    //if (err == 0)
                    //{
                    //    if (((temp >> 4) & 1) == 1) iosta |= MTIO.LMTP;
                    //    else if (((temp >> 5) & 1) == 1) iosta |= MTIO.LMTN;
                    //    else if (((temp >> 6) & 1) == 1)
                    //        iosta |= MTIO.HOMEING;
                    //    if (((temp >> 9) & 1) == 1)
                    //        iosta |= MTIO.SLMTP;
                    //    else if (((temp >> 10) & 1) == 1) iosta |= MTIO.SLMTN;
                    //    else if (((temp >> 22) & 1) == 1) iosta |= MTIO.ALM;
                    //}
                    //else
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 读取AxisStatus出错，Err:{1}", disc, err));
                    //    res = EM_RES.ERR;
                    //}

                    ////IN(0)设为EMG
                    //uint inport = 0;
                    //err = zmcaux.ZAux_Direct_GetIn(card.handle, 0, ref inport);
                    //if (err == 0 && inport == 0) iosta |= MTIO.EMG;

                    ////SVRON
                    //int itemp =0;
                    //err = zmcaux.ZAux_Direct_GetAxisEnable_new(card.handle, num, ref itemp);
                    //if (err == 0)
                    //{
                    //    if (itemp == logic_svr_on) iosta |= MTIO.SVON;
                    //}
                    //else
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 读取SVRON出错，Err:{1}", disc, err));
                    //    res = EM_RES.ERR;
                    //}

                    ////ORG
                    //if (org_num > 0)
                    //{
                    //    err = zmcaux.ZAux_Direct_GetDatumIn(card.handle, num, ref itemp);
                    //    if (err == 0)
                    //    {
                    //        if (itemp == 1) iosta |= MTIO.ORG;
                    //    }
                    //    else
                    //    {
                    //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 读取ORG出错，Err:{1}", disc, err));
                    //        res = EM_RES.ERR;
                    //    }
                    //}
                    res = UpdateStatusBuff(false);
                    temp = AxStaBuf.mtio;
                    if (((temp >> 4) & 1) == 1)
                        iosta |= MTIO.LMTP;
                    if (((temp >> 5) & 1) == 1)
                        iosta |= MTIO.LMTN;
                    if (((temp >> 6) & 1) == 1)
                        iosta |= MTIO.HOMEING;
                    if (((temp >> 9) & 1) == 1)
                        iosta |= MTIO.SLMTP;
                    if (((temp >> 10) & 1) == 1)
                        iosta |= MTIO.SLMTN;
                    if (((temp >> 22) & 1) == 1)
                        iosta |= MTIO.ALM;

                    if (card.GetIOFrBuff(0, GPIO.IO_DIR.IN) == GPIO.IO_STA.IN_ON)
                        iosta |= MTIO.EMG;
                    if (card.GetIOFrBuff(org_num, GPIO.IO_DIR.IN) == GPIO.IO_STA.IN_ON) iosta |= MTIO.ORG;
                    if (svron_num > -1 && card.GetIOFrBuff(svron_num + num, GPIO.IO_DIR.OUT) == logic_svr_on) iosta |= MTIO.SVON;

                    m_mt_io = (uint)iosta;
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    MTIO iosta = 0;
                    int lGpiValue = 0;
                    int err = mc.GT_ClrSts((short)card.card_id, 1, 8);
                    //get ax io state
                    err += mc.GT_GetSts((short)card.card_id, (short)(num + 1), out GT_pSts, 1, out GT_axis_time);


                    //(*MoStus).bFlagMError = ((AxStus >> 4) & 1);  // 跟随误差越限标志      
                    //(*MoStus).bFlagSmoothStop = ((AxStus >> 7) & 1); // 平滑停止标志                       
                    //(*MoStus).bFlagMotion = ((AxStus >> 10) & 1);   // 规划器运动标志
                    if (err == 0)
                    {


                        if ((GT_pSts & 0x200) != 0)
                            iosta |= MTIO.SVON;

                        if ((GT_pSts & 0x20) != 0)
                        {
                            iosta |= MTIO.LMTP;
                            iosta |= MTIO.SLMTP;
                        }

                        if ((GT_pSts & 0x40) != 0)
                        {
                            iosta |= MTIO.LMTN;
                            iosta |= MTIO.SLMTN;
                        }

                        if ((GT_pSts & 0x100) != 0)
                        {
                            iosta |= MTIO.EMG;
                        }
                        //if (GT_axis_time & (1 << 11) == 1)
                        //    iosta |= MTIO.ORG; 

                        if ((GT_pSts & 0x800) != 0)
                        {
                            iosta |= MTIO.RDY;//没有运动,
                            iosta |= MTIO.INP;//运动到位,
                        }
                        //if (encode == ENC_TYPE.YES)
                        //{
                        //    err = mc.GT_GetAxisError((short)card.card_id, (short)(num + 1), out pos_err, 1, out mclock);
                        //    if (err == 0)
                        //    {
                        //        if (pos_err < 30)//30脉冲差
                        //        {
                        //            iosta |= MTIO.INP;//运动到位,

                        //        }

                        //    }
                        //}

                        if ((GT_pSts & 0x2) != 0)
                            iosta |= MTIO.ALM;

                        m_mt_io = (uint)iosta;
                    }
                    else
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取轴状态异常{1}", disc, err));

                    }
                    err = mc.GT_GetDi((short)card.card_id, mc.MC_HOME, out lGpiValue);
                    if (err == 0)
                    {
                        if ((lGpiValue & (1 << num)) != 0)
                            iosta |= MTIO.ORG;
                    }
                    //err += mc.GT_GetDi((short)card.card_id, mc.MC_LIMIT_NEGATIVE, out lGpiValue);
                    //if (err == 0)
                    //{
                    //    if ((lGpiValue & (1 << num)) != 0)
                    //        iosta |= MTIO.LMTN;
                    //}
                    //err += mc.GT_GetDi((short)card.card_id, mc.MC_LIMIT_POSITIVE, out lGpiValue);
                    //if (err == 0)
                    //{
                    //    if (((lGpiValue>>num)&1 )!= 0)
                    //        iosta |= MTIO.LMTP;
                    //}
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取轴状态异常{1}", disc, err));
                    }
                    m_mt_io = (uint)iosta;

                }
#endif


                return m_mt_io;
            }
        }
        #endregion
        #region 轴IO判定
        public bool isORG
        {
            get { return (mt_io & (uint)MTIO.ORG) == (uint)MTIO.ORG ? true : false; }
        }
        public bool isELN
        {
            get { return (mt_io & (uint)MTIO.LMTN) == (uint)MTIO.LMTN ? true : false; }
        }
        public bool isELP
        {
            get { return (mt_io & (uint)MTIO.LMTP) == (uint)MTIO.LMTP ? true : false; }
        }
        public bool isALM
        {
            get { return (mt_io & (uint)MTIO.ALM) == (uint)MTIO.ALM ? true : false; }
        }
        public bool isSLP
        {
            get { return (mt_io & (uint)MTIO.SLMTP) == (uint)MTIO.SLMTP ? true : false; }
        }
        public bool isSLN
        {
            get { return (mt_io & (uint)MTIO.SLMTN) == (uint)MTIO.SLMTN ? true : false; }
        }
        public bool isINP
        {
            get
            {
                if (mt_type != MT_TYPE.SEVER)
                {
                    if (isStop)
                        return true;
                    else
                        return false;
                }
                return (mt_io & (uint)MTIO.INP) == (uint)MTIO.INP ? true : false;
            }
        }
        public bool isSVRON
        {
            get
            {
                if (mt_type == MT_TYPE.VIRTUAL) return true;
                return (mt_io & (uint)MTIO.SVON) == (uint)MTIO.SVON ? true : false;
            }
        }
        public bool isEMG
        {
            get { return (mt_io & (uint)MTIO.EMG) == (uint)MTIO.EMG ? true : false; }
        }
        public bool isIORDY
        {
            get
            {
                if (mt_type != MT_TYPE.SEVER)
                {
                    if (isStop)
                        return true;
                    else
                        return false;
                }
                return (mt_io & (uint)MTIO.RDY) == (uint)MTIO.RDY ? true : false;
            }
        }
        #endregion
        #region SVR ON/OFF
        public bool SVRON
        {
            get
            {
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //int itemp = 0;
                    //int err = zmcaux.ZAux_Direct_GetAxisEnable_new(card.handle, num, ref itemp);
                    //if (err == 0)
                    //{
                    //    if (itemp == (logic_svr_on == GPIO.IO_STA.OUT_ON ? 1:0)) return true;
                    //}
                    //else
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 读取SVRON出错，Err:{1}", disc, err));
                    //    res = EM_RES.ERR;
                    //}
                    //return false;

                    UpdateStatusBuff();
                    GPIO.IO_STA inport = card.GetIOFrBuff(svron_num + num, GPIO.IO_DIR.OUT);
                    if (inport == logic_svr_on) return true;
                    else return false;
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    return (mt_io & (uint)MTIO.SVON) == (uint)MTIO.SVON ? true : false;
                }
#endif

                return isSVRON;
            }
            set
            {
                if (mt_type == MT_TYPE.VIRTUAL) return;
                int err = 0;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    if (value == true)
                        err = LTDMC.dmc_write_sevon_pin((ushort)card.card_id, (ushort)num, (ushort)logic_svr_on);
                    else
                        err = LTDMC.dmc_write_sevon_pin((ushort)card.card_id, (ushort)num, (ushort)(logic_svr_on > 0 ? 0 : 1));
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    err = zmcaux.ZAux_Direct_SetOp(card.handle, svron_num + num, (uint)(value ? (logic_svr_on == GPIO.IO_STA.OUT_ON ? 1 : 0) : (logic_svr_on == GPIO.IO_STA.OUT_ON ? 0 : 1)));
                    //err = zmcaux.ZAux_Direct_SetAxisEnable(card.handle, num, value ? (logic_svr_on == GPIO.IO_STA.OUT_ON ? 1: 0) : (logic_svr_on == GPIO.IO_STA.OUT_ON ? 0 : 1));
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {

                    err = mc.GT_AxisOn((short)card.card_id, (short)(num + 1)); //

                }
#endif
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 设置SVRON出错，0x{1:X8}", disc, err));
                    res = EM_RES.ERR;
                    return;
                }
                res = EM_RES.OK;
            }
        }
        #endregion
        #region 手轮设置
        public EM_RES SetHandwheel(ushort channel = 0)
        {
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                int err = LTDMC.dmc_set_handwheel_channel((ushort)card.card_id, channel);
                if (res != EM_RES.OK) return res;
                err = LTDMC.dmc_set_counter_inmode((ushort)card.card_id, (ushort)num, 3);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 手轮设置出错，0x{1:X8}", disc, err));
                    res = EM_RES.ERR;
                    return res;
                }
            }
#endif
            return EM_RES.OK;
        }
        #endregion
        #region ERC
        public bool ERC
        {
            get
            {
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    return LTDMC.dmc_read_erc_pin((ushort)card.card_id, (ushort)num) == (short)GPIO.IO_STA.OUT_ON ? true : false;
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {

                    return !isALM;
                }
#endif
                return false;
            }
            set
            {
                if (mt_type == MT_TYPE.VIRTUAL) return;
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    int err = LTDMC.dmc_write_erc_pin((ushort)card.card_id, (ushort)num, (ushort)(value ? GPIO.IO_STA.OUT_ON : GPIO.IO_STA.OUT_OFF));
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 设置ERC出错，0x{1:X8}", disc, err));
                        res = EM_RES.ERR;
                    }
                    else res = EM_RES.OK;
                }

#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    int err = mc.GT_ClrSts((short)card.card_id, (short)(num + 1), 1);
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 清除报警失败，0x{1:X8}", disc, err));
                        res = EM_RES.ERR;
                    }
                }
#endif
            }
        }
        #endregion
        #region 设置触发点
        public EM_RES DisableHcmp()
        {
            double[] pos = new double[1];
            pos[0] = 10000;
            res = SetHcmp(pos, hcmp_io_num, 0);
            return res;
        }
        public EM_RES SetHcmp(double[] pos)
        {
            return SetHcmp(pos, hcmp_io_num, 4);
        }
        public EM_RES SetHcmp(double[] pos, ushort io_num, ushort mode = 4)
        {
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            //check
            if (pos.Length < 1 || mode > 5) return res = EM_RES.PARA_ERR;

            ushort cmp = 0;
            switch (io_num)
            {
                case 12:
                    cmp = 0;
                    break;
                case 13:
                    cmp = 1;
                    break;
                case 14:
                    cmp = 2;
                    break;
                case 15:
                    cmp = 3;
                    break;
                default:
                    return res = EM_RES.PARA_ERR;
            }
            hcmp_io_num = io_num;


            //set io to H
            LTDMC.dmc_write_cmp_pin((ushort)card.card_id, cmp, 0);

            //config,L=active,T=1000us
            res = (EM_RES)LTDMC.dmc_hcmp_set_config((ushort)card.card_id, cmp, (ushort)num, 1, 0, 1000);
            if (res != EM_RES.OK)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 触发设置出错,{1}", disc, res));
                return res;
            }

            //set mode
            LTDMC.dmc_hcmp_clear_points((ushort)card.card_id, cmp);
            res = (EM_RES)LTDMC.dmc_hcmp_set_mode((ushort)card.card_id, cmp, mode);
            if (res != EM_RES.OK)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 触发模式设置出错 Err,{1}", disc, res));
                return res;
            }

            //clear
            res = (EM_RES)LTDMC.dmc_hcmp_clear_points((ushort)card.card_id, cmp);
            if (res != EM_RES.OK)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 清除触发点出错 Err,{1}", disc, res));
                return res;
            }

            if (mode != 0)
            {
                //add point
                for (int n = 0; n < pos.Length; n++)
                {
                    if (pos[n] > slp || pos[n] < sln)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 增加触发点 {1:F3} 超软限位范围[{2:F3},{3:F3}]！", disc, pos[n], sln, slp));
                        return res = EM_RES.PARA_ERR;
                    }
                    res = (EM_RES)LTDMC.dmc_hcmp_add_point((ushort)card.card_id, cmp, (int)(pos[n] * pul_per_mm));
                    if (res != EM_RES.OK)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 增加触发点出错,{1}", disc, res));
                        return res;
                    }
                }
            }
            else
            {
                //set io to H
                LTDMC.dmc_write_cmp_pin((ushort)card.card_id, cmp, 0);
                //LTDMC.dmc_write_outbit(card_id, hcmp_io_num, 0);
            }

            return EM_RES.OK;
        }
        //已触发点数
        public int HcmpTriCnt()
        {
            int PointNum = 0;
            res = (EM_RES)LTDMC.dmc_compare_get_points_runned_extern((ushort)card.card_id, ref PointNum);
            if (res != EM_RES.OK) return PointNum;
            else return 0;
        }
        #endregion
        #region 停止
        public EM_RES Stop(bool bemg = false)
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;
            int err = 0;
#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                //dec stop
                for (int trycnt = 0; trycnt < 5; trycnt++)
                {
                    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} STOP", disc));
                    err = LTDMC.dmc_stop((ushort)card.card_id, (ushort)num, 0);
                    if (err == 0) break;
                    Thread.Sleep(1);
                }

                //EMG stop
                if (bemg || err != 0)
                {
                    LTDMC.dmc_emg_stop((ushort)card.card_id);
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 停止出错，{1}", disc, res));
                        res = EM_RES.ERR;
                    }
                }
            }
#endif
#if GOOGOLTECH
            if (card.brand == CARD.BRAND.GOOGOLTECH)
            {
                //dec stop
                for (int trycnt = 0; trycnt < 5; trycnt++)
                {
                    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} STOP", disc));
                    err = mc.GT_Stop((short)card.card_id, (int)(1 << num), 0);//平滑停止
                    if (err == 0) break;
                    Thread.Sleep(10);
                }

                //EMG stop
                if (bemg || err != 0)
                {
                    err = mc.GT_Stop((short)card.card_id, (int)(1 << num), (int)(1 << num));//急停                    
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 停止出错，{1}", disc, res));
                        res = EM_RES.ERR;
                    }
                }
            }
#endif
#if ZMOTION
            if (card.brand == CARD.BRAND.ZMOTION)
            {
                //dec stop
                for (int trycnt = 0; trycnt < 5; trycnt++)
                {
                    err = zmcaux.ZAux_Direct_Singl_Cancel(card.handle, num, 2);
                    if (err == 0) break;
                    Thread.Sleep(1);
                }

                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 停止出错，{1}", disc, res));
                    res = EM_RES.ERR;
                }
            }
#endif
            return res;
        }
        #endregion
        #region 等待回零停止
        public EM_RES WaitHomeStop(ref bool bquit, int timeout_ms = 10000)
        {
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;
            int timeout = timeout_ms;
            //wait stop
            while (true)
            {
                //quit
                if (bquit)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 异常停止,{1}！", disc, str_status));
                    Stop();
                    return res = EM_RES.QUIT;
                }

                //check safe protect
                if (ChkSafeSen != null && EM_RES.OK != ChkSafeSen(id))
                {
                    //if (VAR.gsys_set.status == CONST.SYS_STATUS_RESET) 
                    Stop();
                    return res = EM_RES.MOVE_PROTECT;
                }
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //axis is stop     
                    int pos = cmd_pos;
                    if (1 == LTDMC.dmc_check_done((ushort)card.card_id, (ushort)num))
                    {
                        Application.DoEvents();
                        Thread.Sleep(100);
                        if (1 == LTDMC.dmc_check_done((ushort)card.card_id, (ushort)num))
                        {
                            if (pos != cmd_pos) continue;
                            if (bquit) return res = EM_RES.QUIT;
                            if (status != AX_STA.READY && ((home_dir == AX_DIR.N && status != AX_STA.ELN) || (home_dir == AX_DIR.P && status != AX_STA.ELP)))
                            {
                                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 异常停止,{1}！", disc, str_status));
                                Stop();
                                return res = EM_RES.ERR;
                            }
                            break;
                        }
                    }
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //axis is stop     
                    int temp = 0;
                    int err = zmcaux.ZAux_Direct_GetIfIdle(card.handle, num, ref temp);
                    if (err == 0 && temp == -1)
                    {
                        if (bquit)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.WAR, string.Format("{0} 用户取消停止！", disc));
                            return res = EM_RES.QUIT;
                        }
                        if (status != AX_STA.READY)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 异常停止,{1}！", disc, str_status));
                            Stop();
                            return res = EM_RES.ERR;
                        }
                        break;
                    }
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    //axis is stop
                    //short cap_home_ret = 0;
                    //int capture_pos;
                    //uint m_parm = 0;

                    //int err = mc.GT_SetCaptureMode((short)card.card_id, (short)(num + 1), mc.CAPTURE_INDEX);
                    // err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                    //if (0 != cap_home_ret)
                    //{
                    //    Stop();
                    //    break;
                    //}
                    //if (isStop || (err != 0))
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未找到原点,0x{1:X8}", disc, res));
                    //    home_status = HOME_STA.ERROR;
                    //    return res = EM_RES.ERR;
                    //}
                    if (isStop)
                    {
                        Thread.Sleep(50);
                        if (isStop)
                        {
                            if ((status != AX_STA.READY) && (
                                ((home_dir == AX_DIR.N) && (status != AX_STA.ELN)) ||
                                ((home_dir == AX_DIR.P) && (status != AX_STA.ELP))))
                            {
                                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 异常停止,{1}！", disc, str_status));
                                Stop();
                                return res = EM_RES.ERR;
                            }                           
                            break;
                        }
                    }

                }
#endif
                //time out
                if (timeout_ms <= 0)
                {
                    Stop();
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 等待停止超时({1}ms)！", disc, timeout));
                    return res = EM_RES.TIMEOUT;
                }
                if (timeout_ms > 0)
                {
                    timeout_ms -= 50;
                    Thread.Sleep(50);
                    Application.DoEvents();
                }
            }
            Thread.Sleep(100);

            return res = EM_RES.OK;
        }

        #endregion
        #region 等待轴停止
        public EM_RES WaitForStop(ref bool pbquit, int timeout_ms, bool bdoevent = false, bool bshowmsg = true)
        {
            //check
            res = ChkParam();
            if (res != EM_RES.OK)   return res;
            int timeout = timeout_ms;
            //wait stop
            while (true)
            {
                //quit
                if (pbquit)
                    return res = EM_RES.QUIT;
                //check safe protect
                if (ChkSafeSen != null && EM_RES.OK != ChkSafeSen(id))
                {
                    if (VAR.gsys_set.status == EM_SYS_STA.RESET) 
                        Stop();
                }
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //axis is stop             
                    if (1 == LTDMC.dmc_check_done((ushort)card.card_id, (ushort)num))
                    {
                        Thread.Sleep(1);
                        if (1 == LTDMC.dmc_check_done((ushort)card.card_id, (ushort)num))
                        {
                            if (pbquit) return res = EM_RES.QUIT;
                            if (status != AX_STA.READY)
                            {
                                if (bshowmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 异常停止,{1}！", disc, str_status));
                                Stop();
                                return res = EM_RES.ERR;
                            }
                            break;
                        }
                    }
                }

#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    //axis is stop 
                    Thread.Sleep(10);
                    if (isStop)   //已经停止
                    {
                         Thread.Sleep(10);
                         if (isStop)   break;
                         
                    }

                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //axis is stop     
                    int temp = 0;
                    int err = zmcaux.ZAux_Direct_GetIfIdle(card.handle, num, ref temp);
                    if (err == 0 && temp == -1)
                    {
                        if (pbquit) return res = EM_RES.QUIT;
                        if (status != AX_STA.READY)
                        {
                            if (bshowmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 异常停止,{1}！", disc, str_status));
                            Stop();
                            return res = EM_RES.ERR;
                        }
                        break;
                    }
                }
#endif
                //time out
                if (timeout_ms <= 0)
                {
                    Stop();
                    if (bshowmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 等待停止超时({1}ms)！", disc, timeout));
                    return res = EM_RES.TIMEOUT;
                }
                if (timeout_ms > 0)
                {
                    timeout_ms = timeout_ms-10;
                    Thread.Sleep(10);
                    if (bdoevent) Application.DoEvents();
                }
            }
            return res = EM_RES.OK;
        }
        #endregion

        #region 等待轴INP
        public EM_RES WaitINP(ref bool pbquit, int timeout_ms = 100, bool bdoevent = false)
        {
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;
            int time_temp = timeout_ms;
            //wait stop
            while (true)
            {
                //quit
                if (pbquit)
                    return res = EM_RES.QUIT;
                //check safe protect
                if (ChkSafeSen != null && EM_RES.OK != ChkSafeSen(id))
                {
                    if (VAR.gsys_set.status == EM_SYS_STA.RESET)
                        Stop();
                }
                //axis is stop             
                if (isINP) break;

                //time out
                if (timeout_ms <= 0)
                {
                    //Stop();
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 等待到位信号超时({1}ms)！", disc, time_temp));
                    return res = EM_RES.TIMEOUT;
                }
                if (timeout_ms > 0)
                {
                    timeout_ms--;
                    Thread.Sleep(1);
                    if (bdoevent) Application.DoEvents();
                }
            }
            return res = EM_RES.OK;
        }
        #endregion
        #region 等待定位完成
        public EM_RES WaitForMoveDone(ref bool pbquit, double targetpos, int timeout_ms, bool bdoevent = false)
        {
          if(!isStop)
          {
              res = WaitForStop(ref pbquit, timeout_ms, bdoevent);
              if (res != EM_RES.OK) return res;
          }
          if (isELN || isELP)
              return EM_RES.PARA_OUTOFRANG;
            if (Math.Abs(targetpos  - fcmd_pos) >1)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.WAR, string.Format("{0}异常停止,MtIO:0x{1:X8},tag:{2:F3},cmd:{3:F3},enc:{4:F3}", disc, mt_io, targetpos, fcmd_pos, fenc_pos));
                return res = EM_RES.ERR;              
            }
            return res = EM_RES.OK;
        }
        #endregion
        #region 速度曲线PVT
        #region 获取两个速度之间的距离
        EM_RES get_acc_s(double start_spd, double stop_spd, double a, ref double s, ref double t)
        {
            double min, max;
            if (start_spd > stop_spd)
            {
                max = start_spd;
                min = stop_spd;
            }
            else
            {
                min = start_spd;
                max = stop_spd;
            }

            t = (max - min) / a;
            //if (t < 0.05) t = 0.05;
            s = min * t + a * t * t / 2;

            return EM_RES.OK;
        }
        double get_acc_s(double start_spd, double stop_spd, double a)
        {

            double min, max;
            if (start_spd > stop_spd)
            {
                max = start_spd;
                min = stop_spd;
            }
            else
            {
                min = start_spd;
                max = stop_spd;
            }

            double t = (max - min) / a;
            double s = min * t + a * t * t / 2;

            return s;
        }
        #endregion
        #region 计算两点距离能达到的最大速度
        EM_RES get_spd_s(double spd_start, double spd_end, double a, double dis, ref double vh)
        {
            double min, max;
            max = spd_start > spd_end ? spd_start : spd_end;
            min = spd_start < spd_end ? spd_start : spd_end;

            double ss = 0;
            double tt = 0;
            get_acc_s(spd_start, spd_end, a, ref ss, ref tt);
            if (ss > dis)
            {
                return EM_RES.ERR;
            }

            double t1 = (max - min) / a;
            double c = a * t1 * t1 / 2 + min * t1 - dis;
            double b = 2 * max;

            double t2 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            vh = max + a * t2;

            return EM_RES.OK;
        }
        #endregion
        public EM_RES PVTMove(double tri_spd, double mov_spd, double[] tri_pos, double stop_pos)
        {

            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;

            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            //debug
            string str = string.Format("PVT:{0},飞拍速度{1},定位速度{2}", disc, tri_spd, mov_spd);
            foreach (double pp in tri_pos) str = str + string.Format(",Tri{0:F3}", pp);
            str = str + string.Format(",STOP{0:F3}", stop_pos);
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, str);

            double[] p = new double[10];
            double[] t = new double[10];
            double[] spd = new double[10];
            uint n = 0;

            //calc a
            double tacc = this.tacc * 1.2;
            double a = mov_spd / tacc;
            if (a > max_acc) a = max_acc;

            //get curret pos
            double cur_pos = fcmd_pos;
            //get the near point
            double cap_p = Math.Abs(tri_pos.Min() - cur_pos) < Math.Abs(tri_pos.Max() - cur_pos) ? tri_pos.Min() - cur_pos : tri_pos.Max() - cur_pos;
            double stop_p = stop_pos - cur_pos;

            //direct
            int dir_p = cap_p > stop_p ? -1 : 1;
            cap_p = System.Math.Abs(cap_p);
            stop_p = System.Math.Abs(stop_p);

            // cap start,end pos
            double cap_p_b = cap_p - 10;
            double cap_p_e = cap_p + 10 + System.Math.Abs(tri_pos.Max() - tri_pos.Min());

            //make table
            n = 0;
            p[n] = 0;
            t[n] = 0;
            n++;

            double ds = 0;
            double dt = 0;
            //取料到飞拍点不能达到最高速度
            if (cap_p_b < (get_acc_s(0, mov_spd, a) + get_acc_s(mov_spd, tri_spd, a)))
            {
                //升到最大移动速度
                res = get_spd_s(0, tri_spd, a, cap_p_b, ref spd[n]);
                if (res != EM_RES.OK) return res;
                get_acc_s(0, spd[n], a, ref ds, ref dt);
                p[n] = p[n - 1] + ds;
                t[n] = t[n - 1] + dt;
                n++;

                //降到飞拍速度
                p[n] = cap_p_b;
                t[n] = t[n - 1] + Math.Abs(tri_spd - spd[n - 1]) / a;
                spd[n] = tri_spd;
                n++;
            }
            //取料到飞拍点能达到最高速度
            else
            {
                //检查是否匀速段过小
                double mov_spd_temp;
                ds = cap_p_b - get_acc_s(mov_spd, tri_spd, a);
                dt = ds / mov_spd;
                if (dt > 0 && dt < 0.1)
                {
                    mov_spd_temp = ds / 0.1;
                    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, "dt=" + dt.ToString("F3"));
                }
                else mov_spd_temp = mov_spd;

                //升到最大移动速度
                spd[n] = mov_spd_temp;
                get_acc_s(0, spd[n], a, ref ds, ref dt);
                p[n] = p[n - 1] + ds;
                t[n] = t[n - 1] + dt;
                n++;

                //匀速
                p[n] = cap_p_b - get_acc_s(mov_spd_temp, tri_spd, a);
                t[n] = t[n - 1] + (p[n] - p[n - 1]) / mov_spd_temp;
                spd[n] = mov_spd_temp;
                n++;

                //降到飞拍速度
                p[n] = cap_p_b;
                t[n] = t[n - 1] + tacc;
                spd[n] = tri_spd;
                n++;
            }

            //匀速飞拍
            //const move  to cap_p+1mm;
            p[n] = cap_p_e;
            t[n] = t[n - 1] + System.Math.Abs(p[n] - p[n - 1]) / tri_spd;
            spd[n] = tri_spd;
            n++;

            //移动到终点，达不到最高速度
            if (System.Math.Abs(stop_p - p[n - 1]) <= (get_acc_s(tri_spd, mov_spd, a) + get_acc_s(0, mov_spd, a)))
            {
                //升到最大移动速度
                res = get_spd_s(tri_spd, 0, a, stop_p - cap_p_e, ref spd[n]);
                if (res != EM_RES.OK) return res;
                get_acc_s(tri_spd, spd[n], a, ref ds, ref dt);
                p[n] = p[n - 1] + ds;
                t[n] = t[n - 1] + dt;
                n++;

                //降速到0
                get_acc_s(spd[n - 1], 0, a, ref ds, ref dt);
                p[n] = stop_p;
                t[n] = t[n - 1] + dt;
                spd[n] = 0;
                n++;
            }
            else
            {
                //检查是否匀速段过小
                double mov_spd_temp;
                ds = stop_p - get_acc_s(mov_spd, 0, a);
                dt = ds / mov_spd;
                if (dt > 0 && dt < 0.1)
                {
                    mov_spd_temp = ds / 0.1;
                    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "dt=" + dt.ToString("F3"));
                }
                else mov_spd_temp = mov_spd;

                //升到最大移动速度
                get_acc_s(tri_spd, mov_spd_temp, a, ref ds, ref dt);
                p[n] = p[n - 1] + ds;
                t[n] = t[n - 1] + dt;
                spd[n] = mov_spd_temp;
                n++;

                //匀速
                p[n] = stop_p - get_acc_s(mov_spd_temp, 0, a);
                t[n] = t[n - 1] + (p[n] - p[n - 1]) / mov_spd_temp;
                spd[n] = mov_spd_temp;
                n++;

                //降速到0
                get_acc_s(spd[n - 1], 0, a, ref ds, ref dt);
                p[n] = stop_p;
                t[n] = t[n - 1] + dt;
                spd[n] = 0;
                n++;
            }

            //set ptt table
            int[] pos = new int[10];
            for (int i = 0; i < n; i++)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("T={0:F3},P={1:F3},SPD={2:F3}", t[i], p[i], spd[i]));
                // mm-> puls
                pos[i] = (int)(p[i] * pul_per_mm * dir_p);
                spd[i] = (int)(spd[i] * pul_per_mm * dir_p);

            }
            res = (EM_RES)LTDMC.dmc_PvtTable((ushort)card.card_id, (ushort)num, n, t, pos, spd);
            if (res != EM_RES.OK)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 设置PVT表格出错，{1}", disc, res));
                return res;
            }

            //check safe
            res = ChkSafeSen(num);
            if (res != EM_RES.OK) return res;
            res = ChkSafePos(num);
            if (res != EM_RES.OK) return res;

            //set triger
            res = SetHcmp(tri_pos, hcmp_io_num);
            if (res != EM_RES.OK) return res;

            //start pvt       
            ushort[] axislist = new ushort[1];
            axislist[0] = (ushort)num;
            res = (EM_RES)LTDMC.dmc_PvtMove((ushort)card.card_id, 1, axislist);
            if (res != EM_RES.OK)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} Pvt运行出错,{1}", disc, res));
                return res;
            }

            //targetpos = stop_pos;
            return EM_RES.OK;
        }
        #endregion
        #region 定位
        /// <summary>
        /// 定位
        /// </summary>
        /// <param name="bquit"></param>
        /// <param name="pos">目标位置</param>
        /// <param name="wait_ms">超时时间</param>
        /// <param name="bdoevent">线程刷新等待 </param>
        /// <param name="bchkpos">True:基于坐标安全检查(ChkSafePos)</param>
        /// <returns></returns>
        public EM_RES MoveTo(ref bool bquit, double pos, int wait_ms = 0, bool bdoevent = false, bool bchkpos = true)
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;
            //check param
            res = ChkParam();
            if (res != EM_RES.OK) return res;
            int try_cnt = 0;
        TRYAGAIN:
            //用户取消
            if (bquit == true) return res = EM_RES.QUIT;
            if (try_cnt > 0)
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.WAR, "ABS_MOVE_TO(),重试!");
            if (try_cnt++ >= 3) return res;

            //在范围+/- 3puls 则OK
            if (Math.Abs(cmd_pos - pos * pul_per_mm) < 3)
            {
                //targetpos = pos;
                return res = EM_RES.OK;
            }

            //软限位置
            if (pos > slp || pos < sln)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 目标位 {1:F3} 超软限位范围 [{2:F3},{3:F3}]", disc, pos, sln, slp));
                return res = EM_RES.PARA_ERR;
            }

            //check safe sensor
            if (ChkSafeSen != null)
            {
                res = ChkSafeSen(id);
                if (res != EM_RES.OK) return res;
            }

            //check safe pos
            if (bchkpos && ChkSafePos != null)
            {
                res = ChkSafePos(id, pos);
                if (res != EM_RES.OK) return res;
            }
            //check state 
            switch (status)
            {
                case AX_STA.DIS:
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + str_status);
                    res = EM_RES.ERR; //未上电
                    break;
                case AX_STA.ALM:
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + str_status);
                    res = EM_RES.ERR;
                    break;
                case AX_STA.PTP:
                    //change target pos
                    //if (Math.Abs(fcmd_pos - pos) > 50)
                    //{
                    //    //check safe sensor
                    //    if (ChkSafeSen != null)
                    //    {
                    //        res = ChkSafeSen(id);
                    //        if (res != EM_RES.OK) return res;
                    //    }

                    //    //check safe pos
                    //    if (ChkSafePos != null)
                    //    {
                    //        res = ChkSafePos(id);
                    //        if (res != EM_RES.OK) return res;
                    //    }

                    //    res = LTDMC.dmc_reset_target_position(card_id, num, (int)(pos * pul_per_mm), 0);
                    //    if (res != EM_RES.OK)
                    //    {
                    //        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} reset_target_position res, 0x{1:X8}", disc, res));
                    //        res = EM_RES.ERR;
                    //    }
                    //    else targetpos = pos;
                    //}
                    //else
                    {
                        //wait stop and retry
                        res = WaitForStop(ref bquit, 9000, bdoevent);
                        if (res != EM_RES.OK)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 等待定位完成超时(5S)!", disc));
                            res = EM_RES.ERR;
                            goto TRYAGAIN;
                        }
                    }
                    break;
                case AX_STA.ELN:
                case AX_STA.ELP:
                case AX_STA.SLP:
                case AX_STA.SLN:
                case AX_STA.READY:
                    if ((status == AX_STA.ELN || status == AX_STA.SLN) && pos < fcmd_pos)
                    {
                     //   VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 异常, {1}", disc, str_status));
                        res = EM_RES.OK;
                        break;
                    }
                    if ((status == AX_STA.ELP || status == AX_STA.SLP) && pos > fcmd_pos)
                    {
                        //VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 异常, {1}", disc, str_status));
                        res = EM_RES.OK;
                        break;
                    }
                    //check safe sensor
                    if (ChkSafeSen != null)
                    {
                        res = ChkSafeSen(id);
                        if (res != EM_RES.OK) return res;
                    }

                    //check safe pos
                    if (ChkSafePos != null)
                    {
                        res = ChkSafePos(id, pos);
                        if (res != EM_RES.OK) return res;
                    }

                    //ttl distance
                    dis_ttl += Math.Abs(pos - fcmd_pos);


                    //ready and start move
                    //targetpos = pos;
                    int err = 0;
#if LEADSHINE
                    if (card.brand == CARD.BRAND.LEADSHINE)
                    {
                        err = LTDMC.dmc_pmove((ushort)card.card_id, (ushort)num, (int)(pos * pul_per_mm), 1);
                        if (err != 0)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 定位命令出错，0x{1:X8}", disc, err));
                            res = EM_RES.ERR;
                        }
                    }
#endif
#if ZMOTION
                    if (card.brand == CARD.BRAND.ZMOTION)
                    {

                        err = zmcaux.ZAux_Direct_Singl_MoveAbs(card.handle, num, (float)(pos * pul_per_mm));
                        if (err != 0)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 定位命令出错，Err:{1}", disc, err));
                            res = EM_RES.ERR;
                        }
                    }
#endif
#if GOOGOLTECH
                    if (card.brand == CARD.BRAND.GOOGOLTECH)
                    {
                        //mc.TTrapPrm m_axis_prm;
                        //m_axis_prm.acc = Tacc;
                        //m_axis_prm.dec = Tdec;
                        //m_axis_prm.velStart = spd_start * pul_per_mm;
                        //m_axis_prm.smoothTime = (short)(ts * 1000);
                        //err = mc.GT_SetTrapPrm((short)card.card_id, (short)(num + 1), ref m_axis_prm);
                        EM_RES ret = SetToWorkSpd();
                        if (ret != EM_RES.OK) MessageBox.Show("速度设置异常!");
                        err = mc.GT_SetPos((short)card.card_id, (short)(num + 1), (int)(pos * pul_per_mm));
                        err += mc.GT_Update((short)card.card_id, 1 << num);
                                           
                        if (err != 0)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 定位命令出错，Err:{1}", disc, err));
                            res = EM_RES.ERR;
                        }

                    }
#endif
                    //wait for start move
                    Thread.Sleep(50);
                    break;
                default:
                    //res status
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 异常, {1}", disc, str_status));
                    res = EM_RES.ERR;
                    break;
            }

            //wait for done
            if (wait_ms != 0)
            {
                res = WaitForMoveDone(ref bquit, pos, wait_ms, bdoevent);
                if (res == EM_RES.ERR)
                {
                    goto TRYAGAIN;
                }
                else
                return res; //限位  EM_RES.PARA_OUTOFRANG
            }

            return EM_RES.OK;
        }
        #endregion
        #region JOG运动
        public void JOG_ChkAndStop(double t = 0.5)
        {
            if (!isStop)
            {
                if (JOGDir == AX_DIR.P)
                {
                    if ((slp - fcmd_pos) < t * spd_manual_high) Stop();
                }
                else
                {
                    if ((fcmd_pos - sln) < t * spd_manual_high) Stop();
                }
            }
        }
        public EM_RES JOG_Step(ref bool bquit, AX_DIR dir)
        {
            EM_RES ret=SetToManualHighSpd();
            if (ret != EM_RES.OK) MessageBox.Show("速度设置异常!");
           // double m_pref_pos;
           // uint m_test = 1;
           //int  err = mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
           //if (dir == AX_DIR.N)
           //{

           //    ret = MoveTo(ref bquit, (int)(m_pref_pos / pul_per_mm) - 1, 10000, true);
           //}
           //else
           //ret = MoveTo(ref bquit, (int)(m_pref_pos / pul_per_mm)+ 1, 10000, true);
           // if (ret != EM_RES.OK) 
           //     MessageBox.Show("定位异常!");
           
             ret = JOG_Move(ref bquit, dir, spd_manual_high, true, manual_step);


            bkey = false;
            return ret;
        }
        public EM_RES JOG_Step(ref bool bquit, AX_DIR dir, double step)
        {
            SetToManualHighSpd();
            EM_RES ret = JOG_Move(ref bquit, dir, spd_manual_high, true, step);
            bkey = false;
            return ret;
        }
        public EM_RES JOG_Step(ref bool bquit, AX_DIR dir, double step, double spd)
        {
            EM_RES ret = SetSpeed(spd);
            if (ret != EM_RES.OK) return ret;
            ret = JOG_Move(ref bquit, dir, spd, true, step);
            bkey = false;
            return ret;
        }
        public EM_RES JOG_VMove(ref bool bquit, AX_DIR dir)
        {
            if (bman_high_spd) res = SetToManualHighSpd();
            else res = SetToManualLowSpd();
            if (res != EM_RES.OK) return res;

            res = JOG_Move(ref bquit, dir, bman_high_spd ? spd_manual_high : spd_manual_low, false, 0);
            return res;
        }
        public EM_RES JOG_VMove(ref bool bquit, AX_DIR dir, double spd)
        {
            if (spd > spd_manual_high + 1) res = SetToManualHighSpd();
            if (spd < spd_manual_low - 1) res = SetToManualLowSpd();
            if (res != EM_RES.OK) return res;

            res = JOG_Move(ref bquit, dir, mspd_cur, false, 0);
            return res;
        }

        public EM_RES JOG_Move(ref bool bquit, AX_DIR dir, double spd, bool bstep = false, double step = 0)
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;

            //check param
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            //check pos every enter
            if (dir == AX_DIR.P && fcmd_pos > slp || dir == AX_DIR.N && fcmd_pos < sln)
            {
                Stop();
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 点动，软限位!", disc));
                return res = EM_RES.ERR;
            }

            //run once time before release
            if (bkey) return EM_RES.ABORT;
            //key down
            bkey = true;

            //check safe sensor
            if (ChkSafeSen != null)
            {
                //check safe sensor
                res = ChkSafeSen(id);
                if (res == EM_RES.SAFE_PROTECT)
                {
                    Stop();
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 点动, 安全防护 ，停止!", disc));
                    return res;
                }
            }

            //check safe pos
            if (ChkSafePos != null)
            {
                res = ChkSafePos(id);
                if (res != EM_RES.OK) return res;
            }

            //not ready then return            
            if (status != AX_STA.READY)
            {
                if (dir == AX_DIR.N && (status == AX_STA.ELP || status == AX_STA.SLP)) { ;}
                else if (dir == AX_DIR.P && (status == AX_STA.ELN || status == AX_STA.SLN)) { ;}
                else
                {
                    Stop();
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 点动出错,{1}", disc, str_status));
                    return res = EM_RES.OK;
                }
            }

            //set speed	
            res = SetSpeed(spd);
            if (res != EM_RES.OK && res != EM_RES.BUSY)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 点动设置速度出错!", disc));
                return EM_RES.ERR;
            }

            if (spd != spd_cur)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 点动速度异常!", disc));
                return EM_RES.ERR;
            }

            // vmove
            JOGDir = dir;
            int err = 0;
            int dis = (int)(step * pul_per_mm + 0.5);
            if (bstep == false)
            {
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    err = LTDMC.dmc_vmove((ushort)card.card_id, (ushort)num, (ushort)dir);
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    err = zmcaux.ZAux_Direct_Singl_Vmove(card.handle, num, dir == AX_DIR.P ? 1 : -1);
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
               //     mc.TJogPrm GT_jog;
             //       GT_jog.acc = tacc;
              //      GT_jog.dec = tdec;
            //        GT_jog.smooth = 25;
             //       err = mc.GT_PrfJog((short)card.card_id, (short)(num + 1));
            //        err += mc.GT_SetJogPrm((short)card.card_id, (short)(num + 1), ref GT_jog);
            //        err += mc.GT_SetVel((short)card.card_id, (short)(num + 1), spd * pul_per_mm/1000);
           //         err += mc.GT_Update((short)card.card_id, 1 << num);
                    mc.TTrapPrm m_axis_prm;
                    m_axis_prm.acc = tacc;
                    m_axis_prm.dec = tdec;
                    m_axis_prm.velStart = spd_start * pul_per_mm;
                    m_axis_prm.smoothTime = 25;
                    err = mc.GT_SetTrapPrm((short)card.card_id, (short)(num + 1), ref m_axis_prm);
                    err = mc.GT_SetVel((short)card.card_id, (short)(num + 1), spd * pul_per_mm / 1000);
                    if (dir == AX_DIR.N)
                    {
                        dis = -dis;
                    }
                    double m_pref_pos;
                    uint m_test = 1;
                    err = mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    //int mret = mc.GT_GetAxisEncPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    if (err == 0)
                    {
                        err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), (int)m_pref_pos + dis);
                        err += mc.GT_Update((short)card.card_id, 1 << num);
                    }
                }
#endif
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} JOG定速命令错误,Err:{1}！", disc, err));
                    Stop();
                    return res = EM_RES.ERR;
                }
            }
            //step move
            else
            {
                //check soft limit
                step = Math.Abs(step);
                if (step > Math.Abs(slp - sln) / 5) step = 1;

                if (dir == AX_DIR.P && (fcmd_pos + step) > slp || dir == AX_DIR.N && (fcmd_pos - step) < sln)
                {
                    if (step > 1) step = 1;
                    else step = 0.1;
                    if (dir == AX_DIR.P && (fcmd_pos + step) > slp || dir == AX_DIR.N && (fcmd_pos - step) < sln)
                    {
                        Stop();
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 点动，软限位!", disc));
                        return res = EM_RES.ERR;
                    }
                }
                //calc step
              
#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    err = LTDMC.dmc_pmove((ushort)card.card_id, (ushort)num, (dir == AX_DIR.P) ? dis : -dis, 0);
                    if (err == 4)
                    {
                        res = EM_RES.BUSY;
                        return res;
                    }
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    //double now_pos = 0;
                    //uint gt_olock = 0;

                    //int mret;
                    //mret = mc.GT_GetPrfPos((short)(card.card_id), (short)(num + 1), out now_pos, 1, out gt_olock);
                    //if (mret != 0)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 获取当前位置失败Err:{1}！", disc, err));
                    //    Stop();
                    //    return res = EM_RES.ERR;
                    //}
                    //res = MoveTo(ref bquit, now_pos / pul_per_mm + ((dir == AX_DIR.P) ? step : -step), 99000);
                    mc.TTrapPrm m_axis_prm;
                    m_axis_prm.acc = tacc;
                    m_axis_prm.dec = tdec;
                    m_axis_prm.velStart = spd_start * pul_per_mm;
                    m_axis_prm.smoothTime = 25;
                    err = mc.GT_SetTrapPrm((short)card.card_id, (short)(num + 1), ref m_axis_prm);
                    err = mc.GT_SetVel((short)card.card_id, (short)(num + 1), spd * pul_per_mm/1000);
                    if(dir == AX_DIR.N)
                    {
                      dis = -dis;
                    }
                    double m_pref_pos;
                    uint m_test = 1;
                     err = mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    //int mret = mc.GT_GetAxisEncPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    if (err == 0)
                    {
                        err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), (int)m_pref_pos + dis);
                        err += mc.GT_Update((short)card.card_id, 1 << num);
                    }
                  
                    if (err != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} step命令出错，Err:{1}", disc, err));
                        res = EM_RES.ERR;
                    }
                    bool mquit = false;
                    //res = WaitForStop(ref mquit, 2000, false);
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    err = zmcaux.ZAux_Direct_Singl_Move(card.handle, num, (dir == AX_DIR.P) ? dis : -dis);
                }
#endif
                if (err != 0)
                {
                    res = EM_RES.ERR;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} JOG点动命令出错, Err:{1}", disc, err));
                    Stop();
                }

                WaitForStop(ref bquit, 2000);
                bkey = false;
                SetToManualHighSpd();
            }
            return res;
        }
        public EM_RES JOG_Stop()
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;
            bkey = false;
            Stop();
            bool bquit = false;
            res = WaitForStop(ref bquit, 1000, true);
            if (res == EM_RES.OK) res = SetToManualHighSpd();
            return res;
        }
        #endregion
        # region 回零
        public void SetHomeParam(double spd, double offset, ushort mode = 2, AX_DIR dir = AX_DIR.N)
        {
            home_dir = dir;
            home_offset = offset;
            home_mode = mode;
            home_spd = spd;
        }
        public EM_RES HomeOffset(ref bool bquit, int timeout_ms = 100, double offset = double.MaxValue, bool bdoevent = true)
        {
            //wait home completed
            //auto time
            if (timeout_ms == -1)
            {
                timeout_ms = (int)(1000 * Math.Abs(slp - sln) / spd_cur + 3000);
                if (timeout_ms < 5000) timeout_ms = 5000;
                if (timeout_ms > 20000) timeout_ms = 20000;
            }
            if (timeout_ms != 0)
            {
                res = WaitHomeStop(ref bquit, timeout_ms);
                if (res != EM_RES.OK)
                {
                    home_status = HOME_STA.ERROR;
                    return res;
                }
            }

            Stop();
            Thread.Sleep(100);

            if (offset == double.MaxValue)
                offset = home_offset;

            //check offset
            if (offset > slp || offset < sln)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 偏移={1}，超出软限位范围！", disc, offset));
                home_status = HOME_STA.ERROR;
                return EM_RES.PARA_ERR;
            }

            //offset pos
            try
            {
                //make sure stop
                bool quit = false;
                home_task_ret = WaitHomeStop(ref quit, timeout_ms);
                if (home_task_ret != EM_RES.OK) return home_task_ret;

                fcmd_pos = -offset;
                if (encode == ENC_TYPE.YES)
                    fenc_pos = -offset;
            }

            catch (System.ArgumentException ex)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ex.Message);
                home_status = HOME_STA.ERROR;
                return res = EM_RES.ERR;
            }
            //set spd	
            res = SetSpeed(home_spd * 3);
            //if (res != EM_RES.OK) return res;

            //timeout
            int ms = (int)(1000 * Math.Abs(slp - sln) / spd_cur + 3000);
            if (ms < 5000) ms = 5000;
            if (ms > 20000) ms = 20000;
            //move
            res = MoveTo(ref bquit, offset, ms, bdoevent);

            if (res != EM_RES.OK)
            {
                home_status = HOME_STA.ERROR;
                return res;
            }
            else
                home_status = HOME_STA.OK;
           int err= mc.GT_ZeroPos((short)card.card_id, (short)(num + 1), 1);
            if (err != 0)
            {
                home_status = HOME_STA.ERROR;
                return res;
            }                 
            return res;
        }

        public EM_RES Home(ref bool bquit, double spd, ushort mode, AX_DIR dir, int timeout_ms = 0, bool bdoevent = true)
        {
            if (mt_type == MT_TYPE.VIRTUAL) return res = EM_RES.OK;
            if (bquit) return EM_RES.QUIT;

            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            //check safe sensor
            if (ChkSafeSen != null)
            {
                res = ChkSafeSen(id);
                if (res != EM_RES.OK) return res;
            }

            //check safe pos
            if (ChkSafePos != null)
            {
                res = ChkSafePos(id);
                if (res != EM_RES.OK) return res;
            }

            //svron
            if (!isSVRON)
            {
                SVRON = true;
                //wait for svr on
                Thread.Sleep(1000);
            }

            //dis soflt limit
            res = EnSlm(false);
            if (res != EM_RES.OK) return res;

            //check mt io
            if (status == AX_STA.EMG)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "急停信号按下");
                return res = EM_RES.ERR;
            }
            if (status == AX_STA.ALM)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, disc + str_status);
                return res = EM_RES.ERR;
            }

            //set spd	
            if (home_mode != 4)
            {
                res = SetSpeed(spd);
                if (res != EM_RES.OK) return res;
            }
            else
            {
                res = SetSpeed(spd, spd, 0, tacc, tdec);
                if (res != EM_RES.OK) return res;
            }

            int err = 0;
            //home start
            home_status = HOME_STA.HOMING;

#if LEADSHINE
            if (card.brand == CARD.BRAND.LEADSHINE)
            {
                //set mode
                err = LTDMC.dmc_set_homemode((ushort)card.card_id, (ushort)num, (ushort)dir, 1, mode, 0);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}设置回零模式出错,0x{1:X8}", disc, res));
                    return res = EM_RES.ERR;
                }

                err = LTDMC.dmc_home_move((ushort)card.card_id, (ushort)num);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}回零出错,0x{1:X8}", disc, res));
                    home_status = HOME_STA.ERROR;
                    return res = EM_RES.ERR;
                }
            }
#endif
#if GOOGOLTECH
            if (card.brand == CARD.BRAND.GOOGOLTECH)
            {

                //set mode
                int m_obj_pos = 9999999;
                err += mc.GT_SetCaptureSense((short)card.card_id, (short)(num + 1), mc.CAPTURE_HOME, 0);
                if (dir == AX_DIR.N)
                {
                    m_obj_pos = -9999999;
                    err += mc.GT_SetCaptureSense((short)card.card_id, (short)(num + 1), mc.CAPTURE_HOME, 1);
                }
                short cap_home_ret = 0;
                int capture_pos;
                uint m_parm;
                ulong m_home_time = 0;
                double m_pref_pos;
                uint m_test = 1;
                //一次捕捉
                err += mc.GT_ClrSts((short)card.card_id, (short)(num + 1), 1);
                err += mc.GT_SetCaptureMode((short)card.card_id, (short)(num + 1), mc.CAPTURE_HOME);
                err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), cmd_pos + m_obj_pos);
                err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                err += mc.GT_SetVel((short)card.card_id, (short)(num + 1), home_spd * pul_per_mm / 1000);
                err += mc.GT_Update((short)card.card_id, (1 << num));
                m_home_time = 0;
                do
                {
                    err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                    Thread.Sleep(1);
                    err += mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                    m_home_time++;
                    if ((err != 0) || (m_home_time > 90000))
                    {
                        home_status = HOME_STA.ERROR;
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}未找到原点,0x{1:X8}", disc, res));
                        m_home_time = 0;
                        return res = EM_RES.ERR;
                    }
                    if (isStop)
                    {
                        break;
                    }
              
                }
                while (0 == cap_home_ret);
                if (isStop)
                {
                    if (m_obj_pos == 999999)
                    {
                        m_obj_pos = -999999;
                        err += mc.GT_SetCaptureSense((short)card.card_id, (short)(num + 1), mc.CAPTURE_HOME, 1);
                    }
                    else
                    {
                        m_obj_pos = 999999;
                        err += mc.GT_SetCaptureSense((short)card.card_id, (short)(num + 1), mc.CAPTURE_HOME, 0);
                    }
                    err += mc.GT_ClrSts((short)card.card_id, (short)(num + 1), 1);
                    err += mc.GT_SetCaptureMode((short)card.card_id, (short)(num + 1), mc.CAPTURE_HOME);
                    err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), cmd_pos + m_obj_pos);
                    err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                    err += mc.GT_SetVel((short)card.card_id, (short)(num + 1), home_spd * pul_per_mm / 1000);
                    err += mc.GT_Update((short)card.card_id, (1 << num));
                    m_home_time = 0;
                    do
                    {
                        err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                        Thread.Sleep(1);
                        err += mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out  m_pref_pos, 1, out m_test);
                        m_home_time++;
                        if (isStop || (err != 0) || (m_home_time > 90000))
                        {
                            home_status = HOME_STA.ERROR;
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}未找到原点,0x{1:X8}", disc, res));
                            m_home_time = 0;
                            return res = EM_RES.ERR;
                        }
                    }
                    while (0 == cap_home_ret);
                }

                if (mt_type == MT_TYPE.SEVER)
                {
                    err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), capture_pos + (int)(1 * pul_per_mm));
                }
                else
                {
                    err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), (int)m_pref_pos + (int)(1 * pul_per_mm));
                }
                err += mc.GT_Update((short)card.card_id, (1 << num));
                m_home_time = 0;
                do
                {
                    Thread.Sleep(30);
                    m_home_time++;
                    if (m_home_time > 2000)
                    { 
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}等待停止超时,0x{1:X8}", disc, res));
                        m_home_time = 0;
                        return res = EM_RES.ERR;
                    }
                }
                while(!isStop);
                
                
                //伺服index捕捉
                if (mt_type == MT_TYPE.SEVER)
                {
                    err += mc.GT_SetCaptureSense((short)card.card_id, (short)(num + 1), mc.CAPTURE_INDEX, 0);
                    m_obj_pos = 9999;
                    err += mc.GT_ClrSts((short)card.card_id, (short)(num + 1), 1);
                    err += mc.GT_SetCaptureMode((short)card.card_id, (short)(num + 1), mc.CAPTURE_INDEX);
                    err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), cmd_pos + (int)(m_obj_pos * pul_per_mm));
                    err += mc.GT_SetVel((short)card.card_id, (short)(num + 1), home_spd * pul_per_mm / 1000);
                    err += mc.GT_Update((short)card.card_id, (1 << num));
                    m_home_time = 0;
                    do
                    {
                        err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                        Thread.Sleep(1);
                        err += mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out m_pref_pos, 1, out m_test);
                        m_home_time++;
                        if ((err != 0) || (m_home_time > 90000))
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} 未找到原点,0x{1:X8}", disc, res));
                            home_status = HOME_STA.ERROR;
                            m_home_time = 0;
                            return res = EM_RES.ERR;
                        }
                        if (isStop)
                        {
                            break;
                        }
                    }
                    while (0 == cap_home_ret);
                    if (isStop)
                    {
                        err += mc.GT_SetCaptureSense((short)card.card_id, (short)(num + 1), mc.CAPTURE_INDEX, 0);
                        m_obj_pos = -9999;
                        err += mc.GT_ClrSts((short)card.card_id, (short)(num + 1), 1);
                        err += mc.GT_SetCaptureMode((short)card.card_id, (short)(num + 1), mc.CAPTURE_INDEX);
                        err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), cmd_pos + (int)(m_obj_pos * pul_per_mm));
                        err += mc.GT_SetVel((short)card.card_id, (short)(num + 1), home_spd * pul_per_mm / 1000);
                        err += mc.GT_Update((short)card.card_id, (1 << num));
                        m_home_time = 0;
                        do
                        {
                            err += mc.GT_GetCaptureStatus((short)card.card_id, (short)(num + 1), out cap_home_ret, out capture_pos, 1, out m_parm);
                            Thread.Sleep(1);
                            err += mc.GT_GetPrfPos((short)card.card_id, (short)(num + 1), out m_pref_pos, 1, out m_test);
                            m_home_time++;
                            if (isStop || (err != 0) || (m_home_time > 90000))
                            {
                                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}未找到原点,0x{1:X8}", disc, res));
                                home_status = HOME_STA.ERROR;
                                m_home_time = 0;
                                return res = EM_RES.ERR;
                            }
                        }
                        while (0 == cap_home_ret);

                    }
                    err += mc.GT_SetPos((short)card.card_id, (short)(num + 1), capture_pos + (int)(1 * pul_per_mm));
                    err += mc.GT_Update((short)card.card_id, (1 << num));
                    m_home_time = 0;
                    do
                    {
                        Thread.Sleep(30);
                        m_home_time++;
                        if (m_home_time > 2000)
                        {
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}等待停止超时,0x{1:X8}", disc, res));
                            m_home_time = 0;
                            return res = EM_RES.ERR;
                        }
                    }
                    while (!isStop);
                }
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}回零设置出错,0x{1:X8}", disc, res));
                    home_status = HOME_STA.ERROR;
                    return res = EM_RES.ERR;
                }
                if (cap_home_ret != 0)
                {
                    return res = EM_RES.OK;
                }


            }

#endif

#if ZMOTION

            if (card.brand == CARD.BRAND.ZMOTION)
            {
                err = zmcaux.ZAux_Direct_Singl_Datum(card.handle, num, mode);
                if (err != 0)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}回零出错,0x{1:X8}", disc, res));
                    home_status = HOME_STA.ERROR;
                    return res = EM_RES.ERR;
                }
                Thread.Sleep(100);
            }
#endif

            //wait home completed
            //auto time
            if (timeout_ms == -1)
            {
                timeout_ms = (int)(1000 * Math.Abs(slp - sln) / spd_cur + 3000);
                if (timeout_ms < 5000) timeout_ms = 5000;
                if (timeout_ms > 20000) timeout_ms = 20000;
            }
            if (timeout_ms != 0)
            {
                //res = WaitForStop(ref bquit,timeout_ms, bdoevent);
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} WaitHomeStop...", disc));
                res = WaitHomeStop(ref bquit, timeout_ms);
                if (res != EM_RES.OK)
                {
                    home_status = HOME_STA.ERROR;
                    return res;
                }

                //enable soflt limit
                res = EnSlm(true);
                if (res != EM_RES.OK)
                    home_status = HOME_STA.ERROR;
            }

            return res;
        }


        EM_RES home_task_ret;
        bool bend = false;
        public bool bhomequit = false;


        void task()
        {
            int err;
            double spd_temp = spd_start;
            spd_start = home_spd / 3;

            bend = false;
            home_status = HOME_STA.HOMING;
            try
            {
                home_task_ret = ChkParam();
                if (home_task_ret != EM_RES.OK) 
                    return;

#if LEADSHINE
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    //脱离限位
                    if (isELP)
                    {
                        //set spd
                        home_task_ret = SetSpeed(home_spd);
                        if (home_task_ret != EM_RES.OK) return;
                        //move
                        err = LTDMC.dmc_vmove((ushort)card.card_id, (ushort)num, (ushort)AX_DIR.N);
                        if (err != 0)
                        {
                            home_task_ret = EM_RES.ERR;
                            return;
                        }
                        //wait
                        for (int i = 0; i < 20; i++)
                        {
                            Thread.Sleep(100);
                            if (!isELP)
                            {
                                Thread.Sleep(10);
                                if (!isELP)
                                    break;
                            }
                        }
                        Stop();
                        WaitForStop(ref bhomequit, 10000);
                    }
                    //脱离限位
                    if (isELN)
                    {
                        //set spd
                        home_task_ret = SetSpeed(home_spd);
                        if (home_task_ret != EM_RES.OK) return;
                        //move
                        err = LTDMC.dmc_vmove((ushort)card.card_id, (ushort)num, (ushort)AX_DIR.P);
                        if (err != 0)
                        {
                            home_task_ret = EM_RES.ERR;
                            return;
                        }
                        //wait
                        for (int i = 0; i < 20; i++)
                        {
                            Thread.Sleep(100);
                            if (!isELN)
                            {
                                Thread.Sleep(10);
                                if (!isELN)
                                    break;
                            }
                        }
                        Stop();
                        WaitForStop(ref bhomequit, 10000);
                    }

                    //如果感应到原点先快速脱离
                    if (home_mode != 4 && isORG)
                    {
                        home_task_ret = Home(ref bhomequit, home_spd / 3, 1, home_dir, 3000);
                        Stop();
                        if (home_task_ret != EM_RES.OK) return;
                    }

                    //try home
                    spd_start = spd_temp;
                    home_task_ret = Home(ref bhomequit, home_spd, home_mode, home_dir, 20000);
                    Stop();
                    if (home_task_ret == EM_RES.OK)
                    {
                        home_task_ret = HomeOffset(ref bhomequit);
                        return;
                    }

                    spd_start = home_spd;
                    //限位反向
                    if (status == AX_STA.ELP || status == AX_STA.ELN)
                    {
                        home_task_ret = Home(ref bhomequit, home_spd, 0, home_dir == AX_DIR.N ? AX_DIR.P : AX_DIR.N, 20000);
                        Stop();
                        if (home_task_ret != EM_RES.OK) return;
                    }

                    //如果感应到原点先快速脱离
                    if (home_mode != 4 && isORG)
                    {
                        home_task_ret = Home(ref bhomequit, home_spd / 3, 1, home_dir, 3000);
                        Stop();
                        if (home_task_ret != EM_RES.OK) return;
                    }

                    //限位反向
                    if (status == AX_STA.ELP || status == AX_STA.ELN)
                    {
                        home_task_ret = Home(ref bhomequit, home_spd, 0, home_dir == AX_DIR.N ? AX_DIR.P : AX_DIR.N, 20000);
                        Stop();
                        if (home_task_ret != EM_RES.OK) return;
                    }
                }
#endif
#if GOOGOLTECH
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    
                    home_task_ret = SetSpeed(home_spd);
                    if (home_task_ret != EM_RES.OK) return;
                    //脱离限位

                    if (isELP)
                    {
                        err = mc.GT_SetPos((short)card.card_id, (short)(num + 1), (int)(cmd_pos - 990000));
                        err = mc.GT_Update((short)card.card_id, 1 << num);
                        if (err != 0)
                        {
                            home_task_ret = EM_RES.ERR;
                            return;
                        }
                        //wait
                        for (int i = 0; i < 20; i++)
                        {
                            Thread.Sleep(100);
                            if (!isELP)
                            {
                                Thread.Sleep(10);
                                if (!isELP)
                                    break;
                            }
                        }
                        Stop();
                        WaitForStop(ref bhomequit, 10000);
                        //try home
                        spd_start = spd_temp;
                        home_task_ret = Home(ref bhomequit, home_spd, home_mode, AX_DIR.N, 20000);
                        Stop();
                        if (home_task_ret == EM_RES.OK)
                        {
                            home_task_ret = HomeOffset(ref bhomequit);
                            Thread.Sleep(300);                           
                        }
                        return;
                    }
                    //脱离限位
                    if (isELN)
                    {
                        //set spd
                        home_task_ret = SetSpeed(home_spd);
                        if (home_task_ret != EM_RES.OK) return;
                        //move
                        err = mc.GT_SetPos((short)card.card_id, (short)(num+1), (int)(cmd_pos + 999999));
                        err = mc.GT_Update((short)card.card_id, 1 << num);
                        if (err != 0)
                        {
                            home_task_ret = EM_RES.ERR;
                            return;
                        }
                        //wait
                        for (int i = 0; i < 20; i++)
                        {
                            Thread.Sleep(100);
                            if (!isELN)
                            {
                                Thread.Sleep(10);
                                if (!isELN)
                                    break;
                            }
                        }
                        Stop();
                        WaitForStop(ref bhomequit, 10000);
                        //try home
                        spd_start = spd_temp;
                        home_task_ret = Home(ref bhomequit, home_spd, home_mode, AX_DIR.P, 20000);
                        Stop();
                        if (home_task_ret == EM_RES.OK)
                        {
                            home_task_ret = HomeOffset(ref bhomequit);
                            Thread.Sleep(300);                            
                        }
                        return;
                    }

                    ////如果感应到原点先快速脱离

                    if (home_mode != 4 && isORG)//  home_mode==4是无正负限位的模式
                    {
                        //home_task_ret = Home(ref bhomequit, home_spd / 3, 1, home_dir, 3000);
                        //Stop();
                        //if (home_task_ret != EM_RES.OK) return;
                        home_task_ret = SetSpeed(home_spd);
                        if (home_task_ret != EM_RES.OK) return;
                        //move
                        err = mc.GT_SetPos((short)card.card_id, (short)(num+1), (int)(cmd_pos + 999999));
                        err = mc.GT_Update((short)card.card_id, 1<<num);
                        if (err != 0)
                        {
                            home_task_ret = EM_RES.ERR;
                            return;
                        }
                        //wait
                        for (int i = 0; i < 2000; i++)
                        {
                            Thread.Sleep(100);
                            if (!isORG)
                            {
                                Thread.Sleep(300);
                                if (!isORG)
                                    break;
                            }
                            Thread.Sleep(100);
                            if (isStop)
                            {                               
                                home_task_ret = EM_RES.ERR;
                                return;
                            }
                        }
                        Stop();
                        WaitForStop(ref bhomequit, 10000);
                        //try home
                        spd_start = spd_temp;
                        
                        home_task_ret = Home(ref bhomequit, home_spd, home_mode, AX_DIR.N, 20000);
                        Stop();
                        if (home_task_ret == EM_RES.OK)
                        {
                            home_task_ret = HomeOffset(ref bhomequit);
                            Thread.Sleep(300);                           
                        }
                        return;
                        
                    }

                    //try home
                    spd_start = spd_temp;
                    home_task_ret = Home(ref bhomequit, home_spd, home_mode, home_dir, 20000);
                    Stop();
                    if (home_task_ret == EM_RES.OK)
                    {
                        home_task_ret = HomeOffset(ref bhomequit);
                        Thread.Sleep(300);
                       
                    }
                    return;

              
                }
#endif
                //正式回零
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} home...", disc));
                spd_start = spd_temp;
                home_task_ret = Home(ref bhomequit, home_spd, home_mode, home_dir, 20000);
                Stop();
                if (home_task_ret != EM_RES.OK) return;

                //再次回零
                //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} rehome...", disc));
                //home_task_ret = Home(ref bhomequit, home_spd, home_mode, home_dir, 5000);
                //Stop();
                //if (home_task_ret != EM_RES.OK) return;
                //offset
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} home offset...", disc));
                home_task_ret = HomeOffset(ref bhomequit);
            }
            finally
            {
                //恢复
                Thread.Sleep(300);
                SetToWorkSpd();
                spd_start = spd_temp;
                //result
                if (home_task_ret != EM_RES.OK) home_status = HOME_STA.ERROR;
                else home_status = HOME_STA.OK;
                bend = true;
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} home task end", disc));
            }
        }
        void task2()
        {
            home_task_ret = ChkParam();
            if (home_task_ret != EM_RES.OK) return;
            bend = false;
            try
            {
                //正式回零
                home_task_ret = Home(ref bhomequit, home_spd, home_mode, home_dir, 20000);
                Stop();
                if (home_task_ret != EM_RES.OK) return;
                home_task_ret = HomeOffset(ref bhomequit);
            }
            finally
            {
                //result
                if (home_task_ret != EM_RES.OK) home_status = HOME_STA.ERROR;
                else home_status = HOME_STA.OK;
                bend = true;
            }
        }
        Task _th_home = null;
        public EM_RES HomeTask(int timeout = int.MaxValue, double offset = double.MaxValue, bool bsimplemode = false)
        {
            home_task_ret = EM_RES.OK;
            if (offset != double.MaxValue) home_offset = offset;
            if (timeout != int.MaxValue) home_timeout = timeout;
            else home_timeout = -1;
            bend = false;
            if (bsimplemode)
                _th_home = new Task(task2);
            else
                _th_home = new Task(task);
            bhomequit = false;
            _th_home.Start();
            return 
                home_task_ret;
        }
        public bool HomeTaskisEnd
        {
            get
            {
                return bend;
            }
        }
        public EM_RES HomeTaskRet
        {
            get
            {
                return home_task_ret;
            }
        }
        #endregion
    }
}
