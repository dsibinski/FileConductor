using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.Helpers;
using FileConductor.Operation;
using FileConductor.Schedule.OperationSchedule;

namespace FileConductor.Schedule
{
    public static class ScheduleFactory
    {
        public static OperationSchedule.OperationSchedule GetSchedule(ScheduleData schedule)
        {

            int interval;
            Int32.TryParse(schedule.Interval, out interval);
            var days = GetDaysArray(schedule);
            DateTime time;
            DateTime.TryParseExact(schedule.Hours, "HHmm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out time);


            if (interval != 0)
            {
                return new IntervalSchedule(interval);
            }

            return new SpecifiedTimeSchedule(days, new TimeSpan(0, time.Hour, time.Minute, 0));
        }

        private static int[] GetDaysArray(ScheduleData shedule)
        {
            var days = shedule.DaysOfWeek.Split(Constants.DaysSeparator)
                .ToList()
                .ConvertAll(Convert.ToInt32)
                .ToArray();
            return days;
        }

        private static OperationProperties FillOperationsProperties(TargetData destinationTarget,
            ServerData destinationServer, int[] days, DateTime time,
            TargetData sourceTarget, ServerData sourceServer, WatcherData watcher)
        {
            var operationProperties = new OperationProperties()
            {
                DestinationTarget =
                    new TargetTransformData(destinationServer.Ip, destinationTarget.Path, destinationServer.User,
                        destinationServer.Password),
                NotificationSettings =
                    new SpecifiedTimeSchedule(days, new TimeSpan(0, time.Hour, time.Minute, 0)),
                SourceTarget =
                    new TargetTransformData(sourceServer.Ip, sourceTarget.Path, sourceServer.User, sourceServer.Password),
                Regex = watcher.FileNameRegex
            };
            return operationProperties;
        }
    }
}