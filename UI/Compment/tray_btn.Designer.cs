namespace UI.Compment
{
    partial class tray_btn
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnl_dgv = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_statu = new System.Windows.Forms.Label();
            this.lbl_index = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_dgv
            // 
            this.pnl_dgv.Controls.Add(this.dgv);
            this.pnl_dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_dgv.Location = new System.Drawing.Point(0, 44);
            this.pnl_dgv.Name = "pnl_dgv";
            this.pnl_dgv.Size = new System.Drawing.Size(289, 214);
            this.pnl_dgv.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(289, 214);
            this.dgv.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_statu
            // 
            this.lbl_statu.AutoSize = true;
            this.lbl_statu.Location = new System.Drawing.Point(10, 19);
            this.lbl_statu.Name = "lbl_statu";
            this.lbl_statu.Size = new System.Drawing.Size(41, 12);
            this.lbl_statu.TabIndex = 1;
            this.lbl_statu.Text = "label1";
            // 
            // lbl_index
            // 
            this.lbl_index.AutoSize = true;
            this.lbl_index.Location = new System.Drawing.Point(227, 19);
            this.lbl_index.Name = "lbl_index";
            this.lbl_index.Size = new System.Drawing.Size(41, 12);
            this.lbl_index.TabIndex = 2;
            this.lbl_index.Text = "label2";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(113, 19);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(41, 12);
            this.lbl_name.TabIndex = 3;
            this.lbl_name.Text = "label3";
            // 
            // tray_btn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_index);
            this.Controls.Add(this.lbl_statu);
            this.Controls.Add(this.pnl_dgv);
            this.Name = "tray_btn";
            this.Size = new System.Drawing.Size(289, 258);
            this.Load += new System.EventHandler(this.tray_btn_Load);
            this.pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_dgv;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_statu;
        private System.Windows.Forms.Label lbl_index;
        private System.Windows.Forms.Label lbl_name;
    }
}
