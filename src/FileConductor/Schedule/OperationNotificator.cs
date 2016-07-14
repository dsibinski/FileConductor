using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NLog;

namespace FileConductor
{
   public abstract class OperationNotificator
   {
       public event ElapsedEventHandler OnElapsed;

       protected static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Execute()
       {
            OnElapsed.Invoke(this,null);
       }
   }
}
