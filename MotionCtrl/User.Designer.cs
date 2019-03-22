namespace MotionCtrl
{
    partial class User
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_log = new System.Windows.Forms.Panel();
            this.btn_manager = new System.Windows.Forms.Button();
            this.lb_log_inf = new System.Windows.Forms.Label();
            this.tb_pw = new System.Windows.Forms.TextBox();
            this.cb_user = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_logout = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.pnl_manager = new System.Windows.Forms.Panel();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_dele = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pms = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.pnl_log.SuspendLayout();
            this.pnl_manager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_log
            // 
            this.pnl_log.Controls.Add(this.btn_manager);
            this.pnl_log.Controls.Add(this.lb_log_inf);
            this.pnl_log.Controls.Add(this.tb_pw);
            this.pnl_log.Controls.Add(this.cb_user);
            this.pnl_log.Controls.Add(this.label2);
            this.pnl_log.Controls.Add(this.label1);
            this.pnl_log.Controls.Add(this.btn_logout);
            this.pnl_log.Controls.Add(this.btn_login);
            this.pnl_log.Location = new System.Drawing.Point(3, 3);
            this.pnl_log.Name = "pnl_log";
            this.pnl_log.Size = new System.Drawing.Size(304, 307);
            this.pnl_log.TabIndex = 1;
            // 
            // btn_manager
            // 
            this.btn_manager.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_manager.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_manager.Location = new System.Drawing.Point(169, 200);
            this.btn_manager.Name = "btn_manager";
            this.btn_manager.Size = new System.Drawing.Size(85, 60);
            this.btn_manager.TabIndex = 147;
            this.btn_manager.Text = "管理";
            this.btn_manager.UseVisualStyleBackColor = false;
            this.btn_manager.Click += new System.EventHandler(this.btn_manager_Click);
            // 
            // lb_log_inf
            // 
            this.lb_log_inf.AutoSize = true;
            this.lb_log_inf.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_log_inf.Location = new System.Drawing.Point(24, 274);
            this.lb_log_inf.Name = "lb_log_inf";
            this.lb_log_inf.Size = new System.Drawing.Size(267, 24);
            this.lb_log_inf.TabIndex = 146;
            this.lb_log_inf.Text = "登陆成功，当前用户为 管理员!";
            // 
            // tb_pw
            // 
            this.tb_pw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tb_pw.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_pw.Location = new System.Drawing.Point(78, 66);
            this.tb_pw.MaxLength = 8;
            this.tb_pw.Name = "tb_pw";
            this.tb_pw.PasswordChar = '*';
            this.tb_pw.Size = new System.Drawing.Size(178, 38);
            this.tb_pw.TabIndex = 141;
            // 
            // cb_user
            // 
            this.cb_user.BackColor = System.Drawing.Color.Silver;
            this.cb_user.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_user.FormattingEnabled = true;
            this.cb_user.Items.AddRange(new object[] {
            "作业员",
            "工程师",
            "管理员",
            "工厂设定"});
            this.cb_user.Location = new System.Drawing.Point(78, 13);
            this.cb_user.Name = "cb_user";
            this.cb_user.Size = new System.Drawing.Size(178, 39);
            this.cb_user.TabIndex = 140;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 24);
            this.label2.TabIndex = 145;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 24);
            this.label1.TabIndex = 144;
            this.label1.Text = "用户：";
            // 
            // btn_logout
            // 
            this.btn_logout.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_logout.Location = new System.Drawing.Point(78, 200);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(85, 60);
            this.btn_logout.TabIndex = 143;
            this.btn_logout.Text = "注销";
            this.btn_logout.UseVisualStyleBackColor = false;
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_login.Location = new System.Drawing.Point(77, 122);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(178, 60);
            this.btn_login.TabIndex = 142;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = false;
            // 
            // pnl_manager
            // 
            this.pnl_manager.Controls.Add(this.btn_save);
            this.pnl_manager.Controls.Add(this.btn_dele);
            this.pnl_manager.Controls.Add(this.btn_add);
            this.pnl_manager.Controls.Add(this.dgv);
            this.pnl_manager.Location = new System.Drawing.Point(310, 3);
            this.pnl_manager.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnl_manager.Name = "pnl_manager";
            this.pnl_manager.Size = new System.Drawing.Size(349, 307);
            this.pnl_manager.TabIndex = 2;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(236, 265);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(110, 42);
            this.btn_save.TabIndex = 145;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_dele
            // 
            this.btn_dele.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_dele.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_dele.Location = new System.Drawing.Point(119, 265);
            this.btn_dele.Name = "btn_dele";
            this.btn_dele.Size = new System.Drawing.Size(110, 42);
            this.btn_dele.TabIndex = 144;
            this.btn_dele.Text = "删除";
            this.btn_dele.UseVisualStyleBackColor = false;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.Location = new System.Drawing.Point(3, 265);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(110, 42);
            this.btn_add.TabIndex = 143;
            this.btn_add.Text = "增加";
            this.btn_add.UseVisualStyleBackColor = false;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.name,
            this.password,
            this.pms});
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Location = new System.Drawing.Point(3, 13);
            this.dgv.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 27;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(343, 247);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            // 
            // num
            // 
            this.num.FillWeight = 1F;
            this.num.Frozen = true;
            this.num.HeaderText = "编号";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.Width = 60;
            // 
            // name
            // 
            this.name.Frozen = true;
            this.name.HeaderText = "用户名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // password
            // 
            this.password.Frozen = true;
            this.password.HeaderText = "密码";
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Width = 80;
            // 
            // pms
            // 
            this.pms.Frozen = true;
            this.pms.HeaderText = "权限";
            this.pms.Items.AddRange(new object[] {
            "超级管理员",
            "管理员",
            "工程师",
            "作业员",
            "无权限"});
            this.pms.Name = "pms";
            this.pms.ReadOnly = true;
            this.pms.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pms.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_manager);
            this.Controls.Add(this.pnl_log);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "User";
            this.Size = new System.Drawing.Size(659, 317);
            this.Load += new System.EventHandler(this.User_Load);
            this.pnl_log.ResumeLayout(false);
            this.pnl_log.PerformLayout();
            this.pnl_manager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_log;
        private System.Windows.Forms.Label lb_log_inf;
        private System.Windows.Forms.TextBox tb_pw;
        public System.Windows.Forms.ComboBox cb_user;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_logout;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Panel pnl_manager;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_dele;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_manager;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewComboBoxColumn pms;
    }
}
