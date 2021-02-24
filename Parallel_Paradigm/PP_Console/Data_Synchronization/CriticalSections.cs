using PP_Console.Data_Synchronization.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization
{
    public class CriticalSections
    {
        public CriticalSections()
        {
            Moderator();
        }

        private void Moderator()
        {
            NonAtomicTransaction();
            AtomicTransaction();
        }

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

            // due to core bank object being unsafe, the net 
            // deposit and withdrawal may not cancel each other and
            // net balance, most of the time will ne non-zero
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

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

            // due to core bank object being padlock safe, the net 
            // deposit and withdrawal will cancel each other and
            // net balance will be always zero
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

    }

    
}
