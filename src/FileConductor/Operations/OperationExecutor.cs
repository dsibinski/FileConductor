using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using NLog;

namespace FileConductor.Operations
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IProxyFileProvider _proxyFileProvider;
        private readonly ILoggingService _loggingService;

        public OperationExecutor(IProxyFileProvider proxyFileProvider, ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _proxyFileProvider = proxyFileProvider;
        }

        public void Execute(IOperation operation)
        {
            //todo: add logging here
            List<string> receivedFiles = ReceiveFiles(operation);
            if(receivedFiles.Count != 0)
            SendFiles(operation,receivedFiles);
        }

        private void SendFiles(IOperation operation,IList<string> receivedFiles)
        {
            try
            {
                operation.Protocol.Sender.Send(operation.Properties.DestinationTarget, receivedFiles);
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex,operation, "Exception occured during sending files");
            }
        }

        private List<string> ReceiveFiles(IOperation operation)
        {
            List<string> receivedFiles = null;
            try
            {
                receivedFiles = operation.Protocol.Receiver.Receive(operation.Properties.SourceTarget, _proxyFileProvider.ProxyPath, operation.Properties.Regex);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured during receiving files", ex);
            }
            return receivedFiles;
        }

    }
}
