using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.Entities;
using FileConductor.ConfigurationTool.Services;
using FileConductor.ConfigurationTool.Tabs;
using FileConductor.FileTransport;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.ProxyFile;
using FileConductorUI.UI;
using Ninject;

namespace FileConductor.ConfigurationTool.ViewModels
{
    public class MainTabViewModel : Tab
    {
        public IOperationExecutor OperationExecutor;

        public IConfigurationService ConfigurationService;
        public MainTabViewModel(ITabController controller) : base(controller) 
        {
            Name = "Watchers";
           // IsClosable = Visibility.Collapsed;
            TestCommand = new CommandHandler(TestWatcher);
            LoggingService = new LogginServiceWindow();
            TransportManager.Initialize();
            EditCommand = new CommandHandler(EditWatcher);
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            OperationExecutor = kernel.Get<IOperationExecutor>();
            ConfigurationService = kernel.Get<IConfigurationService>();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            Configuration = ConfigurationService.GetConfigurationData();
            Watchers = new List<Watcher>();
            foreach (var watcherData in Configuration.Watchers)
            {
                Watchers.Add(new Watcher(Configuration,watcherData));
            }
        }

        private void EditWatcher()
        {
            if (SelectedWatcher == null) return;
            var editWatcherViewModel = new EditWatcherTabViewModel(TabController,Configuration, SelectedWatcher);
            editWatcherViewModel.OnOperationModified += SaveConfig;
            TabController.OpenTab(editWatcherViewModel);
        }

        private void SaveConfig()
        {
           ConfigurationService.SaveConfigurationData(Configuration);
        }

        private void TestWatcher()
        {
            ((OperationExecutor)OperationExecutor).LoggingService = LoggingService;
            if (SelectedWatcher == null) return;
            var operation = ConfigurationService.GetOperation(Configuration, SelectedWatcher.WatcherData);
            OperationExecutor.Execute(operation);
        }


        public Watcher SelectedWatcher { get; set; }
        public CommandHandler EditCommand { get; set; }
        public CommandHandler RemoveCommand { get; set; }
        public CommandHandler TestCommand { get; set; }
        public ConfigurationData Configuration { get; set; }
        public List<Watcher> Watchers { get; set; }
        public LogginServiceWindow LoggingService { get; set; }
    }
}