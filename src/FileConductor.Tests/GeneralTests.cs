using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.FileTransport.FtpFileTransport;
using FileConductor.FileTransport.LocalFileTransport;
using FileConductor.Operations;
using NUnit.Framework;

namespace FileConductor.Tests
{
    [TestFixture]
    public class GeneralTests
    {
        // Sample test class
        [Test]
        public void SampleTest()
        {
            Assert.AreEqual(3,3);
        }

        [Test]
        public void CheckSFTPProtocol()
        {
  
            var destination = new TargetTransformData("127.0.0.1", "/", "tester", "password");
            var source = new TargetTransformData("locahost","c:/source","","");
            var operationProperties = new OperationProperties() {DestinationTarget = destination,NotificationSettings = null, Regex = "*.csv",SourceTarget = source};
            var operation = new Operation(new Protocol(new LocalTransfer(), new SftpTransfer()),operationProperties,1);
            operation.Execute(); 

           

        }
    }
}
