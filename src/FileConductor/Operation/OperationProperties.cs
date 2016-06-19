using System.Timers;

namespace FileConductor.Operation
{
    /// <summary>
    /// </summary>
    public class OperationProperties
    {
        public string SourcePath;
        public string DestinyPath;
        public string Regex;
        public Timer SchedulerTimer;  
    }
}