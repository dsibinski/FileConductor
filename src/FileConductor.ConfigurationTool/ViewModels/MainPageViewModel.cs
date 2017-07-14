using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ServiceProcess;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ConfigurationTool.Tabs;
using Microsoft.Expression.Interactivity.Core;

namespace ConfigurationTool.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand NewTabCommand { get; }
        public ActionCommand CloseWindowCommand { get; }
        public TabController CurrentTabController { get; set; }


        public MainPageViewModel()
        {
            CurrentTabController = new TabController();
            CloseWindowCommand = new ActionCommand(SaveAndClose);
            NewTabCommand = new ActionCommand(x => NewTab());
            CurrentTabController.OpenTab(new MainTabViewModel(CurrentTabController));
        }

        private void SaveAndClose()
        {
            MessageBox.Show("Do You want to save?"); //TODO: Wrong behaviour, it shouldn't happend in view model
        }

        private void NewTab()
        {
            CurrentTabController.OpenTab(new DatabaseEditTabViewModel(CurrentTabController));
        }
    }
}