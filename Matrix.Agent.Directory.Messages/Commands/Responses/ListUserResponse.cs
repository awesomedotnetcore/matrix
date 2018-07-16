using EasyNetQ;
using Matrix.Agent.Directory.Model;
using Matrix.Agent.Messages.Commands.Responses;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.ListUserResponse", ExchangeName = "Directory.ListUserResponse")]
    public class ListUserResponse : Response
    {
        public List<User> Users { get; set; }

        public ListUserResponse()
            : base(Guid.Empty)
        {
            Users = new List<User>();
        }

        public ListUserResponse(Guid requestId)
           : base(requestId)
        {
            Users = new List<User>();
        }

        public ListUserResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            Users = new List<User>();
        }
    }
}