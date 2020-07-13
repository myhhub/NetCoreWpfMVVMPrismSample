using Prism.Commands;
using Prism.Mvvm;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private DelegateCommand _clickCommnd;
        public DelegateCommand ClickCommnd =>
            _clickCommnd ?? (_clickCommnd = new DelegateCommand(ExecuteClickCommnd));

        void ExecuteClickCommnd()
        {
            this.Text = "Click Me！";
        }

        public MainWindowViewModel()
        {
            this.Text = "Hello Prism!";
        }
    }
}