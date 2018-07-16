using Matrix.Agent.Configurator.Messages.Commands.Requests;
using Matrix.Agent.Configurator.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Configuration
{
    public class GetConfigurationCommand : Command<GetConfigurationRequest, GetConfigurationResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Key { get; set; }

        public GetConfigurationCommand()
            : base("get application configuration")
        {
        }

        protected override void PreExecute(GetConfigurationRequest request)
        {
            request.Application = Guid.Parse(App.Value);
            request.Key = Key.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");
            Key = cmd.Argument("key", "key of the configuration");

            base.Execute(cmd);
        }
    }
}