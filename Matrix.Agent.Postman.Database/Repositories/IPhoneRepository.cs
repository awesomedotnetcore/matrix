using Matrix.Agent.Database;
using Matrix.Agent.Postman.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Database.Repositories
{
    public interface IPhoneRepository : IRepository
    {
        Task<List<PhoneMessage>> GetPhoneMessagesByStatus(Guid application, int status);

        Task<PhoneMessage> GetPhoneMessageById(Guid id);

        Task<Guid> CreatePhoneMessage(Guid application, string from, List<string> to, string message, int status);

        Task<bool> UpdatePhoneMessage(Guid id, int status);

        Task<bool> DeletePhoneMessageById(Guid id);

        Task<bool> DeletePhoneMessagesByStatus(Guid application, int status);
    }
}