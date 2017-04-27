using System;
using System.Timers;
using FileConductor.Schedule.OperationSchedule;

namespace FileConductor.Operations
{
    /// <summary>
    /// Stores information about the Operation (source/destination targets, regex to look for files etc.)
    /// </summary>
    public class OperationProperties
    {
        public TargetTransformData SourceTarget;
        public TargetTransformData DestinationTarget;
        public OperationScheduleBase Schedule;
        public ProcedureData ProcedureData;
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