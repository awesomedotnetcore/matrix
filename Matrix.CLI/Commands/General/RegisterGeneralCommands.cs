using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.General
{
    public class RegisterGeneralCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage system";

            cmd.Command("clean", new CleanCommand().Execute);
            cmd.Command("error", new GetErrorCommand().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("system");
                return 0;
            });
        }
    }
}