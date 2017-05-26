using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.Entities;
using FileConductor.ConfigurationTool.Tabs;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Protocols;
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
            LoggingService = new LogginServiceWindow();

        }

        private void TestWatcher()
        {
            if(SelectedWatcher == null) return;
            OperationExecutor executor = new OperationExecutor(new ProxyFileProvider());
            executor.LoggingService = LoggingService;
        }

        private void LoadConfiguration()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();
            Configuration = deserializer.XmlData;
            //Watchers = new List<Watcher>();
            //foreach (var watcherData in deserializer.XmlData.Watchers)
            //{
            //    Watchers.Add(new Watcher() { Code = watcherData.Code, Regex = watcherData.FileNameRegex});
            //}
        }



        //public List<Watcher> Watchers { get; set; }
        public WatcherData SelectedWatcher { get; set; }
        public CommandHandler EditCommand { get; set; }
        public CommandHandler RemoveCommand { get; set; }
        public CommandHandler TestCommand { get; set; }
        public ConfigurationData Configuration { get; set; }
        public LogginServiceWindow LoggingService { get; set; }
    }

    public class LogginServiceWindow : ILoggingService
    {
        public string Logs => LogsLines.ToString();

        public LogginServiceWindow()
        {
            LogsLines = new StringBuilder();
        }
        public StringBuilder LogsLines { get; set; }
        public void LogInfo(IOperation operation, string message)
        {
            LogsLines.AppendLine(message);
        }

        public void LogException(Exception exception, IOperation operation, string message)
        {
            LogsLines.AppendLine(message);
        }

        public void LogInfo(string message)
        {
            LogsLines.AppendLine(message);
        }

        public void LogException(Exception exception, string message)
        {
            LogsLines.AppendLine(message);
        }
    }

}