using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class WatcherData
    {
        [XmlElement("code")]
        public string Code { get; set; }
        
        [XmlElement("database")]
        public string Database { get; set; }

        [XmlElement("watcherRouting")]
        public WatcherRoutingData WatcherRouting { get; set; }

        [XmlElement("schedule")]
        public string Schedule { get; set; }

        [XmlElement("fileNameRegex")]
        public string FileNameRegex { get; set; }

    }
}
