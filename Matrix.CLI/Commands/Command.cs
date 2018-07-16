using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using Matrix.Agent.Messages.Commands.Responses;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using NLog;
using System;
using System.Configuration;

namespace Matrix.CLI.Commands
{
    public abstract class Command<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response
    {
        protected ILogger Logger { get; }

        protected string CommandDescription { get; }

        protected bool Verbose { get; private set; }

        protected bool PrettyPrint { get; private set; }

        public Command(string description)
        {
            Logger = LogManager.GetCurrentClassLogger();

            CommandDescription = description ?? throw new ArgumentNullException(nameof(description));
        }

        public virtual void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = CommandDescription;

            var pretty = cmd.Option("--pretty | -p", "pretty print json", CommandOptionType.NoValue);
            var verbose = cmd.Option("--verbose | -v", "verbose execution", CommandOptionType.NoValue);

            cmd.OnExecute(() =>
            {
                PrettyPrint = pretty.HasValue();
                Verbose = verbose.HasValue();

                try
                {
                    using (var bus = RabbitHutch.CreateBus(ConfigurationManager.AppSettings["matrix.bus.url"]))
                    {
                        var request = Activator.CreateInstance<TRequest>();

                        PreExecute(request);

                        if (Verbose)
                            Print(request);

                        TResponse response = default(TResponse);

                        response = bus.Request<TRequest, TResponse>(request);

                        PostExecute(request, response);

                        if (response != null)
                            Print(response);
                        else
                            throw new Exception("response is null");
                    }
                }
                catch (Exception e)
                {
                    Print(new { error = e.Message });
                }

                return 0;
            });
        }

        protected virtual void PreExecute(TRequest request)
        {
        }

        protected virtual void PostExecute(TRequest request, TResponse response)
        {
        }

        //protected abstract void Execute(TRequest request, TResponse response);

        private void Print(object o)
        {
            Console.WriteLine(JsonConvert.SerializeObject(o, PrettyPrint ? Formatting.Indented : Formatting.None));
        }
    }
}