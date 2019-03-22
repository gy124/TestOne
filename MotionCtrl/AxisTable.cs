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
    public partial class AxisTable : UserControl
    {
        List<AXIS> list_ax = new List<AXIS>();

        public AxisTable()
        {
            InitializeComponent();
            list_ax.Clear();
        }

        private void FillTableWithAxisInf(AXIS ax, int row = -2)
        {
            if (ax == null || ax.mt_type == AXIS.MT_TYPE.NULL) return;
            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count) row = dgv.Rows.Add();
            //the last row
            else if (row < 0) row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = ax.disc;
            dgv.Rows[row].Cells[1].Value = ax.str_status;
            dgv.Rows[row].Cells[2].Value = ax.fcmd_pos.ToString("F3");
            dgv.Rows[row].Cells[3].Value = ax.fenc_pos.ToString("F3");
            dgv.Rows[row].Cells[4].Value = ax.isORG;
            dgv.Rows[row].Cells[5].Value = ax.isELN;
            dgv.Rows[row].Cells[6].Value = ax.isELP;
            dgv.Rows[row].Cells[7].Value = ax.isSLN;
            dgv.Rows[row].Cells[8].Value = ax.isSLP;
            dgv.Rows[row].Cells[9].Value = ax.isINP;
            dgv.Rows[row].Cells[10].Value = ax.isALM;
            dgv.Rows[row].Cells[11].Value = ax.isSVRON;
        }

        public void AddAxis(AXIS ax)
        {
            if (true)//if (ax.isInit)
            {
                if (list_ax.Contains(ax) == false)
                {
                    list_ax.Add(ax);
                    FillTableWithAxisInf(ax);
                }
            }
            else
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("轴状态列表,{0} 未初始化！", ax.disc));
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

        public void AutoUpdate(int intv_ms = 300)
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
                tmr_update.Stop();
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

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EM_RES ret;
            if (e.RowIndex < 0 || e.RowIndex > list_ax.Count) return;
            if (list_ax.ElementAt(e.RowIndex).disc != dgv.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                MessageBox.Show("轴列表异常，请重新启动软件!");
                return;
            }

            AXIS ax = list_ax.ElementAt(e.RowIndex);

            //负向
            if (e.ColumnIndex == 14)
            {
                ret = ax.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.N);
                if (ret != EM_RES.OK) MessageBox.Show(ax.disc + "负向移动异常!");

            }
            //正向
            else if (e.ColumnIndex == 15)
            {
                ret = ax.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.P);
                if (ret != EM_RES.OK) MessageBox.Show(ax.disc + "正向移动异常!");
                //int i = 32;
                //int m = 1;
                //m = (i >> 5) & 1;
                //i = m;
                //m = (32 & 0x20);
                //i = m;

            }
            //定位
            else if (e.ColumnIndex == 13)
            {
                bool bquit = false;
                double pos = double.MaxValue;
                try
                {
                    if (dgv.Rows[e.RowIndex].Cells[12].Value == null || dgv.Rows[e.RowIndex].Cells[12].Value.ToString().Length == 0)
                        return;
                    pos = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[12].Value);
                }
                catch
                {
                    MessageBox.Show(ax.disc + "获取定位坐标异常，请确保定位栏坐标输入正常!");
                    return;
                }
                ret = ax.SetToManualHighSpd();
                if (ret != EM_RES.OK) MessageBox.Show(ax.disc + "速度设置异常!");
                ret = ax.MoveTo(ref bquit, pos, 10000, true);
                if (ret != EM_RES.OK) MessageBox.Show(ax.disc + "定位异常!");
            }
            else if (e.ColumnIndex == 16)
            {
                VAR.gsys_set.bquit = false;
                ax.HomeTask(20000);
                while (VAR.gsys_set.bquit == false)
                {
                    if (ax.HomeTaskisEnd) break;
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG, string.Format("{0} end...", ax.disc));
                if (ax.HomeTaskRet != EM_RES.OK)
                    MessageBox.Show(ax.disc + "回零异常!");
                else
                    MessageBox.Show(ax.disc + "回零成功!");
            }
            //使能
            else if (e.ColumnIndex == 11)
            {
                if (ax.SVRON && DialogResult.Yes == MessageBox.Show("是否松开电机?\r\n松开后需要归零操作!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    ax.SVRON = false;
                    VAR.gsys_set.status = EM_SYS_STA.UNKOWN;
                }
                else ax.SVRON = true;
            }
        }
    }
}