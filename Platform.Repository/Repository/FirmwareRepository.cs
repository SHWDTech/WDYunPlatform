using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 固件信息数据仓库
    /// </summary>
    public class FirmwareRepository : SysRepository<Firmware>, IFirmwareRepository
    {
        public IList<Firmware> GetFirmwaresFullLoaded() => DbContext.Firmwares.Include("FirmwareSets").Include("Protocols").ToList();
    }
}