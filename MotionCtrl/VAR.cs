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
        //系统设置/状态
        public static SYS_SET gsys_set = new SYS_SET();
        public static readonly object lockthis = new object();

        //信息
        public static Msg msg = new Msg();        

        //系统提示
        public static SYS_INF sys_inf = new SYS_INF();
        //清料标志位
        public static bool ClearMt = false;
        //点检PC
        public static int  ChkPC = 1;
    }
}
