using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class GetUserCommand : Command<GetUserRequest, GetUserResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Id { get; set; }

        public GetUserCommand()
            : base("get user registered in the platform")
        {
        }

        protected override void PreExecute(GetUserRequest request)
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