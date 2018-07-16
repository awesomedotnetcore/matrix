using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Directory.Model;
using Matrix.Extension;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Database.Repositories
{
    public class UserRoleRepository : Repository, IUserRoleRepository
    {
        public UserRoleRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<List<UserRole>> GetUserRoles(Guid application)
        {
            var result = new List<UserRole>();

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoles");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Name], [Description] FROM [UserRoles] WHERE [Application] = @application", new { application });

                    foreach (var i in entity)
                    {
                        var o = new UserRole();

                        o.Id = Parse.Guid(i.Id);
                        o.Application = application;
                        o.Name = i.Name;
                        o.Description = i.Description;

                        result.Add(o);
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoles");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoles");

            return result;
        }

        public async Task<UserRole> GetUserRoleById(Guid id)
        {
            UserRole result = null;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoleById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [Name], [Description] FROM [UserRoles] WHERE [Id] = @id", new { id });

                    if (entity != null)
                    {
                        result = new UserRole()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            Name = entity.Name,
                            Description = entity.Description
                        };
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoleById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoleById");

            return result;
        }

        public async Task<UserRole> GetUserRoleByName(Guid application, string name)
        {
            UserRole result = null;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoleByName");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [Name], [Description] FROM [UserRoles] WHERE [Application] = @application AND [Name] = @name", new { application, name });

                    if (entity != null)
                    {
                        result = new UserRole()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            Name = entity.Name,
                            Description = entity.Description
                        };
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoleByName");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRoleRepository.GetUserRoleByName");

            return result;
        }

        public async Task<Guid> CreateUserRole(Guid application, string name, string description)
        {
            var result = Guid.Empty;

            Logger.Begin();

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    var id = Guid.NewGuid();

                    var inserted = await connection.ExecuteAsync("INSERT INTO [UserRoles] ([Id], [Application], [Name], [Description], [Created], [Updated], [Deleted]) VALUES (@id, @application, @name, @description, @created, @updated, @deleted)", new
                    {
                        @id = id,
                        @application = application,
                        @name = name,
                        @description = description,
                        @created = DateTime.Now,
                        @updated = DateTime.Now,
                        @deleted = false
                    });

                    result = inserted > 0 ? id : Guid.Empty;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserRole(Guid id, string name, string description)
        {
            var result = false;

            Logger.Begin();

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [UserRoles] SET [Name] = @name, [Description] = @description, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        @id = id,
                        @name = name,
                        @description = description,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> DeleteUserRole(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRoleRepository.DeleteUserRole");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [UserRole] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRoleRepository.DeleteUserRole");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRoleRepository.DeleteUserRole");

            return result;
        }
    }
}