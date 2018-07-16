using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class CreateUserCommand : Command<CreateUserRequest, CreateUserResponse>
    {
        private CommandArgument App { get; set; }

        private CommandArgument FirstName { get; set; }
        private CommandArgument LastName { get; set; }

        private CommandArgument Username { get; set; }
        private CommandArgument Password { get; set; }

        private CommandArgument Email { get; set; }
        private CommandArgument Phone { get; set; }

        public CreateUserCommand()
            : base("create user")
        {
        }

        protected override void PreExecute(CreateUserRequest request)
        {
            request.Application = Guid.Parse(App.Value);

            request.FirstName = FirstName.Value;
            request.LastName = LastName.Value;

            request.Username = Username.Value;
            request.Password = Password.Value;

            request.Email = Email.Value;
            request.Phone = Phone.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            App = cmd.Argument("app", "id of the application");

            FirstName = cmd.Argument("fname", "first name of the user");
            LastName = cmd.Argument("lname", "last name of the user");

            Username = cmd.Argument("username", "username of the user");
            Password = cmd.Argument("password", "password of the user");

            Email = cmd.Argument("email", "email of the user");
            Phone = cmd.Argument("phone", "phone of the user");

            base.Execute(cmd);
        }
    }
}