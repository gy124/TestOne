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

namespace clasp
{
    public partial class FrRun : Form
    {
        public bool bupdate;
        public FrRun()
        {
            InitializeComponent();
        }

        private void FrRun_Load(object sender, EventArgs e)
        {
            VAR.sys_inf.Init(lb_war_inf, MT.GPIO_OUT_ALM_RED, MT.GPIO_OUT_ALM_GREEN, MT.GPIO_OUT_ALM_YELLOW, MT.GPIO_OUT_ALM_BEEPER, VAR.gsys_set.beep_tmr);//lb_war_inf
            VAR.msg.StartUpdate(dgv_msg);
        }
    }
}
