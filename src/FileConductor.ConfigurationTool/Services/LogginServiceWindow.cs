using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.UI.Annotations;

namespace FileConductor.ConfigurationTool.Services
{
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