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
        /// 读取所有固件信息以及关联的固件集以及协议
        /// </summary>
        /// <returns></returns>
        IList<Firmware> GetFirmwaresFullLoaded();
    }
}