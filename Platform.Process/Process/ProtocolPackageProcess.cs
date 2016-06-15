using System;
using System.Collections.Generic;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class ProtocolPackageProcess
    {
        public void AddOrUpdateMonitorData(IList<MonitorData> monitorDatas)
        {
            using (var repo = DbRepository.Repo<MonitorDataRepository>())
            {
                repo.BulkInsert(monitorDatas);
            }
        }
    }
}
