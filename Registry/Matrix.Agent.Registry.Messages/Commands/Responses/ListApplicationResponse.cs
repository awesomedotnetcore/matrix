using Matrix.Agent.Registry.Model;
using Matrix.Messages.Commands.Responses;
using System.Collections.Generic;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class ListApplicationResponse : Response
    {
        public List<Application> Applications { get; set; }

        public ListApplicationResponse()
        {
            Applications = new List<Application>();
        }
    }
}