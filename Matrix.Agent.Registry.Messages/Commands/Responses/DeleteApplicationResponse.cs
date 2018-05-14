using Matrix.Messages.Commands.Responses;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class DeleteApplicationResponse : Response
    {
        public bool Deleted { get; set; }
    }
}