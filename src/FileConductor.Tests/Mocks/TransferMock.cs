using System.Collections.Generic;
using FileConductor.FileTransport;
using FileConductor.Operations;

namespace FileConductor.Tests.Mocks
{
    public class TransferMock : ITransfer
    {
        public string Name => "Mock";

        public List<string> Receive(TargetTransformData sourceData, string targetPath, string regex)
        {
            return new List<string>();
        }

        public void Send(TargetTransformData targetData, IList<string> files)
        {
        }
    }
}