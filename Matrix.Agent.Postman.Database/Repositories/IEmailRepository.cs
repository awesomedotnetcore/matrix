using Matrix.Agent.Database;
using Matrix.Agent.Postman.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Database.Repositories
{
    public interface IEmailRepository : IRepository
    {
        Task<List<Email>> GetEmailByStatus(Guid application, int status);

        Task<Email> GetEmailById(Guid id);

        Task<Guid> CreateEmail(Guid application, string from, List<string> to, List<string> cc, List<string> bcc, string subject, string body, bool html, int status);

        Task<bool> UpdateEmail(Guid id, int status);

        Task<bool> DeleteEmailById(Guid id);

        Task<bool> DeleteEmailByStatus(Guid application, int status);
    }
}