using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operation;

namespace FileConductor.Protocols
{
    public interface IOperationExecutor
    {
        void Execute(IOperation operation);
    }
}
