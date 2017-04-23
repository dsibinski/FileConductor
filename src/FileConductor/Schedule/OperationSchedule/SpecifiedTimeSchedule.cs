using System;
using System.Timers;

namespace FileConductor.Schedule.OperationSchedule
{
    public class SpecifiedTimeScheduleBase : OperationScheduleBase
    {
        private readonly SpecifiedTimeScheduler _sheduler;

        public SpecifiedTimeScheduleBase(int[] days, TimeSpan executionTime)
        {
            _sheduler = new SpecifiedTimeScheduler(days, executionTime, OperationScheduleElapsed);
        }
    }
}
