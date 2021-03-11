using BenchmarkDotNet.Running;
using PP_Console.Common;
using PP_Console.Data_Synchronization;
using PP_Console.Parallel_Collections;
using PP_Console.Task_Programming;
using System;

namespace PP_Console
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entery point of our program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Runner();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// Staring point of the program, where all other pieces are called and referred to.
        /// 
        /// In order to keep concepts / examples de-cluttered 
        /// We have -
        /// 
        /// 1. Partitioned into different regions e.g [GENERIC - BASICS OF TASK PROGRAMMING] and so forth.
        /// 2. Each section has its own separate callee point with its class and implementation.
        /// 3. It's recommended, uncomment single callee point at a time and understand its complete code flow.
        /// 
        /// Example - Uncomment <code>//var g_canellation = new Cancellation_TPL();</code> and run,
        /// to understand implementation of Cancellation tokens and thread operations.
        /// </summary>
        private static void Runner()
        {

            #region  GENERIC - BASICS OF TASK PROGRAMMING
            //var g_tpl = new Generic_TPL();

            // GENERIC - CANCELLATIONs IN TPL
            //var g_canellation = new Cancellation_TPL();

            // GENRIC - EXCEPTIONS HANDLING IN TPL
            //var g_exceptions = new Exceptions_TPL();

            #endregion

            /*------------------------------------------------*/

            #region SYNCHRONIZATION AND LOCKING
            // SYNCHRONIZATION AND LOCKING - USING CRITICAL SECTION
            //var sync_criticalSection = new CriticalSections();

            // SYNCHRONIZATION AND LOCKING - MUTEX
            //var sync_mutex = new MutexSync();

            // SYNCHRONIZATION AND LOCKING - READ(ER)WRITE(ER) LOCK
            //var sync_readerWriter = new ReaderWriter_Lock();

            // SYNCHRONIZATION AND LOCKING - SEMAPHORE
            //var sync_semaphore = new Semaphore();

            #endregion

            /*------------------------------------------------*/

            #region PARALLEL COLLECTION
            // PARALLEL LOOP - FOR
            //var parallel_loop = new Parallel_For();

            #endregion

            /*------------------------------------------------*/

            #region PUB-SUB BEHAVIOUR IN TASK
            // PUBLISHER-CONSUMER RELATIONSHIP 
            var produce_consume = new Producer_Consumer();

            #endregion

            Console.WriteLine("Back to Runner");
        }
    }
}
