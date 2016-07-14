

using System;
using NLog;

namespace FileConductor.Protocols
{
    public abstract class Protocol
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();

        public OperationProperties Properties { get; set; }

        public void ExecuteProcess()
        {
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }   
        }

        public abstract void Execute();
    }
}