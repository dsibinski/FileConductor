using System;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.Schedule.OperationSchedule;
using FileConductor.Tests.Mocks;
using NUnit.Framework;

namespace FileConductor.Tests
{
    [TestFixture]
    internal class OperationProcessorTests
    {
        [SetUp]
        public void Setup()
        {
            ILoggingService logginService = new LoggingServiceMock();
            _processor = new OperationProcessor(logginService,
                new OperationExecutor(new ProxyFileProviderMock(), logginService));
            OperationProperties properties = new OperationProperties(new OperationScheduleMock());
            _operation = new Operation(new ProtocolMock(),properties);
           _processor.Start(new ScheduleMock());
        }

        private OperationProcessor _processor;
        private Operation _operation;


        [Test]
        public void CheckOperationAssingation()
        {
            Assert.AreEqual(_processor.Operations.Count, 0);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 0);
            _processor.AssignOperation(_operation);
            Assert.AreEqual(_processor.Operations.Count, 1);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 0);
        }

        [Test]
        public void CheckOperationQueue()
        {
            _processor.AssignOperation(_operation);
            _operation.Properties.Schedule.OperationScheduleElapsed();
            Assert.AreEqual(_processor.Operations.Count, 1);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 1);
        }

        [Test]
        public void CheckOperationExecution()
        {
            _processor.AssignOperation(_operation);
            _operation.Properties.Schedule.OperationScheduleElapsed();
            Assert.AreEqual(_processor.Operations.Count, 1);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 1);
           _processor.ProcessOperation();
            Assert.AreEqual(_processor.Operations.Count, 1);
            Assert.AreEqual(_processor.OperationsToExecute.Count, 0);
        }
    }
}