using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.FileTransport.FtpFileTransport;
using FileConductor.FileTransport.LocalFileTransport;

namespace FileConductor.FileTransport
{
    public class TransportManager
    {
        public static void Initialize()
        {
            TransportFactory.AddTransferImplementation(new LocalTransfer());
            TransportFactory.AddTransferImplementation(new FtpTransfer());
        }
        
    }
}
