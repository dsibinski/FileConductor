using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Properties;
using FileConductor.Schedule;
using FileConductor.Operation;
using FileConductor.Protocols;
using NLog;

namespace FileConductor
{
    public class OperationProcessor : IOperationProcessor
    {
        private readonly ILoggingService _logger;
        private readonly IOperationExecutor _operationExecutor;
        private readonly object _locker = new object();
        private readonly List<IOperation> _operations;
        private readonly ConcurrentQueue<IOperation> _readyOperations;
        private readonly IntervalScheduler _sheduler;

        public OperationProcessor(ILoggingService logger,IOperationExecutor operationExecutor)
        {
            _operationExecutor = operationExecutor;
            _logger = logger;
            _readyOperations = new ConcurrentQueue<IOperation>();
            _operations = new List<IOperation>();
            _sheduler = new IntervalScheduler(Constants.SchedulerIntervaltime, OnElapsedTime);
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                while (_readyOperations.Any())
                {
                    IOperation currentOperation;
                    _readyOperations.TryDequeue(out currentOperation);

                    if (currentOperation != null)
                    {
                        _logger.LogInfo(currentOperation,String.Format(Resources.Executing_operation__id, currentOperation.Code));
                        _operationExecutor.Execute(currentOperation);
                        _logger.LogInfo(currentOperation,String.Format(Resources.Operation_executed_without_errors, currentOperation.Code));
                    }
                }
            }
        }

        private void AddOperationToQueue(IOperation sender, ElapsedEventArgs e)
        {
            _logger.LogInfo(sender, String.Format(Resources.Adding_operation_to_queue, sender.Code));
            _readyOperations.Enqueue(sender);
        }

        public void AssignOperation(IOperation operation)
        {
            _logger.LogInfo(operation, String.Format(Resources.Assigning_operation, operation.Code));
            _operations.Add(operation);
            operation.OnTimeElapsed += AddOperationToQueue;
        }
    }
}