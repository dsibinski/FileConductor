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
                var shedule = configurationData.Schedules.First(x => x.Id == watcher.ScheduleId);
                var database = configurationData.Databases.First(x => x.Id == watcher.DatabaseId);
                var sourceTarget = configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.SourceTargetId);
                var sourceServer = configurationData.Servers.First(x => x.Id == sourceTarget.ServerId);
                var destinationTarget =
                    configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.DestinationTargetId);
                var destinationServer = configurationData.Servers.First(x => x.Id == destinationTarget.ServerId);

                var days = shedule.DaysOfWeek.Split(Constants.DaysSeparator).Cast<int>().ToArray();
               // var hour = DateTime.TryParse(shedule.Hours,);
                var operationPropTmp = new OperationProperties()
                {
                    DestinyPath = destinationTarget.Path,

                    NotificationSettings =
                        new SpecifiedTimeNotification(days, new TimeSpan()), //TODO: WHAT HERE?   seperator ;   
                    SourcePath = sourceTarget.Path,
                    Regex = destinationTarget.Name   //TODO: REGEX?
                };
                var operation = new Operation(ProtocolFactory.GetProtocol(destinationServer.Protocol), operationPropTmp);

            }


            var operatio = new OperationProcessor();
            //operatio.AssignOperation(operation);
        }
    }
}