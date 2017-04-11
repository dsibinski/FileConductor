using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Protocols;

namespace FileConductor.Operation
{
    public class Operation
    {
        public delegate void OperationElapsedEventHandler(Operation sender, ElapsedEventArgs e);

        private readonly ProtocolExecutor _protocolExecutor;

        public Operation(Protocol protocol, OperationProperties properties, string code)
        {
            _protocolExecutor = new ProtocolExecutor(protocol, properties, NotificationHandler);
           Code = code;
        }

        public string Code { get; set; }

        public event OperationElapsedEventHandler OnTimeElapsed;

        private void NotificationHandler(object sender, ElapsedEventArgs e)
        {
            OnTimeElapsed?.Invoke(this, e);
        }

        public void Execute()
        {
            _protocolExecutor.ExecuteProtocol();
        }
    }
}