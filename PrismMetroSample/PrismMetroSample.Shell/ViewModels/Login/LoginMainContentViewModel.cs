using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismMetroSample.Infrastructure.Constants;
using PrismMetroSample.Infrastructure.Models;
using PrismMetroSample.Shell.Views;
using PrismMetroSample.Shell.Views.Login;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Prism.Services.Dialogs;

namespace PrismMetroSample.Shell.ViewModels.Login
{
    public class LoginMainContentViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        #region Fields

        private IRegionNavigationJournal _journal;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        private User _currentUser = new User();
        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        public bool KeepAlive => true;

        #endregion

        #region Commands

        private DelegateCommand _createAccountCommand;
        public DelegateCommand CreateAccountCommand =>
            _createAccountCommand ?? (_createAccountCommand = new DelegateCommand(ExecuteCreateAccountCommand));

        private DelegateCommand _goForwardCommand;
        public DelegateCommand GoForwardCommand =>
            _goForwardCommand ?? (_goForwardCommand = new DelegateCommand(ExecuteGoForwardCommand));

        private DelegateCommand<PasswordBox> _loginCommand;
        public DelegateCommand<PasswordBox> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<PasswordBox>(ExecuteLoginCommand, CanExecuteGoForwardCommand));

        #endregion

        #region  Excutes

        void ExecuteCreateAccountCommand()
        {
            Navigate("CreateAccount");
        }
        void ExecuteLoginCommand(PasswordBox passwordBox)
        {
            if (string.IsNullOrEmpty(this.CurrentUser.LoginId))
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"LoginId 不能为空!"}"),null);
                return;
            }
            this.CurrentUser.PassWord = passwordBox.Password;
            if (string.IsNullOrEmpty(this.CurrentUser.PassWord))
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"PassWord 不能为空!"}"), null);
                return;
            }
            else if (Global.AllUsers.Where(t => t.LoginId == this.CurrentUser.LoginId && t.PassWord == this.CurrentUser.PassWord).Count() == 0)
            {
                _dialogService.Show("WarningDialog", new DialogParameters($"message={"LoginId 或者 PassWord 错误!"}"), null);
                return;
            }
            ShellSwitcher.Switch<LoginWindow, MainWindow>();
        }

        private void ExecuteGoForwardCommand()
        {
            _journal.GoForward();
        }

        #endregion

        public LoginMainContentViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
        }



       private bool CanExecuteGoForwardCommand(PasswordBox passwordBox)
        {
            this.IsCanExcute=_journal != null && _journal.CanGoForward;
            return true;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {          
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //MessageBox.Show("退出了LoginMainContent");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("从CreateAccount导航到LoginMainContent");
            _journal = navigationContext.NavigationService.Journal;

            var loginId= navigationContext.Parameters["loginId"] as string;
            if (loginId!=null)
            {
                this.CurrentUser = new User() { LoginId=loginId};
            }
            LoginCommand.RaiseCanExecuteChanged();
        }
    }
}
