using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Requests
{
    public class GetApplicationByIdRequest : Request
    {
        public Guid Id { get; set; }
    }
}