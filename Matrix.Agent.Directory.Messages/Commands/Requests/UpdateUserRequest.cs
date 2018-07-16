using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.UpdateUserRequest", ExchangeName = "Directory.UpdateUserRequest")]
    public class UpdateUserRequest : Request
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<Guid> Roles { get; set; }

        public List<Guid> Groups { get; set; }

        public UpdateUserRequest()
        {
            Roles = new List<Guid>();
            Groups = new List<Guid>();
        }

        public UpdateUserRequest(Guid app)
            : base(app)
        {
            Roles = new List<Guid>();
            Groups = new List<Guid>();
        }
    }
}