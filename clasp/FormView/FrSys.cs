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

namespace clasp
{
    public partial class FrSys : Form
    {
        public bool bupdate;
        public FrSys()
        {
            InitializeComponent();
        }

        private void tabConSlect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void FrSys_Load_1(object sender, EventArgs e)
        {
            //加载卡列表CardTable
            FieldInfo[] pArray = typeof(MT).GetFields();
            foreach (FieldInfo p in pArray)
            {
                if (p.FieldType.Name == "CARD")
                {
                    if (((CARD)p.GetValue(typeof(MT))).id == 5)
                        CardTable.AddCard(((CARD)p.GetValue(typeof(MT))));
                }

                if (p.FieldType.Name == "GPIO")
                {
                    if (((GPIO)p.GetValue(typeof(MT))).card == MT.CARD_DMC3800_5)
                    {
                        if(((GPIO)p.GetValue(typeof(MT))).dir== GPIO.IO_DIR.OUT)//分类输入输出
                           ioTable.AddIO(((GPIO)p.GetValue(typeof(MT))));
                        if (((GPIO)p.GetValue(typeof(MT))).dir == GPIO.IO_DIR.IN)
                            ioTableIN.AddIO(((GPIO)p.GetValue(typeof(MT))));

                    }
                        
                }
                if (p.FieldType.Name == "AXIS")
                {
                    if (((AXIS)p.GetValue(typeof(MT))).card == MT.CARD_DMC3800_5)
                    {
                        axisTable.AddAxis(((AXIS)p.GetValue(typeof(MT))));
                        axisConfig.AddAxis(((AXIS)p.GetValue(typeof(MT))));
                    }
                }

                if (p.FieldType.Name == "Cylinder")
                {
                    if (((Cylinder)p.GetValue(typeof(MT))).io_out.card == MT.CARD_DMC3800_5)
                        cylinderTable.AddCylinder(((Cylinder)p.GetValue(typeof(MT))));
                }

            }
            ioTable.ShowCfg(2);//选择全部显示
            ioTableIN.ShowCfg(2);
            //轴列表
            tabConSlect_SelectedIndexChanged(ctb_ax_sel, null);
        }

        private void timerUpdate_Tick_1(object sender, EventArgs e)
        {
            int t;
            timerUpdate.Enabled = false;
            switch (tabConSlect.SelectedIndex)
            {
                case 0:
                    t = Environment.TickCount;
                    CardTable.UpdateShow();
                    t = Environment.TickCount - t;
                    //      lb_card_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 1:
                    t = Environment.TickCount;
                    axisTable.UpdateShow();
                    t = Environment.TickCount - t;
                    //   lb_ax_update_ms.Text = string.Format("{0}ms", t);
                    break;
                case 2:
                    if (ioTable.Visible) ioTable.UpdateShow();
                    if (ioTableIN.Visible) ioTableIN.UpdateShow();
                    if (cylinderTable.Visible) cylinderTable.UpdateShow();
                    //  lb_io_update_ms.Text = string.Format("{0}ms", ioTable.UpdateCt);
                    break;
            }
            //CardTable.UpdateShow();   
            timerUpdate.Interval = 500;
            timerUpdate.Enabled = true;
        }
    }
}
