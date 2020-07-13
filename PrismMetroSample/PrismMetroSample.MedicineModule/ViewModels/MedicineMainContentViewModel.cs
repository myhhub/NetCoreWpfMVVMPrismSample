using Prism.Mvvm;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infrastructure.Services;
using System.Collections.ObjectModel;
using Prism.Events;
using PrismMetroSample.Infrastructure.Events;
using System;
using Prism;
using System.Windows;
using Prism.Services.Dialogs;

namespace PrismMetroSample.MedicineModule.ViewModels
{
    public class MedicineMainContentViewModel : BindableBase,IActiveAware
    {
        #region Fields

        private readonly IMedicineSerivce _medicineSerivce;
        private readonly IEventAggregator _ea;
        private readonly IDialogService _dialogService;

        public event EventHandler IsActiveChanged;

        #endregion

        #region Properties

        private ObservableCollection<Medicine> _allMedicines;
        public ObservableCollection<Medicine> AllMedicines
        {
            get { return _allMedicines; }
            set { SetProperty(ref _allMedicines, value); }
        }

        bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"视图被激活了"}"), null);
                }
                else
                {
                    _dialogService.ShowDialog("WarningDialog", new DialogParameters($"message={"视图失效了"}"), null);
                }
                IsActiveChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion

        #region Commands


        #endregion

        #region  Excutes



        #endregion



        public MedicineMainContentViewModel(IMedicineSerivce medicineSerivce,IEventAggregator ea,IDialogService dialogService)
        {
            _medicineSerivce = medicineSerivce;
            _ea = ea;
            _dialogService = dialogService;
            this.AllMedicines = new ObservableCollection<Medicine>(_medicineSerivce.GetAllMedicines());
            _ea.GetEvent<MedicineSentEvent>().Subscribe(MedicineMessageReceived);//订阅事件
        }

        /// <summary>
        /// 事件消息接受函数
        /// </summary>
        private void MedicineMessageReceived(Medicine medicine)
        {
            this.AllMedicines?.Add(medicine);
        }
    }
}
