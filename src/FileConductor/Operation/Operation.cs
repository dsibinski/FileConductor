using System;
using System.Timers;
using FileConductor.Protocols;

namespace FileConductor
{
    public class Operation
    {
        public int Id { get; set; }

        public delegate void OperationElapsedEventHandler(Operation sender, ElapsedEventArgs e);

        private readonly ProtocolExecutor _protocolExecutor;

        public Operation(IProtocol protocol, OperationProperties properties, int id)
        {
            _protocolExecutor = new ProtocolExecutor(protocol, properties, NotificationHandler);
            Id = id;
        }

        public event OperationElapsedEventHandler OnTimeElapsed;

        private void NotificationHandler(object sender, ElapsedEventArgs e)
        {
            OnTimeElapsed?.Invoke(this, e);
        }

        public void Execute()
        {
            _protocolExecutor.ExecuteProcess();
        }
    }
}