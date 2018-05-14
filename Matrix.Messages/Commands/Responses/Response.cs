using System;

namespace Matrix.Messages.Commands.Responses
{
    public class Response : Command, IResponse
    {
        public int Status { get; set; }

        public bool Error { get; set; }

        public Response()
        {
        }

        public Response(Guid app)
            : base(app)
        {
        }
    }
}