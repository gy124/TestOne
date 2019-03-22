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
    public partial class CardTable : UserControl
    {
        List<CARD> list_card=new List<CARD> ();

        public CardTable()
        {
            InitializeComponent();
            list_card.Clear();            
        }

        private  void FillTableWithAxisInf(CARD card, int row=-2)
        {
            if (card == null) return;
            //if empty or add mode then add
            //if (dgv.Rows.Count == 0|| row == -2) row = dgv.Rows.Add();
            if (dgv.Rows.Count == 0 || row < 0 || row >= dgv.Rows.Count) row = dgv.Rows.Add();
            //the last row
            else if (row < 0) row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = card.disc;
            dgv.Rows[row].Cells[1].Value = card.isReady?"就绪":"异常";
            dgv.Rows[row].Cells[2].Value = card.id;
            dgv.Rows[row].Cells[3].Value = card.ip.Length > 0 ? card.ip : (card.maincard_id > 0 ? string.Format("{0}/{1}", card.card_id, card.maincard_id) : card.card_id.ToString());
            dgv.Rows[row].Cells[4].Value = card.ax_num;
            dgv.Rows[row].Cells[5].Value = card.output_num;
            dgv.Rows[row].Cells[6].Value = card.input_num;
            dgv.Rows[row].Cells[7].Value = card.brand;
            dgv.Rows[row].Cells[8].Value = card.ps;
        }

        public void AddCard(CARD card)
        {
            if (true)//if (ax.isInit)
            {
                if (list_card.Contains(card) == false)
                {
                    list_card.Add(card);
                    FillTableWithAxisInf(card);
                }
            }
            else
            {
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("板卡列表,{0} 未初始化！", card.disc));
                return;
            }            
        }

        public void AddCard(List<CARD> list_card)
        {
            foreach (CARD card in list_card)
            {
                AddCard(card);
            }
        }

        public void ClearCard()
        {
            list_card.Clear();
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
            if (dgv.Rows.Count != list_card.Count) dgv.Rows.Clear();
            for (int r = 0; r < list_card.Count; r++)
            {
                FillTableWithAxisInf(list_card.ElementAt(r), r);
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
            if (e.RowIndex < 0 || e.RowIndex > list_card.Count) return;
            if (list_card.ElementAt(e.RowIndex).disc != dgv.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                MessageBox.Show("卡列表异常，请重新启动软件!");
                return;
            }

            //打开
            if (e.ColumnIndex == 9)
            {
                ret = list_card.ElementAt(e.RowIndex).Init();
                if (ret != EM_RES.OK) MessageBox.Show(list_card.ElementAt(e.RowIndex).disc + "打开异常!Err:" + ret.ToString());
            }
            //关闭
            else if (e.ColumnIndex == 10)
            {
                ret = list_card.ElementAt(e.RowIndex).Close();
                if (ret != EM_RES.OK) MessageBox.Show(list_card.ElementAt(e.RowIndex).disc + "关闭异常!Err:" + ret.ToString());
            }
        }
    }
}
