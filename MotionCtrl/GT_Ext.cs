using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace MotionCtrl //命名空间根据应用程序修改
{
    public partial class GT_Ext
    {
        //---------------------   板卡初始和配置函数  ----------------------
        [DllImport("ExtMdl.dll", EntryPoint = "GT_OpenExtMdl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short GT_OpenExtMdl();
        [DllImport("LTDMC.dll", EntryPoint = "GT_CloseExtMdl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short GT_CloseExtMdl();
        [DllImport("LTDMC.dll", EntryPoint = "GT_SwitchtoCardNoExtMdl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short GT_SwitchtoCardNoExtMdl();
        [DllImport("LTDMC.dll", EntryPoint = "dmc_board_init_onecard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short dmc_board_init_onecard(ushort CardNo);
        [DllImport("LTDMC.dll", EntryPoint = "dmc_board_close_onecard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short dmc_board_close_onecard(ushort CardNo);
        [DllImport("LTDMC.dll", EntryPoint = "dmc_board_reset_onecard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern short dmc_board_reset_onecard(ushort CardNo);

        [DllImport("LTDMC.dll")]
        public static extern short dmc_soft_reset(ushort CardNo);
        [DllImport("LTDMC.dll")]
        public static extern short dmc_cool_reset(ushort CardNo);
        [DllImport("LTDMC.dll", EntryPoint = "dmc_original_reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    }
}