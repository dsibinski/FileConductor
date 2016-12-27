using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using FileConductor.Properties;
using FileConductor.Schedule;
using NLog;

namespace FileConductor
{
    public class OperationProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly object _locker = new object();
        private readonly List<Operation> _operations;
        private readonly ConcurrentQueue<Operation> _readyOperations;
        private readonly IntervalScheduler _sheduler;

        public OperationProcessor()
        {
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
                        Logger.Info(String.Format(Resources.Executing_operation__id, currentOperation.Id));
                        currentOperation.Execute();
                        Logger.Info(String.Format(Resources.Operation_executed_without_errors, currentOperation.Id));
                    }
                }
            }
        }

        public void AssignOperation(Operation operation)
        {
            Logger.Info(String.Format(Resources.Assigning_operation, operation.Id));
            _operations.Add(operation);
            operation.OnTimeElapsed += AddOperationToQueue;
        }

        private void AddOperationToQueue(Operation sender, ElapsedEventArgs e)
        {
            Logger.Info(String.Format(Resources.Adding_operation_to_queue, sender.Id));
            _readyOperations.Enqueue(sender);
        }
    }
}