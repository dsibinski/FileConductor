using System.Timers;

namespace FileConductor.Operations
{
    /// <summary>
    /// Stores information about the Operation (source/destination targets, regex to look for files etc.)
    /// </summary>
    public class OperationProperties
    {
        // public WatcherData Data;
        //  public ServerData DestinationServer;
        public TargetTransformData DestinationTarget;
        public OperationSchedule NotificationSettings;
        public string Regex;
        //public ServerData SourceServer;
        public TargetTransformData SourceTarget;

        public void AssignOperationHandler(ElapsedEventHandler afterOperationElapsedHandler)
        {
            NotificationSettings.OnElapsed += afterOperationElapsedHandler;
        }
    }
    
}