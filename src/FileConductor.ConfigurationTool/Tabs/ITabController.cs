﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.ConfigurationTool.Tabs
{
    public interface ITabController
    {
        void OpenTab(ITab tab);
    }
}