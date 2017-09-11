using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationTool.Entities;
using ConfigurationTool.Tabs;
using FileConductor.Configuration.XmlData;
using Microsoft.Expression.Interactivity.Core;

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
            EditServerCommand = new ActionCommand(EditServer);
            AddServerCommand = new ActionCommand(AddServer);
            RemoveServerCommand =  new ActionCommand(RemoveServer);
        }

        private void RemoveServer()
        {
            TabController.Configuration.Servers.Remove(ServerData);
        }

        private void AddServer()
        {
            var server = TabController.ConfigurationService.GetEmptyObject<ServerData>(TabController.Configuration);
            server.Code = "New server";
            var sEditVm = new ServerEditViewModel(TabController, server);
            ServerData = server;
            TabController.OpenTab(sEditVm);
        }

        public ActionCommand AddServerCommand { get; set; }
        public ActionCommand RemoveServerCommand { get; set; }
        public ActionCommand EditServerCommand { get; set; }

        public TargetData TargetData { get; set; }


        private void EditServer()
        {
            if (ServerData == null) return;
            var schEditVm = new ServerEditViewModel(TabController, ServerData);
            TabController.OpenTab(schEditVm);
        }

        public ServerData ServerData

        {
            get { return _serverData; }
            set
            {
                if (value != null)
                {
                    TargetData.ServerId = value.Id;
                }
                else
                {
                    TargetData.ServerId = null;
                }
                _serverData = value;
            }
        }
    }
}