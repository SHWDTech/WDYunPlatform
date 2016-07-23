using System;
using System.Linq.Expressions;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    public interface IMonitorDataProcess
    {
        /// <summary>
        /// 获取指定条件的数值最小的监测数据
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        MonitorData GetMinHotelMonitorData(Expression<Func<MonitorData, bool>> exp);

        /// <summary>
        /// 获取指定条件的数值最大的监测数据
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        MonitorData GetMaxHotelMonitorData(Expression<Func<MonitorData, bool>> exp);

        /// <summary>
        /// 获取指定条件的最后更新的数据更新时间
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        DateTime GetLastUpdateDataDate(Expression<Func<MonitorData, bool>> exp);
    }
}
