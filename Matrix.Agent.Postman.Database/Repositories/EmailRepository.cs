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
    public class EmailRepository : Repository, IEmailRepository
    {
        public EmailRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<Email> GetEmailById(Guid id)
        {
            Email result = null;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.GetEmailById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryFirstOrDefaultAsync<dynamic>("SELECT [Id], [Application], [From], [Subject], [Body], [HTML], [Status] FROM [Emails] WHERE [Id] = @id AND [Deleted] = @deleted", new { id, @deleted = false });

                    if (entity != null)
                    {
                        result = new Email()
                        {
                            Id = Parse.Guid(entity.Id),
                            Application = Parse.Guid(entity.Application),
                            From = entity.From,
                            Subject = entity.Subject,
                            Body = entity.Body,
                            HTML = Parse.Bool(entity.HTML),
                            Status = Parse.Integer(entity.Status)
                        };

                        dynamic list = await connection.QueryAsync<dynamic>("SELECT [Address], [Blind], [Copy] FROM [EmailAddress] WHERE [EmailId] = @id AND [Deleted] = @deleted", new { id, @deleted = false });

                        if (list != null)
                        {
                            foreach (var i in list)
                            {
                                var address = i.Address;

                                var copy = Parse.Bool(i.Copy);

                                var blind = Parse.Bool(i.Blind);

                                if (!copy && !blind)
                                    result.To.Add(address);
                                else
                                {
                                    if (copy)
                                        result.Cc.Add(address);

                                    if (blind)
                                        result.Bcc.Add(address);
                                }
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.GetEmailById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.GetEmailById");

            return result;
        }

        public async Task<List<Email>> GetEmailByStatus(Guid application, int status)
        {
            var result = new List<Email>();

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.GetEmailByStatus");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    dynamic entity = await connection.QueryAsync<dynamic>("SELECT [Id], [Application], [From], [Subject], [Body], [HTML], [Status] FROM [Emails] WHERE [Application] = @Application AND [Status] = @status AND [Deleted] = @deleted", new { application, status, @deleted = false });

                    if (entity != null)
                    {
                        foreach (var i in entity)
                        {
                            var o = new Email()
                            {
                                Id = Parse.Guid(i.Id),
                                Application = Parse.Guid(i.Application),
                                From = i.From,
                                Subject = i.Subject,
                                Body = i.Body,
                                HTML = Parse.Bool(i.HTML),
                                Status = Parse.Integer(i.Status)
                            };

                            dynamic list = await connection.QueryAsync<dynamic>("SELECT [Address], [Blind], [Copy] FROM [EmailAddress] WHERE [EmailId] = @id AND [Deleted] = @deleted", new { o.Id, @deleted = false });

                            if (list != null)
                            {
                                foreach (var x in list)
                                {
                                    var address = x.Address;

                                    var copy = Parse.Bool(x.Copy);

                                    var blind = Parse.Bool(x.Blind);

                                    if (!copy && !blind)
                                        i.To.Add(address);
                                    else
                                    {
                                        if (copy)
                                            i.Cc.Add(address);

                                        if (blind)
                                            i.Bcc.Add(address);
                                    }
                                }
                            }

                            result.Add(o);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.GetEmailByStatus");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.GetEmailByStatus");

            return result;
        }

        public async Task<Guid> CreateEmail(Guid application, string from, List<string> to, List<string> cc, List<string> bcc, string subject, string body, bool html, int status)
        {
            var result = Guid.Empty;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.CreateEmail");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    var id = Guid.NewGuid();

                    var inserted = await connection.ExecuteAsync("INSERT INTO [Emails] ([Id], [Application], [From], [Subject], [Body], [HTML], [Status], [Created], [Updated], [Deleted]) VALUES (@id, @application, @from, @subject, @body, @html, @status, @created, @updated, @deleted)", new
                    {
                        id,
                        application,
                        from,
                        subject,
                        body,
                        html,
                        status,
                        @created = DateTime.Now,
                        @updated = DateTime.Now,
                        @deleted = false
                    });

                    foreach (var i in to)
                    {
                        await connection.ExecuteAsync("INSERT INTO [EmailAddress] ([EmailId], [Address], [Blind], [Copy], [Created], [Updated], [Deleted]) VALUES (@id, @address, @blind, @copy, @created, @updated, @deleted)", new
                        {
                            id,
                            @address = i,
                            @blind = false,
                            @copy = false,
                            @created = DateTime.Now,
                            @updated = DateTime.Now,
                            @deleted = false
                        });
                    }

                    foreach (var i in cc)
                    {
                        await connection.ExecuteAsync("INSERT INTO [EmailAddress] ([EmailId], [Address], [Blind], [Copy], [Created], [Updated], [Deleted]) VALUES (@id, @address, @blind, @copy, @created, @updated, @deleted)", new
                        {
                            id,
                            @address = i,
                            @blind = false,
                            @copy = true,
                            @created = DateTime.Now,
                            @updated = DateTime.Now,
                            @deleted = false
                        });
                    }


                    foreach (var i in bcc)
                    {
                        await connection.ExecuteAsync("INSERT INTO [EmailAddress] ([EmailId], [Address], [Blind], [Copy], [Created], [Updated], [Deleted]) VALUES (@id, @address, @blind, @copy, @created, @updated, @deleted)", new
                        {
                            id,
                            @address = i,
                            @blind = true,
                            @copy = false,
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
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.CreateEmail");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.CreateEmail");

            return result;
        }

        public async Task<bool> UpdateEmail(Guid id, int status)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.UpdateEmail");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [Emails] SET [Status] = @status, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.UpdateEmail");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.UpdateEmail");

            return result;
        }

        public async Task<bool> DeleteEmailById(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.DeleteEmailById");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [EmailAddress] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
                    {
                        id,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    result = await connection.ExecuteAsync("UPDATE [Emails] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Id] = @id", new
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
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.DeleteEmailById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.DeleteEmailById");

            return result;
        }

        public async Task<bool> DeleteEmailByStatus(Guid application, int status)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.EmailRepository.DeleteEmailByStatus");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result = await connection.ExecuteAsync("UPDATE [EmailAddress] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Application] = @application AND [Status] = @status", new
                    {
                        application,
                        status,
                        @deleted = true,
                        @updated = DateTime.Now
                    }) > 0;

                    result = await connection.ExecuteAsync("UPDATE [Emails] SET [Deleted] = @deleted, [Updated] = @updated WHERE [Application] = @application AND [Status] = @status", new
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
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.EmailRepository.DeleteEmailByStatus");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.EmailRepository.DeleteEmailByStatus");

            return result;
        }
    }
}