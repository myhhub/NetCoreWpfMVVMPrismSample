using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using System.Windows;
using PrismMetroSample.Infrastructure.Services;
using PrismMetroSample.Infrastructure;
using PrismMetroSample.Shell.Views.Login;
using Prism.Regions;
using System.Windows.Controls.Primitives;
using PrismMetroSample.Infrastructure.CustomerRegionAdapters;
using PrismMetroSample.Shell.Views.Dialogs;
using PrismMetroSample.Shell.ViewModels.Dialogs;

namespace PrismMetroSample.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册服务
            containerRegistry.Register<IMedicineSerivce, MedicineSerivce>();
            containerRegistry.Register<IPatientService, PatientService>();
            containerRegistry.Register<IUserService, UserService>();

            //注册全局命令
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());

            //注册导航
            containerRegistry.RegisterForNavigation<LoginMainContent>();
            containerRegistry.RegisterForNavigation<CreateAccount>();

            //注册对话框
            containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
            containerRegistry.RegisterDialog<SuccessDialog, SuccessDialogViewModel>();
            containerRegistry.RegisterDialog<WarningDialog, WarningDialogViewModel>();
            containerRegistry.RegisterDialogWindow<DialogWindow>();
        }

        //protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        //{
        //    moduleCatalog.AddModule<PrismMetroSample.PatientModule.PatientModule>();
        //    var MedicineModuleType = typeof(PrismMetroSample.MedicineModule.MedicineModule);
        //    moduleCatalog.AddModule(new ModuleInfo()
        //    {
        //        ModuleName= MedicineModuleType.Name,
        //        ModuleType=MedicineModuleType.AssemblyQualifiedName,
        //        InitializationMode=InitializationMode.OnDemand
        //    });

        //}

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(UniformGrid), Container.Resolve<UniformGridRegionAdapter>());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
            //return new ConfigurationModuleCatalog();
        }
    }
}
