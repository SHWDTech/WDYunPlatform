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

        public void AddOrUpdateLampblackRecord(IList<LampblackRecord> records)
        {
            using (var repo = Repo<LampblackRecordRepository>())
            {
                repo.BulkInsert(records);
            }
        }

        public void AddOrUpdateProtocolData(ProtocolData protocolData)
            => Repo<ProtocolDataRepository>().AddOrUpdateDoCommit(protocolData);

        public void AddOrUpdateAlarm(List<Alarm> alarmList)
            => Repo<AlarmRepository>().AddOrUpdateDoCommit(alarmList);
    }
}
