using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.LoggingService;
using Ninject.Modules;

namespace FileConductor.Ninject
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoggingService>().To<LoggingService.LoggingService>();
            Bind<IOperationProcessor>().To<OperationProcessor>();
        }
    }
}
