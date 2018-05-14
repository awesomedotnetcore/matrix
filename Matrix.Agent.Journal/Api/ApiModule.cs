using Matrix.Agent.Journal.Business;
using Matrix.Agent.Journal.Model;
using Matrix.Threading;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Journal.Api
{
    public class ApiModule : NancyModule
    {
        private ILogService Server { get; set; }

        private IApplicationService Application { get; }

        public ApiModule(ILogService server, IApplicationService application)
            : base("/api")
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));

            Application = application ?? throw new ArgumentNullException(nameof(application));

            Get["/"] = _ =>
            {
                var response = (Response)JsonConvert.SerializeObject(new { id = AssemblyInfo.Id, name = AssemblyInfo.Name, description = AssemblyInfo.Description, version = AssemblyInfo.Version, build = AssemblyInfo.Build, copyright = AssemblyInfo.Copyright, hash = AssemblyInfo.Hash });

                response.ContentType = "application/json";

                return response;
            };

            Get["/apps"] = _ =>
            {
                var apps = Async.Execute(() => Application.GetApplications());

                var response = (Response)JsonConvert.SerializeObject(apps);

                response.ContentType = "application/json";

                return response;
            };

            Get["/apps/{app}/logs"] = _ =>
            {
                var logs = Async.Execute<List<Log>>(() => Server.GetLogEntries(_.app, DateTime.Now.Subtract(new TimeSpan(1, 0, 0)), DateTime.Now));

                var response = (Response)JsonConvert.SerializeObject(logs);

                response.ContentType = "application/json";

                return response;
            };

            Get["/apps/{app}/logs/{startDate}/{endDate}"] = _ =>
            {
                var start = DateTime.ParseExact(_.startDate, "yyyyMMddTHHmmss", null);

                var end = DateTime.ParseExact(_.endDate, "yyyyMMddTHHmmss", null);

                var logs = Async.Execute<List<Log>>(() => Server.GetLogEntries(_.app, start, end));

                var response = (Response)JsonConvert.SerializeObject(logs);

                response.ContentType = "application/json";

                return response;
            };

            Get["/apps/{app}/logs/{startDate}/{endDate}/{searchTerm}"] = _ =>
            {
                var start = DateTime.ParseExact(_.startDate, "yyyyMMddTHHmmss", null);

                var end = DateTime.ParseExact(_.endDate, "yyyyMMddTHHmmss", null);

                var logs = Async.Execute<List<Log>>(() => Server.SearchLogEntries(_.app, start, end, _.searchTerm));

                var response = (Response)JsonConvert.SerializeObject(logs);

                response.ContentType = "application/json";

                return response;
            };
        }
    }
}