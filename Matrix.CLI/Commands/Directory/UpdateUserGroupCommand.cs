using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class UpdateUserGroupCommand : Command<UpdateUserGroupRequest, UpdateUserGroupResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Id { get; set; }

        private CommandArgument Name { get; set; }

        private CommandArgument Description { get; set; }

        public UpdateUserGroupCommand()
            : base("update user group")
        {
        }

        protected override void PreExecute(UpdateUserGroupRequest request)
        {
            request.Application = Guid.Parse(App.Value);

            request.Id = Guid.Parse(Id.Value);

            request.Name = Name.Value;

            request.Description = Description.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Id = cmd.Argument("id", "id of the user group");

            App = cmd.Argument("app", "id of the application");

            Name = cmd.Argument("name", "name of the user group");

            Description = cmd.Argument("desc", "description of the user group");

            base.Execute(cmd);
        }
    }
}