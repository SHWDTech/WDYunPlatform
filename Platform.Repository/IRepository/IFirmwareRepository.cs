using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 固件信息数据仓库接口
    /// </summary>
    public interface IFirmwareRepository : ISysRepository<Firmware>
    {
        /// <summary>
        /// 获取设备相关的固件信息
        /// </summary>
        /// <param name="deviceGuid">设备ID</param>
        /// <returns>设备相关的固件信息</returns>
        IList<Firmware> GetFirmwaresByDeviceGuid(Guid deviceGuid);
    }
}