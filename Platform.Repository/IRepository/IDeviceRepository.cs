using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 设备数据仓库接口
    /// </summary>
    public interface IDeviceRepository : ISysDomainRepository<Device>
    {
        /// <summary>
        /// 通过GUID号获取设备信息
        /// </summary>
        /// <param name="deviceGuid">设备的GUID号</param>
        /// <returns>指定ID号对应的设备信息</returns>
        Device GetDeviceById(Guid deviceGuid);

        /// <summary>
        /// （同步）读取设备关联的协议信息
        /// </summary>
        /// <param name="deviceGuid">设备ID</param>
        /// <returns>设备关联的协议信息</returns>
        IList<Protocol> GetDeviceProtocolsFullLoaded(Guid deviceGuid);
    }
}