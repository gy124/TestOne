using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionCtrl;

namespace UI
{
    public partial class traybox : UserControl
    {
        public TrayBox box = new TrayBox();
        bool bflash = false;
        List<Rectangle> list_rect = new List<Rectangle>();

        private TrayBox.EM_DIR _bdir =  TrayBox.EM_DIR.IN_OUT;
        [DefaultValue(typeof(TrayBox.EM_DIR), "TrayBox.EM_DIR.IN_OUT")]
        [Description("料仓方向")]
        public TrayBox.EM_DIR Direction
        {
            get { return _bdir; }
            set
            {
                _bdir = value;
                switch(value)
                {
                    case TrayBox.EM_DIR.ONLY_IN:
                        btn_in.Visible = true;
                        btn_out.Visible = false;
                        break;
                    case TrayBox.EM_DIR.ONLY_OUT:
                        btn_in.Visible = false;
                        btn_out.Visible = true;
                        break;
                    case TrayBox.EM_DIR.IN_OUT:
                        btn_in.Visible = true;
                        btn_out.Visible = true;
                        break;
                }
                base.Invalidate(true);
            }
        }

        private Color _bordercolor = Color.DodgerBlue;
        [DefaultValue(typeof(Color), "Color.DodgerBlue")]
        [Description("描边色")]
        public Color Bordercolor
        {
            get { return _bordercolor; }
            set
            {
                _bordercolor = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_done = Color.LightSkyBlue;
        [DefaultValue(typeof(Color), "Brushes.LightSkyBlue")]
        [Description("完成颜色")]
        public Color DONEcolor
        {
            get { return _cl_done; }
            set
            {
                _cl_done = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_empty = Color.Gray;
        [DefaultValue(typeof(Color), "Brushes.Gray")]
        [Description("无料颜色")]
        public Color Emptycolor
        {
            get { return _cl_empty; }
            set
            {
                _cl_empty = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_untest = Color.SkyBlue;
        [DefaultValue(typeof(Color), "Brushes.SkyBlue")]
        [Description("待测颜色")]
        public Color UNTESTcolor
        {
            get { return _cl_untest; }
            set
            {
                _cl_untest = value;
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
        [Description("料仓名称")]
        public string TrayBoxName
        {
            get { return lb_name.Text; }
            set
            {
                lb_name.Text = value;
                base.Invalidate(true);
            }
        }

        public traybox()
        {
            InitializeComponent();
        }

        public void UpdateShow()
        {
            Direction = box.direction;
            chk_zk_on.Checked = box.out_tray_zk != null ? box.out_tray_zk.isON :false;
         //   chk_zk_sen.Checked = box.in_tray_zk != null ? box.in_tray_zk.isON : false;
         //   chk_box_sen.Checked = box.in_box_sen != null ? box.in_box_sen.isON : false;

            bflash = !bflash;
            switch (box.status)
            {
                case TrayBox.EM_STA.HOME:
                case TrayBox.EM_STA.EMPTY:
                case TrayBox.EM_STA.FULL:
                case TrayBox.EM_STA.UNKNOWN:
                    if(bflash)lb_status.BackColor = Color.Gold;
                    else lb_status.BackColor = BackColor;
                    break;
                case TrayBox.EM_STA.ERR:
                    if (bflash) lb_status.BackColor = Color.Red;
                    else lb_status.BackColor = BackColor;
                    break;
                default:
                    lb_status.BackColor = BackColor;
                    break;

            }
            lb_status.Text = Utility.GetDescription(box.status);
            lb_z_pos.Text = string.Format("{0:F3}",box.ax_z.fcmd_pos);
            pnl_status.Refresh();
        }

        private void pnl_status_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (box != null && box.list_sta != null && box.list_sta.Count > 0)
                {
                    //get buf
                    BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                    BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
                    Graphics gg = myBuffer.Graphics;
                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    gg.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                    gg.Clear(BackColor);


                    Pen p = new Pen(Bordercolor, 2);
                    SolidBrush br = new SolidBrush(Emptycolor);
                    Font ft = new Font("宋体", 9);

                    float w = e.ClipRectangle.Width - 10;
                    float h = (float)e.ClipRectangle.Height / (float)box.tray_cnt;
                    list_rect.Clear();
                    for (int n = 0; n < box.list_sta.Count; n++)
                    {
                        //set color
                        switch (box.list_sta.ElementAt(n))
                        {
                            case TrayBox.EM_STA.FULL:
                            case TrayBox.EM_STA.DONE:
                                br.Color = DONEcolor;
                                break;
                            case TrayBox.EM_STA.UNTEST:
                                br.Color = UNTESTcolor;
                                break;
                            case TrayBox.EM_STA.ERR:
                                br.Color = ERRcolor;
                                break;

                            case TrayBox.EM_STA.EMPTY:
                            default:
                                br.Color = Emptycolor;
                                break;
                        }

                        //set rect
                        Rectangle rect = new Rectangle();
                        rect.X = 0;
                        rect.Y = (int)(h * n + 0.5);
                        rect.Width = (int)w;
                        rect.Height = (int)(h + 0.5);
                        gg.DrawRectangle(p, rect);
                        gg.FillRectangle(br, rect);
                        list_rect.Add(rect);
                        //arrow
                        if (n == box.tray_idx)
                        {
                            PointF[] ptf = new PointF[] { new PointF(w + 2, rect.Y + h / 2), new PointF(w + 8, rect.Y + h / 2 + 4), new PointF(w + 9, rect.Y + h / 2 - 4) };
                            gg.FillPolygon(Brushes.Red, ptf);
                        }
                        gg.DrawString(n.ToString(), ft, Brushes.White, new PointF(rect.X + rect.Width / 2 - gg.MeasureString(n.ToString(), ft).Width / 2, rect.Y + rect.Height / 2 - gg.MeasureString(n.ToString(), ft).Height / 2));
                    }

                    //show buf, then dispose
                    myBuffer.Render(e.Graphics);
                    gg.Dispose();
                    myBuffer.Dispose();
                }
            }
            catch
            { }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            box.Up(ref VAR.gsys_set.bquit);
            pnl_status.Refresh();
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            box.Down(ref VAR.gsys_set.bquit);
            pnl_status.Refresh();
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
           
            EM_RES ret = box.TrayIn(ref VAR.gsys_set.bquit);
            if (ret == EM_RES.OK) MessageBox.Show("进料成功");
            else MessageBox.Show("进料失败");
            pnl_status.Refresh();
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
        EM_RES ret=   box.TrayOut (ref VAR.gsys_set.bquit);
        if (ret == EM_RES.OK) MessageBox.Show("出料成功");
        else MessageBox.Show("出料失败");
            pnl_status.Refresh();
        }
    }
}
