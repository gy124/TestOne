namespace UI.Compment
{
    partial class DownLoad
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_pos = new System.Windows.Forms.Label();
            this.pnl_sta = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_sta = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lb_pos
            // 
            this.lb_pos.AutoSize = true;
            this.lb_pos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_pos.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_pos.ForeColor = System.Drawing.Color.Silver;
            this.lb_pos.Location = new System.Drawing.Point(3, 24);
            this.lb_pos.Name = "lb_pos";
            this.lb_pos.Size = new System.Drawing.Size(144, 36);
            this.lb_pos.TabIndex = 0;
            this.lb_pos.Text = "Y:-000.000\r\nZ:-000.000";
            this.lb_pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_sta
            // 
            this.pnl_sta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_sta.Location = new System.Drawing.Point(3, 63);
            this.pnl_sta.Name = "pnl_sta";
            this.pnl_sta.Size = new System.Drawing.Size(144, 84);
            this.pnl_sta.TabIndex = 2;
            this.pnl_sta.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_sta_Paint);
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
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(144, 18);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_name.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_name.ForeColor = System.Drawing.Color.Gray;
            this.lb_name.Location = new System.Drawing.Point(3, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(36, 18);
            this.lb_name.TabIndex = 2;
            this.lb_name.Text = "下料";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_sta
            // 
            this.lb_sta.AutoSize = true;
            this.lb_sta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_sta.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_sta.ForeColor = System.Drawing.Color.Gray;
            this.lb_sta.Location = new System.Drawing.Point(45, 0);
            this.lb_sta.Name = "lb_sta";
            this.lb_sta.Size = new System.Drawing.Size(96, 18);
            this.lb_sta.TabIndex = 3;
            this.lb_sta.Text = "状态";
            this.lb_sta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "DownLoad";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lb_pos;
        private System.Windows.Forms.Panel pnl_sta;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lb_sta;
        private System.Windows.Forms.Label lb_name;
    }
}
