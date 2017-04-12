using System.Linq;
using System.Reflection;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.Ninject;
using FileConductor.Operation;
using FileConductor.Protocols;
using FileConductor.Schedule;
using Ninject;

namespace FileConductor
{
    public class FileConductor
    {
        
        public void Initialize(ConfigurationData configurationData)
        {
            TransportManager.Initialize();
            var operationProcessor = IoC.Resolve<IOperationProcessor>();
            foreach (var watcher in configurationData.Watchers)
            {
                var schedule = configurationData.Schedules.First(x => x.Id == watcher.ScheduleId);
                               var sourceTarget = configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.SourceTargetId);
                var sourceServer = configurationData.Servers.First(x => x.Id == sourceTarget.ServerId);
                var destinationTarget =
                    configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.DestinationTargetId);
                var destinationServer = configurationData.Servers.First(x => x.Id == destinationTarget.ServerId);
                string operationCode = watcher.Code;

                var operationProperties = FillOperationsProperties(destinationTarget, destinationServer, schedule,
                    sourceTarget, sourceServer, watcher);

                var receiver = TransportFactory.GetTransfer(sourceServer.Protocol);
                var sender = TransportFactory.GetTransfer(destinationServer.Protocol);
                var protocol = new Protocol(receiver, sender);

                operationProcessor.AssignOperation(new Operation.Operation(protocol, operationProperties, operationCode));
            }
        }

        private OperationProperties FillOperationsProperties(TargetData destinationTarget, ServerData destinationServer, ScheduleData schedule, TargetData sourceTarget, ServerData sourceServer, WatcherData watcher)
        {
            var operationProperties = new OperationProperties()
            {
                DestinationTarget =
              new TargetTransformData(destinationServer.Ip, destinationTarget.Path, destinationServer.User,
                  destinationServer.Password),
                Schedule = ScheduleFactory.GetSchedule(schedule),
                SourceTarget =
              new TargetTransformData(sourceServer.Ip, sourceTarget.Path, sourceServer.User, sourceServer.Password),
                Regex = watcher.FileNameRegex
            };
            return operationProperties;
        }
    }
}