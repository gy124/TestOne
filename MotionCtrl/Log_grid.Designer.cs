namespace MotionCtrl
{
    partial class Log_grid
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示调试信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示正常信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示警告信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示错误信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示系统信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.保存调试信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存正常信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dt,
            this.type,
            this.disc});
            this.dgv.ContextMenuStrip = this.cms;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(2);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 16;
            this.dgv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(553, 442);
            this.dgv.TabIndex = 1;
            // 
            // dt
            // 
            this.dt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dt.FillWeight = 5F;
            this.dt.Frozen = true;
            this.dt.HeaderText = "时间";
            this.dt.Name = "dt";
            this.dt.ReadOnly = true;
            this.dt.Width = 52;
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.type.FillWeight = 5F;
            this.type.Frozen = true;
            this.type.HeaderText = "类型";
            this.type.MinimumWidth = 32;
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 52;
            // 
            // disc
            // 
            this.disc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.disc.FillWeight = 90F;
            this.disc.Frozen = true;
            this.disc.HeaderText = "内容";
            this.disc.Name = "disc";
            this.disc.ReadOnly = true;
            this.disc.Width = 52;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示调试信息ToolStripMenuItem,
            this.显示正常信息ToolStripMenuItem,
            this.显示警告信息ToolStripMenuItem,
            this.显示错误信息ToolStripMenuItem,
            this.显示系统信息ToolStripMenuItem,
            this.toolStripSeparator1,
            this.保存调试信息ToolStripMenuItem,
            this.保存正常信息ToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(149, 164);
            // 
            // 显示调试信息ToolStripMenuItem
            // 
            this.显示调试信息ToolStripMenuItem.Name = "显示调试信息ToolStripMenuItem";
            this.显示调试信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示调试信息ToolStripMenuItem.Text = "显示调试信息";
            // 
            // 显示正常信息ToolStripMenuItem
            // 
            this.显示正常信息ToolStripMenuItem.Name = "显示正常信息ToolStripMenuItem";
            this.显示正常信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示正常信息ToolStripMenuItem.Text = "显示正常信息";
            // 
            // 显示警告信息ToolStripMenuItem
            // 
            this.显示警告信息ToolStripMenuItem.Name = "显示警告信息ToolStripMenuItem";
            this.显示警告信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示警告信息ToolStripMenuItem.Text = "显示警告信息";
            // 
            // 显示错误信息ToolStripMenuItem
            // 
            this.显示错误信息ToolStripMenuItem.Name = "显示错误信息ToolStripMenuItem";
            this.显示错误信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示错误信息ToolStripMenuItem.Text = "显示错误信息";
            // 
            // 显示系统信息ToolStripMenuItem
            // 
            this.显示系统信息ToolStripMenuItem.Name = "显示系统信息ToolStripMenuItem";
            this.显示系统信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示系统信息ToolStripMenuItem.Text = "显示系统信息";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 保存调试信息ToolStripMenuItem
            // 
            this.保存调试信息ToolStripMenuItem.Name = "保存调试信息ToolStripMenuItem";
            this.保存调试信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存调试信息ToolStripMenuItem.Text = "保存调试信息";
            // 
            // 保存正常信息ToolStripMenuItem
            // 
            this.保存正常信息ToolStripMenuItem.Name = "保存正常信息ToolStripMenuItem";
            this.保存正常信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存正常信息ToolStripMenuItem.Text = "保存正常信息";
            // 
            // Log_grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.DoubleBuffered = true;
            this.Name = "Log_grid";
            this.Size = new System.Drawing.Size(553, 442);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem 显示调试信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示正常信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示警告信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示错误信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示系统信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 保存调试信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存正常信息ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn disc;
    }
}
