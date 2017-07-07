using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTool.Tabs
{
    public interface ITabController
    {
        void OpenTab(ITab tab);
    }
}
