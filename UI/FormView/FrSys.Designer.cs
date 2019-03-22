namespace UI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tmr_update = new System.Windows.Forms.Timer(this.components);
            this.axTcpClient_cmd = new SocketHelper.AxTcpClient(this.components);
            this.axTcpServer1 = new SocketHelper.AxTcpServer(this.components);
            this.axTcpClient1 = new SocketHelper.AxTcpClient(this.components);
            this.ctb_sys = new CTabControl();
            this.tp_card = new System.Windows.Forms.TabPage();
            this.lb_card_update_ms = new System.Windows.Forms.Label();
            this.CardTable = new MotionCtrl.CardTable();
            this.tp_axis = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PosTable = new MotionCtrl.PosTable();
            this.axiS_Panle1 = new MotionCtrl.AXIS_Panle();
            this.ctb_ax_sel = new CTabControl();
            this.ctp_ax_ul = new System.Windows.Forms.TabPage();
            this.ctp_ax_dl = new System.Windows.Forms.TabPage();
            this.ctp_bullet_move = new System.Windows.Forms.TabPage();
            this.ctp_bullet_feed = new System.Windows.Forms.TabPage();
            this.ctp_bullet_back = new System.Windows.Forms.TabPage();
            this.ctp_ax_ws1 = new System.Windows.Forms.TabPage();
            this.ctp_ax_ws2 = new System.Windows.Forms.TabPage();
            this.ctp_ax_ws5 = new System.Windows.Forms.TabPage();
            this.ctp_ax_all = new System.Windows.Forms.TabPage();
            this.axisTable = new MotionCtrl.AxisTable();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_save_axis_cfg = new System.Windows.Forms.Button();
            this.axisConfig = new MotionCtrl.AxisConfig();
            this.btn_load_axis_cfg = new System.Windows.Forms.Button();
            this.btn_update_card = new System.Windows.Forms.Button();
            this.lb_ax_update_ms = new System.Windows.Forms.Label();
            this.tp_gpio = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.ioTable = new MotionCtrl.IOTable();
            this.cylinderTable = new MotionCtrl.CylinderTable();
            this.ctb_io_type = new CTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lb_cld_update_ms = new System.Windows.Forms.Label();
            this.lb_io_update_ms = new System.Windows.Forms.Label();
            this.tp_vision = new System.Windows.Forms.TabPage();
            this.client_sta_inf = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_send = new System.Windows.Forms.Button();
            this.textBox_recvive = new System.Windows.Forms.TextBox();
            this.textBox_msgSend = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.form1 = new UI.form();
            this.tp_calc = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.axiS_Panle2 = new MotionCtrl.AXIS_Panle();
            this.pos_table_mini_test = new MotionCtrl.pos_table_mini();
            this.iO_table_mini_test = new MotionCtrl.IO_table_mini();
            this.cyl_table_mini_test = new MotionCtrl.cyl_table_mini();
            this.btn_start = new System.Windows.Forms.Button();
            this.cTab_work = new CTabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_get_Yscale_y = new System.Windows.Forms.TextBox();
            this.textBox_get_Yscale_x = new System.Windows.Forms.TextBox();
            this.textBox_get_Xscale_y = new System.Windows.Forms.TextBox();
            this.textBox_get_Xscale_x = new System.Windows.Forms.TextBox();
            this.textBox_Lget_mouth_a = new System.Windows.Forms.TextBox();
            this.textBox_Lget_mouth_y = new System.Windows.Forms.TextBox();
            this.textBox_Lget_mouth_x = new System.Windows.Forms.TextBox();
            this.textBox_Lget_modu_a = new System.Windows.Forms.TextBox();
            this.textBox_Lget_modu_y = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_Lget_modu_x = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_feed_Yscale_y = new System.Windows.Forms.TextBox();
            this.textBox_feed_Yscale_x = new System.Windows.Forms.TextBox();
            this.textBox_feed_Xscale_y = new System.Windows.Forms.TextBox();
            this.textBox_feed_mouth_a = new System.Windows.Forms.TextBox();
            this.textBox_feed_Xscale_x = new System.Windows.Forms.TextBox();
            this.textBox_feed_mouth_y = new System.Windows.Forms.TextBox();
            this.textBox_feed_mouth_x = new System.Windows.Forms.TextBox();
            this.textBox_feed_modu_a = new System.Windows.Forms.TextBox();
            this.textBox_feed_modu_y = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_feed_modu_x = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ctb_sys.SuspendLayout();
            this.tp_card.SuspendLayout();
            this.tp_axis.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ctb_ax_sel.SuspendLayout();
            this.tp_gpio.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.ctb_io_type.SuspendLayout();
            this.tp_vision.SuspendLayout();
            this.tp_calc.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.cTab_work.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1274, 461);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctb_sys);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1268, 455);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(-10, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1298, 2);
            this.panel3.TabIndex = 1;
            // 
            // tmr_update
            // 
            this.tmr_update.Interval = 200;
            this.tmr_update.Tick += new System.EventHandler(this.tmr_update_Tick);
            // 
            // axTcpClient_cmd
            // 
            this.axTcpClient_cmd.Isclosed = false;
            this.axTcpClient_cmd.IsStartTcpthreading = false;
            this.axTcpClient_cmd.Receivestr = null;
            this.axTcpClient_cmd.ReConectedCount = 0;
            this.axTcpClient_cmd.ReConnectionTime = 3000;
            this.axTcpClient_cmd.ServerIp = null;
            this.axTcpClient_cmd.ServerPort = 0;
            this.axTcpClient_cmd.Tcpclient = null;
            this.axTcpClient_cmd.Tcpthread = null;
            this.axTcpClient_cmd.OnReceviceByte += new SocketHelper.AxTcpClient.ReceviceByteEventHandler(this.axTcpClient_cmd_OnReceviceByte);
            this.axTcpClient_cmd.OnErrorMsg += new SocketHelper.AxTcpClient.ErrorMsgEventHandler(this.axTcpClient_cmd_OnErrorMsg);
            this.axTcpClient_cmd.OnStateInfo += new SocketHelper.AxTcpClient.StateInfoEventHandler(this.axTcpClient_cmd_OnStateInfo);
            // 
            // axTcpServer1
            // 
            this.axTcpServer1.ServerIp = "192.168.0.4";
            this.axTcpServer1.ServerPort = 2000;
            this.axTcpServer1.OnOnlineClient += new SocketHelper.AxTcpServer.AddClientEventHandler(this.axTcpServer1_OnOnlineClient);
            // 
            // axTcpClient1
            // 
            this.axTcpClient1.Isclosed = false;
            this.axTcpClient1.IsStartTcpthreading = false;
            this.axTcpClient1.Receivestr = null;
            this.axTcpClient1.ReConectedCount = 0;
            this.axTcpClient1.ReConnectionTime = 3000;
            this.axTcpClient1.ServerIp = "192.168.1.5";
            this.axTcpClient1.ServerPort = 8234;
            this.axTcpClient1.Tcpclient = null;
            this.axTcpClient1.Tcpthread = null;
            // 
            // ctb_sys
            // 
            this.ctb_sys.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.ctb_sys.BackColor = System.Drawing.SystemColors.Control;
            this.ctb_sys.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ctb_sys.Controls.Add(this.tp_card);
            this.ctb_sys.Controls.Add(this.tp_axis);
            this.ctb_sys.Controls.Add(this.tp_gpio);
            this.ctb_sys.Controls.Add(this.tp_vision);
            this.ctb_sys.Controls.Add(this.tp_calc);
            this.ctb_sys.Controls.Add(this.tabPage13);
            this.ctb_sys.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.ctb_sys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctb_sys.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctb_sys.HeaderBackColor = System.Drawing.Color.Transparent;
            this.ctb_sys.HeadSelectedBorderColor = System.Drawing.Color.Transparent;
            this.ctb_sys.ItemSize = new System.Drawing.Size(200, 100);
            this.ctb_sys.Location = new System.Drawing.Point(0, 0);
            this.ctb_sys.Multiline = true;
            this.ctb_sys.Name = "ctb_sys";
            this.ctb_sys.SelectedIndex = 0;
            this.ctb_sys.Size = new System.Drawing.Size(1268, 455);
            this.ctb_sys.TabIndex = 2;
            this.ctb_sys.SelectedIndexChanged += new System.EventHandler(this.ctb_sys_SelectedIndexChanged);
            // 
            // tp_card
            // 
            this.tp_card.Controls.Add(this.lb_card_update_ms);
            this.tp_card.Controls.Add(this.CardTable);
            this.tp_card.Location = new System.Drawing.Point(104, 4);
            this.tp_card.Name = "tp_card";
            this.tp_card.Size = new System.Drawing.Size(1160, 447);
            this.tp_card.TabIndex = 6;
            this.tp_card.Text = "板卡状态";
            this.tp_card.UseVisualStyleBackColor = true;
            // 
            // lb_card_update_ms
            // 
            this.lb_card_update_ms.AutoSize = true;
            this.lb_card_update_ms.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_card_update_ms.Location = new System.Drawing.Point(1016, 22);
            this.lb_card_update_ms.Name = "lb_card_update_ms";
            this.lb_card_update_ms.Size = new System.Drawing.Size(32, 16);
            this.lb_card_update_ms.TabIndex = 23;
            this.lb_card_update_ms.Text = "0ms";
            // 
            // CardTable
            // 
            this.CardTable.BackColor = System.Drawing.Color.Transparent;
            this.CardTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CardTable.Location = new System.Drawing.Point(29, 42);
            this.CardTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CardTable.Name = "CardTable";
            this.CardTable.Size = new System.Drawing.Size(1019, 347);
            this.CardTable.TabIndex = 0;
            this.CardTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.CardTable.TableBackgColor = System.Drawing.SystemColors.Window;
            this.CardTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CardTable.TableRowColor = System.Drawing.SystemColors.Window;
            this.CardTable.TableRowHight = 32;
            // 
            // tp_axis
            // 
            this.tp_axis.Controls.Add(this.tableLayoutPanel4);
            this.tp_axis.Controls.Add(this.btn_update_card);
            this.tp_axis.Controls.Add(this.lb_ax_update_ms);
            this.tp_axis.Location = new System.Drawing.Point(104, 4);
            this.tp_axis.Name = "tp_axis";
            this.tp_axis.Size = new System.Drawing.Size(1160, 447);
            this.tp_axis.TabIndex = 2;
            this.tp_axis.Text = "轴参数  ";
            this.tp_axis.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.ctb_ax_sel, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.axisTable, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.btn_stop, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.btn_save_axis_cfg, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.axisConfig, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.btn_load_axis_cfg, 1, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1160, 447);
            this.tableLayoutPanel4.TabIndex = 29;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel5.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.axiS_Panle1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 301);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1074, 123);
            this.tableLayoutPanel5.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PosTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 117);
            this.panel1.TabIndex = 33;
            // 
            // PosTable
            // 
            this.PosTable.AutoSize = true;
            this.PosTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PosTable.Location = new System.Drawing.Point(0, 0);
            this.PosTable.Margin = new System.Windows.Forms.Padding(9);
            this.PosTable.Name = "PosTable";
            this.PosTable.Size = new System.Drawing.Size(767, 117);
            this.PosTable.TabIndex = 32;
            // 
            // axiS_Panle1
            // 
            this.axiS_Panle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axiS_Panle1.Location = new System.Drawing.Point(777, 4);
            this.axiS_Panle1.Margin = new System.Windows.Forms.Padding(4);
            this.axiS_Panle1.Name = "axiS_Panle1";
            this.axiS_Panle1.Size = new System.Drawing.Size(293, 115);
            this.axiS_Panle1.TabIndex = 28;
            // 
            // ctb_ax_sel
            // 
            this.ctb_ax_sel.BackColor = System.Drawing.SystemColors.Control;
            this.ctb_ax_sel.BorderColor = System.Drawing.SystemColors.Control;
            this.ctb_ax_sel.Controls.Add(this.ctp_ax_ul);
            this.ctb_ax_sel.Controls.Add(this.ctp_ax_dl);
            this.ctb_ax_sel.Controls.Add(this.ctp_bullet_move);
            this.ctb_ax_sel.Controls.Add(this.ctp_bullet_feed);
            this.ctb_ax_sel.Controls.Add(this.ctp_bullet_back);
            this.ctb_ax_sel.Controls.Add(this.ctp_ax_ws1);
            this.ctb_ax_sel.Controls.Add(this.ctp_ax_ws2);
            this.ctb_ax_sel.Controls.Add(this.ctp_ax_ws5);
            this.ctb_ax_sel.Controls.Add(this.ctp_ax_all);
            this.ctb_ax_sel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctb_ax_sel.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.ctb_ax_sel.ItemSize = new System.Drawing.Size(60, 32);
            this.ctb_ax_sel.Location = new System.Drawing.Point(3, 3);
            this.ctb_ax_sel.Name = "ctb_ax_sel";
            this.ctb_ax_sel.SelectedIndex = 0;
            this.ctb_ax_sel.Size = new System.Drawing.Size(1074, 34);
            this.ctb_ax_sel.TabIndex = 23;
            this.ctb_ax_sel.SelectedIndexChanged += new System.EventHandler(this.ctb_ax_sel_SelectedIndexChanged);
            // 
            // ctp_ax_ul
            // 
            this.ctp_ax_ul.Location = new System.Drawing.Point(4, 36);
            this.ctp_ax_ul.Name = "ctp_ax_ul";
            this.ctp_ax_ul.Padding = new System.Windows.Forms.Padding(3);
            this.ctp_ax_ul.Size = new System.Drawing.Size(1066, 0);
            this.ctp_ax_ul.TabIndex = 0;
            this.ctp_ax_ul.Text = "      ";
            this.ctp_ax_ul.UseVisualStyleBackColor = true;
            // 
            // ctp_ax_dl
            // 
            this.ctp_ax_dl.Location = new System.Drawing.Point(4, 36);
            this.ctp_ax_dl.Name = "ctp_ax_dl";
            this.ctp_ax_dl.Padding = new System.Windows.Forms.Padding(3);
            this.ctp_ax_dl.Size = new System.Drawing.Size(0, 0);
            this.ctp_ax_dl.TabIndex = 1;
            this.ctp_ax_dl.Text = "      ";
            this.ctp_ax_dl.UseVisualStyleBackColor = true;
            // 
            // ctp_bullet_move
            // 
            this.ctp_bullet_move.Location = new System.Drawing.Point(4, 36);
            this.ctp_bullet_move.Name = "ctp_bullet_move";
            this.ctp_bullet_move.Size = new System.Drawing.Size(0, 0);
            this.ctp_bullet_move.TabIndex = 2;
            this.ctp_bullet_move.Text = " 移栽轴   ";
            this.ctp_bullet_move.UseVisualStyleBackColor = true;
            // 
            // ctp_bullet_feed
            // 
            this.ctp_bullet_feed.Location = new System.Drawing.Point(4, 36);
            this.ctp_bullet_feed.Name = "ctp_bullet_feed";
            this.ctp_bullet_feed.Size = new System.Drawing.Size(0, 0);
            this.ctp_bullet_feed.TabIndex = 3;
            this.ctp_bullet_feed.Text = "弹夹上料     ";
            this.ctp_bullet_feed.UseVisualStyleBackColor = true;
            // 
            // ctp_bullet_back
            // 
            this.ctp_bullet_back.Location = new System.Drawing.Point(4, 36);
            this.ctp_bullet_back.Name = "ctp_bullet_back";
            this.ctp_bullet_back.Size = new System.Drawing.Size(0, 0);
            this.ctp_bullet_back.TabIndex = 9;
            this.ctp_bullet_back.Text = "弹夹收料 ";
            this.ctp_bullet_back.UseVisualStyleBackColor = true;
            // 
            // ctp_ax_ws1
            // 
            this.ctp_ax_ws1.Location = new System.Drawing.Point(4, 36);
            this.ctp_ax_ws1.Name = "ctp_ax_ws1";
            this.ctp_ax_ws1.Size = new System.Drawing.Size(0, 0);
            this.ctp_ax_ws1.TabIndex = 4;
            this.ctp_ax_ws1.Text = "转盘取料      ";
            this.ctp_ax_ws1.UseVisualStyleBackColor = true;
            // 
            // ctp_ax_ws2
            // 
            this.ctp_ax_ws2.Location = new System.Drawing.Point(4, 36);
            this.ctp_ax_ws2.Name = "ctp_ax_ws2";
            this.ctp_ax_ws2.Size = new System.Drawing.Size(0, 0);
            this.ctp_ax_ws2.TabIndex = 5;
            this.ctp_ax_ws2.Text = "转盘上料      ";
            this.ctp_ax_ws2.UseVisualStyleBackColor = true;
            // 
            // ctp_ax_ws5
            // 
            this.ctp_ax_ws5.Location = new System.Drawing.Point(4, 36);
            this.ctp_ax_ws5.Name = "ctp_ax_ws5";
            this.ctp_ax_ws5.Size = new System.Drawing.Size(0, 0);
            this.ctp_ax_ws5.TabIndex = 6;
            this.ctp_ax_ws5.Text = "转盘收料     ";
            this.ctp_ax_ws5.UseVisualStyleBackColor = true;
            // 
            // ctp_ax_all
            // 
            this.ctp_ax_all.Location = new System.Drawing.Point(4, 36);
            this.ctp_ax_all.Name = "ctp_ax_all";
            this.ctp_ax_all.Size = new System.Drawing.Size(0, 0);
            this.ctp_ax_all.TabIndex = 7;
            this.ctp_ax_all.Text = "所有轴      ";
            this.ctp_ax_all.UseVisualStyleBackColor = true;
            // 
            // axisTable
            // 
            this.axisTable.BackColor = System.Drawing.Color.Transparent;
            this.axisTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisTable.Location = new System.Drawing.Point(3, 44);
            this.axisTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axisTable.Name = "axisTable";
            this.axisTable.Size = new System.Drawing.Size(1074, 121);
            this.axisTable.TabIndex = 0;
            this.axisTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.axisTable.TableBackgColor = System.Drawing.SystemColors.Window;
            this.axisTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisTable.TableRowColor = System.Drawing.SystemColors.Window;
            this.axisTable.TableRowHight = 32;
  
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Gold;
            this.btn_stop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_stop.Location = new System.Drawing.Point(1083, 301);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(74, 123);
            this.btn_stop.TabIndex = 25;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_save_axis_cfg
            // 
            this.btn_save_axis_cfg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_save_axis_cfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_save_axis_cfg.Location = new System.Drawing.Point(1083, 43);
            this.btn_save_axis_cfg.Name = "btn_save_axis_cfg";
            this.btn_save_axis_cfg.Size = new System.Drawing.Size(74, 123);
            this.btn_save_axis_cfg.TabIndex = 19;
            this.btn_save_axis_cfg.Text = "保存";
            this.btn_save_axis_cfg.UseVisualStyleBackColor = false;
            this.btn_save_axis_cfg.Click += new System.EventHandler(this.btn_save_axis_cfg_Click);
            // 
            // axisConfig
            // 
            this.axisConfig.BackColor = System.Drawing.Color.Transparent;
            this.axisConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisConfig.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisConfig.Location = new System.Drawing.Point(3, 173);
            this.axisConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axisConfig.Name = "axisConfig";
            this.axisConfig.Size = new System.Drawing.Size(1074, 121);
            this.axisConfig.TabIndex = 1;
            this.axisConfig.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.axisConfig.TableBackgColor = System.Drawing.SystemColors.Window;
            this.axisConfig.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.axisConfig.TableRowColor = System.Drawing.SystemColors.Window;
            this.axisConfig.TableRowHight = 32;
            // 
            // btn_load_axis_cfg
            // 
            this.btn_load_axis_cfg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_load_axis_cfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_load_axis_cfg.Location = new System.Drawing.Point(1083, 172);
            this.btn_load_axis_cfg.Name = "btn_load_axis_cfg";
            this.btn_load_axis_cfg.Size = new System.Drawing.Size(74, 123);
            this.btn_load_axis_cfg.TabIndex = 20;
            this.btn_load_axis_cfg.Text = "加载";
            this.btn_load_axis_cfg.UseVisualStyleBackColor = false;
            this.btn_load_axis_cfg.Click += new System.EventHandler(this.btn_load_axis_cfg_Click);
            // 
            // btn_update_card
            // 
            this.btn_update_card.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_update_card.Location = new System.Drawing.Point(445, 778);
            this.btn_update_card.Name = "btn_update_card";
            this.btn_update_card.Size = new System.Drawing.Size(134, 71);
            this.btn_update_card.TabIndex = 26;
            this.btn_update_card.Text = "更新到控制器";
            this.btn_update_card.UseVisualStyleBackColor = false;
            this.btn_update_card.Click += new System.EventHandler(this.btn_update_card_Click);
            // 
            // lb_ax_update_ms
            // 
            this.lb_ax_update_ms.AutoSize = true;
            this.lb_ax_update_ms.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_ax_update_ms.Location = new System.Drawing.Point(1003, 61);
            this.lb_ax_update_ms.Name = "lb_ax_update_ms";
            this.lb_ax_update_ms.Size = new System.Drawing.Size(32, 16);
            this.lb_ax_update_ms.TabIndex = 24;
            this.lb_ax_update_ms.Text = "0ms";
            // 
            // tp_gpio
            // 
            this.tp_gpio.Controls.Add(this.tableLayoutPanel6);
            this.tp_gpio.Controls.Add(this.lb_cld_update_ms);
            this.tp_gpio.Controls.Add(this.lb_io_update_ms);
            this.tp_gpio.Location = new System.Drawing.Point(104, 4);
            this.tp_gpio.Name = "tp_gpio";
            this.tp_gpio.Padding = new System.Windows.Forms.Padding(3);
            this.tp_gpio.Size = new System.Drawing.Size(1160, 447);
            this.tp_gpio.TabIndex = 0;
            this.tp_gpio.Text = "输入输出";
            this.tp_gpio.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.ctb_io_type, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(54, 86);
            this.tableLayoutPanel6.TabIndex = 28;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.ioTable, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cylinderTable, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(48, 40);
            this.tableLayoutPanel7.TabIndex = 29;
            // 
            // ioTable
            // 
            this.ioTable.BackColor = System.Drawing.Color.Transparent;
            this.ioTable.color_in_on = System.Drawing.Color.Orange;
            this.ioTable.color_out_on = System.Drawing.Color.Lime;
            this.ioTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ioTable.Location = new System.Drawing.Point(3, 4);
            this.ioTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ioTable.Name = "ioTable";
            this.ioTable.Size = new System.Drawing.Size(18, 32);
            this.ioTable.TabIndex = 19;
            this.ioTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.ioTable.TableBackgColor = System.Drawing.SystemColors.Window;
            this.ioTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ioTable.TableRowColor = System.Drawing.SystemColors.Window;
            this.ioTable.TableRowHight = 32;
            // 
            // cylinderTable
            // 
            this.cylinderTable.BackColor = System.Drawing.Color.Transparent;
            this.cylinderTable.color_in_on = System.Drawing.Color.Orange;
            this.cylinderTable.color_out_on = System.Drawing.Color.Lime;
            this.cylinderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cylinderTable.HeaderBackgColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cylinderTable.Location = new System.Drawing.Point(27, 4);
            this.cylinderTable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cylinderTable.Name = "cylinderTable";
            this.cylinderTable.Size = new System.Drawing.Size(18, 32);
            this.cylinderTable.TabIndex = 20;
            this.cylinderTable.TableAltRowColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.cylinderTable.TableBackgColor = System.Drawing.SystemColors.Window;
            this.cylinderTable.TableFontSet = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cylinderTable.TableRowColor = System.Drawing.SystemColors.Window;
            this.cylinderTable.TableRowHight = 32;
            // 
            // ctb_io_type
            // 
            this.ctb_io_type.BackColor = System.Drawing.SystemColors.Control;
            this.ctb_io_type.BorderColor = System.Drawing.SystemColors.Control;
            this.ctb_io_type.Controls.Add(this.tabPage1);
            this.ctb_io_type.Controls.Add(this.tabPage2);
            this.ctb_io_type.Controls.Add(this.tabPage3);
            this.ctb_io_type.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctb_io_type.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.ctb_io_type.ItemSize = new System.Drawing.Size(60, 32);
            this.ctb_io_type.Location = new System.Drawing.Point(3, 3);
            this.ctb_io_type.Name = "ctb_io_type";
            this.ctb_io_type.SelectedIndex = 0;
            this.ctb_io_type.Size = new System.Drawing.Size(48, 34);
            this.ctb_io_type.TabIndex = 27;
            this.ctb_io_type.SelectedIndexChanged += new System.EventHandler(this.ctb_io_type_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(40, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "输出      ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(40, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输入      ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 36);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(40, 0);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "所有      ";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lb_cld_update_ms
            // 
            this.lb_cld_update_ms.AutoSize = true;
            this.lb_cld_update_ms.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_cld_update_ms.Location = new System.Drawing.Point(1008, 457);
            this.lb_cld_update_ms.Name = "lb_cld_update_ms";
            this.lb_cld_update_ms.Size = new System.Drawing.Size(32, 16);
            this.lb_cld_update_ms.TabIndex = 26;
            this.lb_cld_update_ms.Text = "0ms";
            // 
            // lb_io_update_ms
            // 
            this.lb_io_update_ms.AutoSize = true;
            this.lb_io_update_ms.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_io_update_ms.Location = new System.Drawing.Point(1008, 31);
            this.lb_io_update_ms.Name = "lb_io_update_ms";
            this.lb_io_update_ms.Size = new System.Drawing.Size(32, 16);
            this.lb_io_update_ms.TabIndex = 25;
            this.lb_io_update_ms.Text = "0ms";
            // 
            // tp_vision
            // 
            this.tp_vision.Controls.Add(this.client_sta_inf);
            this.tp_vision.Controls.Add(this.button4);
            this.tp_vision.Controls.Add(this.button3);
            this.tp_vision.Controls.Add(this.button1);
            this.tp_vision.Controls.Add(this.label4);
            this.tp_vision.Controls.Add(this.label3);
            this.tp_vision.Controls.Add(this.bt_send);
            this.tp_vision.Controls.Add(this.textBox_recvive);
            this.tp_vision.Controls.Add(this.textBox_msgSend);
            this.tp_vision.Controls.Add(this.button2);
            this.tp_vision.Controls.Add(this.textBox_port);
            this.tp_vision.Controls.Add(this.label2);
            this.tp_vision.Controls.Add(this.textBox_IP);
            this.tp_vision.Controls.Add(this.label1);
            this.tp_vision.Controls.Add(this.form1);
            this.tp_vision.Location = new System.Drawing.Point(104, 4);
            this.tp_vision.Name = "tp_vision";
            this.tp_vision.Size = new System.Drawing.Size(1160, 447);
            this.tp_vision.TabIndex = 4;
            this.tp_vision.Text = "通讯";
            this.tp_vision.UseVisualStyleBackColor = true;
            // 
            // client_sta_inf
            // 
            this.client_sta_inf.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.client_sta_inf.Location = new System.Drawing.Point(359, 100);
            this.client_sta_inf.Name = "client_sta_inf";
            this.client_sta_inf.Size = new System.Drawing.Size(43, 34);
            this.client_sta_inf.TabIndex = 26;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(776, 124);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 43);
            this.button4.TabIndex = 25;
            this.button4.Text = "打开客户端";
            this.button4.UseVisualStyleBackColor = true;      
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(785, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 43);
            this.button3.TabIndex = 24;
            this.button3.Text = "打开服务器";
            this.button3.UseVisualStyleBackColor = true;
        
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(592, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(560, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "接收";
            // 
            // bt_send
            // 
            this.bt_send.Location = new System.Drawing.Point(554, 162);
            this.bt_send.Name = "bt_send";
            this.bt_send.Size = new System.Drawing.Size(75, 23);
            this.bt_send.TabIndex = 19;
            this.bt_send.Text = "发送";
            this.bt_send.UseVisualStyleBackColor = true;
            this.bt_send.Click += new System.EventHandler(this.bt_send_Click);
            // 
            // textBox_recvive
            // 
            this.textBox_recvive.Location = new System.Drawing.Point(359, 210);
            this.textBox_recvive.Name = "textBox_recvive";
            this.textBox_recvive.Size = new System.Drawing.Size(189, 26);
            this.textBox_recvive.TabIndex = 18;
            // 
            // textBox_msgSend
            // 
            this.textBox_msgSend.Location = new System.Drawing.Point(359, 162);
            this.textBox_msgSend.Name = "textBox_msgSend";
            this.textBox_msgSend.Size = new System.Drawing.Size(189, 26);
            this.textBox_msgSend.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(554, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "启动";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(511, 41);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(59, 26);
            this.textBox_port.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(508, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "端口";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(359, 41);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(118, 26);
            this.textBox_IP.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "ip";
       
            // 
            // form1
            // 
            this.form1.Location = new System.Drawing.Point(16, 6);
            this.form1.Margin = new System.Windows.Forms.Padding(4);
            this.form1.Name = "form1";
            this.form1.Size = new System.Drawing.Size(231, 379);
            this.form1.TabIndex = 9;
            // 
            // tp_calc
            // 
            this.tp_calc.Controls.Add(this.tableLayoutPanel3);
            this.tp_calc.Location = new System.Drawing.Point(104, 4);
            this.tp_calc.Name = "tp_calc";
            this.tp_calc.Size = new System.Drawing.Size(1160, 447);
            this.tp_calc.TabIndex = 5;
            this.tp_calc.Text = "系统校准";
            this.tp_calc.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.cTab_work, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(60, 92);
            this.tableLayoutPanel3.TabIndex = 26;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.Controls.Add(this.axiS_Panle2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pos_table_mini_test, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.iO_table_mini_test, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cyl_table_mini_test, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_start, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(54, 36);
            this.tableLayoutPanel2.TabIndex = 30;
            // 
            // axiS_Panle2
            // 
            this.axiS_Panle2.Location = new System.Drawing.Point(5, 23);
            this.axiS_Panle2.Margin = new System.Windows.Forms.Padding(5);
            this.axiS_Panle2.Name = "axiS_Panle2";
            this.axiS_Panle2.Size = new System.Drawing.Size(1, 8);
            this.axiS_Panle2.TabIndex = 29;
            // 
            // pos_table_mini_test
            // 
            this.pos_table_mini_test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pos_table_mini_test.Location = new System.Drawing.Point(5, 5);
            this.pos_table_mini_test.Margin = new System.Windows.Forms.Padding(5);
            this.pos_table_mini_test.Name = "pos_table_mini_test";
            this.pos_table_mini_test.Size = new System.Drawing.Size(1, 8);
            this.pos_table_mini_test.TabIndex = 1;
            // 
            // iO_table_mini_test
            // 
            this.iO_table_mini_test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iO_table_mini_test.Location = new System.Drawing.Point(5, 5);
            this.iO_table_mini_test.Margin = new System.Windows.Forms.Padding(5);
            this.iO_table_mini_test.Name = "iO_table_mini_test";
            this.iO_table_mini_test.Size = new System.Drawing.Size(1, 8);
            this.iO_table_mini_test.TabIndex = 0;
            // 
            // cyl_table_mini_test
            // 
            this.cyl_table_mini_test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cyl_table_mini_test.Location = new System.Drawing.Point(5, 23);
            this.cyl_table_mini_test.Margin = new System.Windows.Forms.Padding(5);
            this.cyl_table_mini_test.Name = "cyl_table_mini_test";
            this.cyl_table_mini_test.Size = new System.Drawing.Size(1, 8);
            this.cyl_table_mini_test.TabIndex = 2;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(3, 3);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(45, 12);
            this.btn_start.TabIndex = 25;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = true;
          
            // 
            // cTab_work
            // 
            this.cTab_work.BackColor = System.Drawing.SystemColors.Control;
            this.cTab_work.BorderColor = System.Drawing.SystemColors.Control;
            this.cTab_work.Controls.Add(this.tabPage4);
            this.cTab_work.Controls.Add(this.tabPage5);
            this.cTab_work.Controls.Add(this.tabPage6);
            this.cTab_work.Controls.Add(this.tabPage7);
            this.cTab_work.Controls.Add(this.tabPage8);
            this.cTab_work.Controls.Add(this.tabPage9);
            this.cTab_work.Controls.Add(this.tabPage10);
            this.cTab_work.Controls.Add(this.tabPage11);
            this.cTab_work.Controls.Add(this.tabPage12);
            this.cTab_work.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTab_work.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.cTab_work.ItemSize = new System.Drawing.Size(60, 32);
            this.cTab_work.Location = new System.Drawing.Point(3, 3);
            this.cTab_work.Name = "cTab_work";
            this.cTab_work.SelectedIndex = 0;
            this.cTab_work.Size = new System.Drawing.Size(54, 44);
            this.cTab_work.TabIndex = 24;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 36);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(46, 4);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "      ";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 36);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(46, 4);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "      ";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 36);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(46, 4);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = " 移栽轴   ";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 36);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(46, 4);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "弹夹上料     ";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 36);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(46, 4);
            this.tabPage8.TabIndex = 9;
            this.tabPage8.Text = "弹夹收料Z ";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 36);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(46, 4);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "转盘收料      ";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 36);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(46, 4);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "转盘上料      ";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(4, 36);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(46, 4);
            this.tabPage11.TabIndex = 6;
            this.tabPage11.Text = "转盘取料     ";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // tabPage12
            // 
            this.tabPage12.Location = new System.Drawing.Point(4, 36);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Size = new System.Drawing.Size(46, 4);
            this.tabPage12.TabIndex = 7;
            this.tabPage12.Text = "所有轴      ";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.panel5);
            this.tabPage13.Controls.Add(this.panel4);
            this.tabPage13.Location = new System.Drawing.Point(104, 4);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(1160, 447);
            this.tabPage13.TabIndex = 7;
            this.tabPage13.Text = "视觉参数";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.textBox_get_Yscale_y);
            this.panel5.Controls.Add(this.textBox_get_Yscale_x);
            this.panel5.Controls.Add(this.textBox_get_Xscale_y);
            this.panel5.Controls.Add(this.textBox_get_Xscale_x);
            this.panel5.Controls.Add(this.textBox_Lget_mouth_a);
            this.panel5.Controls.Add(this.textBox_Lget_mouth_y);
            this.panel5.Controls.Add(this.textBox_Lget_mouth_x);
            this.panel5.Controls.Add(this.textBox_Lget_modu_a);
            this.panel5.Controls.Add(this.textBox_Lget_modu_y);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.textBox_Lget_modu_x);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Location = new System.Drawing.Point(35, 235);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(320, 206);
            this.panel5.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(72, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(152, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "左取料模板视觉参数";

            // 
            // textBox_get_Yscale_y
            // 
            this.textBox_get_Yscale_y.Location = new System.Drawing.Point(156, 161);
            this.textBox_get_Yscale_y.Name = "textBox_get_Yscale_y";
            this.textBox_get_Yscale_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_get_Yscale_y.TabIndex = 19;
            // 
            // textBox_get_Yscale_x
            // 
            this.textBox_get_Yscale_x.Location = new System.Drawing.Point(75, 161);
            this.textBox_get_Yscale_x.Name = "textBox_get_Yscale_x";
            this.textBox_get_Yscale_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_get_Yscale_x.TabIndex = 18;
            // 
            // textBox_get_Xscale_y
            // 
            this.textBox_get_Xscale_y.Location = new System.Drawing.Point(156, 129);
            this.textBox_get_Xscale_y.Name = "textBox_get_Xscale_y";
            this.textBox_get_Xscale_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_get_Xscale_y.TabIndex = 16;
            // 
            // textBox_get_Xscale_x
            // 
            this.textBox_get_Xscale_x.Location = new System.Drawing.Point(75, 129);
            this.textBox_get_Xscale_x.Name = "textBox_get_Xscale_x";
            this.textBox_get_Xscale_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_get_Xscale_x.TabIndex = 15;
            // 
            // textBox_Lget_mouth_a
            // 
            this.textBox_Lget_mouth_a.Location = new System.Drawing.Point(245, 94);
            this.textBox_Lget_mouth_a.Name = "textBox_Lget_mouth_a";
            this.textBox_Lget_mouth_a.Size = new System.Drawing.Size(61, 26);
            this.textBox_Lget_mouth_a.TabIndex = 14;
            // 
            // textBox_Lget_mouth_y
            // 
            this.textBox_Lget_mouth_y.Location = new System.Drawing.Point(156, 94);
            this.textBox_Lget_mouth_y.Name = "textBox_Lget_mouth_y";
            this.textBox_Lget_mouth_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_Lget_mouth_y.TabIndex = 13;
            // 
            // textBox_Lget_mouth_x
            // 
            this.textBox_Lget_mouth_x.Location = new System.Drawing.Point(75, 94);
            this.textBox_Lget_mouth_x.Name = "textBox_Lget_mouth_x";
            this.textBox_Lget_mouth_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_Lget_mouth_x.TabIndex = 12;
            // 
            // textBox_Lget_modu_a
            // 
            this.textBox_Lget_modu_a.Location = new System.Drawing.Point(245, 59);
            this.textBox_Lget_modu_a.Name = "textBox_Lget_modu_a";
            this.textBox_Lget_modu_a.Size = new System.Drawing.Size(61, 26);
            this.textBox_Lget_modu_a.TabIndex = 11;
            // 
            // textBox_Lget_modu_y
            // 
            this.textBox_Lget_modu_y.Location = new System.Drawing.Point(156, 59);
            this.textBox_Lget_modu_y.Name = "textBox_Lget_modu_y";
            this.textBox_Lget_modu_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_Lget_modu_y.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "Y比例";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "X比例";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 3;
            this.label11.Text = "旋转中心";
            // 
            // textBox_Lget_modu_x
            // 
            this.textBox_Lget_modu_x.Location = new System.Drawing.Point(75, 59);
            this.textBox_Lget_modu_x.Name = "textBox_Lget_modu_x";
            this.textBox_Lget_modu_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_Lget_modu_x.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 1;
            this.label12.Text = "模板";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button6);
            this.panel4.Controls.Add(this.button5);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.textBox_feed_Yscale_y);
            this.panel4.Controls.Add(this.textBox_feed_Yscale_x);
            this.panel4.Controls.Add(this.textBox_feed_Xscale_y);
            this.panel4.Controls.Add(this.textBox_feed_mouth_a);
            this.panel4.Controls.Add(this.textBox_feed_Xscale_x);
            this.panel4.Controls.Add(this.textBox_feed_mouth_y);
            this.panel4.Controls.Add(this.textBox_feed_mouth_x);
            this.panel4.Controls.Add(this.textBox_feed_modu_a);
            this.panel4.Controls.Add(this.textBox_feed_modu_y);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.textBox_feed_modu_x);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Location = new System.Drawing.Point(35, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(320, 209);
            this.panel4.TabIndex = 2;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(244, 167);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(73, 33);
            this.button6.TabIndex = 22;
            this.button6.Text = "获取";
            this.button6.UseVisualStyleBackColor = true;
     
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(244, 128);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 33);
            this.button5.TabIndex = 21;
            this.button5.Text = "保存";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(72, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 16);
            this.label13.TabIndex = 20;
            this.label13.Text = "上料模板视觉参数";
            // 
            // textBox_feed_Yscale_y
            // 
            this.textBox_feed_Yscale_y.Location = new System.Drawing.Point(155, 160);
            this.textBox_feed_Yscale_y.Name = "textBox_feed_Yscale_y";
            this.textBox_feed_Yscale_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_feed_Yscale_y.TabIndex = 19;
            // 
            // textBox_feed_Yscale_x
            // 
            this.textBox_feed_Yscale_x.Location = new System.Drawing.Point(74, 160);
            this.textBox_feed_Yscale_x.Name = "textBox_feed_Yscale_x";
            this.textBox_feed_Yscale_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_feed_Yscale_x.TabIndex = 18;
            // 
            // textBox_feed_Xscale_y
            // 
            this.textBox_feed_Xscale_y.Location = new System.Drawing.Point(155, 128);
            this.textBox_feed_Xscale_y.Name = "textBox_feed_Xscale_y";
            this.textBox_feed_Xscale_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_feed_Xscale_y.TabIndex = 16;
            // 
            // textBox_feed_mouth_a
            // 
            this.textBox_feed_mouth_a.Location = new System.Drawing.Point(244, 93);
            this.textBox_feed_mouth_a.Name = "textBox_feed_mouth_a";
            this.textBox_feed_mouth_a.Size = new System.Drawing.Size(61, 26);
            this.textBox_feed_mouth_a.TabIndex = 14;
            // 
            // textBox_feed_Xscale_x
            // 
            this.textBox_feed_Xscale_x.Location = new System.Drawing.Point(74, 128);
            this.textBox_feed_Xscale_x.Name = "textBox_feed_Xscale_x";
            this.textBox_feed_Xscale_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_feed_Xscale_x.TabIndex = 15;
            // 
            // textBox_feed_mouth_y
            // 
            this.textBox_feed_mouth_y.Location = new System.Drawing.Point(155, 93);
            this.textBox_feed_mouth_y.Name = "textBox_feed_mouth_y";
            this.textBox_feed_mouth_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_feed_mouth_y.TabIndex = 13;
            // 
            // textBox_feed_mouth_x
            // 
            this.textBox_feed_mouth_x.Location = new System.Drawing.Point(74, 93);
            this.textBox_feed_mouth_x.Name = "textBox_feed_mouth_x";
            this.textBox_feed_mouth_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_feed_mouth_x.TabIndex = 12;
            // 
            // textBox_feed_modu_a
            // 
            this.textBox_feed_modu_a.Location = new System.Drawing.Point(244, 58);
            this.textBox_feed_modu_a.Name = "textBox_feed_modu_a";
            this.textBox_feed_modu_a.Size = new System.Drawing.Size(61, 26);
            this.textBox_feed_modu_a.TabIndex = 11;
            // 
            // textBox_feed_modu_y
            // 
            this.textBox_feed_modu_y.Location = new System.Drawing.Point(155, 58);
            this.textBox_feed_modu_y.Name = "textBox_feed_modu_y";
            this.textBox_feed_modu_y.Size = new System.Drawing.Size(65, 26);
            this.textBox_feed_modu_y.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Y比例";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "X比例";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "旋转中心";
            // 
            // textBox_feed_modu_x
            // 
            this.textBox_feed_modu_x.Location = new System.Drawing.Point(74, 58);
            this.textBox_feed_modu_x.Name = "textBox_feed_modu_x";
            this.textBox_feed_modu_x.Size = new System.Drawing.Size(61, 26);
            this.textBox_feed_modu_x.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "模板";
            // 
            // FrSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrSys";
            this.Text = "FrSys";
            this.Load += new System.EventHandler(this.FrSys_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ctb_sys.ResumeLayout(false);
            this.tp_card.ResumeLayout(false);
            this.tp_card.PerformLayout();
            this.tp_axis.ResumeLayout(false);
            this.tp_axis.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ctb_ax_sel.ResumeLayout(false);
            this.tp_gpio.ResumeLayout(false);
            this.tp_gpio.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.ctb_io_type.ResumeLayout(false);
            this.tp_vision.ResumeLayout(false);
            this.tp_vision.PerformLayout();
            this.tp_calc.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.cTab_work.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabPage tp_gpio;
        private System.Windows.Forms.TabPage tp_axis;
        private System.Windows.Forms.TabPage tp_vision;
        public CTabControl ctb_sys;
        private System.Windows.Forms.Timer tmr_update;
        private System.Windows.Forms.TabPage tp_calc;
        private System.Windows.Forms.Button btn_load_axis_cfg;
        private System.Windows.Forms.Button btn_save_axis_cfg;
        private MotionCtrl.AxisConfig axisConfig;
        private MotionCtrl.AxisTable axisTable;
        //private System.Windows.Forms.TabPage ctp_ax_ws3;
        //private System.Windows.Forms.TabPage ctp_ax_ws4;
        private System.Windows.Forms.TabPage tp_card;
        private MotionCtrl.CardTable CardTable;
        private MotionCtrl.CylinderTable cylinderTable;
        private MotionCtrl.IOTable ioTable;
        private System.Windows.Forms.Label lb_card_update_ms;
        private System.Windows.Forms.Label lb_ax_update_ms;
        private System.Windows.Forms.Label lb_cld_update_ms;
        private System.Windows.Forms.Label lb_io_update_ms;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_update_card;
        private CTabControl ctb_io_type;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private MotionCtrl.AXIS_Panle axiS_Panle1;
        private MotionCtrl.pos_table_mini pos_table_mini_test;
        private MotionCtrl.IO_table_mini iO_table_mini_test;
        private MotionCtrl.cyl_table_mini cyl_table_mini_test;
        private CTabControl cTab_work;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabPage12;
        private MotionCtrl.AXIS_Panle axiS_Panle2;
        private System.Windows.Forms.Button btn_start;
        private form form1;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_send;
        private System.Windows.Forms.TextBox textBox_recvive;
        private System.Windows.Forms.TextBox textBox_msgSend;
        private System.Windows.Forms.Button button2;
        public SocketHelper.AxTcpClient axTcpClient_cmd;
        private System.Windows.Forms.TabPage tabPage13;
        private CTabControl ctb_ax_sel;
        private System.Windows.Forms.TabPage ctp_ax_ul;
        private System.Windows.Forms.TabPage ctp_ax_dl;
        private System.Windows.Forms.TabPage ctp_bullet_move;
        private System.Windows.Forms.TabPage ctp_bullet_feed;
        private System.Windows.Forms.TabPage ctp_bullet_back;
        private System.Windows.Forms.TabPage ctp_ax_ws1;
        private System.Windows.Forms.TabPage ctp_ax_ws2;
        private System.Windows.Forms.TabPage ctp_ax_ws5;
        private System.Windows.Forms.TabPage ctp_ax_all;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private SocketHelper.AxTcpServer axTcpServer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private SocketHelper.AxTcpClient axTcpClient1;
        private System.Windows.Forms.Panel client_sta_inf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel1;
        private MotionCtrl.PosTable PosTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_feed_modu_x;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_feed_Yscale_y;
        private System.Windows.Forms.TextBox textBox_feed_Yscale_x;
        private System.Windows.Forms.TextBox textBox_feed_Xscale_y;
        private System.Windows.Forms.TextBox textBox_feed_Xscale_x;
        private System.Windows.Forms.TextBox textBox_feed_mouth_a;
        private System.Windows.Forms.TextBox textBox_feed_mouth_y;
        private System.Windows.Forms.TextBox textBox_feed_mouth_x;
        private System.Windows.Forms.TextBox textBox_feed_modu_a;
        private System.Windows.Forms.TextBox textBox_feed_modu_y;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_get_Yscale_y;
        private System.Windows.Forms.TextBox textBox_get_Yscale_x;
        private System.Windows.Forms.TextBox textBox_get_Xscale_y;
        private System.Windows.Forms.TextBox textBox_get_Xscale_x;
        private System.Windows.Forms.TextBox textBox_Lget_mouth_a;
        private System.Windows.Forms.TextBox textBox_Lget_mouth_y;
        private System.Windows.Forms.TextBox textBox_Lget_mouth_x;
        private System.Windows.Forms.TextBox textBox_Lget_modu_a;
        private System.Windows.Forms.TextBox textBox_Lget_modu_y;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_Lget_modu_x;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
    }
}