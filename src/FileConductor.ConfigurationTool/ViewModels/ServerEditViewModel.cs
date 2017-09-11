using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConfigurationTool.Annotations;
using ConfigurationTool.Tabs;
using FileConductor.Configuration.XmlData;
using FileConductor.FileTransport;
using FileConductor.Transport;
using FileConductor.TransportDictionary;

namespace ConfigurationTool.ViewModels
{
    public class ServerEditViewModel : Tab, INotifyPropertyChanged
    {
        public ITransfer[] Transfers { get; set; }
        public ITransfer SelectedTransfer { get; set; }
        public ServerData CurrentServerData { get; set; }
        public ServerEditViewModel(ITabController tabController, ServerData data) : base(tabController)
        {
            Name = data.Code;
            CurrentServerData = data;
            TransportDictionary dic = new TransportDictionary();
            Transfers = dic.Transfers.ToArray();
            SelectedTransfer = Transfers.FirstOrDefault(x => x.Name == data.Protocol);
            OnPropertyChanged(nameof(Transfers));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
