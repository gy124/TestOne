namespace clasp
{
    partial class FrSys
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
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.tabConSlect = new CTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CardTable = new MotionCtrl.CardTable();
            this.ctb_ax_sel = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.axisTable = new MotionCtrl.AxisTable();
            this.axisConfig = new MotionCtrl.AxisConfig();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ctb_io_type = new CTabControl();
            this.tp_out = new System.Windows.Forms.TabPage();
            this.ioTable = new MotionCtrl.IOTable();
            this.tp_in = new System.Windows.Forms.TabPage();
            this.ioTableIN = new MotionCtrl.IOTable();
            this.tp_cy = new System.Windows.Forms.TabPage();
            this.cylinderTable = new MotionCtrl.CylinderTable();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabConSlect.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ctb_ax_sel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.ctb_io_type.SuspendLayout();
            this.tp_out.SuspendLayout();
            this.tp_in.SuspendLayout();
            this.tp_cy.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick_1);
            // 
            // tabConSlect
            // 
            this.tabConSlect.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabConSlect.BackColor = System.Drawing.Color.LightGray;
            this.tabConSlect.Controls.Add(this.tabPage1);
            this.tabConSlect.Controls.Add(this.ctb_ax_sel);
            this.tabConSlect.Controls.Add(this.tabPage4);
            this.tabConSlect.Controls.Add(this.tabPage5);
            this.tabConSlect.Controls.Add(this.tabPage8);
            this.tabConSlect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConSlect.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabConSlect.HeaderBackColor = System.Drawing.Color.Silver;
            this.tabConSlect.HeadSelectedBackColor = System.Drawing.Color.DodgerBlue;
            this.tabConSlect.HeadSelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabConSlect.ItemSize = new System.Drawing.Size(80, 160);
            this.tabConSlect.Location = new System.Drawing.Point(0, 0);
            this.tabConSlect.Multiline = true;
            this.tabConSlect.Name = "tabConSlect";
            this.tabConSlect.SelectedIndex = 0;
            this.tabConSlect.Size = new System.Drawing.Size(868, 414);
            this.tabConSlect.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CardTable);
            this.tabPage1.Location = new System.Drawing.Point(164, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(700, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "控制卡";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CardTable
            // 
            this.CardTable.BackColor = System.Drawing.Color.Transparent;
            this.CardTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardTable.Location = new System.Drawing.Point(3, 3);
            this.CardTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CardTable.Name = "CardTable";
            this.CardTable.Size = new System.Drawing.Size(694, 400);
            this.CardTable.TabIndex = 0;
            this.CardTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.CardTable.TableBackgColor = System.Drawing.SystemColors.ButtonFace;
            this.CardTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardTable.TableRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardTable.TableRowHight = 32;
            // 
            // ctb_ax_sel
            // 
            this.ctb_ax_sel.Controls.Add(this.tableLayoutPanel1);
            this.ctb_ax_sel.Location = new System.Drawing.Point(164, 4);
            this.ctb_ax_sel.Name = "ctb_ax_sel";
            this.ctb_ax_sel.Padding = new System.Windows.Forms.Padding(3);
            this.ctb_ax_sel.Size = new System.Drawing.Size(700, 406);
            this.ctb_ax_sel.TabIndex = 1;
            this.ctb_ax_sel.Text = "轴参数";
            this.ctb_ax_sel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.axisTable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.axisConfig, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(694, 400);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // axisTable
            // 
            this.axisTable.BackColor = System.Drawing.Color.Transparent;
            this.axisTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisTable.Location = new System.Drawing.Point(3, 4);
            this.axisTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axisTable.Name = "axisTable";
            this.axisTable.Size = new System.Drawing.Size(688, 192);
            this.axisTable.TabIndex = 0;
            this.axisTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.axisTable.TableBackgColor = System.Drawing.SystemColors.ButtonFace;
            this.axisTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisTable.TableRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisTable.TableRowHight = 32;
            // 
            // axisConfig
            // 
            this.axisConfig.BackColor = System.Drawing.Color.Transparent;
            this.axisConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisConfig.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisConfig.Location = new System.Drawing.Point(3, 204);
            this.axisConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axisConfig.Name = "axisConfig";
            this.axisConfig.Size = new System.Drawing.Size(688, 192);
            this.axisConfig.TabIndex = 1;
            this.axisConfig.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisConfig.TableBackgColor = System.Drawing.SystemColors.Control;
            this.axisConfig.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisConfig.TableRowColor = System.Drawing.Color.Silver;
            this.axisConfig.TableRowHight = 32;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ctb_io_type);
            this.tabPage4.Location = new System.Drawing.Point(164, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(700, 406);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "IO";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ctb_io_type
            // 
            this.ctb_io_type.BackColor = System.Drawing.SystemColors.Control;
            this.ctb_io_type.BorderColor = System.Drawing.SystemColors.Control;
            this.ctb_io_type.Controls.Add(this.tp_out);
            this.ctb_io_type.Controls.Add(this.tp_in);
            this.ctb_io_type.Controls.Add(this.tp_cy);
            this.ctb_io_type.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctb_io_type.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.ctb_io_type.ItemSize = new System.Drawing.Size(60, 32);
            this.ctb_io_type.Location = new System.Drawing.Point(3, 3);
            this.ctb_io_type.Name = "ctb_io_type";
            this.ctb_io_type.SelectedIndex = 0;
            this.ctb_io_type.Size = new System.Drawing.Size(694, 400);
            this.ctb_io_type.TabIndex = 28;
            // 
            // tp_out
            // 
            this.tp_out.Controls.Add(this.ioTable);
            this.tp_out.Location = new System.Drawing.Point(4, 36);
            this.tp_out.Name = "tp_out";
            this.tp_out.Padding = new System.Windows.Forms.Padding(3);
            this.tp_out.Size = new System.Drawing.Size(686, 360);
            this.tp_out.TabIndex = 0;
            this.tp_out.Text = "输出      ";
            this.tp_out.UseVisualStyleBackColor = true;
            // 
            // ioTable
            // 
            this.ioTable.BackColor = System.Drawing.Color.Transparent;
            this.ioTable.color_in_on = System.Drawing.Color.Orange;
            this.ioTable.color_out_on = System.Drawing.Color.Lime;
            this.ioTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ioTable.Location = new System.Drawing.Point(3, 3);
            this.ioTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ioTable.Name = "ioTable";
            this.ioTable.Size = new System.Drawing.Size(680, 354);
            this.ioTable.TabIndex = 0;
            this.ioTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.ioTable.TableBackgColor = System.Drawing.SystemColors.ButtonFace;
            this.ioTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioTable.TableRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ioTable.TableRowHight = 32;
            // 
            // tp_in
            // 
            this.tp_in.Controls.Add(this.ioTableIN);
            this.tp_in.Location = new System.Drawing.Point(4, 36);
            this.tp_in.Name = "tp_in";
            this.tp_in.Padding = new System.Windows.Forms.Padding(3);
            this.tp_in.Size = new System.Drawing.Size(686, 216);
            this.tp_in.TabIndex = 1;
            this.tp_in.Text = "输入      ";
            this.tp_in.UseVisualStyleBackColor = true;
            // 
            // ioTableIN
            // 
            this.ioTableIN.BackColor = System.Drawing.Color.Transparent;
            this.ioTableIN.color_in_on = System.Drawing.Color.Orange;
            this.ioTableIN.color_out_on = System.Drawing.Color.Lime;
            this.ioTableIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioTableIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioTableIN.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ioTableIN.Location = new System.Drawing.Point(3, 3);
            this.ioTableIN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ioTableIN.Name = "ioTableIN";
            this.ioTableIN.Size = new System.Drawing.Size(680, 210);
            this.ioTableIN.TabIndex = 0;
            this.ioTableIN.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.ioTableIN.TableBackgColor = System.Drawing.SystemColors.ButtonFace;
            this.ioTableIN.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioTableIN.TableRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ioTableIN.TableRowHight = 32;
            // 
            // tp_cy
            // 
            this.tp_cy.Controls.Add(this.cylinderTable);
            this.tp_cy.Location = new System.Drawing.Point(4, 36);
            this.tp_cy.Name = "tp_cy";
            this.tp_cy.Size = new System.Drawing.Size(68, 216);
            this.tp_cy.TabIndex = 2;
            this.tp_cy.Text = "气缸/组合      ";
            this.tp_cy.UseVisualStyleBackColor = true;
            // 
            // cylinderTable
            // 
            this.cylinderTable.BackColor = System.Drawing.Color.Transparent;
            this.cylinderTable.color_in_on = System.Drawing.Color.Orange;
            this.cylinderTable.color_out_on = System.Drawing.Color.Lime;
            this.cylinderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cylinderTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cylinderTable.Location = new System.Drawing.Point(0, 0);
            this.cylinderTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cylinderTable.Name = "cylinderTable";
            this.cylinderTable.Size = new System.Drawing.Size(68, 216);
            this.cylinderTable.TabIndex = 1;
            this.cylinderTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.cylinderTable.TableBackgColor = System.Drawing.SystemColors.ButtonFace;
            this.cylinderTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cylinderTable.TableRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cylinderTable.TableRowHight = 32;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(164, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(700, 406);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(164, 4);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(700, 406);
            this.tabPage8.TabIndex = 4;
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // FrSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 414);
            this.Controls.Add(this.tabConSlect);
            this.Name = "FrSys";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrSys_Load_1);
            this.tabConSlect.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ctb_ax_sel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ctb_io_type.ResumeLayout(false);
            this.tp_out.ResumeLayout(false);
            this.tp_in.ResumeLayout(false);
            this.tp_cy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CTabControl tabConSlect;
        private System.Windows.Forms.TabPage tabPage1;
        private MotionCtrl.CardTable CardTable;
        private System.Windows.Forms.TabPage ctb_ax_sel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MotionCtrl.AxisTable axisTable;
        private MotionCtrl.AxisConfig axisConfig;
        private System.Windows.Forms.TabPage tabPage4;
        private MotionCtrl.IOTable ioTable;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Timer timerUpdate;
        private CTabControl ctb_io_type;
        private System.Windows.Forms.TabPage tp_out;
        private System.Windows.Forms.TabPage tp_in;
        private MotionCtrl.IOTable ioTableIN;
        private System.Windows.Forms.TabPage tp_cy;
        private MotionCtrl.CylinderTable cylinderTable;
    }
}