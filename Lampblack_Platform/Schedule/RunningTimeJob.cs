using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using Quartz;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.Enums;

namespace Lampblack_Platform.Schedule
{
    public class RunningTimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var commandDatas = (List<Guid>)context.JobDetail.JobDataMap.Get("commandDatas");
            var date = DateTime.Now.GetToday();
            using (var ctx = new RepositoryDbContext())
            {
                foreach (var project in ctx.Projects)
                {
                    foreach (var device in ctx.Devices.Where(d => d.ProjectId == project.Id))
                    {
                        foreach (var commandData in commandDatas)
                        {
                            try
                            {
                                var runTime = ProcessInvoke.Instance<HotelRestaurantProcess>()
                                    .GetDeviceRunTime(project.Identity, device.Identity, date);
                                var runningTime = new RunningTime
                                {
                                    DomainId = device.DomainId,
                                    UpdateTime = date,
                                    ProjectIdentity = project.Identity,
                                    RunningTimeSpan = runTime,
                                    DeviceIdentity = device.Identity,
                                    Type = GetRunningType(commandData)
                                };
                                ProcessInvoke.Instance<RunningTimeProcess>().StoreRunningTime(runningTime);
                            }
                            catch (Exception ex)
                            {
                                LogService.Instance.Error("Add HourStatistics Exception", ex);
                            }
                        }
                        var devRunTime = ProcessInvoke.Instance<HotelRestaurantProcess>()
                            .GetDeviceRunTime(project.Identity, device.Identity, date);
                        var devRunningTime = new RunningTime
                        {
                            DomainId = device.DomainId,
                            UpdateTime = date,
                            ProjectIdentity = project.Identity,
                            RunningTimeSpan = devRunTime,
                            DeviceIdentity = device.Identity,
                            Type = RunningTimeType.Device
                        };
                        ProcessInvoke.Instance<RunningTimeProcess>().StoreRunningTime(devRunningTime);
                    }
                }
            }
        }

        private static RunningTimeType GetRunningType(Guid dataId)
        {
            if (dataId == CommandDataId.CleanerCurrent) return RunningTimeType.Cleaner;
            if (dataId == CommandDataId.FanCurrent) return RunningTimeType.Fan;
            if (dataId == CommandDataId.LampblackInCon) return RunningTimeType.Lampbalck;
            return RunningTimeType.Lampbalck;
        }
    }
}