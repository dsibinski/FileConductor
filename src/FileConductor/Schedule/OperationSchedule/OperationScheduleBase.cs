using System;
using System.Timers;
using FileConductor.Protocols;

namespace FileConductor.Schedule.OperationSchedule
{
   public abstract class OperationScheduleBase
   {
       public event Action OnElapsed;
       public void OperationScheduleElapsed()
       {
            OnElapsed?.Invoke();
       }
    }
}