using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using FileConductor.Configuration.XmlData;
using ConfigurationTool.Properties;
using ConfigurationTool.Tabs;

namespace ConfigurationTool.ViewModels
{
    public class DatabaseEditTabViewModel : Tab,INotifyPropertyChanged
    {
        private List<string> _databaseList;
        private string _selectedDatabaseName;
        private string _host;
        private string _user;
        private string _password;
        private string _dbName;


        public DatabaseEditTabViewModel(ITabController controller) : base(controller)
        {
            CheckConnectionCommand = new CommandHandler(CheckDBConenction);
            CloseHandler = new CommandHandler(CloseAction);
            Name = "Database";
        }

        public CommandHandler CheckConnectionCommand { get; set; }
        public CommandHandler CloseHandler { get; set; }

        public string SelectedDatabaseName
        {
            get { return _selectedDatabaseName; }
            set
            {
                _selectedDatabaseName = value;
                OnPropertyChanged(nameof(SelectedDatabaseName));
            }
        }


        public string Host
        {
            get { return _host; }
            set
            {
                _host = value;
                OnPropertyChanged(nameof(Host));
            }
        }

        public string User
        {
            get { return _user; }
            set
            {
                _user = value; 
                OnPropertyChanged(nameof(User));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string DbName
        {
            get { return _dbName; }
            set
            {
                _dbName = value;
                OnPropertyChanged(nameof(DbName));
            }
        }

        public List<string> DatabaseList
        {
            get { return _databaseList; }
            set
            {
                _databaseList = value;
                OnPropertyChanged(nameof(DatabaseList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CloseAction()
        {
           Application.Current.MainWindow.Close();
        }


        public void LoadDatabases()
        {
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};", Host, User, Password);

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    DataTable databases = con.GetSchema("Databases");
                    List<string> databaseList = new List<string>();
                    foreach (DataRow database in databases.Rows)
                    {
                        String databaseName = database.Field<String>("database_name");
                        databaseList.Add(databaseName);
                    }
                    DatabaseList = databaseList;
                    SelectedDatabaseName = databaseList.FirstOrDefault();
                }
                catch (SqlException)
                {
                }
            }
        }


        private void CheckDBConenction()
        {
            IsServerConnected();
        }

        public bool IsServerConnected()
        {
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};", Host, User, Password);

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void LoadDatabaseData(ProcedureData data)
        {
            Host = data.Host;
            DbName = data.Name;
            Password = data.Password;
            User = data.User;

        }
    }
}