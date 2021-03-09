using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization.Context
{
    /// <summary>
    ///  Example of an object which deposits and withdraws amount form 
    ///  account of an user, but does not checks for locking an operation
    ///  in case multiple threads starts accessing the same instance of bank
    ///  account, this could lead to anomaly in balances
    /// </summary>
    public class BadBankAccount
    {
        /// <summary>
        /// Balance properties
        /// </summary>
        public double Balance { get; private set; }

        /// <summary>
        /// Add [amount] to bank account
        /// </summary>
        /// <param name="amount"></param>
        public void Deposit(double amount)
        {
            if(amount>0)
            Balance += amount;
        }

        /// <summary>
        /// Substract [amount] from bank account
        /// </summary>
        /// <param name="amount"></param>
        public void Withdraw(double amount)
        {
            if (amount > 0)
                Balance -= amount;
        }

        /// <summary>
        /// Transfer amount form one bank account to another
        /// </summary>
        /// <param name="recepient"></param>
        /// <param name="amount"></param>
        public void Transfer(BadBankAccount recepient, int amount)
        {
            if (amount > 0)
            {
                Balance -= amount;
                recepient.Balance += amount;
            }
        }
    }

    /// <summary>
    ///  Example of an object which deposits and withdraws amount form 
    ///  account of an user, while locking an operation during the time its
    ///  accessd from multiple threads, this prevents multiple threads to  
    ///  abnormally enter and exit any particular operation.
    /// </summary>
    public class LockedBankAccount
    {
        /// <summary>
        ///  Simple lock object
        /// </summary>
        public object padlock = new object();

        /// <summary>
        /// Balance
        /// </summary>
        public double Balance { get; private set; }

        /// <summary>
        /// Add [amount] to bank account
        /// </summary>
        /// <param name="amount"></param>
        public void Deposit(double amount)
        {
            lock (padlock)
            {
                if (amount > 0)
                    Balance += amount;
            }
        }

        /// <summary>
        /// Substract [amount] to bank account
        /// </summary>
        /// <param name="amount"></param>
        public void Withdraw(double amount)
        {
            lock (padlock)
            {
                if (amount > 0)
                    Balance -= amount;
            }
        }
    }


}
