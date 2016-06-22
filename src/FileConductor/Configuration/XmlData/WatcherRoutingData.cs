using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class WatcherRoutingData
    {

        [XmlElement("sourceTargetId")]
        public int SourceTargetId { get; set; }

        [XmlElement("destinationTargetId")]
        public int DestinationTargetId { get; set; }
    }
}
