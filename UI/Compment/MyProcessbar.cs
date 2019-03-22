using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System;
using System.Drawing.Text;
using System.ComponentModel;

public class MyProgressBar : ProgressBar
{
    public MyProgressBar()
    {
        base.SetStyle(ControlStyles.UserPaint, true);
    }

    //重写OnPaint方法
    protected override void OnPaint(PaintEventArgs e)
    {
        SolidBrush brush = null;
        Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
        //...
        //e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 1, 1, bounds.Width, bounds.Height);
        bounds.Height -= 2;
        bounds.Width = ((int)(bounds.Width * (((double)base.Value) / ((double)base.Maximum)))) - 2;
        brush = new SolidBrush(ForeColor);
        e.Graphics.FillRectangle(brush, 1, 1, bounds.Width, bounds.Height);


    }
}