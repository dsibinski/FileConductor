using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;

namespace ConfigurationTool.Tabs
{
    public class TabController : INotifyPropertyChanged, ITabController
    {
        private readonly ObservableCollection<ITab> tabs = new ObservableCollection<ITab>();
        private ITab _selectedTab;

        public TabController()
        {
            tabs.CollectionChanged += Tabs_CollectionChanged;
            Tabs = tabs;
        }

        public ITab SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }

        public ICollection<ITab> Tabs { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ITab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (ITab) e.NewItems[0];
                    tab.CloseRequested += OnTabCloseRequested;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    tab = (ITab) e.OldItems[0];
                    tab.CloseRequested -= OnTabCloseRequested;
                    break;
            }
        }

        private void OnTabCloseRequested(object sender, EventArgs e)
        {
            Tabs.Remove((ITab) sender);
        }


        public void OpenTab(ITab tab)
        {
            Tabs.Add(tab);
            SelectedTab = tab;
        }


        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}