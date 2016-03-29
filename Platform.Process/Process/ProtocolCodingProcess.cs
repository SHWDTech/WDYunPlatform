﻿using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 协议编解码处理类
    /// </summary>
    public class ProtocolCodingProcess : IProtocolCodingProcess
    {
        public IList<Protocol> GetProtocolsFullLoaded() 
            => DbRepository.Repo<ProtocolRepository>().GetProtocolsFullLoaded();
    }
}
