using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class ProtocolDataProcess : IProtocolDataProcess
    {
        private readonly IProtocolDataRepository _protocolDataRepository = DbRepository.Repo<ProtocolDataRepository>();

        public ProtocolData GetNewProtocolData() => _protocolDataRepository.CreateDefaultModel();

        public void AddProtocolData(ProtocolData protocol) => _protocolDataRepository.AddOrUpdate(protocol);
    }
}
