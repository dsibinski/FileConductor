using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ServiceProcess;
using System.Text;
using System.Windows.Input;
using FileConductor.ConfigurationTool.Tabs;
using FileConductorUI.UI;
using FileConductorUI.UI.Entities;
using Microsoft.Expression.Interactivity.Core;

namespace FileConductor.ConfigurationTool.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public const string ServiceName = "FileConductor";
        private readonly ObservableCollection<ITab> tabs;
        private ITab _selectedTab;
        public CommandHandler ButtonClick { get; set; }

        public ITab SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }

        public MainPageViewModel()
        {
            NewTabCommand = new ActionCommand(x => NewTab());
            tabs = new ObservableCollection<ITab>();
            tabs.CollectionChanged += Tabs_CollectionChanged;
            Tabs = tabs;
            OpenTab(new MainTabViewModel());
        }


        public ConfigurationDataCollection ConfigurationData { get; set; } = new ConfigurationDataCollection();

        public ICommand NewTabCommand { get; }
        public ICollection<ITab> Tabs { get; }


        public string ServiceStatus => GetServiceStatus();

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

        private void NewTab()
        {
           OpenTab(new DatabaseEditTabViewModel());
        }

        private void OpenTab(ITab tab)
        {
            Tabs.Add(tab);
            SelectedTab = tab;
        }

        public string GetServiceStatus()
        {
            ServiceController ctrl = new ServiceController(ServiceName);
            try
            {
                return ctrl.Status.ToString();
            }
            catch (Exception ex)
            {
                Exception currentException = ex;
                StringBuilder builder = new StringBuilder();

                while (currentException != null)
                {
                    builder.AppendLine(currentException.Message);
                    currentException = currentException.InnerException;
                }
                return builder.ToString();
            }
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