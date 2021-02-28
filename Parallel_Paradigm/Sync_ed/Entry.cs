using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync_ed
{
    public partial class Entry : Form
    {
        public Entry()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var blocking_window = new Blocking();
            blocking_window.Show();
        }

        private void btn_nonblocking_Click(object sender, EventArgs e)
        {
            var non_blockingWindow = new Non_Blocking();
            non_blockingWindow.Show();
        }
    }
}
