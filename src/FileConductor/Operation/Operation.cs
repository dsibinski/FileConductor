using System.Timers;
using FileConductor.Protocols;

namespace FileConductor
{
    public class Operation
    {
        public int Id { get; set; }

        public delegate void OperationElapsedEventHandler(Operation sender, ElapsedEventArgs e);

        private readonly Protocol _protocol;

        public Operation(Protocol protocol, OperationProperties properties,int id)
        {
            _protocol = protocol;
            protocol.Properties = properties;
            protocol.Properties.NotificationSettings.OnElapsed += NotificationExecute;
            Id = id;
        }

        public event OperationElapsedEventHandler OnTimeElapsed;

        private void NotificationExecute(object sender, ElapsedEventArgs e)
        {
            OnTimeElapsed?.Invoke(this, e);
        }

        public void Execute()
        {
            _protocol.ExecuteProcess();
        }
    }
}