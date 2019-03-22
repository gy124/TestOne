using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionCtrl;


using System.Threading;

using System.IO;



namespace UI
{
    public partial class FrProduct : Form
    {

        public FrProduct()
        {
            InitializeComponent();
        }


        private void btn_logout_Click(object sender, EventArgs e)
        {
            //cb_user.SelectedIndex = 0;
            //tb_pw.Text = "";
            //lb_log_inf.Text = "";
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //string pw = VAR.gst_user[cb_user.SelectedIndex].psw;
            //if (tb_pw.Text == pw)
            //{
            //    VAR.gsys_set.user_idx = cb_user.SelectedIndex;
            //    lb_log_inf.Text = "登陆成功，当前用户为：" + VAR.gst_user[cb_user.SelectedIndex].id;
            //}
            //else
            //{
            //    //MessageBox.Show("Password error!");
            //    lb_log_inf.Text = "登陆失败，密码错误!";
            //}
            //cb_user.SelectedIndex = VAR.gsys_set.user_idx;
            //if (VAR.gsys_set.user_idx >= 0)
            //    tb_pw.Text = VAR.gst_user[VAR.gsys_set.user_idx].psw;
            //else cb_user.SelectedIndex = 0;
        }

        //private void pic_pallet_Paint(object sender, PaintEventArgs e)
        //{
        //    int w = 8;
        //    Point pt_start = new Point(w, w);
        //    Point pt_vend = new Point(w, e.ClipRectangle.Height - w * 2);
        //    Point pt_hend = new Point(e.ClipRectangle.Width - w * 2, w);
        //    Pen pen = new Pen(Color.Blue, 2);

        //    e.Graphics.DrawLine(pen, pt_start, pt_vend);
        //    e.Graphics.DrawLine(pen, pt_vend, new Point(pt_vend.X - w, pt_vend.Y - w));
        //    e.Graphics.DrawLine(pen, pt_vend, new Point(pt_vend.X + w, pt_vend.Y - w));
        //    e.Graphics.DrawLine(pen, pt_start, pt_hend);
        //    e.Graphics.DrawLine(pen, pt_hend, new Point(pt_hend.X - w, pt_hend.Y + w));
        //    e.Graphics.DrawLine(pen, pt_hend, new Point(pt_hend.X - w, pt_hend.Y - w));
        //}
        /// <summary>
        /// 保存料盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                UI.Product.Tray mtry = UI.COM.product.TrayList[cTabControl1.SelectedIndex];
                mtry.row = (int)numberRow.Value;
                mtry.col = (int)numberLine.Value;
                mtry.tl = PosTable1.list_pos[0].pos_xyza;
                mtry.tr = PosTable1.list_pos[1].pos_xyza;
                mtry.bl = PosTable1.list_pos[2].pos_xyza;
                EM_RES ret = mtry.SavTrayCfg(VAR.gsys_set.cur_product_name);
                if (ret != EM_RES.OK)
                {
                    MessageBox.Show("保存料盘数据失败");
                }
                MessageBox.Show("保存料盘数据成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cTabControl1_Click(object sender, EventArgs e)
        {

        }
        public void updateShow()
        {
            COM.product.LoadProductList(listBox_product);
            numberRow.Value = COM.product.TrayList[cTabControl1.SelectedIndex].row;
            numberLine.Value = COM.product.TrayList[cTabControl1.SelectedIndex].col;
            numer_tray_cnt.Value = VAR.gsys_set.box_tray_cnt;
            PosTable1.ClearPos();
            axis_panle.clear();
            PosTable1.UpdateShow();

            switch (cTabControl1.SelectedIndex)
            {
                case 0:
                    PosTable1.Addpos(MT.pos_tray_get);
                    axis_panle.axis_x = MT.AXIS_GET_X;
                    axis_panle.axis_y = MT.AXIS_GET_Y;
                    axis_panle.axis_z = MT.AXIS_GET_Z;
                    axis_panle.axis_a = MT.AXIS_GET_A;
                    axis_panle.update_show();
                    break;
                case 1:
                    PosTable1.Addpos(MT.pos_tray_bk_ok);
                    axis_panle.axis_x = MT.AxList_WS_BACK[0];
                    axis_panle.axis_y = MT.AxList_WS_BACK[1];
                    axis_panle.axis_z = MT.AxList_WS_BACK[2];
                    axis_panle.axis_a = MT.AxList_WS_BACK[3];
                    axis_panle.update_show();
                    break;
                case 2:
                    PosTable1.Addpos(MT.pos_tray_bk_ng);
                    axis_panle.axis_x = MT.AxList_WS_BACK[0];
                    axis_panle.axis_y = MT.AxList_WS_BACK[1];
                    axis_panle.axis_z = MT.AxList_WS_BACK[2];
                    axis_panle.axis_a = MT.AxList_WS_BACK[3];
                    axis_panle.update_show();
                    break;
                case 3:
                    PosTable1.Addpos(MT.pos_tray_bk_AANG);
                    axis_panle.axis_x = MT.AxList_WS_BACK[0];
                    axis_panle.axis_y = MT.AxList_WS_BACK[1];
                    axis_panle.axis_z = MT.AxList_WS_BACK[2];
                    axis_panle.axis_a = MT.AxList_WS_BACK[3];
                    axis_panle.update_show();
                    break;
                default:
                    break;
            }
        }
        private void cTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            updateShow();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string str = "未知产品";
            if (VAR.gsys_set.cur_product_name != null)
                textBox1.Text = VAR.gsys_set.cur_product_name;
            else
                textBox1.Text = str;
            //  COM.product.LoadProductList(listBox_product);
        }

        //切换产品
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                VAR.gsys_set.cur_product_name = listBox_product.SelectedItem.ToString();
                VAR.gsys_set.box_tray_cnt = (int)numer_tray_cnt.Value;
                UI.COM.MVS.LoadInf();//Vison Update
                foreach(Product.Tray  ee  in   UI.COM.product.TrayList)
                {
                    ee.LoadTrayCfg();
                }
                //Pos Update
                foreach (POS mpos in MT.pos_list_get)
                {
                    mpos.UpdatePos();
                }
                foreach (POS mpos in MT.pos_list_bull_move)
                {
                    mpos.UpdatePos();
                }
                foreach (POS mpos in MT.pos_list_feed)
                {
                    mpos.UpdatePos();
                }
                foreach (POS mpos in MT.pos_list_back)
                {
                    mpos.UpdatePos();
                }
            }
            catch
            {
                MessageBox.Show("请选择产品");
            }
            //EM_RES ret = MT.PosInit();
            //if (ret != EM_RES.OK)
            //{
            //    MessageBox.Show("切换失败"); return;
            //}
            //VAR.gsys_set.SaveSysCfg();
            //ret = COM.product.LoadDat(VAR.gsys_set.cur_product_name);
            //if (ret != EM_RES.OK)
            //    MessageBox.Show("切换失败");
            //MessageBox.Show("切换成功");
        }

        private void FrProduct_Load_1(object sender, EventArgs e)
        {
            updateShow();

        }

        private void bt_get_tray_data_Click(object sender, EventArgs e)
        {
            EM_RES ret = COM.product.LoadDat(VAR.gsys_set.cur_product_name);
            if (ret != EM_RES.OK)
            {
                MessageBox.Show("料盘数据加载失败！"); return;
            }
            updateShow();

        }

    }
}
