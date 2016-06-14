using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Entities;
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
        public DeviceRepository()
        {
            
        }

        public DeviceRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public IDevice GetDeviceById(Guid deviceGuid) => GetAllModels().First(device => device.Id == deviceGuid);

        public IList<Device> GetDeviceByNodeId(string nodeId)
            => DbContext.Devices.Include("FirmwareSet")
                    .Include("FirmwareSet.Firmwares")
                    .Where(device => device.DeviceNodeId == nodeId)
                    .ToList();
    }
}