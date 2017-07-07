using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using ConfigurationTool.Entities;
using ConfigurationTool.Tabs;
using FileConductor.Operations;

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
                   Watcher.WatcherData.DatabaseId = 0;
               }
               else
               {
                   if (Configuration.Databases.Any())
                   {
                       Watcher.ProcedureData = Configuration.Databases.FirstOrDefault();
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
            SaveCommand = new CommandHandler(SaveWatcher);
            Watcher = watcher;
            Configuration = config;
        }

       private void SaveWatcher()
       {
           OnOperationModified?.Invoke();
       }

       public CommandHandler SaveCommand { get; set; }

   }
}
