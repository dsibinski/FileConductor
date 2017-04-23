using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.Helpers;
using FileConductor.Schedule.OperationSchedule;

namespace FileConductor.Schedule
{
    public static class OperationScheduleFactory
    {
        public static OperationScheduleBase GetSchedule(ScheduleData schedule)
        {
            int interval;
            Int32.TryParse(schedule.Interval, out interval);
            var days = GetDaysArray(schedule);
            DateTime time;
            DateTime.TryParseExact(schedule.Hours, "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
            if (interval != 0)
            {
                return new OperationIntervalSchedule(interval);
            }
            return new OperationSpecifiedTimeSchedule(days, new TimeSpan(0, time.Hour, time.Minute, 0));
        }

        private static int[] GetDaysArray(ScheduleData shedule)
        {
            var days = shedule.DaysOfWeek.Split(Constants.DaysSeparator)
                .ToList()
                .ConvertAll(Convert.ToInt32)
                .ToArray();
            return days;
        }
    }
}