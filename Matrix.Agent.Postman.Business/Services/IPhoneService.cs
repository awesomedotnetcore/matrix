using Matrix.Agent.Business;
using Matrix.Agent.Postman.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Business.Services
{
    public interface IPhoneService : IService
    {
        Task<List<MessageRecord>> GetMessages(Guid application);

        Task<Guid> SendMessage(Guid application, string from, List<string> to, string message);
    }
}