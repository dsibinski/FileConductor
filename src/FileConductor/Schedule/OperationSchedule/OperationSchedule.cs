using System.Timers;

namespace FileConductor.Schedule.OperationSchedule
{
    public abstract class OperationSchedule
    {
        public event ElapsedEventHandler OnElapsed;

        protected void OperationScheduleElapsed(object sender, ElapsedEventArgs e)
        {
            OnElapsed.Invoke(sender, e);
        }

    }
}