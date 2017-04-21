using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 餐饮企业、饭店处理程序
    /// </summary>
    public class CateringEnterpriseProcess : ProcessBase, ICateringEnterpriseProcess
    {
        public IPagedList<CateringCompany> GetPagedCateringCompanies(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = Repo<CateringCompanyRepository>())
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

        public Dictionary<Guid,  string> GetCateringCompanySelectList()
        {
            using (var repo = Repo<CateringCompanyRepository>())
            {
                return repo.GetAllModels().ToDictionary(obj => obj.Id, item => item.CompanyName);
            }
        }

        public DbEntityValidationException AddOrUpdateCateringEnterprise(CateringCompany model, List<string> propertyNames)
        {
            using (var repo = Repo<CateringCompanyRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = repo.CreateDefaultModel();
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

        public List<CateringCompany> GetCateringCompanyByArea(Expression<Func<CateringCompany, bool>> exp,int offset, int limit, out int count)
        {
            using (var repo = Repo<CateringCompanyRepository>())
            {
                var ents = repo.GetAllModels();
                if (exp != null)
                {
                    ents = ents.Where(exp);
                }

                count = ents.Count();

                return ents.OrderBy(e => e.Id).Skip(offset).Take(limit).ToList();
            }
        }

        public bool DeleteCateringEnterprise(Guid componyId)
        {
            using (var repo = Repo<CateringCompanyRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == componyId);
                if (item == null) return false;

                return repo.DeleteDoCommit(item);
            }
        }

        public CateringCompany GetCateringEnterprise(Guid guid)
        {
            using (var repo = Repo<CateringCompanyRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}



