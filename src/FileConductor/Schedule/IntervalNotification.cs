using System;
using System.Linq;
using System.Timers;
using FileConductor.Schedule;

namespace FileConductor
{
    public class IntervalNotification : OperationNotificator
    {
        private readonly IntervalScheduler _sheduler;

        public IntervalNotification(int interval)
        {
            _sheduler = new IntervalScheduler(interval, OnIntervalElapsed);
        }

        private void OnIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            Execute();
        }
    }
}