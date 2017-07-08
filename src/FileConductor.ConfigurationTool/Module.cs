using FileConductor.Configuration;
using FileConductor;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Operations.ProcedureExecution;
using FileConductor.ProxyFile;
using Ninject.Modules;

namespace ConfigurationTool
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileConductor>().To<FileConductor.FileConductor>();
            Bind<IConfigurationService>().To<ConfigurationService>();
            Bind<ILoggingService>().To<LoggingService>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
            Bind<IOperationExecutor>().To<OperationExecutor>();
            Bind<IProxyFileProvider>().To<ProxyFileProvider>();
            Bind<IProcedureExecutionService>().To<ProcedureExecutionService>();
        }
    }
}
