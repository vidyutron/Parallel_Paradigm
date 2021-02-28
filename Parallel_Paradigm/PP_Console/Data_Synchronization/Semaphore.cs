using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization
{
    public class Semaphore
    {
        public Semaphore()
        {
            Moderator();
        }

        private void Moderator()
        {
            SemaphoreTaskCoordination();
        }

        private void SemaphoreTaskCoordination()
        {
            // A mechanism to coordinate tasks, spread across different threads and
            // control processing-'n' tasks at a time(concurrently).
            // SemaphoreSlim: Params - Intial Count of requests , Total count of requests
            // which could be processed concurrently
            var semaphore = new SemaphoreSlim(2,10);
            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");

                    // Release count reduced by 1, ie, the concurrent request which could
                    // go past this stage will be reduced, this means that if release count is
                    // released equals or below zero the requests won't go beyond this stage
                    // example - In our case intial count is 2, hence first 2 threads(offcourse randomly)
                    // would go beyond this point and print Processing task - x but once, limit is reached-0 
                    // it will stop processing the next line, semaphore tracker will activated for a 
                    // particular thread at this point, such that once some n count of semaphores are 
                    // released, processing could resume for those n tasks from this point

                    semaphore.Wait();
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }

            while (semaphore.CurrentCount<=2)
            {
                Console.WriteLine($"Semaphore count : {semaphore.CurrentCount}");
                Console.ReadKey();

                // Here we are releasing n semaphore, and that many tasks would be continue from the point of 
                // sempahore.wait(), hence that many concurrent tasks will be processed
                // Here basically we are increasing the Release count by 2, such that, that many
                // requests could be processed from the point of .Wait();
                semaphore.Release(2);
            }
        }
    }
}
