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
        IOperationProcessor GetOperationProcessor(ConfigurationData configurationData);
        IOperation GetOperation(ConfigurationData configurationData, WatcherData watcher);
    }
}
