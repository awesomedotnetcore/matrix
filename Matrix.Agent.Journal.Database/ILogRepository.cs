using Matrix.Agent.Database;
using Matrix.Agent.Journal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Database
{
    public interface ILogRepository : IRepository
    {
        Task<List<Log>> SearchLogEntries(Guid app, DateTime from, DateTime to, string searchTerm);

        Task<List<Log>> GetLogEntries(Guid app, DateTime from, DateTime to);

        Task<bool> CreateLogEntry(Guid app, bool enabled, DateTime timestamp, string source, int level, int @event, string message, Dictionary<string, string> properties = null, List<string> tags = null);

        Task<bool> DeleteLogEntries(Guid app, DateTime from, DateTime to);
    }
}