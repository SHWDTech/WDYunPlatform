using System;
using System.Collections.Generic;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 油烟系统用户数据仓库
    /// </summary>
    public class LampblackUserRepository : SysDomainRepository<LampblackUser>
    {
        public LampblackUserRepository()
        {

        }

        public LampblackUserRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }

        public new static LampblackUser CreateDefaultModel()
        {
            var model = SysRepository<LampblackUser>.CreateDefaultModel();
            model.Status = UserStatus.Enabled;

            return model;
        }
    }
}
