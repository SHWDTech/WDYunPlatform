using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using SqlComponents.SqlExcute;

namespace Platform.Process.Process
{
    public class WdRoleProcess : ProcessBase, IWdRoleProcess
    {
        public IPagedList<WdRole> GetPagedRoles(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = Repo<RoleRepository>())
            {
                var query = repo.GetModels(obj => obj.IsVisiable);
                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.RoleName.Contains(queryName));
                }
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public DbEntityValidationException AddOrUpdateRole(WdRole model, List<string> propertyNames)
        {
            using (var repo = Repo<RoleRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = RoleRepository.CreateDefaultModel();
                        foreach (var propertyName in propertyNames)
                        {
                            dbModel.GetType().GetProperty(propertyName).SetValue(dbModel, model.GetType().GetProperty(propertyName).GetValue(model));
                        }
                        repo.AddOrUpdateDoCommit(dbModel);
                    }
                    else
                    {
                        repo.PartialUpdateDoCommit(model, propertyNames);
                    }

                    GeneralProcess.RefreashRolePermissionsCache();
                }
                catch (DbEntityValidationException ex)
                {
                    return ex;
                }
            }

            return null;
        }

        public Dictionary<Guid, string> GetRoleSelectList()
        {
            using (var repo = Repo<RoleRepository>())
            {
                return repo.GetAllModels().ToDictionary(obj => obj.Id, item => item.RoleName);
            }
        }

        public SqlExcuteResult DeleteRole(Guid roleId)
        {
            using (var repo = Repo<RoleRepository>())
            {
                var sqlResult = new SqlExcuteResult() { Success = false };
                var role = repo.GetModel(obj => obj.Id == roleId);
                if (role == null) return sqlResult;

                try
                {
                    repo.DeleteDoCommit(role);

                    GeneralProcess.RefreashRolePermissionsCache();
                }
                catch (Exception ex)
                {
                    var innerEx = ex;
                    while (innerEx != null)
                    {
                        var sqlEx = innerEx as SqlException;
                        if (sqlEx != null)
                        {
                            sqlResult.Exception = sqlEx;
                            sqlResult.ErrorNumber = sqlEx.Number;

                            return sqlResult;
                        }

                        innerEx = innerEx.InnerException;
                    }

                    throw;
                }

                sqlResult.Success = true;
                return sqlResult;
            }
        }

        public WdRole GetRole(Guid guid)
        {
            using (var repo = Repo<RoleRepository>())
            {
                return repo.GetModelById(guid);
            }
        }

        public WdRole UpdatePermissions(Guid roleId, List<string> permissions)
        {
            using (var repo = Repo<RoleRepository>())
            {
                var role = repo.GetModelById(roleId);

                role.Permissions.Clear();

                if (permissions != null && permissions.Count > 0)
                {
                    var permissionList = Repo<PermissionRepository>().GetAllModelList();

                    foreach (var permission in permissionList)
                    {
                        if (permissions.Any(obj => obj == permission.Id.ToString()))
                        {
                            role.Permissions.Add(permission);
                        }
                    }
                }

                Commit();

                GeneralProcess.RefreashRolePermissionsCache();

                return role;
            }
        }
    }
}
