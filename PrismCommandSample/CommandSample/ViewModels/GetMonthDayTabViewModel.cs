using CompositeCommandsCore;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace CommandSample.ViewModels
{
    public class GetMonthDayTabViewModel : BindableBase
    {
        IApplicationCommands _applicationCommands;

        private string _currentMonthDay;
        public string CurrentMonthDay
        {
            get { return _currentMonthDay; }
            set { SetProperty(ref _currentMonthDay, value); }
        }

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        public string Title { get; set; }

        public GetMonthDayTabViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            //给复合命令GetCurrentAllTimeCommand注册子命令GetMonthCommand
            _applicationCommands.GetCurrentAllTimeCommand.RegisterCommand(GetMonthCommand);
        }

        private DelegateCommand _getMonthCommand;
        public DelegateCommand GetMonthCommand =>
             _getMonthCommand ?? (_getMonthCommand = new DelegateCommand(ExecuteCommandName).ObservesCanExecute(() => IsCanExcute));

        void ExecuteCommandName()
        {
            this.CurrentMonthDay = DateTime.Now.ToString("MM:dd");
        }
    }
}
