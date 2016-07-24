using System;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 运行时间处理程序
    /// </summary>
    public class RunningTimeProcess : ProcessBase, IRunningTimeProcess
    {
        public DateTime LastRecordDateTime(Guid hotelGuid)
        {
            using (var repo = Repo<RunningTimeRepository>())
            {
                var lastRecord = repo.GetModels(obj => obj.ProjectId == hotelGuid)
                    .OrderByDescending(item => item.UpdateTime).FirstOrDefault();

                return lastRecord?.UpdateTime ?? DateTime.MinValue;
            }
        }

        public void StoreRunningTime(RunningTime runningTime)
            => Repo<RunningTimeRepository>().AddOrUpdateDoCommit(runningTime);
    }
}
