using System.Diagnostics;
using System.Timers;
using FileConductor.Configuration.XmlData;

namespace FileConductor
{
    public class OperationProperties
    {
        // public WatcherData Data;
        //  public ServerData DestinationServer;
        public TargetTransformData DestinationTarget;
        public OperationNotificator NotificationSettings;
        public string Regex;
        //public ServerData SourceServer;
        public TargetTransformData SourceTarget;

        public void AssignOperationHandler(ElapsedEventHandler afterOperationElapsedHandler)
        {
            NotificationSettings.OnElapsed += afterOperationElapsedHandler;
        }
    }


    public class TargetTransformData
    {
        public TargetTransformData(string ipAddress, string path, string login, string password)
        {
            IpAddress = ipAddress;
            Path = path;
            Login = login;
            Password = password;
        }

        public string IpAddress { get; set; }
        public string Path { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}