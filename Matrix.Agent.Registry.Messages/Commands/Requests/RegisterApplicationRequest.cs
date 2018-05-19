using Matrix.Agent.Messages.Commands.Requests;

namespace Matrix.Agent.Registry.Messages.Commands.Requests
{
    public class RegisterApplicationRequest : Request
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}