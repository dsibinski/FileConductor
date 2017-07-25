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
        public EditWatcherTabViewModel(ITabController controller, Watcher watcher)
            : base(controller)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            OperationExecutor = kernel.Get<IOperationExecutor>();
            ConfigurationService = kernel.Get<IConfigurationService>();
            Name = watcher.WatcherData.Code;
            TestCommand = new ActionCommand(TestWatcher);
            AddProcedureCommand = new ActionCommand(AddProcedure);
            RemoveProcedureCommand = new ActionCommand(RemoveProcedure);
            EditProcedureCommand = new ActionCommand(EditProcedure);
            EditSourceTargetCommand = new ActionCommand(EditSourceTarget);
            EditDestinationTargetCommand = new ActionCommand(EditDestinationTarget);
            Watcher = watcher;
          
        }

        private void EditDestinationTarget()
        {
            var dbEditVm = new TargetEditViewModel(TabController, Watcher.Destination);
            TabController.OpenTab(dbEditVm);
        }

        private void EditSourceTarget()
        {
            var dbEditVm = new TargetEditViewModel(TabController, Watcher.Source);
            TabController.OpenTab(dbEditVm);
        }

        private void RemoveProcedure()
        {
            TabController.Configuration.Procedures.Remove(Watcher.ProcedureData);
        }


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
                    if (TabController.Configuration.Procedures.Any())
                    {
                        Watcher.ProcedureData = TabController.Configuration.Procedures.FirstOrDefault();
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
        public ActionCommand TestCommand { get; set; }
        public ActionCommand EditSourceTargetCommand { get; set; }
        public ActionCommand EditDestinationTargetCommand { get; set; }
        public ActionCommand AddProcedureCommand { get; set; }
        public ActionCommand RemoveProcedureCommand { get; set; }
        public ActionCommand EditProcedureCommand { get; set; }


        private void AddProcedure()
        {
            var procedureData = ConfigurationService.GetEmptyObject<ProcedureData>(TabController.Configuration);
            procedureData.Code = "New procedure";
            var dbEditVm = new DatabaseEditTabViewModel(TabController, procedureData);
            Watcher.ProcedureData = procedureData;
            TabController.OpenTab(dbEditVm);
        }

        private void EditProcedure()
        {
            var dbEditVm = new DatabaseEditTabViewModel(TabController, Watcher.ProcedureData);
            TabController.OpenTab(dbEditVm);
        }

        private void TestWatcher()
        {
            var operation = ConfigurationService.GetOperation(TabController.Configuration, Watcher.WatcherData);
            OperationExecutor.Execute(operation);
        }
    }
}