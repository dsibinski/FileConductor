using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using ConfigurationTool.Entities;
using ConfigurationTool.Tabs;
using FileConductor.Configuration;
using FileConductor.LoggingService;
using FileConductor.Operations;
using Microsoft.Expression.Interactivity.Core;
using Ninject;

namespace ConfigurationTool.ViewModels
{
   public class EditWatcherTabViewModel : Tab
   {
       public bool IsProcedureTriggered
       {
           get { return Watcher.ProcedureData != null; }
           set
           {
               if (value == false)
               {
                    Watcher.ProcedureData = null;
                   Watcher.WatcherData.ProcedureId = 0;
               }
               else
               {
                   if (Configuration.Procedures.Any())
                   {
                       Watcher.ProcedureData = Configuration.Procedures.FirstOrDefault();
                   }
                   else
                   {
                       throw new NotImplementedException(
                           "No procedures defined in configuration file. This functionality is not implemented yet");
                   }
               }
           }
       }

       public event Action OnOperationModified;
       public Watcher Watcher { get; set; }
       public ConfigurationData Configuration { get; set; }

       public EditWatcherTabViewModel(ITabController controller, ConfigurationData config, Watcher watcher) : base(controller)
        {
            Name = watcher.WatcherData.Code;
            SaveCommand = new ActionCommand(SaveWatcher);
            TestCommand = new ActionCommand(TestWatcher);
            Watcher = watcher;
            Configuration = config;
        }

       private void TestWatcher()
       {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var operationExecutor = kernel.Get<IOperationExecutor>();
            var configurationService = kernel.Get<IConfigurationService>();
            var operation = configurationService.GetOperation(Configuration, Watcher.WatcherData);
            operationExecutor.Execute(operation);
        }

       private void SaveWatcher()
       {
           OnOperationModified?.Invoke();
       }

       public ActionCommand SaveCommand { get; set; }
       public ActionCommand TestCommand { get; set; }

    }
}
