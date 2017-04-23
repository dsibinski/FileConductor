using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.FileTransport.Local;
using FileConductor.FileTransport.SFTP;
using FileConductor.Helpers;
using FileConductor.Operation;
using FileConductor.Protocols;
using NUnit.Framework;

namespace FileConductor.Tests
{
    [TestFixture]
    public class GeneralTests
    {
        [Test]
        public void SampleTest()
        {
            Assert.AreEqual(3,3);
        }

        [Test]
        public void CheckSFTPProtocol()
        {
            var destination = new TargetTransformData("locahost", "c:/Destiny", "", "");
            var source = new TargetTransformData("locahost", "c:/Source", "", "");
            var operationProperties = new OperationProperties() { DestinationTarget = destination, Schedule = null, Regex = "*.txt", SourceTarget = source };
            var protocol = new Protocol(new LocalTransfer(), new LocalTransfer());
            var operation = new Operation.Operation(protocol, operationProperties);
            OperationExecutor exec = new OperationExecutor(new ProxyFileProvider(),new LoggingService.LoggingService());
            exec.Execute(operation);
        }

    }
}
