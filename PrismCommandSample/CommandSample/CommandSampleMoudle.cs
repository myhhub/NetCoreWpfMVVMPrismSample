using CommandSample.ViewModels;
using CommandSample.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CommandSample
{
    public class CommandSampleMoudle : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            IRegion region = regionManager.Regions["ContentRegion"];

            var mainWindow = containerProvider.Resolve<MainWindow>();
            (mainWindow.DataContext as MainWindowViewModel).Title = "GetYearTab";
            region.Add(mainWindow);

            var getMonthTab = containerProvider.Resolve<GetMonthDayTab>();
            (getMonthTab.DataContext as GetMonthDayTabViewModel).Title = "GetMonthDayTab";
            region.Add(getMonthTab);

            var getHourTab = containerProvider.Resolve<GetHourTab>();
            (getHourTab.DataContext as GetHourTabViewModel).Title = "GetHourTab";
            region.Add(getHourTab);
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}