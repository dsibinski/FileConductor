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
        public FileConductor(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        [Inject]
        public ILoggingService LoggingService { private get; set; }

        public IConfigurationService ConfigurationService { get; set; }

        private IOperationProcessor OperationProcessor { get; set; }

        public void Initialize(ConfigurationData configurationData)
        {
            try
            {
                TransportManager.Initialize();
                OperationProcessor = ConfigurationService.GetOperationProcessor(configurationData);
            }
            catch (Exception exception)
            {
                LoggingService.LogException(exception, "Loading configuration failed");
            }
            OperationProcessor.Start(new IntervalSchedule(Constants.SchedulerIntervaltime));
        }
    }
}