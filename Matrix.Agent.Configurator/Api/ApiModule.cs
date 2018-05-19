using Matrix.Agent.Configuration;
using Matrix.Agent.Configurator.Api.Model;
using Matrix.Agent.Configurator.Business;
using Matrix.Threading;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Configurator.Api
{
    public class ApiModule : NancyModule
    {
        private const string ResponseContentType = "application/json";

        private IConfigurationService Server { get; }

        public ApiModule(IConfigurationService server)
            : base("/api")
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));

            Get["/"] = _ =>
            {
                var response = (Response)JsonConvert.SerializeObject(new { id = AssemblyInfo.Id, name = AssemblyInfo.Name, description = AssemblyInfo.Description, version = AssemblyInfo.Version, build = AssemblyInfo.Build, copyright = AssemblyInfo.Copyright, hash = AssemblyInfo.Hash });

                response.ContentType = ResponseContentType;

                return response;
            };

            Get["/applications/{id}/configuration"] = _ =>
            {
                var apps = Async.Execute<List<KeyValuePair<string, string>>>(() => Server.GetSettings(Guid.Parse(_.id.ToString())));

                var response = (Response)JsonConvert.SerializeObject(apps);

                response.ContentType = ResponseContentType;

                return response;
            };

            Get["/applications/{id}/configuration/{key}"] = _ =>
            {
                var result = Async.Execute<bool>(() => Server.GetSettings(Guid.Parse(_.id.ToString()), _.key.ToString()));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };

            Post["/applications/{id}/configuration"] = _ =>
            {
                var result = false;

                var app = this.BindAndValidate<CreateConfigurationModel>();

                if (app != null)
                    result = Async.Execute(() => Server.UpdateSettings(app.Application, app.Key, app.Value));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };

            Put["/applications/{id}/configuration"] = _ =>
            {
                var result = false;

                var app = this.BindAndValidate<UpdateConfigurationModel>();

                if (app != null)
                    result = Async.Execute(() => Server.UpdateSettings(app.Application, app.Key, app.Value));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };

            Delete["/applications/{id}/configuration/{key}"] = _ =>
            {
                var result = false;

                result = Async.Execute<bool>(() => Server.DeleteSettings(Guid.Parse(_.id.ToString()), _.key.ToString()));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };
        }
    }
}