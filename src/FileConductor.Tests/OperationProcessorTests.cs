using System;
using Castle.Components.DictionaryAdapter.Xml;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.Schedule;
using FileConductor.Schedule.OperationSchedule;
using Moq;
using NUnit.Framework;

namespace FileConductor.Tests
{
    [TestFixture]
    internal class OperationProcessorTests
    {
        [SetUp]
        public void Setup()
        {
            _processor = new OperationProcessor();
            _processor.LoggingService = new Mock<ILoggingService>().Object;
            var schedule = new Mock<ISchedule>();
            _processor.Start(schedule.Object);
            var operationScheduleMock = new Mock<OperationScheduleBase>();
            var propertiesMock = new OperationProperties(operationScheduleMock.Object);
            var protocolMock = new Mock<IProtocol>();
            var operationMock = new Operation(protocolMock.Object,propertiesMock); //TODO: Operation is dependent of protocol - wrong architecture
           _operation = operationMock;
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