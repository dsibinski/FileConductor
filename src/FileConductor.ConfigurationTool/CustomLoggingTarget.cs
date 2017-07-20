using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NLog;
using NLog.Targets;

namespace ConfigurationTool
{
    [Target("CustomLoggingTarget")]
    public sealed class CustomLoggingTarget : TargetWithLayout
    {
        public ObservableCollection<string> Logs { get; set; } = new ObservableCollection<string>();

        protected override void Write(LogEventInfo logEvent)
        {
            string logMessage = logEvent.TimeStamp + " | " + logEvent.Message;
            Action<string> addMethod = Logs.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, logMessage);
        }

        public void Clear()
        {
            Logs.Clear();
        }
    }
}