using CompositeCommandsCore;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace CommandSample.ViewModels
{
    public class GetHourTabViewModel : BindableBase 
    {
        IApplicationCommands _applicationCommands;

        private string _currentHour;
        public string CurrentHour
        {
            get { return _currentHour; }
            set { SetProperty(ref _currentHour, value); }
        }

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        public string Title { get; set; }

        public GetHourTabViewModel(IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            //给复合命令GetCurrentAllTimeCommand注册子命令GetHourCommand
            _applicationCommands.GetCurrentAllTimeCommand.RegisterCommand(GetHourCommand);
        }

        private DelegateCommand _getHourCommand;
        public DelegateCommand GetHourCommand =>
           _getHourCommand ?? (_getHourCommand = new DelegateCommand(ExecuteGetHourCommand).ObservesCanExecute(() => IsCanExcute));

        void ExecuteGetHourCommand()
        {
            this.CurrentHour = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
