using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MotionCtrl
{
    public class CONST
    {
        #region 吸嘴定义
        public const ushort FLY_COUNT = 3;
        public static double[] fly_trg_spd = new double[CONST.FLY_COUNT] { 300, 600, 1000 };
        public const ushort OFS_NUM_PER_XT = 2;
        #endregion
        #region 拍照流程定义
        //下相机流程
        public  static string[] XtShpDwFw = new string[2] { "Xt1_Shp", "Xt2_Shp" };//空吸头流程
        public  static string[] ModShpDwFw = new string[2] { "ModDw1_Shp", "ModDw2_Shp" };//下相机流程
        //上相机流程
        public static string  ModShpUpFw = "ModUp_Shp";//模组拍照流程
        public static string  TrayShpUpFw = "Tray_Shp";//工站拍照流程
        #endregion
        #region 相机定义       
        public static int UpCam1 = 0;
        public static int UpCam2 = 1;
        public static int DwCam  = 2;
        #endregion

    }
}