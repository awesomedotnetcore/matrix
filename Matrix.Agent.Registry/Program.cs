using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Host;
using Matrix.Agent.Jobs;
using Matrix.Agent.Middlewares;
using Matrix.Agent.Registry.Bus;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Database;
using Matrix.Threading;
using Nancy.Bootstrapper;
using NLog;
using Quartz.Impl;
using SimpleInjector;
using System;
using System.Linq;
using Topshelf;
using Topshelf.Nancy;
using Topshelf.SimpleInjector;

namespace Matrix.Agent.Registry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var config = new Settings("registry");

                var container = new Container();
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
                container.RegisterSingleton<IHealthRepository, HealthRepository>();
                container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
                container.RegisterSingleton<IServiceContext, ServiceContext>();
                container.RegisterSingleton<IHealthService, HealthService>();
                container.RegisterSingleton<IRegistryService, RegistryService>();
                container.RegisterSingleton<Pulse>();
                container.RegisterSingleton<IHost, Host>();

                if (config.Web.Enabled)
                    container.RegisterSingleton<INancyBootstrapper, Bootstrapper>();

                container.Verify();

                Bootstrap(container);

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
                    host.SetDescription("Matrix Platform Registry");
                    host.SetDisplayName("Matrix Registry");
                    host.SetServiceName("Matrix.Registry");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Bootstrap(Container container)
        {
            var server = container.GetInstance<IRegistryService>();

            if (server != null)
            {
                var matrix = Async.Execute(() => server.GetApplications()).FirstOrDefault(i => i.Name.Equals("Matrix"));

                if (matrix == null)
                    Async.Execute(() => server.Register("Matrix", "Matrix Platform"));
            }
        }
    }
}