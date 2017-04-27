using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.LoggingService;
using FileConductor.Operations;

namespace FileConductor.Tests.Mocks
{
    public class LoggingServiceMock : ILoggingService
    {
        public void LogInfo(IOperation operation, string message)
        {
        }

        public void LogException(Exception exception, IOperation operation, string message)
        {
        }

        public void LogInfo(string message)
        {
        }

        public void LogException(Exception exception, string message)
        {
        }
    }
}
