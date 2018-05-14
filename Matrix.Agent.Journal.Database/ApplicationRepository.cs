using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Journal.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Database
{
    public class ApplicationRepository : Repository, IApplicationRepository
    {
        public ApplicationRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<List<Application>> GetApplications()
        {
            var result = new List<Application>();

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.ApplicationRepository.GetApplications");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted] FROM [JournalApplication]");

                    foreach (var i in entity)
                    {
                        var o = new Application();

                        o.Id = i.Id;
                        o.Enabled = i.Enabled;
                        o.Name = i.Name;
                        o.Description = i.Description;
                        o.Created = i.Created;
                        o.Updated = i.Updated;

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.ApplicationRepository.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Database.ApplicationRepository.GetApplications");

            return result;
        }

        public async Task<bool> HasApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.ApplicationRepository.HasApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstAsync<dynamic>("SELECT [Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted] FROM [JournalApplication] WHERE [Id] = @id", new { @id = id });

                    if (entity != null)
                        result = ((Guid)entity.ID) == id;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.ApplicationRepository.HasApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Database.ApplicationRepository.HasApplication");

            return result;
        }

        public async Task<bool> CreateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.ApplicationRepository.CreateApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    var inserted = await connection.QueryFirstOrDefaultAsync("INSERT INTO [JournalApplication] ([Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted]) OUTPUT INSERTED.ID VALUES (@id, @enabled, @name, @description, @created, @updated, @deleted)", new
                    {
                        @id = id,
                        @enabled = true,
                        @name = name,
                        @description = description,
                        @created = DateTime.Now,
                        @updated = DateTime.Now,
                        @deleted = false
                    });

                    result = inserted != null;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.ApplicationRepository.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Database.ApplicationRepository.CreateApplication");

            return result;
        }

        public async Task<bool> UpdateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.ApplicationRepository.UpdateApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [JournalApplication] SET [Name] = @name, [Description] = @description, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        @id = id,
                        @enabled = true,
                        @name = name,
                        @description = description,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.ApplicationRepository.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Database.ApplicationRepository.UpdateApplication");

            return result;
        }

        public async Task<bool> DeleteApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.ApplicationRepository.DeleteApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("DELETE FROM [JournalApplication] WHERE [Id] = @id", new { @id = id }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.ApplicationRepository.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Database.ApplicationRepository.DeleteApplication");

            return result;
        }
    }
}