using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class DeleteUserRoleCommand : Command<DeleteUserRoleRequest, DeleteUserRoleResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Id { get; set; }

        public DeleteUserRoleCommand()
            : base("delete user role")
        {
        }

        protected override void PreExecute(DeleteUserRoleRequest request)
        {
            request.Application = Guid.Parse(App.Value);

            request.Id = Guid.Parse(Id.Value);
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Id = cmd.Argument("id", "id of the user role");

            App = cmd.Argument("app", "id of the application");

            base.Execute(cmd);
        }
    }
}