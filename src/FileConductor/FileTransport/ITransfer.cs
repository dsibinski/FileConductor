using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.FileTransport
{
    //TODO: Maybe absolete
    public interface ITransfer
    {
        void Transfer(TargetTransformData targetData,string regex);
    }
}