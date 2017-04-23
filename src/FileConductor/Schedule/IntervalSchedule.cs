using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileConductor.Schedule
{
    public class IntervalSchedule : ISchedule
    {
        private readonly Timer _interval;

        /// <param name="interval">Schedulding time in miliseconds</param>
        public IntervalSchedule(int interval)
        {
            _interval = new Timer(interval);
        }

        public void Start()
        {
            _interval.Start();
        }


        public void Stop()
        {
            _interval.Stop();
        }

        /// <param name="action">Handler for action after each interval</param>
        public void StartSchedule(Action action)
        {
            _interval.Elapsed += (s, e) =>
            {
                action();
            };
            _interval.Start();
        }
    }
}