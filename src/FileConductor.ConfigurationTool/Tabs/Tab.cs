using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileConductor.Configuration;
using Microsoft.Expression.Interactivity.Core;

namespace ConfigurationTool.Tabs
{
    public abstract class Tab : ITab
    {
        private bool _isClosable;

        protected Tab(ITabController tabController)
        {
            TabController = tabController;
            IsClosable = true;
            CloseCommand = new ActionCommand(x => CloseRequested?.Invoke(this, EventArgs.Empty));
            SaveCommand = new ActionCommand(SaveConfig);
        }

        private void SaveConfig()
        {
            ConfigurationService.SaveConfigurationData(TabController.Configuration);
        }

        public string Name { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public event EventHandler CloseRequested;

        public bool IsClosable
        {
            get { return _isClosable; }
            set
            {
                if (!value)
                {
                    CloseCommand = null;
                }
                _isClosable = value;
            }
        }

        public ITabController TabController { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
    }
}
