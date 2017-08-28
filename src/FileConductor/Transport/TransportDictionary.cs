using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileConductor.Attributes;
using FileConductor.FileTransport;
using Ninject.Infrastructure.Language;

namespace FileConductor.Transport
{
    public class TransportDictionary : ITransportDictionary
    {
        private readonly Dictionary<string, ITransfer> _transfersImplementations =
            new Dictionary<string, ITransfer>();

        public ITransfer GetTransfer(string type)
        {
            return _transfersImplementations[type.ToUpper()];
        }

        public void AddTransferImplementation(ITransfer transfer)
        {
            _transfersImplementations.Add(transfer.Name.ToUpper(), transfer);
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

        public IEnumerable<ITransfer> Transfers
        {
            get { return _transfersImplementations.Select(x=>x.Value); } 
        }
    }
}