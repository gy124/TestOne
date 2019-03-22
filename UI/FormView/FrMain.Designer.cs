namespace UI
{
    partial class FrMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrMain));
            this.tbl_main = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_top_menu = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_Time = new System.Windows.Forms.Label();
            this.lb_Date = new System.Windows.Forms.Label();
            this.rbtn_rst = new System.Windows.Forms.RadioButton();
            this.img_list_main = new System.Windows.Forms.ImageList(this.components);
            this.rbtn_user = new System.Windows.Forms.RadioButton();
            this.btn_quit = new System.Windows.Forms.Button();
            this.rbtn_sys = new System.Windows.Forms.RadioButton();
            this.rbtn_product = new System.Windows.Forms.RadioButton();
            this.rbtn_run = new System.Windows.Forms.RadioButton();
            this.pnl_sub = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.timer_key = new System.Windows.Forms.Timer(this.components);
            this.axTcpClient_cmd = new SocketHelper.AxTcpClient(this.components);
            this.axTcpClient_status = new SocketHelper.AxTcpClient(this.components);
            this.timer_reconnect = new System.Windows.Forms.Timer(this.components);
            this.tbl_main.SuspendLayout();
            this.pnl_top_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbl_main
            // 
            this.tbl_main.ColumnCount = 1;
            this.tbl_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_main.Controls.Add(this.pnl_top_menu, 0, 0);
            this.tbl_main.Controls.Add(this.pnl_sub, 0, 1);
            this.tbl_main.Controls.Add(this.panel1, 0, 2);
            this.tbl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_main.Location = new System.Drawing.Point(0, 0);
            this.tbl_main.Name = "tbl_main";
            this.tbl_main.RowCount = 3;
            this.tbl_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tbl_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbl_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_main.Size = new System.Drawing.Size(1280, 700);
            this.tbl_main.TabIndex = 0;
            // 
            // pnl_top_menu
            // 
            this.pnl_top_menu.Controls.Add(this.pictureBox1);
            this.pnl_top_menu.Controls.Add(this.label1);
            this.pnl_top_menu.Controls.Add(this.lb_Time);
            this.pnl_top_menu.Controls.Add(this.lb_Date);
            this.pnl_top_menu.Controls.Add(this.rbtn_rst);
            this.pnl_top_menu.Controls.Add(this.rbtn_user);
            this.pnl_top_menu.Controls.Add(this.btn_quit);
            this.pnl_top_menu.Controls.Add(this.rbtn_sys);
            this.pnl_top_menu.Controls.Add(this.rbtn_product);
            this.pnl_top_menu.Controls.Add(this.rbtn_run);
            this.pnl_top_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_top_menu.Location = new System.Drawing.Point(3, 3);
            this.pnl_top_menu.Name = "pnl_top_menu";
            this.pnl_top_menu.Size = new System.Drawing.Size(1274, 74);
            this.pnl_top_menu.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(976, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 68;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(1046, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 67;
            this.label1.Text = "扣料  V2.0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Time
            // 
            this.lb_Time.BackColor = System.Drawing.Color.Transparent;
            this.lb_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Time.ForeColor = System.Drawing.Color.Gray;
            this.lb_Time.Location = new System.Drawing.Point(1165, 34);
            this.lb_Time.Name = "lb_Time";
            this.lb_Time.Size = new System.Drawing.Size(91, 26);
            this.lb_Time.TabIndex = 65;
            this.lb_Time.Text = "21:09";
            // 
            // lb_Date
            // 
            this.lb_Date.BackColor = System.Drawing.Color.Transparent;
            this.lb_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Date.ForeColor = System.Drawing.Color.Gray;
            this.lb_Date.Location = new System.Drawing.Point(1165, 6);
            this.lb_Date.Name = "lb_Date";
            this.lb_Date.Size = new System.Drawing.Size(106, 61);
            this.lb_Date.TabIndex = 66;
            this.lb_Date.Text = "2019-3-23\r\n";
            // 
            // rbtn_rst
            // 
            this.rbtn_rst.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtn_rst.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_rst.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.rbtn_rst.FlatAppearance.BorderSize = 0;
            this.rbtn_rst.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.rbtn_rst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.rbtn_rst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn_rst.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtn_rst.ForeColor = System.Drawing.Color.Gray;
            this.rbtn_rst.ImageIndex = 0;
            this.rbtn_rst.ImageList = this.img_list_main;
            this.rbtn_rst.Location = new System.Drawing.Point(6, 0);
            this.rbtn_rst.Name = "rbtn_rst";
            this.rbtn_rst.Size = new System.Drawing.Size(125, 71);
            this.rbtn_rst.TabIndex = 48;
            this.rbtn_rst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtn_rst.UseVisualStyleBackColor = false;
            this.rbtn_rst.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // img_list_main
            // 
            this.img_list_main.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_list_main.ImageStream")));
            this.img_list_main.TransparentColor = System.Drawing.Color.Transparent;
            this.img_list_main.Images.SetKeyName(0, "home.ico");
            this.img_list_main.Images.SetKeyName(1, "product.ico");
            this.img_list_main.Images.SetKeyName(2, "quit..ico");
            this.img_list_main.Images.SetKeyName(3, "quit2.ico");
            this.img_list_main.Images.SetKeyName(4, "set.ico");
            this.img_list_main.Images.SetKeyName(5, "user.ico");
            // 
            // rbtn_user
            // 
            this.rbtn_user.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtn_user.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_user.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.rbtn_user.FlatAppearance.BorderSize = 0;
            this.rbtn_user.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.rbtn_user.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.rbtn_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtn_user.ForeColor = System.Drawing.Color.Gray;
            this.rbtn_user.ImageIndex = 5;
            this.rbtn_user.ImageList = this.img_list_main;
            this.rbtn_user.Location = new System.Drawing.Point(510, 0);
            this.rbtn_user.Name = "rbtn_user";
            this.rbtn_user.Size = new System.Drawing.Size(125, 71);
            this.rbtn_user.TabIndex = 47;
            this.rbtn_user.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtn_user.UseVisualStyleBackColor = false;
            this.rbtn_user.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // btn_quit
            // 
            this.btn_quit.BackColor = System.Drawing.Color.Transparent;
            this.btn_quit.FlatAppearance.BorderSize = 0;
            this.btn_quit.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.btn_quit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_quit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_quit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_quit.ForeColor = System.Drawing.Color.Gray;
            this.btn_quit.ImageIndex = 2;
            this.btn_quit.ImageList = this.img_list_main;
            this.btn_quit.Location = new System.Drawing.Point(636, 0);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(125, 71);
            this.btn_quit.TabIndex = 46;
            this.btn_quit.UseVisualStyleBackColor = false;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // rbtn_sys
            // 
            this.rbtn_sys.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtn_sys.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_sys.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.rbtn_sys.FlatAppearance.BorderSize = 0;
            this.rbtn_sys.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.rbtn_sys.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.rbtn_sys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn_sys.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtn_sys.ForeColor = System.Drawing.Color.Gray;
            this.rbtn_sys.ImageIndex = 4;
            this.rbtn_sys.ImageList = this.img_list_main;
            this.rbtn_sys.Location = new System.Drawing.Point(384, 0);
            this.rbtn_sys.Name = "rbtn_sys";
            this.rbtn_sys.Size = new System.Drawing.Size(125, 71);
            this.rbtn_sys.TabIndex = 44;
            this.rbtn_sys.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtn_sys.UseVisualStyleBackColor = false;
            this.rbtn_sys.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // rbtn_product
            // 
            this.rbtn_product.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtn_product.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_product.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.rbtn_product.FlatAppearance.BorderSize = 0;
            this.rbtn_product.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.rbtn_product.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.rbtn_product.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn_product.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtn_product.ForeColor = System.Drawing.Color.Gray;
            this.rbtn_product.ImageIndex = 1;
            this.rbtn_product.ImageList = this.img_list_main;
            this.rbtn_product.Location = new System.Drawing.Point(258, 0);
            this.rbtn_product.Name = "rbtn_product";
            this.rbtn_product.Size = new System.Drawing.Size(125, 71);
            this.rbtn_product.TabIndex = 43;
            this.rbtn_product.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtn_product.UseVisualStyleBackColor = false;
            this.rbtn_product.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // rbtn_run
            // 
            this.rbtn_run.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtn_run.BackColor = System.Drawing.Color.DarkGray;
            this.rbtn_run.Checked = true;
            this.rbtn_run.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.rbtn_run.FlatAppearance.BorderSize = 0;
            this.rbtn_run.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.rbtn_run.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.rbtn_run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn_run.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtn_run.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rbtn_run.ImageIndex = 3;
            this.rbtn_run.ImageList = this.img_list_main;
            this.rbtn_run.Location = new System.Drawing.Point(132, 0);
            this.rbtn_run.Name = "rbtn_run";
            this.rbtn_run.Size = new System.Drawing.Size(125, 71);
            this.rbtn_run.TabIndex = 42;
            this.rbtn_run.TabStop = true;
            this.rbtn_run.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtn_run.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtn_run.UseVisualStyleBackColor = false;
            this.rbtn_run.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // pnl_sub
            // 
            this.pnl_sub.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_sub.Location = new System.Drawing.Point(3, 83);
            this.pnl_sub.Name = "pnl_sub";
            this.pnl_sub.Size = new System.Drawing.Size(1274, 600);
            this.pnl_sub.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 689);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1274, 14);
            this.panel1.TabIndex = 2;
            // 
            // timer_update
            // 
            this.timer_update.Enabled = true;
            this.timer_update.Interval = 1000;
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // timer_key
            // 
            this.timer_key.Tick += new System.EventHandler(this.timer_key_Tick);
            // 
            // axTcpClient_cmd
            // 
            this.axTcpClient_cmd.Isclosed = false;
            this.axTcpClient_cmd.IsStartTcpthreading = false;
            this.axTcpClient_cmd.Receivestr = null;
            this.axTcpClient_cmd.ReConectedCount = 0;
            this.axTcpClient_cmd.ReConnectionTime = 3000;
            this.axTcpClient_cmd.ServerIp = null;
            this.axTcpClient_cmd.ServerPort = 0;
            this.axTcpClient_cmd.Tcpclient = null;
            this.axTcpClient_cmd.Tcpthread = null;
            this.axTcpClient_cmd.OnReceviceByte += new SocketHelper.AxTcpClient.ReceviceByteEventHandler(this.axTcpClient_cmd_OnReceviceByte);
            // 
            // axTcpClient_status
            // 
            this.axTcpClient_status.Isclosed = false;
            this.axTcpClient_status.IsStartTcpthreading = false;
            this.axTcpClient_status.Receivestr = null;
            this.axTcpClient_status.ReConectedCount = 0;
            this.axTcpClient_status.ReConnectionTime = 3000;
            this.axTcpClient_status.ServerIp = null;
            this.axTcpClient_status.ServerPort = 0;
            this.axTcpClient_status.Tcpclient = null;
            this.axTcpClient_status.Tcpthread = null;
            // 
            // timer_reconnect
            // 
            this.timer_reconnect.Interval = 5000;
            this.timer_reconnect.Tick += new System.EventHandler(this.timer_reconnect_Tick);
            // 
            // FrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 700);
            this.Controls.Add(this.tbl_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrMain_FormClosing);
            this.Load += new System.EventHandler(this.FrMain_Load);
            this.tbl_main.ResumeLayout(false);
            this.pnl_top_menu.ResumeLayout(false);
            this.pnl_top_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_main;
        private System.Windows.Forms.Panel pnl_sub;
        private System.Windows.Forms.ImageList img_list_main;
        private System.Windows.Forms.Panel pnl_top_menu;
        private System.Windows.Forms.RadioButton rbtn_rst;
        private System.Windows.Forms.RadioButton rbtn_user;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.RadioButton rbtn_sys;
        public System.Windows.Forms.RadioButton rbtn_product;
        public System.Windows.Forms.RadioButton rbtn_run;
        private System.Windows.Forms.Label lb_Time;
        private System.Windows.Forms.Label lb_Date;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.Timer timer_key;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private SocketHelper.AxTcpClient axTcpClient_status;
        private System.Windows.Forms.Timer timer_reconnect;
        public SocketHelper.AxTcpClient axTcpClient_cmd;
        private System.Windows.Forms.Panel panel1;
    }
}

