using System;
using System.Linq;
using System.Timers;

namespace FileConductor
{
    public class SpecifiedTimeNotification : OperationNotificator
    {
        private readonly TimeSpan _executionTime;
        private readonly Timer _interval;
        private readonly int[] _days;
        private int _executedDay = -1;
        private int Today => (int) DateTime.Now.DayOfWeek;


        public SpecifiedTimeNotification(int[] days, TimeSpan executionTime)
        {
            _days = days;
            _executionTime = executionTime;
            CalculateRequiredTime();
            _interval = new Timer(Constants.Secund);
            _interval.Elapsed += OnIntervalElapsed;
            _interval.Start();
        }

        private void CalculateRequiredTime()
        {
            int closestDay = FindClosestDay();
            TimeSpan nextExecutionTime = new TimeSpan(closestDay,_executionTime.Hours,_executionTime.Days);
            TimeSpan currentTime = DateTime.Now.TimeOfDay;


        }

        private int FindClosestDay()
        {
            int[] tempDays = _days;

            for (int i=0;i<tempDays.Length;i++)
            {
                tempDays[i] -= Today;
                tempDays[i] = Math.Abs(tempDays[i]);
            }
            int closestDayIndex = Array.IndexOf(tempDays, tempDays.Min());
            return tempDays[closestDayIndex];
        }

        private void OnIntervalElapsed(object sender, ElapsedEventArgs e)
        {
            int today = (int)DateTime.Today.DayOfWeek;

            if (today == _executedDay) return;

            if (CheckIfShouldBeExecutedToday(today))
            {
                if (CheckIfShouldBeExecutedNow())
                {
                    Execute();
                    _executedDay = today;
                }
            }
        }

        private bool CheckIfShouldBeExecutedNow()
        {
            return DateTime.Now.TimeOfDay < _executionTime;
        }

        private bool CheckIfShouldBeExecutedToday(int today)
        {
            return _days.Where(x => x == today).Any();
        }

    }
}