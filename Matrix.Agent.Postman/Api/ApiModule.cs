using Matrix.Agent.Postman.Business.Services;
using Matrix.Agent.Postman.Model;
using Matrix.Threading;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Postman.Api
{
    public class ApiModule : NancyModule
    {
        private const string ResponseContentType = "application/json";

        private IEmailService Mail { get; }

        private IPhoneService Phone { get; }

        public ApiModule(IEmailService mail, IPhoneService phone)
            : base("/api")
        {
            Mail = mail ?? throw new ArgumentNullException(nameof(mail));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));

            Get["/"] = _ =>
            {
                var response = (Response)JsonConvert.SerializeObject(new { id = AssemblyInfo.Id, name = AssemblyInfo.Name, description = AssemblyInfo.Description, version = AssemblyInfo.Version, build = AssemblyInfo.Build, copyright = AssemblyInfo.Copyright, hash = AssemblyInfo.Hash });

                response.ContentType = ResponseContentType;

                return response;
            };

            Get["/applications/{id}/mails"] = _ =>
            {
                var mails = Async.Execute<List<EmailRecord>>(() => Mail.GetMails(Guid.Parse(_.id.ToString())));

                var response = (Response)JsonConvert.SerializeObject(mails);

                response.ContentType = ResponseContentType;

                return response;
            };

            Get["/applications/{id}/messages"] = _ =>
            {
                var result = Async.Execute<List<MessageRecord>>(() => Phone.GetMessages(Guid.Parse(_.id.ToString())));

                var response = (Response)JsonConvert.SerializeObject(result);

                response.ContentType = ResponseContentType;

                return response;
            };
        }
    }
}