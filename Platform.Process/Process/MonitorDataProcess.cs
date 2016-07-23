using System;
using System.Linq;
using System.Linq.Expressions;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
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
    }
}
