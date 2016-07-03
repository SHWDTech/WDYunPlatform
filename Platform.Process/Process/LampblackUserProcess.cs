using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 油烟系统用户处理程序
    /// </summary>
    public class LampblackUserProcess : ILampblackUserProcess
    {
        public IPagedList<LampblackUser> GetPagedLampblackUsers(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = DbRepository.Repo<LampblackUserRepository>())
            {
                var query = repo.GetAllModels();
                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.UserName.Contains(queryName) || obj.UserIdentityName.Contains(queryName) || obj.LoginName.Contains(queryName));
                }
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public DbEntityValidationException AddOrUpdateLampblackUser(LampblackUser model, List<string> propertyNames)
        {
            using (var repo = DbRepository.Repo<LampblackUserRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = LampblackUserRepository.CreateDefaultModel();
                        foreach (var propertyName in propertyNames)
                        {
                            dbModel.GetType().GetProperty(propertyName).SetValue(dbModel, model.GetType().GetProperty(propertyName).GetValue(model));
                        }
                        repo.AddOrUpdate(dbModel);
                    }
                    else
                    {
                        repo.PartialUpdate(model, propertyNames);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return ex;
                }
            }

            return null;
        }

        public bool DeleteLampblackUser(Guid userId)
        {
            using (var repo = DbRepository.Repo<LampblackUserRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == userId);
                if (item == null) return false;

                return repo.Delete(item);
            }
        }

        public LampblackUser GetLampblackUser(Guid guid)
        {
            using (var repo = DbRepository.Repo<LampblackUserRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}
