using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace MotionCtrl
{
    public class VAR
    {
        #region 视觉数据      
        /// <summary>
        ///系统设置
        /// </summary>
        public static SYS_SET gsys_set = new SYS_SET();
        public static readonly object lockthis = new object();

        /// <summary>
        /// 打印信息
        /// </summary>
        public static Msg msg = new Msg();
        public static void ErrMsg(string inf = "未知错误") 
        {
            msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format(inf));
        }
        public static void SysMsg(string inf = "系统信息")
        {

            msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format(inf));
        }
        public static void WarnMsg(string inf = "警告信息")
        {

            msg.AddMsg(Msg.EM_MSGTYPE.WAR, string.Format(inf));
        }
        /// <summary>
        /// 系统提示
        /// </summary>
        public static SYS_INF sys_inf = new SYS_INF();
        #endregion 
    }
}
