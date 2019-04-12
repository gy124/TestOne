using System;
using System.ComponentModel;
using System.Drawing;

namespace MotionCtrl
{
    partial class AxisConfig
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.layer_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.layer_tg_name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.layer_part_name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.layer_offset_x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_offset_y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_offset_z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_offset_a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.axis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startspd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopspd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.homespd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workspd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tacc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tdec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sln = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puls_per_mm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pos0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmr_update = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // layer_num
            // 
            this.layer_num.DataPropertyName = "layer_num";
            dataGridViewCellStyle1.Format = "N0";
            this.layer_num.DefaultCellStyle = dataGridViewCellStyle1;
            this.layer_num.HeaderText = "编号";
            this.layer_num.Name = "layer_num";
            this.layer_num.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.layer_num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // layer_id
            // 
            this.layer_id.DataPropertyName = "layer_id";
            dataGridViewCellStyle2.Format = "N0";
            this.layer_id.DefaultCellStyle = dataGridViewCellStyle2;
            this.layer_id.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.layer_id.HeaderText = "对应层";
            this.layer_id.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.layer_id.Name = "layer_id";
            this.layer_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // layer_tg_name
            // 
            this.layer_tg_name.DataPropertyName = "layer_tg_name";
            this.layer_tg_name.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.layer_tg_name.HeaderText = "目标名称";
            this.layer_tg_name.Items.AddRange(new object[] {
            "TA",
            "TB",
            "TC",
            "TD"});
            this.layer_tg_name.Name = "layer_tg_name";
            this.layer_tg_name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // layer_part_name
            // 
            this.layer_part_name.DataPropertyName = "layer_part_name";
            this.layer_part_name.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.layer_part_name.HeaderText = "物料名称";
            this.layer_part_name.Items.AddRange(new object[] {
            "FA",
            "FB",
            "FC",
            "FD"});
            this.layer_part_name.Name = "layer_part_name";
            this.layer_part_name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // layer_offset_x
            // 
            this.layer_offset_x.DataPropertyName = "layer_offset_x";
            dataGridViewCellStyle3.Format = "N3";
            this.layer_offset_x.DefaultCellStyle = dataGridViewCellStyle3;
            this.layer_offset_x.HeaderText = "X";
            this.layer_offset_x.MaxInputLength = 9;
            this.layer_offset_x.Name = "layer_offset_x";
            // 
            // layer_offset_y
            // 
            this.layer_offset_y.DataPropertyName = "layer_offset_y";
            dataGridViewCellStyle4.Format = "N3";
            this.layer_offset_y.DefaultCellStyle = dataGridViewCellStyle4;
            this.layer_offset_y.HeaderText = "Y";
            this.layer_offset_y.MaxInputLength = 9;
            this.layer_offset_y.Name = "layer_offset_y";
            // 
            // layer_offset_z
            // 
            this.layer_offset_z.DataPropertyName = "layer_offset_z";
            dataGridViewCellStyle5.Format = "N3";
            this.layer_offset_z.DefaultCellStyle = dataGridViewCellStyle5;
            this.layer_offset_z.HeaderText = "Z";
            this.layer_offset_z.MaxInputLength = 9;
            this.layer_offset_z.Name = "layer_offset_z";
            // 
            // layer_offset_a
            // 
            this.layer_offset_a.DataPropertyName = "layer_offset_a";
            dataGridViewCellStyle6.Format = "N3";
            this.layer_offset_a.DefaultCellStyle = dataGridViewCellStyle6;
            this.layer_offset_a.HeaderText = "R";
            this.layer_offset_a.MaxInputLength = 9;
            this.layer_offset_a.Name = "layer_offset_a";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.ColumnHeadersHeight = 32;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.axis,
            this.startspd,
            this.stopspd,
            this.homespd,
            this.workspd,
            this.tacc,
            this.tdec,
            this.sln,
            this.slp,
            this.puls_per_mm,
            this.offset,
            this.pos0,
            this.pos1});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.Gray;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgv.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgv.RowTemplate.Height = 32;
            this.dgv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowEditingIcon = false;
            this.dgv.Size = new System.Drawing.Size(1078, 347);
            this.dgv.TabIndex = 0;
            this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
            // 
            // axis
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.DarkGray;
            this.axis.DefaultCellStyle = dataGridViewCellStyle9;
            this.axis.HeaderText = "    轴";
            this.axis.Name = "axis";
            this.axis.ReadOnly = true;
            this.axis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.axis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.axis.Width = 140;
            // 
            // startspd
            // 
            this.startspd.HeaderText = "启动速度";
            this.startspd.Name = "startspd";
            this.startspd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.startspd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.startspd.Width = 80;
            // 
            // stopspd
            // 
            this.stopspd.HeaderText = "停止速度";
            this.stopspd.Name = "stopspd";
            this.stopspd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.stopspd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.stopspd.Width = 80;
            // 
            // homespd
            // 
            this.homespd.HeaderText = "回零速度";
            this.homespd.Name = "homespd";
            this.homespd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.homespd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.homespd.Width = 80;
            // 
            // workspd
            // 
            this.workspd.HeaderText = "工作速度";
            this.workspd.Name = "workspd";
            this.workspd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.workspd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.workspd.Width = 80;
            // 
            // tacc
            // 
            this.tacc.HeaderText = "加速时间";
            this.tacc.Name = "tacc";
            this.tacc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tacc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tacc.Width = 80;
            // 
            // tdec
            // 
            this.tdec.HeaderText = "减速时间";
            this.tdec.Name = "tdec";
            this.tdec.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tdec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tdec.Width = 80;
            // 
            // sln
            // 
            this.sln.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sln.FillWeight = 10F;
            this.sln.HeaderText = "负限位";
            this.sln.MinimumWidth = 70;
            this.sln.Name = "sln";
            this.sln.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sln.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // slp
            // 
            this.slp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.slp.FillWeight = 10F;
            this.slp.HeaderText = "正限位";
            this.slp.MinimumWidth = 70;
            this.slp.Name = "slp";
            this.slp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.slp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // puls_per_mm
            // 
            this.puls_per_mm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.puls_per_mm.HeaderText = "脉冲/mm";
            this.puls_per_mm.MinimumWidth = 80;
            this.puls_per_mm.Name = "puls_per_mm";
            this.puls_per_mm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.puls_per_mm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // offset
            // 
            this.offset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.offset.FillWeight = 10F;
            this.offset.HeaderText = "偏移";
            this.offset.MinimumWidth = 60;
            this.offset.Name = "offset";
            this.offset.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.offset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pos0
            // 
            this.pos0.HeaderText = "位置0";
            this.pos0.Name = "pos0";
            this.pos0.Width = 60;
            // 
            // pos1
            // 
            this.pos1.HeaderText = "位置1";
            this.pos1.Name = "pos1";
            this.pos1.Width = 60;
            // 
            // AxisConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dgv);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AxisConfig";
            this.Size = new System.Drawing.Size(1078, 347);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn layer_num;
        private System.Windows.Forms.DataGridViewComboBoxColumn layer_id;
        private System.Windows.Forms.DataGridViewComboBoxColumn layer_tg_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn layer_part_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn layer_offset_x;
        private System.Windows.Forms.DataGridViewTextBoxColumn layer_offset_y;
        private System.Windows.Forms.DataGridViewTextBoxColumn layer_offset_z;
        private System.Windows.Forms.DataGridViewTextBoxColumn layer_offset_a;
        private System.Windows.Forms.Timer tmr_update;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn axis;
        private System.Windows.Forms.DataGridViewTextBoxColumn startspd;
        private System.Windows.Forms.DataGridViewTextBoxColumn stopspd;
        private System.Windows.Forms.DataGridViewTextBoxColumn homespd;
        private System.Windows.Forms.DataGridViewTextBoxColumn workspd;
        private System.Windows.Forms.DataGridViewTextBoxColumn tacc;
        private System.Windows.Forms.DataGridViewTextBoxColumn tdec;
        private System.Windows.Forms.DataGridViewTextBoxColumn sln;
        private System.Windows.Forms.DataGridViewTextBoxColumn slp;
        private System.Windows.Forms.DataGridViewTextBoxColumn puls_per_mm;
        private System.Windows.Forms.DataGridViewTextBoxColumn offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos0;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos1;

        #region 属性设定

        [Browsable(true)]
        [Description("TableBackgColor设定")]
        public Color TableBackgColor
        {
            get { return dgv.BackgroundColor; }
            set { dgv.BackgroundColor = value; }
        }

        [Browsable(true)]
        [Description("TableBackgColor设定")]
        public Color HeaderBackgColor
        {
            get { return dgv.ColumnHeadersDefaultCellStyle.BackColor; }
            set { dgv.ColumnHeadersDefaultCellStyle.BackColor = value; }
        }

        [Browsable(true)]
        [Description("TableRowColor设定")]
        public Color TableRowColor
        {
            get { return dgv.DefaultCellStyle.BackColor; }
            set { dgv.DefaultCellStyle.BackColor = value;  dgv.RowTemplate.DefaultCellStyle.BackColor = value; }
        }

        [Description("TableAltColor设定")]
        public Color TableAltRowColor
        {
            get { return dgv.AlternatingRowsDefaultCellStyle.BackColor; }
            set { dgv.AlternatingRowsDefaultCellStyle.BackColor = value; }
        }

        [Browsable(true)]
        [Description("TableFontSet设定")]
        public Font TableFontSet
        {
            get { return dgv.DefaultCellStyle.Font; }
            set { dgv.DefaultCellStyle.Font = value; dgv.ColumnHeadersDefaultCellStyle.Font = value; }
        }

        [Browsable(true)]
        [Description("TableRowHight设定")]
        public int TableRowHight
        {
            get { return dgv.RowTemplate.Height; }
            set { dgv.RowTemplate.Height = value; }
        }
        #endregion
    }
}
