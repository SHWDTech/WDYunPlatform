using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 监测数据仓库
    /// </summary>
    internal class MonitorDataRepository : DataRepository<IMonitorData>, IMonitorDataRepository
    {
        /// <summary>
        /// 创建新的监测数据仓库实例
        /// </summary>
        protected MonitorDataRepository()
        {
            
        }
    }
}
