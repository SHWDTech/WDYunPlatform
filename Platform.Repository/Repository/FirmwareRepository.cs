using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 固件信息数据仓库
    /// </summary>
    public class FirmwareRepository : SysRepository<Firmware>, IFirmwareRepository
    {
        public FirmwareRepository()
        {
            
        }

        public FirmwareRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public IList<Firmware> GetFirmwaresByDeviceGuid(Guid deviceGuid) => DbContext.Devices.First(dev => dev.Id == deviceGuid).FirmwareSet.Firmwares.ToList();
    }
}