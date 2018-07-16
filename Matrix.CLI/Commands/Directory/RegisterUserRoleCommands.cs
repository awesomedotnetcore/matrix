using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Directory
{
    public class RegisterUserRoleCommands
    {
        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "manage user roles";

            cmd.Command("ls", new ListUserRoleCommand().Execute);
            cmd.Command("create", new CreateUserRoleCommand().Execute);
            cmd.Command("update", new UpdateUserRoleCommand().Execute);
            cmd.Command("rm", new DeleteUserRoleCommand().Execute);

            cmd.OnExecute(() =>
            {
                cmd.ShowHelp("role");
                return 0;
            });
        }
    }
}