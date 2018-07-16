using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class CreateUserGroupCommand : Command<CreateUserGroupRequest, CreateUserGroupResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Name { get; set; }

        private CommandArgument Description { get; set; }

        public CreateUserGroupCommand()
            : base("create user group")
        {
        }

        protected override void PreExecute(CreateUserGroupRequest request)
        {
            request.Application = Guid.Parse(App.Value);

            request.Name = Name.Value;

            request.Description = Description.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");

            Name = cmd.Argument("name", "name of the user group");

            Description = cmd.Argument("desc", "description of the user group");

            base.Execute(cmd);
        }
    }
}