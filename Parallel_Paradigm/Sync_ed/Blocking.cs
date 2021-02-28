using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sync_ed
{
    public partial class Blocking : Form
    {
        const int _sleepTime = 6500;
        public Blocking()
        {
            InitializeComponent();
        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            int val = CalculateValue();
        }

        private int CalculateValue()
        {
            MessageBox.Show($"Going to sleep for {_sleepTime} millseconds\t Click ok and see how everything in this window freezes!");
            Thread.Sleep(_sleepTime);
            return 123;
        }
    }
}
