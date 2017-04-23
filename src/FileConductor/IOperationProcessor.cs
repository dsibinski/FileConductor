﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.Operation;

namespace FileConductor
{
    public interface IOperationProcessor
    {
        void AssignOperation(IOperation operation);
        List<IOperation> Operations { get; set; }
        void ProcessOperation(object sender, ElapsedEventArgs e);
        void AddOperationToQueue(IOperation sender, ElapsedEventArgs e);
        ConcurrentQueue<IOperation> OperationsToExecute { get; set; }
    }
}
