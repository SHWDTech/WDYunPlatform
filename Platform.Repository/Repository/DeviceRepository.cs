using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 设备数据仓库
    /// </summary>
    public class DeviceRepository : SysDomainRepository<Device>, IDeviceRepository
    {
        public IDevice GetDeviceById(Guid deviceGuid) => GetAllModels().First(device => device.Id == deviceGuid);

        public IList<Device> GetDeviceByNodeId(string nodeId)
            => GetModels(device => device.DeviceNodeId == nodeId).ToList();
    }
}