using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.Exceptions
{
    public class InvalidTransferTypeException : Exception
    {
        private Exception e;

        public InvalidTransferTypeException(Exception e)
        {
            this.e = e;
        }
    }
}
