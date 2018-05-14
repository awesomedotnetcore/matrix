using System;

namespace Matrix.Messages.Commands.Responses
{
    public class GetConfigurationResponse : Response
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public GetConfigurationResponse()
        {
        }

        public GetConfigurationResponse(Guid app)
            : base(app)
        {
        }
    }
}