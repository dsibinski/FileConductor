using FileConductor.Configuration;
using FileConductor;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Operations.ProcedureExecution;
using FileConductor.ProxyFile;
using FileConductor.Schedule;
using FileConductor.Transport;
using Ninject.Modules;

namespace ConfigurationTool
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileConductor>().To<FileConductor.FileConductor>();
            Bind<ITransportDictionary>().To<TransportDictionary>();
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<ILoggingService>().To<LoggingService>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
            Bind<IOperationExecutor>().To<OperationExecutor>();
            Bind<IProxyFileProvider>().To<ProxyFileProvider>();
            Bind<IProcedureExecutionService>().To<ProcedureExecutionService>();
            Bind<ISchedule>().To<IntervalSchedule>().WithConstructorArgument(typeof(int), (Constants.SchedulerIntervaltime));
        }
    }
}
