using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议包处理程序接口
    /// </summary>
    public interface IProtocolPackageProcess : IProcessBase
    {
        /// <summary>
        /// 更新监测数据
        /// </summary>
        /// <param name="monitorDatas"></param>
        void AddOrUpdateMonitorData(IList<MonitorData> monitorDatas);

        /// <summary>
        /// 更新协议数据包
        /// </summary>
        /// <param name="protocolData"></param>
        void AddOrUpdateProtocolData(ProtocolData protocolData);

        /// <summary>
        /// 更新报警数据
        /// </summary>
        /// <param name="alarmList"></param>
        void AddOrUpdateAlarm(List<Alarm> alarmList);
    }
}
