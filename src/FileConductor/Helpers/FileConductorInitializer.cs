using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.Protocols;

namespace FileConductor.Helpers
{
    public class FileConductorInitializer
    {
        public void InitializeOperations()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();


            var configurationData = deserializer.XmlData;

            foreach (var watcher in configurationData.Watchers)
            {
               var shedule =  configurationData.Schedules.First(x => x.Id == watcher.ScheduleId);

            }

            var operationProp = new OperationProperties()
            {
                NotificationSettings =
                    new SpecifiedTimeNotification(new int[] {0, 1, 2, 3, 4, 5, 6}, new TimeSpan(14, 27, 0)),
                DestinyPath = "C:/Destiny/",
                SourcePath = "C:/Source/",
                Regex = "*.csv"
            };

         

            var operation = new Operation(new LocalProtocol(), operationProp);


            var operatio = new OperationProcessor();
            operatio.AssignOperation(operation);
        }
    }
}