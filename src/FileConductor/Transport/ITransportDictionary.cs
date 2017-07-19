using FileConductor.FileTransport;

namespace FileConductor.Transport
{
    public interface ITransportDictionary
    {
        ITransfer GetTransfer(string type);
    }
}
