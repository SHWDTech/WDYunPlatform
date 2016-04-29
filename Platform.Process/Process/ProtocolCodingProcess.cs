using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// 协议数据仓库
        /// </summary>
        private readonly ProtocolRepository _repository = DbRepository.Repo<ProtocolRepository>();

        public IList<Protocol> GetProtocolsFullLoaded() 
            => _repository.GetProtocolsFullLoaded();


        public Protocol GetProtocolFullLoadedByName(string name)
            => _repository.GetProtocolFullLoadedByName(name);

        public Protocol GetProtocolByName(string name)
            => _repository.GetModels(model => model.ProtocolName == name).FirstOrDefault();

    }
}
