using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NLog;

namespace FileConductor.Protocols
{
    public class ProtocolExecutor
    {

        protected static Logger logger = LogManager.GetCurrentClassLogger();

        private OperationProperties _properties { get; set; }

        private readonly IProtocol _protocol;


        public ProtocolExecutor(IProtocol protocol,OperationProperties properties, ElapsedEventHandler afterOperationElapsedHandler)
        {
            _protocol = protocol;
            _properties = properties;
            _properties.AssignOperationHandler(afterOperationElapsedHandler);
        }

        public void ExecuteProcess()
        {
            try
            {
                _protocol.Execute(_properties.SourceTarget, _properties.DestinationTarget, _properties.Regex);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

    }
}
