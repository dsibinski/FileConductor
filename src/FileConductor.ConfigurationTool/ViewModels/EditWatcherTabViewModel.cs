using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.ConfigurationTool.Tabs;

namespace FileConductor.ConfigurationTool.ViewModels
{
   public class EditWatcherTabViewModel : Tab
    {

        public EditWatcherTabViewModel(ITabController controller) : base(controller)
        {
            Name = "Edit";
        }
    }
}
