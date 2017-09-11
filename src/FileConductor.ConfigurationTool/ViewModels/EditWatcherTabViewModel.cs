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
            Name = watcher.WatcherData.Code;
            TestCommand = new ActionCommand(TestWatcher);
            AddProcedureCommand = new ActionCommand(AddProcedure);
            RemoveProcedureCommand = new ActionCommand(RemoveProcedure);
            EditProcedureCommand = new ActionCommand(EditProcedure);
            EditSourceTargetCommand = new ActionCommand(EditSourceTarget);
            AddSourceTargetCommand = new ActionCommand(AddSourceTarget);
            RemoveSourceTargetCommand = new ActionCommand(RemoveSourceTarget);
            EditDestinationTargetCommand = new ActionCommand(EditDestinationTarget);
            AddDestinationTargetCommand = new ActionCommand(AddDestinationTarget);
            RemoveDestinationTargetCommand = new ActionCommand(RemoveDestinationTarget);
            EditScheduleCommand = new ActionCommand(EditSchedule);
            AddScheduleCommand = new ActionCommand(AddSchedule);
            RemoveScheduleCommand = new ActionCommand(RemoveSchedule);
            Watcher = watcher;
        }

        #region Adds
        private void AddProcedure()
        {
            var procedureData = TabController.ConfigurationService.GetEmptyObject<ProcedureData>(TabController.Configuration);
            procedureData.Code = "New procedure";
            var dbEditVm = new DatabaseEditTabViewModel(TabController, procedureData);
            Watcher.ProcedureData = procedureData;
            TabController.OpenTab(dbEditVm);
        }
        private void AddSchedule()
        {
            var schedule = TabController.ConfigurationService.GetEmptyObject<ScheduleData>(TabController.Configuration);
            schedule.Code = "New schedule";
            var sEditVm = new ScheduleEditTabViewModel(TabController, schedule);
            Watcher.Schedule = schedule;
            TabController.OpenTab(sEditVm);
        }
        private void AddDestinationTarget()
        {
            var targetData = TabController.ConfigurationService.GetEmptyObject<TargetData>(TabController.Configuration);
            targetData.Code = "New target";
            var sEditVm = new TargetEditViewModel(TabController, targetData);
            Watcher.Destination = targetData;
            TabController.OpenTab(sEditVm);
        }
        private void AddSourceTarget()
        {
            var targetData = TabController.ConfigurationService.GetEmptyObject<TargetData>(TabController.Configuration);
            targetData.Code = "New target";
            var sEditVm = new TargetEditViewModel(TabController, targetData);
            Watcher.Source = targetData;
            TabController.OpenTab(sEditVm);
        }
        #endregion

        #region Edits
        private void EditSchedule()
        {
            if(Watcher.Schedule == null) return;
            var schEditVm = new ScheduleEditTabViewModel(TabController,Watcher.Schedule);
            TabController.OpenTab(schEditVm);
        }

        private void EditDestinationTarget()
        {
            if (Watcher.Destination == null) return;
            var dbEditVm = new TargetEditViewModel(TabController, Watcher.Destination);
            TabController.OpenTab(dbEditVm);
        }

        private void EditSourceTarget()
        {
            if (Watcher.Source == null) return;
            var dbEditVm = new TargetEditViewModel(TabController, Watcher.Source);
            TabController.OpenTab(dbEditVm);
        }
        private void EditProcedure()
        {
            if (Watcher.ProcedureData == null) return;
            var dbEditVm = new DatabaseEditTabViewModel(TabController, Watcher.ProcedureData);
            TabController.OpenTab(dbEditVm);
        }

        #endregion

        #region Removes
        private void RemoveProcedure()
        {
            TabController.Configuration.Procedures.Remove(Watcher.ProcedureData);
        }

        private void RemoveSourceTarget()
        {
            TabController.Configuration.Targets.Remove(Watcher.Source);
        }

        private void RemoveDestinationTarget()
        {
            TabController.Configuration.Targets.Remove(Watcher.Destination);
        }

        private void RemoveSchedule()
        {
            TabController.Configuration.Schedules.Remove(Watcher.Schedule);
        }
#endregion

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
        public ActionCommand RemoveSourceTargetCommand { get; set; }
        public ActionCommand AddSourceTargetCommand { get; set; }
        public ActionCommand EditDestinationTargetCommand { get; set; }
        public ActionCommand RemoveDestinationTargetCommand { get; set; }
        public ActionCommand AddDestinationTargetCommand { get; set; }
        public ActionCommand RemoveScheduleCommand { get; set; }
        public ActionCommand AddScheduleCommand { get; set; }
        public ActionCommand EditScheduleCommand { get; set; }
        public ActionCommand AddProcedureCommand { get; set; }
        public ActionCommand RemoveProcedureCommand { get; set; }
        public ActionCommand EditProcedureCommand { get; set; }
        private void TestWatcher()
        {
            var operation = TabController.ConfigurationService.GetOperation(TabController.Configuration, Watcher.WatcherData);
            OperationExecutor.Execute(operation);
        }
    }
}