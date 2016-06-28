using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
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
