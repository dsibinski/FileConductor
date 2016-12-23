using System.Diagnostics;
using System.Timers;
using FileConductor.Configuration.XmlData;

namespace FileConductor
{
    public class OperationProperties
    {
        public WatcherData Data;
        public ServerData SourceServer;
        public ServerData DestinationServer;
        public TargetData SourceTarget;
        public TargetData DestinationTarget;
        public string Regex;
        public OperationNotificator NotificationSettings;

        public void AssignOperationHandler(ElapsedEventHandler afterOperationElapsedHandler)
        {
            NotificationSettings.OnElapsed += afterOperationElapsedHandler;
        }

    }
}