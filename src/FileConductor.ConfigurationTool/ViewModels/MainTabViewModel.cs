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
using Microsoft.Expression.Interactivity.Core;
using Ninject;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ConfigurationTool.ViewModels
{

    
    public class MainTabViewModel : Tab
    {
        public IFileConductor FileConductor { get; set; }
        public MainTabViewModel(ITabController controller) : base(controller)
        {
            IsClosable = false;
            SimpleConfigurator.ConfigureForTargetLogging(LoggingTarget);
            Name = "Watchers";
            StartCommand = new ActionCommand(TestWatcher);
            StopCommand = new ActionCommand(StopWatcher);
            EditCommand = new ActionCommand(EditWatcher);
            AddCommand = new ActionCommand(AddWatcher);
            ClearCommand = new ActionCommand(ClearLogs);
            RemoveCommand = new ActionCommand(RemoveWatcher);
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            FileConductor = kernel.Get<IFileConductor>();
            ConfigurationService = kernel.Get<IConfigurationService>();
            LoadConfiguration();
        }

        private void StopWatcher()
        {
          FileConductor.Stop();
        }

        private void ClearLogs()
        {
           LoggingTarget.Clear();
        }

        private void AddWatcher()
        {
            var watcher = ConfigurationService.GetEmptyObject<WatcherData>(TabController.Configuration);
            watcher.Code = "New watcher";
            Watchers.Add(new Watcher(TabController.Configuration, watcher));
        }

        private void RemoveWatcher()
        {
            ConfigurationService.RemoveObject(TabController.Configuration,SelectedWatcher.WatcherData);
            Watchers.Remove(SelectedWatcher);
        }

        private void LoadConfiguration()
        {
            Watchers = new ObservableCollection<Watcher>();
            foreach (var watcherData in TabController.Configuration.Watchers)
            {
                Watchers.Add(new Watcher(TabController.Configuration, watcherData));
            }
        }

        private void EditWatcher()
        {
            if (SelectedWatcher == null) return;
            var editWatcherViewModel = new EditWatcherTabViewModel(TabController, SelectedWatcher);
            TabController.OpenTab(editWatcherViewModel);
        }


        private void TestWatcher()
        {
            FileConductor.Start(TabController.Configuration);
        }


        public Watcher SelectedWatcher { get; set; }
        public ActionCommand EditCommand { get; set; }
        public ActionCommand SaveCommand { get; set; }
        public ActionCommand AddCommand { get; set; }
        public ActionCommand RemoveCommand { get; set; }
        public ActionCommand StartCommand { get; set; }
        public ActionCommand StopCommand { get; set; }
        public ActionCommand ClearCommand { get; set; }
        public ObservableCollection<Watcher> Watchers { get; set; }
        public CustomLoggingTarget LoggingTarget { get; set; } = new CustomLoggingTarget();

    }
}