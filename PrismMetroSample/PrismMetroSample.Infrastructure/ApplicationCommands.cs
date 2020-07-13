using Prism.Commands;

namespace PrismMetroSample.Infrastructure
{
    public interface IApplicationCommands
    {
        public CompositeCommand ShowCommand { get; }
    }

    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _showCommand=new CompositeCommand();

        public CompositeCommand ShowCommand
        {
            get { return _showCommand; }
        }

    }
}
