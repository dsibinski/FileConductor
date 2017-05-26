using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class WatcherRoutingData
    {
        [XmlElement("sourceTarget")]
        public string SourceTarget { get; set; }

        [XmlElement("destinationTarget")]
        public string DestinationTarget { get; set; }
    }
}
