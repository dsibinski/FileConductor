﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using FileConductor.UI.Annotations;

namespace FileConductor.ConfigurationTool.Entities
{
    public class Watcher : INotifyPropertyChanged
    {

        public DatabaseData ProcedureData { get; set; }
        public ScheduleData Schedule { get; set; }
        public string Status => "OFF";
        public string Code { get; set; }
        public string Regex { get; set; }
        public string ScheduleCode { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
