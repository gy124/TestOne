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
    public partial class IOTable : UserControl
    {
        List<GPIO> list_IO=new List<GPIO> ();
        Color cl_out_on = Color.Lime;
        Color cl_in_on = Color.Orange;
        public IOTable()
        {
            InitializeComponent();
            list_IO.Clear();            
        }

        private void FillTableWithAxisInf(GPIO io, int row = -2)
        {
            if (io == null) return;
            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count) row = dgv.Rows.Add();
            //the last row
            else if (row < 0) row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = io.str_disc;
            dgv.Rows[row].Cells[1].Value = io.isON ? "ON" : "OFF";
            dgv.Rows[row].Cells[2].Value = io.dir == GPIO.IO_DIR.IN ? "IN" : "OUT";
            dgv.Rows[row].Cells[3].Value = io.card.disc;
            dgv.Rows[row].Cells[4].Value = io.num;
        }

        public void AddIO(GPIO io)
        {
            if (true)//if (ax.isInit)
            {
                if (io != null && io.card!=null && list_IO.Contains(io) == false)
                {
                    list_IO.Add(io);
                    FillTableWithAxisInf(io);
                }
            }
            else
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("板卡列表,{0} 未初始化！", io.disc));
                return;
            }            
        }

        public void AddIO(List<GPIO> list_io)
        {
            foreach (GPIO io in list_io)
            {
                AddIO(io);
            }
        }

        public void ClearIO()
        {
            list_IO.Clear();
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
            if (dgv.Rows.Count != list_IO.Count) dgv.Rows.Clear();
            for (int r = 0; r < list_IO.Count; r++)
            {
                FillTableWithAxisInf(list_IO.ElementAt(r), r);
            }
            dgv.Update();
        }

        private void tmr_update_Tick(object sender, EventArgs e)
        {
            UpdateShow();            
        }
        /// <summary>
        /// 分类显示
        /// </summary>
        /// <param name="cfg">0：只显示OUT,1:只显示IN, 2：显示所有</param>
        public void ShowCfg(int cfg = 0)
        {
            if(cfg == 0)
            {
                dgv.Columns[5].Visible = true;
                dgv.Columns[6].Visible = true;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[2].Value.ToString() == "OUT") row.Visible = true;
                    else row.Visible = false;
                }
            }
            else if (cfg == 1)
            {
                dgv.Columns[5].Visible = false;
                dgv.Columns[6].Visible = false;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[2].Value.ToString() == "IN") row.Visible = true;
                    else row.Visible = false;
                }
            }
            else if (cfg == 2)
            {
                dgv.Columns[5].Visible = true;
                dgv.Columns[6].Visible = true;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Visible = true;
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EM_RES ret;
            if (e.RowIndex < 0 || e.RowIndex > list_IO.Count) return;
            if (list_IO.ElementAt(e.RowIndex).str_disc != dgv.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                MessageBox.Show("IO列表异常，请重新启动软件!");
                return;
            }

            //打开
            if (e.ColumnIndex == 5)
            {
                ret = list_IO.ElementAt(e.RowIndex).SetOn();
                if (ret != EM_RES.OK) 
                MessageBox.Show(list_IO.ElementAt(e.RowIndex).disc + "打开异常!Err:" + ret.ToString());  
            }
            //关闭
            else if (e.ColumnIndex == 6)
            {
                ret = list_IO.ElementAt(e.RowIndex).SetOff();
                if (ret != EM_RES.OK) 
                MessageBox.Show(list_IO.ElementAt(e.RowIndex).disc + "关闭异常!Err:" + ret.ToString());             
            }
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.Value.ToString() == "ON")
                {
                    if (list_IO.ElementAt(e.RowIndex).dir == GPIO.IO_DIR.OUT) e.CellStyle.BackColor = cl_out_on;
                    else e.CellStyle.BackColor = cl_in_on;
                }
            }
        }
    }
}
