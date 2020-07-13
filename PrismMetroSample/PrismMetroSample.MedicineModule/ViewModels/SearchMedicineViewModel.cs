using Prism.Commands;
using Prism.Mvvm;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.Infrastructure.Models;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using PrismMetroSample.Infrastructure.Events;

namespace PrismMetroSample.MedicineModule.ViewModels
{
    public class SearchMedicineViewModel : BindableBase
    {
        #region Fields

        private readonly IMedicineSerivce _medicineSerivce;
        private readonly IEventAggregator _ea;

        #endregion

        #region Properties

        private List<Medicine> _allMedicines;
        public List<Medicine> AllMedicines
        {
            get { return _allMedicines; }
            set { SetProperty(ref _allMedicines, value); }
        }

        private List<Medicine> _currentMedicines;
        public List<Medicine> CurrentMedicines
        {
            get { return _currentMedicines; }
            set { SetProperty(ref _currentMedicines, value); }
        }

        private string _searchCondition;
        public string SearchCondition
        {
            get { return _searchCondition; }
            set { SetProperty(ref _searchCondition, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand));

        private DelegateCommand<Medicine> _addMedicineCommand;
        public DelegateCommand<Medicine> AddMedicineCommand =>
            _addMedicineCommand ?? (_addMedicineCommand = new DelegateCommand<Medicine>(ExecuteAddMedicineCommand));

        #endregion

        #region  Excutes

        void ExecuteSearchCommand()
        {
            this.CurrentMedicines = this.AllMedicines.Where(t => t.Name.Contains(this.SearchCondition) || t.Type.Contains(this.SearchCondition)
            || t.Unit.Contains(this.SearchCondition)).ToList();
        }

        void ExecuteAddMedicineCommand(Medicine currentMedicine)
        {
            _ea.GetEvent<MedicineSentEvent>().Publish(currentMedicine);
        }

        #endregion



        public SearchMedicineViewModel(IMedicineSerivce medicineSerivce, IEventAggregator ea)
        {
            _ea = ea;
            _medicineSerivce = medicineSerivce;
            this.CurrentMedicines = this.AllMedicines = _medicineSerivce.GetAllMedicines();
        }
    }
}
