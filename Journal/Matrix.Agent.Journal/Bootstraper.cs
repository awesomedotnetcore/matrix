using Autofac;
using Matrix.Agent.Journal.Business.Interfaces;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using NLog;
using Quartz;
using System;
using System.IO;

namespace Matrix.Agent.Journal
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private ILogger Logger { get; set; }

        private IApplicationService Application { get; }

        private ILogService Server { get; set; }

        private IScheduler Scheduler { get; set; }

        public Bootstrapper(ILogger logger, IScheduler scheduler, IApplicationService application, ILogService server)
            : base()
        {
            Logger = logger;
            Scheduler = scheduler;
            Application = application;
            Server = server;

            ApplicationPipelines.OnError += OnError;
        }

        protected override void ConfigureConventions(NancyConventions o)
        {
            base.ConfigureConventions(o);

            o.StaticContentsConventions.AddDirectory("/", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/Views/Web"));
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder => builder.Register(i => Logger).As<ILogger>().SingleInstance());
            container.Update(builder => builder.Register(i => Scheduler).As<IScheduler>().SingleInstance());
            container.Update(builder => builder.Register(i => Application).As<IApplicationService>().SingleInstance());
            container.Update(builder => builder.Register(i => Server).As<ILogService>().SingleInstance());
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response
                   .WithHeader("Access-Control-Allow-Origin", "http://localhost:4200")
                   .WithHeader("Access-Control-Allow-Methods", "GET,POST")
                   .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
        }

        private dynamic OnError(NancyContext context, Exception e)
        {
            Logger.Error(e, e.Message);

            return false;
        }
    }
}