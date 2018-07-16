using EasyNetQ;
using Matrix.Agent.Directory.Model;
using Matrix.Agent.Messages.Commands.Responses;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.ListUserRoleResponse", ExchangeName = "Directory.ListUserRoleResponse")]
    public class ListUserRoleResponse : Response
    {
        public List<UserRole> UserRoles { get; set; }

        public ListUserRoleResponse()
            : base(Guid.Empty)
        {
            UserRoles = new List<UserRole>();
        }

        public ListUserRoleResponse(Guid requestId)
           : base(requestId)
        {
            UserRoles = new List<UserRole>();
        }

        public ListUserRoleResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            UserRoles = new List<UserRole>();
        }
    }
}