using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Protocols;

namespace FileConductor.Helpers
{
    public class ProtocolFactory
    {
        public static IProtocol GetProtocol(ProtocolType type)
        {
            switch (type)
            {
                case ProtocolType.Local:
                    return new LocalProtocol();
                default:
                    throw new Exception("No protocol with specified type!");
            }

        }
    }
}
