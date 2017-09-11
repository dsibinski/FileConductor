using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationTool.Tabs;
using FileConductor.Configuration.XmlData;

namespace ConfigurationTool.ViewModels
{
    public class ScheduleEditTabViewModel :Tab
    {
        public ScheduleData CurrentScheduleData{ get; set; }
        public ScheduleEditTabViewModel(ITabController tabController, ScheduleData schedule) : base(tabController)
        {
            Name = schedule.Code;
            CurrentScheduleData = schedule;
        }
    }
}
