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
    public class FileConductor : IFileConductor
    {
        [Inject]
        public ILoggingService LoggingService { private get; set; }

        [Inject]
        public IConfigurationService ConfigurationService { get; set; }

        [Inject]
        public IOperationProcessor OperationProcessor { get; set; }

        [Inject]
        public ISchedule FileConductorScheduler { get; set; }

        public void Start(ConfigurationData configurationData)
        {
            try
            {
                ConfigurationService.InitializeOperationProcessor(OperationProcessor,configurationData);
            }
            catch (Exception exception)
            {
                LoggingService.LogException(exception, "Loading configuration failed");
            }
            FileConductorScheduler.StartSchedule(OperationProcessor.ProcessOperation);
        }

        public void Stop()
        {
            FileConductorScheduler.StopSchedule();
        }
    }
}