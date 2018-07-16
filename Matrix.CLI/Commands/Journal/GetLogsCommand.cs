using Matrix.Agent.Journal.Messages.Commands.Requests;
using Matrix.Agent.Journal.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Journal
{
    public class GetLogsCommand : Command<GetLogsRequest, GetLogsResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument From { get; set; }

        private CommandArgument To { get; set; }

        public GetLogsCommand()
            : base("lists application logs")
        {
        }

        protected override void PreExecute(GetLogsRequest request)
        {
            request.Application = Guid.Parse(App.Value);
            request.From = DateTime.Parse(From.Value);
            request.To = DateTime.Parse(To.Value);
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");
            From = cmd.Argument("from", "from date time range");
            To = cmd.Argument("to", "to date time range");

            base.Execute(cmd);
        }
    }
}