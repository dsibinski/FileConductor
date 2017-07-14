using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using ConfigurationTool.Annotations;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using ConfigurationTool.Entities;
using ConfigurationTool.Tabs;
using FileConductor;
using FileConductor.FileTransport;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.ProxyFile;
using Ninject;

namespace ConfigurationTool.ViewModels
{
    public class MainTabViewModel : Tab
    {
        public IFileConductor FileConductor { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
        public MainTabViewModel(ITabController controller) : base(controller) 
        {
            Name = "Watchers";
            TestCommand = new CommandHandler(TestWatcher);
            EditCommand = new CommandHandler(EditWatcher);
            AddCommand = new CommandHandler(AddWatcher);
            RemoveCommand = new CommandHandler(RemoveWatcher);
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            FileConductor = kernel.Get<IFileConductor>();
            ConfigurationService = kernel.Get<IConfigurationService>();
            LoadConfiguration();
        }

        private void AddWatcher()
        {
            var watcher = ConfigurationService.GetEmptyObject<WatcherData>(Configuration);
            watcher.Code = "New watcher";
            Watchers.Add(new Watcher(Configuration, watcher));
        }

        private void RemoveWatcher()
        {
            Watchers.Remove(SelectedWatcher);
        }

        private void LoadConfiguration()
        {
            Configuration = ConfigurationService.GetConfigurationData();
            Watchers = new ObservableCollection<Watcher>();
            foreach (var watcherData in Configuration.Watchers)
            {
                Watchers.Add(new Watcher(Configuration, watcherData));
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
            FileConductor.Start(Configuration);
        }


        public Watcher SelectedWatcher { get; set; }
        public CommandHandler EditCommand { get; set; }
        public CommandHandler AddCommand { get; set; }
        public CommandHandler RemoveCommand { get; set; }
        public CommandHandler TestCommand { get; set; }
        public ConfigurationData Configuration { get; set; }
        public ObservableCollection<Watcher> Watchers { get; set; }
        public ILoggingService LoggingService { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}