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
    public partial class XYZU : UserControl
    {
        [Browsable(true)]
        [Description("DecimalPlaces")]
        public String DecimalPlaces
        {
            get { return string.Format("{0},{1},{2},{3}", nud_x.DecimalPlaces, nud_y.DecimalPlaces, nud_z.DecimalPlaces, nud_u.DecimalPlaces); }
            set
            {
                string[] str = value.Split(',');
                nud_x.DecimalPlaces = Convert.ToInt16(str[0]);
                nud_y.DecimalPlaces = Convert.ToInt16(str[1]);
                nud_z.DecimalPlaces = Convert.ToInt16(str[2]);
                nud_u.DecimalPlaces = Convert.ToInt16(str[3]);
            }
        }

        [Browsable(true)]
        [Description("XYZUVisible")]
        public String XYZUVisible
        {
            get { return string.Format("{0},{1},{2},{3}", nud_x.Visible, nud_y.Visible, nud_z.Visible, nud_u.Visible); }
            set
            {
                string[] str = value.Split(',');
                nud_x.Visible = Convert.ToBoolean(str[0]);
                nud_y.Visible = Convert.ToBoolean(str[1]);
                nud_z.Visible = Convert.ToBoolean(str[2]);
                nud_u.Visible = Convert.ToBoolean(str[3]);

                lb_x.Visible = nud_x.Visible;
                lb_y.Visible = nud_y.Visible;
                lb_z.Visible = nud_z.Visible;
                lb_u.Visible = nud_u.Visible;

                //重新排位
                int w = 2;
                int x = this.Margin.Left;
                if (nud_x.Visible) { nud_x.Left = x; lb_x.Left = x; x += nud_x.Width + w; }
                if (nud_y.Visible) { nud_y.Left = x; lb_y.Left = x; x += nud_x.Width + w; }
                if (nud_z.Visible) { nud_z.Left = x; lb_z.Left = x; x += nud_x.Width + w; }
                if (nud_u.Visible) { nud_u.Left = x; lb_u.Left = x; x += nud_x.Width + w; }
                //this.Width = x;
            }
        }

        [Browsable(true)]
        [Description("XMinMax")]
        public String XMaxMin
        {
            get { return nud_x.Minimum+","+nud_x.Maximum; }
            set
            {
                string[] str = value.Split(',');
                nud_x.Minimum = Convert.ToInt16(str[0]);
                nud_x.Maximum = Convert.ToInt16(str[1]);
            }
        }

        [Browsable(true)]
        [Description("YMinMax")]
        public String YMaxMin
        {
            get { return nud_y.Minimum + "," + nud_y.Maximum; }
            set
            {
                string[] str = value.Split(',');
                nud_y.Minimum = Convert.ToInt16(str[0]);
                nud_y.Maximum = Convert.ToInt16(str[1]);
            }
        }

        [Browsable(true)]
        [Description("ZMinMax")]
        public String ZMaxMin
        {
            get { return nud_z.Minimum + "," + nud_z.Maximum; }
            set
            {
                string[] str = value.Split(',');
                nud_z.Minimum = Convert.ToInt16(str[0]);
                nud_z.Maximum = Convert.ToInt16(str[1]);
            }
        }

        [Browsable(true)]
        [Description("UMinMax")]
        public String UMaxMin
        {
            get { return nud_u.Minimum + "," + nud_u.Maximum; }
            set
            {
                string[] str = value.Split(',');
                nud_u.Minimum = Convert.ToInt16(str[0]);
                nud_u.Maximum = Convert.ToInt16(str[1]);
            }
        }

        [Browsable(true)]
        [Description("X")]
        public double X
        {
            get { return (double)nud_x.Value; }
            set { nud_x.Value = (decimal)value; }
        }

        [Browsable(true)]
        [Description("Y")]
        public double Y
        {
            get { return (double)nud_y.Value; }
            set { nud_y.Value = (decimal)value; }
        }

        [Browsable(true)]
        [Description("Z")]
        public double Z
        {
            get { return (double)nud_z.Value; }
            set { nud_z.Value = (decimal)value; }
        }

        [Browsable(true)]
        [Description("U")]
        public double U
        {
            get { return (double)nud_u.Value; }
            set { nud_u.Value = (decimal)value; }
        }

        public XYZU()
        {
            InitializeComponent();
        }

        public ST_XYZA Value
        {
            get
            {
                return new ST_XYZA((double)nud_x.Value, (double)nud_y.Value, (double)nud_z.Value, (double)nud_u.Value) ;
            }
            set
            {
                try
                {
                    nud_x.Value = (decimal)value.x;
                    nud_y.Value = (decimal)value.y;
                    nud_z.Value = (decimal)value.z;
                    nud_u.Value = (decimal)value.a;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("{0}赋值异常,{1}", this.Name, ex.Message)); 
                }
            }
        }
    }
}
