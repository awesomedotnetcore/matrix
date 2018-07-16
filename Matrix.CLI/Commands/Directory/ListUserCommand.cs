using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class ListUserCommand : Command<ListUserRequest, ListUserResponse>
    {
        private CommandArgument App { get; set; }

        public ListUserCommand()
            : base("lists users registered in the platform")
        {
        }

        protected override void PreExecute(ListUserRequest request)
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