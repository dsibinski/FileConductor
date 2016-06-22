using System.Timers;
using FileConductor.Protocols;

namespace FileConductor
{
    public class Operation
    {
        public delegate void OperationElapsedEventHandler(Operation sender, ElapsedEventArgs e);

        private readonly IProtocol _protocol;

        public Operation(IProtocol protocol, OperationProperties properties)
        {
            _protocol = protocol;
            protocol.Properties = properties;
            //protocol.Properties.SchedulerTimer.Elapsed += NotificationExecute;
            //protocol.Properties.SchedulerTimer.Start();
            protocol.Properties.NotificationSettings.OnElapsed += NotificationExecute;
           // protocol.Properties.SchedulerTimer.Start();
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