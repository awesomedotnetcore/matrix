using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Directory
{
    public class RegisterDirectoryCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage user directory";

            cmd.Command("ls", new ListUserCommand().Execute);
            cmd.Command("id", new GetUserCommand().Execute);
            cmd.Command("create", new CreateUserCommand().Execute);
            cmd.Command("update", new UpdateUserCommand().Execute);
            cmd.Command("rm", new DeleteUserCommand().Execute);

            cmd.Command("group", new RegisterUserGroupCommands().Execute);
            cmd.Command("role", new RegisterUserRoleCommands().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("user");
                return 0;
            });
        }
    }
}