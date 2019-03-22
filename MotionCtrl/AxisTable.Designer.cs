using System;
using System.ComponentModel;
using System.Drawing;

namespace MotionCtrl
{
    partial class AxisTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.axis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmd_pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enc_pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.eln = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.elp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.slp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sln = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.inp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.alm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.svron = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tg_pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_go = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_n = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_p = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_home = new System.Windows.Forms.DataGridViewButtonColumn();
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
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.ColumnHeadersHeight = 32;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.axis,
            this.status,
            this.cmd_pos,
            this.enc_pos,
            this.org,
            this.eln,
            this.elp,
            this.slp,
            this.sln,
            this.inp,
            this.alm,
            this.svron,
            this.tg_pos,
            this.btn_go,
            this.btn_n,
            this.btn_p,
            this.btn_home});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.Gray;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(144)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.White;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgv.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            // 
            // tmr_update
            // 
            this.tmr_update.Interval = 300;
            this.tmr_update.Tick += new System.EventHandler(this.tmr_update_Tick);
            // 
            // axis
            // 
            this.axis.HeaderText = "    轴";
            this.axis.Name = "axis";
            this.axis.ReadOnly = true;
            this.axis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.axis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.axis.Width = 140;
            // 
            // status
            // 
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.status.Width = 60;
            // 
            // cmd_pos
            // 
            this.cmd_pos.HeaderText = "命令位置";
            this.cmd_pos.Name = "cmd_pos";
            this.cmd_pos.ReadOnly = true;
            this.cmd_pos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cmd_pos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cmd_pos.Width = 90;
            // 
            // enc_pos
            // 
            this.enc_pos.HeaderText = "反馈位置";
            this.enc_pos.Name = "enc_pos";
            this.enc_pos.ReadOnly = true;
            this.enc_pos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.enc_pos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.enc_pos.Width = 90;
            // 
            // org
            // 
            this.org.HeaderText = "ORG";
            this.org.Name = "org";
            this.org.ReadOnly = true;
            this.org.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.org.Width = 46;
            // 
            // eln
            // 
            this.eln.HeaderText = "EL+";
            this.eln.Name = "eln";
            this.eln.ReadOnly = true;
            this.eln.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eln.Width = 42;
            // 
            // elp
            // 
            this.elp.HeaderText = "EL-";
            this.elp.Name = "elp";
            this.elp.ReadOnly = true;
            this.elp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.elp.Width = 42;
            // 
            // slp
            // 
            this.slp.HeaderText = "SL+";
            this.slp.Name = "slp";
            this.slp.ReadOnly = true;
            this.slp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.slp.Width = 42;
            // 
            // sln
            // 
            this.sln.HeaderText = "SL-";
            this.sln.Name = "sln";
            this.sln.ReadOnly = true;
            this.sln.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.sln.Width = 42;
            // 
            // inp
            // 
            this.inp.HeaderText = "INP";
            this.inp.Name = "inp";
            this.inp.ReadOnly = true;
            this.inp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.inp.Width = 42;
            // 
            // alm
            // 
            this.alm.HeaderText = "ALM";
            this.alm.Name = "alm";
            this.alm.ReadOnly = true;
            this.alm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.alm.Width = 42;
            // 
            // svron
            // 
            this.svron.HeaderText = "ON";
            this.svron.Name = "svron";
            this.svron.ReadOnly = true;
            this.svron.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.svron.Width = 42;
            // 
            // tg_pos
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.tg_pos.DefaultCellStyle = dataGridViewCellStyle9;
            this.tg_pos.HeaderText = "定位";
            this.tg_pos.Name = "tg_pos";
            this.tg_pos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tg_pos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tg_pos.Width = 90;
            // 
            // btn_go
            // 
            this.btn_go.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Transparent;
            this.btn_go.DefaultCellStyle = dataGridViewCellStyle10;
            this.btn_go.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_go.HeaderText = "";
            this.btn_go.Name = "btn_go";
            this.btn_go.ReadOnly = true;
            this.btn_go.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btn_go.Text = "定位";
            this.btn_go.UseColumnTextForButtonValue = true;
            // 
            // btn_n
            // 
            this.btn_n.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Transparent;
            this.btn_n.DefaultCellStyle = dataGridViewCellStyle11;
            this.btn_n.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_n.HeaderText = "";
            this.btn_n.Name = "btn_n";
            this.btn_n.ReadOnly = true;
            this.btn_n.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btn_n.Text = "负向";
            this.btn_n.UseColumnTextForButtonValue = true;
            // 
            // btn_p
            // 
            this.btn_p.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Transparent;
            this.btn_p.DefaultCellStyle = dataGridViewCellStyle12;
            this.btn_p.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_p.HeaderText = "";
            this.btn_p.Name = "btn_p";
            this.btn_p.ReadOnly = true;
            this.btn_p.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btn_p.Text = "正向";
            this.btn_p.UseColumnTextForButtonValue = true;
            // 
            // btn_home
            // 
            this.btn_home.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Transparent;
            this.btn_home.DefaultCellStyle = dataGridViewCellStyle13;
            this.btn_home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_home.HeaderText = "";
            this.btn_home.Name = "btn_home";
            this.btn_home.ReadOnly = true;
            this.btn_home.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btn_home.Text = "回零";
            this.btn_home.UseColumnTextForButtonValue = true;
            // 
            // AxisTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dgv);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AxisTable";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn axis;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmd_pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn enc_pos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn org;
        private System.Windows.Forms.DataGridViewCheckBoxColumn eln;
        private System.Windows.Forms.DataGridViewCheckBoxColumn elp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn slp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sln;
        private System.Windows.Forms.DataGridViewCheckBoxColumn inp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn alm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn svron;
        private System.Windows.Forms.DataGridViewTextBoxColumn tg_pos;
        private System.Windows.Forms.DataGridViewButtonColumn btn_go;
        private System.Windows.Forms.DataGridViewButtonColumn btn_n;
        private System.Windows.Forms.DataGridViewButtonColumn btn_p;
        private System.Windows.Forms.DataGridViewButtonColumn btn_home;

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
