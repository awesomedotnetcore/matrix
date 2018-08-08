using Matrix.Agent.Business;
using Matrix.Agent.Postman.Database.Repositories;
using Matrix.Agent.Postman.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Business.Services
{
    public class PhoneService : Service, IPhoneService
    {
        public IPhoneRepository Repository { get; }

        public PhoneService(IServiceContext context, ILogger logger, IPhoneRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<MessageRecord>> GetMessages(Guid application)
        {
            var result = new List<MessageRecord>();

            return result;
        }

        public async Task<Guid> SendMessage(Guid application, string from, List<string> to, string message)
        {
            var result = Guid.Empty;

            result = await Repository.CreatePhoneMessage(application, from, to, message, (int)MessageStatus.Pending);

            return result;
        }
    }
}