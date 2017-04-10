using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using FileConductor.Helpers;
using FileConductor.LoggingService;
using FileConductor.Ninject;
using FileConductor.Properties;
using FileConductor.Schedule;
using NLog;

namespace FileConductor
{
    public class OperationProcessor : IOperationProcessor
    {
        private ILoggingService _logger;
        private readonly object _locker = new object();
        private readonly List<Operation> _operations;
        private readonly ConcurrentQueue<Operation> _readyOperations;
        private readonly IntervalScheduler _sheduler;

        public OperationProcessor()
        {
             _logger = IoC.Resolve<ILoggingService>();
            _readyOperations = new ConcurrentQueue<Operation>();
            _operations = new List<Operation>();
            _sheduler = new IntervalScheduler(Constants.SchedulerIntervaltime, OnElapsedTime);
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            lock (_locker)
            {
                while (_readyOperations.Any())
                {
                    Operation currentOperation;
                    _readyOperations.TryDequeue(out currentOperation);

                    if (currentOperation != null)
                    {
                        _logger.LogInfo(currentOperation,String.Format(Resources.Executing_operation__id, currentOperation.Code));
                        currentOperation.Execute();
                        _logger.LogInfo(currentOperation,String.Format(Resources.Operation_executed_without_errors, currentOperation.Code));
                    }
                }
            }
        }

        public void AssignOperation(Operation operation)
        {
            _logger.LogInfo(operation, String.Format(Resources.Assigning_operation, operation.Code));
            _operations.Add(operation);
            operation.OnTimeElapsed += AddOperationToQueue;
        }

        private void AddOperationToQueue(Operation sender, ElapsedEventArgs e)
        {
            _logger.LogInfo(sender, String.Format(Resources.Adding_operation_to_queue, sender.Code));
            _readyOperations.Enqueue(sender);
        }
    }
}