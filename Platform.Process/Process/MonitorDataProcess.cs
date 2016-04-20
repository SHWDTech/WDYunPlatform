using System.Collections.Generic;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 检测数据处理类
    /// </summary>
    public class MonitorDataProcess : IMonitorDataProcess
    {
        /// <summary>
        /// 检测数据数据仓库
        /// </summary>
        private readonly IMonitorDataRepository _monitorDataRepository = DbRepository.Repo<MonitorDataRepository>();

        public MonitorData GetNewMonitorData() => _monitorDataRepository.CreateDefaultModel();

        public void AddOrUpdateMonitorData(IList<MonitorData> monitorDatas)
            => _monitorDataRepository.AddOrUpdate(monitorDatas);

        public void AddOrUpdateMonitorData(MonitorData monitorData) => _monitorDataRepository.AddOrUpdate(monitorData);
    }
}
