using Autofac;
using Matrix.Agent.Business;
using Matrix.Agent.Registry.Business;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using NLog;
using Quartz;
using System;
using System.IO;

namespace Matrix.Agent.Registry
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private ILogger Logger { get; set; }

        public IHealthService Health { get; }

        public IRegistryService Application { get; }

        private IScheduler Scheduler { get; set; }

        public Bootstrapper(ILogger logger, IScheduler scheduler, IHealthService health, IRegistryService application)
            : base()
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            Health = health ?? throw new ArgumentNullException(nameof(health));
            Application = application ?? throw new ArgumentNullException(nameof(application));

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
            container.Update(builder => builder.Register(i => Health).As<IHealthService>().SingleInstance());
            container.Update(builder => builder.Register(i => Application).As<IRegistryService>().SingleInstance());
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