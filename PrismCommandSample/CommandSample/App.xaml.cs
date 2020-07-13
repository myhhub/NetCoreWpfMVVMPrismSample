using Prism.Unity;
using Prism.Ioc;
using System.Windows;
using Prism.Mvvm;
using CommandSample.Views;
using CommandSample.ViewModels;

namespace CommandSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        //设置启动起始页
        protected override Window CreateShell()
        {
            return null;//Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        //配置规则
        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}
