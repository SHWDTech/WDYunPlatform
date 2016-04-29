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

        /// <summary>
        /// （同步）读取指定名称的协议信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Protocol GetProtocolFullLoadedByName(string name);
    }
}