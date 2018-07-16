using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Requests
{
    public class UpdateApplicationRequest : Request
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}