using Matrix.Agent.Journal.Business;
using Matrix.Agent.Journal.Messages.Commands.Requests;
using Matrix.Agent.Journal.Messages.Commands.Responses;
using Matrix.Agent.Middlewares.Responders;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus.Responders
{
    public class LogResponder : Endpoint
    {
        private ILogService Server { get; }

        public LogResponder(ILogger logger, ILogService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<GetLogsResponse> GetLogs(GetLogsRequest o)
        {
            var result = new GetLogsResponse(o.RequestId);

            result.Logs.AddRange(await Server.GetLogEntries(o.Application, o.From, o.To));

            return result;
        }

        public async Task<SearchLogsResponse> SearchLogs(SearchLogsRequest o)
        {
            var result = new SearchLogsResponse(o.RequestId);

            result.Logs.AddRange(await Server.SearchLogEntries(o.Application, o.From, o.To, o.SearchTerm));

            return result;
        }
    }
}