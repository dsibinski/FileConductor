using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operations;

namespace FileConductor.FileTransport
{
    /// <summary>
    /// Implement to transferable object (able to send/receive files)
    /// </summary>
    public interface ITransfer
    {
        List<string> Receive(TargetTransformData sourceData, string targetPath, string regex);
        void Send(TargetTransformData targetData, IList<string> files);

    }
}