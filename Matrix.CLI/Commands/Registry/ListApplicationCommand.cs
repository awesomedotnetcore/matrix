using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;

namespace Matrix.CLI.Commands.Registry
{
    public class ListApplicationCommand : Command<ListApplicationRequest, ListApplicationResponse>
    {
        public ListApplicationCommand()
            : base("lists applications registered in the registry")
        {
        }
    }
}