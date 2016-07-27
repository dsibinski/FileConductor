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
      /*  public string SourcePath;
        public string DestinyPath;*/
        public string Regex;
        public OperationNotificator NotificationSettings;
    }
}