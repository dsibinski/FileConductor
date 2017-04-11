namespace FileConductor.Schedule.OperationSchedule
{
    public class IntervalSchedule : global::FileConductor.Schedule.OperationSchedule.OperationSchedule
    {
        private readonly IntervalScheduler _sheduler;

        public IntervalSchedule(int interval)
        {
            _sheduler = new IntervalScheduler(interval, OperationScheduleElapsed);
        }
    }
}