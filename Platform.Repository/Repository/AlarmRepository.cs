using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 报警信息数据仓库
    /// </summary>
    public class AlarmRepository : DataRepository<IAlarm>, IAlarmRepository
    {
    }
}
