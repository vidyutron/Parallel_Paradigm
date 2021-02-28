using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync_ed
{
    public partial class Non_Blocking : Form
    {
        const int _sleepTime = 6500;
        public Non_Blocking()
        {
            InitializeComponent();
        }

        private Task<int> CalculateValueFactory()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(_sleepTime);
                return 123;
            });
        }

        private async Task<int> CalculateValueAsync()
        {
            await Task.Delay(_sleepTime);
            return 123;
        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            var calculation = CalculateValueFactory();
            calculation.ContinueWith(t =>
            {
                MessageBox.Show($"Return Value from non-blocking task : {t.Result}");
            },TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void btn_calculate_async_ClickAsync(object sender, EventArgs e)
        {
            int value = await CalculateValueAsync();
            MessageBox.Show($"Result from the asyned function returned with value : {value}");
        }
    }
}
