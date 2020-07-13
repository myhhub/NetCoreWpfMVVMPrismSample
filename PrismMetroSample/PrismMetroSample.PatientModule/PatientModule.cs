using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismMetroSample.PatientModule.Views;
using PrismMetroSample.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace PrismMetroSample.PatientModule
{
    public class PatientModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            //PatientList
            //regionManager.RegisterViewWithRegion(RegionNames.PatientListRegion, typeof(PatientList));
            //PatientDetail-Flyout
            regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(PatientDetail));

           
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
