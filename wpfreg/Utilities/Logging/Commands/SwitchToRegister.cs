using Microsoft.Extensions.Logging;

namespace wpfreg.Utilities.Logging.Commands
{
    public class SwitchToRegister : BaseCommand
    {
        private readonly ILogger<SwitchToRegister> _switchToRegisterCommandLogger;

        public SwitchToRegister(ILogger<SwitchToRegister> switchToRegisterCommandLogger)
        {
            _switchToRegisterCommandLogger = switchToRegisterCommandLogger;
        }

        public override void Execute(object parameter)
        {
            _switchToRegisterCommandLogger.LogInformation($"{parameter}, User switched from login window to register.");
        }
    }
}
