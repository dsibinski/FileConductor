using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileConductor.Schedule.OperationShedule
{
    public class SpecifiedTimeSchedule : OperationSchedule
    {
        private readonly SpecifiedTimeScheduler _sheduler;


        public SpecifiedTimeSchedule(int[] days, TimeSpan executionTime)
        {
            _sheduler = new SpecifiedTimeScheduler(days,  executionTime, OperationScheduleElapsed);
        }

    }
}
