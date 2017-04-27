using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.FileTransport;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations.ProcedureExecution;
using FileConductor.ProxyFile;
using Ninject;
using NLog;

namespace FileConductor.Operations
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IProxyFileProvider _proxyFileProvider;
        [Inject]
        public  ILoggingService LoggingService { private get; set; }
        [Inject]
        public  IProcedureExecutionService ProcedureExecutionService { private get; set; }

        public OperationExecutor(IProxyFileProvider proxyFileProvider)
        {
            _proxyFileProvider = proxyFileProvider;
        }

        public void Execute(IOperation operation)
        {
            try
            {
                List<string> receivedFiles = ReceiveFiles(operation);
                if (receivedFiles.Count != 0)
                    SendFiles(operation, receivedFiles);
                if (operation.Properties.ProcedureData != null)
                    ProcedureExecutionService.ExecuteProcedure(operation);
            }
            catch (Exception e)
            {
                LoggingService.LogException(e,operation,"Exception occured during proccesing operation");
            }
        }

        private void SendFiles(IOperation operation,IList<string> receivedFiles)
        {
            try
            {
                operation.Protocol.Sender.Send(operation.Properties.DestinationTarget, receivedFiles);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured during sending files",ex);    
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
