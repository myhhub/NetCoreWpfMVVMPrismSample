using Prism.Commands;

namespace CompositeCommandsCore
{
    public interface IApplicationCommands
    {
        CompositeCommand GetCurrentAllTimeCommand { get; }
    }

    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _getCurrentAllTimeCommand = new CompositeCommand();
        public CompositeCommand GetCurrentAllTimeCommand
        {
            get { return _getCurrentAllTimeCommand; }
        }
    }
}
