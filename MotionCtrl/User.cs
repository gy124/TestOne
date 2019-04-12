using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MotionCtrl
{
    public partial class User : UserControl
    {
        public enum PERMISSION
        {
            None,
            Operator,
            Engineer,
            Admin,
            SuperAdmin
        }
        public struct ST_USER
        {
            public String name;
            public String password;
            public PERMISSION pms;
            public bool bUpdate;
            
            public override string ToString()
            {
                return string.Format("{0},{1},{2}", name, password, Enum.GetName(typeof(PERMISSION), pms));
            }
            public bool FromString(string str)
            {
                if (str.Length < 3) return false;
                str.Trim();
                string[] str_list = str.Split(',');
                if (str_list.Count() != 3) return false;
                name = str_list[0];
                password = str_list[1];
                pms = (PERMISSION)Enum.Parse(typeof(PERMISSION), str_list[2], false);
                return true;
            }
        }

        List<ST_USER> list_user = new List<ST_USER>();
        public ST_USER cur_user = new ST_USER();

        public User()
        {
            InitializeComponent();
        }
        private void FillTable(ST_USER user, int row = -2)
        {
            if (user.name.Length < 3 || user.password.Length < 3) return;
            //if empty or add mode then add
            if (dgv.Rows.Count == 0 || row == -2) row = dgv.Rows.Add();
            //the last row
            else if (row < 0) row = dgv.Rows.Count - 1;

            dgv.Rows[row].Cells[0].Value = row + 1;
            dgv.Rows[row].Cells[1].Value = user.name;
            dgv.Rows[row].Cells[2].Value = user.password;            

            string str = "无权限";
            switch (user.pms)
            {
                case PERMISSION.SuperAdmin:
                    str = "超级管理员";
                    break;
                case PERMISSION.Admin:
                    str = "管理员";
                    break;
                case PERMISSION.Engineer:
                    str = "工程师";
                    break;
                case PERMISSION.Operator:
                    str = "作业员";
                    break;
                case PERMISSION.None:
                    str = "无权限";
                    break;
                default:
                    str = "无权限";
                    break;
            }
            dgv.Rows[row].Cells[3].Value = str;
        }


        public void UpdateShow()
        {
          
            for (int r = 0; r < list_user.Count; r++)
            {
                if(r >= dgv.Rows.Count) FillTable(list_user.ElementAt(r), -2);
                else FillTable(list_user.ElementAt(r), r);
            }
            dgv.Update();
        }

        private void btn_manager_Click(object sender, EventArgs e)
        {
            int min_w = pnl_log.Width + pnl_log.Margin.Left * 2;
            int max_w = pnl_log.Width + pnl_log.Margin.Left * 2 + pnl_manager.Width + pnl_manager.Margin.Left * 2;
            Width = Width == min_w ? max_w : min_w;

            //load data
            if (Width == max_w)
            {
                LoadFromFile();                    
            }
        }
        public bool LoadFromFile(string filename = "\\syscfg\\user.bin")
        {
            List<ST_USER> list_temp = new List<ST_USER>();
            list_temp.Clear();
            //read
            filename = Path.GetFullPath("..") + filename;
            if (File.Exists(filename))
            {
                StreamReader reader1 = new StreamReader(filename, System.Text.Encoding.GetEncoding("UTF-8"));
                while (true)
                {
                    string str = reader1.ReadLine();
                    if (str == null) break;
                    ST_USER user = new ST_USER();
                    if (user.FromString(str)) list_temp.Add(user);
                }
                reader1.Close();
                reader1.Dispose();
            }
            else
            {
                ST_USER user = new ST_USER();
                user.FromString("超级管理员,12345678,SuperAdmin");
                list_temp.Add(user);

                user = new ST_USER();
                user.FromString("管理员,1234567,Admin");
                list_temp.Add(user);

                user = new ST_USER();
                user.FromString("工程师,123456,Engineer");
                list_temp.Add(user);

                user = new ST_USER();
                user.FromString("作业员,123456,Operator");
                list_temp.Add(user);
            }

            if (list_temp.Count == 0) return false;
            list_user = list_temp;
            string cb_user_text=cb_user.Text;
            cb_user_text = cb_user.Text;
            cb_user.Items.Clear();
            foreach (ST_USER user_temp in list_user)
            {
                cb_user.Items.Add(user_temp.name);
                if (cb_user_text ==user_temp.name)
                {
                    cb_user.Text = cb_user_text;
                }
            }
            if (cur_user.name == "超级管理员" && cur_user.pms == PERMISSION.SuperAdmin)
            {
                btn_add.Visible = true;
                btn_dele.Visible = true;
                btn_add.Enabled = true;
                btn_dele.Enabled = true;
            }
            else
            {
                btn_add.Visible = false;
                btn_dele.Visible = false;
                btn_add.Enabled = false;
                btn_dele.Enabled = false;
            }
            UpdateShow();
            return true;
        }
        public bool SaveToFile(string filename = "\\syscfg\\user.bin")
        {
            filename = Path.GetFullPath("..") + filename;
            List<ST_USER> list_temp = new List<ST_USER>();
            list_temp.Clear();
            //检查表格内是否有重复用户名
             foreach (DataGridViewRow v in dgv.Rows)
                {
                    if (v.Cells[1].Value != null)
                    {
                        var count = 0;
                        foreach (DataGridViewRow v2 in dgv.Rows)
                        {
                            if (v2.Cells[1].Value != null)
                            {
                                if (v.Cells[1].Value.ToString().Equals(v2.Cells[1].Value.ToString()))
                                    count++;
                            }
                        }
                        if (count > 1)
                        {
                            MessageBox.Show("用户名有重复，重复的内容是:【" + v.Cells[1].Value + "】!");
                            return false; 
                        }
                    }
                }
    
            //检查表格是否有两个超级管理员
            foreach (DataGridViewRow v in dgv.Rows)
            {
                if (v.Cells[3].Value.ToString().Equals("超级管理员"))
                {
                    if (v.Index!=0)
                    {
                        MessageBox.Show("第一个用户为超级管理员,其它超级管理员降级为管理员!");
                        v.Cells[3].Value = "管理员";
                    }
                }
            }

             foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[1].Value.ToString().Length < 3 || row.Cells[2].Value.ToString().Length < 3) continue;
                ST_USER user = new ST_USER();
                user.name = row.Cells[1].Value.ToString();
                user.password = row.Cells[2].Value.ToString();
                switch (row.Cells[3].Value.ToString())
                {
                    case "超级管理员":
                        user.pms = PERMISSION.SuperAdmin;
                        break;
                    case "管理员":
                        user.pms = PERMISSION.Admin;
                        break;
                    case "工程师":
                        user.pms = PERMISSION.Engineer;
                        break;
                    case "作业员":
                        user.pms = PERMISSION.Operator;
                        break;
                    case "无权限":
                    default:
                        user.pms = PERMISSION.None;
                        break;
                }
                list_temp.Add(user);
            }
            if (list_temp.Count == 0) return false;

            //save
            list_user = list_temp;
            StreamWriter writer1 = new StreamWriter(filename,false, System.Text.Encoding.GetEncoding("UTF-8"));
            foreach (ST_USER user in list_temp)
            {
                writer1.WriteLine(user.ToString());
            }
            writer1.Close();
            writer1.Dispose();
            return true;
        }
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= list_user.Count) return;
          if(list_user[e.RowIndex].pms>cur_user.pms|| (list_user[e.RowIndex].pms==cur_user.pms && list_user[e.RowIndex].name!= cur_user.name))
            {

                if (e.ColumnIndex == 2)
                {
                    if (e.Value != null && e.Value.ToString().Length > 0)
                    {
                        e.Value = new string('*', e.Value.ToString().Length);

                    }
                }
            }
            
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveToFile();
            LoadFromFile();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgv.Rows[e.RowIndex].Cells[1].Value == null) return;
            if (list_user.Count <= e.RowIndex)
            {
                dgv.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                dgv.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                dgv.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                return;
            }
            if (dgv.Rows[e.RowIndex].Cells[1].Value.ToString() == cur_user.name || list_user[e.RowIndex].pms <cur_user.pms)
            {
                if(list_user[e.RowIndex].name == "超级管理员")
                {
                    dgv.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                }               
                else
                {
                    dgv.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                }                
                dgv.Rows[e.RowIndex].Cells[2].ReadOnly = false; 
                dgv.Rows[e.RowIndex].Cells[3].ReadOnly = true;              
            }
            else
            {
                dgv.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                dgv.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                dgv.Rows[e.RowIndex].Cells[3].ReadOnly = true;
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            Width = pnl_log.Width + pnl_log.Margin.Left * 2;
            LoadFromFile();
            
            lb_log_inf.Text = string.Empty;
            lb_grade_inf.Text = string.Empty;
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].SortMode= DataGridViewColumnSortMode.NotSortable;

            }


        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int index=dgv.Rows.Add();
            dgv.Rows[index].Cells[0].Value = (index + 1).ToString();
            dgv.Rows[index].Cells[1].Value = "XXX";
            dgv.Rows[index].Cells[2].Value = "1234";
            dgv.Rows[index].Cells[3].Value = "作业员";
           

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
         
            string str=string.Empty;
            foreach (ST_USER user_temp in list_user)
            {
                if (tb_pw.Text==user_temp.password && cb_user.Text==user_temp.name)
                {
                    cur_user = user_temp;
                    switch (cur_user.pms)
                    {
                        case PERMISSION.SuperAdmin:
                            str = "超级管理员";
                            break;
                        case PERMISSION.Admin:
                            str = "管理员";
                            break;
                        case PERMISSION.Engineer:
                            str = "工程师";
                            break;
                        case PERMISSION.Operator:
                            str = "作业员";
                            break;
                        case PERMISSION.None:
                            str = "无权限";
                            break;
                        default:
                            str = "无权限";
                            break;
                    }
                    cur_user.bUpdate = true;
                    lb_log_inf.Text = cur_user.name+ "登陆成功" ;
                    lb_grade_inf.Text = "用户级别为:" + str;
                    //更新界面
                    if (Width == pnl_log.Width + pnl_log.Margin.Left * 2 + pnl_manager.Width + pnl_manager.Margin.Left * 2)
                    {
                        Width = pnl_log.Width + pnl_log.Margin.Left * 2 ;
                        Width = pnl_log.Width + pnl_log.Margin.Left * 2 + pnl_manager.Width + pnl_manager.Margin.Left * 2;
                    }
                    LoadFromFile();
                    return;
                }
            }
            Width = pnl_log.Width + pnl_log.Margin.Left * 2;
            lb_log_inf.Text = cb_user.Text+ "登陆失败，密码错误!";
            lb_grade_inf.Text = String.Empty;
           
        }

        private void btn_dele_Click(object sender, EventArgs e)
        {
             if(dgv.CurrentRow.Cells[1].Value.ToString()=="超级管理员" && dgv.CurrentRow.Cells[3].Value.ToString()== "超级管理员")
             {               
                MessageBox.Show("超级管理员不能删除!");
                return;
            }
            if(dgv.CurrentRow.Cells[1].Value.ToString()==cur_user.name)
            {
                
                MessageBox.Show("当前用户不能删除!");
                return;
            }
            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            cur_user.bUpdate = true;
            cur_user.name = "作业员";
            cur_user.pms = PERMISSION.None;
            Width = pnl_log.Width + pnl_log.Margin.Left * 2;
            cb_user.Text = string.Empty;
            tb_pw.Text = string.Empty;
            lb_log_inf.Text ="当前用户已注销!";
            lb_grade_inf.Text = string.Empty;

        }
    }
}
