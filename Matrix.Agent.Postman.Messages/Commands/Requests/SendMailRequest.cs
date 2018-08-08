using Matrix.Agent.Messages.Commands.Requests;
using System.Collections.Generic;

namespace Matrix.Agent.Postman.Bus.Commands.Requests
{
    public class SendMailRequest : Request
    {
        public string From { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }

        public List<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool Html { get; set; }

        public SendMailRequest()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
        }
    }
}