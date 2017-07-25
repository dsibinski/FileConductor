﻿using System;
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
        private ServerData _serverData;

        public TargetEditViewModel(ITabController tabController, TargetData data) : base(tabController)
        {
            Name = data.Code;
            TargetData = data;
            ServerData = tabController.Configuration.Servers.FirstOrDefault(x => x.Id == TargetData.ServerId);
        }

        public TargetData TargetData { get; set; }

        public ServerData ServerData

        {
            get { return _serverData; }
            set
            {
                TargetData.ServerId = value.Id;
                _serverData = value;
            }
        }
    }
}