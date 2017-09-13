using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ConfigurationTool.Services;
using ConfigurationTool.Tabs;
using FileConductor.Configuration;
using Microsoft.Expression.Interactivity.Core;
using Ninject;

namespace ConfigurationTool.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            ConfigurationService = kernel.Get<IConfigurationService>();
            NotificationService = kernel.Get<INotificationService>();
            SaveConfigurationCommand = new ActionCommand(SaveConfiguration);
            var configuration = ConfigurationService.GetConfigurationData();
            CurrentTabController = new TabController(ConfigurationService, configuration);
            CloseWindowCommand = new ActionCommand(SaveAndClose);
            NewTabCommand = new ActionCommand(x => NewTab());
            CurrentTabController.OpenTab(new MainTabViewModel(CurrentTabController));
        }

        public ICommand NewTabCommand { get; }
        public ActionCommand CloseWindowCommand { get; }
        public TabController CurrentTabController { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
        public INotificationService NotificationService { get; set; }
        public ActionCommand SaveConfigurationCommand { get; set; }

        private void SaveConfiguration()
        {
            ConfigurationService.SaveConfigurationData(CurrentTabController.Configuration);
        }

        private void SaveAndClose()
        {
            var result = NotificationService.ShowQuestion("Exit", "Do You want to save before exit?");
            if (result == MessageBoxResult.Yes)
                SaveConfiguration();
        }

        private void NewTab()
        {
            CurrentTabController.OpenTab(new MainTabViewModel(CurrentTabController));
        }
    }
}