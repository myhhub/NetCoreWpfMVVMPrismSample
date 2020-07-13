using Prism.Unity;
using Prism.Ioc;
using System.Windows;
using CompositeCommandsSample.Views;
using Prism.Modularity;
using CompositeCommandsCore;

namespace CompositeCommandsSample
{

    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        //通过IOC容器注册IApplicationCommands为单例
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }

        //注册子窗体模块
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CommandSample.CommandSampleMoudle>();
        }
    }

}