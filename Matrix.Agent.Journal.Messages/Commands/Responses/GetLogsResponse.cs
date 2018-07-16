using Matrix.Agent.Messages.Commands.Responses;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Journal.Messages.Commands.Responses
{
    public class GetLogsResponse : Response
    {
        public List<Model.Log> Logs { get; set; }

        public GetLogsResponse()
            : base(Guid.Empty)
        {
            Logs = new List<Model.Log>();
        }

        public GetLogsResponse(Guid requestId)
           : base(requestId)
        {
            Logs = new List<Model.Log>();
        }

        public GetLogsResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            Logs = new List<Model.Log>();
        }
    }
}