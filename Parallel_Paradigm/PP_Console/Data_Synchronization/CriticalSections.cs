using PP_Console.Data_Synchronization.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization
{
    /// <summary>
    /// Performing multi-threaded data synchronization using CRITICAL SECTIONS 
    /// </summary>
    public class CriticalSections
    {
        /// <summary>
        /// Critical Section - Constructor
        /// </summary>
        public CriticalSections()
        {
            Moderator();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Moderator()
        {
            NonAtomicTransaction();
            AtomicTransaction();
        }

        /// <summary>
        /// Demonstrating the effect of thread unsafe operations through a 
        /// thread unsafe class/interation
        /// 
        /// Due to core bank object being unsafe, net 
        /// deposit and withdrawal may not cancel each other and
        /// net balance most of the times will be non-zero
        /// </summary>
        public void NonAtomicTransaction()
        {
            var ba = new BadBankAccount();
            var tasks = new List<Task>();
            const int trans= 100;

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        ba.Deposit(trans);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        ba.Withdraw(trans);
                    }
                }));              
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

        /// <summary>
        /// Demonstrating the operation of object which supports basic 
        /// thread locking mechanism using padlock mechanism.
        /// 
        /// Due to core bank object being padlock safe, the net 
        /// deposit and withdrawal will cancel each other and
        /// net balance will be always zero.
        /// </summary>
        public void AtomicTransaction()
        {
            var ba = new LockedBankAccount();
            var tasks = new List<Task>();
            const int trans = 100;

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        ba.Deposit(trans);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        ba.Withdraw(trans);
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

    }

    
}
