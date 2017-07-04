using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.Entities;
using FileConductor.ConfigurationTool.Tabs;
using FileConductor.FileTransport;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Protocols;
using FileConductor.ProxyFile;
using FileConductor.UI.Annotations;
using FileConductorUI.UI;
using Ninject;

namespace FileConductor.ConfigurationTool.ViewModels
{
    public class MainTabViewModel : Tab
    {
        public MainTabViewModel(ITabController controller) : base(controller) 
        {
            Name = "Watchers";
            IsClosable = Visibility.Collapsed;
            LoadConfiguration();
            TestCommand = new CommandHandler(TestWatcher);
            LoggingService = new LogginServiceWindow();
            TransportManager.Initialize();
            EditCommand = new CommandHandler(EditWatcher);

        }

        private void EditWatcher()
        {
            TabController.OpenTab(new EditWatcherTabViewModel(TabController));
        }

        private void TestWatcher()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var executor = kernel.Get<IOperationExecutor>();
            ((OperationExecutor)executor).LoggingService = LoggingService;
            if (SelectedWatcher == null) return;
            var configurationService = kernel.Get<IConfigurationService>();
            var operation = configurationService.GetOperation(Configuration, SelectedWatcher);
            executor.Execute(operation);
        }

        private void LoadConfiguration()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();
            Configuration = deserializer.XmlData;
        }

        public WatcherData SelectedWatcher { get; set; }
        public CommandHandler EditCommand { get; set; }
        public CommandHandler RemoveCommand { get; set; }
        public CommandHandler TestCommand { get; set; }
        public ConfigurationData Configuration { get; set; }
        public LogginServiceWindow LoggingService { get; set; }
    }

    public class LogginServiceWindow : ILoggingService, INotifyPropertyChanged
    {
        private string _logs;
        public string Logs
        {
            get { return _logs; }
            set
            {
                _logs = value;
                OnPropertyChanged(nameof(Logs));
            }
        }

        public void LogLine(string line)
        {
            Logs += line + Environment.NewLine;
        }

        public void LogInfo(IOperation operation, string message)
        {
            LogLine(message);
        }

        public void LogException(Exception exception, IOperation operation, string message)
        {
            StringBuilder callstack = new StringBuilder();
            callstack.AppendLine($"<Code: {operation.Code}> Exception occured!");
            callstack.AppendLine(message);
            Exception currentException = exception;
            while (currentException != null)
            {
                callstack.AppendLine(currentException.Message);
                currentException = currentException.InnerException;
            }
            LogLine(callstack.ToString());
        }

        public void LogInfo(string message)
        {
            LogLine(message);
        }

        public void LogException(Exception exception, string message)
        {

            StringBuilder callstack = new StringBuilder();
            callstack.AppendLine(message);
            Exception currentException = exception;
            while (currentException != null)
            {
                callstack.AppendLine(currentException.Message);
                currentException = currentException.InnerException;
            }
            LogLine(callstack.ToString());

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}