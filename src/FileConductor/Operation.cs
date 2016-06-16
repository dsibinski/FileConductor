using System.Timers;

namespace FileConductor
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
            _properties.ShedulerTimer.Elapsed += ShedulerExecute;
            _properties.ShedulerTimer.Start();
            protocol.Properties = _properties;
        }

        public event OperationElapsedEventHandler OnTimeElapsed;

        private void ShedulerExecute(object sender, ElapsedEventArgs e)
        {
            OnTimeElapsed?.Invoke(this, e);
        }

        public void Execute()
        {
            _protocol.ExecuteProcess();
        }
    }
}