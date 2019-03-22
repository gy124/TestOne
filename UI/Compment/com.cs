using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms;
using System.IO.Ports;
using MotionCtrl;

 namespace UI
{
    public partial class form : UserControl
    {
        
        public form()
        {
            InitializeComponent();
            InitralConfig();
        }
        string  ReDataStr;
        byte[] ReDatas;
        private void UserControl1_Load(object sender, EventArgs e)
        {
            
        }
        //定义端口类
        private SerialPort ComDevice = new SerialPort();
        //开辟接收缓冲区
    
        /// <summary>
        /// 配置初始化
        /// </summary>
        private void InitralConfig()
        {
            //查询主机上存在的串口
            comboBox_Port.Items.AddRange(SerialPort.GetPortNames());

            if (comboBox_Port.Items.Count > 0)
            {
                comboBox_Port.SelectedIndex = 0;
            }
            else
            {
                comboBox_Port.Text = "未检测到串口";
            }
            comboBox_BaudRate.SelectedIndex = 3;//9600
            radioButton_UTF8.Checked=true;
            comboBox_DataBits.SelectedIndex = 0;
            comboBox_StopBits.SelectedIndex = 0;
            //comboBox_CheckBits.SelectedIndex = 0;
            //pictureBox_Status.BackgroundImage = Properties.Resources.red;

            //向ComDevice.DataReceived（是一个事件）注册一个方法Com_DataReceived，当端口类接收到信息时时会自动调用Com_DataReceived方法
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
        }

        /// <summary>
        /// 一旦ComDevice.DataReceived事件发生，就将从串口接收到的数据显示到接收端对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReDatas = new byte[ComDevice.BytesToRead];
            //从串口读取数据
            ComDevice.Read(ReDatas, 0, ReDatas.Length);
            //实现数据的解码与显示
            
            AddData(ReDatas);
        }

        /// <summary>
        /// 解码过程
        /// </summary>
        /// <param name="data">串口通信的数据编码方式因串口而异，需要查询串口相关信息以获取</param>
        public void AddData(byte[] data)
        {
            if (radioButton_Hex.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                AddContent(sb.ToString().ToUpper());
            }
            else if (radioButton_ASCII.Checked)
            {
                AddContent(new ASCIIEncoding().GetString(data));
            }
            else if (radioButton_UTF8.Checked)
            {
                AddContent(new UTF8Encoding().GetString(data));
            }
            else if (radioButton_Unicode.Checked)
            {
                AddContent(new UnicodeEncoding().GetString(data));
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
               
                VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("收到消息11：{0}-", sb.ToString().ToUpper()));
                
            }
        }

        /// <summary>
        /// 接收端对话框显示消息
        /// </summary>
        /// <param name="content"></param>
        private void AddContent(string content)
        {
            //BeginInvoke(new MethodInvoker(delegate
            //{              
            //        textBox_Receive.AppendText(content);              
            //}));
            MessageBox.Show(content);
            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SYS, string.Format("收到测试信息：{0}-", content));
            
        }   
        /// <summary>
        /// 此函数将编码后的消息传递给串口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendData(string msg)
        {


            byte[] sendData = null;

            if (radioButton_Hex.Checked)
            {
                sendData = strToHexByte(msg.Trim());
            }
            else if (radioButton_ASCII.Checked)
            {
                sendData = Encoding.ASCII.GetBytes(msg.Trim());
            }
            else if (radioButton_UTF8.Checked)
            {
                sendData = Encoding.UTF8.GetBytes(msg.Trim());
            }
            else if (radioButton_Unicode.Checked)
            {
                sendData = Encoding.Unicode.GetBytes(msg.Trim());
            }
            else
            {
                sendData = strToHexByte(msg.Trim());
            }

            if (ComDevice.IsOpen)
            {
                try
                {
                    //将消息传递给串口
                    ComDevice.Write(sendData, 0, sendData.Length);
                    return true;
                }
                catch (Exception ex)
                {
                    VAR.ErrMsg("测试串口发送数据异常"+ex.ToString());
                }
            }
            else
            {
                VAR.ErrMsg("测试串口未打开");
            }
            return false;
        }
        public void OpenCom()
        {
            if (comboBox_Port.Items.Count <= 0)
            {
                VAR.ErrMsg("无可用串口");
                return;
            }

            if (ComDevice.IsOpen == false)
            {
                //设置串口相关属性
                ComDevice.PortName = comboBox_Port.SelectedItem.ToString();
                ComDevice.BaudRate = Convert.ToInt32(comboBox_BaudRate.SelectedItem.ToString());
                //ComDevice.Parity = (Parity)Convert.ToInt32(comboBox_CheckBits.SelectedIndex.ToString());
                ComDevice.DataBits = Convert.ToInt32(comboBox_DataBits.SelectedItem.ToString());
                ComDevice.StopBits = (StopBits)Convert.ToInt32(comboBox_StopBits.SelectedItem.ToString());
                try
                {
                    //开启串口
                    ComDevice.Open();
                    button_Send.Enabled = true;
                }
                catch (Exception ex)
                {
                    VAR.ErrMsg("串口打开异常" + ex.ToString());
                    return;
                }
                button_Switch.Text = "关闭";
                //  pictureBox_Status.BackgroundImage = Properties.Resources.green;
            }


            comboBox_Port.Enabled = !ComDevice.IsOpen;
            comboBox_BaudRate.Enabled = !ComDevice.IsOpen;
            //comboBox_DataBits.Enabled = !ComDevice.IsOpen;
            //comboBox_StopBits.Enabled = !ComDevice.IsOpen;
            //comboBox_CheckBits.Enabled = !ComDevice.IsOpen;
        }
        public void CloseCom()
        {
            if (comboBox_Port.Items.Count <= 0)
            {
                VAR.ErrMsg("无可用串口");
                return;
            }

            if (ComDevice.IsOpen )
            {
                try
                {
                    //关闭串口
                    ComDevice.Close();
                    button_Send.Enabled = false;
                }
                catch (Exception ex)
                {
                    VAR.ErrMsg("串口关闭异常"+ex.ToString());
                }
                button_Switch.Text = "开启";
                //  pictureBox_Status.BackgroundImage = Properties.Resources.red;
            }

            comboBox_Port.Enabled = !ComDevice.IsOpen;
            comboBox_BaudRate.Enabled = !ComDevice.IsOpen;
            //comboBox_DataBits.Enabled = !ComDevice.IsOpen;
            //comboBox_StopBits.Enabled = !ComDevice.IsOpen;
            //comboBox_CheckBits.Enabled = !ComDevice.IsOpen;
        }
        /// <summary>
        /// 16进制编码
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }
        /// <summary>
        /// 串口开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Switch_Click_1(object sender, EventArgs e)
        {
            if (comboBox_Port.Items.Count <= 0)
            {
                MessageBox.Show("未发现可用串口，请检查硬件设备");
                return;
            }

            if (ComDevice.IsOpen == false)
            {
                //设置串口相关属性
                ComDevice.PortName = comboBox_Port.SelectedItem.ToString();
                ComDevice.BaudRate = Convert.ToInt32(comboBox_BaudRate.SelectedItem.ToString());
                //ComDevice.Parity = (Parity)Convert.ToInt32(comboBox_CheckBits.SelectedIndex.ToString());
                ComDevice.DataBits = Convert.ToInt32(comboBox_DataBits.SelectedItem.ToString());
                ComDevice.StopBits = (StopBits)Convert.ToInt32(comboBox_StopBits.SelectedItem.ToString());
                try
                {
                    //开启串口
                    ComDevice.Open();
                    button_Send.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "未能成功开启串口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                button_Switch.Text = "关闭";
                //  pictureBox_Status.BackgroundImage = Properties.Resources.green;
            }
            else
            {
                try
                {
                    //关闭串口
                    ComDevice.Close();
                    button_Send.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "串口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                button_Switch.Text = "开启";
                //  pictureBox_Status.BackgroundImage = Properties.Resources.red;
            }

            comboBox_Port.Enabled = !ComDevice.IsOpen;
            comboBox_BaudRate.Enabled = !ComDevice.IsOpen;
            //comboBox_DataBits.Enabled = !ComDevice.IsOpen;
            //comboBox_StopBits.Enabled = !ComDevice.IsOpen;
            //comboBox_CheckBits.Enabled = !ComDevice.IsOpen;
        }


        /// <summary>
        /// 将消息编码并发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Send_Click_1(object sender, EventArgs e)
        {
            //if (textBox_Receive.Text.Length > 0)
            //{
            //    textBox_Receive.AppendText("\n");
            //}
            SendData(textBox_Send.Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_Port_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //以下两个指令是结合一款继电器而设计的
        //private void button_On_Click(object sender, EventArgs e)
        //{
        //    textBox_Send.Text = "005A540001010000B0";
        //}

        //private void button_Off_Click(object sender, EventArgs e)
        //{
        //    textBox_Send.Text = "005A540002010000B1";
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}

        //private void button_Switch_Click_1(object sender, EventArgs e)
        //{

        //}
    }
}
