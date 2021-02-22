using PP_Console.Common;
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
            //
            //var g_tpl = new Generic_TPL();

            //
            var g_canellation = new Cancellation_TPL();
        }
    }
}
