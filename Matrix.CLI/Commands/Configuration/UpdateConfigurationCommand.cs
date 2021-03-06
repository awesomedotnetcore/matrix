﻿using Matrix.Agent.Configurator.Messages.Commands.Requests;
using Matrix.Agent.Configurator.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Configuration
{
    public class UpdateConfigurationCommand : Command<UpdateConfigurationRequest, UpdateConfigurationResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument Key { get; set; }

        private CommandArgument Value { get; set; }

        public UpdateConfigurationCommand()
            : base("create application configuration")
        {
        }

        protected override void PreExecute(UpdateConfigurationRequest request)
        {
            request.Application = Guid.Parse(App.Value);
            request.Key = Key.Value;
            request.Value = Value.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");
            Key = cmd.Argument("key", "key of the configuration");
            Value = cmd.Argument("value", "value of the configuration");

            base.Execute(cmd);
        }
    }
}