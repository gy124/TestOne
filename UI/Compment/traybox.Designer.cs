namespace UI
{
    partial class traybox
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
            this.pnl_status = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_out = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_in = new System.Windows.Forms.Button();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_status = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_box_sen = new System.Windows.Forms.CheckBox();
            this.chk_zk_sen = new System.Windows.Forms.CheckBox();
            this.chk_zk_on = new System.Windows.Forms.CheckBox();
            this.lb_z_pos = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_status
            // 
            this.pnl_status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_status.Location = new System.Drawing.Point(3, 3);
            this.pnl_status.Name = "pnl_status";
            this.pnl_status.Size = new System.Drawing.Size(54, 135);
            this.pnl_status.TabIndex = 0;
            this.pnl_status.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_status_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl_status, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 141);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btn_down, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.btn_out, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.btn_up, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btn_in, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.lb_name, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lb_status, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lb_z_pos, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(63, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(174, 135);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // btn_down
            // 
            this.btn_down.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_down.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_down.Location = new System.Drawing.Point(3, 91);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(81, 41);
            this.btn_down.TabIndex = 3;
            this.btn_down.Text = "下降";
            this.btn_down.UseVisualStyleBackColor = false;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_out
            // 
            this.btn_out.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_out.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_out.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_out.Location = new System.Drawing.Point(90, 91);
            this.btn_out.Name = "btn_out";
            this.btn_out.Size = new System.Drawing.Size(81, 41);
            this.btn_out.TabIndex = 2;
            this.btn_out.Text = "出料";
            this.btn_out.UseVisualStyleBackColor = false;
            this.btn_out.Click += new System.EventHandler(this.btn_out_Click);
            // 
            // btn_up
            // 
            this.btn_up.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_up.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_up.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_up.Location = new System.Drawing.Point(3, 45);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(81, 40);
            this.btn_up.TabIndex = 1;
            this.btn_up.Text = "上升";
            this.btn_up.UseVisualStyleBackColor = false;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_in
            // 
            this.btn_in.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_in.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_in.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_in.Location = new System.Drawing.Point(90, 45);
            this.btn_in.Name = "btn_in";
            this.btn_in.Size = new System.Drawing.Size(81, 40);
            this.btn_in.TabIndex = 4;
            this.btn_in.Text = "进料";
            this.btn_in.UseVisualStyleBackColor = false;
            this.btn_in.Click += new System.EventHandler(this.btn_in_Click);
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_name.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_name.ForeColor = System.Drawing.Color.DimGray;
            this.lb_name.Location = new System.Drawing.Point(3, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(81, 24);
            this.lb_name.TabIndex = 5;
            this.lb_name.Text = "名称";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_status.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_status.ForeColor = System.Drawing.Color.DimGray;
            this.lb_status.Location = new System.Drawing.Point(90, 0);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(81, 24);
            this.lb_status.TabIndex = 6;
            this.lb_status.Text = "状态";
            this.lb_status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.chk_box_sen, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chk_zk_sen, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.chk_zk_on, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(110, 27);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(61, 12);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // chk_box_sen
            // 
            this.chk_box_sen.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_box_sen.AutoSize = true;
            this.chk_box_sen.Checked = true;
            this.chk_box_sen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_box_sen.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk_box_sen.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chk_box_sen.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gold;
            this.chk_box_sen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_box_sen.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_box_sen.Location = new System.Drawing.Point(3, 3);
            this.chk_box_sen.Name = "chk_box_sen";
            this.chk_box_sen.Size = new System.Drawing.Size(14, 6);
            this.chk_box_sen.TabIndex = 14;
            this.chk_box_sen.Text = " ";
            this.chk_box_sen.UseVisualStyleBackColor = true;
            // 
            // chk_zk_sen
            // 
            this.chk_zk_sen.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_zk_sen.AutoSize = true;
            this.chk_zk_sen.Checked = true;
            this.chk_zk_sen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_zk_sen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chk_zk_sen.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chk_zk_sen.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gold;
            this.chk_zk_sen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_zk_sen.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_zk_sen.Location = new System.Drawing.Point(43, 3);
            this.chk_zk_sen.Name = "chk_zk_sen";
            this.chk_zk_sen.Size = new System.Drawing.Size(15, 6);
            this.chk_zk_sen.TabIndex = 13;
            this.chk_zk_sen.Text = " ";
            this.chk_zk_sen.UseVisualStyleBackColor = true;
            // 
            // chk_zk_on
            // 
            this.chk_zk_on.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_zk_on.AutoSize = true;
            this.chk_zk_on.Checked = true;
            this.chk_zk_on.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_zk_on.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chk_zk_on.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chk_zk_on.FlatAppearance.CheckedBackColor = System.Drawing.Color.PaleGreen;
            this.chk_zk_on.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_zk_on.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_zk_on.Location = new System.Drawing.Point(23, 3);
            this.chk_zk_on.Name = "chk_zk_on";
            this.chk_zk_on.Size = new System.Drawing.Size(14, 6);
            this.chk_zk_on.TabIndex = 12;
            this.chk_zk_on.Text = " ";
            this.chk_zk_on.UseVisualStyleBackColor = true;
            // 
            // lb_z_pos
            // 
            this.lb_z_pos.AutoSize = true;
            this.lb_z_pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_z_pos.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_z_pos.ForeColor = System.Drawing.Color.Silver;
            this.lb_z_pos.Location = new System.Drawing.Point(3, 24);
            this.lb_z_pos.Name = "lb_z_pos";
            this.lb_z_pos.Size = new System.Drawing.Size(81, 18);
            this.lb_z_pos.TabIndex = 9;
            this.lb_z_pos.Text = "-000.000";
            this.lb_z_pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // traybox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "traybox";
            this.Size = new System.Drawing.Size(240, 141);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_out;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_in;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox chk_zk_sen;
        private System.Windows.Forms.CheckBox chk_zk_on;
        private System.Windows.Forms.Label lb_z_pos;
        private System.Windows.Forms.CheckBox chk_box_sen;
    }
}
