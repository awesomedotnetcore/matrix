using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Registry.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Database
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

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.ApplicationRepository.GetApplications");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted] FROM [RegistryApplication] WHERE [Deleted] = @deleted", new { @deleted = false });

                    foreach (var i in entity)
                    {
                        var o = new Application();

                        o.Id = Parse.Guid(entity.Id);
                        o.Enabled = Parse.Bool(entity.Enabled);
                        o.Name = entity.Name;
                        o.Description = entity.Description;
                        o.Created = Parse.DateTime(entity.Created);
                        o.Updated = Parse.DateTime(entity.Updated);
                        o.Deleted = Parse.Bool(entity.Deleted);

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.ApplicationRepository.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.ApplicationRepository.GetApplications");

            return result;
        }

        public async Task<Application> GetApplicationById(Guid id)
        {
            var result = new Application();

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.ApplicationRepository.GetApplicationById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted] FROM [RegistryApplication] WHERE [Id] = @id AND [Deleted] = @deleted", new { @id = id, @deleted = false });

                    result.Id = Parse.Guid(entity.Id);
                    result.Enabled = Parse.Bool(entity.Enabled);
                    result.Name = entity.Name;
                    result.Description = entity.Description;
                    result.Created = Parse.DateTime(entity.Created);
                    result.Updated = Parse.DateTime(entity.Updated);
                    result.Deleted = Parse.Bool(entity.Deleted);

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.ApplicationRepository.GetApplicationById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.ApplicationRepository.GetApplicationById");

            return result;
        }

        public async Task<bool> ContainsApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.ApplicationRepository.ContainsApplication");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.QueryFirstOrDefaultAsync<int>("SELECT COUNT([Id]) FROM [RegistryApplication] WHERE [Id] = @id", new { @id = id }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.ApplicationRepository.ContainsApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.ApplicationRepository.ContainsApplication");

            return result;
        }

        public async Task<bool> CreateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.ApplicationRepository.CreateApplication");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    var inserted = await connection.ExecuteAsync("INSERT INTO [RegistryApplication] ([Id], [Enabled], [Name], [Description], [Created], [Updated], [Deleted]) VALUES (@id, @enabled, @name, @description, @created, @updated, @deleted)", new
                    {
                        @id = id,
                        @enabled = true,
                        @name = name,
                        @description = description,
                        @created = DateTime.Now,
                        @updated = DateTime.Now,
                        @deleted = false
                    });

                    result = inserted > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.ApplicationRepository.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.ApplicationRepository.CreateApplication");

            return result;
        }

        public async Task<bool> UpdateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.ApplicationRepository.UpdateApplication");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [RegistryApplication] SET [Name] = @name, [Description] = @description, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.ApplicationRepository.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.ApplicationRepository.UpdateApplication");

            return result;
        }

        public async Task<bool> DeleteApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.ApplicationRepository.DeleteApplication");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [RegistryApplication] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.ApplicationRepository.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.ApplicationRepository.DeleteApplication");

            return result;
        }
    }
}