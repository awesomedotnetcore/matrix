using EasyNetQ;
using Matrix.Agent.Directory.Model;
using Matrix.Agent.Messages.Commands.Responses;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.ListUserGroupResponse", ExchangeName = "Directory.ListUserGroupResponse")]
    public class ListUserGroupResponse : Response
    {
        public List<UserGroup> UserGroups { get; set; }

        public ListUserGroupResponse()
            : base(Guid.Empty)
        {
            UserGroups = new List<UserGroup>();
        }

        public ListUserGroupResponse(Guid requestId)
           : base(requestId)
        {
            UserGroups = new List<UserGroup>();
        }

        public ListUserGroupResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            UserGroups = new List<UserGroup>();
        }
    }
}