using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Directory
{
    public class RegisterUserGroupCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage user groups";

            cmd.Command("ls", new ListUserGroupCommand().Execute);
            cmd.Command("create", new CreateUserGroupCommand().Execute);
            cmd.Command("update", new UpdateUserGroupCommand().Execute);
            cmd.Command("rm", new DeleteUserGroupCommand().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("group");
                return 0;
            });
        }
    }
}