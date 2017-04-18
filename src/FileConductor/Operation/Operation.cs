using System;
using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Protocols;

namespace FileConductor.Operation
{
    public class Operation : IOperation
    {
        public OperationProperties Properties { get; set; }
        public Operation(IProtocol protocol, OperationProperties properties)
        {
            Protocol = protocol;
            Properties = properties;
            Properties.AssignOperationHandler(NotificationHandler);
        }

        public IProtocol Protocol { get; set; }

        public string Code { get; set; }
        public event OperationElapsedEventHandler OnTimeElapsed;
        private void NotificationHandler(object sender, ElapsedEventArgs e)
        {
            OnTimeElapsed?.Invoke(this, e);
        }
    }
}