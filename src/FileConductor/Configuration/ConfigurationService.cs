using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.FileTransport;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.Schedule;
using FileConductor.Transport;
using Ninject;
using ProcedureData = FileConductor.Configuration.XmlData.ProcedureData;

namespace FileConductor.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        [Inject]
        public ITransportDictionary TransportDictionary { get; set; }

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
            var procedureData = configurationData.Procedures.FirstOrDefault(x => x.Id == watcher.ProcedureId);
            var operationProperties = FillOperationsProperties(destinationTarget, destinationServer, schedule,
                sourceTarget, sourceServer, watcher, procedureData);
            var receiver = TransportDictionary.GetTransfer(sourceServer.Protocol);
            var sender = TransportDictionary.GetTransfer(destinationServer.Protocol);
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
            ProcedureData dbData)
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
                operationProperties.ProcedureData = dbData;
            }
            return operationProperties;
        }

        public T GetEmptyObject<T>(ConfigurationData configuration) where T: IConfigurationElement, new()
        {
            ObservableCollection<T> castedListOfProperties = GetCastedListOfProperties<T>(configuration);
            int i = 1;
            var newWatcherData = new T();
            bool idNotFound = true;
            while (idNotFound)
            {
                if (castedListOfProperties.Any(x => x.Id == i))
                {
                    i++;
                }
                else
                {
                    idNotFound = false;
                }
            }
            newWatcherData.Id = i;
            castedListOfProperties.Add(newWatcherData);
            return newWatcherData;
        }

        public void RemoveObject<T>(ConfigurationData configuration, T obj) where T : IConfigurationElement, new()
        {
            ObservableCollection<T> castedListOfProperties = GetCastedListOfProperties<T>(configuration);
            castedListOfProperties.Remove(obj);
        }

        private ObservableCollection<T> GetCastedListOfProperties<T>(ConfigurationData configuration) where T : new()
        {
            var properties = configuration.GetType().GetProperties();
            var propertyOfType = properties.FirstOrDefault(x => x.PropertyType == typeof(ObservableCollection<T>));
            if (propertyOfType == null) throw new Exception("Something wrong with configuration");
            return (ObservableCollection<T>)propertyOfType.GetValue(configuration);
        }
    }
}