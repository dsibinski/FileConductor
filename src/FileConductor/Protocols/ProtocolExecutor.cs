using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.Operation;
using NLog;

namespace FileConductor.Protocols
{
    public class ProtocolExecutor
    {
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
            //todo: add logging here
            List<string> receivedFiles = ReceiveFiles();
            if(receivedFiles.Count != 0)
            SendFiles(receivedFiles);
        }

        private void SendFiles(IList<string> receivedFiles)
        {
            try
            {
                _protocol.Sender.Send(_properties.DestinationTarget, receivedFiles);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured during sending files", ex);
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
                throw new Exception("Exception occured during receiving files", ex);
            }
            return receivedFiles;
        }
    }
}
