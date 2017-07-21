using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using ConfigurationTool.Entities;
using ConfigurationTool.Tabs;
using FileConductor.Configuration;
using FileConductor.LoggingService;
using FileConductor.Operations;
using Microsoft.Expression.Interactivity.Core;
using Ninject;

namespace ConfigurationTool.ViewModels
{
    public class EditWatcherTabViewModel : Tab
    {
        public EditWatcherTabViewModel(ITabController controller, ConfigurationData config, Watcher watcher)
            : base(controller)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            OperationExecutor = kernel.Get<IOperationExecutor>();
            ConfigurationService = kernel.Get<IConfigurationService>();
            Name = watcher.WatcherData.Code;
            SaveCommand = new ActionCommand(SaveWatcher);
            TestCommand = new ActionCommand(TestWatcher);
            AddProcedureCommand = new ActionCommand(AddProcedure);
            RemoveProcedureCommand = new ActionCommand(RemoveProcedure);
            EditProcedureCommand = new ActionCommand(EditProcedure);
            Watcher = watcher;
            Configuration = config;
        }

        private void RemoveProcedure()
        {
            Configuration.Procedures.Remove(Watcher.ProcedureData);
            Watcher.ProcedureData = Configuration.Procedures.FirstOrDefault();
            OnOperationModified?.Invoke();
        }

        [Inject]
        public IConfigurationService ConfigurationService { get; set; }

        [Inject]
        public IOperationExecutor OperationExecutor { get; set; }

        public bool IsProcedureTriggered
        {
            get { return Watcher.ProcedureData != null; }
            set
            {
                if (value == false)
                {
                    Watcher.ProcedureData = null;
                    Watcher.WatcherData.ProcedureId = 0;
                }
                else
                {
                    if (Configuration.Procedures.Any())
                    {
                        Watcher.ProcedureData = Configuration.Procedures.FirstOrDefault();
                    }
                    else
                    {
                        throw new NotImplementedException(
                            "No procedures defined in configuration file. This functionality is not implemented yet");
                    }
                }
            }
        }

        public Watcher Watcher { get; set; }
        public ConfigurationData Configuration { get; set; }
        public ActionCommand SaveCommand { get; set; }
        public ActionCommand TestCommand { get; set; }
        public ActionCommand AddProcedureCommand { get; set; }
        public ActionCommand RemoveProcedureCommand { get; set; }
        public ActionCommand EditProcedureCommand { get; set; }

        public event Action OnOperationModified;

        private void AddProcedure()
        {
            var procedureData = ConfigurationService.GetEmptyObject<ProcedureData>(Configuration);
            procedureData.Code = "New procedure";
            var dbEditVm = new DatabaseEditTabViewModel(TabController, procedureData);
            dbEditVm.OnProcedureModified += SaveWatcher;
            Watcher.ProcedureData = procedureData;
            TabController.OpenTab(dbEditVm);
        }

        private void EditProcedure()
        {
            var dbEditVm = new DatabaseEditTabViewModel(TabController, Watcher.ProcedureData);
            dbEditVm.OnProcedureModified += SaveWatcher;
            TabController.OpenTab(dbEditVm);
        }

        private void TestWatcher()
        {
            var operation = ConfigurationService.GetOperation(Configuration, Watcher.WatcherData);
            OperationExecutor.Execute(operation);
        }

        private void SaveWatcher()
        {
            OnOperationModified?.Invoke();
        }
    }
}