using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PrismMetroSample.Infrastructure.Constants;
using Prism.Services.Dialogs;

namespace PrismMetroSample.Shell.ViewModels.Login
{
    public class CreateAccountViewModel : BindableBase,INavigationAware,IConfirmNavigationRequest
    {

        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private IRegionNavigationJournal _journal;

        #endregion

        #region Properties

        private string _registeredLoginId;
        public string RegisteredLoginId
        {
            get { return _registeredLoginId; }
            set { SetProperty(ref _registeredLoginId, value); }
        }

        public bool IsUseRequest { get; set; }

        #endregion

        #region Commands

        private DelegateCommand _loginMainContentCommand;
        public DelegateCommand LoginMainContentCommand =>
            _loginMainContentCommand ?? (_loginMainContentCommand = new DelegateCommand(ExecuteLoginMainContentCommand));

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(ExecuteGoBackCommand));

        private DelegateCommand<object> _verityCommand;
        public DelegateCommand<object> VerityCommand =>
    _verityCommand ?? (_verityCommand = new DelegateCommand<object>(ExecuteVerityCommand));

        #endregion

        #region  Excutes

        void ExecuteGoBackCommand()
        {
            _journal.GoBack();
        }

        void ExecuteLoginMainContentCommand()
        {
            Navigate("LoginMainContent");
        }

        void ExecuteVerityCommand(object parameter)
        {
            if (!VerityRegister(parameter))
            {
                return;
            }
            this.IsUseRequest = true;
            var Title = string.Empty;
            _dialogService.ShowDialog("SuccessDialog", new DialogParameters($"message={"注册成功"}"), null);
            _journal.GoBack();
        }

        #endregion


        public CreateAccountViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        private bool VerityRegister(object parameter)
        {
            if (string.IsNullOrEmpty(this.RegisteredLoginId))
            {
                MessageBox.Show("LoginId 不能为空！");
                return false;
            }
            var passwords = parameter as Dictionary<string, PasswordBox>;
            var password = (passwords["Password"] as PasswordBox).Password;
            var confimPassword = (passwords["ConfirmPassword"] as PasswordBox).Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password 不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(confimPassword))
            {
                MessageBox.Show("ConfirmPassword 不能为空！");
                return false;
            }
            if (password.Trim() != confimPassword.Trim())
            {
                MessageBox.Show("两次密码不一致");
                return false;
            }
            Global.AllUsers.Add(new User()
            {
                Id = Global.AllUsers.Max(t => t.Id) + 1,
                LoginId = this.RegisteredLoginId,
                PassWord = password
            });
            return true;
        }


        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //MessageBox.Show("退出了CreateAccount");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("从LoginMainContent导航到CreateAccount");
            _journal = navigationContext.NavigationService.Journal;
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (!string.IsNullOrEmpty(RegisteredLoginId) && this.IsUseRequest)
            {
                _dialogService.ShowDialog("AlertDialog", new DialogParameters($"message={"是否需要用当前注册的用户登录?"}"), r =>
                {
                    if (r.Result == ButtonResult.Yes)
                        navigationContext.Parameters.Add("loginId", RegisteredLoginId);
                });
            }
            continuationCallback(true);
            //var result = false;
            //if (MessageBox.Show("是否需要导航到LoginMainContent页面?", "Naviagte?",MessageBoxButton.YesNo) ==MessageBoxResult.Yes)
            //{
            //    result = true;
            //}
            //continuationCallback(result);
        }
    }
}
