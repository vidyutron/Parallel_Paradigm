using PP_Console.Data_Synchronization.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization
{
    public class MutexSync
    {
        public MutexSync()
        {
            Moderator();
        }

        private void Moderator()
        {
            IntraMutexedTransactions();
            InterMutexedTransactions();
        }

        private void IntraMutexedTransactions()
        {
            // Create a ledge object which is, a combination bad bank account and its related mutex, what is mutex?
            // Mutex is a synchronization primitive that grants exclusive access to the shared resource to only one thread, until released
            // In our case this facilitates the modificaiton of even out-of-sync operations what we perform in bad bank example
            var account_ledger    = new Dictionary<string, BankAccountLedger>
            { { "acc_one", new BankAccountLedger(){Account=new BadBankAccount(),AccountMutex=new Mutex()}}};
            var tasks       = new List<Task>();

            //
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        // Expression to grant an exclusve lock for this partiicular operation on the current thread.
                        bool haveLock = account_ledger["acc_one"].AccountMutex.WaitOne();
                        try
                        {
                            account_ledger["acc_one"].Account.Deposit(100);
                        }
                        finally
                        {
                            // Check if lock is in place, then release the mutex locked by current thread, such that
                            // other thread scan start with their operatioins
                            if (haveLock) account_ledger["acc_one"].AccountMutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        bool haveLock = account_ledger["acc_one"].AccountMutex.WaitOne();
                        try
                        {
                            account_ledger["acc_one"].Account.Withdraw(100);
                        }
                        finally
                        {
                            if (haveLock) account_ledger["acc_one"].AccountMutex.ReleaseMutex();
                        }
                    }
                }));
            }


            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final Balance is {account_ledger["acc_one"].Account.Balance}");
        }

        private void InterMutexedTransactions()
        {
            // Little different to the above beahviour where combinatioin of two operations where being carried out in one account only
            // here we will be doing some operration between two different instances and try to maintain the exclusive lock per thread
            // 
            var account_ledger = new Dictionary<string, BankAccountLedger>
            {{ "acc_one", new BankAccountLedger(){Account=new BadBankAccount(),AccountMutex=new Mutex()}}
            ,{ "acc_two", new BankAccountLedger(){Account=new BadBankAccount(),AccountMutex=new Mutex()}}};

            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        // Expression to grant an exclusve lock for this partiicular operation on the current thread.
                        bool haveloack =account_ledger["acc_one"].AccountMutex.WaitOne();
                        try
                        {
                            account_ledger["acc_one"].Account.Deposit(100);
                        }
                        finally
                        {
                            if (haveloack) account_ledger["acc_one"].AccountMutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        bool haveloack = account_ledger["acc_two"].AccountMutex.WaitOne();
                        try
                        {
                            account_ledger["acc_two"].Account.Deposit(100);
                        }
                        finally
                        {
                            if (haveloack) account_ledger["acc_two"].AccountMutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        // Intrestengly we need to wait on all the mutexs which are part of the current transaction, 
                        // which in our case if two different accounts, hence all the mutexes from account ledger needs to be 
                        // waited
                        bool haveloack = WaitHandle
                        .WaitAll(account_ledger.Select(x => x.Value.AccountMutex).ToArray());
                        try
                        {
                            account_ledger["acc_one"].Account.Transfer(account_ledger["acc_two"].Account,100);
                            //Console.WriteLine($"Remaining balance is : {account_ledger["acc_one"].Account.Balance}");
                        }
                        finally
                        {
                            // once done, release mutex belonging to all the accounts in this operation.
                            if (haveloack) account_ledger
                                .Select(x => x.Value.AccountMutex).ToList().ForEach(e => e.ReleaseMutex());
                        }
                    }
                }));

            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final Balance of acc_one is {account_ledger["acc_one"].Account.Balance}");
            Console.WriteLine($"Final Balance of acc_two is {account_ledger["acc_two"].Account.Balance}");
        }
    }



}
