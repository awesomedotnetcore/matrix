using System;

namespace Matrix.Messages.Errors
{
    public class Error : Message, IError
    {
        public int Code { get; set; }

        public Exception Exception { get; set; }

        public Error()
        {
        }

        public Error(Guid app)
            : base(app)
        {
        }
    }
}