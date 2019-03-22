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

namespace UI.Compment
{
    public partial class DownLoad : UserControl
    {

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
        private Color _cl_out_on = Color.Lime;
        [DefaultValue(typeof(Color), "Color.Lime")]
        [Description("输出ON颜色")]
        public Color OutOnColor
        {
            get { return _cl_out_on; }
            set
            {
                _cl_out_on = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_in_on = Color.DarkOrange;
        [DefaultValue(typeof(Color), "Color.DarkOrange")]
        [Description("传感器输入ON颜色")]
        public Color SenOnColor
        {
            get { return _cl_in_on; }
            set
            {
                _cl_in_on = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_off = Color.WhiteSmoke;
        [DefaultValue(typeof(Color), "Color.WhiteSmoke")]
        [Description("输入输出OFF颜色")]
        public Color OFFColor
        {
            get { return _cl_off; }
            set
            {
                _cl_off = value;
                base.Invalidate(true);
            }
        }
        private Color _cl_err = Color.Red;
        [DefaultValue(typeof(Color), "Color.Red")]
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

        public DownLoad()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        public void UpdateShow()
        {
    //        lb_pos.Text = string.Format("Y:{0:000.000}\nZ:{1:000.000}", DownloadModle.ax_y.fenc_pos, DownloadModle.ax_z.fenc_pos);
            pnl_sta.Refresh();
        }
        void drawHD(int id, Cylinder cy_ud, Cylinder cy_hd, ref Graphics gg, float x, float y, float w, float h)
        {
            Pen p = new Pen(Bordercolor, 2);
            SolidBrush br = new SolidBrush(OutOnColor);
            Font ft = new Font("宋体", 10);
            Rectangle rect = new Rectangle();
            
            //ud
            rect.X = (int)(x + w * 0.35 + 0.5);
            rect.Y = (int)(y + h * 0.4 + 0.5);
            rect.Width = (int)(w * 0.3 + 0.5);
            rect.Height = (int)(h * 0.3 + 0.5);
            gg.DrawRectangle(p, rect);
            if (cy_ud.isON)
            {                
                if(!cy_ud.isSenOFFActive) br.Color = OutOnColor;
                else br.Color = ERRcolor;
            }
            else br.Color = OFFColor;
            gg.FillRectangle(br, rect);

            //sensor    
            if (cy_ud.isSenOFFActive)
            {
                rect.Height = (int)(rect.Height * 0.4);
                br.Color = SenOnColor;
                gg.FillRectangle(br, rect);
            }

            //id
            string str = (id+1).ToString();
            float str_w = gg.MeasureString(str, ft).Width;
            float str_h = gg.MeasureString(str, ft).Height;
            gg.DrawString(str, ft, Brushes.DarkGray, new PointF(rect.X + rect.Width / 2 - str_w / 2, rect.Y - str_h));

            //down
            rect.X = (int)(x + w * 0.2 + 0.5);
            rect.Y = (int)(y + h * 0.7 + 0.5);
            rect.Width = (int)(w * 0.6 + 0.5);
            rect.Height = (int)(h * 0.15 + 0.5);
            gg.DrawRectangle(p, rect);
            if (cy_hd.isON)
            {
                if (!cy_hd.isSenOFFActive) br.Color = OutOnColor;
                else br.Color = ERRcolor;
            }
            else br.Color = OFFColor;
            gg.FillRectangle(br, rect);
            //sensor
            if (cy_hd.isSenONActive)
            {
                rect.Width = (int)(rect.Width * 0.4);
                br.Color = SenOnColor;
                gg.FillRectangle(br, rect);
            }
            
        }
        private void pnl_sta_Paint(object sender, PaintEventArgs e)
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
                int row = 2;
                int col = 5;
                float w = (float)e.ClipRectangle.Width / (float)col;
                float h = (float)e.ClipRectangle.Height / (float)row;

                //for (int n = 0; n < 10 && n < 10; n++)
                //{
                //    drawHD(n, DownloadModle.List_CLD_UD_HD[n], DownloadModle.List_CLD_HD_HD[n], ref gg, w * (n % col), h * (n / col), w, h);
                //    //drawHD(DownloadModle.List_CLD_UD_HD.Count / 2 + n, DownloadModle.List_CLD_UD_HD[DownloadModle.List_CLD_UD_HD.Count / 2 + n], DownloadModle.List_CLD_HD_HD[DownloadModle.List_CLD_UD_HD.Count / 2 + n], ref gg, n * w, h, w, h);
                //}

                //show buf, then dispose
                myBuffer.Render(e.Graphics);
                gg.Dispose();
                myBuffer.Dispose();
            }
            catch
            { }
        }
    }
}
