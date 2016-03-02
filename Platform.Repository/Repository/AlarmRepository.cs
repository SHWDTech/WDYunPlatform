using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 报警信息数据仓库
    /// </summary>
    internal class AlarmRepository : DataRepository<IAlarm>, IAlarmRepository
    {
        /// <summary>
        /// 创建新的报警信息数据仓库实例
        /// </summary>
        protected AlarmRepository()
        {
            
        }
    }
}
