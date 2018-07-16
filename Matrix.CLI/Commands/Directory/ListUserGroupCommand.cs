using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class ListUserGroupCommand : Command<ListUserGroupRequest, ListUserGroupResponse>
    {
        private CommandArgument App { get; set; }

        public ListUserGroupCommand()
            : base("lists user groups registered for an application in the platform")
        {
        }

        protected override void PreExecute(ListUserGroupRequest request)
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