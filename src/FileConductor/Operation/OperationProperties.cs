using System.Timers;
using FileConductor.Schedule.OperationSchedule;

namespace FileConductor.Operation
{
    /// <summary>
    /// Stores information about the Operation (source/destination targets, regex to look for files etc.)
    /// </summary>
    public class OperationProperties
    {
        public TargetTransformData SourceTarget;
        public TargetTransformData DestinationTarget;
        public OperationScheduleBase Schedule;
        public string Regex;
        public void AssignOperationHandler(ElapsedEventHandler afterOperationElapsedHandler)
        {
            if (Schedule != null)
                Schedule.OnElapsed += afterOperationElapsedHandler;
        }
    }
    
}