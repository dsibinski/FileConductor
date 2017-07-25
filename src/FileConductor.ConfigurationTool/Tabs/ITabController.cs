using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;

namespace ConfigurationTool.Tabs
{
    public interface ITabController
    {
        void OpenTab(ITab tab);
        ConfigurationData Configuration { get; set; }
    }
}
