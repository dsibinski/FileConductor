using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.Tabs;
using FileConductor.Operations;
using FileConductorUI.UI;

namespace FileConductor.ConfigurationTool.ViewModels
{
   public class EditWatcherTabViewModel : Tab
   {
       public event Action OnOperationModified;

       public WatcherData Watcher { get; set; }
       public ConfigurationData Configuration { get; set; }

       public EditWatcherTabViewModel(ITabController controller, ConfigurationData config, WatcherData watcher) : base(controller)
        {
            Name = "Edit";
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
