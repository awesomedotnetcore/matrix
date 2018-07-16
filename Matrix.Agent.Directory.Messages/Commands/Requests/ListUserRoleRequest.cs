using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.ListUserRoleRequest", ExchangeName = "Directory.ListUserRoleRequest")]
    public class ListUserRoleRequest : Request
    {
        public ListUserRoleRequest()
        {
        }

        public ListUserRoleRequest(Guid app)
            : base(app)
        {
        }
    }
}