namespace UI.Compment
{
    partial class UpLoad
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
            this.lb_pos = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_sta = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_zk_sen = new System.Windows.Forms.CheckBox();
            this.chk_box_sen = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chk_zk_on = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_sta = new System.Windows.Forms.Label();
            this.lb_name = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_sta.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_pos
            // 
            this.lb_pos.AutoSize = true;
            this.lb_pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_pos.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_pos.ForeColor = System.Drawing.Color.Silver;
            this.lb_pos.Location = new System.Drawing.Point(3, 24);
            this.lb_pos.Name = "lb_pos";
            this.lb_pos.Size = new System.Drawing.Size(154, 116);
            this.lb_pos.TabIndex = 0;
            this.lb_pos.Text = " X:-000.000\r\n Y:-000.000\r\n Z:-000.000\r\nU1:-000.000\r\nU2:-000.000";
            this.lb_pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_pos.Click += new System.EventHandler(this.lb_pos_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_sta, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lb_pos, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(160, 160);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pnl_sta
            // 
            this.pnl_sta.Controls.Add(this.tableLayoutPanel3);
            this.pnl_sta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_sta.Location = new System.Drawing.Point(3, 143);
            this.pnl_sta.Name = "pnl_sta";
            this.pnl_sta.Size = new System.Drawing.Size(154, 14);
            this.pnl_sta.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.chk_zk_sen, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chk_box_sen, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBox1, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.chk_zk_on, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(51, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(103, 14);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // chk_zk_sen
            // 
            this.chk_zk_sen.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_zk_sen.AutoSize = true;
            this.chk_zk_sen.Checked = true;
            this.chk_zk_sen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_zk_sen.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chk_zk_sen.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gold;
            this.chk_zk_sen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_zk_sen.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_zk_sen.Location = new System.Drawing.Point(23, 3);
            this.chk_zk_sen.Name = "chk_zk_sen";
            this.chk_zk_sen.Size = new System.Drawing.Size(14, 8);
            this.chk_zk_sen.TabIndex = 13;
            this.chk_zk_sen.Text = " ";
            this.chk_zk_sen.UseVisualStyleBackColor = true;
            // 
            // chk_box_sen
            // 
            this.chk_box_sen.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_box_sen.AutoSize = true;
            this.chk_box_sen.Checked = true;
            this.chk_box_sen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_box_sen.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chk_box_sen.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gold;
            this.chk_box_sen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_box_sen.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_box_sen.Location = new System.Drawing.Point(86, 3);
            this.chk_box_sen.Name = "chk_box_sen";
            this.chk_box_sen.Size = new System.Drawing.Size(14, 8);
            this.chk_box_sen.TabIndex = 14;
            this.chk_box_sen.Text = " ";
            this.chk_box_sen.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.checkBox1.FlatAppearance.CheckedBackColor = System.Drawing.Color.PaleGreen;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(66, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(14, 8);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = " ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chk_zk_on
            // 
            this.chk_zk_on.Appearance = System.Windows.Forms.Appearance.Button;
            this.chk_zk_on.AutoSize = true;
            this.chk_zk_on.Checked = true;
            this.chk_zk_on.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_zk_on.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chk_zk_on.FlatAppearance.CheckedBackColor = System.Drawing.Color.PaleGreen;
            this.chk_zk_on.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_zk_on.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_zk_on.Location = new System.Drawing.Point(3, 3);
            this.chk_zk_on.Name = "chk_zk_on";
            this.chk_zk_on.Size = new System.Drawing.Size(14, 8);
            this.chk_zk_on.TabIndex = 12;
            this.chk_zk_on.Text = " ";
            this.chk_zk_on.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.86111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.13889F));
            this.tableLayoutPanel2.Controls.Add(this.lb_sta, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lb_name, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(154, 18);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // lb_sta
            // 
            this.lb_sta.AutoSize = true;
            this.lb_sta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_sta.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_sta.ForeColor = System.Drawing.Color.Gray;
            this.lb_sta.Location = new System.Drawing.Point(48, 0);
            this.lb_sta.Name = "lb_sta";
            this.lb_sta.Size = new System.Drawing.Size(103, 18);
            this.lb_sta.TabIndex = 3;
            this.lb_sta.Text = "状态";
            this.lb_sta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_name.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_name.ForeColor = System.Drawing.Color.Gray;
            this.lb_name.Location = new System.Drawing.Point(3, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(39, 18);
            this.lb_name.TabIndex = 2;
            this.lb_name.Text = "上料";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UpLoad";
            this.Size = new System.Drawing.Size(160, 160);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnl_sta.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_pos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_sta;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lb_sta;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox chk_zk_sen;
        private System.Windows.Forms.CheckBox chk_box_sen;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chk_zk_on;
    }
}
