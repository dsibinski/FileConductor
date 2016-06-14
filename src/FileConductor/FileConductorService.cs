using System.Timers;

namespace FileConductor
{
    internal class FileConductorService
    {
        public bool Start()
        {
            var operationProp = new OperationProperties()
            {
                ShedulerTimer = new Timer(10),
                DestinyPath = "C:/Destiny/",
                SourcePath = "C:/Source/",
                Regex = "*csv"
            };

            var operation = new Operation(new LocalProtocol(), operationProp);
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