using System;
using System.Collections.Generic;
using System.IO;
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
        [Inject]
        public IProxyFileProvider ProxyFileProvider { private get; set; }
        [Inject]
        public  ILoggingService LoggingService { private get; set; }
        [Inject]
        public  IProcedureExecutionService ProcedureExecutionService { private get; set; }

        public void Execute(IOperation operation)
        {
            try
            {
                LoggingService.LogInfo(operation,"Execution started");
                List<string> receivedFiles = ReceiveFiles(operation);
                if (!receivedFiles.Any()) { LoggingService.LogInfo("No files detected");return; }
                LoggingService.LogInfo(operation,$"Found {receivedFiles.Count} files to send:");
                foreach (var file in receivedFiles)
                {
                    LoggingService.LogInfo(Path.GetFileName(file));
                }
                LoggingService.LogInfo("Sending files...");
                if (receivedFiles.Count != 0)
                    SendFiles(operation, receivedFiles);
                LoggingService.LogInfo("Sending succesfull!");
                if (operation.Properties.ProcedureData != null)
                {
                    LoggingService.LogInfo("SQL procedure execution started!");
                    ProcedureExecutionService.ExecuteProcedure(operation);
                }
                LoggingService.LogInfo(operation, "Execution finished");
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
                receivedFiles = operation.Protocol.Receiver.Receive(operation.Properties.SourceTarget, ProxyFileProvider.ProxyPath, operation.Properties.Regex);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured during receiving files", ex);
            }
            return receivedFiles;
        }

    }
}
