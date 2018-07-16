using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class ListUserRoleCommand : Command<ListUserRoleRequest, ListUserRoleResponse>
    {
        private CommandArgument App { get; set; }

        public ListUserRoleCommand()
            : base("lists user roles registered for an application in the platform")
        {
        }

        protected override void PreExecute(ListUserRoleRequest request)
        {
            request.Application = Guid.Parse(App.Value);
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");

            base.Execute(cmd);
        }
    }
}