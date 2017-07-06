using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.FileTransport;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.Schedule;
using Ninject;

namespace FileConductor.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private XmlSerializer<ConfigurationData> serializer;
        public void InitializeOperationProcessor(IOperationProcessor operationProcessor,ConfigurationData configurationData)
        {
            foreach (var watcher in configurationData.Watchers)
            {
                operationProcessor.AssignOperation(GetOperation(configurationData, watcher));
            }
        }

        public ConfigurationService()
        {
            serializer = new XmlSerializer<ConfigurationData>("Configuration\\Config.xml");
        }

        public IOperation GetOperation(ConfigurationData configurationData, WatcherData watcher)
        {
            var schedule = configurationData.Schedules.First(x => x.Id == watcher.ScheduleId);
            var sourceTarget = configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.SourceTargetId);
            var sourceServer = configurationData.Servers.First(x => x.Id == sourceTarget.ServerId);
            var destinationTarget =
                configurationData.Targets.First(x => x.ServerId == watcher.WatcherRouting.DestinationTargetId);
            var destinationServer = configurationData.Servers.First(x => x.Id == destinationTarget.ServerId);
            var procedureData = configurationData.Databases.FirstOrDefault(x => x.Id == watcher.DatabaseId);
            var operationProperties = FillOperationsProperties(destinationTarget, destinationServer, schedule,
                sourceTarget, sourceServer, watcher, procedureData);
            var receiver = TransportFactory.GetTransfer(sourceServer.Protocol);
            var sender = TransportFactory.GetTransfer(destinationServer.Protocol);
            var protocol = new Protocol(receiver, sender);
            var operation = new Operation(protocol, operationProperties,watcher.Id);
            operation.Code = watcher.Code;
            return operation;
        }

        public ConfigurationData GetConfigurationData()
        {
            serializer.Deserialize();
            return serializer.XmlData;
        }

        public void SaveConfigurationData(ConfigurationData configuration)
        {
            serializer.Serialize(configuration);
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
                    new ProcedureData(dbData.Host, dbData.User, dbData.Password, dbData.Name, dbData.DatabaseName);
            }
            return operationProperties;
        }
    }
}