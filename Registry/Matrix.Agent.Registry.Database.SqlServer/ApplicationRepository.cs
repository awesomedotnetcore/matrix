using Dapper;
using Matrix.Agent.Database;
using Matrix.Agent.Registry.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Database.SqlServer
{
    public class ApplicationRepository : SqlServerRepository, IApplicationRepository
    {
        public ApplicationRepository(IDatabaseContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public async Task<List<Application>> GetApplications()
        {
            var result = new List<Application>();

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.GetApplications");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted] FROM [dbo].[RegistryApplication] WHERE [Deleted] = @deleted", new { @deleted = false });

                    foreach (var i in entity)
                    {
                        var o = new Application();

                        o.Id = i.Id;
                        o.Enabled = i.Enabled;
                        o.Name = i.Name;
                        o.Description = i.Description;
                        o.Created = i.Created;
                        o.Updated = i.Updated;
                        o.Deleted = i.Deleted;

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.GetApplications");

            return result;
        }

        public async Task<Application> GetApplicationById(Guid id)
        {
            Application result = null;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.GetApplicationById");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result = await connection.QueryFirstOrDefaultAsync<Application>("SELECT [Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted] FROM [dbo].[RegistryApplication] WHERE [Id] = @id AND [Deleted] = @deleted", new { @id = id, @deleted = false });

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.GetApplicationById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.GetApplicationById");

            return result;
        }

        public async Task<bool> ContainsApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.ContainsApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result = await connection.QueryFirstOrDefaultAsync<int>("SELECT COUNT([Id]) FROM [dbo].[RegistryApplication] WHERE [Id] = @id", new { @id = id }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.ContainsApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.ContainsApplication");

            return result;
        }

        public async Task<bool> CreateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.CreateApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    var inserted = await connection.QueryFirstOrDefaultAsync("INSERT INTO [dbo].[RegistryApplication] ([Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted]) OUTPUT INSERTED.ID VALUES (@id, @enabled, @name, @description, @created, @updated, @deleted)", new
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
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.CreateApplication");

            return result;
        }

        public async Task<bool> UpdateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.UpdateApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [dbo].[RegistryApplication] SET [Name] = @name, [Description] = @description, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.UpdateApplication");

            return result;
        }

        public async Task<bool> DeleteApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.DeleteApplication");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [dbo].[RegistryApplication] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        @id = id,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.DeleteApplication");

            return result;
        }
    }
}