using System;
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
            var devs = GetHotelDevices(hotelIdentity);
            foreach (var dev in devs)
            {
                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理设备小时历史数据，设备名称：{dev.DeviceCode}");
                foreach (var data in ProduceDatas)
                {
                    if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id))
                    {
                        continue;
                    }

                    Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理小时历史数据，数据名称：{data.DataName}");
                    var lastDate = ProcessInvoke.Instance<DataStatisticsProcess>()
                        .GetLastUpdateDataDate(obj => obj.Type == StatisticsType.Hour && obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id);

                    var startDate = lastDate == DateTime.MinValue
                        ? ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id).UpdateTime
                        : lastDate;

                    startDate = startDate.GetCurrentHour();

                    _produceEndDayHour = ProcessInvoke.Instance<MonitorDataProcess>()
                        .GetLastUpdateDataDate(obj => obj.ProjectIdentity == hotelIdentity &&
                                                      obj.DeviceIdentity == dev.Identity
                                                      && obj.CommandDataId == data.Id && obj.DataChannel == 0 &&
                                                      obj.DoubleValue != null);
                    while (startDate < _produceEndDayHour)
                    {
                        var endDate = startDate.AddHours(1);
                        var date = startDate;
                        var min = ProcessInvoke.Instance<MonitorDataProcess>()
                            .GetHotelAverage(obj =>
                                obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.UpdateTime > date && obj.UpdateTime < endDate
                                && obj.CommandDataId == data.Id && obj.DataChannel == 0 && obj.DoubleValue != null, out double avg);

                        if (min == null)
                        {
                            startDate = startDate.AddHours(1);
                            continue;
                        }

                        StoreDataStatistic(min, endDate, StatisticsType.Hour, avg);

                        startDate = startDate.AddHours(1);
                    }
                }
            }
        }

        private static void ProduceDayDatas(long hotelIdentity)
        {
            var devs = GetHotelDevices(hotelIdentity);
            foreach (var dev in devs)
            {
                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理设备每日历史数据，设备名称：{dev.DeviceCode}");
                foreach (var data in ProduceDatas)
                {
                    if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id))
                    {
                        continue;
                    }
                    Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理每日历史数据，数据名称：{data.DataName}");
                    var lastDate = ProcessInvoke.Instance<DataStatisticsProcess>()
                        .GetLastUpdateDataDate(obj => obj.Type == StatisticsType.Day && obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id);

                    var startDate = lastDate == DateTime.MinValue
                        ? ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id).UpdateTime
                        : lastDate;

                    startDate = startDate.GetToday();

                    while (startDate < _produceEndDay)
                    {
                        var endDate = startDate.AddDays(1);
                        var date = startDate;
                        var min = ProcessInvoke.Instance<MonitorDataProcess>()
                            .GetMinHotelMonitorData(obj =>
                                obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.UpdateTime > date && obj.UpdateTime < endDate
                                && obj.CommandDataId == data.Id && obj.DataChannel == 0);

                        var avg = ProcessInvoke.Instance<DataStatisticsProcess>().GetDayAverage(
                            d => d.CommandDataId == data.Id && d.DataChannel == 0
                                 && d.ProjectIdentity == hotelIdentity && d.DeviceIdentity == dev.Identity &&
                                 d.UpdateTime > date && d.UpdateTime < endDate);

                        if (min == null)
                        {
                            startDate = startDate.AddDays(1);
                            continue;
                        }

                        StoreDataStatistic(min, endDate, StatisticsType.Day, avg);

                        startDate = startDate.AddDays(1);
                    }
                }
            }
        }

        private static void ProduceDayRunningTime(long hotelIdentity)
        {
            var devs = GetHotelDevices(hotelIdentity);
            foreach (var dev in devs)
            {
                foreach (var data in RunningTimeDatas)
                {
                    if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity && obj.CommandDataId == data.Id))
                    {
                        continue;
                    }
                    Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理每日运行时间数据，数据名称：{data.DataName}");
                    var process = ProcessInvoke.Instance<RunningTimeProcess>();

                    var type = GetRunningType(data.DataName);
                    var lastDate = process.LastRecordDateTime(hotelIdentity, dev.Identity, type);
                    var firstMonitorData = ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity
                                                                                                        && obj.DeviceIdentity == dev.Identity
                                                                                                        && obj.CommandDataId == data.Id
                                                                                                        && obj.DataChannel == 0);
                    var startDate = lastDate == DateTime.MinValue
                        ? firstMonitorData.UpdateTime
                        : lastDate.AddDays(1);

                    while (startDate <= _produceEndDay)
                    {
                        var date = startDate;
                        var runTime = ProcessInvoke.Instance<HotelRestaurantProcess>()
                            .GetRunTime(hotelIdentity, dev.Identity, date, data.Id);

                        var runningTime = new RunningTimeRepository().CreateDefaultModel();
                        runningTime.UpdateTime = date.GetToday();
                        runningTime.ProjectIdentity = hotelIdentity;
                        runningTime.RunningTimeSpan = runTime;
                        runningTime.DeviceIdentity = dev.Identity;
                        runningTime.Type = type;

                        ProcessInvoke.Instance<RunningTimeProcess>().StoreRunningTime(runningTime);

                        startDate = startDate.AddDays(1);
                    }
                }
            }
        }

        private static void ProduceDeviceDayRunningTime(long hotelIdentity)
        {
            var devs = GetHotelDevices(hotelIdentity);
            foreach (var dev in devs)
            {
                if (!ProcessInvoke.Instance<MonitorDataProcess>().GetDataCount(obj => obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == dev.Identity))
                {
                    return;
                }
                Log($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】开始处理每日设备运行时间数据。");
                var process = ProcessInvoke.Instance<RunningTimeProcess>();

                var lastDate = process.LastRecordDateTime(hotelIdentity, dev.Identity, RunningTimeType.Device);
                var firstMonitorData = ProcessInvoke.Instance<MonitorDataProcess>().GetFirst(obj => obj.ProjectIdentity == hotelIdentity
                && obj.DeviceIdentity == dev.Identity);
                var startDate = lastDate == DateTime.MinValue
                    ? firstMonitorData.UpdateTime
                    : lastDate.AddDays(1);
                startDate = startDate.GetToday();
                while (startDate <= _produceEndDay)
                {
                    var date = startDate;
                    var runTime = ProcessInvoke.Instance<HotelRestaurantProcess>()
                        .GetDeviceRunTime(hotelIdentity, dev.Identity, date);

                    var runningTime = new RunningTimeRepository().CreateDefaultModel();
                    runningTime.UpdateTime = date.GetToday();
                    runningTime.ProjectIdentity = hotelIdentity;
                    runningTime.RunningTimeSpan = runTime;
                    runningTime.DeviceIdentity = dev.Identity;
                    runningTime.Type = RunningTimeType.Device;
                    ProcessInvoke.Instance<RunningTimeProcess>().StoreRunningTime(runningTime);

                    startDate = startDate.AddDays(1);
                }
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

        private static void StoreDataStatistic(MonitorData data, DateTime endDate, StatisticsType type, double avg)
        {
            var dataStatistic = new DataStatisticsRepository().CreateDefaultModel();
            dataStatistic.DoubleValue = avg;
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

        private static List<Device> GetHotelDevices(long projectIdentity)
            => ProcessInvoke.Instance<DeviceProcess>().GetProjectDevices(projectIdentity);

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
