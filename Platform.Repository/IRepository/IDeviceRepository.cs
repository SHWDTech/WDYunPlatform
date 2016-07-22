using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
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
        IDevice GetDeviceById(Guid deviceGuid);

        /// <summary>
        /// 通过设备短ID号获取设备信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        IList<Device> GetDeviceByNodeId(string nodeId, bool isEnabled);
    }
}