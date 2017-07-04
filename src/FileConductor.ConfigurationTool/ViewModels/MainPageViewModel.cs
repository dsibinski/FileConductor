using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ServiceProcess;
using System.Text;
using System.Windows.Input;
using FileConductor.ConfigurationTool.Tabs;
using FileConductorUI.UI;
using FileConductorUI.UI.Entities;
using Microsoft.Expression.Interactivity.Core;

namespace FileConductor.ConfigurationTool.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand NewTabCommand { get; }
        public TabController CurrentTabController { get; set; }


        public MainPageViewModel()
        {
            CurrentTabController = new TabController();
            NewTabCommand = new ActionCommand(x => NewTab());
            CurrentTabController.OpenTab(new MainTabViewModel(CurrentTabController));
        }
        private void NewTab()
        {
            CurrentTabController.OpenTab(new DatabaseEditTabViewModel(CurrentTabController));
        }
    }
}