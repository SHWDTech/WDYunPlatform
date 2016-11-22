using System;
using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 油烟系统用户数据仓库
    /// </summary>
    public class LampblackUserRepository : SysDomainRepository<LampblackUser>, ILampblackUserRepository
    {
        public LampblackUserRepository()
        {

        }

        public LampblackUserRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }

        public LampblackUser CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Status = UserStatus.Enabled;

            return model;
        }

        public LampblackUser GetUserById(Guid id) => GetModels(obj => obj.Id == id).FirstOrDefault();
    }
}
