using System;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 油烟系统用户数据仓库接口
    /// </summary>
    public interface ILampblackUserRepository : ISysDomainRepository<LampblackUser>
    {
        /// <summary>
        /// 获取指定ID的油烟系统用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        LampblackUser GetUserById(Guid id);
    }
}
