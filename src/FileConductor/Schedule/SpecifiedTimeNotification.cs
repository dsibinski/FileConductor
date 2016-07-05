using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FileConductor
{
    public class SpecifiedTimeNotification : OperationNotificator
    {
        private readonly int[] _days;
        private readonly TimeSpan _executionTime;
        private Timer _interval;
        private int _previousExecutionDay = -1;


        public SpecifiedTimeNotification(int[] days, TimeSpan executionTime)
        {
            _days = days;
            _executionTime = executionTime;
            CalculateNextRequiredTime();
            _interval.Elapsed += OnIntervalElapsed;
            _interval.Start();
        }

        private int Today => (int) DateTime.Now.DayOfWeek;

        private void CalculateNextRequiredTime()
        {
            var closestDay = FindClosestDay();
            var nextExecutionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, closestDay, _executionTime.Hours,
                _executionTime.Minutes, _executionTime.Seconds);

            var timeDiffrence = nextExecutionTime.Subtract(DateTime.Now);
            _interval = new Timer(timeDiffrence.TotalMilliseconds);
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
            _previousExecutionDay = Today;
            Execute();
            CalculateNextRequiredTime();
        }

      }
}