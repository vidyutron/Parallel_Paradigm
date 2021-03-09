using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization.Context
{
    /// <summary>
    /// Ledger POCO class for the BankAccount object
    /// </summary>
    public class BankAccountLedger
    {
        /// <summary>
        /// Bank Account object of an user
        /// </summary>
        public BadBankAccount Account { get; set; }

        /// <summary>
        /// Bank Account Mutex for thread safe operation
        /// </summary>
        public Mutex AccountMutex { get; set; }
    }
}
