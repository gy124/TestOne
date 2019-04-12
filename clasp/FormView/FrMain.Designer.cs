namespace clasp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtn_count = new System.Windows.Forms.Button();
            this.rbtn_sys = new System.Windows.Forms.Button();
            this.rbtn_run = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.rbtn_product = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 611);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel2.Controls.Add(this.rbtn_count, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbtn_sys, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbtn_run, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbtn_product, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1178, 74);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // rbtn_count
            // 
            this.rbtn_count.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbtn_count.BackgroundImage")));
            this.rbtn_count.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtn_count.Location = new System.Drawing.Point(483, 3);
            this.rbtn_count.Name = "rbtn_count";
            this.rbtn_count.Size = new System.Drawing.Size(114, 68);
            this.rbtn_count.TabIndex = 4;
            this.rbtn_count.UseVisualStyleBackColor = true;
            this.rbtn_count.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // rbtn_sys
            // 
            this.rbtn_sys.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbtn_sys.BackgroundImage")));
            this.rbtn_sys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtn_sys.Location = new System.Drawing.Point(243, 3);
            this.rbtn_sys.Name = "rbtn_sys";
            this.rbtn_sys.Size = new System.Drawing.Size(114, 68);
            this.rbtn_sys.TabIndex = 3;
            this.rbtn_sys.UseVisualStyleBackColor = true;
            this.rbtn_sys.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // rbtn_run
            // 
            this.rbtn_run.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbtn_run.BackgroundImage")));
            this.rbtn_run.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtn_run.Image = ((System.Drawing.Image)(resources.GetObject("rbtn_run.Image")));
            this.rbtn_run.Location = new System.Drawing.Point(123, 3);
            this.rbtn_run.Name = "rbtn_run";
            this.rbtn_run.Size = new System.Drawing.Size(114, 68);
            this.rbtn_run.TabIndex = 2;
            this.rbtn_run.UseVisualStyleBackColor = true;
            this.rbtn_run.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 68);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // rbtn_product
            // 
            this.rbtn_product.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbtn_product.BackgroundImage")));
            this.rbtn_product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtn_product.Location = new System.Drawing.Point(363, 3);
            this.rbtn_product.Name = "rbtn_product";
            this.rbtn_product.Size = new System.Drawing.Size(114, 68);
            this.rbtn_product.TabIndex = 0;
            this.rbtn_product.UseVisualStyleBackColor = true;
            this.rbtn_product.Click += new System.EventHandler(this.rbtn_run_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(781, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(394, 68);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 525);
            this.panel1.TabIndex = 1;
            // 
            // FrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrMain";
            this.Text = "扣合机";
            this.Load += new System.EventHandler(this.FrMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button rbtn_count;
        private System.Windows.Forms.Button rbtn_sys;
        private System.Windows.Forms.Button rbtn_run;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button rbtn_product;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}

