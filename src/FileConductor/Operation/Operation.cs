using System.Timers;
using FileConductor.Protocols;

namespace FileConductor.Operation
{
    public class Operation
    {
        public delegate void OperationElapsedEventHandler(Operation sender, ElapsedEventArgs e);

        private readonly OperationProperties _properties;
        private readonly IProtocol _protocol;

        public Operation(IProtocol protocol, OperationProperties properties)
        {
            _protocol = protocol;
            _properties = properties;
            _properties.SchedulerTimer.Elapsed += SchedulerExecute;
            _properties.SchedulerTimer.Start();
            protocol.Properties = _properties;
        }

        public event OperationElapsedEventHandler OnTimeElapsed;

        private void SchedulerExecute(object sender, ElapsedEventArgs e)
        {
            OnTimeElapsed?.Invoke(this, e);
        }

        public void Execute()
        {
            _protocol.ExecuteProcess();
        }
    }
}