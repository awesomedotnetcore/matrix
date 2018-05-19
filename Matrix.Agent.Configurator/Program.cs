using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Configurator.Bus;
using Matrix.Agent.Configurator.Business;
using Matrix.Agent.Configurator.Database;
using Matrix.Agent.Database;
using Matrix.Agent.Host;
using Matrix.Agent.Jobs;
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

namespace Matrix.Agent.Configurator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var container = new Container();
                var config = new Settings("configuration");

                container.RegisterSingleton<IConfiguration>(() => { return config; });
                container.RegisterSingleton<ILogger>(() => { return LogManager.GetLogger(typeof(Program).Namespace); });
                container.RegisterSingleton(() =>
                {
                    var scheduler = Async.Execute(() => StdSchedulerFactory.GetDefaultScheduler());
                    scheduler.JobFactory = new JobFactory(container);
                    return scheduler;
                });

                container.RegisterSingleton<IMiddlewareContext>(() => new MiddlewareContext(config.Bus.Url));
                container.RegisterSingleton<IMiddleware, RabbitMiddleware>();
                container.RegisterSingleton<IDatabaseContext>(() => new DatabaseContext(config.Database.Url));
                container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
                container.RegisterSingleton<IHealthRepository, HealthRepository>();
                container.RegisterSingleton<IConfigurationRepository, ConfigurationRepository>();
                container.RegisterSingleton<IServiceContext>(() => new ServiceContext());
                container.RegisterSingleton<IHealthService, HealthService>();
                container.RegisterSingleton<IApplicationService, ApplicationService>();
                container.RegisterSingleton<IConfigurationService, ConfigurationService>();
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
                    host.SetDescription("Matrix Platform Configuration");
                    host.SetDisplayName("Matrix Configuration");
                    host.SetServiceName("Matrix.Configuration");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}