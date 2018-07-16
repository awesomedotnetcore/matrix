using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Journal.Messages.Commands.Requests
{
    public class GetLogsRequest : Request
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}