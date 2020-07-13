using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.MedicineModule.Views;
using PrismMetroSample.PatientModule.Views;
using PrismMetroSample.Shell.Views.RegionAdapterViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PrismMetroSample.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields

        private IModuleManager _moduleManager;
        private readonly IDialogService _dialogService;
        private IRegion _paientListRegion;
        private IRegion _medicineListRegion;
        private PatientList _patientListView;
        private MedicineMainContent _medicineMainContentView;

        #endregion

        #region Properties

        public IRegionManager RegionMannager { get; }

        private bool _isCanExcute = false;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _loadingCommand;
        public DelegateCommand LoadingCommand =>
            _loadingCommand ?? (_loadingCommand = new DelegateCommand(ExecuteLoadingCommand));

        private DelegateCommand _activePaientListCommand;
        public DelegateCommand ActivePaientListCommand =>
            _activePaientListCommand ?? (_activePaientListCommand = new DelegateCommand(ExecuteActivePaientListCommand));

        private DelegateCommand _deactivePaientListCommand;
        public DelegateCommand DeactivePaientListCommand =>
            _deactivePaientListCommand ?? (_deactivePaientListCommand = new DelegateCommand(ExecuteDeactivePaientListCommand));

        private DelegateCommand _activeMedicineListCommand;
        public DelegateCommand ActiveMedicineListCommand =>
            _activeMedicineListCommand ?? (_activeMedicineListCommand = new DelegateCommand(ExecuteActiveMedicineListCommand).ObservesCanExecute(() => IsCanExcute));

        private DelegateCommand _deactiveMedicineListCommand;
        public DelegateCommand DeactiveMedicineListCommand =>
            _deactiveMedicineListCommand ?? (_deactiveMedicineListCommand = new DelegateCommand(ExecuteDeactiveMedicineListCommand).ObservesCanExecute(() => IsCanExcute));


        private DelegateCommand _loadMedicineModuleCommand;
        public DelegateCommand LoadMedicineModuleCommand =>
            _loadMedicineModuleCommand ?? (_loadMedicineModuleCommand = new DelegateCommand(ExecuteLoadMedicineModuleCommand));

        #endregion

        #region  Excutes

        void ExecuteLoadingCommand()
        {

            _paientListRegion = RegionMannager.Regions[RegionNames.PatientListRegion];
            _patientListView = CommonServiceLocator.ServiceLocator.Current.GetInstance<PatientList>();
            _paientListRegion.Add(_patientListView);

            var uniformContentRegion = RegionMannager.Regions["UniformContentRegion"];
            var regionAdapterView1 = CommonServiceLocator.ServiceLocator.Current.GetInstance<RegionAdapterView1>();
            uniformContentRegion.Add(regionAdapterView1);
            var regionAdapterView2 = CommonServiceLocator.ServiceLocator.Current.GetInstance<RegionAdapterView2>();
            uniformContentRegion.Add(regionAdapterView2);

            _medicineListRegion = RegionMannager.Regions[RegionNames.MedicineMainContentRegion];
        }


        void ExecuteDeactiveMedicineListCommand()
        {
            _medicineListRegion.Deactivate(_medicineMainContentView);
        }

        void ExecuteActiveMedicineListCommand()
        {
            _medicineListRegion.Activate(_medicineMainContentView);
        }

        void ExecuteLoadMedicineModuleCommand()
        {
            _moduleManager.LoadModule("MedicineModule");
            _medicineMainContentView = (MedicineMainContent)_medicineListRegion.Views.Where(t => t.GetType() == typeof(MedicineMainContent)).FirstOrDefault();
            this.IsCanExcute = true;
        }

        void ExecuteDeactivePaientListCommand()
        {
            _paientListRegion.Deactivate(_patientListView);
        }

        void ExecuteActivePaientListCommand()
        {
            _paientListRegion.Activate(_patientListView);
        }

        #endregion

        public MainWindowViewModel(IModuleManager moduleManager,IRegionManager regionManager,IDialogService dialogService)
        {
            _moduleManager = moduleManager;
            RegionMannager = regionManager;
            _dialogService = dialogService;
            _moduleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
        }


        private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={e.ModuleInfo.ModuleName+ "模块被加载了"}"), null);
        }


    }
}
