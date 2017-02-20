using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.UI.Annotations;

namespace FileConductorUI.UI
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public const string ServiceName = "FileConductor";


        private ConfigurationData _configData;
        public ConfigurationData ConfigData

        {
            get { return _configData; }
            set
            {
                _configData = value;
                ConfigData.SelectedTarget = ConfigData.Targets.FirstOrDefault();
                ConfigData.SelectedDatabase = ConfigData.Databases.FirstOrDefault();
                ConfigData.SelectedSchedule = ConfigData.Schedules.FirstOrDefault();
                OnPropertyChanged("ConfigData");
        
            }
        }



        public MainPageViewModel()
        {
            InstallService = new CommandHandler(InstallServiceLogic);
            SettingsHandler = new CommandHandler(OpenSettingsView);

            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();
             ConfigData = deserializer.XmlData;
        }

        public string ServiceStatus => GetServiceStatus();

        public CommandHandler InstallService { get; set; }
        public CommandHandler SettingsHandler { get; set; }       

        public event PropertyChangedEventHandler PropertyChanged;


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

        private void OpenSettingsView()
        {
            //  SettingsView settingsView = new SettingsView();
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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}