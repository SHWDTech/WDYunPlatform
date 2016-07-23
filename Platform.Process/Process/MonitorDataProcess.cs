using System;
using System.Linq;
using System.Linq.Expressions;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 监测数据处理程序
    /// </summary>
    public class MonitorDataProcess : ProcessBase, IMonitorDataProcess
    {
        public MonitorData GetMinHotelMonitorData(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var ret = repo.GetModels(exp);
                return ret.OrderBy(item => item.DoubleValue).FirstOrDefault();
            }
        }

        public MonitorData GetMaxHotelMonitorData(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                return repo.GetModels(exp)
                    .OrderByDescending(item => item.DoubleValue).FirstOrDefault();
            }
        }

        public DateTime GetLastUpdateDataDate(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var data = repo.GetModels(exp)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                return data?.UpdateTime ?? DateTime.MinValue;
            }
        }

        public int GetDataCount(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                return repo.GetCount(exp);
            }
        }

        public MonitorData GetFirst(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                return repo.GetModels(exp).First();
            }
        }
    }
}
