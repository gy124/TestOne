using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionCtrl;
using System.Reflection;

using System.Net;

using System.Net.Sockets;

using System.Threading;

namespace UI
{
    public partial class FrSys : Form
    {
        public bool bupdate
        {
            get { return tmr_update.Enabled; }
            set { tmr_update.Enabled = value; }
        }
        public FrSys()
        {
            InitializeComponent();
        }

        private void FrSys_Load(object sender, EventArgs e)
        {
            //加载卡列表CardTable
            FieldInfo[] pArray = typeof(MT).GetFields();
            foreach (FieldInfo p in pArray)
            {
            //    if (p.FieldType.Name == "CARD")
               //     CardTable.AddCard(((CARD)p.GetValue(typeof(MT))));

                if (p.FieldType.Name == "GPIO")
                    ioTable.AddIO(((GPIO)p.GetValue(typeof(MT))));

                if (p.FieldType.Name == "Cylinder")
                    cylinderTable.AddCylinder(((Cylinder)p.GetValue(typeof(MT))));
            }
            foreach(CARD aa in MT.CardList)
            {
                CardTable.AddCard(aa);
            }
            ioTable.ShowCfg(0);
            
            //轴列表
            ctb_ax_sel_SelectedIndexChanged(ctb_ax_sel, null);
            // 网口通讯
            COMM.LoadCfg();
            textBox_IP.Text = COMM.mip;
            textBox_port.Text = COMM.mport.ToString();
            get_vs_parm();// 读取视觉参数

           

        }
        #region 登陆
        private void btn_logout_Click(object sender, EventArgs e)
        {
            //cb_user.SelectedIndex = 0;
            //tb_pw.Text = "";
            //lb_log_inf.Text = "";
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //string pw = VAR.gst_user[cb_user.SelectedIndex].psw;
            //if (tb_pw.Text == pw)
            //{
            //    VAR.gsys_set.user_idx = cb_user.SelectedIndex;
            //    lb_log_inf.Text = "登陆成功，当前用户为：" + VAR.gst_user[cb_user.SelectedIndex].id;
            //}
            //else
            //{
            //    //MessageBox.Show("Password error!");
            //    lb_log_inf.Text = "登陆失败，密码错误!";
            //}
            //cb_user.SelectedIndex = VAR.gsys_set.user_idx;
            //if (VAR.gsys_set.user_idx >= 0)
            //    tb_pw.Text = VAR.gst_user[VAR.gsys_set.user_idx].psw;
            //else cb_user.SelectedIndex = 0;
        }
        #endregion
        private void tmr_update_Tick(object sender, EventArgs e)
        {
            int t;
            switch(ctb_sys.SelectedIndex)
            {
                case 0:
                    t = Environment.TickCount;
                    CardTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_card_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 1:
                    t = Environment.TickCount;
                    axisTable.UpdateShow();           
                    t = Environment.TickCount - t;
                    lb_ax_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 2:                    
                    t = Environment.TickCount;
                    ioTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_io_update_ms.Text = string.Format("{0}ms", t);

                    t = Environment.TickCount;
                    cylinderTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_cld_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 4:
                    t = Environment.TickCount;
                    PosTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_ax_update_ms.Text = string.Format("{0}ms", t);
                    pos_table_mini_test.ClearPos();
                    pos_table_mini_test.Addpos(MT.pos_list_bull_feed);
                    break;
                case 5:
                   // get_vs_parm();
                    break;
            }
            CardTable.UpdateShow();
            if (COMM.bCnnect)
                client_sta_inf.BackColor = Color.Green;
            else
                client_sta_inf.BackColor = Color.Red;
        }
        private void btn_load_axis_cfg_Click(object sender, EventArgs e)
        {
            EM_RES ret = EM_RES.OK;
            ret=   axisConfig.LoadFrFile();
            if(ret!=EM_RES.OK)
                MessageBox.Show("轴列表参数加载失败!");
            else
            MessageBox.Show("轴列表参数加载完成!");
        }
        private void btn_save_axis_cfg_Click(object sender, EventArgs e)
        {
            bool bOK = axisConfig.SaveToFile();
            if (bOK)
            
                MessageBox.Show("轴列表参数保存完成!");
            else
                MessageBox.Show("轴列表参数保存!失败");
        }
        public  void get_vs_parm()
        {
            UI.COM.MVS.LoadInf();
            textBox_feed_modu_x.Text = UI.VS.st_set.Feed_mode_pos.x.ToString();
            textBox_feed_modu_y.Text = UI.VS.st_set.Feed_mode_pos.y.ToString();
            textBox_feed_modu_a.Text = UI.VS.st_set.Feed_mode_pos.z.ToString();

            textBox_feed_mouth_x.Text = UI.VS.st_set.Feed_mouth_center.x.ToString();
            textBox_feed_mouth_y.Text = UI.VS.st_set.Feed_mouth_center.y.ToString();
            textBox_feed_mouth_a.Text = UI.VS.st_set.Feed_mouth_center.z.ToString();

            textBox_feed_Xscale_x.Text = UI.VS.st_set.Feed_x_scale.x.ToString();
            textBox_feed_Xscale_y.Text = UI.VS.st_set.Feed_x_scale.y.ToString();
            textBox_feed_Yscale_x.Text = UI.VS.st_set.Feed_y_scale.x.ToString();
            textBox_feed_Yscale_y.Text = UI.VS.st_set.Feed_y_scale.y.ToString();

            textBox_Lget_modu_x.Text = UI.VS.st_set.L_mode_modu_pos.x.ToString();
            textBox_Lget_modu_y.Text = UI.VS.st_set.L_mode_modu_pos.y.ToString();
            textBox_Lget_modu_a.Text = UI.VS.st_set.L_mode_modu_pos.z.ToString();

            textBox_Lget_mouth_x.Text = UI.VS.st_set.L_mouth1_center.x.ToString();
            textBox_Lget_mouth_y.Text = UI.VS.st_set.L_mouth1_center.y.ToString();
            textBox_Lget_mouth_a.Text = UI.VS.st_set.L_mouth1_center.z.ToString();

            textBox_get_Xscale_x.Text = UI.VS.st_set.modu_x_scale.x.ToString();
            textBox_get_Xscale_y.Text = UI.VS.st_set.modu_x_scale.y.ToString();
            textBox_get_Yscale_x.Text = UI.VS.st_set.modu_y_scale.x.ToString();
            textBox_get_Yscale_y.Text = UI.VS.st_set.modu_y_scale.y.ToString();
        }
        #region 工站切换
        private void ctb_ax_sel_SelectedIndexChanged(object sender, EventArgs e)
        {            
            axisTable.ClearAxis();

            axisConfig.ClearAxis();
            axisTable.UpdateShow();
            axisConfig.UpdateShow();
            PosTable.ClearPos();
            axiS_Panle1.clear();
            PosTable.UpdateShow();
            switch (((CTabControl)sender).SelectedIndex)
            {                
                case 0:
                  //  axisTable.AddAxis(MT.AxList_UL);
                  //  axisConfig.AddAxis(MT.AxList_UL);
                    break;
                case 1:
                   // axisTable.AddAxis(MT.AxList_DL);
                  //  axisConfig.AddAxis(MT.AxList_DL);
                    break;
                case 2:
                    axisTable.AddAxis(MT.AXIS_bullet_move);
                    axisConfig.AddAxis(MT.AXIS_bullet_move);
                    PosTable.Addpos(MT.pos_list_bull_move);
                    axiS_Panle1.axis_x = MT.AXIS_bullet_move;
                    axiS_Panle1.update_show();
                    break;
                case 3:
                    axisTable.AddAxis(MT.AXIS_bullet_feed);
                    axisConfig.AddAxis(MT.AXIS_bullet_feed);
                    PosTable.Addpos(MT.pos_list_bull_feed);
                    axiS_Panle1.axis_y = MT.AXIS_bullet_feed;
                    axiS_Panle1.update_show();
                    break;
                case 4:
                    axisTable.AddAxis(MT.AxList_bullet);
                    axisConfig.AddAxis(MT.AxList_bullet);
                    PosTable.Addpos(MT.pos_list_bull_back);
                    axiS_Panle1.axis_y = MT.AXIS_bullet_back;
                    axiS_Panle1.update_show();
                    break;
                case 7:
                    axisTable.AddAxis(MT.AxList_WS_BACK);
                    axisConfig.AddAxis(MT.AxList_WS_BACK);
                    PosTable.Addpos(MT.pos_list_back);
                    axiS_Panle1.axis_x = MT.AxList_WS_BACK[0];
                    axiS_Panle1.axis_y = MT.AxList_WS_BACK[1];
                    axiS_Panle1.axis_z = MT.AxList_WS_BACK[2];
                    axiS_Panle1.axis_a = MT.AxList_WS_BACK[3];
                    axiS_Panle1.update_show();
                    break;
                case 6:
                    axisTable.AddAxis(MT.AxList_WS_FEED);
                    axisConfig.AddAxis(MT.AxList_WS_FEED);
                    PosTable.Addpos(MT.pos_list_feed);
                    axiS_Panle1.axis_x = MT.AxList_WS_FEED[0];
                    axiS_Panle1.axis_y = MT.AxList_WS_FEED[1];
                    axiS_Panle1.axis_z = MT.AxList_WS_FEED[2];
                    axiS_Panle1.axis_a = MT.AxList_WS_FEED[3];
                    axiS_Panle1.update_show();
                    break;

                case 5:
                    axisTable.AddAxis(MT.AxList_WS_GET);
                    axisConfig.AddAxis(MT.AxList_WS_GET);
                    PosTable.Addpos(MT.pos_list_get);
                    axiS_Panle1.axis_x = MT.AxList_WS_GET[0];
                    axiS_Panle1.axis_y = MT.AxList_WS_GET[1];
                    axiS_Panle1.axis_z = MT.AxList_WS_GET[2];
                    axiS_Panle1.axis_a = MT.AxList_WS_GET[3];
                    axiS_Panle1.update_show();
                    
                    break;
                case 8:
                    axisTable.AddAxis(MT.AxList_ALL);       
                    axisConfig.AddAxis(MT.AxList_ALL);
                    break;
            }
            bupdate = true;
        }
        #endregion
        private void ctb_sys_SelectedIndexChanged(object sender, EventArgs e)
        {
            bupdate = true;
            int t;
            switch (ctb_sys.SelectedIndex)
            {
                case 0:
                    t = Environment.TickCount;
                    CardTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_card_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 1:
                    t = Environment.TickCount;
                    axisTable.UpdateShow();

                    t = Environment.TickCount - t;
                    lb_ax_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 2:
                    t = Environment.TickCount;
                    ioTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_io_update_ms.Text = string.Format("{0}ms", t);

                    t = Environment.TickCount;
                    cylinderTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_cld_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 4:
                    t = Environment.TickCount;
                    PosTable.UpdateShow();
                    t = Environment.TickCount - t;
                    lb_ax_update_ms.Text = string.Format("{0}ms", t);
                    pos_table_mini_test.ClearPos();
                    pos_table_mini_test.Addpos(MT.pos_list_bull_feed);
                    break;
                case 5:
                    get_vs_parm();
                    break;
            }
        }

        private void btn_update_card_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            foreach (CARD card in MT.CardList)
            {
                if (card.isReady)
                {
                    if (EM_RES.OK == card.DownLoadFile()) str = str + String.Format("{0}/id{1},更新配置成功！\r\n", card.disc, card.id);
                    else str = str + String.Format("{0}/id{1},更新配置失败！\r\n", card.disc, card.id);
                }
                else
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, String.Format("{0}/id{1},未初始化！", card.disc, card.id));
                    str = str + String.Format("{0}/id{1},未初始化！\r\n", card.disc, card.id);
                }
            }
            MessageBox.Show(str);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, String.Format("stop"));
            MT.AllAxStop();            
        }

        private void ctb_io_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ioTable.ShowCfg(((CTabControl)sender).SelectedIndex);
        }

        #region 网络通讯事件相关

        private void button2_Click(object sender, EventArgs e)
        {
            string  mip ;
            int mport;
            EM_RES ret=EM_RES.OK;
            COMM.LoadCfg();
            mip = COMM.mip;
            mport = COMM.mport;

            //IPAddress ip = IPAddress.Parse(mip);

            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //try
            //{

            //    clientSocket.Connect(new IPEndPoint(ip, mport)); //配置服务器IP与端口  

            //  //  Console.WriteLine("连接服务器成功");
            //    MessageBox.Show("连接服务器成功");

            //}

            //catch
            //{

            //    Console.WriteLine("连接服务器失败，请按回车键退出！");
            //  //  MessageBox.Show("连接服务器失败");
            //    return;

            //}  

            axTcpClient_cmd.ReConectedCount = 4000;
            axTcpClient_cmd.ServerIp = mip;
            axTcpClient_cmd.ServerPort = mport;
            axTcpClient_cmd.StartConnection();
            System.Threading.Thread.Sleep(50);
             
                   
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            String str = textBox_msgSend.Text;
            if (str.Length < 1)
            {
                MessageBox.Show("请输入信息");
                return;
            }
            axTcpClient_cmd.SendCommand(str);       
        }

        private void axTcpClient_cmd_OnReceviceByte(byte[] date)
        {
           
            textBox_recvive.Text = date.ToString();
            MessageBox.Show("收到消息：" + date.ToString());
            WSROLL.CK_cmd(date.ToString());
        }

        private void axTcpClient_cmd_OnStateInfo(string msg, SocketHelper.SocketState state)
        {
         //   textBox_client_sta.Text = msg;
            VAR.WarnMsg(msg);
            if (state == SocketHelper.SocketState.Connected)
                COMM.bCnnect = true;
            else
                COMM.bCnnect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if( textBox_IP.Text!=null)
           COMM.mip= textBox_IP.Text  ;
           if (textBox_port.Text != null)
           COMM.mport= Convert.ToInt16( textBox_port.Text.ToString()) ;
           COMM.SavCfg();
           MessageBox.Show("保存成功");
        }

        private void axTcpClient_cmd_OnErrorMsg(string msg)
        {
            
            VAR.WarnMsg(msg);
        }

        private void axTcpServer1_OnOnlineClient(System.Net.Sockets.Socket temp)
        {
            MessageBox.Show("有客户端上线");
        }

        #endregion      
    }
}
