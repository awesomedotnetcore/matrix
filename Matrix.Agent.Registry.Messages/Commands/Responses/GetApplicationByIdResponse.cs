using Matrix.Agent.Messages.Commands.Responses;
using Matrix.Agent.Registry.Model;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class GetApplicationByIdResponse : Response
    {
        public Application Application { get; set; }

        public GetApplicationByIdResponse()
            : base(Guid.Empty)
        {
        }

        public GetApplicationByIdResponse(Guid requestId)
            : base(requestId)
        {
        }

        public GetApplicationByIdResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}