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

namespace ConfigurationTool.ViewModels
{
    public class ServerEditViewModel : Tab, INotifyPropertyChanged
    {
        public ITransfer[] Transfers { get; set; }
        public ServerData CurrentServerData { get; set; }
        public ServerEditViewModel(ITabController tabController, ServerData data) : base(tabController)
        {
            Name = data.Code;
            CurrentServerData = data;
            TransportDictionary dic = new TransportDictionary();
            Transfers = dic.Transfers.ToArray();
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
