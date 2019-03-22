namespace UI
{
    partial class FrRun
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrRun));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayout2 = new WT.ExecuteUI.TableLayout();
            this.tableLayout1 = new WT.ExecuteUI.TableLayout();
            this.dvg_msg = new System.Windows.Forms.DataGridView();
            this.dgv_vs = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ax_pable = new MotionCtrl.AXIS_Panle();
            this.cTabControl1 = new CTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bt_mark_cam2 = new System.Windows.Forms.Button();
            this.bt_mark_cam1 = new System.Windows.Forms.Button();
            this.bt_cam2_get = new System.Windows.Forms.Button();
            this.bt_open_cam = new System.Windows.Forms.Button();
            this.bt_close_cam = new System.Windows.Forms.Button();
            this.bt_cam1_get = new System.Windows.Forms.Button();
            this.axGeneralVisionControl1 = new AxGeneralVisionControlLib.AxGeneralVisionControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayout3 = new WT.ExecuteUI.TableLayout();
            this.pnl_war_inf = new System.Windows.Forms.Panel();
            this.lb_war_inf = new System.Windows.Forms.Label();
            this.cb_product_list = new System.Windows.Forms.ComboBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox_null_run = new System.Windows.Forms.CheckBox();
            this.timer_500ms = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.tableLayout2.SuspendLayout();
            this.tableLayout1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvg_msg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vs)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.cTabControl1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axGeneralVisionControl1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayout3.SuspendLayout();
            this.pnl_war_inf.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.tableLayout2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1135, 788);
            this.panel2.TabIndex = 1;
            // 
            // tableLayout2
            // 
            this.tableLayout2.BorderColor = System.Drawing.Color.Black;
            this.tableLayout2.ColumnCount = 3;
            this.tableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 750F));
            this.tableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout2.Controls.Add(this.tableLayout1, 2, 0);
            this.tableLayout2.Controls.Add(this.panel3, 1, 0);
            this.tableLayout2.Controls.Add(this.panel1, 0, 0);
            this.tableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout2.Location = new System.Drawing.Point(0, 0);
            this.tableLayout2.Name = "tableLayout2";
            this.tableLayout2.RowCount = 2;
            this.tableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 580F));
            this.tableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout2.Size = new System.Drawing.Size(1135, 788);
            this.tableLayout2.TabIndex = 10;
            // 
            // tableLayout1
            // 
            this.tableLayout1.BorderColor = System.Drawing.Color.Black;
            this.tableLayout1.ColumnCount = 1;
            this.tableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout1.Controls.Add(this.dvg_msg, 0, 2);
            this.tableLayout1.Controls.Add(this.dgv_vs, 0, 1);
            this.tableLayout1.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout1.Location = new System.Drawing.Point(903, 3);
            this.tableLayout1.Name = "tableLayout1";
            this.tableLayout1.RowCount = 3;
            this.tableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayout1.Size = new System.Drawing.Size(229, 574);
            this.tableLayout1.TabIndex = 19;
            // 
            // dvg_msg
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvg_msg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvg_msg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvg_msg.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvg_msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvg_msg.Location = new System.Drawing.Point(3, 385);
            this.dvg_msg.Name = "dvg_msg";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvg_msg.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvg_msg.RowTemplate.Height = 23;
            this.dvg_msg.Size = new System.Drawing.Size(223, 186);
            this.dvg_msg.TabIndex = 11;
            // 
            // dgv_vs
            // 
            this.dgv_vs.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_vs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_vs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_vs.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_vs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_vs.Location = new System.Drawing.Point(3, 194);
            this.dgv_vs.Name = "dgv_vs";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_vs.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_vs.RowTemplate.Height = 23;
            this.dgv_vs.Size = new System.Drawing.Size(223, 185);
            this.dgv_vs.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.ax_pable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cTabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(223, 185);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // ax_pable
            // 
            this.ax_pable.Dock = System.Windows.Forms.DockStyle.Left;
            this.ax_pable.Location = new System.Drawing.Point(3, 3);
            this.ax_pable.Name = "ax_pable";
            this.ax_pable.Size = new System.Drawing.Size(217, 149);
            this.ax_pable.TabIndex = 0;
            // 
            // cTabControl1
            // 
            this.cTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cTabControl1.BorderColor = System.Drawing.Color.White;
            this.cTabControl1.Controls.Add(this.tabPage1);
            this.cTabControl1.Controls.Add(this.tabPage2);
            this.cTabControl1.HeaderBackColor = System.Drawing.Color.White;
            this.cTabControl1.HeadSelectedBackColor = System.Drawing.Color.White;
            this.cTabControl1.HeadSelectedBorderColor = System.Drawing.Color.White;
            this.cTabControl1.Location = new System.Drawing.Point(3, 158);
            this.cTabControl1.Name = "cTabControl1";
            this.cTabControl1.RightToLeftLayout = true;
            this.cTabControl1.SelectedIndex = 0;
            this.cTabControl1.Size = new System.Drawing.Size(214, 24);
            this.cTabControl1.TabIndex = 1;
            this.cTabControl1.SelectedIndexChanged += new System.EventHandler(this.cTabControl1_SelectedIndexChanged_1);
            this.cTabControl1.TabIndexChanged += new System.EventHandler(this.cTabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(206, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "取料";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(206, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "上料";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bt_mark_cam2);
            this.panel3.Controls.Add(this.bt_mark_cam1);
            this.panel3.Controls.Add(this.bt_cam2_get);
            this.panel3.Controls.Add(this.bt_open_cam);
            this.panel3.Controls.Add(this.bt_close_cam);
            this.panel3.Controls.Add(this.bt_cam1_get);
            this.panel3.Controls.Add(this.axGeneralVisionControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(153, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(744, 574);
            this.panel3.TabIndex = 18;
            // 
            // bt_mark_cam2
            // 
            this.bt_mark_cam2.Location = new System.Drawing.Point(674, 489);
            this.bt_mark_cam2.Name = "bt_mark_cam2";
            this.bt_mark_cam2.Size = new System.Drawing.Size(73, 60);
            this.bt_mark_cam2.TabIndex = 19;
            this.bt_mark_cam2.Text = "相机标定2";
            this.bt_mark_cam2.UseVisualStyleBackColor = true;
            this.bt_mark_cam2.Click += new System.EventHandler(this.bt_mark_cam2_Click);
            // 
            // bt_mark_cam1
            // 
            this.bt_mark_cam1.Location = new System.Drawing.Point(600, 489);
            this.bt_mark_cam1.Name = "bt_mark_cam1";
            this.bt_mark_cam1.Size = new System.Drawing.Size(68, 61);
            this.bt_mark_cam1.TabIndex = 18;
            this.bt_mark_cam1.Text = "相机标定1";
            this.bt_mark_cam1.UseVisualStyleBackColor = true;
            this.bt_mark_cam1.Click += new System.EventHandler(this.bt_mark_cam1_Click);
            // 
            // bt_cam2_get
            // 
            this.bt_cam2_get.Location = new System.Drawing.Point(519, 489);
            this.bt_cam2_get.Name = "bt_cam2_get";
            this.bt_cam2_get.Size = new System.Drawing.Size(75, 61);
            this.bt_cam2_get.TabIndex = 17;
            this.bt_cam2_get.Text = "相机2获取";
            this.bt_cam2_get.UseVisualStyleBackColor = true;
            this.bt_cam2_get.Click += new System.EventHandler(this.button3_Click);
            // 
            // bt_open_cam
            // 
            this.bt_open_cam.Location = new System.Drawing.Point(262, 488);
            this.bt_open_cam.Name = "bt_open_cam";
            this.bt_open_cam.Size = new System.Drawing.Size(91, 61);
            this.bt_open_cam.TabIndex = 15;
            this.bt_open_cam.Text = "打开相机";
            this.bt_open_cam.UseVisualStyleBackColor = true;
            this.bt_open_cam.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // bt_close_cam
            // 
            this.bt_close_cam.Location = new System.Drawing.Point(359, 489);
            this.bt_close_cam.Name = "bt_close_cam";
            this.bt_close_cam.Size = new System.Drawing.Size(73, 60);
            this.bt_close_cam.TabIndex = 16;
            this.bt_close_cam.Text = "关闭相机";
            this.bt_close_cam.UseVisualStyleBackColor = true;
            this.bt_close_cam.Click += new System.EventHandler(this.bt_close_cam_Click);
            // 
            // bt_cam1_get
            // 
            this.bt_cam1_get.Location = new System.Drawing.Point(438, 490);
            this.bt_cam1_get.Name = "bt_cam1_get";
            this.bt_cam1_get.Size = new System.Drawing.Size(75, 60);
            this.bt_cam1_get.TabIndex = 14;
            this.bt_cam1_get.Text = "相机1获取";
            this.bt_cam1_get.UseVisualStyleBackColor = true;
            this.bt_cam1_get.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // axGeneralVisionControl1
            // 
            this.axGeneralVisionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGeneralVisionControl1.Enabled = true;
            this.axGeneralVisionControl1.Location = new System.Drawing.Point(0, 0);
            this.axGeneralVisionControl1.Name = "axGeneralVisionControl1";
            this.axGeneralVisionControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGeneralVisionControl1.OcxState")));
            this.axGeneralVisionControl1.Size = new System.Drawing.Size(744, 574);
            this.axGeneralVisionControl1.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayout3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 574);
            this.panel1.TabIndex = 0;
            // 
            // tableLayout3
            // 
            this.tableLayout3.BorderColor = System.Drawing.Color.Black;
            this.tableLayout3.ColumnCount = 1;
            this.tableLayout3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout3.Controls.Add(this.pnl_war_inf, 0, 0);
            this.tableLayout3.Controls.Add(this.cb_product_list, 0, 1);
            this.tableLayout3.Controls.Add(this.btn_run, 0, 2);
            this.tableLayout3.Controls.Add(this.btn_stop, 0, 3);
            this.tableLayout3.Controls.Add(this.button1, 0, 4);
            this.tableLayout3.Controls.Add(this.checkBox_null_run, 0, 5);
            this.tableLayout3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout3.Location = new System.Drawing.Point(0, 0);
            this.tableLayout3.Name = "tableLayout3";
            this.tableLayout3.RowCount = 7;
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28163F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28163F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28735F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28735F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28735F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28735F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28735F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout3.Size = new System.Drawing.Size(144, 574);
            this.tableLayout3.TabIndex = 7;
            // 
            // pnl_war_inf
            // 
            this.pnl_war_inf.BackColor = System.Drawing.Color.Gainsboro;
            this.pnl_war_inf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_war_inf.Controls.Add(this.lb_war_inf);
            this.pnl_war_inf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_war_inf.Location = new System.Drawing.Point(3, 3);
            this.pnl_war_inf.Name = "pnl_war_inf";
            this.pnl_war_inf.Size = new System.Drawing.Size(138, 75);
            this.pnl_war_inf.TabIndex = 6;
            // 
            // lb_war_inf
            // 
            this.lb_war_inf.BackColor = System.Drawing.Color.Gold;
            this.lb_war_inf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_war_inf.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_war_inf.Location = new System.Drawing.Point(0, 0);
            this.lb_war_inf.Name = "lb_war_inf";
            this.lb_war_inf.Size = new System.Drawing.Size(134, 71);
            this.lb_war_inf.TabIndex = 0;
            this.lb_war_inf.Text = "等待复位";
            this.lb_war_inf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_product_list
            // 
            this.cb_product_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_product_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_product_list.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_product_list.FormattingEnabled = true;
            this.cb_product_list.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "45",
            "5"});
            this.cb_product_list.Location = new System.Drawing.Point(3, 84);
            this.cb_product_list.Name = "cb_product_list";
            this.cb_product_list.Size = new System.Drawing.Size(138, 32);
            this.cb_product_list.TabIndex = 2;
            // 
            // btn_run
            // 
            this.btn_run.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_run.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_run.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_run.Location = new System.Drawing.Point(3, 165);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(138, 76);
            this.btn_run.TabIndex = 0;
            this.btn_run.Text = "运行";
            this.btn_run.UseVisualStyleBackColor = false;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_stop.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stop.Location = new System.Drawing.Point(3, 247);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(138, 76);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(3, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 76);
            this.button1.TabIndex = 7;
            this.button1.Text = "暂停";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox_null_run
            // 
            this.checkBox_null_run.AutoSize = true;
            this.checkBox_null_run.Location = new System.Drawing.Point(3, 411);
            this.checkBox_null_run.Name = "checkBox_null_run";
            this.checkBox_null_run.Size = new System.Drawing.Size(60, 16);
            this.checkBox_null_run.TabIndex = 176;
            this.checkBox_null_run.Text = "空运行";
            this.checkBox_null_run.UseVisualStyleBackColor = true;
            this.checkBox_null_run.CheckedChanged += new System.EventHandler(this.checkBox_null_run_CheckedChanged);
            // 
            // timer_500ms
            // 
            this.timer_500ms.Interval = 500;
            this.timer_500ms.Tick += new System.EventHandler(this.timer_500ms_Tick);
            // 
            // FrRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 788);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrRun";
            this.Text = "FrRun";
            this.Load += new System.EventHandler(this.FrRun_Load);
            this.panel2.ResumeLayout(false);
            this.tableLayout2.ResumeLayout(false);
            this.tableLayout1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvg_msg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vs)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.cTabControl1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axGeneralVisionControl1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayout3.ResumeLayout(false);
            this.tableLayout3.PerformLayout();
            this.pnl_war_inf.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_war_inf;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label lb_war_inf;
        public System.Windows.Forms.ComboBox cb_product_list;
        //private WorkStation ws3;
        public System.Windows.Forms.Timer timer_500ms;
        private System.Windows.Forms.DataGridView dgv_vs;
        private System.Windows.Forms.Button bt_cam1_get;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bt_cam2_get;
        private System.Windows.Forms.Button bt_open_cam;
        private System.Windows.Forms.Button bt_close_cam;
        private System.Windows.Forms.DataGridView dvg_msg;
        private WT.ExecuteUI.TableLayout tableLayout1;
        private WT.ExecuteUI.TableLayout tableLayout3;
        private System.Windows.Forms.Button bt_mark_cam2;
        private System.Windows.Forms.Button bt_mark_cam1;
        private WT.ExecuteUI.TableLayout tableLayout2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MotionCtrl.AXIS_Panle ax_pable;
        private CTabControl cTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private AxGeneralVisionControlLib.AxGeneralVisionControl axGeneralVisionControl1;
        private System.Windows.Forms.CheckBox checkBox_null_run;
    }
}