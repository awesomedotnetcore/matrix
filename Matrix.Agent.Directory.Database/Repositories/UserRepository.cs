using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Directory.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Database.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        // GetUsers

        public async Task<List<User>> GetUsers(Guid application)
        {
            var result = new List<User>();

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.GetUsers");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [FirstName], [LastName], [Username], [Email], [Phone], [Created], [Updated], [Deleted] FROM [Users] WHERE [Application] = @application AND [Deleted] = @deleted", new { application, @deleted = false });

                    foreach (var i in entity)
                    {
                        result.Add(new User()
                        {
                            Id = Parse.Guid(i.Id),
                            Application = Parse.Guid(i.Application),
                            FirstName = i.FirstName,
                            LastName = i.LastName,
                            Username = i.Username,
                            Password = string.Empty,
                            Email = i.Email,
                            Phone = i.Phone
                        });
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.GetUsers");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.GetUsers");

            return result;
        }

        // GetUser

        public async Task<User> GetUserById(Guid id)
        {
            User result = null;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.GetUserById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [FirstName], [LastName], [Username], [Phone], [Email] FROM [Users] WHERE [Id] = @id", new { id });

                    if (entity != null)
                    {
                        result = new User()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Username = entity.Username,
                            Password = string.Empty,
                            Phone = entity.Phone,
                            Email = entity.Email
                        };
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.GetUserById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.GetUserById");

            return result;
        }

        public async Task<User> GetUserByUsername(Guid application, string username)
        {
            User result = null;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.GetUserByUsername");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [FirstName], [LastName], [Username], [Phone], [Email] FROM [Users] WHERE [Application] = @application AND [Username] = @username", new { application, username });

                    if (entity != null)
                    {
                        result = new User()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Username = entity.Username,
                            Password = string.Empty,
                            Phone = entity.Phone,
                            Email = entity.Email
                        };
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.GetUserByUsername");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.GetUserByUsername");

            return result;
        }

        public async Task<User> GetUserByEmail(Guid application, string email)
        {
            User result = null;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.GetUserByUsername");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [FirstName], [LastName], [Username], [Phone], [Email] FROM [Users] WHERE [Application] = @application AND [Email] = @email", new { application, email });

                    if (entity != null)
                    {
                        result = new User()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Username = entity.Username,
                            Password = string.Empty,
                            Phone = entity.Phone,
                            Email = entity.Email
                        };
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.GetUserByUsername");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.GetUserByUsername");

            return result;
        }

        // CreateUser

        public async Task<Guid> CreateUser(Guid application, string username, string password, string firstName, string lastName, string email, string phone)
        {
            var result = Guid.Empty;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.CreateUser");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    var id = Guid.NewGuid();

                    var inserted = await connection.ExecuteAsync("INSERT INTO [Users] ([Id], [Application], [FirstName], [LastName], [Username], [Password], [Email], [Phone], [Created], [Updated], [Deleted]) VALUES (@id, @application, @firstname, @lastname, @username, @password, @email, @phone, @created, @updated, @deleted)", new
                    {
                        @id = id,
                        @application = application,
                        @firstName = firstName,
                        @lastName = lastName,
                        @username = username,
                        @password = password,
                        @email = email,
                        @phone = phone,
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
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.CreateUser");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.CreateUser");

            return result;
        }

        // UpdateUserProfile

        public async Task<bool> UpdateUserProfileById(Guid id, string firstName, string lastName, string email, string phone)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [FirstName] = @firstname, [LastName] = @lastname, [Email] = @email, [Phone] = @phone, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        @id = id,
                        @firstName = firstName,
                        @lastName = lastName,
                        @email = email,
                        @phone = phone,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileById");

            return result;
        }

        public async Task<bool> UpdateUserProfileByUsername(Guid application, string username, string firstName, string lastName, string email, string phone)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileByUsername");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [FirstName] = @firstname, [LastName] = @lastname, [Email] = @email, [Phone] = @phone, [Updated] = @updated WHERE [Username] = @username", new
                    {
                        @username = username,
                        @firstName = firstName,
                        @lastName = lastName,
                        @email = email,
                        @phone = phone,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileByUsername");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileByUsername");

            return result;
        }

        public async Task<bool> UpdateUserProfileByEmail(Guid application, string email, string firstName, string lastName, string phone)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileByEmail");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [FirstName] = @firstname, [LastName] = @lastname, [Phone] = @phone, [Updated] = @updated WHERE [Application] = @application AND [Email] = @email", new
                    {
                        @application = application,
                        @firstName = firstName,
                        @lastName = lastName,
                        @email = email,
                        @phone = phone,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileByEmail");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.UpdateUserProfileByEmail");

            return result;
        }

        // UpdateUserPassword

        public async Task<bool> UpdateUserPasswordById(Guid id, string password)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [Password] = @password, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        @id = id,
                        @password = password,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordById");

            return result;
        }

        public async Task<bool> UpdateUserPasswordByUsername(Guid application, string username, string password)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordByUsername");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [Password] = @password, [Updated] = @updated WHERE [Application] = @application AND [Username] = @username", new
                    {
                        @application = application,
                        @username = username,
                        @password = password,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordByUsername");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordByUsername");

            return result;
        }

        public async Task<bool> UpdateUserPasswordByEmail(Guid application, string email, string password)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordByEmail");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [Password] = @password, [Updated] = @updated WHERE [Application] = @application AND [Email] = @email", new
                    {
                        @application = application,
                        @email = email,
                        @password = password,
                        @updated = DateTime.Now,
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordByEmail");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.UpdateUserPasswordByEmail");

            return result;
        }

        // AddUserGroups

        public Task<bool> AddUserGroupsById(Guid userId, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByUsername(Guid application, string username, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByUsername(Guid application, string username, params string[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByEmail(Guid application, string email, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByEmail(Guid application, string email, params string[] groups)
        {
            throw new NotImplementedException();
        }

        // AddUserRoles

        public Task<bool> AddUserRolesById(Guid userId, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesUsername(Guid application, string username, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesUsername(Guid application, string username, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesByEmail(Guid application, string email, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesByEmail(Guid application, string email, params string[] roles)
        {
            throw new NotImplementedException();
        }

        // RemoveUserGroups

        public Task<bool> RemoveUserGroupsById(Guid userId, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params string[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params string[] groups)
        {
            throw new NotImplementedException();
        }

        // RemoveUserRoles

        public Task<bool> RemoveUserRolesById(Guid userId, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByUsername(Guid application, string username, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByUsername(Guid application, string username, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByEmail(Guid application, string email, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByEmail(Guid application, string email, params string[] roles)
        {
            throw new NotImplementedException();
        }

        // DeleteUser

        public async Task<bool> DeleteUserById(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.DeleteUserById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.DeleteUserById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.DeleteUserById");

            return result;
        }

        public async Task<bool> DeleteUserByUsername(Guid application, string username)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.DeleteUserByUsername");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Application] = @application AND [Username] = @username", new
                    {
                        @application = application,
                        @username = username,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.DeleteUserByUsername");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.DeleteUserByUsername");

            return result;
        }

        public async Task<bool> DeleteUserByEmail(Guid application, string email)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Database.UserRepository.DeleteUserByEmail");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Users] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Application] = @application AND [Username] = @username", new
                    {
                        @application = application,
                        @email = email,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Database.UserRepository.DeleteUserByEmail");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Database.UserRepository.DeleteUserByEmail");

            return result;
        }
    }
}