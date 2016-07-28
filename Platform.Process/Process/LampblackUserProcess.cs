using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using System.Transactions;

namespace Platform.Process.Process
{
    /// <summary>
    /// 油烟系统用户处理程序
    /// </summary>
    public class LampblackUserProcess : ProcessBase, ILampblackUserProcess
    {
        public IPagedList<LampblackUser> GetPagedLampblackUsers(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = Repo<LampblackUserRepository>())
            {
                var query = repo.GetAllModels();
                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.UserName.Contains(queryName) || obj.UserIdentityName.Contains(queryName) || obj.LoginName.Contains(queryName));
                }
                count = query.Count();

                return query.Include("Department").Include("CateringCompany").OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public Exception AddOrUpdateLampblackUser(LampblackUser model, List<string> propertyNames, List<string> roleList)
        {
            using (var scope = new TransactionScope())
            {
                using (var repo = Repo<LampblackUserRepository>())
                {
                    try
                    {
                        Guid userId;

                        if (model.Id == Guid.Empty)
                        {
                            var dbModel = LampblackUserRepository.CreateDefaultModel();
                            foreach (var propertyName in propertyNames)
                            {
                                if (dbModel.GetType().GetProperties().Any(obj => obj.Name == propertyName))
                                {
                                    dbModel.GetType().GetProperty(propertyName).SetValue(dbModel, model.GetType().GetProperty(propertyName).GetValue(model));
                                }
                            }
                            userId = repo.AddOrUpdateDoCommit(dbModel);
                        }
                        else
                        {
                            userId = repo.PartialUpdateDoCommit(model, propertyNames);
                        }

                        var user = repo.GetModelIncludeById(userId, new List<string> { "Roles" });

                        UpdateUserRoles(user, roleList);
                        Commit();
                        GeneralProcess.RefreashUserPermissionsCache();
                    }
                    catch (Exception ex) when (ex is DbUpdateException || ex is DbEntityValidationException)
                    {
                        return ex;
                    }
                }

                scope.Complete();
                return null;
            }
        }

        public bool DeleteLampblackUser(Guid userId)
        {
            using (var repo = Repo<LampblackUserRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == userId);
                if (item == null) return false;

                GeneralProcess.RefreashUserPermissionsCache();

                return repo.DeleteDoCommit(item);
            }
        }

        public LampblackUser GetLampblackUser(Guid guid)
        {
            using (var repo = Repo<LampblackUserRepository>())
            {
                return repo.GetModelIncludeById(guid, new List<string>() { "Roles" });
            }
        }

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleList"></param>
        private void UpdateUserRoles(LampblackUser user, List<string> roleList)
        {
            user.Roles.Clear();

            if (roleList == null) return;

            var roleRepo = Repo<RoleRepository>();
            user.Roles.Clear();
            foreach (var roleId in roleList)
            {
                user.Roles.Add(roleRepo.GetModelById(Guid.Parse(roleId)));
            }
        }

        public bool HasLoginName(LampblackUser user)
            => Repo<LampblackUserRepository>().IsExists(obj => obj.LoginName == user.LoginName);
    }
}
