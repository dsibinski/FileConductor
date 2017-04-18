using FileConductor.FileTransport;

namespace FileConductor.Protocols
{
    /// <summary>
    /// Repsesents a sender-receiver entity called Protocol
    /// </summary>
    public class Protocol : IProtocol
    {
        public Protocol(ITransfer receiver, ITransfer sender)
        {
            Receiver = receiver;
            Sender = sender;
        }

        public ITransfer Receiver { get; set; }
        public ITransfer Sender { get; set; }
    }
}