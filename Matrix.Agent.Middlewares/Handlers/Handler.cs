using NLog;
using System;

namespace Matrix.Agent.Middlewares.Handlers
{
    public class Handler : IHandler
    {
        protected ILogger Logger { get; }

        public Handler(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}