using Matrix.Agent.Database;
using Matrix.Threading;
using NLog;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Configuration;

namespace Matrix.Agent.Registry.Database.Test
{
    [TestFixture]
    public class SqlServerTest
    {
        private Container _container;

        public SqlServerTest()
        {
            _container = new Container();
            _container.RegisterSingleton<ILogger>(() => { return LogManager.GetLogger(typeof(SqlServerTest).Namespace); });
            _container.RegisterSingleton<IDatabaseContext>(() => new DatabaseContext(ConfigurationManager.AppSettings["matrix.db.url"]));
            _container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
        }

        [Test]
        public void TestApplicationRepository()
        {
            var repository = _container.GetInstance<IApplicationRepository>();

            Assert.IsNotNull(repository);

            var id = Guid.NewGuid();

            var created = Async.Execute(() => repository.CreateApplication(id, "TEST", "TESTING"));

            Assert.IsTrue(created);

            var updated = Async.Execute(() => repository.UpdateApplication(id, "TEST_UPDATED", "TESTING_UPDATED"));

            Assert.IsTrue(updated);

            var fetched = Async.Execute(() => repository.GetApplications());

            Assert.IsNotNull(fetched);

            Assert.IsFalse(fetched.Count.Equals(0));

            var deleted = Async.Execute(() => repository.DeleteApplication(id));

            Assert.IsTrue(deleted);
        }
    }
}