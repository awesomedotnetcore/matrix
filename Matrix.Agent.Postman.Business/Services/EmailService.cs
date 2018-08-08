using Matrix.Agent.Business;
using Matrix.Agent.Postman.Database.Repositories;
using Matrix.Agent.Postman.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Business.Services
{
    public class EmailService : Service, IEmailService
    {
        public IEmailRepository Repository { get; }

        public EmailService(IServiceContext context, ILogger logger, IEmailRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<EmailRecord>> GetMails(Guid application)
        {
            var result = new List<EmailRecord>();

            return result;
        }

        public async Task<Guid> SendEmail(Guid application, string from, List<string> to, List<string> cc, List<string> bcc, string subject, string body, bool html)
        {
            var result = Guid.Empty;

            result = await Repository.CreateEmail(application, from, to, cc, bcc, subject, body, html, (int)EmailStatus.Pending);

            return result;
        }
    }
}