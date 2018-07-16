using Matrix.Agent.Messages.Commands.Responses;
using Matrix.Agent.Registry.Model;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class ListApplicationResponse : Response
    {
        public List<Application> Applications { get; set; }

        public ListApplicationResponse()
            : base(Guid.Empty)
        {
            Applications = new List<Application>();
        }

        public ListApplicationResponse(Guid requestId)
            : base(requestId)
        {
            Applications = new List<Application>();
        }

        public ListApplicationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            Applications = new List<Application>();
        }
    }
}