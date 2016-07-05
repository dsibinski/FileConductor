using System;
using System.Linq;
using System.Timers;

namespace FileConductor
{
    public class IntervalNotification : OperationNotificator
    {
        private readonly Timer _interval;

        public IntervalNotification(int interval)
        {
            _interval = new Timer(interval);
            _interval.Elapsed += OnIntervalElapsed;
            _interval.Start();
        }

        private void OnIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            Execute();
        }

    }
}