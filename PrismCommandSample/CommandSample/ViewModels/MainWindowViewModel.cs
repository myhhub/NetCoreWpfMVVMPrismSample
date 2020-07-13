using CompositeCommandsCore;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace CommandSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IApplicationCommands _applicationCommands;

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }

        public string Title { get; set; }

        public MainWindowViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            //给复合命令GetCurrentAllTimeCommand注册子命令GetYearCommand
            _applicationCommands.GetCurrentAllTimeCommand.RegisterCommand(GetYearCommand);
        }

        private DelegateCommand _getYearCommand;
        public DelegateCommand GetYearCommand =>
           _getYearCommand ?? (_getYearCommand = new DelegateCommand(ExecuteGetYearCommand).ObservesCanExecute(() => IsCanExcute));

        void ExecuteGetYearCommand()
        {
            this.CurrentTime = DateTime.Now.ToString("yyyy");
        }
    }
}