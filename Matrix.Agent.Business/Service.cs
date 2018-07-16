using Newtonsoft.Json;
using NLog;
using System;
using System.Diagnostics;

namespace Matrix.Agent.Business
{
    public class Service : IService
    {
        protected IServiceContext Context { get; }

        protected ILogger Logger { get; }

        public Service(IServiceContext context, ILogger logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected string GetTrace(string status)
        {
            var result = string.Empty;

            var stack = new StackFrame(1);

            var method = stack.GetMethod();

            var name = $"{method.DeclaringType.FullName}.{method.Name}";

            result = JsonConvert.SerializeObject(new { status, name });

            return result;
        }
    }
}