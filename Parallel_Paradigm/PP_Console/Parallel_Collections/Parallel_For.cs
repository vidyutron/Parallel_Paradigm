using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Parallel_Collections
{
    public class Parallel_For
    {
        private const int SUM_LIMIT = 1001;
        public Parallel_For()
        {
            Moderator();
        }

        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i+=step)
            {
                yield return i; 
            }
        }

        private void Moderator()
        {
            SimpleParallelInvoke();
            Console.WriteLine("");
            SimpleParallelFor();
            Console.WriteLine("");
            CancellableParallelFor();
            Console.WriteLine("");
            ThreadLocalStorage();
            Console.WriteLine("");
            ThreadPartionining();
            Console.WriteLine("");
            SingleThreadSum();
        }

        private void SimpleParallelInvoke()
        {
            var a = new Action(() => Console.WriteLine($"Action's current task = {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Action's current task = {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Action's current task = {Task.CurrentId}"));

            Parallel.Invoke(a, b, c);
        }

        private void SimpleParallelFor()
        {
            // Step-size is always 1
            // if we want some other step size, then we can modify the foreach and use it as required
            Parallel.For(1, 11, a =>
            {
                Console.WriteLine($"Value : {a * 21}");
            });

            // Customizing the step-size for parallel for with foreach traversing over custom
            // function, which in our case is Range()
            Parallel.ForEach(Range(1, 101, 5), a => { Console.WriteLine(a);});
        }

        private void CancellableParallelFor()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            // state => ParallelLoopState, object having information about the state of
            // Parallel loop and its threads
            // used to Break,Stop the execution and returns the state at that moment

            // Like always, parallel execution cancellations are not absolute and breaking the 
            // flow might have some other concurrent threads continue execution, but will have 
            // eventual breakage
            var p_result = Parallel.For(1, 202, (x, state) =>
            {
                Console.WriteLine($"Currently on {x}[{Task.CurrentId}]");
                if(cts.IsCancellationRequested){ Console.WriteLine("Cancellation requested");}
                if (x > 801) {
                    state.Break();
                    //state.Stop();
                }
            });

            Console.WriteLine($"Was the loop complete ? : {p_result.IsCompleted}");
            // This owrks only with state.Break() not with token cancellation or state.Stop()
            if (p_result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Loop was stoped at: {p_result.LowestBreakIteration}");
            }
        }

        public void ThreadLocalStorage()
        {

            // Problem Statement - How can we sum in parallel loop
            // maybe by interlocking every iteration such that, 
            // we can avoid any race condition.
            // But, isn't interlocking every step will add unecessary overhead 
            // and cause delays
            
            // In order to metigate above, we can have per thread local sum and then 
            // in the end thread by thread do sum of all partial sums to obtain final sum
            int sum = 0;
            /* Details of the parallel for with thread local storage
            Parallel.For(1, 1001, () => 0,
                :: Thread Local Operation
                (x: init , state: state of thread local, curr_tls: current thread local sum) => {
                    :: Add sum to current local thread
                    curr_tls += x;
                    :: return the current thread local sum
                    return curr_tls;
                },
                :: Final summation of all partial sums
                partialSum => {
                    :: Interlocked add partial sum and return
                    Interlocked.Add(ref sum, partialSum);
                });
            */
            Parallel.For(1, SUM_LIMIT, () => 0,
                (x, state, curr_tls) => {
                    curr_tls += x;
                    return curr_tls;
                },
                partialSum => {
                    Interlocked.Add(ref sum, partialSum);
                });
            Console.WriteLine($"Final Sum - Parallel For : {sum}");
        }

        
        public void ThreadPartionining()
        {
            // loop counter
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            // create a partition using 0=> starting pint, count=>exclusive end range
            // ,10000 => range size
            // Combine the parallel operation with chunks to oprimise the performance 
            var part = Partitioner.Create(0, count, 10000);
            Parallel.ForEach(part, range =>
             {
                 for (int i = range.Item1; i < range.Item2; i++)
                 {
                     results[i] = (int)Math.Pow(i, 2);
                 }
             });
        }

        public void SingleThreadSum()
        {
            int sum = 0;
            Range(0, SUM_LIMIT, 1).ToList().ForEach(x => sum += x);
            Console.WriteLine($"Final Sum - General For : {sum}");
        }


    }
}
