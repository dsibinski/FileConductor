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
        public TargetData TargetData { get; set; }
        public TargetEditViewModel(ITabController tabController, TargetData data) : base(tabController)
        {
            Name = data.Code;
            TargetData = data;
        }
    }
}
