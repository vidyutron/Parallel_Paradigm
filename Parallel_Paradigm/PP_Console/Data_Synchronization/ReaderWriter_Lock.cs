using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization
{
    /// <summary>
    /// Performing multi-threaded data synchronization using READER WRITER LOCK 
    /// </summary>
    public class ReaderWriter_Lock
    {
        /// <summary>
        /// Specific kind of lock which blocks certain setter and getter accesses 
        /// defined with Enter() and Exit() pattern where anything within them will 
        /// be granted read or write access across the threads and avoid deadlocks and
        /// unwanted updates.
        /// </summary>
        public ReaderWriterLockSlim _padlock   = new ReaderWriterLockSlim();
        public Random _rand                    = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        public ReaderWriter_Lock()
        {
            Moderator();
        }

        /// <summary>
        /// Reader Writer controller method
        /// </summary>
        public void Moderator()
        {
            LockerJob();
            UpgradeLockerJob();
        }

        /// <summary>
        /// The Enter() and Exit() pattern as mentioned above
        /// during this even other thread won't be able to access
        /// the variable {x} until 1 seconds is passed(because of thread sleep)
        /// <code>
        ///  _padlock.EnterReadLock();
        ///  Console.WriteLine($"Entered read lock, x= {x}");
        ///  Thread.Sleep(1000);
        /// </code>
        /// 
        /// 
        /// Here(in mddle of Enter and Exit section) in case we now want 
        /// to get Write Lock, we can't do that. that will throw exception 
        /// because this operation by design is prone to deadlocks.
        /// <code>
        /// _padlock.ExitReadLock();
        /// Console.WriteLine($"Exited read lock, x= {x}");
        /// </code>
        /// </summary>
        public void LockerJob()
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

                    // Here(in mddle of Enter and Exit section) in case we now want 
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

        /// <summary>
        /// To overcome the shortcomings of regular lock, specially in dynamic
        /// scenarios where we may want to write in middle of read lock or vice-versa
        /// for such cases we have Upgradable locks which can be modifed in middle of 
        /// exisitng locking section.
        /// <code>
        /// _padlock.EnterUpgradeableReadLock();
        /// </code>
        /// 
        /// </summary>
        public void UpgradeLockerJob()
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
