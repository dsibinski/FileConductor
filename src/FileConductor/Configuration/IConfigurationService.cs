using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.Operations;

namespace FileConductor.Configuration
{
    public interface IConfigurationService
    {
        void InitializeOperationProcessor(IOperationProcessor operationProcessor, ConfigurationData configurationData);
        IOperation GetOperation(ConfigurationData configurationData, WatcherData watcher);
        ConfigurationData GetConfigurationData();
        void SaveConfigurationData(ConfigurationData configuration);
        T GetEmptyObject<T>(ConfigurationData configuration) where T : IConfigurationElement, new();
        void RemoveObject<T>(ConfigurationData configuration, T obj) where T : IConfigurationElement, new();
    }
}
