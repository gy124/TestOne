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
    public class POS
    {

        public double pos_x;
        public double pos_y ;
        public double pos_z ;
        public double pos_a ;
        public double pos_al;
        public ST_XYZA pos_all ;          
        public string disc = "";
        public ushort index ;
        public ushort ws_id;//工站序号
        public ushort axis_num ;//轴数量
        public int axis_state ;   //二进制轴状态
        public AXIS  AxisX=null;
        public AXIS AxisY = null;
        public AXIS AxisZ = null;
        public AXIS AxisA = null;
        public ST_XYZA pos_xyza
        {
            get
            {
                ST_XYZA mpos=new ST_XYZA();                 
                LoadCfgPosInf("");
                if (AxisX != null)
                    mpos.x = pos_x;
                if (AxisY != null)
                    mpos.y = pos_y;
                if (AxisZ != null)
                    mpos.z = pos_z;
                if (AxisA != null)
                    mpos.a = pos_a;
                return mpos;
            }
        }


        private   List<AXIS> list_ax = new List<AXIS>();


        public IntPtr handle;

        #region 初始化
        public void POS_init(List<AXIS> list_axis, string disc = "说明", ushort index = 0, ushort ws_id = 0)
        {

            handle = (IntPtr)0;//      
            this.disc = disc;
            this.index = index;
            this.ws_id = ws_id;
          
            axis_num = 0;
            this.ws_id = ws_id;
            if (list_axis == null)
            {
                return;
            }
            if (list_axis[0] != null)
            {
                AxisX = list_axis[0];
                axis_state = (axis_state | (1 << 3));
                axis_num++;
            }
            if (list_axis[1] != null)
            {
                AxisY = list_axis[1];
                axis_state = (axis_state | (1 << 2));
                axis_num++;
            }
            if (list_axis[2] != null)
            {
                AxisY = list_axis[2];
                axis_state = (axis_state | (1 << 1));
                axis_num++;
            }

            if (list_axis[3] != null)
            {
                AxisA = list_axis[3];
                axis_state = (axis_state | 1);
                axis_num++;
            }
         
          
            ReadUpdatePos();

        }

        public void POS_init(AXIS axis_x, AXIS axis_y, AXIS axis_z, AXIS axis_a, string disc = "说明", ushort index = 0, ushort ws_id = 0)
        {

            this.disc = disc;
            this.index = index;         
            this.ws_id = ws_id;
            axis_num = 0;
            if (axis_x!=null)
            {
                AxisX = axis_x;
                axis_state = axis_state | (1 << 3);
                axis_num++;
            }
            if (axis_y != null)
            {
                AxisY = axis_y;               
                axis_state = axis_state | (1 << 2);
                axis_num++;
            }
            if (axis_z != null)
            {
                AxisZ = axis_z;          
                axis_state = axis_state | (1 << 1);
                axis_num++;
            }
            if (axis_a != null)
            {
                AxisA = axis_a;
          
                axis_state = axis_state | 1;
                axis_num++;
            }

            ReadUpdatePos();

        }
       public bool  ReadUpdatePos()
        {
            try
            {
                EM_RES ret = LoadCfgPosInf("");
                if (ret != EM_RES.OK)
                    return false;
                return true;
            }
           catch
            {
                VAR.ErrMsg("加载异常错误");
                return false;
           }

        }
       public void WriteUpdatePos(ST_XYZA mpos )
       {
           
           if (AxisX != null)
               pos_x = mpos.x;
           if (AxisY != null)
               pos_y = mpos.y;
           if (AxisZ != null)
               pos_z = mpos.z;
           if (AxisA != null)
               pos_a = mpos.a;
          SaveCfgPosInf("");

       }
       public void UpdatePos()
       {
           LoadCfgPosInf("");
         

       }
        public POS(List<AXIS> list_axis, string disc = "说明", ushort index = 0, ushort ws_id = 0)
        {

            POS_init(list_axis, disc, index, ws_id);
            //handle = (IntPtr)0;//      
            //this.disc = disc;
            //this.index list_ax= index;
            //this.ws_id = ws_id;
            //axis_num = 0;
            //this.ws_id = ws_id;
            //if (list_axis == null)
            //{
            //    return;
            //}
            //if (list_axis[0].isInit)
            //{
            //    AxisX = list_axis[0];
            //    axis_state = (axis_state | (1 << 3));
            //    axis_num++;
            //}
            //if (list_axis[1] != null)
            //{
            //    AxisY = list_axis[1];
            //    axis_state = (axis_state | (1 << 2));
            //    axis_num++;
            //}
            //if (list_axis[2] != null)
            //{
            //    AxisY = list_axis[2];
            //    axis_state = (axis_state | (1 << 1));
            //    axis_num++;
            //}

            //if (list_axis[3] != null)
            //{
            //    AxisA = list_axis[3];
            //    axis_state = (axis_state | 1);
            //    axis_num++;
            //}

        }

        public POS(AXIS axis_x, AXIS axis_y, AXIS axis_z, AXIS axis_a, string disc = "说明", ushort index = 0, ushort ws_id = 0, ST_XYZA pos = new ST_XYZA())
        {
            this.disc = disc;
            this.index = index;
            this.ws_id = ws_id;
            pos_all = pos;
            axis_num = 0;
            if (axis_x != null)
            {
                AxisX = axis_x;
                axis_state = axis_state | (1 << 3);
                axis_num++;
            }
            if (axis_y != null)
            {
                AxisY = axis_y;
                axis_state = axis_state | (1 << 2);
                axis_num++;
            }
            if (axis_z != null)
            {
                AxisZ = axis_z;
                axis_state = axis_state | (1 << 1);
                axis_num++;
            }
            if (axis_a != null)
            {
                AxisA = axis_a;
                axis_state = axis_state | 1;
                axis_num++;
            }
        }
        public POS(ref AXIS axis_x, ref AXIS axis_y, ref AXIS axis_z, ref AXIS axis_a, string disc = "说明", ushort index = 0, ushort ws_id = 0, ST_XYZA pos = new ST_XYZA())
        {
            this.disc = disc;
            this.index = index;
            this.ws_id = ws_id;
            pos_all = pos;
            axis_num = 0;
            if (axis_x != null)
            {
                AxisX = axis_x;
                axis_state = axis_state | (1 << 3);
                axis_num++;
            }
            if (axis_y != null)
            {
                AxisY = axis_y;
                axis_state = axis_state | (1 << 2);
                axis_num++;
            }
            if (axis_z != null)
            {
                AxisZ = axis_z;
                axis_state = axis_state | (1 << 1);
                axis_num++;
            }
            if (axis_a != null)
            {
                AxisA = axis_a;
                axis_state = axis_state | 1;
                axis_num++;
            }
        }
       
        public POS(AXIS axis_x, AXIS axis_y, AXIS axis_z, string disc = "说明", ushort index = 0, ushort ws_id = 0)
        {

            POS_init(axis_x, axis_y, axis_z, null, disc, index, ws_id);
          
        }
        public POS(AXIS axis_x, AXIS axis_y, string disc = "说明", ushort index = 0, ushort ws_id = 0, ushort axis_num = 0, double pos_x = 0, double pos_y = 0, double pos_z = 0, double pos_a = 0)
        {

            POS_init(axis_x, axis_y, null, null, disc, index, ws_id);


        }
        public POS(AXIS axis_x, string disc = "说明", ushort index = 0, ushort ws_id = 0)
        {

            POS_init(axis_x, null, null, null, disc, index, ws_id);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public bool isInit
        {
            get
            {
                //check ws_id 
                if ((AxisX == null)&&(AxisY == null)&&(AxisZ == null)&&(AxisA == null))
                    return false;
                if (ws_id == 0) return false;

                return true;
            }
        }
        #endregion
        #region 加载与保存参数
        public EM_RES LoadCfgPosInf(string filename = "")
        {
            try
            {


                if (filename.Length < 3)
                {
                    if (VAR.gsys_set.cur_product_name != null && VAR.gsys_set.cur_product_name .Length>1)

                        filename = Path.GetFullPath("..") + "\\product\\" + VAR.gsys_set.cur_product_name + "\\poscfg.ini";
                    else
                        filename = Path.GetFullPath("..") + "\\syscfg\\poscfg.ini";
                }
                IniFile inf = new IniFile(filename);

                string Section = string.Format("POS_{0}/{1}", index, ws_id);
                if (AxisX != null)
                {
                    pos_x = inf.ReadDouble(Section, "POS_X", 0);
                    pos_all.x = pos_x;
                }
                if (AxisY != null)
                {
                    pos_y = inf.ReadDouble(Section, "POS_Y", 0);
                    pos_all.y = pos_y;
                }
                if (AxisZ != null)
                {
                    pos_z = inf.ReadDouble(Section, "POS_Z", 0);
                    pos_all.z = pos_z;
                }
                if (AxisA != null)
                {
                    pos_a = inf.ReadDouble(Section, "POS_A", 0);
                    pos_all.a = pos_a;
                }
                return EM_RES.OK;
            }
            catch 
            {
                VAR.ErrMsg("加载位置参数错误");
                return EM_RES.ERR;
            
            }
        }
        public EM_RES SaveCfgPosInf(string filename = "")
        {
            if (filename.Length < 3)
            {
                if (VAR.gsys_set.cur_product_name != null && VAR.gsys_set.cur_product_name.Length > 1)

                    filename = Path.GetFullPath("..") + "\\product\\" + VAR.gsys_set.cur_product_name + "\\poscfg.ini";
                else
                    filename = Path.GetFullPath("..") + "\\syscfg\\poscfg.ini";
            }
               
            IniFile inf = new IniFile(filename);

            string Section = string.Format("POS_{0}/{1}", index, ws_id);

            if (AxisX != null)
                inf.WriteDouble(Section, "POS_X", pos_x);
            if (AxisY != null)
                inf.WriteDouble(Section, "POS_Y", pos_y);
            if (AxisZ != null)
                inf.WriteDouble(Section, "POS_Z", pos_z);
            if (AxisA != null)
                inf.WriteDouble(Section, "POS_A", pos_a);
            pos_all.x = pos_x;
            pos_all.y = pos_y;
            pos_all.z = pos_z;
            pos_all.a = pos_a;
            return EM_RES.OK;
        }
        #endregion
        public EM_RES MoveTo(ref bool bquit,bool bwait=true)
        {
            ReadUpdatePos();
            EM_RES ret = EM_RES.OK;
            string name = disc;
            int wait_ms = 99000;//
            if (!bwait) wait_ms = 0;
            if (bquit )
                return EM_RES.QUIT;
            if (!isInit)
                return EM_RES.ERR;          
            if (AxisZ != null)
            {
                ret = AxisZ.MoveTo(ref bquit, pos_z, wait_ms, false, true);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (AxisX != null)
            {
                ret = AxisX.MoveTo(ref bquit, pos_x, 0, false, false);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (AxisY != null)
            {
                ret = AxisY.MoveTo(ref bquit, pos_y, 0, false, true);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (AxisA != null)
            {
                ret = AxisA.MoveTo(ref bquit, pos_a, 0, false, false);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (!bwait) return ret;
            if (AxisX != null)
            {
                ret = AxisX.WaitForMoveDone(ref bquit, pos_x, wait_ms, false);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (AxisY != null)
            {
                ret = AxisY.WaitForMoveDone(ref bquit, pos_y, wait_ms, false);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
            if (AxisA != null)
            {
                ret = AxisA.WaitForMoveDone(ref bquit, pos_a, wait_ms, false);
                if (ret != EM_RES.OK) goto MOVE_END;
            }
        MOVE_END:
            if (AxisX != null) AxisX.Stop();
            if (AxisY != null) AxisY.Stop();
            if (AxisZ != null) AxisZ.Stop();
            if (AxisA != null) AxisA.Stop();
            return ret;
        }
        public EM_RES GetPos(ref bool bquit)
        {
            if (bquit == true)
                return EM_RES.QUIT;
            if (!isInit)
                return EM_RES.ERR;
            if (AxisX != null) pos_x = AxisX.fcmd_pos;
            if (AxisY != null) pos_y = AxisY.fcmd_pos;
            if (AxisZ != null) pos_z = AxisZ.fcmd_pos;
            if (AxisA != null) pos_a = AxisA.fcmd_pos;

            return EM_RES.OK;
        }



    }
}
