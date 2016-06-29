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

        public DbEntityValidationException AddOrUpdateCateringEnterprise(CateringCompany model)
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
            {
                try
                {
                    var company = CateringCompanyRepository.CreateDefaultModel();
                    company.CompanyName = model.CompanyName;
                    company.CompanyCode = model.CompanyCode;
                    company.ChargeMan = model.ChargeMan;
                    company.Telephone = model.Telephone;
                    company.Address = model.Address;
                    company.Email = model.Email;
                    company.RegisterDateTime = model.RegisterDateTime;
                    repo.AddOrUpdate(company);
                }
                catch (DbEntityValidationException ex)
                {
                    return ex;
                }
            }

            return null;
        }
    }
}



