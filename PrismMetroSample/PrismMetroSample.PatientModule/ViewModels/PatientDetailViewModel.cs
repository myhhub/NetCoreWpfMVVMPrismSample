using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismMetroSample.Infrastructure.Events;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace PrismMetroSample.PatientModule.ViewModels
{
    public class PatientDetailViewModel : BindableBase
    {

        #region Fields

        private readonly IMedicineSerivce _medicineSerivce;
        private readonly IEventAggregator _ea;

        #endregion

        #region Properties

        private Patient _currentPatient;
        public Patient CurrentPatient
        {
            get { return _currentPatient; }
            set { SetProperty(ref _currentPatient, value); }
        }

        private ObservableCollection<Medicine> _lstMedicines;
        public ObservableCollection<Medicine> lstMedicines
        {
            get { return _lstMedicines; }
            set { SetProperty(ref _lstMedicines, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _cancleSubscribeCommand;
        public DelegateCommand CancleSubscribeCommand =>
            _cancleSubscribeCommand ?? (_cancleSubscribeCommand = new DelegateCommand(ExecuteCancleSubscribeCommand));

        #endregion

        #region  Excutes

        void ExecuteCancleSubscribeCommand()
        {
            _ea.GetEvent<MedicineSentEvent>().Unsubscribe(MedicineMessageReceived);
        }

        #endregion



        public PatientDetailViewModel(IEventAggregator ea, IMedicineSerivce medicineSerivce)
        {
            _medicineSerivce = medicineSerivce;
            _ea = ea;
            _ea.GetEvent<PatientSentEvent>().Subscribe(PatientMessageReceived);
            _ea.GetEvent<MedicineSentEvent>().Subscribe(MedicineMessageReceived,ThreadOption.PublisherThread,false,
                medicine=>medicine.Name=="当归"|| medicine.Name== "琼浆玉露");
        }

        /// <summary>
        /// 接受事件消息函数
        /// </summary>
        private void MedicineMessageReceived(Medicine  medicine)
        {
            this.lstMedicines?.Add(medicine);
        }

        private void PatientMessageReceived(Patient patient)
        {
            this.CurrentPatient = patient;
            this.lstMedicines = new ObservableCollection<Medicine>(_medicineSerivce.GetRecipesByPatientId(this.CurrentPatient.Id).FirstOrDefault().LstMedicines);
        }
    }
}
