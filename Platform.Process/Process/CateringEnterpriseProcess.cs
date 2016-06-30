using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 餐饮企业、饭店处理程序
    /// </summary>
    public class CateringEnterpriseProcess : ICateringEnterpriseProcess
    {
        public IPagedList<CateringCompany> GetPagedCateringCompanies(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
            {
                var query = repo.GetAllModels();
                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.CompanyName.Contains(queryName));
                }
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public DbEntityValidationException AddOrUpdateCateringEnterprise(CateringCompany model, List<string> propertyNames)
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = CateringCompanyRepository.CreateDefaultModel();
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

        public bool DeleteCateringEnterprise(Guid componyId)
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == componyId);
                if (item == null) return false;

                return repo.Delete(item);
            }
        }

        public CateringCompany GetCateringEnterprise(Guid guid)
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}



