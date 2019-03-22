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

namespace UI
{
    public partial class FrUser : Form
    {
        public FrUser()
        {
            InitializeComponent();
        }

        private void FrUser_Load(object sender, EventArgs e)
        {

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
    }
}
