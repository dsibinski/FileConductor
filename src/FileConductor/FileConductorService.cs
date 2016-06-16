using System.IO;
using System.Timers;

namespace FileConductor
{
    internal class FileConductorService
    {
        public bool Start()
        {

            var operationProp = new OperationProperties()
            {
                ShedulerTimer = new Timer(10000),
                DestinyPath = "C:/Destiny/",
                SourcePath = "C:/Source/",
                Regex = "*.csv"
            };

            var operation2Prop = new OperationProperties()
            {
                ShedulerTimer = new Timer(10000),
                DestinyPath = "C:/Source/",
                SourcePath = "C:/Destiny/",
                Regex = "*.csv"
            };

            var operation = new Operation(new LocalProtocol(), operationProp);
            var operation2 = new Operation(new LocalProtocol(), operation2Prop);
            var operatio = new OperationSheduler();
            operatio.AssignOperation(operation);
            operatio.AssignOperation(operation2);
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