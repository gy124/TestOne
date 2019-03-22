using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Compment
{
    public partial class tray_btn : UserControl
    {
        public Product.Tray tray_dat = new Product.Tray(0);
        private Color _cl_ng = Color.Red;
        [DefaultValue(typeof(Color), "Brushes.Red")]
        [Description("NG颜色")]
        public Color NGcolor
        {
            get { return _cl_ng; }
            set
            {
                _cl_ng = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_ok = Color.Lime;
        [DefaultValue(typeof(Color), "Brushes.Lime")]
        [Description("OK颜色")]
        public Color OKcolor
        {
            get { return _cl_ok; }
            set
            {
                _cl_ok = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_un = Color.Silver;
        [DefaultValue(typeof(Color), "Brushes.Silver")]
        [Description("无料颜色")]
        public Color UNcolor
        {
            get { return _cl_un; }
            set
            {
                _cl_un = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_ut = Color.SkyBlue;
        [DefaultValue(typeof(Color), "Brushes.SkyBlue")]
        [Description("待测颜色")]
        public Color UTcolor
        {
            get { return _cl_ut; }
            set
            {
                _cl_ut = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_err = Color.Gold;
        [DefaultValue(typeof(Color), "Brushes.Gold")]
        [Description("异常颜色")]
        public Color ERRcolor
        {
            get { return _cl_err; }
            set
            {
                _cl_err = value;
                base.Invalidate(true);
            }
        }
        [DefaultValue(typeof(string), "名称")]
        [Description("名称")]
        public string TrayName
        {
            get { return lbl_name.Text; }
            set
            {
                lbl_name.Text = value;
                base.Invalidate(true);
            }
        }
        [Description("列数")]
        public int col_num
        {
            get { return dgv.Columns.Count/dgv.Rows.Count; }

        }
        public tray_btn()
        {
            InitializeComponent();
        }
        public void UpdateShow()
        {
            int okcnt = 0;
            int ngcnt = 0;
            int emptycnt = 0;
            int maskcnt = 0;
            int row = 0;
            int col = 0;

            if (tray_dat == null || tray_dat.col == 0|| tray_dat.row == 0)
            {
                return;
            }
                     
                dgv.Columns.Clear();             
                for (int n = 0; n < tray_dat.list_cam.Count; n++)
                {
                    //set color
                    row = n / col_num;
                    col = n % col_num;
                    dgv.Rows[row].Cells[col].Value = tray_dat.list_cam[n].index;
                    switch (tray_dat.list_cam[n].res)
                    {
                        case Product.EM_CM_RES.UNTEST:
                            dgv.Rows[row].Cells[col].Style.BackColor = UTcolor;
                            break;
                        case Product.EM_CM_RES.NONE:
                            dgv.Rows[row].Cells[col].Style.BackColor = UNcolor;
                            if (!tray_dat.list_mask[n]) emptycnt++;
                            break;
                        case Product.EM_CM_RES.OK:
                            dgv.Rows[row].Cells[col].Style.BackColor = OKcolor;
                            okcnt++;
                            break;
                        case Product.EM_CM_RES.ERR:
                            dgv.Rows[row].Cells[col].Style.BackColor = ERRcolor;
                            break;
                        case Product.EM_CM_RES.NG:
                             ngcnt++;
                            dgv.Rows[row].Cells[col].Style.BackColor = NGcolor;
                            break;
                        default:
                            ngcnt++;
                            dgv.Rows[row].Cells[col].Style.BackColor = NGcolor;
                            break;
                    }
                    //mask
                    if (tray_dat.list_mask[n])
                    {
                        maskcnt++;
                        dgv.Rows[row].Cells[col].Style.BackColor = OKcolor;
                    }

                }
                lbl_statu.Text = string.Format("{0}/{1}", emptycnt, tray_dat.list_cam.Count);
            // 3sec
        }
        private void FillTableWithindex( )
        {
            int n = 0;       
            if (dgv.Columns.Count == 0 )
            {                    
                 return;
            }
        
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; i < dgv.Rows.Count; i++)
                {
                    n = i * col_num + j;
                    dgv.Rows[i].Cells[j].Value = n.ToString();
                
                }

            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateShow();
        }

        private void tray_btn_Load(object sender, EventArgs e)
        {
            //Font ft = new Font("宋体", 12);
            if (tray_dat != null && tray_dat.col != 0 && tray_dat.row != 0)
            {
              
                dgv.Columns.Clear();
                DataGridViewRow row = new DataGridViewRow();

              //  DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
               // textboxcell.Value = "aaa";
             //   row.Cells.Add(textboxcell);
             //   DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
            //    row.Cells.Add(comboxcell);

                DataGridViewButtonCell btncell = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell2 = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell3 = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell4 = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell5 = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell6 = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell7 = new DataGridViewButtonCell();
                DataGridViewButtonCell btncell8 = new DataGridViewButtonCell();

                List<DataGridViewButtonCell> cell_list = new List<DataGridViewButtonCell> { btncell, btncell2,
                    btncell3, btncell4, btncell5, btncell6, btncell7, btncell8 };

                DataTable dt = new DataTable();
               

             







               for (int j = 0; j < 3; j++)
               {
                   dt.Columns.Add("n", typeof(string));

               }
                
               //for (int i = 0; i < 3; i++)
               //{
               //    dt.Rows.Add();
               //}
               dgv.DataSource = dt;//绑定
               dgv.RowHeadersVisible = false;//datagridview前面的空白部分去除
               dgv.ScrollBars = ScrollBars.None;//滚动条去除
            }
         
            //else
            //{
            //    lbl_name.Text = string.Format("空盘");
            //}
        }
    }
}
