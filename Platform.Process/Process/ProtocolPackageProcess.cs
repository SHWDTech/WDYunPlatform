using System.Collections.Generic;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class ProtocolPackageProcess
    {
        public void AddOrUpdateMonitorData(IList<MonitorData> monitorDatas, ProtocolData protocolData)
        {
            
        }

        public ProtocolData CreateNewProtocolData()
        {
            var repo = new ProtocolDataRepository();
            return repo.CreateDefaultModel();
        }

        public MonitorData CreateNewMonitorData()
        {
            var repo = new MonitorDataRepository();
            return repo.CreateDefaultModel();
        }
    }
}
