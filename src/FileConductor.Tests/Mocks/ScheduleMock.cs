using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Schedule;

namespace FileConductor.Tests.Mocks
{
   public class ScheduleMock : ISchedule
    {
       public void StartSchedule(Action action)
       {
           action();
       }
    }
}
