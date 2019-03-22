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
    public partial class tray : UserControl
    {
        public Product.Tray tray_dat=new Product.Tray(); 
        List<Rectangle> list_rect = new List<Rectangle>();
        Point m_start;
        bool bm_down = false;
        bool ben_edit = false;
        int tmr = 0;
        
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
            get { return lb_disc.Text; }
            set
            {
                lb_disc.Text = value;
                base.Invalidate(true);
            }
        }
        public tray()
        {
            InitializeComponent();
        }

        public void UpdateShow()
        {
            pnl_tray.Refresh();
            
            // 3sec
            if (tmr++ > 5)
            {
                tmr = 0;
                ben_edit = false;
            }
        }

        private void pnl_tray_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //get buf
                BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);
                Graphics gg = myBuffer.Graphics;
                gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                gg.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                gg.Clear(BackColor);

                //draw at buf
                Pen p = new Pen(Bordercolor, 2);

                SolidBrush br = new SolidBrush(OKcolor);
                Font ft = new Font("宋体", 12);

                if (tray_dat != null && tray_dat.col != 0 && tray_dat.row != 0)
                {
                    float w = (float)e.ClipRectangle.Width / (float)tray_dat.col;
                    float h = (float)e.ClipRectangle.Height / (float)tray_dat.row;

                    list_rect.Clear();
                    int okcnt = 0;
                    int ngcnt = 0;
                    int emptycnt = 0;
                    int maskcnt = 0;
                    for (int n = 0; n < tray_dat.list_cam.Count; n++)
                    {
                        //set color
                        switch (tray_dat.list_cam[n].res)
                        {
                            case Product.EM_CM_RES.UNTEST:
                                br.Color = UTcolor;
                                break;
                            case Product.EM_CM_RES.NONE:
                                br.Color = UNcolor;
                                if (tray_dat.list_mask[n]) emptycnt++;
                                break;
                            case Product.EM_CM_RES.OK:
                                br.Color = OKcolor;
                                okcnt++;
                                break;
                            case Product.EM_CM_RES.ERR:
                                br.Color = ERRcolor;
                                break;
                            case Product.EM_CM_RES.NG:
                            default:
                                ngcnt++;
                                br.Color = NGcolor;
                                break;
                        }

                        //set rect
                        Rectangle rect = new Rectangle();
                        rect.X = (int)(tray_dat.list_cam[n].col * w + 0.5);
                        rect.Y = (int)(tray_dat.list_cam[n].row * h + 0.5);
                        rect.Width = (int)(w + 0.5);
                        rect.Height = (int)(h + 0.5);                    
                        list_rect.Add(rect);
                        gg.DrawRectangle(p, rect);
                        gg.FillRectangle(br, rect);

                        //mask  判断有模组
                        if (!tray_dat.list_mask[n])
                        {
                            maskcnt++;
                            if (br.Color == Color.Red)
                            {
                                p.Color = Color.White;
                            }
                            else
                            {
                                p.Color = Color.Red;
                            }
                            gg.DrawLine(p, rect.Location, new Point(rect.X + rect.Width, rect.Y + rect.Height));
                            gg.DrawLine(p, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X, rect.Y + rect.Height));
                            p.Color = Bordercolor;
                        }
                        //idx
                        string str = tray_dat.list_cam[n].index.ToString();
                        float ft_w = gg.MeasureString(str, ft).Width;
                        float ft_h = gg.MeasureString(str, ft).Height;
                        gg.DrawString(str, ft, Brushes.White, new PointF(rect.X + rect.Width / 2 - ft_w / 2, rect.Y + rect.Height / 2 - ft_h / 2));
                    }
                    lb_status.Text = string.Format("{0}{1}/{2}", tray_dat.barcode, tray_dat.list_cam.Count - emptycnt, tray_dat.list_cam.Count);
                    if (ben_edit)
                    {
                        Rectangle rect = new Rectangle();
                        rect.X = 0;
                        rect.Y = 0;
                        rect.Width = e.ClipRectangle.Width - 2;
                        rect.Height = e.ClipRectangle.Height - 2;
                        p.Color = Color.FromArgb(23, 169, 254);
                        p.Width = 3;
                        gg.DrawRectangle(p, rect);
                    }
                }
                else
                {
                    lb_status.Text = string.Format("NO TRAY");
                }

                //show buf, then dispose
                myBuffer.Render(e.Graphics);
                gg.Dispose();
                myBuffer.Dispose();
            }
            catch
            { }
        }

        private void pnl_tray_MouseDown(object sender, MouseEventArgs e)
        {
            if (tray_dat == null) return;
            if(ben_edit==false)
            {
                if (DialogResult.OK == MessageBox.Show(this, "确定要修改料盘物料状态?", "提示" , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    ben_edit = true;
                    tmr = 0;
                    return;
                }
            }
            tmr = 0;
            m_start.X = e.X;
            m_start.Y = e.Y;
            bm_down = true;
            //for(int n = 0;n< list_rect.Count;n++)
            //{
            //    if (list_rect[n].Contains(new Point(e.X, e.Y)))
            //    {
            //        tray_dat.list_mask[n] = !tray_dat.list_mask[n];
            //        pnl_tray.Refresh();
            //    }
            //}
        }

        private void pnl_tray_MouseUp(object sender, MouseEventArgs e)
        {
            if (ben_edit)
            {
                if (Math.Abs(e.X - m_start.X) < 3 && Math.Abs(e.Y - m_start.Y) < 3)
                {
                    for (int n = 0; n < list_rect.Count; n++)
                    {
                        if (list_rect[n].Contains(e.X, e.Y))
                            for (int i = 0; i <= n;i++ )
                            {
                                tray_dat.list_mask[i] = !tray_dat.list_mask[n];
                            }
                                
                    }
                    pnl_tray.Refresh();
                }
            }
            bm_down =false;            
        }

        private void pnl_tray_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bm_down || !ben_edit || tray_dat == null) return;
            int w = e.X - m_start.X;
            int h = e.Y - m_start.Y;
            Rectangle rect = new Rectangle(Math.Min(m_start.X, e.X), Math.Min(m_start.Y, e.Y), Math.Abs(w), Math.Abs(h));
            for (int n = 0; n < list_rect.Count && n < tray_dat.list_mask.Count; n++)
            {
                if (rect.Contains(list_rect[n].X + list_rect[n].Width/ 2, list_rect[n].Y + list_rect[n].Height / 2))
                {
                    tray_dat.list_mask[n] = w < 0 || h < 0 ?
                        false : true;
                }
            }
            pnl_tray.Refresh();
        }
    }
}
