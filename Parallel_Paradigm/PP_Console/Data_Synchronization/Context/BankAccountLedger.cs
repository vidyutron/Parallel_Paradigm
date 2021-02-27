using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Console.Data_Synchronization.Context
{
    public class BankAccountLedger
    {
        public BadBankAccount Account { get; set; }
        public Mutex AccountMutex { get; set; }
    }
}
