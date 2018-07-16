using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Registry
{
    public class DeleteApplicationCommand : Command<DeleteApplicationRequest, DeleteApplicationResponse>
    {
        private CommandArgument Id { get; set; }

        public DeleteApplicationCommand()
            : base("delete application in the registry")
        {
        }

        protected override void PreExecute(DeleteApplicationRequest request)
        {
            request.Id = Guid.Parse(Id.Value);
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Id = cmd.Argument("id", "id of the application");

            base.Execute(cmd);
        }
    }
}