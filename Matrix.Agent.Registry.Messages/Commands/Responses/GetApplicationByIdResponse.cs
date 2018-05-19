using Matrix.Agent.Messages.Commands.Responses;
using Matrix.Agent.Registry.Model;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class GetApplicationByIdResponse : Response
    {
        public Application Application { get; set; }
    }
}