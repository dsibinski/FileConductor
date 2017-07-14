using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class WatcherData : IConfigurationElement
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("code")]
        public string Code { get; set; } = string.Empty;

        [XmlElement("procedureId")]
        public int ProcedureId { get; set; }

        [XmlElement("watcherRouting")]
        public WatcherRoutingData WatcherRouting { get; set; } = new WatcherRoutingData();

        [XmlElement("scheduleId")]
        public int ScheduleId { get; set; }

        [XmlElement("fileNameRegex")]
        public string FileNameRegex { get; set; }

    }
}