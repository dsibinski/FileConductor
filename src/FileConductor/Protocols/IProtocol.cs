using FileConductor.Operation;

namespace FileConductor.Protocols
{
    public interface IProtocol
    {
        OperationProperties Properties { get; set; }
        void ExecuteProcess();
    }
}