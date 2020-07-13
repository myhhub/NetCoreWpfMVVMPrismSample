using CompositeCommandsCore;
using Prism.Mvvm;

namespace CompositeCommandsSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        public MainWindowViewModel(IApplicationCommands applicationCommands)
        {
            this.ApplicationCommands = applicationCommands;
        }

    }
}