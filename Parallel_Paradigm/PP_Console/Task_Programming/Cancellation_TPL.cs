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

            // As currently not wrapped in try-catch block,
            // will throw unhandled exception 
            //ExplicitCancellation();

            CompositeCancellation();
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
                    // it won't be known,outside of the task block 
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
        private void ExplicitCancellation()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            // An pipeline function which is invoked as soon as 
            // a cancellation is requested
            token.Register(() =>
            {
                Console.WriteLine($"Cancellation was requested at: {DateTime.Now.ToShortTimeString()}");
            });
            var task = Task.Factory.StartNew(() =>
             {
                 int i = 0;
                 while (true)
                 {
                     // A shorthand to throw an exception when cancellation has been requested
                     token.ThrowIfCancellationRequested();
                     Console.WriteLine(++i);
                 }
             }, token);

            task.Start();
            Console.ReadKey();
            cts.Cancel();
        }

        private void CompositeCancellation()
        {
            var planned = new CancellationTokenSource();
            var preventitive = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();
            var trigger = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventitive.Token, emergency.Token);

            var task = Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    trigger.Token.ThrowIfCancellationRequested();
                    Console.WriteLine(++i);
                    Thread.Sleep(500);
                }
            }, trigger.Token);

            Console.ReadKey();
            emergency.Cancel();
        }
        
    }
}
