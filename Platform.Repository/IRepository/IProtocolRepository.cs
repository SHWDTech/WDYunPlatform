using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 协议信息数据仓库接口
    /// </summary>
    public interface IProtocolRepository : ISysRepository<Protocol>
    {
        /// <summary>
        /// （同步）读取所有协议信息
        /// </summary>
        /// <returns></returns>
        IList<Protocol> GetProtocolsFullLoaded();
    }
}