using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor
{
    public interface IOperationProcessor
    {
        void AssignOperation(Operation operation);
    }
}
