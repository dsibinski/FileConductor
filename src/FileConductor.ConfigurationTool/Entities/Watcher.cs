using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConfigurationTool.Annotations;
using FileConductor.Configuration.XmlData;

namespace ConfigurationTool.Entities
{
    public class Watcher : INotifyPropertyChanged
    {
        private TargetData _source;
        private TargetData _destination;
        private ProcedureData _procedureData;

        public Watcher(ConfigurationData configuration)
        {
            Configuration = configuration;
        }

        public Watcher(ConfigurationData configuration, WatcherData watcher)
        {
            Configuration = configuration;
            ProcedureData = configuration.Procedures.FirstOrDefault(x => watcher.ProcedureId == x.Id);
            Schedule = configuration.Schedules.FirstOrDefault(x => watcher.ScheduleId == x.Id);
            Source = configuration.Targets.FirstOrDefault(x => watcher.WatcherRouting.SourceTargetId == x.Id);
            Destination = configuration.Targets.FirstOrDefault(x => watcher.WatcherRouting.DestinationTargetId == x.Id);
            if (Source != null) SourceServerData = configuration.Servers.FirstOrDefault(x => x.Id == Source.ServerId);
            if (Destination != null)
                DestinationServerData = configuration.Servers.FirstOrDefault(x => x.Id == Destination.ServerId);
            WatcherData = watcher;
        }

        public ProcedureData ProcedureData
        {
            get { return _procedureData; }
            set
            {
                _procedureData = value;
                OnPropertyChanged(nameof(ProcedureData));
            }
        }

        public ScheduleData Schedule { get; set; }
        public WatcherData WatcherData { get; set; }

        public TargetData Source
        {
            get { return _source; }
            set
            {
                SourceServerData = Configuration.Servers.FirstOrDefault(x => x.Id == value.ServerId);
                OnPropertyChanged(nameof(SourceServerData));
                _source = value;
                
            }
        }

        public ServerData SourceServerData { get; set; }
        public TargetData Destination
        {
            get { return _destination; }
            set
            {
                DestinationServerData = Configuration.Servers.FirstOrDefault(x => x.Id == value.ServerId);
                OnPropertyChanged(nameof(DestinationServerData));
                _destination = value;
            }
        }

        public ServerData DestinationServerData { get; set; }
        public ConfigurationData Configuration { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}