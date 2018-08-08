using Matrix.Agent.Messages.Commands.Requests;
using System.Collections.Generic;

namespace Matrix.Agent.Postman.Bus.Commands.Requests
{
    public class SendMessageRequest : Request
    {
        public string From { get; set; }

        public List<string> To { get; set; }

        public string Message { get; set; }

        public SendMessageRequest()
        {
            To = new List<string>();
        }
    }
}