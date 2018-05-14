using Matrix.Agent.Registry.Api.Model;
using Matrix.Agent.Registry.Business;
using Matrix.Threading;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;

namespace Matrix.Agent.Registry.Api
{
    public class ApiModule : NancyModule
    {
        private const string ResponseContentType = "application/json";

        private IRegistryService Server { get; }

        public ApiModule(IRegistryService server)
            : base("/api")
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));

            Get["/"] = _ =>
            {
                var response = (Response)JsonConvert.SerializeObject(new { id = AssemblyInfo.Id, name = AssemblyInfo.Name, description = AssemblyInfo.Description, version = AssemblyInfo.Version, build = AssemblyInfo.Build, copyright = AssemblyInfo.Copyright, hash = AssemblyInfo.Hash });

                response.ContentType = ResponseContentType;

                return response;
            };

            Get["/apps"] = _ =>
            {
                var apps = Async.Execute(() => Server.GetApplications());

                var response = (Response)JsonConvert.SerializeObject(apps);

                response.ContentType = ResponseContentType;

                return response;
            };

            Get["/unregister/{id}"] = _ =>
            {
                var result = Async.Execute<bool>(() => Server.Delete(_.id));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };

            Post["/register"] = _ =>
            {
                var result = Guid.Empty;

                var app = this.BindAndValidate<RegisterApplicationModel>();

                if (app != null)
                    result = Async.Execute(() => Server.Register(app.Name, app.Description));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };

            Post["/apps"] = _ =>
            {
                var result = false;

                var app = this.BindAndValidate<UpdateApplicationModel>();

                if (app != null)
                    result = Async.Execute(() => Server.Update(app.Id, app.Name, app.Description));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };
        }
    }
}