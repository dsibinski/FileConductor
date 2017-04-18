using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operation;
using NLog;

namespace FileConductor.LoggingService
{
    public class LoggingService : ILoggingService
    {
        protected Logger Logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(IOperation operation, string message)
        {
            Logger.Info(message);
        }

        public void LogException(Exception exception, IOperation operation, string message)
        {
            StringBuilder callstack = new StringBuilder();
            callstack.AppendLine(message);
            callstack.AppendLine($"<Code: {operation.Code}> Exception occured");
            callstack.AppendLine(exception.Message);
            Logger.Info(message);
        }
    }
}
