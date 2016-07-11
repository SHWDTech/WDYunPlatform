using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 协议包处理类
    /// </summary>
    public class ProtocolPackageProcess : ProcessBase, IProtocolPackageProcess
    {
        public void AddOrUpdateMonitorData(IList<MonitorData> monitorDatas)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                repo.BulkInsert(monitorDatas);
            }
        }

        public void AddOrUpdateProtocolData(ProtocolData protocolData)
            => Repo<ProtocolDataRepository>().AddOrUpdateDoCommit(protocolData);
    }
}
