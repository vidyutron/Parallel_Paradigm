using PP_Console.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Console.Task_Programming
{
    public class Generic_TPL:IParallelize
    {

        public Generic_TPL()
        {
            Moderator();
        }

        public void Moderator()
        {
            ParallelForm();
        }

        public void ParallelForm()
        {
            // Generic Task Creation - Contsructor Method <T> is mandatory
            var task1 = new Task<int>(TextLength, 305.AlphaNumericSet());

            // Generic Task Creation - Factory Method <T> is not mandatory
            // Generic Task Creation - Start is automatic
            var task2 = Task.Factory.StartNew<int>(TextLength, 24.NumericSet());

            // Task Creation using constructor method requires explict Start()
            task1.Start();

            // This is blocking operation, that is flow will cross this line only once
            // the taskx i completely processed.
            Console.WriteLine($"Length of task1 processed item is {task1.Result}");
            // This is blocking operation as well
            Console.WriteLine($"Length of task2 processed item is {task2.Result}");


        }

        public void NonParallelForm()
        {

        }

        private int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}");
            return o.ToString().Length;
        }
    }
}
