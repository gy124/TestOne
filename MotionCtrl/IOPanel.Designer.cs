using System;
using System.ComponentModel;
using System.Drawing;

namespace MotionCtrl
{
    partial class IOPanel
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
            this.pnl = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_on = new System.Windows.Forms.Label();
            this.btn_m = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_sen_down = new System.Windows.Forms.Label();
            this.lbl_sen_up = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(199, 43);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnl.ColumnCount = 3;
            this.pnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnl.Controls.Add(this.lbl_on, 0, 0);
            this.pnl.Controls.Add(this.btn_m, 2, 0);
            this.pnl.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(4, 4);
            this.pnl.Name = "pnl";
            this.pnl.RowCount = 1;
            this.pnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl.Size = new System.Drawing.Size(191, 35);
            this.pnl.TabIndex = 10;
            // 
            // lbl_on
            // 
            this.lbl_on.BackColor = System.Drawing.Color.YellowGreen;
            this.lbl_on.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_on.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_on.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_on.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_on.Location = new System.Drawing.Point(6, 6);
            this.lbl_on.Margin = new System.Windows.Forms.Padding(6, 6, 0, 6);
            this.lbl_on.Name = "lbl_on";
            this.lbl_on.Size = new System.Drawing.Size(24, 23);
            this.lbl_on.TabIndex = 9;
            this.lbl_on.Text = "on";
            this.lbl_on.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_on.Click += new System.EventHandler(this.lbl_on_Click);
            // 
            // btn_m
            // 
            this.btn_m.AutoSize = true;
            this.btn_m.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_m.FlatAppearance.BorderSize = 0;
            this.btn_m.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_m.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_m.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_m.Location = new System.Drawing.Point(57, 3);
            this.btn_m.Name = "btn_m";
            this.btn_m.Size = new System.Drawing.Size(133, 29);
            this.btn_m.TabIndex = 11;
            this.btn_m.Text = "吸头1破真空";
            this.btn_m.UseVisualStyleBackColor = false;
            this.btn_m.Click += new System.EventHandler(this.btn_m_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_sen_down, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl_sen_up, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(30, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(24, 35);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // lbl_sen_down
            // 
            this.lbl_sen_down.BackColor = System.Drawing.Color.Silver;
            this.lbl_sen_down.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_sen_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_sen_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_sen_down.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_sen_down.Location = new System.Drawing.Point(6, 20);
            this.lbl_sen_down.Margin = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.lbl_sen_down.Name = "lbl_sen_down";
            this.lbl_sen_down.Size = new System.Drawing.Size(12, 9);
            this.lbl_sen_down.TabIndex = 12;
            this.lbl_sen_down.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_sen_up
            // 
            this.lbl_sen_up.BackColor = System.Drawing.Color.Coral;
            this.lbl_sen_up.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_sen_up.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_sen_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_sen_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_sen_up.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_sen_up.Location = new System.Drawing.Point(6, 6);
            this.lbl_sen_up.Margin = new System.Windows.Forms.Padding(6, 6, 6, 3);
            this.lbl_sen_up.Name = "lbl_sen_up";
            this.lbl_sen_up.Size = new System.Drawing.Size(12, 8);
            this.lbl_sen_up.TabIndex = 11;
            this.lbl_sen_up.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IOPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "IOPanel";
            this.Size = new System.Drawing.Size(199, 43);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region 属性设定
        private Color mOUT_Color= Color.YellowGreen;
        private Color mSEN_Color= Color.Coral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel pnl;
        private System.Windows.Forms.Label lbl_on;
        public System.Windows.Forms.Button btn_m;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbl_sen_down;
        private System.Windows.Forms.Label lbl_sen_up;

        //内容设定
        [Browsable(true)]
        [Description("IOP text设定")]
        public String IOP_text
        {
            get { return btn_m.Text; }
            set { btn_m.Text = value; }
        }
        //字体设定
        [Browsable(true)]
        [Description("IOP font设定")]
        public Font IOP_Font
        {
            get { return btn_m.Font; }
            set { btn_m.Font = value; }
        }
        //颜色设定
        [Browsable(true)]
        [Description("IOP color设定")]
        public Color IOP_Color
        {
            get { return btn_m.BackColor; }
            set { btn_m.BackColor = value; pnl.BackColor = value; }
        }

        [Browsable(true)]
        [Description("OUT color设定")]
        public Color OUT_Color
        {
            get { return mOUT_Color; }
            set { mOUT_Color = value;lbl_on.BackColor = value; }
        }

        [Browsable(true)]
        [Description("SEN color设定")]
        public Color SEN_Color
        {
            get { return mSEN_Color; }
            set { mSEN_Color = value; lbl_sen_up.BackColor = value; }
        }

        [Browsable(true)]
        [Description("SEN_UP TEXT设定")]
        public string SEN_UP_Text
        {
            get { return lbl_sen_up.Text; }
            set { lbl_sen_up.Text = value; }
        }

        [Browsable(true)]
        [Description("SEN_DOWN TEXT设定")]
        public string SEN_DOWN_Text
        {
            get { return lbl_sen_down.Text; }
            set { lbl_sen_down.Text = value; }
        }

        #endregion
    }
}
