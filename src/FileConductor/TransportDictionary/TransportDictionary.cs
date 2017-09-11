using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileConductor.Attributes;
using FileConductor.Exceptions;
using FileConductor.FileTransport;
using FileConductor.Transport;

namespace FileConductor.TransportDictionary
{
    public class TransportDictionary : ITransportDictionary
    {
        private readonly Dictionary<string, ITransfer> _transfersImplementations =
            new Dictionary<string, ITransfer>();

        public ITransfer GetTransfer(string type)
        {
            try
            {
                return _transfersImplementations[type];
            }
            catch (Exception e)
            {
                throw new InvalidTransferTypeException(e);
            }
        }

        public void AddTransferImplementation(ITransfer transfer)
        {
            _transfersImplementations.Add(transfer.Name, transfer);
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