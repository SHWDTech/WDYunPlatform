using System.Collections.Generic;
using System.Linq;
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
        public List<CateringCompany> GetCateringCompanies()
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
            {
                return repo.GetAllModels().OrderBy(obj => obj.CreateDateTime).ToList();
            }
        }

        public void AddOrUpdateCateringEnterprise(CateringCompany model)
        {
            using (var repo = DbRepository.Repo<CateringCompanyRepository>())
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
        }
    }
}



