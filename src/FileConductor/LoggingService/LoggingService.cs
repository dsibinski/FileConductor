using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operations;
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
            callstack.AppendLine($"<Code: {operation.Code}> Exception occured!");
            callstack.AppendLine(message);
            Exception currentException = exception;
            while (currentException != null)
            {
                callstack.AppendLine(currentException.Message);
                currentException = currentException.InnerException;
            }
            Logger.Info(callstack.ToString);    
        }

        public void LogInfo(string message)
        {
            Logger.Info(message);
        }

        public void LogException(Exception exception, string message)
        {
            StringBuilder callstack = new StringBuilder();
            callstack.AppendLine(message);

            Exception currentException = exception;
            while (currentException != null)
            {
                callstack.AppendLine(currentException.Message);
                currentException = currentException.InnerException;
            }
            Logger.Info(callstack.ToString);
        }
    }
}
