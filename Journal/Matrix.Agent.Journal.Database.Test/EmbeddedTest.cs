using Matrix.Agent.Database;
using Matrix.Threading;
using NLog;
using NUnit.Framework;
using SimpleInjector;
using System;
using System.Configuration;
using System.Linq;

namespace Matrix.Agent.Journal.Database.Test
{
    [TestFixture]
    public class EmbeddedTest
    {
        private Container _container;

        public EmbeddedTest()
        {
            _container = new Container();
            _container.RegisterSingleton<ILogger>(() => { return LogManager.GetLogger(typeof(SqlServerTest).Namespace); });
            _container.RegisterSingleton<IDatabaseContext>(() => new DatabaseContext(ConfigurationManager.AppSettings["matrix.db.url"]));
            _container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
            _container.RegisterSingleton<ILogRepository, LogRepository>();
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

        [Test]
        public void TestLogRepository()
        {
            var app = Async.Execute(() => _container.GetInstance<IApplicationRepository>().GetApplications()).FirstOrDefault(i => i.Name.Equals("Matrix.Agent.Log"));

            Assert.IsNotNull(app);

            var repository = _container.GetInstance<ILogRepository>();

            Assert.IsNotNull(repository);

            var created = Async.Execute(() => repository.CreateLogEntry(app.Id, true, DateTime.Now, "TEST", 0, -1, "TESTING"));

            Assert.IsTrue(created);

            var fetched = Async.Execute(() => repository.GetLogEntries(app.Id, DateTime.Now.Subtract(new TimeSpan(0, 1, 0)), DateTime.Now.AddMinutes(1)));

            Assert.IsNotNull(fetched);

            Assert.IsFalse(fetched.Count.Equals(0));

            var searched = Async.Execute(() => repository.SearchLogEntries(app.Id, DateTime.Now.Subtract(new TimeSpan(0, 1, 0)), DateTime.Now.AddMinutes(1), "TEST"));

            Assert.IsNotNull(searched);

            Assert.IsFalse(searched.Count.Equals(0));

            var deleted = Async.Execute(() => repository.DeleteLogEntries(app.Id, DateTime.Now.Subtract(new TimeSpan(0, 1, 0)), DateTime.Now.AddMinutes(1)));

            Assert.IsTrue(deleted);
        }
    }
}