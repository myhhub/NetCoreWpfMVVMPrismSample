using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismMetroSample.MedicineModule.Views;
using PrismMetroSample.Infrastructure.Constants;
using System;
using System.Windows.Controls;

namespace PrismMetroSample.MedicineModule
{
    [Module(ModuleName = "MedicineModule", OnDemand =true)]
    public class MedicineModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();


            //MedicineMainContent
            regionManager.RegisterViewWithRegion(RegionNames.MedicineMainContentRegion, typeof(MedicineMainContent));

            //SearchMedicine-Flyout
            regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(SearchMedicine));

            //rightWindowCommandsRegion
            regionManager.RegisterViewWithRegion(RegionNames.ShowSearchPatientRegion, typeof(ShowSearchPatient));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
