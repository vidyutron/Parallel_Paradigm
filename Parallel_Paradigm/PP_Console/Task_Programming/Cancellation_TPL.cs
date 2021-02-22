using PP_Console.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Task_Programming
{
    public class Cancellation_TPL : IParallelize
    {
        public Cancellation_TPL()
        {
            Moderator();
        }
        public void Moderator()
        {
            ParallelForm();
        }

        public void NonParallelForm()
        {
            throw new NotImplementedException();
        }

        public void ParallelForm()
        {
            SoftCancellation();
        }

        private void SoftCancellation()
        {
            // Cancellation token source
            var cts = new CancellationTokenSource();
            // Create token from the cancellationtoken source
            var token = cts.Token;

            // Task Creation - Factory Method
            var task1 = Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    // Soft cancellation - simply breaking out of the current 
                    // programmtic flow, using existing means like break, continue etc.
                    // knowledge of cancellation will be suppressed internally and 
                    // outside of the task block, it won't ne known
                    // *******  Not preferred way of handlig cancellation ********
                    if (token.IsCancellationRequested)
                        break;
                    else
                        Console.WriteLine(i++);
                }
            }, token);

            Console.ReadKey();
            cts.Cancel();
        }
    }
}
