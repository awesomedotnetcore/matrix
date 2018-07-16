using Matrix.Agent.Messages.Commands.Responses;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Journal.Messages.Commands.Responses
{
    public class SearchLogsResponse : Response
    {
        public List<Model.Log> Logs { get; set; }

        public SearchLogsResponse()
            : base(Guid.Empty)
        {
            Logs = new List<Model.Log>();
        }

        public SearchLogsResponse(Guid requestId)
           : base(requestId)
        {
            Logs = new List<Model.Log>();
        }

        public SearchLogsResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            Logs = new List<Model.Log>();
        }
    }
}