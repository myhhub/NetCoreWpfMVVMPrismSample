using Prism.Commands;
using Prism.Mvvm;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using PrismMetroSample.Infrastructure;
using Prism.Events;
using PrismMetroSample.Infrastructure.Events;
using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.PatientModule.Views;

namespace PrismMetroSample.PatientModule.ViewModels
{
    public class PatientListViewModel : BindableBase, IRegionMemberLifetime
    {

        #region Fields

        private readonly IEventAggregator _ea;
        private readonly IRegionManager _regionManager;
        private readonly IPatientService _patientService;
        private IRegion _region;
        private PatientList _patientListView;

        #endregion

        #region Properties

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        private List<Patient> _allPatients;
        public List<Patient> AllPatients
        {
            get { return _allPatients; }
            set { SetProperty(ref _allPatients, value); }
        }


        public bool KeepAlive => true;

        #endregion

        #region Commands

        private DelegateCommand<Patient> _mouseDoubleClickCommand;
        public DelegateCommand<Patient> MouseDoubleClickCommand =>
            _mouseDoubleClickCommand ?? (_mouseDoubleClickCommand = new DelegateCommand<Patient>(ExecuteMouseDoubleClickCommand));

        #endregion

        #region  Excutes

        /// <summary>
        /// DataGrid 双击按钮命令方法
        /// </summary>
        void ExecuteMouseDoubleClickCommand(Patient patient)
        {
            this.ApplicationCommands.ShowCommand.Execute(FlyoutNames.PatientDetailFlyout);//打开窗体
            _ea.GetEvent<PatientSentEvent>().Publish(patient);//发布消息
        }

        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public PatientListViewModel(IPatientService patientService, IEventAggregator ea, IApplicationCommands applicationCommands)
        {
            _ea = ea;
            this.ApplicationCommands = applicationCommands;
            _patientService = patientService;
            this.AllPatients = _patientService.GetAllPatients();         
        }

    }
}
