using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Host;
using Matrix.Agent.Jobs;
using Matrix.Agent.Journal.Bus.MSMQ;
using Matrix.Agent.Journal.Bus.RabbitMQ;
using Matrix.Agent.Journal.Business;
using Matrix.Agent.Journal.Database;
using Matrix.Agent.Middlewares;
using Matrix.Threading;
using Nancy.Bootstrapper;
using NLog;
using Quartz.Impl;
using SimpleInjector;
using System;
using Topshelf;
using Topshelf.Nancy;
using Topshelf.SimpleInjector;

namespace Matrix.Agent.Journal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var config = new Settings("journal");
                var container = new Container();
                container.RegisterSingleton<IConfiguration>(() => { return config; });
                container.RegisterSingleton<ILogger>(() => { return LogManager.GetLogger(typeof(Program).Namespace); });
                container.RegisterSingleton(() =>
                {
                    var scheduler = Async.Execute(() => StdSchedulerFactory.GetDefaultScheduler());
                    scheduler.JobFactory = new JobFactory(container);
                    return scheduler;
                });

                if (config.Bus.Enabled)
                {
                    container.RegisterSingleton<IMiddlewareContext>(() => new MiddlewareContext(config.Bus.Url));

                    if (config.Bus.Type.Equals(MiddlewareType.RabbitMQ.ToString(), StringComparison.CurrentCultureIgnoreCase))
                        container.RegisterSingleton<IMiddleware, RabbitMiddleware>();

                    if (config.Bus.Type.Equals(MiddlewareType.MSMQ.ToString(), StringComparison.CurrentCultureIgnoreCase))
                        container.RegisterSingleton<IMiddleware, MsmqMiddleware>();
                }

                if (config.Database.Enabled)
                {
                    container.RegisterSingleton<IDatabaseContext>(() => new DatabaseContext(config.Database.Url));
                    container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
                    container.RegisterSingleton<IHealthRepository, HealthRepository>();
                    container.RegisterSingleton<ILogRepository, LogRepository>();
                }

                container.RegisterSingleton<IServiceContext>(() => new ServiceContext());
                container.RegisterSingleton<IApplicationService, ApplicationService>();
                container.RegisterSingleton<IHealthService, HealthService>();
                container.RegisterSingleton<ILogService, LogService>();
                container.RegisterSingleton<Pulse>();

                if (config.Web.Enabled)
                    container.RegisterSingleton<INancyBootstrapper, Bootstrapper>();

                container.Register<IHost, Host>();
                container.Verify();

                HostFactory.Run(host =>
                {
                    host.UseSimpleInjector(container);

                    host.OnException((Exception e) => { container.GetInstance<ILogger>().Error(e, e.ToString()); });

                    host.Service<IHost>(server =>
                    {
                        server.ConstructUsingSimpleInjector();
                        server.WhenStarted(async i => await i.Start());
                        server.WhenStopped(async i => await i.Stop());

                        if (config.Web.Enabled)
                        {
                            server.WithNancyEndpoint(host, web =>
                            {
                                web.AddHost(config.Web.Scheme, config.Web.Server, config.Web.Port, "/");
                                web.Bootstrapper = container.GetInstance<INancyBootstrapper>();
                            });
                        }
                    });

                    host.UseNLog();
                    host.RunAsNetworkService();
                    host.SetDescription("Matrix Platform Journal");
                    host.SetDisplayName("Matrix Journal");
                    host.SetServiceName("Matrix.Journal");
                });
            }
            catch (Exception e)
            {
            }
        }
    }
}