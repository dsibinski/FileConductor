using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using FileConductor.Properties;
using NLog;

namespace FileConductor
{
    public class OperationProcessor
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ConcurrentQueue<Operation> _readyOperations;
        private readonly List<Operation> _operations;
        private readonly Timer _timer;
        private readonly object _locker = new object();

        public OperationProcessor()
        {
            _readyOperations = new ConcurrentQueue<Operation>();
            _operations = new List<Operation>();
            _timer = new Timer(Constants.SchedulerIntervaltime);
            _timer.Elapsed += OnElapsedTime;
            _timer.Start();
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
                        _logger.Info(String.Format(Resources.Executing_operation__id, currentOperation.Id));
                        currentOperation.Execute();
                        _logger.Info(String.Format(Resources.Operation_executed_without_errors, currentOperation.Id));
                    }   
                }
            }
        }

        public void AssignOperation(Operation operation)
        {
            _logger.Info(String.Format(Resources.Assigning_operation, operation.Id));
            _operations.Add(operation);
            operation.OnTimeElapsed += AddOperationToQueue;
        }

        private void AddOperationToQueue(Operation sender, ElapsedEventArgs e)
        {
            _logger.Info(String.Format(Resources.Adding_operation_to_queue, sender.Id));
            _readyOperations.Enqueue(sender);
        }
    }
}