using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.CreateUserRequest", ExchangeName = "Directory.CreateUserRequest")]
    public class CreateUserRequest : Request
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<Guid> Roles { get; set; }

        public List<Guid> Groups { get; set; }

        public CreateUserRequest()
        {
            Roles = new List<Guid>();
            Groups = new List<Guid>();
        }

        public CreateUserRequest(Guid app)
            : base(app)
        {
            Roles = new List<Guid>();
            Groups = new List<Guid>();
        }
    }
}