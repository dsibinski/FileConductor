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
            Logger.Info($"<Watcher: {operation.Code}>: {message}");
        }

        public void LogException(Exception exception, IOperation operation, string message)
        {  
            Logger.Log(LogLevel.Error,exception, $"<Watcher: {operation.Code}> Exception occured!");
        }

        public void LogInfo(string message)
        {
            Logger.Info(message);
        }
        public void LogException(Exception exception, string message)
        {
            Logger.Log(LogLevel.Error, exception);
        }
    }
}
