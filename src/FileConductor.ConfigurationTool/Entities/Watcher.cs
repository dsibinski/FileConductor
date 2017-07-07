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
        public DatabaseData ProcedureData { get; set; }
        public ScheduleData Schedule { get; set; }
        public TargetData Source { get; set; }
        public WatcherData WatcherData { get; set; }
        public TargetData Destination { get; set; }

        public Watcher()
        {
            
        }

        public Watcher(ConfigurationData configuration, WatcherData watcher)
        {
            ProcedureData = configuration.Databases.FirstOrDefault(x => watcher.DatabaseId == x.Id);
            Schedule = configuration.Schedules.FirstOrDefault(x => watcher.ScheduleId == x.Id);
            Source = configuration.Targets.FirstOrDefault(x => watcher.WatcherRouting.SourceTargetId == x.Id);
            Destination = configuration.Targets.FirstOrDefault(x => watcher.WatcherRouting.DestinationTargetId == x.Id);
            WatcherData = watcher;

        }
    }
}
