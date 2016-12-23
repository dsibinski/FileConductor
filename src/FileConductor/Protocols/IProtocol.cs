using System;
using FileConductor.Configuration.XmlData;
using NLog;

namespace FileConductor.Protocols
{
    public interface IProtocol
    {
        void Execute(TargetData sourceData, TargetData destinationData, string regex);
    }
}