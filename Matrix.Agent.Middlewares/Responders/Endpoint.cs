using NLog;
using System;

namespace Matrix.Agent.Middlewares.Responders
{
    public class Endpoint : IEndpoint
    {
        protected ILogger Logger { get; }

        public Endpoint(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}