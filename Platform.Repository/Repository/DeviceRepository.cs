using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 设备数据仓库
    /// </summary>
    public class DeviceRepository : SysDomainRepository<Device>, IDeviceRepository
    {
        public Device GetDeviceById(Guid deviceGuid) => GetAllModels().First(device => device.Id == deviceGuid);

        public IList<Protocol> GetDeviceProtocolsFullLoaded(Guid deviceGuid)
        {
            var device =
                DbContext.Devices.Include("FirmwareSet.Firmwares.Protocols.ProtocolStructures")
                                 .Include("FirmwareSet.Firmwares.Protocols.ProtocolCommands.CommandDatas")
                                 .First(dev => dev.Id == deviceGuid);

            var protocols = new List<Protocol>();
            foreach (var firmware in device.FirmwareSet.Firmwares)
            {
                protocols.AddRange(firmware.Protocols);
            }

            return protocols;
        }
    }
}