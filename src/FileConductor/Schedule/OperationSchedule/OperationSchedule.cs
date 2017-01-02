using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NLog;

namespace FileConductor
{
    public abstract class OperationSchedule
    {
        public event ElapsedEventHandler OnElapsed;

        protected void OperationScheduleElapsed(object sender, ElapsedEventArgs e)
        {
            OnElapsed.Invoke(sender, e);
        }

    }
}