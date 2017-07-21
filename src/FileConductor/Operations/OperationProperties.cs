using System;
using System.Timers;
using FileConductor.Configuration.XmlData;
using FileConductor.Schedule.OperationSchedule;

namespace FileConductor.Operations
{
    /// <summary>
    /// Stores information about the Operation (source/destination targets, regex to look for files etc.)
    /// </summary>
    public class OperationProperties
    {
        public TargetTransformData SourceTarget { get; set; }
        public TargetTransformData DestinationTarget { get; set; }
        public OperationScheduleBase Schedule { get; set; }
        public ProcedureData ProcedureData { get; set; }
        public string Regex;

        public OperationProperties(OperationScheduleBase schedule)
        {
            Schedule = schedule;
        }
        public void AssignOperationHandler(Action afterOperationElapsedHandler)
        {
            if (Schedule != null)
                Schedule.OnElapsed += afterOperationElapsedHandler;
        }
    }
    
}