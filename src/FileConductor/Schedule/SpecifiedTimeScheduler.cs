using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FileConductor.Schedule
{
    public class SpecifiedTimeScheduler 
    {
        private readonly int[] _days;
        private readonly TimeSpan _executionTime;
        private Timer _interval;
        private int _previousExecutionDay = -1;
        private ElapsedEventHandler _scheduleElapsed;


        public SpecifiedTimeScheduler(int[] days, TimeSpan executionTime, ElapsedEventHandler scheduleElapsed)
        {
            _scheduleElapsed = scheduleElapsed;
            _days = days;
            _executionTime = executionTime;
            CalculateNextRequiredTime();
            _interval.Elapsed += OnIntervalElapsed;
        }

        private int Today => (int) DateTime.Now.DayOfWeek;

        private void CalculateNextRequiredTime()
        {
            var closestDay = FindClosestDay();
            var nextExecutionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, closestDay, _executionTime.Hours,
                _executionTime.Minutes, _executionTime.Seconds);
            var timeDiffrence = nextExecutionTime.Subtract(DateTime.Now);
            if(timeDiffrence.TotalMilliseconds < 0)
            {
                _previousExecutionDay = 0;
                CalculateNextRequiredTime();
            }
            else
            {
                _interval = new Timer(timeDiffrence.TotalMilliseconds);
                _interval.Start();
            }   
        }

        private int FindClosestDay()
        {
            List<int> daysToClosestDay = new List<int>(_days);

            for (var i = 0; i < daysToClosestDay.Count; i++)
            {
                daysToClosestDay[i] -= Today;
                daysToClosestDay[i] = daysToClosestDay[i];
            }
            daysToClosestDay.ToList().Sort();

            int closestDay = daysToClosestDay.First(x=>x != _previousExecutionDay && x >= 0);
         
            return DateTime.Now.Day + closestDay;
        }

        private void OnIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            _interval.Stop();
            _previousExecutionDay = 0;
            _scheduleElapsed(sender,e);
            CalculateNextRequiredTime();
        }

      }
}