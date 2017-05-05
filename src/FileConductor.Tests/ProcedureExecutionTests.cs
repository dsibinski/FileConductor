using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operations.ProcedureExecution;
using FileConductor.Tests.Mocks;
using NUnit.Framework;

namespace FileConductor.Tests
{
    [TestFixture]
    public class ProcedureExecutionTests
    {
        [Test]
        public void ExecutingProcedureTests()
        {
            ProcedureExecutionService service = new ProcedureExecutionService();
            service.LoggingService =  new LoggingServiceMock();
            service.ExecuteProcedure(@"PCWGRZESIAK2","gemo_dev","admin","admin", "Implementations.LaunchExportOfProductNomenclature");
        }


    }
}
