using System;

namespace Matrix.Messages
{
    public class Log : Message
    {
        public string Source { get; set; }

        public int Event { get; set; }

        public int Level { get; set; }

        public string Message { get; set; }

        public Log(Guid app)
            : base(app)
        {
        }
    }
}