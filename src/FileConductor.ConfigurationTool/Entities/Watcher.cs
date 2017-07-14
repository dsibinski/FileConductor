using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;

namespace ConfigurationTool.Entities
{
    public class Watcher
    {
        public ProcedureData ProcedureData { get; set; }
        public ScheduleData Schedule { get; set; }
        public WatcherData WatcherData { get; set; }
        public TargetData Source { get; set; }
        public ServerData SourceServerData { get; set; }
        public TargetData Destination { get; set; }
        public ServerData DestinationServerData { get; set; }

        public Watcher() { } 

        public Watcher(ConfigurationData configuration, WatcherData watcher)
        {
            ProcedureData = configuration.Procedures.FirstOrDefault(x => watcher.ProcedureId == x.Id);
            Schedule = configuration.Schedules.FirstOrDefault(x => watcher.ScheduleId == x.Id);
            Source = configuration.Targets.FirstOrDefault(x => watcher.WatcherRouting.SourceTargetId == x.Id);
            Destination = configuration.Targets.FirstOrDefault(x => watcher.WatcherRouting.DestinationTargetId == x.Id);
            if(Source != null)SourceServerData = configuration.Servers.FirstOrDefault(x => x.Id == Source.ServerId);
            if(Destination !=null)DestinationServerData = configuration.Servers.FirstOrDefault(x => x.Id == Destination.ServerId);
            WatcherData = watcher;

        }
    }
}
