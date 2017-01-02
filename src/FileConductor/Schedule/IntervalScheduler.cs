using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf.Builders;

namespace FileConductor.Schedule
{
    public class IntervalScheduler
    {
        private readonly Timer _interval;

        /// <param name="sheduldingTime">shedulding time in miliseconds</param>
        /// <param name="scheduleElapsed"> handler for action after each interval</param>
        /// <param name="startImmediately"></param>
        public IntervalScheduler(int interval, ElapsedEventHandler scheduleElapsed,
            bool startImmediately = true)
        {
            _interval = new Timer(interval);
            _interval.Elapsed += scheduleElapsed;
            if (startImmediately)
                _interval.Start();
        }

        public void Start()
        {
            _interval.Start();
        }

        public void Stop()
        {
            _interval.Stop();
        }
    }
}