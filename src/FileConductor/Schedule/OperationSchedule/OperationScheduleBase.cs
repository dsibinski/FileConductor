using System.Timers;
using FileConductor.Operation;
using FileConductor.Protocols;

namespace FileConductor.Schedule.OperationSchedule
{
   public abstract  class OperationScheduleBase
   {
       public event ElapsedEventHandler OnElapsed ;
       public void OperationScheduleElapsed(object sender, ElapsedEventArgs e)
       {
            OnElapsed?.Invoke(sender, e);
       }
    }
}