using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ConfigurationTool.Tabs;
using FileConductor;
using FileConductor.Configuration;
using Microsoft.Expression.Interactivity.Core;
using Ninject;

namespace ConfigurationTool.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand NewTabCommand { get; }
        public ActionCommand CloseWindowCommand { get; }
        public TabController CurrentTabController { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
        public ActionCommand SaveConfigurationCommand { get; set; }


        public MainPageViewModel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            ConfigurationService = kernel.Get<IConfigurationService>();
            SaveConfigurationCommand = new ActionCommand(SaveConfiguration);
            var configuration = ConfigurationService.GetConfigurationData();
            CurrentTabController = new TabController(ConfigurationService, configuration);
            CloseWindowCommand = new ActionCommand(SaveAndClose);
            NewTabCommand = new ActionCommand(x => NewTab());
            CurrentTabController.OpenTab(new MainTabViewModel(CurrentTabController));
        }

        private void SaveConfiguration()
        {
            ConfigurationService.SaveConfigurationData(CurrentTabController.Configuration);
        }

        private void SaveAndClose()
        {
           /* MessageBox.Show("Do You want to save?");*/ //TODO: Wrong behaviour, it shouldn't happend in view model
        }

        private void NewTab()
        {
            CurrentTabController.OpenTab(new MainTabViewModel(CurrentTabController));
        }

    }
}