using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;

namespace FileConductor.Protocols
{
    public interface IProtocol 
    {
        ITransfer Receiver { get; set; }
        ITransfer Sender { get; set; }
    }
}
