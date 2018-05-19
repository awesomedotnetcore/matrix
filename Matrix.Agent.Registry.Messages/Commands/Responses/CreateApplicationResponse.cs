using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class CreateApplicationResponse : Response
    {
        public Guid ApplicationId { get; set; }
    }
}