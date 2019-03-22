using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace MotionCtrl
{
    public partial class AxisConfig : UserControl
    {
        List<AXIS> list_ax=new List<AXIS> ();

        public AxisConfig()
        {
            InitializeComponent();
            list_ax.Clear();            
        }

        private  void FillTableWithAxisInf(AXIS ax, int row=-2)
        {
            if (ax == null || ax.mt_type == AXIS.MT_TYPE.NULL) return;
            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count) row = dgv.Rows.Add();
            //the last row
            else if (row < 0) row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = ax.disc;
            dgv.Rows[row].Cells[1].Value = ax.spd_start.ToString("F0");
            dgv.Rows[row].Cells[2].Value = ax.spd_stop.ToString("F0");
            dgv.Rows[row].Cells[3].Value = ax.home_spd.ToString("F0");
            dgv.Rows[row].Cells[4].Value = ax.spd_work.ToString("F0");
            //dgv.Rows[row].Cells[5].Value = ax.max_spd.ToString("F0");
            dgv.Rows[row].Cells[5].Value = ax.tacc.ToString("F3");
            dgv.Rows[row].Cells[6].Value = ax.tdec.ToString("F3");
            dgv.Rows[row].Cells[7].Value = ax.sln.ToString("F0");
            dgv.Rows[row].Cells[8].Value = ax.slp.ToString("F0");
            dgv.Rows[row].Cells[9].Value = ax.pul_per_mm.ToString("F7");
            dgv.Rows[row].Cells[10].Value = ax.home_offset.ToString("F3");
        }

        public void AddAxis(AXIS ax)
        {
            if(true)//if (ax.isInit)
            {
                if (list_ax.Contains(ax) == false)
                {
                    list_ax.Add(ax);
                    FillTableWithAxisInf(ax);
                }
            }
            else
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("轴设置列表,{0} 未初始化！", ax.disc));
                return;
            }            
        }
        public void AddAxis(List<AXIS> list_ax)
        {
            foreach (AXIS ax in list_ax)
            {
                AddAxis(ax);
            }
        }

        public void ClearAxis()
        {
            list_ax.Clear();
        }

        public void AutoUpdate(int intv_ms=300)
        {
            if (intv_ms > 0)
            {
                tmr_update.Interval = intv_ms;
                tmr_update.Enabled = true;
                tmr_update.Start();                
            }
            else
            {
                tmr_update.Enabled = false;
                tmr_update.Stop ();
            }
        }

        public void UpdateShow()
        {
            if (dgv.Rows.Count != list_ax.Count) dgv.Rows.Clear();
            for (int r = 0; r < list_ax.Count; r++)
            {
                FillTableWithAxisInf(list_ax.ElementAt(r), r);
            }
            dgv.Update();
        }

        private void tmr_update_Tick(object sender, EventArgs e)
        {
            UpdateShow();
        }

        public bool SaveToFile(string filename = "")
        {
            //update data
            string disc = "";
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (AXIS ax in list_ax)
                    {
                        if (row.Cells[0].Value.ToString() == ax.disc)
                        {
                            disc = ax.disc;
                            ax.spd_start = Convert.ToDouble(row.Cells[1].Value.ToString());
                            ax.spd_stop = Convert.ToDouble(row.Cells[2].Value.ToString());
                            ax.home_spd = Convert.ToDouble(row.Cells[3].Value.ToString());
                            ax.spd_work = Convert.ToDouble(row.Cells[4].Value.ToString());
                            //ax.max_spd = Convert.ToDouble(row.Cells[5].Value.ToString());
                            ax.tacc = Convert.ToDouble(row.Cells[5].Value.ToString());
                            ax.tdec = Convert.ToDouble(row.Cells[6].Value.ToString());
                            ax.sln = Convert.ToDouble(row.Cells[7].Value.ToString());
                            ax.slp = Convert.ToDouble(row.Cells[8].Value.ToString());
                            ax.pul_per_mm = Convert.ToDouble(row.Cells[9].Value.ToString());
                            ax.home_offset = Convert.ToDouble(row.Cells[10].Value.ToString());
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(disc + " 参数输入异常\r\n"+ex.ToString());
                return false;
            }
            //save
            foreach (AXIS ax in list_ax)
            {
                ax.SaveCfgToInf(filename);
                ax.LoadCfgFrInf(filename);
            }
            UpdateShow();
            return true;
        }

        public EM_RES  LoadFrFile(string filename = "")
        {
            //save
            EM_RES ret = EM_RES.OK;
            foreach (AXIS ax in list_ax)
            {
              ret=  ax.LoadCfgFrInf(filename);
                if (ret != EM_RES.OK)
                    return ret;
            }
            UpdateShow();
            return ret;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
