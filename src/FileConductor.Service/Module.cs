using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Protocols;
using Ninject.Modules;

namespace FileConductor.Service
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggingService>().To<LoggingService.LoggingService>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
            Bind<IProxyFileProvider>().To<ProxyFileProvider>();
            Bind<IOperationExecutor>().To<OperationExecutor>();
        }
    }
}
