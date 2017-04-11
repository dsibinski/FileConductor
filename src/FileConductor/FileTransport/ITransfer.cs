using System.Collections.Generic;
using FileConductor.Operation;

namespace FileConductor.FileTransport
{
    /// <summary>
    /// Implement to transferable object (able to send/receive files)
    /// </summary>
    public interface ITransfer
    {
        string Name { get; }
        List<string> Receive(TargetTransformData sourceData, string targetPath, string regex);
        void Send(TargetTransformData targetData, IList<string> files);

    }
}