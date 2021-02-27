using PP_Console.Common;
using PP_Console.Data_Synchronization;
using PP_Console.Task_Programming;
using System;

namespace PP_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void Runner()
        {
            // GENERIC - BASICS OF TASK PROGRAMMING
            //var g_tpl = new Generic_TPL();

            // GENERIC - CANCELLATIONs IN TPL
            //var g_canellation = new Cancellation_TPL();

            // GENRIC - EXCEPTIONS HANDLING IN TPL
            //var g_exceptions = new Exceptions_TPL();


            /*------------------------------------------------*/

            // SYNCHRONIZATION AND LOCKING - USING CRITICAL SECTION
            //var sync_criticalSection = new CriticalSections();

            // SYNCHRONIZATION AND LOCKING - MUTEX
            //var sync_mutex = new MutexSync();

            // SYNCHRONIZATION AND LOCKING - READ(ER)WRITE(ER) LOCK
            //var sync_readerWriter = new ReaderWriter_Lock();

            /*------------------------------------------------*/

            //

            Console.WriteLine("Back to Runner");
        }
    }
}
