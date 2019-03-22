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
    public partial class PosTable : UserControl
    {
      public   List<POS> list_pos = new List<POS>();
        #region 初始化
        public PosTable()
        {
            InitializeComponent();
            list_pos.Clear();
        }
        #endregion
        #region 填充数据
        private void FillTableWithPosInf(POS pos, int row = -2)
        {

            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            int sta = pos.axis_state;//二进制位置状态
            if (sta == 0)
                return;
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count)
                row = dgv.Rows.Add();
            //the last row
            else if (row < 0)
                row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = pos.disc;

            if (pos.AxisX!=null)
            {
                dgv.Rows[row].Cells[1].Value = pos.pos_x;
                dgv.Rows[row].Cells[1].Style.BackColor = Color.White;
            }
               
            else
                dgv.Rows[row].Cells[1].Style.BackColor = Color.DarkGray;
            if (pos.AxisY != null)
            {
                dgv.Rows[row].Cells[2].Value = pos.pos_y;
                dgv.Rows[row].Cells[2].Style.BackColor = Color.White;
            }
               
            else
                dgv.Rows[row].Cells[2].Style.BackColor = Color.DarkGray;
            if (pos.AxisZ != null)
            {
                dgv.Rows[row].Cells[3].Value = pos.pos_z;
                dgv.Rows[row].Cells[3].Style.BackColor = Color.White;
            }
            else
                dgv.Rows[row].Cells[3].Style.BackColor = Color.DarkGray;
            if (pos.AxisA != null)
            {
                dgv.Rows[row].Cells[4].Value = pos.pos_a;
                dgv.Rows[row].Cells[4].Style.BackColor = Color.White;
            }
            else
                dgv.Rows[row].Cells[4].Style.BackColor = Color.DarkGray;
        }
        #endregion
        #region 加位置入列
        public void Addpos(POS pos)
        {

            if (list_pos.Contains(pos) == false)
            {
                list_pos.Add(pos);

                pos.LoadCfgPosInf("");

                FillTableWithPosInf(pos);
            }

        }

        public void Addpos(List<POS> list_pos)
        {
            foreach (POS pos in list_pos)
            {
                Addpos(pos);
            }
        }
        #endregion
        public void ClearPos()
        {
            list_pos.Clear();
            dgv.Rows.Clear();
        }
        public void UpdateShow()
        {
            if (dgv.Rows.Count != list_pos.Count) dgv.Rows.Clear();
            for (int r = 0; r < list_pos.Count; r++)
            {
                FillTableWithPosInf(list_pos.ElementAt(r), r);
            }
            dgv.Update();
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
                    foreach (POS pos in list_pos)
                    {
                        if (row.Cells[0].Value.ToString() == pos.disc)
                        {
                            if (pos.AxisX != null)
                                pos.pos_x = Convert.ToDouble(row.Cells[1].Value);
                            if (pos.AxisY != null)
                                pos.pos_y = Convert.ToDouble(row.Cells[2].Value.ToString());
                            if (pos.AxisZ != null)
                                pos.pos_z = Convert.ToDouble(row.Cells[3].Value.ToString());
                            if (pos.AxisA != null)
                                pos.pos_a = Convert.ToDouble(row.Cells[4].Value);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(disc + " 参数输入异常\r\n" + ex.ToString());
                return false;
            }
            //save
            foreach (POS pos in list_pos)
            {
                pos.SaveCfgPosInf(filename);
                pos.LoadCfgPosInf(filename);
            }
            UpdateShow();

            return true;
        }
        public void LoadFrFile(string filename = "")
        {
            //save
            foreach (POS pos in list_pos)
            {
                pos.LoadCfgPosInf(filename);
            }
            UpdateShow();
        }



        private void tmr_update_Tick_1(object sender, EventArgs e)
        {
            UpdateShow();
        }

        private void dgv_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EM_RES ret;
            if (e.RowIndex < 0 || e.RowIndex > list_pos.Count) return;
            if (list_pos.ElementAt(e.RowIndex).disc != dgv.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                MessageBox.Show("轴列表异常，请重新启动软件!");
                return;
            }

            POS pos = list_pos.ElementAt(e.RowIndex);
             //保存
             if (e.ColumnIndex == 7)
            {
                if(pos.AxisX!=null)
                pos.pos_x =  Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[1].Value);
                if (pos.AxisY != null)
                pos.pos_y = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                if (pos.AxisZ != null)
                pos.pos_z = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[3].Value.ToString());
                if (pos.AxisA != null)
                pos.pos_a = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[4].Value);
                ret = pos.SaveCfgPosInf();
                if (ret == EM_RES.OK)
                {
                    MessageBox.Show(pos.disc + "保存成功!");
                }
                else
                    MessageBox.Show(pos.disc + "保存成功!");

            }
        }

        private void dgv_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            EM_RES ret;
            if (e.RowIndex < 0 || e.RowIndex > list_pos.Count) return;
            if (list_pos.ElementAt(e.RowIndex).disc != dgv.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                MessageBox.Show("轴列表异常，请重新启动软件!");
                return;
            }

            POS pos = list_pos.ElementAt(e.RowIndex);
            //定位
            if (e.ColumnIndex == 5)
            {
                ret = pos.MoveTo(ref VAR.gsys_set.bquit);
                if (ret != EM_RES.OK) MessageBox.Show(pos.disc + "定位异常!");
            }
            //获取
            else if (e.ColumnIndex == 6)
            {
                ret = pos.GetPos(ref VAR.gsys_set.bquit);
                FillTableWithPosInf(pos, e.RowIndex);
                if (ret != EM_RES.OK) MessageBox.Show(pos.disc + " 获取异常!");
            }
            //保存
            else if (e.ColumnIndex == 7)
            {
                if(pos.AxisX!=null)
                pos.pos_x =  Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[1].Value);
                if (pos.AxisY != null)
                pos.pos_y = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                if (pos.AxisZ != null)
                pos.pos_z = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[3].Value.ToString());
                if (pos.AxisA != null)
                pos.pos_a = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[4].Value);
                ret = pos.SaveCfgPosInf();
                if (ret == EM_RES.OK)
                {
                    MessageBox.Show(pos.disc + "保存成功!");
                }
                else
                    MessageBox.Show(pos.disc + "保存成功!");

            }
        }



    }
}

