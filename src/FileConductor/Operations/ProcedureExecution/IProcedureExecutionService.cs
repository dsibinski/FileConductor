using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.Operations.ProcedureExecution
{
    public interface IProcedureExecutionService
    {
        void ExecuteProcedure(IOperation operation);
    }
}
