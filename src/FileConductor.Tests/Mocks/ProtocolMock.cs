using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.Protocols;

namespace FileConductor.Tests.Mocks
{
    public class ProtocolMock : IProtocol
    {
        public ITransfer Receiver { get; set; } = new TransferMock();
        public ITransfer Sender { get; set; } = new TransferMock();
    }
}
