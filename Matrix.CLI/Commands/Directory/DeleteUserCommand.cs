using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class DeleteUserCommand : Command<DeleteUserRequest, DeleteUserResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Id { get; set; }


        public DeleteUserCommand()
            : base("delete user")
        {
        }

        protected override void PreExecute(DeleteUserRequest request)
        {
            request.Application = Guid.Parse(App.Value);

            request.Id = Guid.Parse(Id.Value);
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Id = cmd.Argument("id", "id of the user");

            App = cmd.Argument("app", "id of the application");

            base.Execute(cmd);
        }
    }
}