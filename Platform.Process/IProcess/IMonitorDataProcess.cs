using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    public interface IMonitorDataProcess
    {
        /// <summary>
        /// 创建一个新的检测数据模型
        /// </summary>
        /// <returns></returns>
        MonitorData GetNewMonitorData();

        /// <summary>
        /// 添加或更新检测数据
        /// </summary>
        /// <param name="monitorDatas"></param>
        void AddOrUpdateMonitorData(IList<MonitorData> monitorDatas);

        /// <summary>
        /// 添加或更新检测数据
        /// </summary>
        /// <param name="monitorData"></param>
        void AddOrUpdateMonitorData(MonitorData monitorData);
    }
}
