using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 协议信息数据仓库
    /// </summary>
    public class ProtocolRepository : SysRepository<Protocol>, IProtocolRepository
    {
        public IList<Protocol> GetProtocolsFullLoaded()
            => DbContext.Protocols
                .Include("ProtocolStructures")
                .Include("ProtocolCommands.CommandDatas")
                .Include("Field")
                .Include("SubField")
                .ToList();

        public Protocol GetProtocolFullLoadedByName(string name)
            => GetProtocolsFullLoaded().FirstOrDefault(obj => obj.ProtocolName == name);
    }
}