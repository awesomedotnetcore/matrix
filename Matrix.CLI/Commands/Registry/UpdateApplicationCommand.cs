using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Registry
{
    public class UpdateApplicationCommand : Command<UpdateApplicationRequest, UpdateApplicationResponse>
    {
        private CommandArgument Id { get; set; }

        private CommandArgument Name { get; set; }

        private CommandArgument Description { get; set; }

        public UpdateApplicationCommand()
            : base("update application in the registry")
        {
        }

        protected override void PreExecute(UpdateApplicationRequest request)
        {
            request.Id = Guid.Parse(Id.Value);

            request.Name = Name.Value;

            request.Description = Description.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Id = cmd.Argument("id", "id of the application");

            Name = cmd.Argument("name", "name of the application");

            Description = cmd.Argument("desc", "description of the application");

            base.Execute(cmd);
        }
    }
}