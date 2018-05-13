using System;
using System.Collections.Generic;

namespace Matrix.Messages.Commands
{
    public class SendEmail : Command
    {
        public string From { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }

        public List<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool HTML { get; set; }

        public SendEmail()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
            HTML = true;
        }

        public SendEmail(Guid app)
            : base(app)
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
            HTML = true;
        }
    }
}