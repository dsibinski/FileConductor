using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationTool.Entities;
using ConfigurationTool.Tabs;
using FileConductor.Configuration.XmlData;

namespace ConfigurationTool.ViewModels
{
    public class TargetEditViewModel : Tab
    {
        public event Action OnTargetModified;
        public TargetEditViewModel(ITabController tabController, Watcher data) : base(tabController)
        {
            Name = data.ProcedureData.Code;
        }
    }
}
