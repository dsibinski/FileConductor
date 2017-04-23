using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operation;
using FileConductor.Protocols;
using FileConductor.Schedule.OperationSchedule;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FileConductor.Tests
{
    [TestFixture]
    class OperationProcessorTests
    {
        private OperationProcessor _processor;

        [SetUp]
        public void Setup()
        {
            ILoggingService logginService = new LoggingServiceMock();
            _processor = new OperationProcessor(logginService, new OperationExecutor(new ProxyFileProviderMock(), logginService));
        }


        [Test]
        public void CheckAssigningOperation()
        {
            Assert.AreEqual(_processor.Operations.Count, 0);
            Assert.AreEqual(_processor.OperationsToExecute.Count,0);
            var operation = new OperationMock();
            _processor.AssignOperation(operation);
            Assert.AreEqual(_processor.Operations.Count,1);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 0);
        }

        [Test]
        public void CheckOperationExecution()
        {
            var operation = new OperationMock();
            _processor.AssignOperation(operation);
            var operationSchedule = new OperationScheduleMock();
            operation.Properties.Schedule = operationSchedule;
            operationSchedule.OperationScheduleElapsed(null,null);
            Assert.AreEqual(_processor.Operations.Count, 1);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 1);

        }


        public class LoggingServiceMock : ILoggingService
        {
            public void LogInfo(IOperation operation, string message)
            {
                
            }

            public void LogException(Exception exception, IOperation operation, string message)
            {
                
            }
        }

        public class OperationExecutorMock : IOperationExecutor
        {
            public void Execute(IOperation operation)
            {
             
            }
        }

        public class ProxyFileProviderMock : IProxyFileProvider
        {
            public string ProxyPath { get; set; }
        }

        public class OperationMock : IOperation
        {
            public IProtocol Protocol { get; set; }
            public string Code { get; set; }
            public event OperationElapsedEventHandler OnOperationReady;
            public OperationProperties Properties { get; set; }

            public OperationMock()
            {
                Properties = new OperationProperties();
            }
        }

        public class OperationScheduleMock:OperationScheduleBase
        {
            //public OperationScheduleMock()
            //{
 
            //}

            //private void YMP(object sender, ElapsedEventArgs e)
            //{
                
            //}
        }
    }

    


}
