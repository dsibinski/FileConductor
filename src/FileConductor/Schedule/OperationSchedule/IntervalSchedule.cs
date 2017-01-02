using System;
using System.Linq;
using System.Timers;
using FileConductor.Schedule;

namespace FileConductor
{
    public class IntervalSchedule : OperationSchedule
    {
        private readonly IntervalScheduler _sheduler;

        public IntervalSchedule(int interval)
        {
            _sheduler = new IntervalScheduler(interval, OperationScheduleElapsed);
        }
    }
}