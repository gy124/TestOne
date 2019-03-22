using System;
using System.ComponentModel;
using System.Drawing;

namespace MotionCtrl
{
    partial class CylinderTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.layer_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.layer_tg_name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.layer_part_name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.layer_offset_x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_offset_y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_offset_z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer_offset_a = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tmr_update = new System.Windows.Forms.Timer(this.components);
            this.disc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maincard_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_open = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_close = new System.Windows.Forms.DataGridViewButtonColumn();
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
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.ColumnHeadersHeight = 32;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.disc,
            this.status,
            this.id,
            this.maincard_id,
            this.btn_open,
            this.btn_close});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.Gray;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgv.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
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
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
            // 
            // tmr_update
            // 
            this.tmr_update.Interval = 300;
            this.tmr_update.Tick += new System.EventHandler(this.tmr_update_Tick);
            // 
            // disc
            // 
            this.disc.HeaderText = "气缸模组";
            this.disc.Name = "disc";
            this.disc.ReadOnly = true;
            this.disc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.disc.Width = 150;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            this.status.DefaultCellStyle = dataGridViewCellStyle9;
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.id.HeaderText = "感开";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // maincard_id
            // 
            this.maincard_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maincard_id.HeaderText = "感关";
            this.maincard_id.Name = "maincard_id";
            this.maincard_id.ReadOnly = true;
            this.maincard_id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btn_open
            // 
            this.btn_open.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Transparent;
            this.btn_open.DefaultCellStyle = dataGridViewCellStyle10;
            this.btn_open.FillWeight = 50F;
            this.btn_open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_open.HeaderText = "";
            this.btn_open.Name = "btn_open";
            this.btn_open.ReadOnly = true;
            this.btn_open.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btn_open.Text = "开";
            this.btn_open.UseColumnTextForButtonValue = true;
            // 
            // btn_close
            // 
            this.btn_close.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Transparent;
            this.btn_close.DefaultCellStyle = dataGridViewCellStyle11;
            this.btn_close.FillWeight = 50F;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.HeaderText = "";
            this.btn_close.Name = "btn_close";
            this.btn_close.ReadOnly = true;
            this.btn_close.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btn_close.Text = "关";
            this.btn_close.UseColumnTextForButtonValue = true;
            // 
            // CylinderTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dgv);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CylinderTable";
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
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Timer tmr_update;
        private System.Windows.Forms.DataGridViewTextBoxColumn disc;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn maincard_id;
        private System.Windows.Forms.DataGridViewButtonColumn btn_open;
        private System.Windows.Forms.DataGridViewButtonColumn btn_close;

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
            set { dgv.DefaultCellStyle.BackColor = value; dgv.RowTemplate.DefaultCellStyle.BackColor = value; }
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

        [Browsable(true)]
        [Description("OUT ON 颜色设定")]
        public Color color_out_on
        {
            get { return cl_out_on; }
            set { cl_out_on = value; }
        }

        [Browsable(true)]
        [Description("IN ON 颜色设定")]
        public Color color_in_on
        {
            get { return cl_in_on; }
            set { cl_in_on = value; }
        }
        #endregion
    }
}
