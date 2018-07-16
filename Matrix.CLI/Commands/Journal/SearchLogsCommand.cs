using Matrix.Agent.Journal.Messages.Commands.Requests;
using Matrix.Agent.Journal.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Journal
{
    public class SearchLogsCommand : Command<SearchLogsRequest, SearchLogsResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument From { get; set; }

        private CommandArgument To { get; set; }

        private CommandArgument SearchTerm { get; set; }

        public SearchLogsCommand()
            : base("search application logs")
        {
        }

        protected override void PreExecute(SearchLogsRequest request)
        {
            request.Application = Guid.Parse(App.Value);
            request.From = DateTime.Parse(From.Value);
            request.To = DateTime.Parse(To.Value);
            request.SearchTerm = SearchTerm.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");
            From = cmd.Argument("from", "from date time range");
            To = cmd.Argument("to", "to date time range");
            SearchTerm = cmd.Argument("pattern", "search pattern of the logs");

            base.Execute(cmd);
        }
    }
}