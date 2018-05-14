using Matrix.Agent.Registry.Model;
using Matrix.Messages.Commands.Responses;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class GetApplicationByIdResponse : Response
    {
        public Application Application { get; set; }
    }
}