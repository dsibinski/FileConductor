using FileConductor.Configuration;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Operations.ProcedureExecution;
using FileConductor.Protocols;
using FileConductor.ProxyFile;
using Ninject.Modules;

namespace FileConductor.Service
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<ILoggingService>().To<LoggingService.LoggingService>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
            Bind<IProxyFileProvider>().To<ProxyFileProvider>();
            Bind<IOperationExecutor>().To<OperationExecutor>();
            Bind<IProcedureExecutionService>().To<ProcedureExecutionService>();

        }
    }
}
