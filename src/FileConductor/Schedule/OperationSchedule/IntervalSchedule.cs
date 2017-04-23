using System.Timers;

namespace FileConductor.Schedule.OperationSchedule
{
    public class IntervalSchedule : OperationScheduleBase
    {
        private readonly IntervalScheduler _sheduler;

        public IntervalSchedule(int interval)
        {
            _sheduler = new IntervalScheduler(interval, OperationScheduleElapsed);
        }
    }
}