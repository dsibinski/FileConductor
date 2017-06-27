using FileConductor.Configuration;
using FileConductor.ConfigurationTool.ViewModels;
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
            Bind<IFileConductor>().To<FileConductor>();
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<ILoggingService>().To<LogginServiceWindow>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
            Bind<IOperationExecutor>().To<OperationExecutor>();
            Bind<IProxyFileProvider>().To<ProxyFileProvider>();
            Bind<IProcedureExecutionService>().To<ProcedureExecutionService>();
        }
    }
}
