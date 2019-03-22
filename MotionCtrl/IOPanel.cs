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
    public partial class IOPanel : UserControl
    {
        GPIO gpio_out;
        GPIO gpio_in_sen_up;
        GPIO gpio_in_sen_down;

        public IOPanel()
        {
            InitializeComponent();
            gpio_out = null;
            gpio_in_sen_up = null;
            gpio_in_sen_down = null;
            lbl_on.BackColor = mOUT_Color;
            lbl_sen_up.BackColor = mSEN_Color;
            lbl_sen_down.BackColor = Color.Silver;
            btn_m.SendToBack();
        }

        public void Config(GPIO io_out = null, GPIO io_sen_up = null, GPIO io_sen_down= null)
        {
            gpio_out = io_out;
            gpio_in_sen_up = io_sen_up;
            gpio_in_sen_down = io_sen_down;

            if (gpio_out != null)
            {
                btn_m.Cursor = Cursors.Hand;
                lbl_on.Enabled = true;
                btn_m.Enabled = true;
                btn_m.Text = gpio_out.str_disc;
            }
            else
            {
                btn_m.Cursor = Cursors.Default;
                lbl_on.Enabled = false;
                btn_m.Enabled = false;
                btn_m.Visible = false;
            }

            if (gpio_in_sen_up == null) this.lbl_sen_up.Visible = false;

            if (gpio_in_sen_down == null) this.lbl_sen_down.Visible = false;

        }

        public void Config(Cylinder cd)
        {
            gpio_out = cd.io_out;
            gpio_in_sen_up = cd.io_sen_on;
            gpio_in_sen_down = cd.io_sen_off;

            if (gpio_out != null)
            {
                btn_m.Cursor = Cursors.Hand;
                lbl_on.Enabled = true;
                btn_m.Enabled = true;
                btn_m.Text = gpio_out.str_disc;
            }
            else
            {
                btn_m.Cursor = Cursors.Default;
                lbl_on.Enabled = false;
                btn_m.Enabled = false;
                btn_m.Visible = false;
            }

            if (gpio_in_sen_up == null) lbl_sen_up.Visible = false;

            if (gpio_in_sen_down == null) lbl_sen_down.Visible = false;

        }

        public void UpdateShow()
        {
            if (btn_m.Enabled == false) return;
            if (gpio_out == null && gpio_in_sen_up == null && gpio_in_sen_down == null)
            {
                lbl_sen_down.BackColor = Color.Yellow;
                btn_m.Enabled = false;
                return;
            }

            if (gpio_out != null) lbl_on.BackColor = (gpio_out.isON ? mOUT_Color : Color.Silver);
            if (gpio_in_sen_up != null) lbl_sen_up.BackColor = (gpio_in_sen_up.isON ? mSEN_Color : Color.Silver);
            if (gpio_in_sen_down != null) lbl_sen_down.BackColor = (gpio_in_sen_down.isON ? mSEN_Color : Color.Silver);            
        }


        private void btn_m_Click(object sender, EventArgs e)
        {
            if (gpio_out != null)
            {
                EM_RES ret = gpio_out.Invert();
                if (ret != EM_RES.OK) MessageBox.Show(gpio_out.disc + " IO Operation error！");
                lbl_on.BackColor = (gpio_out.isON ? mOUT_Color : Color.Silver);
            }
        }

        private void lbl_on_Click(object sender, EventArgs e)
        {

        }
    }
}
