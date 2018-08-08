using Matrix.Agent.Business;
using Matrix.Agent.Postman.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Business.Services
{
    public interface IEmailService : IService
    {
        Task<List<EmailRecord>> GetMails(Guid application);

        Task<Guid> SendEmail(Guid application, string from, List<string> to, List<string> cc, List<string> bcc, string subject, string body, bool html);
    }
}