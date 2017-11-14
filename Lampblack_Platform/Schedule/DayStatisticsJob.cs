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
                    foreach (var project in ctx.Projects)
                    {
                        foreach (var device in ctx.Devices.Where(d => d.ProjectId == project.Id))
                        {
                            try
                            {
                                var query = ctx.DataStatisticses.Where(m => m.Type == StatisticsType.Hour
                                                                        && m.ProjectIdentity == project.Identity
                                                                        && m.DeviceIdentity == device.Identity
                                                                        && m.UpdateTime > startTime
                                                                        && m.UpdateTime < endTime
                                                                        && m.CommandDataId == commandData
                                                                        && m.DoubleValue != null);
                                var count = query.Count();
                                StoreDataStatistics(endTime, StatisticsType.Day,
                                    count <= 0 ? 0 : query.Average(q => q.DoubleValue.Value), commandData, device,
                                    project.Identity);
                            }
                            catch (Exception ex)
                            {
                                LogService.Instance.Error("Add HourStatistics Exception", ex);
                            }
                        }
                    }
                }
            }
        }

        private static void StoreDataStatistics(DateTime endDate, StatisticsType type, double avg, Guid commandDataId, Device device, long projectIdentity)
        {
            var dataStatistic = new DataStatistics
            {
                DomainId = device.DomainId,
                DoubleValue = avg,
                CommandDataId = commandDataId,
                DataChannel = 0,
                DeviceIdentity = projectIdentity,
                ProjectIdentity = device.Identity,
                Type = type,
                UpdateTime = endDate
            };

            var ctx = new RepositoryDbContext();
            ctx.DataStatisticses.Add(dataStatistic);
            ctx.SaveChanges();
        }
    }
}