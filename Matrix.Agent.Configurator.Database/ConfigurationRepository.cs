using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Database
{
    public class ConfigurationRepository : Repository, IConfigurationRepository
    {
        public ConfigurationRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<List<Model.KeyValuePair>> GetSettings(Guid application)
        {
            var result = new List<Model.KeyValuePair>();

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Database.ConfigurationRepository.GetSettings");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [Enabled], [Key], [Value], [Created], [Updated] FROM [Configuration] WHERE [Application] = @application AND [Deleted] = @deleted", new { @application = application, @deleted = false });

                    foreach (var i in entity)
                    {
                        var o = new Model.KeyValuePair();

                        o.Id = Parse.Guid(i.Id);
                        o.Application = Parse.Guid(i.Id);
                        o.Enabled = Parse.Bool(i.Enabled);
                        o.Key = i.Key;
                        o.Value = i.Value;
                        o.Created = Parse.DateTime(i.Created);
                        o.Updated = Parse.DateTime(i.Updated);

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Database.ConfigurationRepository.GetSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Database.ConfigurationRepository.GetSettings");

            return result;
        }

        public async Task<Model.KeyValuePair> GetSettings(Guid application, string key)
        {
            var result = new Model.KeyValuePair();

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Database.ConfigurationRepository.GetSettings");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = (await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [Enabled], [Key], [Value], [Created], [Updated] FROM [Configuration] WHERE [Application] = @application AND [Key] = @key AND [Deleted] = @deleted", new { @application = application, @key = key, @deleted = false }))?.FirstOrDefault();

                    if (entity != null)
                    {
                        result.Id = Parse.Guid(entity.Id);
                        result.Application = Parse.Guid(entity.Id);
                        result.Enabled = Parse.Bool(entity.Enabled);
                        result.Key = entity.Key;
                        result.Value = entity.Value;
                        result.Created = Parse.DateTime(entity.Created);
                        result.Updated = Parse.DateTime(entity.Updated);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Database.ConfigurationRepository.GetSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Database.ConfigurationRepository.GetSettings");

            return result;
        }

        public async Task<bool> CreateSettings(Guid application, string key, string value)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Database.ConfigurationRepository.CreateSettings");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    var inserted = await connection.ExecuteAsync("INSERT INTO [Configuration] ([Id], [Enabled], [Key], [Value], [Created], [Updated], [Deleted]) VALUES (@id, @enabled, @key, @value, @created, @updated, @deleted)", new
                    {
                        @id = Guid.NewGuid(),
                        @enabled = true,
                        @key = key,
                        @value = value,
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
                Logger.Trace("ERROR | Matrix.Server.Configurator.Database.ConfigurationRepository.CreateSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Database.ConfigurationRepository.CreateSettings");

            return result;
        }

        public async Task<bool> UpdateSettings(Guid application, string key, string value)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Database.ConfigurationRepository.UpdateSettings");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Configuration] SET [Value] = @value, [Updated] = @updated WHERE [Application] = @application AND [Key] = @key", new
                    {
                        @application = application,
                        @key = key,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Database.ConfigurationRepository.UpdateSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Database.ConfigurationRepository.UpdateSettings");

            return result;
        }

        public async Task<bool> DeleteSettings(Guid application, string key)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Database.ConfigurationRepository.DeleteSettings");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Configuration] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Application] = @application AND [Key] = @key", new
                    {
                        @application = application,
                        @key = key,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Database.ConfigurationRepository.DeleteSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Database.ConfigurationRepository.DeleteSettings");

            return result;
        }
    }
}