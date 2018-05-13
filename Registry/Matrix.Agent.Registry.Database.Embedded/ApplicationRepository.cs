using Matrix.Agent.Database;
using Matrix.Agent.Registry.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Database.Embedded
{
    public class ApplicationRepository : EmbeddedRepository, IApplicationRepository
    {
        public ApplicationRepository(IDatabaseContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public async Task<List<Application>> GetApplications()
        {
            var result = new List<Application>();

            SQLiteConnection connection = null;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.GetApplications");

            try
            {
                await Task.Run(() =>
                {
                    result.AddRange(db?.GetCollection<Application>().FindAll().ToList());
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.GetApplications");

            return result;
        }

        public async Task<Application> GetApplicationById(Guid id)
        {
            Application result = null;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.GetApplicationById");

            try
            {
                await Task.Run(() =>
                {
                    result = db?.GetCollection<Application>().FindById(id);
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.GetApplicationById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.GetApplicationById");

            return result;
        }

        public async Task<bool> ContainsApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.ContainsApplication");

            try
            {
                await Task.Run(() =>
                {
                    var o = db?.GetCollection<Application>().Exists(i => i.Id.Equals(id));

                    if (o.HasValue)
                        result = o.Value;
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.ContainsApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.ApplicationExists");

            return result;
        }

        public async Task<bool> CreateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.CreateApplication");

            try
            {
                await Task.Run(() =>
                {
                    result = db.GetCollection<Application>().Insert(new Application()
                    {
                        Id = id,
                        Enabled = true,
                        Name = name,
                        Description = description,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }) != null;
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.CreateApplication");

            return result;
        }

        public async Task<bool> UpdateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.UpdateApplication");

            try
            {
                await Task.Run(() =>
                {
                    var entity = db.GetCollection<Application>().Find(i => i.Id.Equals(id)).FirstOrDefault();

                    if (entity != null)
                    {
                        entity.Name = name;
                        entity.Description = description;
                        entity.Updated = DateTime.Now;

                        result = db.GetCollection<Application>().Update(entity);
                    }
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.UpdateApplication");

            return result;
        }

        public async Task<bool> DeleteApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.DeleteApplication");

            try
            {
                await Task.Run(() =>
                {
                    result = db.GetCollection<Application>().Delete(i => i.Id.Equals(id)) > 0;
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.DeleteApplication");

            return result;
        }
    }
}