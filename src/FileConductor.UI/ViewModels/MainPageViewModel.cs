using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.UI.Annotations;
using FileConductorUI.UI.Entities;
using FileConductorUI.UI.Services;
using FileConductorUI.UI.ViewModels;
using FileConductorUI.UI.Views;
using Microsoft.Expression.Interactivity.Core;

namespace FileConductorUI.UI
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public const string ServiceName = "FileConductor";
        private readonly ObservableCollection<ITab> tabs;
        private ITab _selectedTab;

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
            InstallService = new CommandHandler(InstallServiceLogic);
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

        public CommandHandler InstallService { get; set; }
        public CommandHandler SettingsHandler { get; set; }


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

        private void InstallServiceLogic()
        {
            //var servicePath = FileConductorService.GetExecutionAssembly();
            //  string strCmdText = servicePath + " install";
            //  ExecuteCommandSync(strCmdText);
        }


        public void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
            }
            catch (Exception objException)
            {
                // Log the exception
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