using System;

namespace Matrix.Messages.Commands
{
    public class SendJabber : Command
    {
        public string Recipient { get; set; }

        public string Text { get; set; }

        public SendJabber()
        {
        }

        public SendJabber(Guid app)
               : base(app)
        {
        }
    }
}