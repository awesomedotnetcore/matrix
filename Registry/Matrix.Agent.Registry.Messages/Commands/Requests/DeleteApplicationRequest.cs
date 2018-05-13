using Matrix.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Requests
{
    public class DeleteApplicationRequest : Request
    {
        public Guid ApplicationId { get; set; }
    }
}