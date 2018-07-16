using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.ListUserGroupRequest", ExchangeName = "Directory.ListUserGroupRequest")]
    public class ListUserGroupRequest : Request
    {
        public ListUserGroupRequest()
        {
        }

        public ListUserGroupRequest(Guid app)
            : base(app)
        {
        }
    }
}