using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Registry
{
    public class RegisterApplicationCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage applications";

            cmd.Command("ls", new ListApplicationCommand().Execute);
            cmd.Command("id", new GetApplicationByIdCommand().Execute);
            cmd.Command("create", new CreateApplicationCommand().Execute);
            cmd.Command("update", new UpdateApplicationCommand().Execute);
            cmd.Command("rm", new DeleteApplicationCommand().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("app");
                return 0;
            });
        }
    }
}