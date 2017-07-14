using FileConductor.Configuration;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Operations.ProcedureExecution;
using FileConductor.Protocols;
using FileConductor.ProxyFile;
using FileConductor.Schedule;
using Ninject.Modules;

namespace FileConductor.Service
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileConductor>().To<FileConductor>();
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<ILoggingService>().To<LoggingService.LoggingService>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
            Bind<IOperationExecutor>().To<OperationExecutor>();
            Bind<IProxyFileProvider>().To<ProxyFileProvider>();
            Bind<IProcedureExecutionService>().To<ProcedureExecutionService>();
            Bind<ISchedule>().To<IntervalSchedule>().WithConstructorArgument(typeof(int), (Constants.SchedulerIntervaltime));
        }
    }
}
