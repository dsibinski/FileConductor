using System.Timers;

namespace FileConductor
{
    public class Operation
    {
        public delegate void OperationElapsedEventHandler(Operation sender, ElapsedEventArgs e);
        private readonly OperationProperties _properties;
        private readonly IProtocol _protocol;
        public event OperationElapsedEventHandler OnTimeElapsed;

        public Operation(IProtocol protocol, OperationProperties properties)
        {
            _protocol = protocol;
            _properties = properties;
            _properties.ShedulerTimer.Elapsed += ShedulerExecute;
        }

        private void ShedulerExecute(object sender, ElapsedEventArgs e)
        {
            Operation operation = (Operation) sender;
            OnTimeElapsed?.Invoke(operation, e);
        }

        public void Execute()
        {
            _protocol.ExecuteProcess();
        }
    }
}