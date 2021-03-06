﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.ExtensionMethod;

namespace AutoDataStaticService
{
    class Program
    {
        /// <summary>
        /// 需要计算的指令数据
        /// </summary>
        private static readonly List<CommandData> ProduceDatas = new List<CommandData>();

        /// <summary>
        /// 运行时间指令数据
        /// </summary>
        private static readonly List<CommandData> RunningTimeDatas = new List<CommandData>();

        /// <summary>
        /// 需要更新的餐饮酒店
        /// </summary>
        private static List<HotelRestaurant> _hotelGuids;

        /// <summary>
        /// 执行结束时间
        /// </summary>
        private static DateTime _produceEndDayHour;

        /// <summary>
        /// 执行结束日期
        /// </summary>
        private static DateTime _produceEndDay;

        /// <summary>
        /// 执行类型
        /// </summary>
        private static string _produceType;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly StringBuilder LogStringBuilder = new StringBuilder();

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Log("Incorrect Arg Numbers!");
                Quit();
                return;
            }

            _produceType = args[0];

            if (!Init())
            {
                Quit();
                return;
            }

            ProduceHotels(_produceType);

            Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】任务执行完成！");

            Quit();
        }

        static void ProduceHotels(string type)
        {
            foreach (var hotel in _hotelGuids)
            {
                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理酒店历史数据，酒店名称：{hotel.ProjectName}");
                try
                {
                    switch (type)
                    {
                        case "hour":
                            ProduceHourDatas(hotel.Identity);
                            break;
                        case "day":
                            ProduceDayDatas(hotel.Identity);
                            break;
                        case "running":
                            ProduceDayRunningTime(hotel.Identity);
                            ProduceDeviceDayRunningTime(hotel.Identity);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error($"更新数据错误，更新类型：{type}", ex);
                }
            }
        }

        static void ProduceHourDatas(long hotelIdentity)
        {
            foreach (var data in ProduceDatas)
            {
                if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity  && obj.CommandDataId == data.Id))
                {
                    continue;
                }

                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理小时历史数据，数据名称：{data.DataName}");
                var lastDate = ProcessInvoke.Instance<DataStatisticsProcess>()
                        .GetLastUpdateDataDate(obj => obj.Type == StatisticsType.Hour && obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id);

                var startDate = lastDate == DateTime.MinValue
                    ? ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id).UpdateTime
                    : lastDate;

                startDate = startDate.GetCurrentHour();

                while (startDate < _produceEndDayHour)
                {
                    var endDate = startDate.AddHours(1);
                    var date = startDate;
                    var min = ProcessInvoke.Instance<MonitorDataProcess>()
                        .GetMinHotelMonitorData(obj =>
                                obj.ProjectIdentity == hotelIdentity && obj.UpdateTime > date && obj.UpdateTime < endDate
                                && obj.CommandDataId == data.Id);

                    if (min == null)
                    {
                        startDate = startDate.AddHours(1);
                        continue;
                    }

                    StoreDataStatistic(min, endDate, StatisticsType.Hour);

                    startDate = startDate.AddHours(1);
                }
            }
        }

        private static void ProduceDayDatas(long hotelIdentity)
        {
            foreach (var data in ProduceDatas)
            {
                if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id))
                {
                    continue;
                }
                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理每日历史数据，数据名称：{data.DataName}");
                var lastDate = ProcessInvoke.Instance<DataStatisticsProcess>()
                        .GetLastUpdateDataDate(obj => obj.Type == StatisticsType.Day && obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id);

                var startDate = lastDate == DateTime.MinValue
                    ? ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id).UpdateTime
                    : lastDate;

                startDate = startDate.GetToday();

                while (startDate < _produceEndDay)
                {
                    var endDate = startDate.AddDays(1);
                    var date = startDate;
                    var min = ProcessInvoke.Instance<MonitorDataProcess>()
                        .GetMinHotelMonitorData(obj =>
                                obj.ProjectIdentity == hotelIdentity && obj.UpdateTime > date && obj.UpdateTime < endDate
                                && obj.CommandDataId == data.Id);

                    if (min == null)
                    {
                        startDate = startDate.AddDays(1);
                        continue;
                    }

                    StoreDataStatistic(min, endDate, StatisticsType.Day);

                    startDate = startDate.AddDays(1);
                }
            }
        }

        private static void ProduceDayRunningTime(long hotelIdentity)
        {
            foreach (var data in RunningTimeDatas)
            {
                if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id))
                {
                    continue;
                }
                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理每日运行时间数据，数据名称：{data.DataName}");
                var process = ProcessInvoke.Instance<RunningTimeProcess>();

                var type = GetRunningType(data.DataName);
                var lastDate = process.LastRecordDateTime(hotelIdentity, type);
                var firstMonitorData = ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity && obj.CommandDataId == data.Id);
                var startDate = lastDate == DateTime.MinValue
                        ? firstMonitorData.UpdateTime
                        : lastDate.AddDays(1);

                while (startDate <= _produceEndDay)
                {
                    var date = startDate;
                    var runTime = ProcessInvoke.Instance<HotelRestaurantProcess>()
                        .GetRunTime(hotelIdentity, date, data.Id);

                    var runningTime = new RunningTimeRepository().CreateDefaultModel();
                    runningTime.UpdateTime = date.GetToday();
                    runningTime.ProjectIdentity = hotelIdentity;
                    runningTime.RunningTimeSpan = runTime;
                    runningTime.DeviceIdentity = firstMonitorData.DeviceIdentity;
                    runningTime.Type = type;

                    ProcessInvoke.Instance<RunningTimeProcess>().StoreRunningTime(runningTime);

                    startDate = startDate.AddDays(1);
                }
            }
        }

        private static void ProduceDeviceDayRunningTime(long hotelIdentity)
        {
            if (ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity))
            {
                return;
            }
            Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理每日设备运行时间数据。");
            var process = ProcessInvoke.Instance<RunningTimeProcess>();

            var lastDate = process.LastRecordDateTime(hotelIdentity, RunningTimeType.Device);
            var firstMonitorData = ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity);
            var startDate = lastDate == DateTime.MinValue
                    ? firstMonitorData.UpdateTime
                    : lastDate.AddDays(1);

            while (startDate <= _produceEndDay)
            {
                var date = startDate;
                var runTime = ProcessInvoke.Instance<HotelRestaurantProcess>()
                    .GetDeviceRunTime(hotelIdentity, date);

                var runningTime = new RunningTimeRepository().CreateDefaultModel();
                runningTime.UpdateTime = date.GetToday();
                runningTime.ProjectIdentity = hotelIdentity;
                runningTime.RunningTimeSpan = runTime;
                runningTime.DeviceIdentity = firstMonitorData.DeviceIdentity;
                runningTime.Type = RunningTimeType.Device;
                ProcessInvoke.Instance<RunningTimeProcess>().StoreRunningTime(runningTime);

                startDate = startDate.AddDays(1);
            }
        }

        private static RunningTimeType GetRunningType(string dataName)
        {
            switch (dataName)
            {
                case ProtocolDataName.CleanerSwitch:
                    return RunningTimeType.Cleaner;
                case ProtocolDataName.FanSwitch:
                    return RunningTimeType.Fan;
                default:
                    return RunningTimeType.Device;
            }
        }

        private static void StoreDataStatistic(MonitorData data, DateTime endDate, StatisticsType type)
        {
            var dataStatistic = new DataStatisticsRepository().CreateDefaultModel();
            dataStatistic.DoubleValue = data.DoubleValue;
            dataStatistic.CommandDataId = data.CommandDataId;
            dataStatistic.DataChannel = data.DataChannel;
            dataStatistic.DeviceIdentity = data.DeviceIdentity;
            dataStatistic.ProjectIdentity = data.ProjectIdentity;
            dataStatistic.Type = type;
            dataStatistic.UpdateTime = endDate;

            ProcessInvoke.Instance<DataStatisticsProcess>().StoreDataStatistic(dataStatistic);
        }

        /// <summary>
        /// 初始化基本变量
        /// </summary>
        /// <returns></returns>
        static bool Init()
        {
            var serverUser = GeneralProcess.GetUserByLoginName(AppConfig.ServerAccount);

            if (serverUser == null)
            {
                Log("通信管理员账号信息错误，请检查配置！");
                return false;
            }

            ProcessInvoke.SetupGlobalRepositoryContext(serverUser, serverUser.Domain);

            using (var context = new RepositoryDbContext())
            {
                foreach (var data in AppConfig.CommandDatas)
                {
                    ProduceDatas.Add(context.Set<CommandData>().First(obj => obj.DataName == data));
                }

                foreach (var data in AppConfig.RunningTimeDatas)
                {
                    RunningTimeDatas.Add(context.Set<CommandData>().First(obj => obj.DataName == data));
                }

                _hotelGuids = context.Set<HotelRestaurant>().ToList();
            }

            _produceEndDayHour = DateTime.Now.AddHours(-1).GetCurrentHour();
            _produceEndDay = DateTime.Now.AddDays(-1).GetToday();

            Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】系统初始化完成。");
            return true;
        }

        private static void Log(string log)
        {
            Console.WriteLine(log);
            LogStringBuilder.Append($"{log}\r\n");
        }

        private static void Quit()
        {
            LogService.Instance.Info(LogStringBuilder.ToString());
        }
    }
}
