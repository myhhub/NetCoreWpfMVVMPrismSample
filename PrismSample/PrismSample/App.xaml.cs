using Prism.Unity;
using Prism.Ioc;
using System.Windows;
using PrismSample.Views;
using Prism.Mvvm;
using System.Reflection;
using System;
using PrismSample.ViewModels;

namespace PrismSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        //设置启动起始页
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
        //配置规则
        protected override void ConfigureViewModelLocator()
        {
            /*
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Replace(".Viewsb.", ".ViewModelsa.OhMyGod.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}Test, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
            */
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
        }
    }
}
