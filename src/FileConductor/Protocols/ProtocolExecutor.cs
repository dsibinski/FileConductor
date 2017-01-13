using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using NLog;

namespace FileConductor.Protocols
{
    public class ProtocolExecutor
    {

        protected static Logger logger = LogManager.GetCurrentClassLogger();

        private OperationProperties _properties { get; set; }
        private readonly Protocol _protocol;


        public ProtocolExecutor(Protocol protocol ,OperationProperties properties, ElapsedEventHandler afterOperationElapsedHandler)
        {
            _protocol = protocol;
            _properties = properties;
            _properties.AssignOperationHandler(afterOperationElapsedHandler);
        }

        public void ExecuteProtocol()
        {
            List<string> receivedFiles = ReceiveFiles();
            SendFiles(receivedFiles);
        }

        private void SendFiles(List<string> receivedFiles)
        {
            try
            {
                _protocol.Sender.Send(_properties.DestinationTarget, receivedFiles, _properties.Regex);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured durind sending files", ex);
            }
        }

        private List<string> ReceiveFiles()
        {
            List<string> receivedFiles = null;
            try
            {
                receivedFiles = _protocol.Receiver.Receive(_properties.SourceTarget, ProxyFile.ProxyPath, _properties.Regex);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured durind receiving files", ex);
            }
            return receivedFiles;
        }
    }
}
