using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    [XmlRoot("configuration")]
    public class ConfigurationData
    {
        [XmlArray("procedures")]
        [XmlArrayItem("procedure", typeof(ProcedureData))]
        public List<ProcedureData> Procedures { get; set; }

        [XmlArray("servers")]
        [XmlArrayItem("server", typeof(ServerData))]
        public List<ServerData> Servers { get; set; }

        [XmlArray("targets")]
        [XmlArrayItem("target", typeof(TargetData))]
        public List<TargetData> Targets { get; set; }

        [XmlArray("schedules")]
        [XmlArrayItem("schedule")]
        public List<ScheduleData> Schedules { get; set; }

        [XmlArray("watchers")]
        [XmlArrayItem("watcher")]
        public List<WatcherData> Watchers { get; set; }

    }
}
