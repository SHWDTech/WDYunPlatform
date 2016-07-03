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
    public class DepartmentProcess : ProcessBase, IDepartmentProcess
    {
        public IPagedList<Department> GetPagedDepartments(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = Repo<DepartmentRepository>())
            {
                var query = repo.GetAllModels();
                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.Name.Contains(queryName));
                }
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public DbEntityValidationException AddOrUpdateDepartmentr(Department model, List<string> propertyNames)
        {
            using (var repo = Repo<DepartmentRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = DepartmentRepository.CreateDefaultModel();
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
                }
                catch (DbEntityValidationException ex)
                {
                    return ex;
                }
            }

            return null;
        }

        public Dictionary<string, string> GetDepartmentSelectList()
        {
            using (var repo = Repo<DepartmentRepository>())
            {
                return repo.GetAllModels().ToDictionary(obj => obj.Name, item => item.Id.ToString());
            }
        }

        public SqlExcuteResult DeleteDepartment(Guid departmentId)
        {
            using (var repo = Repo<RoleRepository>())
            {
                var sqlResult = new SqlExcuteResult() { Success = false };
                var role = repo.GetModel(obj => obj.Id == departmentId);
                if (role == null) return sqlResult;

                try
                {
                    repo.DeleteDoCommit(role);
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

        public Department GetDepartment(Guid guid)
        {
            using (var repo = Repo<DepartmentRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}
