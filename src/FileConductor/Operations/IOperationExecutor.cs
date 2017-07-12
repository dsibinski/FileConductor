using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.LoggingService;

namespace FileConductor.Operations
{
    public interface IOperationExecutor
    {
        void Execute(IOperation operation);
        ILoggingService LoggingService {get;set;}
    }
}
