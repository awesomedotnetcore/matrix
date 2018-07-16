using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.UpdateUserRoleRequest", ExchangeName = "Directory.UpdateUserRoleRequest")]
    public class UpdateUserRoleRequest : Request
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UpdateUserRoleRequest()
        {
        }

        public UpdateUserRoleRequest(Guid app)
            : base(app)
        {
        }
    }
}