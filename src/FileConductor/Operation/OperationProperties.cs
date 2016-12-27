using System.Diagnostics;
using System.Timers;
using FileConductor.Configuration.XmlData;

namespace FileConductor
{
    public class OperationProperties
    {
        public WatcherData Data;
        public ServerData DestinationServer;
        public TargetData DestinationTarget;
        public OperationNotificator NotificationSettings;
        public string Regex;
        public ServerData SourceServer;
        public TargetData SourceTarget;

        public void AssignOperationHandler(ElapsedEventHandler afterOperationElapsedHandler)
        {
            NotificationSettings.OnElapsed += afterOperationElapsedHandler;
        }
    }


    //public class TargetTransformData
    //{
    //    public string IpAddress { get; set; }
    //    public string Path { get; set; }
    //    public string Regex { get; set; }
    //}
}