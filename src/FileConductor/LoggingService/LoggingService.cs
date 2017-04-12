using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace FileConductor.LoggingService
{
    public class LoggingService : ILoggingService
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(Operation.Operation operation, string message)
        {
            Logger.Info(message);
        }

        public void LogException(Exception exception, Operation.Operation operation, string message)
        {
            StringBuilder callstack = new StringBuilder();
            callstack.AppendLine(message);
            callstack.AppendLine($"<Code: {operation.Code}> Exception occured");
            callstack.AppendLine(exception.Message);



            Logger.Info(message);
        }
    }
}
