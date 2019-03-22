using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WT.ExecuteUI
{
    public partial class TableLayout : TableLayoutPanel
    {
        public TableLayout()
        {
            // 防止闪屏
            this.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this, true, null);
        }

        private Color borderColor = Color.Black;

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            //绘制边框
            base.OnCellPaint(e);
            Pen pp = new Pen(BorderColor);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + this.Width - 1, e.CellBounds.Y + this.Height - 1);
        }

    }
}
