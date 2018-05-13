using System;

namespace Matrix.Messages.Commands
{
    public class SendSlack : Command
    {
        public string User { get; set; }

        public string Channel { get; set; }

        public string Text { get; set; }

        public SendSlack()
        {
        }

        public SendSlack(Guid app)
            : base(app)
        {
        }
    }
}