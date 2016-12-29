using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.FileTransport
{
    public class Protocol
    {
        public Protocol(ITransfer receiver, ITransfer sender)
        {
            Receiver = receiver;
            Sender = sender;
        }

        public ITransfer Receiver { get; set; }
        public ITransfer Sender { get; set; }
    }
}