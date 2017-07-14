using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Operations;
using FileConductor.Properties;
using FileConductor.Schedule;
using FileConductor.Protocols;
using Microsoft.PowerShell.Commands;
using Ninject;
using NLog;

namespace FileConductor
{
    public class OperationProcessor : IOperationProcessor
    {
        [Inject]
        public ILoggingService LoggingService { get; set; }
        [Inject]
        public IOperationExecutor OperationExecutor { get; set; }
        private readonly object _locker = new object();
        public List<IOperation> Operations { get; set; }
        public ConcurrentQueue<IOperation> OperationsToExecute { get; set; }

        public OperationProcessor()
        {
            OperationsToExecute = new ConcurrentQueue<IOperation>();
            Operations = new List<IOperation>();
        }

        public void Start(ISchedule schedule)
        {
            schedule.StartSchedule(ProcessOperation);
        }

        public void ProcessOperation()
        {
            lock (_locker)
            {
                while (OperationsToExecute.Any())
                {
                    IOperation currentOperation;
                    OperationsToExecute.TryDequeue(out currentOperation);
                    if (currentOperation != null)
                    {
                        try
                        {
                            LoggingService.LogInfo(currentOperation, String.Format(Resources.Executing_operation__id));
                            OperationExecutor.Execute(currentOperation);
                            LoggingService.LogInfo(currentOperation, String.Format(Resources.Operation_executed_without_errors));
                        }
                        catch (Exception ex)
                        {
                            LoggingService.LogException(ex,currentOperation,String.Format("Error during execution of operation"));
                        }
                    }
                }
            }
        }

        public void AddOperationToQueue(IOperation sender, ElapsedEventArgs e)
        {
            LoggingService.LogInfo(sender, String.Format(Resources.Adding_operation_to_queue));
            OperationsToExecute.Enqueue(sender);
        }

        public void AssignOperation(IOperation operation)
        {
            LoggingService.LogInfo(operation, String.Format(Resources.Assigning_operation));
            Operations.Add(operation);
            operation.OnOperationReady += AddOperationToQueue;
        }
    }
}