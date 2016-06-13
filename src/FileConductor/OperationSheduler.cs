using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FileConductor
{
    public class OperationSheduler
    {
        private readonly Queue<Operation> _readyOperations;
        private readonly List<Operation> _operations;
        private readonly Timer _timer;

        public OperationSheduler()
        {
            _readyOperations = new Queue<Operation>();
            _operations = new List<Operation>();
            _timer = new Timer(Constants.ShedulerIntervaltime);
            _timer.Elapsed += OnElapsedTime;
            _timer.Start();
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            while (_readyOperations.Any())
            {
                var currentOperation = _readyOperations.Dequeue();
                currentOperation.Execute();
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