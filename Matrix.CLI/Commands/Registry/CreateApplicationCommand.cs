using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.Registry
{
    public class CreateApplicationCommand : Command<RegisterApplicationRequest, CreateApplicationResponse>
    {
        private CommandArgument Name { get; set; }

        private CommandArgument Description { get; set; }

        public CreateApplicationCommand()
            : base("register application in the registry")
        {
        }

        protected override void PreExecute(RegisterApplicationRequest request)
        {
            request.Name = Name.Value;

            request.Description = Description.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Name = cmd.Argument("name", "name of the application");

            Description = cmd.Argument("desc", "description of the application");

            base.Execute(cmd);
        }
    }
}