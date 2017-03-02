using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    [XmlRoot("configuration")]
    public class ConfigurationData
    {
        [XmlArray("databases")]
        [XmlArrayItem("database", typeof(DatabaseData))]
        public DatabaseData[] Databases { get; set; }


        [XmlArray("servers")]
        [XmlArrayItem("server", typeof(ServerData))]
        public ServerData[] Servers { get; set; }

        [XmlArray("targets")]
        [XmlArrayItem("target", typeof(TargetData))]
        public TargetData[] Targets { get; set; }

        [XmlArray("schedules")]
        [XmlArrayItem("schedule")]
        public ScheduleData[] Schedules { get; set; }

        [XmlArray("watchers")]
        [XmlArrayItem("watcher")]
        public WatcherData[] Watchers { get; set; }

    }
}
