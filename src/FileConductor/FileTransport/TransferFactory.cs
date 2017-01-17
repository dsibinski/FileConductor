using System.Collections.Generic;

namespace FileConductor.FileTransport
{
    public class TransferFactory
    {
        private static readonly Dictionary<string, ITransfer> TransfersImplementations =
            new Dictionary<string, ITransfer>();

        public static ITransfer GetTransfer(string type)
        {
            return TransfersImplementations[type];
        }

        public static void AddTransferImplementation(ITransfer transfer)
        {
            TransfersImplementations.Add(transfer.Name, transfer);
        }
    }
}