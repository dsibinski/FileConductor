using System;
using System.Timers;

namespace FileConductor.Schedule.OperationSchedule
{
    public class OperationSpecifiedTimeSchedule : OperationScheduleBase
    {
        private readonly SpecifiedTimeSchedule _sheduler;
        public OperationSpecifiedTimeSchedule(int[] days, TimeSpan executionTime)
        {
            _sheduler = new SpecifiedTimeSchedule(days, executionTime);
            _sheduler.StartSchedule(OperationScheduleElapsed);
        }
    }
}
