using System;

namespace Matrix.Messages.Commands.Requests
{
    public class GetConfigurationRequest : Request
    {
        public string Key { get; set; }

        public GetConfigurationRequest()
        {
        }

        public GetConfigurationRequest(Guid app)
            : base(app)
        {
        }
    }
}