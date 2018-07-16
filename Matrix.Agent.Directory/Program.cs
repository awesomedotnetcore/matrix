using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Directory.Bus;
using Matrix.Agent.Directory.Business.Services;
using Matrix.Agent.Directory.Database.Repositories;
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

namespace Matrix.Agent.Directory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var config = new Settings("directory");

                var container = new Container();
                container.RegisterSingleton<IConfiguration>(() => { return config; });
                container.RegisterSingleton<ILogger>(() => { return LogManager.GetLogger(typeof(Program).Namespace); });
                container.RegisterSingleton(() =>
                {
                    var scheduler = Async.Execute(() => StdSchedulerFactory.GetDefaultScheduler());
                    scheduler.JobFactory = new JobFactory(container);
                    return scheduler;
                });

                container.RegisterSingleton<IDatabaseContext>(() => new DatabaseContext(config.Database.Url));
                container.RegisterSingleton<IHealthRepository, HealthRepository>();
                container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
                container.RegisterSingleton<IUserRepository, UserRepository>();
                container.RegisterSingleton<IUserGroupRepository, UserGroupRepository>();
                container.RegisterSingleton<IUserRoleRepository, UserRoleRepository>();
                container.RegisterSingleton<IServiceContext, ServiceContext>();
                container.RegisterSingleton<IHealthService, HealthService>();
                container.RegisterSingleton<IApplicationService, ApplicationService>();
                container.RegisterSingleton<IUserService, UserService>();
                container.RegisterSingleton<IUserGroupService, UserGroupService>();
                container.RegisterSingleton<IUserRoleService, UserRoleService>();
                container.RegisterSingleton<IMiddlewareContext>(() => new MiddlewareContext(config.Bus.Url));
                container.RegisterSingleton<IMiddleware, RabbitMiddleware>();
                container.RegisterSingleton<Pulse>();
                container.RegisterSingleton<IHost, Host>();

                if (config.Web.Enabled)
                    container.RegisterSingleton<INancyBootstrapper, Bootstrapper>();

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
                    host.SetDescription("Matrix Platform Directory");
                    host.SetDisplayName("Matrix Directory");
                    host.SetServiceName("Matrix.Directory");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}