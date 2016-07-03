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
    public class DepartmentProcess : IDepartmentProcess
    {
        public IPagedList<Department> GetPagedDepartments(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = DbRepository.Repo<DepartmentRepository>())
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
            using (var repo = DbRepository.Repo<DepartmentRepository>())
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

        public bool DeleteDepartment(Guid userId)
        {
            using (var repo = DbRepository.Repo<DepartmentRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == userId);
                if (item == null) return false;

                return repo.Delete(item);
            }
        }

        public Department GetDepartment(Guid guid)
        {
            using (var repo = DbRepository.Repo<DepartmentRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}
