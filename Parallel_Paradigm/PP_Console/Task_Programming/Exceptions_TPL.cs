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

        }
        public void Moderator()
        {
            throw new NotImplementedException();
        }

        public void NonParallelForm()
        {
            throw new NotImplementedException();
        }

        public void ParallelForm()
        {
            throw new NotImplementedException();
        }
    }
}
