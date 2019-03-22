namespace UI
{
    partial class tray
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
            this.tl_pnl = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_tray = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_status = new System.Windows.Forms.Label();
            this.lb_disc = new System.Windows.Forms.Label();
            this.tl_pnl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tl_pnl
            // 
            this.tl_pnl.ColumnCount = 1;
            this.tl_pnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tl_pnl.Controls.Add(this.pnl_tray, 0, 1);
            this.tl_pnl.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tl_pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tl_pnl.Location = new System.Drawing.Point(0, 0);
            this.tl_pnl.Name = "tl_pnl";
            this.tl_pnl.RowCount = 2;
            this.tl_pnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tl_pnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tl_pnl.Size = new System.Drawing.Size(537, 339);
            this.tl_pnl.TabIndex = 2;
            // 
            // pnl_tray
            // 
            this.pnl_tray.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_tray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_tray.Location = new System.Drawing.Point(3, 23);
            this.pnl_tray.Name = "pnl_tray";
            this.pnl_tray.Size = new System.Drawing.Size(531, 313);
            this.pnl_tray.TabIndex = 2;
            this.pnl_tray.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_tray_Paint);
            this.pnl_tray.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_tray_MouseDown);
            this.pnl_tray.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_tray_MouseMove);
            this.pnl_tray.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_tray_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.lb_status, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_disc, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 14);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lb_status
            // 
            this.lb_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_status.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lb_status.Location = new System.Drawing.Point(109, 0);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(419, 14);
            this.lb_status.TabIndex = 7;
            this.lb_status.Text = "STATUS";
            this.lb_status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_disc
            // 
            this.lb_disc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_disc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_disc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lb_disc.Location = new System.Drawing.Point(3, 0);
            this.lb_disc.Name = "lb_disc";
            this.lb_disc.Size = new System.Drawing.Size(100, 14);
            this.lb_disc.TabIndex = 2;
            this.lb_disc.Text = "DISC";
            this.lb_disc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tl_pnl);
            this.DoubleBuffered = true;
            this.Name = "tray";
            this.Size = new System.Drawing.Size(537, 339);
            this.tl_pnl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tl_pnl;
        private System.Windows.Forms.Panel pnl_tray;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lb_disc;
        private System.Windows.Forms.Label lb_status;
    }
}
