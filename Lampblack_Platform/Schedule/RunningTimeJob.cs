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
                foreach (var project in ctx.Projects.Select(p => new { p.Id, p.Identity }).ToList())
                {
                    foreach (var device in ctx.Devices.Where(d => d.ProjectId == project.Id).Select(d => new { d.DomainId, d.Identity }).ToList())
                    {
                        foreach (var commandData in commandDatas)
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
                            ctx.RunningTimes.Add(runningTime);
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
                        ctx.RunningTimes.Add(devRunningTime);
                    }
                }
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error("Add RunningTime Exception", ex);
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