using System.Timers;

namespace FileConductor.Schedule.OperationSchedule
{
    public class OperationIntervalSchedule : OperationScheduleBase
    {
        private readonly IntervalSchedule _sheduler;

        public OperationIntervalSchedule(int interval)
        {
            _sheduler = new IntervalSchedule(interval);
            _sheduler.StartSchedule(OperationScheduleElapsed);
        }
    }
}