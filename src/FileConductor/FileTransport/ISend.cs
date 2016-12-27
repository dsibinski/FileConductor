using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.FileTransport
{
    public interface ISend
    {
        void Send(string ip, string path, string fileName);
    }
}