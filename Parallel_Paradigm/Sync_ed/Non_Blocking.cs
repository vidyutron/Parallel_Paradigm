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

        public void btn_calculate_Click(object sender, EventArgs e)
        {
            var calculation = CalculateValueFactory();
            calculation.ContinueWith(t =>
            {
                MessageBox.Show($"Return Value from non-blocking task : {t.Result}");
            },TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// <code>
        /// int result = await await Task.Factory.Startnew(
        /// async delegate{
        /// await Task.Delay(1000);
        /// return 42;
        /// },
        /// CancellationToken.None, TaskCreationOptions.DenyChildAttach,
        /// TaskScheduler.Default);
        /// </code>
        /// 
        /// Let's understand what's hapenning above :
        /// return 42; means return of Task<Task<int>> return at Task level
        /// await, acts like a unwrap() of the async and its underlying return values
        /// Now, we can see why await is used twice, as await await unwraps
        /// Task of Task of int and gives back simply the core 123(int data type)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_calculate_async_ClickAsync(object sender, EventArgs e)
        {
            int value = await CalculateValueAsync();
            MessageBox.Show($"Result from the asyned function returned with value : {value}");
        }
    }
}
