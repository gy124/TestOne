using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCtrl
{
    public partial class AXIS_Panle : UserControl
    {
      public AXIS axis_x = null;
      public AXIS axis_y = null;
      public AXIS axis_z = null;
      public AXIS axis_a = null;
        EM_RES ret = EM_RES.OK;
        public AXIS_Panle()
        {
            InitializeComponent();
            update_show();
        }
        public void clear()
        {
            axis_x = null;
            axis_y = null;
            axis_z = null;
            axis_a = null;
        }
        public void update_show()
        {
           if( axis_x != null)
           {
               Xdec.Visible = true;
               Xplus.Visible = true;
           }
           else
           { 
               Xdec.Visible = false;
               Xplus.Visible = false;
           }

           if (axis_y != null)
           {
               Ydec.Visible = true;
               Yplus.Visible = true;
           }
           else
           {
               Ydec.Visible = false;
               Yplus.Visible = false;
           }
           if (axis_z != null)
           {
               Zdec.Visible = true;
               Zplus.Visible = true;
           }
           else
           {
               Zdec.Visible = false;
               Zplus.Visible = false;
           }
           if (axis_a != null)
           {             
               Adec.Visible = true;
               Aplus.Visible = true;
           }
           else
           {             
               Adec.Visible = false;
               Aplus.Visible = false;
           }
        }

        private void AXIS_Panle_Load(object sender, EventArgs e)
        {
            int cup_width = this.Width / 6;
            int cup_height = this.Height / 4;

            this.Xdec.Width = cup_width;
            this.Xdec.Height = cup_height;
            this.Xplus.Width = cup_width;
            this.Xplus.Height = cup_height;
            this.Ydec.Width = cup_width;
            this.Ydec.Height = cup_height;
            this.Yplus.Width = cup_width;
            this.Yplus.Height = cup_height;
            this.Zdec.Width = cup_width;
            this.Zdec.Height = cup_height;
            this.Zplus.Width = cup_width;
            this.Zplus.Height = cup_height;
            this.Adec.Width = cup_width;
            this.Adec.Height = cup_height;
            this.Aplus.Height = cup_height;
            this.Aplus.Width = cup_width;
            this.Xdec.Left = 0;
            this.Xdec.Top = cup_height;
            this.Xplus.Left = cup_width * 2;
            this.Xplus.Top = cup_height;
            this.Ydec.Left = cup_width;
            this.Ydec.Top = cup_height * 2;
            this.Yplus.Left = cup_width;
            this.Yplus.Top = 0;
            this.Zdec.Left = cup_width * 3;
            this.Zdec.Top = cup_height * 2;
            this.Zplus.Left = cup_width * 3;
            this.Zplus.Top = 0;
            this.Adec.Left = cup_width;
            this.Adec.Top = cup_height;
            this.Aplus.Top = cup_height;
            this.Aplus.Left = cup_width*3;
        }

        private void Xdec_Click(object sender, EventArgs e)
        {
           if( axis_x != null)
           { 
            ret = axis_x.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.N);
            if (ret != EM_RES.OK) MessageBox.Show(axis_x.disc + "负向移动异常!");
           }
        }

        private void Xplus_Click(object sender, EventArgs e)
        {
            if (axis_x != null)
            {
                ret = axis_x.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.P);
                if (ret != EM_RES.OK) MessageBox.Show(axis_x.disc + "负向移动异常!");
            }
            
        }

        private void Yplus_Click(object sender, EventArgs e)
        {
            if (axis_y != null)
            {
                ret = axis_y.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.P);
                if (ret != EM_RES.OK) MessageBox.Show(axis_y.disc + "负向移动异常!");
            }
        }

        private void Ydec_Click(object sender, EventArgs e)
        {
            if (axis_y != null)
            {
                ret = axis_y.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.N);
                if (ret != EM_RES.OK) MessageBox.Show(axis_y.disc + "负向移动异常!");
            }
        }

        private void Zplus_Click(object sender, EventArgs e)
        {
            if (axis_z != null)
            {
                ret = axis_z.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.P);
                if (ret != EM_RES.OK) MessageBox.Show(axis_z.disc + "负向移动异常!");
            }
        }

        private void Zdec_Click(object sender, EventArgs e)
        {
            if (axis_z != null)
            {
                ret = axis_z.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.N);
                if (ret != EM_RES.OK) MessageBox.Show(axis_z.disc + "负向移动异常!");
            }
        }

        private void Aplus_Click(object sender, EventArgs e)
        {
            if (axis_a != null)
            {
                ret = axis_a.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.P);
                if (ret != EM_RES.OK) MessageBox.Show(axis_a.disc + "负向移动异常!");
            }
        }

        private void Adec_Click(object sender, EventArgs e)
        {
            if (axis_a != null)
            {
                ret = axis_a.JOG_Step(ref VAR.gsys_set.bquit, AXIS.AX_DIR.N);
                if (ret != EM_RES.OK) MessageBox.Show(axis_z.disc + "负向移动异常!");
            }
        }
    }
}
