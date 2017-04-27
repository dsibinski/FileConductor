using System.Collections.Generic;

namespace FileConductor.FileTransport
{
    public class TransportFactory
    {
        private static readonly Dictionary<string, ITransfer> TransfersImplementations =
            new Dictionary<string, ITransfer>();

        public static ITransfer GetTransfer(string type)
        {
            return TransfersImplementations[type.ToUpper()];
        }

        public static void AddTransferImplementation(ITransfer transfer)
        {
            TransfersImplementations.Add(transfer.Name.ToUpper(), transfer);
        }
    }
}