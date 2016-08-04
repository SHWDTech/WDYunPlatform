using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 协议编解码处理类
    /// </summary>
    public class ProtocolCodingProcess : ProcessBase, IProtocolCodingProcess
    {
        public IList<Protocol> GetProtocolsFullLoaded() 
            => Repo<ProtocolRepository>().GetProtocolsFullLoaded();


        public Protocol GetProtocolFullLoadedByName(string name)
            => Repo<ProtocolRepository>().GetProtocolFullLoadedByName(name);

        public Protocol GetProtocolByName(string name)
            => Repo<ProtocolRepository>().GetModels(model => model.ProtocolName == name).FirstOrDefault();

        public ProtocolCommand GetProtocolCommandFullLoadedById(Guid commandGuid)
            => Repo<ProtocolCommandRepository>().GetModelIncludeById(commandGuid, new List<string>() {"Protocol", "Protocol.ProtocolStructures", "CommandDefinitions" });

    }
}
