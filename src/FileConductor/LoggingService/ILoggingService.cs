using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operations;

namespace FileConductor.LoggingService
{
    public interface  ILoggingService
    {
        void LogInfo(IOperation operation,string message);
        void LogException(Exception exception,IOperation operation, string message);
        void LogInfo(string message);
        void LogException(Exception exception,string message);

    }
}
