using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FileConductor
{
    public class OperationScheduler
    {
        private readonly ConcurrentQueue<Operation> _readyOperations;
        private readonly List<Operation> _operations;
        private readonly Timer _timer;
        private readonly object _locker = new object();

        public OperationScheduler()
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
                    Operation currentOperation = null;
                    _readyOperations.TryDequeue(out currentOperation);//Dequeue();
                    currentOperation?.Execute();
                }
            }
        }

        public void AssignOperation(Operation operation)
        {
            _operations.Add(operation);
            operation.OnTimeElapsed += AddOperationToQueue;
        }

        private void AddOperationToQueue(Operation sender, ElapsedEventArgs e)
        {
            _readyOperations.Enqueue(sender);
        }
    }
}