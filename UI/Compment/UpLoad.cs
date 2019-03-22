using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Compment
{
    public partial class UpLoad : UserControl
    {
        public UpLoad()
        {
            InitializeComponent();
        }
        public void UpdateShow()
        {
            //lb_pos.Text = string.Format("Y:{0:000.000}\nZ:{1:000.000}", DownloadModle.ax_y.fenc_pos, DownloadModle.ax_z.fenc_pos);
        }

        private void lb_pos_Click(object sender, EventArgs e)
        {

        }
    }
}
