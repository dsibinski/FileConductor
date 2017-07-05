using System;
using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Protocols;

namespace FileConductor.Operations
{
    public class Operation : IOperation
    {
        public OperationProperties Properties { get; set; }
        public int Id { get; set; }

        public Operation(IProtocol protocol, OperationProperties properties, int id)
        {
            Protocol = protocol;
            Properties = properties;
            Properties.AssignOperationHandler(NotificationHandler);
            Id = id;
        }

        public IProtocol Protocol { get; set; }

        public string Code { get; set; }
        public event OperationElapsedEventHandler OnOperationReady;
        private void NotificationHandler()
        {
            OnOperationReady?.Invoke(this,null);
        }

    }
}