using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class WatcherData
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlElement("databaseId")]
        public int DatabaseId { get; set; }

        [XmlElement("watcherRouting")]
        public WatcherRoutingData WatcherRouting { get; set; }

        [XmlElement("scheduleId")]
        public int ScheduleId { get; set; }

        [XmlElement("fileNameRegex")]
        public string FileNameRegex { get; set; }

    }
}
