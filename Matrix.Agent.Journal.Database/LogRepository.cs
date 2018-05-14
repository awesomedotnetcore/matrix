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

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [ID], [Enabled], [Timestamp], [Source], [Level], [Event], [Message] FROM [dbo].[Log] WHERE [JournalApplication] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to AND [Message] LIKE '%" + searchTerm + "%'", new { @app = app, @from = from, @to = to });

                    foreach (var i in entity)
                    {
                        var o = new Log();

                        o.Id = i.Id;
                        o.Enabled = i.Enabled;
                        o.Timestamp = i.Timestamp;
                        o.Source = await connection.QueryFirstAsync<string>("SELECT [Name] FROM [dbo].[LogSource] WHERE [ID] = @id", new { @id = i.Source });
                        o.Level = i.Level;
                        o.Event = i.Event;
                        o.Message = i.Message;

                        foreach (dynamic property in await connection.QueryAsync<dynamic>("SELECT [Name], [Value] FROM [dbo].[JournalLogProperty] WHERE [Log] = @log", new { @log = i.ID }))
                            o.Properties.Add(property.Name, property.Value);

                        foreach (dynamic property in await connection.QueryAsync<dynamic>("SELECT [Tag] FROM [dbo].[JournalLogTag] WHERE [Log] = @log", new { @log = i.ID }))
                            o.Tags.Add(property.Tag);

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

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [ID], [Enabled], [Timestamp], [Source], [Level], [Event], [Message] FROM [dbo].[Log] WHERE [JournalApplication] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to", new { @app = app, @from = from, @to = to });

                    foreach (var i in entity)
                    {
                        var o = new Log();

                        o.Id = i.Id;
                        o.Enabled = i.Enabled;
                        o.Timestamp = i.Timestamp;
                        o.Source = await connection.QueryFirstAsync<string>("SELECT [Name] FROM [dbo].[LogSource] WHERE [ID] = @id", new { @id = i.Source });
                        o.Level = i.Level;
                        o.Event = i.Event;
                        o.Message = i.Message;

                        foreach (dynamic property in await connection.QueryAsync<dynamic>("SELECT [Name], [Value] FROM [dbo].[JournalLogProperty] WHERE [Log] = @log", new { @log = i.ID }))
                            o.Properties.Add(property.Name, property.Value);

                        foreach (dynamic property in await connection.QueryAsync<dynamic>("SELECT [Tag] FROM [dbo].[JournalLogTag] WHERE [Log] = @log", new { @log = i.ID }))
                            o.Tags.Add(property.Tag);

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

                    var sourceId = await connection.QueryFirstOrDefaultAsync<int>("SELECT [ID] FROM [dbo].[LogSource] WHERE [Name] = @source", new { @source = source });

                    if (sourceId.Equals(0))
                        sourceId = await connection.QueryFirstOrDefaultAsync<int>("INSERT INTO [dbo].[LogSource] ([Enabled], [JournalApplication], [Name]) OUTPUT INSERTED.ID VALUES (@enabled, @app, @name)", new { @enabled = true, @app = app, @name = source });

                    var id = await connection.QueryFirstOrDefaultAsync("INSERT INTO [dbo].[Log] ([Enabled], [JournalApplication], [Timestamp], [Source], [Level], [Event], [Message]) OUTPUT INSERTED.ID VALUES (@enabled, @app, @timestamp, @source, @level, @event, @message)", new
                    {
                        @enabled = enabled,
                        @app = app,
                        @timestamp = timestamp,
                        @source = sourceId,
                        @level = level,
                        @event = @event,
                        @message = message
                    });

                    if (properties != null)
                    {
                        foreach (var i in properties)
                            await connection.ExecuteAsync("INSERT INTO [dbo].[JournalLogProperty] ([Log], [Name], [Value]) VALUES (@log, @name, @value)", new { @log = id, @name = i.Key, @value = i.Value });
                    }

                    if (tags != null)
                    {
                        foreach (var i in tags)
                            await connection.ExecuteAsync("INSERT INTO [dbo].[JournalLogTag] ([Log], [Tag]) VALUES (@log, @tag", new { @log = id, @tag = i });
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

                    foreach (var i in await connection.QueryAsync<Guid>("SELECT [ID] FROM [dbo].[Log] WHERE [JournalApplication] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to", new { @app = app, @from = from, @to = to }))
                    {
                        await connection.ExecuteAsync("DELETE FROM [dbo].[JournalLogProperty] WHERE [Log] = @log", new { @log = i });
                        await connection.ExecuteAsync("DELETE FROM [dbo].[JournalLogTag] WHERE [Log] = @log", new { @log = i });
                    }

                    result = await connection.ExecuteAsync("DELETE FROM [dbo].[Log] WHERE [JournalApplication] = @app AND [Timestamp] >= @from AND [Timestamp] <= @to", new { @app = app, @from = from, @to = to }) > 0;

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