using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.FileTransport.Implementations;
using FileConductor.Protocols;

namespace FileConductor.Helpers
{
    public class SenderFactory
    {
        public static ISender Getsender(TransferType type)
        {
            switch (type)
            {
                case TransferType.Local:
                    return new LocalSender();
                default:
                    throw new Exception("No protocol with specified type!");
            }

        }
    }
}
