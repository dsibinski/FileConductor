using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Protocols;

namespace FileConductor.FileTransport
{
    public interface ITransfer
    {
        string Name { get; }
        List<string> Receive(TargetTransformData sourceData,string targetPath,string regex);
        void Send(TargetTransformData targetData,List<string> files);

    }
}