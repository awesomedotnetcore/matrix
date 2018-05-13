using System;

namespace Matrix.Messages.Errors
{
    public class AccessDenied : Error
    {
        public AccessDenied()
        {
        }

        public AccessDenied(Guid app)
            : base(app)
        {
        }
    }
}