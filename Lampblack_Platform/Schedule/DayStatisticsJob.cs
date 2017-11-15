using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.ExtensionMethod;

namespace Lampblack_Platform.Schedule
{
    public class DayStatisticsJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var commandDatas = (List<Guid>)context.JobDetail.JobDataMap.Get("commandDatas");
            var endTime = DateTime.Now.GetCurrentHour();
            var startTime = endTime.AddDays(-1);
            using (var ctx = new RepositoryDbContext())
            {
                foreach (var commandData in commandDatas)
                {
                    foreach (var project in ctx.Projects.Select(p => new { p.Id, p.Identity }).ToList())
                    {
                        foreach (var device in ctx.Devices.Where(d => d.ProjectId == project.Id).Select(d => new { d.DomainId, d.Identity }).ToList())
                        {
                            var query = ctx.DataStatisticses.Where(m => m.Type == StatisticsType.Hour
                                                                        && m.ProjectIdentity == project.Identity
                                                                        && m.DeviceIdentity == device.Identity
                                                                        && m.UpdateTime > startTime
                                                                        && m.UpdateTime < endTime
                                                                        && m.CommandDataId == commandData
                                                                        && m.DoubleValue != null);
                            var count = query.Count();
                            var dataStatistic = new DataStatistics
                            {
                                DomainId = device.DomainId,
                                DoubleValue = count <= 0 ? 0 : query.Average(q => q.DoubleValue.Value),
                                CommandDataId = commandData,
                                DataChannel = 0,
                                ProjectIdentity = project.Identity,
                                DeviceIdentity = device.Identity,
                                Type = StatisticsType.Day,
                                UpdateTime = endTime
                            };
                            ctx.DataStatisticses.Add(dataStatistic);
                        }
                    }
                }
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error("Add DayStatistics Exception", ex);
                }
            }
        }
    }
}