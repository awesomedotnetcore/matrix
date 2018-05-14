using System;

namespace Matrix.Messages.Errors
{
    public class RequestTimeout : Error
    {
        public RequestTimeout()
        {
        }

        public RequestTimeout(Guid app)
               : base(app)
        {
        }
    }
}