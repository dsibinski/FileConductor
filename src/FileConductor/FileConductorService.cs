using System;
using System.IO;
using System.Timers;
using FileConductor.Protocols;

namespace FileConductor
{
    internal class FileConductorService
    {
        public bool Start()
        {
            var tee = DateTime.Now.TimeOfDay;
            var tmp1 = DateTime.Now.Hour;
            var tmp2 = DateTime.Now.Minute;
            var operationProp = new OperationProperties()
            {

                NotificationSettings = new SpecifiedTimeNotification(new int[] {0,1,2,3,4,5,6}, new TimeSpan(20,3,0)),
                DestinyPath = "C:/Destiny/",
                SourcePath = "C:/Source/",
                Regex = "*.csv"
            };

            var operation2Prop = new OperationProperties()
            {

                NotificationSettings = new SpecifiedTimeNotification(new int[] { 0, 1, 2, 3, 4, 5, 6 }, new TimeSpan(20,4, 0)),
                DestinyPath = "C:/Source/",
                SourcePath = "C:/Destiny/",
                Regex = "*.csv"
            };

             var operation = new Operation(new LocalProtocol(), operationProp);


            var operation2 = new Operation(new LocalProtocol(), operation2Prop);
            var operatio = new OperationProcessor();
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