using Dapper;
using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Postman.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Database.Repositories
{
    public class PhoneRepository : Repository, IPhoneRepository
    {
        public PhoneRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<List<PhoneMessage>> GetPhoneMessagesByStatus(Guid application, int status)
        {
            var result = new List<PhoneMessage>();

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.GetPhoneMessagesByStatus");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [From], [Message], [Status] FROM [PhoneMessages] WHERE [Application] = @application AND [Status] = @status AND [Deleted] = @deleted", new { application, status, @deleted = false });

                    if (entity != null)
                    {
                        foreach (var i in entity)
                        {
                            var o = new PhoneMessage()
                            {
                                Id = Parse.Guid(i.Id),
                                Application = Parse.Guid(i.Application),
                                From = i.From,
                                Message = i.Message,
                            };

                            dynamic list = await connection.QueryAsync<dynamic>("SELECT [Numer] FROM [EmailAddress] WHERE [PhoneMessageId] = @id AND [Deleted] = @deleted", new { o.Id, @deleted = false });

                            if (list != null)
                            {
                                foreach (var x in list)
                                    o.To.Add(x.Number);
                            }

                            result.Add(o);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.GetPhoneMessagesByStatus");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.GetPhoneMessagesByStatus");

            return result;
        }

        public async Task<PhoneMessage> GetPhoneMessageById(Guid id)
        {
            PhoneMessage result = null;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.GetPhoneMessageById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [From], [Message] FROM [PhoneMessages] WHERE [Id] = @id AND [Deleted] = @deleted", new { id, @deleted = false });

                    if (entity != null)
                    {
                        result = new PhoneMessage()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            From = entity.From,
                            Message = entity.Message,
                        };

                        dynamic list = await connection.QueryAsync<dynamic>("SELECT [Numer] FROM [EmailAddress] WHERE [PhoneMessageId] = @id AND [Deleted] = @deleted", new { id, @deleted = false });

                        if (list != null)
                        {
                            foreach (var i in list)
                                result.To.Add(i.Number);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.GetPhoneMessageById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.GetPhoneMessageById");

            return result;
        }

        public async Task<Guid> CreatePhoneMessage(Guid application, string from, List<string> to, string message, int status)
        {
            var result = Guid.Empty;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.CreatePhoneMessage");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    var id = Guid.NewGuid();

                    var inserted = await connection.ExecuteAsync("INSERT INTO [PhoneMessages] ([Id], [Application], [From], [Message], [Status], [Created], [Updated], [Deleted]) VALUES (@id, @application, @from, @message, @status, @created, @updated, @deleted)", new
                    {
                        id,
                        application,
                        from,
                        message,
                        status,
                        @created = DateTime.Now,
                        @updated = DateTime.Now,
                        @deleted = false
                    });

                    foreach (var i in to)
                    {
                        await connection.ExecuteAsync("INSERT INTO [PhoneNumber] ([PhoneMessageId], [Number], [Created], [Updated], [Deleted]) VALUES (@id, @address, @blind, @copy, @created, @updated, @deleted)", new
                        {
                            id,
                            @number = i,
                            @created = DateTime.Now,
                            @updated = DateTime.Now,
                            @deleted = false
                        });
                    }

                    result = inserted > 0 ? id : Guid.Empty;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.CreatePhoneMessage");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.CreatePhoneMessage");

            return result;
        }

        public async Task<bool> UpdatePhoneMessage(Guid id, int status)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.PhoneRepository.UpdatePhoneMessage");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [PhoneMessages] SET [Status] = @status, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        id,
                        status,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.PhoneRepository.UpdatePhoneMessage");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.PhoneRepository.UpdatePhoneMessage");

            return result;
        }

        public async Task<bool> DeletePhoneMessageById(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.PhoneRepository.DeletePhoneMessageById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [PhoneMessages] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        id,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.PhoneRepository.DeletePhoneMessageById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.PhoneRepository.DeletePhoneMessageById");

            return result;
        }

        public async Task<bool> DeletePhoneMessagesByStatus(Guid application, int status)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.PhoneRepository.DeletePhoneMessagesByStatus");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [PhoneMessages] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Application] = @application AND [Status] = @status", new
                    {
                        application,
                        status,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.PhoneRepository.DeletePhoneMessagesByStatus");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.PhoneRepository.DeletePhoneMessagesByStatus");

            return result;
        }
    }
}