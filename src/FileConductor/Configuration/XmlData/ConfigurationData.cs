using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    [XmlRoot("configuration")]
    public class ConfigurationData
    {
        [XmlArray("procedures")]
        [XmlArrayItem("procedure", typeof(ProcedureData))]
        public ObservableCollection<ProcedureData> Procedures { get; set; }

        [XmlArray("servers")]
        [XmlArrayItem("server", typeof(ServerData))]
        public ObservableCollection<ServerData> Servers { get; set; }

        [XmlArray("targets")]
        [XmlArrayItem("target", typeof(TargetData))]
        public ObservableCollection<TargetData> Targets { get; set; }

        [XmlArray("schedules")]
        [XmlArrayItem("schedule")]
        public ObservableCollection<ScheduleData> Schedules { get; set; }

        [XmlArray("watchers")]
        [XmlArrayItem("watcher")]
        public ObservableCollection<WatcherData> Watchers { get; set; }

    }
}
