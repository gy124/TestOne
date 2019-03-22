using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCtrl
{
    public partial class pos_table_mini : UserControl
    {
        List<POS> list_pos = new List<POS>();
      public static  List<POS> list_pos_mini = null;
        public pos_table_mini()
        {
            InitializeComponent();
            ClearPos();
        }
        public void ClearPos()
        {
            list_pos.Clear();
            dgv.Rows.Clear();
        }
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
        #region 填充数据
        private void FillTableWithPosInf(POS pos, int row = -2)
        {

            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count)
                row = dgv.Rows.Add();
            //the last row
            else if (row < 0)
                row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = pos.disc;


        }
        #endregion
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            if (e.ColumnIndex ==1)
            {
                if (list_pos_mini != null)
                {
                    ret = list_pos_mini[e.RowIndex].MoveTo(ref VAR.gsys_set.bquit);
                    ret = pos.MoveTo(ref VAR.gsys_set.bquit);
                    if (ret != EM_RES.OK) MessageBox.Show(pos.disc + "定位异常!");
                }
                
            }
        }
    }
}
