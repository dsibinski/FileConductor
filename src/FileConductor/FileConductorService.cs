using System.Timers;

namespace FileConductor
{
    internal class FileConductorService
    {
        public bool Start()
        {
            var operation = new Operation(new LocalProtocol(), new OperationProperties() {ShedulerTimer =  new Timer()});

            var operatio = new OperationSheduler();
            operatio.AssignOperation(operation);
            // Service's initialization logic
            // Executed when the service starts
            return true;
        }

        public bool Stop()
        {
            // Service's disposing logic
            // Executed when the service is stopped/closed
            return true;
        }
    }
}