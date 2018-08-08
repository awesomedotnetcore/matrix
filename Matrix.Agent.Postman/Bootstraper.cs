using Autofac;
using Matrix.Agent.Business;
using Matrix.Agent.Postman.Business.Services;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using NLog;
using Quartz;
using System;
using System.IO;

namespace Matrix.Agent.Postman
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private ILogger Logger { get; set; }

        private IHealthService Health { get; }

        private IEmailService Mail { get; }

        private IPhoneService Phone { get; }

        private IScheduler Scheduler { get; set; }

        public Bootstrapper(ILogger logger, IScheduler scheduler, IHealthService health, IEmailService mail, IPhoneService phone)
            : base()
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            Health = health ?? throw new ArgumentNullException(nameof(health));
            Mail = mail ?? throw new ArgumentNullException(nameof(mail));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));

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
            container.Update(builder => builder.Register(i => Mail).As<IEmailService>().SingleInstance());
            container.Update(builder => builder.Register(i => Phone).As<IPhoneService>().SingleInstance());
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