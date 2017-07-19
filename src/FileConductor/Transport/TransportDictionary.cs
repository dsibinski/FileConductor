using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileConductor.Attributes;
using FileConductor.FileTransport;

namespace FileConductor.Transport
{
    public class TransportDictionary : ITransportDictionary
    {
        private readonly Dictionary<string, ITransfer> TransfersImplementations =
            new Dictionary<string, ITransfer>();

        public ITransfer GetTransfer(string type)
        {
            return TransfersImplementations[type.ToUpper()];
        }

        public void AddTransferImplementation(ITransfer transfer)
        {
            TransfersImplementations.Add(transfer.Name.ToUpper(), transfer);
        }

        public TransportDictionary()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(FileTransferTypeAttribute), true).Any())
                {
                    AddTransferImplementation((ITransfer)Activator.CreateInstance(type));
                }
            }
        }
    }
}