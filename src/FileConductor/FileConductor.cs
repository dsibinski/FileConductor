using System;
using System.Linq;
using System.Reflection;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.Schedule;
using Ninject;

namespace FileConductor
{
    public class FileConductor
    {
        [Inject]
        public ILoggingService LoggingService { private get; set; }
        public FileConductor(IOperationProcessor operationProcessor)
        {
            OperationProcessor = operationProcessor;
        }

        private IOperationProcessor OperationProcessor { get; set; }

        public void Initialize(ConfigurationData configurationData)
        {
            try
            {
                TransportManager.Initialize();
                LoadConfigurations(configurationData);
               
            }
            catch (Exception exception)
            {
                LoggingService.LogException(exception,"Loading configuration failed");
            }
            OperationProcessor.Start(new IntervalSchedule(Constants.SchedulerIntervaltime));
        }

        private void LoadConfigurations(ConfigurationData configurationData)
        {
            foreach (var watcher in configurationData.Watchers)
            {
                var schedule = configurationData.Schedules.First(x => x.Id == watcher.ScheduleId);
                var sourceTarget = configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.SourceTargetId);
                var sourceServer = configurationData.Servers.First(x => x.Id == sourceTarget.ServerId);
                var destinationTarget =
                    configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.DestinationTargetId);
                var destinationServer = configurationData.Servers.First(x => x.Id == destinationTarget.ServerId);
                var procedureData = configurationData.Databases.FirstOrDefault(x => x.Id == watcher.DatabaseId);
                var operationProperties = FillOperationsProperties(destinationTarget, destinationServer, schedule,
                    sourceTarget, sourceServer, watcher, procedureData);
                var receiver = TransportFactory.GetTransfer(sourceServer.Protocol);
                var sender = TransportFactory.GetTransfer(destinationServer.Protocol);
                var protocol = new Protocol(receiver, sender);
                var operation = new Operation(protocol, operationProperties);
                operation.Code = watcher.Code;
                OperationProcessor.AssignOperation(operation);
            }
        }

        private OperationProperties FillOperationsProperties(TargetData destinationTarget, ServerData destinationServer,
            ScheduleData schedule, TargetData sourceTarget, ServerData sourceServer, WatcherData watcher,
            DatabaseData dbData)
        {
            var operationProperties = new OperationProperties(OperationScheduleFactory.GetSchedule(schedule))
            {
                DestinationTarget =
                    new TargetTransformData(destinationServer.Ip, destinationTarget.Path, destinationServer.User,
                        destinationServer.Password),
                SourceTarget =
                    new TargetTransformData(sourceServer.Ip, sourceTarget.Path, sourceServer.User, sourceServer.Password),
                Regex = watcher.FileNameRegex,
            };
            if (dbData != null)
            {
                operationProperties.ProcedureData =
               new ProcedureData(dbData.Host, dbData.User, dbData.Password, dbData.Name,dbData.DatabaseName);
            }        
            return operationProperties;
        }
    }
}