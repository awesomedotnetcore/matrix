using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Journal.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Database
{
    public class LogRepository : Repository, ILogRepository
    {
        public LogRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<List<Log>> SearchLogEntries(Guid app, DateTime from, DateTime to, string searchTerm)
        {
            var result = new List<Log>();

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.SqlServer.LogRepository.SearchLogEntries");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [Enabled], [Timestamp], [Source], [Level], [Event], [Message] FROM [dbo].[Logs] WHERE [Application] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to AND [Message] LIKE '%" + searchTerm + "%'", new { @app = app, @from = from, @to = to });

                    foreach (var i in entity)
                    {
                        var o = new Log();

                        o.Id = i.Id;
                        o.Application = Guid.Parse(i.Application.ToString());
                        o.Enabled = i.Enabled;
                        o.Timestamp = i.Timestamp;
                        o.Source = i.Source;
                        o.Level = i.Level;
                        o.Event = i.Event;
                        o.Message = i.Message;

                        foreach (dynamic property in await connection.QueryAsync<dynamic>("SELECT [Key], [Value] FROM [dbo].[LogProperty] WHERE [LogEntryId] = @log", new { @log = i.Id }))
                            o.Properties.Add(property.Key, property.Value);

                        foreach (dynamic tag in await connection.QueryAsync<dynamic>("SELECT [Value] FROM [dbo].[LogTag] WHERE [LogEntryId] = @log", new { @log = i.Id }))
                            o.Tags.Add(tag.Value);

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.SqlServer.LogRepository.SearchLogEntries");
                Logger.Error(e);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            Logger.Trace("END | Matrix.Server.Log.Database.SqlServer.LogRepository.SearchLogEntries");

            return result;
        }

        public async Task<List<Log>> GetLogEntries(Guid app, DateTime from, DateTime to)
        {
            var result = new List<Log>();

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.SqlServer.LogRepository.GetLogEntries");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [Enabled], [Timestamp], [Source], [Level], [Event], [Message] FROM [dbo].[Logs] WHERE [Application] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to", new { @app = app, @from = from, @to = to });

                    foreach (var i in entity)
                    {
                        var o = new Log();

                        o.Id = i.Id;
                        o.Application = Guid.Parse(i.Application.ToString());
                        o.Enabled = i.Enabled;
                        o.Timestamp = i.Timestamp;
                        o.Source = i.Source;
                        o.Level = i.Level;
                        o.Event = i.Event;
                        o.Message = i.Message;

                        foreach (dynamic property in await connection.QueryAsync<dynamic>("SELECT [Key], [Value] FROM [dbo].[LogProperty] WHERE [LogEntryId] = @log", new { @log = i.Id }))
                            o.Properties.Add(property.Key, property.Value);

                        foreach (dynamic tag in await connection.QueryAsync<dynamic>("SELECT [Value] FROM [dbo].[LogTag] WHERE [LogEntryId] = @log", new { @log = i.Id }))
                            o.Tags.Add(tag.Value);

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.SqlServer.LogRepository.GetLogEntries");
                Logger.Error(e);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            Logger.Trace("END | Matrix.Server.Log.Database.SqlServer.LogRepository.GetLogEntries");

            return result;
        }

        public async Task<bool> CreateLogEntry(Guid app, bool enabled, DateTime timestamp, string source, int level, int @event, string message, Dictionary<string, string> properties = null, List<string> tags = null)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.SqlServer.LogRepository.CreateLogEntry");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    var id = await connection.QueryFirstOrDefaultAsync("INSERT INTO [dbo].[Logs] ([Enabled], [Application], [Timestamp], [Source], [Level], [Event], [Message]) OUTPUT INSERTED.ID VALUES (@enabled, @app, @timestamp, @source, @level, @event, @message)", new
                    {
                        @enabled = enabled,
                        @app = app,
                        @timestamp = timestamp,
                        @source = source,
                        @level = level,
                        @event = @event,
                        @message = message
                    });

                    if (properties != null)
                    {
                        foreach (var i in properties)
                            await connection.ExecuteAsync("INSERT INTO [dbo].[LogProperty] ([Log], [Key], [Value]) VALUES (@log, @key, @value)", new { @log = id, @key = i.Key, @value = i.Value });
                    }

                    if (tags != null)
                    {
                        foreach (var i in tags)
                            await connection.ExecuteAsync("INSERT INTO [dbo].[LogTag] ([Log], [Value]) VALUES (@log, @tag", new { @log = id, @tag = i });
                    }

                    result = id != null;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.SqlServer.LogRepository.CreateLogEntry");
                Logger.Error(e);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            Logger.Trace("END | Matrix.Server.Log.Database.SqlServer.LogRepository.CreateLogEntry");

            return result;
        }

        public async Task<bool> DeleteLogEntries(Guid app, DateTime from, DateTime to)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.SqlServer.LogRepository.DeleteLogEntries");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    foreach (var i in await connection.QueryAsync<Guid>("SELECT [Id] FROM [dbo].[Logs] WHERE [Application] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to", new { @app = app, @from = from, @to = to }))
                    {
                        await connection.ExecuteAsync("DELETE FROM [dbo].[LogProperty] WHERE [LogEntryId] = @log", new { @log = i });
                        await connection.ExecuteAsync("DELETE FROM [dbo].[LogTag] WHERE [LogEntryId] = @log", new { @log = i });
                    }

                    result = await connection.ExecuteAsync("DELETE FROM [dbo].[Logs] WHERE [Application] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to", new { @app = app, @from = from, @to = to }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.SqlServer.LogRepository.DeleteLogEntries");
                Logger.Error(e);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            Logger.Trace("END | Matrix.Server.Log.Database.SqlServer.LogRepository.DeleteLogEntries");

            return result;
        }
    }
}