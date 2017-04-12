using System;

namespace FileConductor.Schedule.OperationSchedule
{
    public class SpecifiedTimeSchedule : OperationSchedule
    {
        private readonly SpecifiedTimeScheduler _sheduler;

        public SpecifiedTimeSchedule(int[] days, TimeSpan executionTime)
        {
            _sheduler = new SpecifiedTimeScheduler(days,  executionTime, OperationScheduleElapsed);
        }

    }
}
