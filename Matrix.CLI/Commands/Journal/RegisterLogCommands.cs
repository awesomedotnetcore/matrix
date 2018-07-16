using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Journal
{
    public class RegisterLogCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage application logs";

            cmd.Command("ls", new GetLogsCommand().Execute);
            cmd.Command("search", new SearchLogsCommand().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("log");
                return 0;
            });
        }
    }
}