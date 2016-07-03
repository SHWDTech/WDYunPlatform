using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 餐饮企业数据仓库
    /// </summary>
    public class CateringCompanyRepository : SysDomainRepository<CateringCompany>, ICateringCompanyRepository
    {
        public CateringCompanyRepository()
        {

        }

        public CateringCompanyRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
