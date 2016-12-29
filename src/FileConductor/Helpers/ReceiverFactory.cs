using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.FileTransport.Implementations;
using FileConductor.Protocols;

namespace FileConductor.Helpers
{
    public class ReceiverFactory
    {
        public static IReceiver GetReceiver(TransferType type)
        {
            switch (type)
            {
                case TransferType.Local:
                    return new LocalReceiver();
                default:
                    throw new Exception("No protocol with specified type!");
            }

        }
    }
}
