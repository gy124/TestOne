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
using System.Reflection;

namespace MotionCtrl
{
    public partial class CylinderTable : UserControl
    {
        List<Cylinder> list_cld=new List<Cylinder> ();
        Color cl_out_on = Color.Lime;
        Color cl_in_on = Color.Orange;
        bool bquit = false;
        public CylinderTable()
        {
            InitializeComponent();
            list_cld.Clear();            
        }

        private  void FillTableWithAxisInf(Cylinder cld, int row=-2)
        {
            if (cld == null) return;
            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count) row = dgv.Rows.Add();
            //the last row
            else if (row < 0) row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = cld.io_out.str_disc;
            dgv.Rows[row].Cells[1].Value = cld.isON?"ON":"OFF";
            dgv.Rows[row].Cells[2].Value = cld.io_sen_on == null? "":(cld.io_sen_on.Status == cld.io_sen_on_active?"ON":"OFF");
            dgv.Rows[row].Cells[3].Value = cld.io_sen_off == null ? "" : (cld.io_sen_off.Status == cld.io_sen_off_active ? "ON" : "OFF");
        }

        public void AddCylinder(Cylinder cld)
        {
            if (true)//if (ax.isInit)
            {
                if (cld != null && cld.io_out !=null && list_cld.Contains(cld) == false)
                {
                    list_cld.Add(cld);
                    FillTableWithAxisInf(cld);
                }
            }
            else
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("气缸列表,{0} 未初始化！", cld.io_out.str_disc));
                return;
            }            
        }

        public void AddCylinder(List<Cylinder> list_cld)
        {
            foreach (Cylinder cld in list_cld)
            {
                AddCylinder(cld);
            }
        }

        public void AddCylinder(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] pArray = type.GetFields();
            foreach (FieldInfo p in pArray)
            {
                if (p.FieldType.Name == "Cylinder")
                    AddCylinder(((Cylinder)p.GetValue(obj)));
            }
        }

        public void ClearCylinder()
        {
            list_cld.Clear();
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
            if (dgv.Rows.Count != list_cld.Count) dgv.Rows.Clear();
            for (int r = 0; r < list_cld.Count; r++)
            {
                FillTableWithAxisInf(list_cld.ElementAt(r), r);
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
           
            if (e.RowIndex < 0 || e.RowIndex > list_cld.Count) return;
            if (list_cld.ElementAt(e.RowIndex).io_out.str_disc != dgv.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                MessageBox.Show("气缸列表异常，请重新启动软件!");
                return;
            }

            //打开
            if (e.ColumnIndex == 4)
            {
                ret = list_cld.ElementAt(e.RowIndex).SetOn();
                if (ret != EM_RES.OK) MessageBox.Show(list_cld.ElementAt(e.RowIndex).io_out.str_disc + "打开异常!Err:" + ret.ToString());
            }
            //关闭
            else if (e.ColumnIndex == 5)
            {
                ret = list_cld.ElementAt(e.RowIndex).SetOff();
                if (ret != EM_RES.OK) MessageBox.Show(list_cld.ElementAt(e.RowIndex).io_out.str_disc + "关闭异常!Err:" + ret.ToString());
            }
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.Value.ToString() == "ON") e.CellStyle.BackColor = cl_out_on;
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                if (e.Value.ToString() == "ON") e.CellStyle.BackColor = cl_in_on;
            }
        }
    }
}
