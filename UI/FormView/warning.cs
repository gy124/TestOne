using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class warning : Form
    {
        public warning()
        {
            InitializeComponent();
            btn_cancle.Text = "取消";
            btn_ok.Text = "确定";
            btn_cancle.Visible = false;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lb_msg_Click(object sender, EventArgs e)
        {

        }
    }
}
