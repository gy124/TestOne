namespace UI
{
    partial class FrProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrProduct));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctb_product = new CTabControl();
            this.tp_file = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numer_tray_cnt = new System.Windows.Forms.NumericUpDown();
            this.button8 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox_product = new System.Windows.Forms.ListBox();
            this.tp_tray = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.axis_panle = new MotionCtrl.AXIS_Panle();
            this.PosTable1 = new MotionCtrl.PosTable();
            this.bt_get_tray_data = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.numberLine = new System.Windows.Forms.NumericUpDown();
            this.numberRow = new System.Windows.Forms.NumericUpDown();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.cTabControl1 = new CTabControl();
            this.ctp_p1 = new System.Windows.Forms.TabPage();
            this.ctp_p2 = new System.Windows.Forms.TabPage();
            this.ctp_p3 = new System.Windows.Forms.TabPage();
            this.ctp_p4 = new System.Windows.Forms.TabPage();
            this.pic_pallet = new System.Windows.Forms.PictureBox();
            this.tp_gd = new System.Windows.Forms.TabPage();
            this.tp_rc = new System.Windows.Forms.TabPage();
            this.tp_vision = new System.Windows.Forms.TabPage();
            this.tp_set = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ctb_product.SuspendLayout();
            this.tp_file.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numer_tray_cnt)).BeginInit();
            this.tp_tray.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberRow)).BeginInit();
            this.cTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_pallet)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1274, 600);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctb_product);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1268, 594);
            this.panel2.TabIndex = 1;
            // 
            // ctb_product
            // 
            this.ctb_product.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.ctb_product.BackColor = System.Drawing.SystemColors.Control;
            this.ctb_product.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ctb_product.Controls.Add(this.tp_file);
            this.ctb_product.Controls.Add(this.tp_tray);
            this.ctb_product.Controls.Add(this.tp_gd);
            this.ctb_product.Controls.Add(this.tp_rc);
            this.ctb_product.Controls.Add(this.tp_vision);
            this.ctb_product.Controls.Add(this.tp_set);
            this.ctb_product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctb_product.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctb_product.HeaderBackColor = System.Drawing.Color.Transparent;
            this.ctb_product.HeadSelectedBorderColor = System.Drawing.Color.Transparent;
            this.ctb_product.ItemSize = new System.Drawing.Size(200, 200);
            this.ctb_product.Location = new System.Drawing.Point(0, 0);
            this.ctb_product.Multiline = true;
            this.ctb_product.Name = "ctb_product";
            this.ctb_product.SelectedIndex = 0;
            this.ctb_product.Size = new System.Drawing.Size(1268, 594);
            this.ctb_product.TabIndex = 2;
            // 
            // tp_file
            // 
            this.tp_file.Controls.Add(this.label1);
            this.tp_file.Controls.Add(this.numer_tray_cnt);
            this.tp_file.Controls.Add(this.button8);
            this.tp_file.Controls.Add(this.label11);
            this.tp_file.Controls.Add(this.textBox1);
            this.tp_file.Controls.Add(this.listBox_product);
            this.tp_file.Location = new System.Drawing.Point(204, 4);
            this.tp_file.Name = "tp_file";
            this.tp_file.Size = new System.Drawing.Size(1060, 586);
            this.tp_file.TabIndex = 3;
            this.tp_file.Text = "产品";
            this.tp_file.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "料盒盘数";
            // 
            // numer_tray_cnt
            // 
            this.numer_tray_cnt.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numer_tray_cnt.Location = new System.Drawing.Point(469, 146);
            this.numer_tray_cnt.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numer_tray_cnt.Name = "numer_tray_cnt";
            this.numer_tray_cnt.Size = new System.Drawing.Size(48, 35);
            this.numer_tray_cnt.TabIndex = 43;
            this.numer_tray_cnt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(379, 233);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(276, 70);
            this.button8.TabIndex = 8;
            this.button8.Text = "切换";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(391, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 2;
            this.label11.Text = "当前产品";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(469, 72);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(201, 32);
            this.textBox1.TabIndex = 1;
            // 
            // listBox_product
            // 
            this.listBox_product.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_product.FormattingEnabled = true;
            this.listBox_product.ItemHeight = 21;
            this.listBox_product.Items.AddRange(new object[] {
            "ABC",
            "CDE"});
            this.listBox_product.Location = new System.Drawing.Point(18, 33);
            this.listBox_product.Name = "listBox_product";
            this.listBox_product.Size = new System.Drawing.Size(355, 466);
            this.listBox_product.TabIndex = 0;
            // 
            // tp_tray
            // 
            this.tp_tray.Controls.Add(this.groupBox2);
            this.tp_tray.Location = new System.Drawing.Point(204, 4);
            this.tp_tray.Name = "tp_tray";
            this.tp_tray.Size = new System.Drawing.Size(1060, 586);
            this.tp_tray.TabIndex = 2;
            this.tp_tray.Text = "料盘";
            this.tp_tray.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.axis_panle);
            this.groupBox2.Controls.Add(this.PosTable1);
            this.groupBox2.Controls.Add(this.bt_get_tray_data);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.numberLine);
            this.groupBox2.Controls.Add(this.numberRow);
            this.groupBox2.Controls.Add(this.radioButton9);
            this.groupBox2.Controls.Add(this.radioButton10);
            this.groupBox2.Controls.Add(this.radioButton11);
            this.groupBox2.Controls.Add(this.cTabControl1);
            this.groupBox2.Controls.Add(this.pic_pallet);
            this.groupBox2.Location = new System.Drawing.Point(14, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1020, 536);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "料盘定义";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "行数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "列数";
            // 
            // axis_panle
            // 
            this.axis_panle.Location = new System.Drawing.Point(289, 109);
            this.axis_panle.Margin = new System.Windows.Forms.Padding(5);
            this.axis_panle.Name = "axis_panle";
            this.axis_panle.Size = new System.Drawing.Size(247, 157);
            this.axis_panle.TabIndex = 39;
            // 
            // PosTable1
            // 
            this.PosTable1.AutoSize = true;
            this.PosTable1.Location = new System.Drawing.Point(25, 313);
            this.PosTable1.Margin = new System.Windows.Forms.Padding(7);
            this.PosTable1.Name = "PosTable1";
            this.PosTable1.Size = new System.Drawing.Size(616, 192);
            this.PosTable1.TabIndex = 38;
            // 
            // bt_get_tray_data
            // 
            this.bt_get_tray_data.Location = new System.Drawing.Point(643, 272);
            this.bt_get_tray_data.Name = "bt_get_tray_data";
            this.bt_get_tray_data.Size = new System.Drawing.Size(86, 41);
            this.bt_get_tray_data.TabIndex = 25;
            this.bt_get_tray_data.Text = "获取";
            this.bt_get_tray_data.UseVisualStyleBackColor = true;
            this.bt_get_tray_data.Click += new System.EventHandler(this.bt_get_tray_data_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(643, 225);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 41);
            this.button4.TabIndex = 24;
            this.button4.Text = "保存";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // numberLine
            // 
            this.numberLine.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numberLine.Location = new System.Drawing.Point(124, 112);
            this.numberLine.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numberLine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberLine.Name = "numberLine";
            this.numberLine.Size = new System.Drawing.Size(47, 35);
            this.numberLine.TabIndex = 15;
            this.numberLine.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});

            // 
            // numberRow
            // 
            this.numberRow.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numberRow.Location = new System.Drawing.Point(29, 171);
            this.numberRow.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numberRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberRow.Name = "numberRow";
            this.numberRow.Size = new System.Drawing.Size(48, 35);
            this.numberRow.TabIndex = 14;
            this.numberRow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // radioButton9
            // 
            this.radioButton9.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton9.Checked = true;
            this.radioButton9.FlatAppearance.CheckedBackColor = System.Drawing.Color.Orange;
            this.radioButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton9.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton9.Location = new System.Drawing.Point(25, 225);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(47, 41);
            this.radioButton9.TabIndex = 7;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "3";
            this.radioButton9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton10
            // 
            this.radioButton10.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton10.Checked = true;
            this.radioButton10.FlatAppearance.CheckedBackColor = System.Drawing.Color.Orange;
            this.radioButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton10.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton10.Location = new System.Drawing.Point(222, 105);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(47, 41);
            this.radioButton10.TabIndex = 6;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "2";
            this.radioButton10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // radioButton11
            // 
            this.radioButton11.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton11.Checked = true;
            this.radioButton11.FlatAppearance.CheckedBackColor = System.Drawing.Color.Orange;
            this.radioButton11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton11.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton11.Location = new System.Drawing.Point(25, 105);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(47, 41);
            this.radioButton11.TabIndex = 5;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "1";
            this.radioButton11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton11.UseVisualStyleBackColor = true;
      
            // 
            // cTabControl1
            // 
            this.cTabControl1.BackColor = System.Drawing.SystemColors.Control;
            this.cTabControl1.BorderColor = System.Drawing.SystemColors.Control;
            this.cTabControl1.Controls.Add(this.ctp_p1);
            this.cTabControl1.Controls.Add(this.ctp_p2);
            this.cTabControl1.Controls.Add(this.ctp_p3);
            this.cTabControl1.Controls.Add(this.ctp_p4);
            this.cTabControl1.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.cTabControl1.ItemSize = new System.Drawing.Size(60, 32);
            this.cTabControl1.Location = new System.Drawing.Point(21, 25);
            this.cTabControl1.Name = "cTabControl1";
            this.cTabControl1.SelectedIndex = 0;
            this.cTabControl1.Size = new System.Drawing.Size(498, 39);
            this.cTabControl1.TabIndex = 1;
            this.cTabControl1.SelectedIndexChanged += new System.EventHandler(this.cTabControl1_SelectedIndexChanged);
            this.cTabControl1.Click += new System.EventHandler(this.cTabControl1_Click);
            // 
            // ctp_p1
            // 
            this.ctp_p1.Location = new System.Drawing.Point(4, 36);
            this.ctp_p1.Name = "ctp_p1";
            this.ctp_p1.Padding = new System.Windows.Forms.Padding(3);
            this.ctp_p1.Size = new System.Drawing.Size(490, 0);
            this.ctp_p1.TabIndex = 0;
            this.ctp_p1.Text = "取料料盘   ";
            this.ctp_p1.UseVisualStyleBackColor = true;
            // 
            // ctp_p2
            // 
            this.ctp_p2.Location = new System.Drawing.Point(4, 36);
            this.ctp_p2.Name = "ctp_p2";
            this.ctp_p2.Padding = new System.Windows.Forms.Padding(3);
            this.ctp_p2.Size = new System.Drawing.Size(490, 0);
            this.ctp_p2.TabIndex = 1;
            this.ctp_p2.Text = "收料OK盘  ";
            this.ctp_p2.UseVisualStyleBackColor = true;
            // 
            // ctp_p3
            // 
            this.ctp_p3.Location = new System.Drawing.Point(4, 36);
            this.ctp_p3.Name = "ctp_p3";
            this.ctp_p3.Size = new System.Drawing.Size(490, 0);
            this.ctp_p3.TabIndex = 2;
            this.ctp_p3.Text = "AA--NG    ";
            this.ctp_p3.UseVisualStyleBackColor = true;
            // 
            // ctp_p4
            // 
            this.ctp_p4.Location = new System.Drawing.Point(4, 36);
            this.ctp_p4.Name = "ctp_p4";
            this.ctp_p4.Size = new System.Drawing.Size(490, 0);
            this.ctp_p4.TabIndex = 3;
            this.ctp_p4.Text = "开图--NG ";
            this.ctp_p4.UseVisualStyleBackColor = true;
            // 
            // pic_pallet
            // 
            this.pic_pallet.Image = ((System.Drawing.Image)(resources.GetObject("pic_pallet.Image")));
            this.pic_pallet.Location = new System.Drawing.Point(88, 152);
            this.pic_pallet.Name = "pic_pallet";
            this.pic_pallet.Size = new System.Drawing.Size(181, 114);
            this.pic_pallet.TabIndex = 37;
            this.pic_pallet.TabStop = false;
            // 
            // tp_gd
            // 
            this.tp_gd.Location = new System.Drawing.Point(204, 4);
            this.tp_gd.Name = "tp_gd";
            this.tp_gd.Padding = new System.Windows.Forms.Padding(3);
            this.tp_gd.Size = new System.Drawing.Size(1060, 586);
            this.tp_gd.TabIndex = 0;
            this.tp_gd.Text = "轨道";
            this.tp_gd.UseVisualStyleBackColor = true;
            // 
            // tp_rc
            // 
            this.tp_rc.Location = new System.Drawing.Point(204, 4);
            this.tp_rc.Name = "tp_rc";
            this.tp_rc.Padding = new System.Windows.Forms.Padding(3);
            this.tp_rc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tp_rc.Size = new System.Drawing.Size(1060, 586);
            this.tp_rc.TabIndex = 1;
            this.tp_rc.Text = "机械手";
            this.tp_rc.UseVisualStyleBackColor = true;
            // 
            // tp_vision
            // 
            this.tp_vision.Location = new System.Drawing.Point(204, 4);
            this.tp_vision.Name = "tp_vision";
            this.tp_vision.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tp_vision.Size = new System.Drawing.Size(1060, 586);
            this.tp_vision.TabIndex = 4;
            this.tp_vision.Text = "视觉";
            this.tp_vision.UseVisualStyleBackColor = true;
            // 
            // tp_set
            // 
            this.tp_set.Location = new System.Drawing.Point(204, 4);
            this.tp_set.Name = "tp_set";
            this.tp_set.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tp_set.Size = new System.Drawing.Size(1060, 586);
            this.tp_set.TabIndex = 5;
            this.tp_set.Text = "参数";
            this.tp_set.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(-10, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1298, 2);
            this.panel3.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrProduct";
            this.Text = "FrProduct";
            this.Load += new System.EventHandler(this.FrProduct_Load_1);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ctb_product.ResumeLayout(false);
            this.tp_file.ResumeLayout(false);
            this.tp_file.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numer_tray_cnt)).EndInit();
            this.tp_tray.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberRow)).EndInit();
            this.cTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_pallet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabPage tp_gd;
        private System.Windows.Forms.TabPage tp_rc;
        private System.Windows.Forms.TabPage tp_file;
        private System.Windows.Forms.TabPage tp_tray;
        private System.Windows.Forms.TabPage tp_vision;
        private System.Windows.Forms.TabPage tp_set;
        public CTabControl ctb_product;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_get_tray_data;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown numberLine;
        private System.Windows.Forms.NumericUpDown numberRow;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton11;
        private CTabControl cTabControl1;
        private System.Windows.Forms.TabPage ctp_p1;
        private System.Windows.Forms.TabPage ctp_p2;
        private System.Windows.Forms.TabPage ctp_p3;
        private System.Windows.Forms.TabPage ctp_p4;
        private System.Windows.Forms.ListBox listBox_product;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pic_pallet;
        private MotionCtrl.PosTable PosTable1;
        private MotionCtrl.AXIS_Panle axis_panle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numer_tray_cnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}