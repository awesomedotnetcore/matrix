using Matrix.Messages.Commands.Responses;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class UpdateApplicationResponse : Response
    {
        public bool Updated { get; set; }
    }
}