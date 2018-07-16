using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Configuration
{
    public class RegisterConfigurationCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage application configuration";

            cmd.Command("ls", new GetSettingsCommand().Execute);
            cmd.Command("id", new GetConfigurationCommand().Execute);
            cmd.Command("create", new CreateConfigurationCommand().Execute);
            cmd.Command("update", new UpdateConfigurationCommand().Execute);
            cmd.Command("rm", new DeleteConfigurationCommand().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("config");
                return 0;
            });
        }
    }
}