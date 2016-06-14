namespace FileConductor
{
    public interface IProtocol
    {
        OperationProperties Properties { get; set; }
        void ExecuteProcess();
    }
}