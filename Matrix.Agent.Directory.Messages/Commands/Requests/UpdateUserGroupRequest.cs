using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.UpdateUserGroupRequest", ExchangeName = "Directory.UpdateUserGroupRequest")]
    public class UpdateUserGroupRequest : Request
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UpdateUserGroupRequest()
        {
        }

        public UpdateUserGroupRequest(Guid app)
            : base(app)
        {
        }
    }
}