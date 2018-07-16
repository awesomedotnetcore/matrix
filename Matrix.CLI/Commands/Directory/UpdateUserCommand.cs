using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.Directory
{
    public class UpdateUserCommand : Command<UpdateUserRequest, UpdateUserResponse>
    {
        private CommandArgument App { get; set; }
        private CommandArgument Id { get; set; }

        private CommandArgument FirstName { get; set; }
        private CommandArgument LastName { get; set; }

        private CommandArgument Email { get; set; }
        private CommandArgument Phone { get; set; }

        public UpdateUserCommand()
            : base("update user")
        {
        }

        protected override void PreExecute(UpdateUserRequest request)
        {
            request.Application = Guid.Parse(App.Value);
            request.Id = Guid.Parse(Id.Value);

            request.FirstName = FirstName.Value;
            request.LastName = LastName.Value;

            request.Email = Email.Value;
            request.Phone = Phone.Value;
        }

        public override void Execute(CommandLineApplication cmd)
        {
            Id = cmd.Argument("id", "id of the user");
            App = cmd.Argument("app", "id of the application");

            FirstName = cmd.Argument("fname", "first name of the user");
            LastName = cmd.Argument("lname", "last name of the user");

            Email = cmd.Argument("email", "email of the user");
            Phone = cmd.Argument("phone", "phone of the user");

            base.Execute(cmd);
        }
    }
}