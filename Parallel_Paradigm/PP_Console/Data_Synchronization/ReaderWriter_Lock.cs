using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization
{
    public class ReaderWriter_Lock
    {

        // Specific kind of lock which blocks certain setter and getter accesses 
        // defined with Enter() and Exit() pattern where anything within them will 
        // be granted read or write access across the threads and avoid deadlocks and
        // unwanted updates
        //
        ReaderWriterLockSlim _padlock = new ReaderWriterLockSlim();
        Random _rand = new Random();
        public ReaderWriter_Lock()
        {
            Moderator();
        }

        private void Moderator()
        {
            LockerJob();
            UpgradeLockerJob();
        }

        private void LockerJob()
        {
            var x = 0;
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    // The Enter() and Exit() pattern as mentioned above
                    // during this even other thread won't be able to access
                    // the variable {x} until 1 seconds is passed(because of thread sleep)
                    _padlock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x= {x}");
                    Thread.Sleep(1000);

                    // Here(In mddle of Enter and Exit section) in case we now want 
                    // to get Write Lock, we can't do that. that will throw exception 
                    // because this operation by design is prone to deadlocks.

                    _padlock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x= {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });              
            }

            int u = 5;
            while (u-->0)
            {
                // Now we will enter the write locks and modify the value as per need.
                _padlock.EnterWriteLock();
                Console.WriteLine($"Entered write lock, x= {x}");

                int newVal = _rand.Next(10);
                x = newVal;
                Console.WriteLine($" Set , x = {x}");
                _padlock.ExitWriteLock();
                Console.WriteLine($"Exited write lock, x= {x}");
            }

            Console.WriteLine("");
        }

        private void UpgradeLockerJob()
        {
            var x = 0;
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {

                    // To overcome the shortcomings of regular lock, specially in dynamic
                    // scenarios where we may want to write in middle of read lock or vice-versa
                    // for suc cases we have Upgradable locks which can be modifed in middle of 
                    // exisitng locking section.
                    _padlock.EnterUpgradeableReadLock();

                    Console.WriteLine($"Entered Upgradable Read Lock , x = {x}");
                    if(i%2==0)
                    {
                        Console.WriteLine($"Entered Upgradable Write Lock , x = {x}");
                        _padlock.EnterWriteLock();
                        x = i;
                        _padlock.ExitWriteLock();
                        Console.WriteLine($"Exited Upgradable Write Lock , x = {x}");
                    }
                    Thread.Sleep(2000);
                    _padlock.ExitUpgradeableReadLock();
                    Console.WriteLine($"Exited Upgradable Read Lock , x = {x}");
                }));

                try
                {
                    Task.WaitAll(tasks.ToArray());
                }
                catch (AggregateException ae)
                {
                    ae.Handle(e =>
                    {
                        Console.WriteLine(e);
                        return true;
                    });
                }
            }
        }
    }
}
