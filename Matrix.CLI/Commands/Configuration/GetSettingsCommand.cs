using Matrix.Agent.Configurator.Messages.Commands.Requests;
using Matrix.Agent.Configurator.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Configuration
{
    public class GetSettingsCommand : Command<GetSettingsRequest, GetSettingsResponse>
    {
        private CommandArgument App { get; set; }

        public GetSettingsCommand()
            : base("lists application configuration")
        {
        }

        protected override void PreExecute(GetSettingsRequest request)
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