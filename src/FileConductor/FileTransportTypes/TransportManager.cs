using System;
using System.Linq;
using System.Reflection;
using FileConductor.Attributes;

namespace FileConductor.FileTransport
{
    public class TransportManager
    {
        public static void Initialize()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof (FileTransferTypeAttribute), true).Any())
                {
                    TransportFactory.AddTransferImplementation((ITransfer) Activator.CreateInstance(type));
                }
            }
        }
    }
}