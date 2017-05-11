using System;
using System.Collections.Generic;
using System.Windows;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.Entities;
using FileConductor.ConfigurationTool.Tabs;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.ProxyFile;
using FileConductorUI.UI;

namespace FileConductor.ConfigurationTool.ViewModels
{
    public class MainTabViewModel : Tab
    {
        public MainTabViewModel()
        {
            Name = "Watchers";
            IsClosable = Visibility.Collapsed;
            LoadConfiguration();
            TestCommand = new CommandHandler(TestWatcher);

        }

        private void TestWatcher()
        {
            if(SelectedWatcher == null) return;
            OperationExecutor executor = new OperationExecutor(new ProxyFileProvider());
           // executor.Execute(new Operation());
        }

        private void LoadConfiguration()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();

            Watchers = new List<Watcher>();
            foreach (var watcherData in deserializer.XmlData.Watchers)
            {
                Watchers.Add(new Watcher() {Code = watcherData.Code});
            }
        }



        public List<Watcher> Watchers { get; set; }
        public Watcher SelectedWatcher { get; set; }
        public CommandHandler EditCommand { get; set; }
        public CommandHandler RemoveCommand { get; set; }
        public CommandHandler TestCommand { get; set; }
        public ConfigurationData Configuration { get; set; }
    }

    public class LogginServiceWindow : ILoggingService
    {
        public LogginServiceWindow()
        {
            Logs = new List<string>();
        }
        public List<string> Logs { get; set; }
        public void LogInfo(IOperation operation, string message)
        {
            Logs.Add(message);
        }

        public void LogException(Exception exception, IOperation operation, string message)
        {
            Logs.Add(message);
        }

        public void LogInfo(string message)
        {
            Logs.Add(message);
        }

        public void LogException(Exception exception, string message)
        {
            Logs.Add(message);
        }
    }

}