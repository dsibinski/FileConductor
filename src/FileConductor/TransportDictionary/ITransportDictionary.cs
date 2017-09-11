using System.Collections.Generic;
using FileConductor.FileTransport;

namespace FileConductor.Transport
{
    public interface ITransportDictionary
    {
        ITransfer GetTransfer(string type);
        IEnumerable<ITransfer> Transfers { get; }
    }
}
