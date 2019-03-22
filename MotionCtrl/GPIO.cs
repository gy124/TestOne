using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionCtrl;
using System.Threading;
using System.Windows.Forms;

namespace MotionCtrl
{
    public delegate EM_RES CHK_GPIO_SAFE(int id);
    /// <summary>
    /// GPIO类，统一IO的API
    /// 创建：李大源 @2018/08/02
    /// </summary>
    public class GPIO
    {
        #region 参数
        /// <summary>
        /// 对应板卡
        /// </summary>
        public CARD card;
        /// <summary>
        /// IO类型所在的卡类型
        /// </summary>
        public enum IO_TYPE { MT_CARD, CAN, VIRTUAL_INP, VIRTUAL_ELP, VIRTUAL_ELN, VIRTUAL_ORG, IO_CARD, NULL }
        /// <summary>
        /// IO方向,输入还是输出口
        /// </summary>
        public enum IO_DIR { IN, OUT }
        /// <summary>
        /// IO状态
        /// </summary>
        public enum IO_STA
        {
            IN_ON = 0,
            IN_OFF = 1,
            OUT_ON = 0,
            OUT_OFF = 1,
            ERR = 2,
            NULL = -1
        }
        /// <summary>
        /// IO编号
        /// </summary>
        public ushort num;
        /// <summary>
        /// IO ID全局id识别所有卡的id
        /// </summary>
        public int m_id;
        /// <summary>
        /// IO方向/类型输入输出
        /// </summary>
        public IO_DIR dir;
        /// <summary>
        /// IO类型
        /// </summary>
        public IO_TYPE type;
        /// <summary>
        /// IO描述
        /// </summary>
        public string str_disc;
        /// <summary>
        /// 操作结果
        /// </summary>
        public EM_RES res;
        /// <summary>
        /// IO默认值
        /// </summary>
        public IO_STA default_sta = IO_STA.NULL;
        /// <summary>
        /// IO防呆
        /// </summary>
        public CHK_GPIO_SAFE ChkSafe;

        bool bShowErrmsg = true;
        /// <summary>
        /// 引用监测io状态
        /// </summary>
         int  lGpOValue;
         /// <summary>
         /// 引用监测固高扩展卡io状态
         /// </summary>
         ushort Gpio_ext;
         /// <summary>
         /// 固高函数返回值
         /// </summary>
         short mret;
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化IO
        /// </summary>
        /// <param name="io_num">IO编号</param>
        /// <param name="card">对应板卡</param>
        /// <param name="dir">输入或输出</param>
        /// <param name="type">类型</param>
        /// <param name="disc">描述</param>
        /// <param name="default_io">默认值</param>
        public GPIO(ushort io_num, CARD card, IO_DIR dir, IO_TYPE type, string disc, IO_STA default_sta = IO_STA.NULL)
        {
            this.card = card;
            if (card != null)

                m_id = (int)(card.id << 8) + (int)(io_num & 0xFF);
            else m_id = -1;

            num = io_num;
            this.dir = dir;
            this.type = type;
            str_disc = disc;
            this.default_sta = default_sta;

            //check num
            if (num < 0 || ((int)type == (int)IO_TYPE.VIRTUAL_ELN && (int)type == (int)IO_TYPE.VIRTUAL_ORG) && num >= card.ax_num)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} io_num {1} 超范围[{2},{3})", disc, num,0, card.ax_num));
            }
            else
            {
                if (dir == IO_DIR.IN && (type == IO_TYPE.MT_CARD && num >= card.input_num) || (type == IO_TYPE.IO_CARD && num > card.input_num))
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} io_num {1} 超范围[{2},{3}]", disc, num, 0, card.input_num));
                if (dir == IO_DIR.OUT && (type == IO_TYPE.MT_CARD && num >= card.output_num) || (type == IO_TYPE.IO_CARD && num > card.output_num))
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} io_num {1} 超范围[{2},{3}]", disc, num, 0, card.output_num));
            }

            ChkSafe = null;

            if(card!=null)
                card.GPIOList.Add(this);
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
        public string disc
        {
            get
            {
                return string.Format("{0}({1}/{2})", str_disc, num, card.disc);
            }
        }
        #endregion
        #region 检查IO参数是否异常
        public EM_RES ChkParam()
        {
            if (type == IO_TYPE.NULL)
            {
                res = EM_RES.OK;
            }

            if(m_id < 0)
            {
                if (bShowErrmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} ID({1})异常!", disc, m_id));
                bShowErrmsg = false;
                res = EM_RES.PARA_ERR;
                return res;
            }

            if (card == null || !card.isReady)
            {
                if (bShowErrmsg) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 对应板卡{1}未初始化!", disc, card.disc));
                bShowErrmsg = false;
                res = EM_RES.PARA_ERR;
                return res;
            }

            bShowErrmsg = true;
            res = EM_RES.OK;
            return res;
        }
        #endregion
        #region IO是否为ON
        public bool isON
        {
            get
            {
                return Status == (dir == IO_DIR.OUT ? IO_STA.OUT_ON : IO_STA.IN_ON) ? true : false;
            }
        }
        public bool isOFF
        {
            get
            {
                return !isON;
            }
        }
        #endregion
        #region 读取/设置IO状态
        /// <summary>
        /// get读取/set设置IO状态
        /// </summary>
        public IO_STA Status
        {
            get
            {
                if (type == IO_TYPE.NULL) return default_sta;

                //check
                res = ChkParam();
                if (res != EM_RES.OK) return IO_STA.ERR;

                //read
                uint inport = 0;
#if LEADSHINE || LEADSHINE_IO
                if (card.brand == CARD.BRAND.LEADSHINE)
                {
                    switch (type)
                    {
#if LEADSHINE
                        case IO_TYPE.MT_CARD:
                            if (dir == IO_DIR.IN)
                                return (IO_STA)LTDMC.dmc_read_inbit((ushort)card.card_id, num);
                            else
                                return (IO_STA)LTDMC.dmc_read_outbit((ushort)card.card_id, num);

                        case IO_TYPE.CAN:
                            if (dir == IO_DIR.IN)
                                return (IO_STA)LTDMC.dmc_read_can_inbit((ushort)card.maincard_id, (ushort)card.card_id, num);
                            else
                                return (IO_STA)LTDMC.dmc_read_can_outbit((ushort)card.maincard_id, (ushort)card.card_id, num);

                        case IO_TYPE.VIRTUAL_ORG:
                            inport = LTDMC.dmc_read_inport((ushort)card.card_id, 1);
                            return ((((inport >> (0 + num)) & 0x01) > 0) ? IO_STA.IN_OFF : IO_STA.IN_ON);
                        case IO_TYPE.VIRTUAL_INP:
                            inport = LTDMC.dmc_read_inport((ushort)card.card_id, 1);
                            return ((((inport >> (24 + num)) & 0x01) > 0) ? IO_STA.IN_OFF : IO_STA.IN_ON);
                        case IO_TYPE.VIRTUAL_ELP:
                            inport = LTDMC.dmc_read_inport((ushort)card.card_id, 0);
                            return ((((inport >> (16 + num)) & 0x01) > 0) ? IO_STA.IN_OFF : IO_STA.IN_ON);
                        case IO_TYPE.VIRTUAL_ELN:
                            inport = LTDMC.dmc_read_inport((ushort)card.card_id, 0);
                            return ((((inport >> (24 + num)) & 0x01) > 0) ? IO_STA.IN_OFF : IO_STA.IN_ON);
#endif
#if LEADSHINE_IO
                    case IO_TYPE.IO_CARD:
                        if (dir == IO_DIR.IN)
                            return (IO_STA)IOC0640.ioc_read_inbit((ushort)card.card_id, num);
                        else
                            return (IO_STA)IOC0640.ioc_read_outbit((ushort)card.card_id, num);
#endif
                        default:
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 类型未定义:{1}！", disc, type));
                            res = EM_RES.PARA_ERR;
                            break;
                    }
                }
#endif

#if GOOGOLTECH 
                if (card.brand == CARD.BRAND.GOOGOLTECH)
                {
                    switch (card.type)
                    {

                        case CARD.TYPE.MOTION:
                            if (dir == IO_DIR.IN)
                            {
                                mret = mc.GT_GetDi((short)card.card_id, mc.MC_GPI, out lGpOValue);
                                if (mret == 0)
                                {
                                    if ((lGpOValue & (1<<num) ) != 0)
                                        return IO_STA.IN_OFF;
                                    else
                                        return IO_STA.IN_ON;
                                }
                                else
                                {
                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取io输入失败:{1}！", disc, type));
                                    res = EM_RES.PARA_ERR;
                                    break;

                                }
                            }
                            else
                            {
                                mret = mc.GT_GetDo((short)card.card_id, mc.MC_GPO, out lGpOValue);
                                if (mret == 0)
                                {
                                    if (((lGpOValue >> num) & 1) != 0)
                                        return IO_STA.OUT_OFF;
                                    else
                                        return IO_STA.OUT_ON;
                                }
                                else
                                {
                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取io输出失败:{1}！", disc, type));
                                    res = EM_RES.PARA_ERR;
                                    break;

                                }
                            }

                        case CARD.TYPE.CAN_IO:
                            if (dir == IO_DIR.IN)
                            {
                                mret = mc.GT_GetExtIoBit((short)card.maincard_id, (short)card.card_id, (short)num, out Gpio_ext);
                                if (mret == 0)
                                {
                                    if (Gpio_ext == 0)
                                        return IO_STA.IN_ON;
                                    else
                                        return IO_STA.IN_OFF;
                                }
                                else
                                {
                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取io输入失败:{1}！", disc, type));
                                    res = EM_RES.PARA_ERR;
                                    break;

                                }
                            }
                            else
                            {

                                mret = mc.GT_GetExtDoValue((short)card.maincard_id, (short)card.card_id,  out Gpio_ext);
                                if (mret == 0)
                                {
                                    if ((Gpio_ext & (1 << num)) != 0)                  
                                        return IO_STA.OUT_ON;
                                    else
                                        return IO_STA.OUT_OFF;
                                }
                                else
                                {
                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 获取io输出失败:{1}！", disc, type));
                                    res = EM_RES.PARA_ERR;
                                    break;

                                }
                            }



                        default:
                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 类型未定义:{1}！", type, disc));
                            res = EM_RES.PARA_ERR;
                            break;
                    }
                }
#endif
#if ZMOTION
                if (card.brand == CARD.BRAND.ZMOTION)
                {
                    //int ret = 0;
                    //if (dir == IO_DIR.IN)
                    //{
                    //    ret = zmcaux.ZAux_Direct_GetIn(card.handle, num, ref inport);
                    //    if (ret == 0) return inport == 1 ? IO_STA.IN_ON : IO_STA.IN_OFF;
                    //}
                    //else
                    //{
                    //    ret = zmcaux.ZAux_Direct_GetOp(card.handle, num, ref inport);
                    //    if (ret == 0) return inport == 1 ? IO_STA.IN_ON : IO_STA.IN_OFF;
                    //}

                    //if (ret != 0)
                    //{
                    //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0}/{1} 读取错误,Err:{2}", disc, card.brand.ToString(), ret));
                    //    res = EM_RES.ERR;
                    //    return IO_STA.ERR;
                    //}

                    res = card.UpdateIOBuff(false);
                    if (res != EM_RES.OK)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0} UpdateStatusBuff出错，Err:{1}", disc, res));
                        res = EM_RES.ERR;
                        return IO_STA.ERR;
                    }
                    return card.GetIOFrBuff(num, dir);
                }
#endif
#if ADVANTTECH
                if (card.brand == CARD.BRAND.ADVANTTECH)
                {

                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "研华库未添加!");
                    return IO_STA.ERR;
                }
#endif
                return IO_STA.ERR;
            }

            set
            {
                if (type == IO_TYPE.NULL || dir == IO_DIR.IN)
                {
                    res = EM_RES.OK;
                    return;
                }

                //check
                res = ChkParam();
                if (res != EM_RES.OK) return;

                if (dir == IO_DIR.OUT)
                {
                    //check
                    res = ChkParam();
                    if (res != EM_RES.OK) return;

                    //

                    if (ChkSafe != null)
                    {
                        res = ChkSafe(id);
                        if (res != EM_RES.OK) return;
                    }

                    //set
                    int ret = 0;
#if LEADSHINE || LEADSHINE_IO
                    if (card.brand == CARD.BRAND.LEADSHINE)
                    {

                        switch (type)
                        {
#if LEADSHINE
                            case IO_TYPE.MT_CARD:
                                ret = LTDMC.dmc_write_outbit((ushort)card.card_id, num, (ushort)(value == IO_STA.OUT_ON ? IO_STA.OUT_ON : IO_STA.OUT_OFF));
                                break;
                            case IO_TYPE.CAN:
                                ret = LTDMC.dmc_write_can_outbit((ushort)card.maincard_id, (ushort)card.card_id, num, (ushort)(value == IO_STA.OUT_ON ? IO_STA.OUT_ON : IO_STA.OUT_OFF));
                                break;
#endif
#if LEADSHINE_IO
                                case IO_TYPE.IO_CARD:
                                ret = (int)IOC0640.ioc_write_outbit((ushort)card.card_id, num, (ushort)(value == IO_STA.OUT_ON ? IO_STA.OUT_ON : IO_STA.OUT_OFF));
                                break;
#endif




                            default:
                                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 类型未定义:{1}！", disc, type));
                                res = EM_RES.PARA_ERR;
                                return;
                        }
                    }
#endif


#if GOOGOLTECH
                    if (card.brand == CARD.BRAND.GOOGOLTECH)
                    {

                        switch (card.type)
                        {

                            case CARD.TYPE.MOTION:                               
                                mret=mc.GT_SetDoBit((short)card.card_id,mc.MC_GPO, (short)(num+1),(short)(value == IO_STA.OUT_ON ? IO_STA.OUT_ON : IO_STA.OUT_OFF));
                                if (mret != 0)
                                {
                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 打开失败:{1}！", disc));
                                    res = EM_RES.PARA_ERR;
                                    return;
                                }
                                break;

                            case CARD.TYPE.CAN_IO: 
                                mret = mc.GT_SetExtIoBit((short)card.maincard_id, (short) card.card_id,  (short)num, (ushort)(value == IO_STA.OUT_ON ? IO_STA.OUT_ON : IO_STA.OUT_OFF));
                                if (mret != 0)
                                {
                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 打开失败！", disc));
                                    res = EM_RES.PARA_ERR;
                                    return;
                                }
                                break;

                            default:
                                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 类型未定义:{1}！", disc, type));
                                res = EM_RES.PARA_ERR;
                                return;
                        }
                    }
#endif


#if ZMOTION
                    if (card.brand == CARD.BRAND.ZMOTION)
                    {
                        ret = zmcaux.ZAux_Direct_SetOp(card.handle, num, (uint)(value == IO_STA.OUT_ON ? 1 : 0));
                    }
#endif
#if ADVANTTECH
                    if (card.brand == CARD.BRAND.ADVANTTECH)
                    {

                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "研华库未添加!");
                        return;
                    }
#endif

                    if (ret != 0)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0}/{1} 写入错误,Err:{2}", disc, card.brand.ToString(), ret));
                        res = EM_RES.ERR;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 打开IO
        /// </summary>
        /// <returns></returns>
        public EM_RES SetOn()
        {
            Status = IO_STA.OUT_ON;
            return res;
        }
        /// <summary>
        /// 关闭IO
        /// </summary>
        /// <returns></returns>
        public EM_RES SetOff()
        {
            Status = IO_STA.OUT_OFF;
            return res;
        }
        /// <summary>
        /// IO翻转
        /// </summary>
        /// <returns></returns>
        public EM_RES Invert()
        {
            if (Status == IO_STA.OUT_ON)
                Status = IO_STA.OUT_OFF;
            else Status = IO_STA.OUT_ON;
            return res;
        }
        #endregion
        #region 等待IO稳定
        /// <summary>
        /// 等待输入口稳定到指定状态
        /// </summary>
        /// <param name="pbquit">取消控制</param>
        /// <param name="sta_wait_for">指定状态</param>
        /// <param name="cnt">检查次数</param>
        /// <param name="delay">检查间隔</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns></returns>
        public bool WaitForStable(ref bool pbquit, IO_STA sta_wait_for = IO_STA.IN_ON, int cnt = 3, int delay = 10, bool bdoevnet = true)
        {
            int n = 0;
            for (int i = 0; i < cnt; i++)
            {
                if (sta_wait_for == Status) n++;
                Thread.Sleep(delay);
                if (bdoevnet) Application.DoEvents();
                if (pbquit) break;
            }
            if (n >= (cnt / 2 + 1)) return true;
            else return false;
        }
        #endregion
        #region 等待IO状态
        /// <summary>
        /// 等待到ON状态后立刻返回
        /// </summary>
        /// <param name="bquit">取消控制</param>
        /// <param name="timeout_ms">超时控制</param>        
        /// <param name="bshow_mgs">显示异常信息</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns>在指时间内检查到ON状态则返回EM_RES.OK</returns>
        public EM_RES WaitON(ref bool bquit, uint timeout_ms = 3000, bool bshow_mgs = true, bool bdoevnet = false)
        {
            res = WaitForSta(ref bquit, IO_STA.IN_ON, timeout_ms, bshow_mgs, bdoevnet);
            return res;
        }
        /// <summary>
        /// 等待到OFF状态后立刻返回
        /// </summary>
        /// <param name="bquit">取消控制</param>
        /// <param name="timeout_ms">超时控制</param>        
        /// <param name="bshow_mgs">显示异常信息</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns>在指时间内检查到OFF状态则返回EM_RES.OK</returns>
        public EM_RES WaitOFF(ref bool bquit, uint timeout_ms = 3000, bool bshow_mgs = true, bool bdoevnet = false)
        {
            res = WaitForSta(ref bquit, IO_STA.IN_OFF, timeout_ms, bshow_mgs, bdoevnet);
            return res;
        }
        /// <summary>
        /// 等待到指定状态后立刻返回
        /// </summary>
        /// <param name="bquit">取消控制</param>
        /// <param name="sta_wait_for">指定状态</param>
        /// <param name="timeout_ms">超时控制</param>        
        /// <param name="bshow_mgs">显示异常信息</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns>在指时间内检查到指定状态则返回EM_RES.OK</returns>
        public EM_RES WaitForSta(ref bool bquit, IO_STA sta_wait_for, uint timeout_ms, bool bshow_mgs = true, bool bdoevnet = false)
        {
            //check
            res = ChkParam();
            if (res != EM_RES.OK) return res;

            IO_STA pbitstate = 0;
            int timeout = (int)timeout_ms;
            do
            {
                //cancel
                if (bquit == true) return res = EM_RES.QUIT;

                //check and return
              //  pbitstate = Status;
                if (Status == sta_wait_for) return res = EM_RES.OK;
                else if (Status == IO_STA.ERR)
                {
                    res = EM_RES.ERR;
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 等待状态返回异常！", disc));
                    return res;
                }

                //delay 10ms
                if (timeout > 0)
                {
                    timeout -= 10;
                    if (bdoevnet) Application.DoEvents();
                    Thread.Sleep(10);
                }
            } while (timeout > 0);

            if (bshow_mgs) VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0} 等待 {1} 超时({2}ms)！", disc, sta_wait_for == IO_STA.IN_ON ? "ON" : "OFF", timeout_ms));
            return EM_RES.TIMEOUT;
        }
        #endregion
        #region 确定IO状态
        /// <summary>
        /// 确定是否为ON状态
        /// </summary>
        /// <param name="cnt">检查次数</param>
        /// <param name="invt">检查间隔</param>
        /// <returns>在检查次数内，超过一半的次数检查到ON状态则返回True</returns>
        public bool AssertON(int cnt = 3, int invt = 10)
        {
            int n = 0;
            for (int i = 0; i < cnt; i++)
            {
                if (isON)
                {
                    Thread.Sleep(invt);
                    n++;
                    if (n >= cnt / 2) return true;
                }
            }
            return false;

        }
        /// <summary>
        /// 确定是否为OFF状态
        /// </summary>
        /// <param name="cnt">检查次数</param>
        /// <param name="invt">检查间隔</param>
        /// <returns>在检查次数内，超过一半的次数检查到OFF状态则返回True</returns>
        public bool AssertOFF(int cnt = 3, int invt = 10)
        {
            int n = 0;
            for (int i = 0; i < cnt; i++)
            {
                if (isOFF)
                {
                    Thread.Sleep(invt);
                    n++;
                    if (n >= cnt / 2) return true;
                }
            }
            return false;
        }
        #endregion
    }
    /// <summary>
    /// 气缸类，包含一个输出口，两个对应位置的传感器。
    /// </summary>
    public class Cylinder
    {
        #region 参数
        /// <summary>
        /// 气缸电磁阀
        /// </summary>
        public GPIO io_out = null;
        /// <summary>
        /// 气缸电磁阀2
        /// </summary>
        public GPIO io_out_back = null;
        /// <summary>
        /// 气缸关闭对应传感器
        /// </summary>
        public GPIO io_sen_off = null;
        /// <summary>
        /// 气缸打开对应传感器
        /// </summary>
        public GPIO io_sen_on = null;

        /// <summary>
        /// 气缸打开时对应感应器状态
        /// </summary>
        public GPIO.IO_STA io_sen_on_active = GPIO.IO_STA.IN_ON;
        /// <summary>
        /// 气缸关闭时对应传感器状态
        /// </summary>
        public GPIO.IO_STA io_sen_off_active = GPIO.IO_STA.IN_ON;

        /// <summary>
        /// IO防呆安全，打开或关闭前调用，可以委托方法检测安全
        /// </summary>
        public CHK_GPIO_SAFE ChkSafe;
        #endregion
        #region 初始化
        public Cylinder(GPIO io_out, GPIO io_sen_on, GPIO io_sen_off = null, GPIO.IO_STA io_sen_on_active = GPIO.IO_STA.IN_ON, GPIO.IO_STA io_sen_off_active = GPIO.IO_STA.IN_ON)
        {
            if (io_out != null && io_out.dir != GPIO.IO_DIR.OUT)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_out.disc));
                io_out = null;
            }
            if (io_sen_on != null && io_sen_on.dir != GPIO.IO_DIR.IN)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_sen_on.disc));
                io_sen_on = null;
            }
            if (io_sen_off != null && io_sen_off.dir != GPIO.IO_DIR.IN)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_sen_off.disc));
                io_sen_off = null;
            }
            this.io_out = io_out;
            this.io_sen_on = io_sen_on;
            this.io_sen_off = io_sen_off;
            this.io_sen_on_active = io_sen_on_active;
            this.io_sen_off_active = io_sen_off_active;
        }
        public Cylinder(ushort out_num,GPIO io_out, GPIO io_sen_on, GPIO io_sen_off = null, GPIO io_out_back = null, GPIO.IO_STA io_sen_on_active = GPIO.IO_STA.IN_ON, GPIO.IO_STA io_sen_off_active = GPIO.IO_STA.IN_ON)
        {
            if (io_out != null && io_out.dir != GPIO.IO_DIR.OUT)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_out.disc));
                io_out = null;
            }
            if (io_out_back != null && io_out_back.dir != GPIO.IO_DIR.OUT)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_out_back.disc));
                io_out = null;
            }
            if (io_sen_on != null && io_sen_on.dir != GPIO.IO_DIR.IN)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_sen_on.disc));
                io_sen_on = null;
            }
            if (io_sen_off != null && io_sen_off.dir != GPIO.IO_DIR.IN)
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0},GPIO方向异常!", io_sen_off.disc));
                io_sen_off = null;
            }
            this.io_out = io_out;

            this.io_out_back = io_out_back;
            if (io_out_back != null)
            {
                this.io_sen_on = io_sen_on;
            }
            this.io_sen_off = io_sen_off;
            this.io_sen_on_active = io_sen_on_active;
            this.io_sen_off_active = io_sen_off_active;
        }
        #endregion
        #region 开关气缸，可选延迟
        /// <summary>
        /// 打开气缸，可选延迟
        /// </summary>
        /// <param name="delay">等待时间,等于0时不等待直接返回OK</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns></returns>
        public EM_RES SetOn( int delay = 0, bool bdoevnet = false)
        {
            //防呆
            if (ChkSafe != null)
            {
                EM_RES res = ChkSafe((int)GPIO.IO_STA.OUT_ON);
                if (res != EM_RES.OK) return res;
            }

            //set on
            io_out.SetOn();
            if (io_out.res != EM_RES.OK) return io_out.res;
            if (io_out_back != null && io_out_back.dir == GPIO.IO_DIR.OUT)
            {
                io_out_back.SetOff();
                if (io_out_back.res != EM_RES.OK) return io_out_back.res;
            }

            //delay
            if (delay > 0)
            {
                while (delay > 0)
                {
                    Thread.Sleep(10);
                    if(io_sen_on!=null)//-gy-1201-
                    {
                        if (io_sen_on.AssertON()) break;
                    }
                    else
                        if (io_sen_off != null)
                        {
                            if (io_sen_off.AssertOFF()) break;
                        }
                    Application.DoEvents();
                    delay -= 10;
                }
            }
            return EM_RES.OK;
        }
        /// <summary>
        /// 关闭气缸，可选延迟
        /// </summary>
        /// <param name="delay">等待时间,等于0时不等待直接返回OK</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns></returns>
        public EM_RES SetOff( int delay = 0, bool bdoevnet = false)
        {
            //防呆
            if (ChkSafe != null)
            {
                EM_RES res = ChkSafe((int)GPIO.IO_STA.OUT_OFF);
                if (res != EM_RES.OK) return res;
            }

            //set off
            io_out.SetOff();
            if (io_out.res != EM_RES.OK) return io_out.res;
            if (io_out_back != null && io_out_back.dir == GPIO.IO_DIR.OUT)
            {
                io_out_back.SetOn();
                if (io_out_back.res != EM_RES.OK) return io_out_back.res;
            }
            //delay
            if (delay > 0)
            {
                while (delay > 0)
                {
                    Thread.Sleep(10);
                     Application.DoEvents();
                    
                         if (io_sen_off != null)
                         {
                             if (io_sen_off.AssertON()) break;
                         }
                         else
                         if (io_sen_on != null)
                         {
                             if (io_sen_on.AssertOFF()) break;
                         }
                         
                    delay -= 10;
                }
            }
            return EM_RES.OK;
        }
        /// <summary>
        /// 取反气缸，可选延迟
        /// </summary>
        /// <param name="delay">等待时间,等于0时不等待直接返回OK</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns></returns>
        public EM_RES Invert( int delay = 0, bool bdoevnet = false)
        {
            //防呆
            if (ChkSafe != null)
            {
                EM_RES res = ChkSafe((int)(io_out.isON?GPIO.IO_STA.OUT_OFF: GPIO.IO_STA.OUT_ON));
                if (res != EM_RES.OK) return res;
            }
            
            //invert
            io_out.Invert();
            if (io_out.res != EM_RES.OK) return io_out.res;
            if (io_out_back != null && io_out_back.dir == GPIO.IO_DIR.OUT)
            {
                
                io_out_back.Invert();
                if (io_out_back.res != EM_RES.OK) return io_out_back.res;
            }
            //delay
            if (delay > 0)
            {
                while (delay > 0)
                {
                    Thread.Sleep(delay);
                    if (bdoevnet) Application.DoEvents();
                    delay -= 10;
                }
            }
            return EM_RES.OK;
        }
        #endregion
        #region 开关气缸，可选等待对应的传感器感应
        /// <summary>
        /// 打开气缸，可选等待传感器到位
        /// </summary>
        /// <param name="bquit">取消控制</param>
        /// <param name="waitms">等待时间,等于0时不等待直接返回OK</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns>在指定时间内感应到对应的传感器则返回OK</returns>
        public EM_RES SetOn(ref bool bquit, int waitms = 0, bool bdoevnet = false)
        {
            //防呆
            if (ChkSafe != null)
            {
                EM_RES res = ChkSafe((int)GPIO.IO_STA.OUT_ON);
                if (res != EM_RES.OK) return res;
            }

            //set on
            io_out.SetOn();
            if (io_out.res != EM_RES.OK) return io_out.res;
            if (io_out_back != null && io_out_back.dir == GPIO.IO_DIR.OUT)
            {
                io_out_back.SetOff();
                if (io_out_back.res != EM_RES.OK) return io_out_back.res;
            }
            //wait for sensor
            EM_RES ret = EM_RES.OK;
            if (waitms > 0)
            {
                if (io_sen_on != null)
                {
                    //ret = io_sen_on.WaitForSta(ref bquit, io_sen_on_active, (uint)waitms, true, bdoevnet);
                    ret = io_sen_on.WaitON(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                }
                if (io_sen_off != null)
                {
                    //ret = io_sen_off.WaitForSta(ref bquit, io_sen_off_active == GPIO.IO_STA.IN_ON ? GPIO.IO_STA.IN_OFF : GPIO.IO_STA.IN_ON, (uint)waitms, true, bdoevnet);
                    ret = io_sen_off.WaitOFF(ref bquit);
                    if (ret != EM_RES.OK) return ret;
                }
                if (io_sen_on == null && io_sen_off == null)
                {
                    waitms = 5;
                    while (waitms > 0)
                    {
                        Thread.Sleep(100);
                        if (bdoevnet) Application.DoEvents();
                        waitms--;
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 关闭气缸，可选等待传感器到位
        /// </summary>
        /// <param name="bquit">取消控制</param>
        /// <param name="waitms">等待时间,等于0时不等待直接返回OK</param>
        /// <param name="bdoevnet">为True时，循环中处理其他系统信息，例如界面刷新等</param>
        /// <returns>在指定时间内感应到对应的传感器则返回OK</returns>
        public EM_RES SetOff(ref bool bquit, int waitms = 0, bool bdoevnet = false)
        {
            //防呆
            if (ChkSafe != null)
            {
                EM_RES res = ChkSafe((int)GPIO.IO_STA.OUT_OFF);
                if (res != EM_RES.OK) return res;
            }

            //set on
            io_out.SetOff();
            if (io_out.res != EM_RES.OK) return io_out.res;
            if (io_out_back != null && io_out_back.dir == GPIO.IO_DIR.OUT)
            {
                io_out_back.SetOn();
                if (io_out_back.res != EM_RES.OK) return io_out_back.res;
            }
            //wait for sensor
            EM_RES ret = EM_RES.OK;
            if (waitms > 0)
            {
                if (io_sen_on != null)
                {
                    ret = io_sen_on.WaitForSta(ref bquit, io_sen_on_active == GPIO.IO_STA.IN_ON ? GPIO.IO_STA.IN_OFF : GPIO.IO_STA.IN_ON, (uint)waitms, true, bdoevnet);
                    if (ret != EM_RES.OK) return ret;
                }
                if (io_sen_off != null)
                {
                    ret = io_sen_off.WaitForSta(ref bquit, io_sen_off_active, (uint)waitms, true, bdoevnet);
                    if (ret != EM_RES.OK) return ret;
                }

                if (io_sen_on == null && io_sen_off == null)
                {
                    waitms = 5;
                    while (waitms > 0)
                    {
                        Thread.Sleep(100);
                        if (bdoevnet) Application.DoEvents();
                        waitms--;
                    }
                }
            }
            return ret;
        }
        #endregion
        #region 检查气缸传感器
        /// <summary>
        /// 是否气缸打开位的传感器感应,无对应传感器则返回False
        /// </summary>
        public bool isSenONActive
        {
            get
            {
                if (io_sen_on != null)
                {
                    return (io_sen_on.Status == io_sen_on_active);
                }
                return false;
            }
        }
        /// <summary>
        /// 是否气缸关闭位的传感器感应,无对应传感器则返回False
        /// </summary>
        public bool isSenOFFActive
        {
            get
            {
                if (io_sen_off != null)
                {
                    return (io_sen_off.Status == io_sen_off_active);
                }
                return false;
            }
        }
        #endregion
        #region 通过OUT确认气缸状态
        /// <summary>
        /// 气缸是否打开
        /// </summary>
        public bool isON
        {
            get
            {
                if (io_out != null)
                {
                    return io_out.isON;
                }
                return false;
            }
        }
        /// <summary>
        /// 气缸是否关闭
        /// </summary>
        public bool isOFF
        {
            get
            {
                if (io_out != null)
                {
                    return io_out.isOFF;
                }
                return false;
            }
        }
        #endregion
        #region 通过传感器确认气缸位置
        /// <summary>
        /// 通过传感器确认气缸是否打开
        /// </summary>
        public bool isONByChkSen
        {
            get
            {
                if ((io_sen_on != null && isSenONActive) && (io_sen_off != null && !isSenOFFActive))
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 通过传感器确认气缸是否关闭
        /// </summary>
        public bool isOFFByChkSen
        {
            get
            {
                if ((io_sen_on != null && !isSenONActive) && (io_sen_off != null && isSenOFFActive))
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}

