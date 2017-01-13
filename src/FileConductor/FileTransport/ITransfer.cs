using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.FileTransport
{
    public interface ITransfer
    {
        List<string> Receive(TargetTransformData sourceData,string targetPath,string regex);
        void Send(TargetTransformData targetData,List<string> files,string regex);

    }
}