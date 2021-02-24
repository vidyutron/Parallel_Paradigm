using PP_Console.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Console.Task_Programming
{
    public class Exceptions_TPL : IParallelize
    {
        public Exceptions_TPL()
        {
            Moderator();
        }
        public void Moderator()
        {
            ParallelForm();
        }

        public void NonParallelForm()
        {
            throw new NotImplementedException();
        }

        public void ParallelForm()
        {
            // Taks used in the demonstration of handling exceptions
            // originating from multiple task which are different in 
            // in their implementation
            var task1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this here")
                { Source = "From Thread TASK1" };
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this here")
                { Source = "From Thread Task2" };
            });

            try
            {
                // Here if no inside try-catch the exceptions will bubble up to the
                // callee of this function
                Task.WaitAll(task1, task2);
            }
            // Here we are catching collection of different exceptions oroginating from 
            // different tasks aggregated together.
            catch (AggregateException ex)
            {
                // loop through and get specific origin of the exception source
                foreach (var item in ex.InnerExceptions)
                {
                    Console.WriteLine($"Exceptions {item.GetType()} from {item.Source}");
                }
            }

            // try-catch inside a try-catch, why ???

            // outer try-catch is draining left over exceptions which were unacaught from
            // inner try-cath because of one reason or another
            try
            {
                try
                {
                    Task.WaitAll(task1, task2);
                }
                catch (AggregateException ex)
                {
                    // Provides a mechanism to explcitly handle a particular type of expceptions
                    // letting the outer block/calle know that zero/more exceptions could not be 
                    // handled internally
                    // ex.handle(()) must return a true/false
                    ex.Handle(e =>
                    {
                        if (e is InvalidOperationException)
                        {
                            Console.WriteLine("Invalid Operation Caught");
                            return true;
                        }
                        // Means, some expceptions are still left uncaught and will be allowed to
                        // bubble up.
                        return false;
                    });
                }
            }
            // this outer catch will finally try to catch some left over-exceptions
            catch (AggregateException ex)
            {
                foreach (var item in ex.InnerExceptions)
                {
                    Console.WriteLine($"Exception {item.GetType()} caught from {item.Source}");
                }
            }
            
        }
    }
}
