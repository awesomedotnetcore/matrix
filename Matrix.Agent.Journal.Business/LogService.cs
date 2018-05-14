using Matrix.Agent.Business;
using Matrix.Agent.Journal.Database;
using Matrix.Agent.Journal.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Business
{
    public class LogService : Service, ILogService
    {
        public ILogRepository Repository { get; }

        public LogService(IServiceContext context, ILogger logger, ILogRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Log>> SearchLogEntries(Guid app, DateTime from, DateTime to, string searchTerm)
        {
            var result = new List<Log>();

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.LogService.SearchLogEntries");

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.SearchLogEntries(app, from, to, searchTerm));
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.LogService.SearchLogEntries");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.LogService.SearchLogEntries");

            return result;
        }

        public async Task<List<Log>> GetLogEntries(Guid app, DateTime from, DateTime to)
        {
            var result = new List<Log>();

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.LogService.GetLogEntries");

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.GetLogEntries(app, from, to));
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.LogService.GetLogEntries");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.LogService.GetLogEntries");

            return result;
        }

        public async Task<bool> CreateLogEntry(Guid app, DateTime timestamp, string source, int level, int @event, string message, Dictionary<string, string> properties = null, List<string> tags = null)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.LogService.CreateLogEntry");

            try
            {
                if (Repository != null)
                    result = await Repository.CreateLogEntry(app, true, timestamp, source, level, @event, message, properties, tags);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.LogService.CreateLogEntry");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.LogService.CreateLogEntry");

            return result;
        }

        public async Task<bool> DeleteLogEntries(Guid app, DateTime from, DateTime to)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.LogService.DeleteLogEntries");

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteLogEntries(app, from, to);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.LogService.DeleteLogEntries");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.LogService.DeleteLogEntries");

            return result;
        }
    }
}