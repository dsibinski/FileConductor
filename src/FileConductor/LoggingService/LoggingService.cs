using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace FileConductor.LoggingService
{
    class LoggingService : ILoggingService
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(Operation.Operation operation, string message)
        {
            logger.Info(message);
        }

        public void LogException(Exception exception, Operation.Operation operation, string message)
        {
            logger.Info(message);
        }
    }
}
