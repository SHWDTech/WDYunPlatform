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
        private static List<CommandData> _produceDatas;

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

            Log($"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】任务执行完成！");
            
            Quit();
        }

        static void ProduceHotels(string type)
        {
            foreach (var hotel in _hotelGuids)
            {
                Log($"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】开始处理酒店历史数据，酒店名称：{hotel.ProjectName}");
                try
                {
                    switch (type)
                    {
                        case "hour":
                            ProduceHourDatas(hotel.Id);
                            break;
                        case "day":
                            ProduceDayDatas(hotel.Id);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error($"更新数据错误，更新类型：{type}", ex);
                }
            }
        }

        static void ProduceHourDatas(Guid hotelGuid)
        {
            foreach (var data in _produceDatas)
            {
                if (ProcessInvoke.GetInstance<MonitorDataProcess>().GetDataCount(obj => obj.CommandDataId == data.Id && obj.ProjectId == hotelGuid) == 0)
                {
                   continue;
                }

                Log($"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】开始处理小时历史数据，数据名称：{data.DataName}");
                var lastDate = ProcessInvoke.GetInstance<DataStatisticsProcess>()
                        .GetLastUpdateDataDate(obj => obj.CommandDataId == data.Id && obj.ProjectId == hotelGuid && obj.Type == StatisticsType.Hour);

                var startDate = lastDate == DateTime.MinValue 
                    ? ProcessInvoke.GetInstance<MonitorDataProcess>().GetFirst(obj => obj.CommandDataId == data.Id && obj.ProjectId == hotelGuid).UpdateTime 
                    : lastDate;

                startDate = startDate.GetCurrentHour();

                while (startDate < _produceEndDayHour)
                {
                    var endDate = startDate.AddHours(1);
                    var date = startDate;
                    var min = ProcessInvoke.GetInstance<MonitorDataProcess>()
                        .GetMinHotelMonitorData(obj =>
                                obj.UpdateTime > date && obj.UpdateTime < endDate && obj.ProjectId == hotelGuid &&
                                obj.CommandDataId == data.Id);

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

        private static void ProduceDayDatas(Guid hotelGuid)
        {
            foreach (var data in _produceDatas)
            {
                if (ProcessInvoke.GetInstance<MonitorDataProcess>().GetDataCount(obj => obj.CommandDataId == data.Id && obj.ProjectId == hotelGuid) == 0)
                {
                    continue;
                }
                Log($"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】开始处理每日历史数据，数据名称：{data.DataName}");
                var lastDate = ProcessInvoke.GetInstance<DataStatisticsProcess>()
                        .GetLastUpdateDataDate(obj => obj.CommandDataId == data.Id && obj.ProjectId == hotelGuid && obj.Type == StatisticsType.Day);

                var startDate = lastDate == DateTime.MinValue
                    ? ProcessInvoke.GetInstance<MonitorDataProcess>().GetFirst(obj => obj.CommandDataId == data.Id && obj.ProjectId == hotelGuid).UpdateTime
                    : lastDate;

                startDate = startDate.GetToday();

                while (startDate < _produceEndDay)
                {
                    var endDate = startDate.AddDays(1);
                    var date = startDate;
                    var min = ProcessInvoke.GetInstance<MonitorDataProcess>()
                        .GetMinHotelMonitorData(obj =>
                                obj.UpdateTime > date && obj.UpdateTime < endDate && obj.ProjectId == hotelGuid &&
                                obj.CommandDataId == data.Id);

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

        private static void StoreDataStatistic(MonitorData data, DateTime endDate, StatisticsType type)
        {
            var dataStatistic = DataStatisticsRepository.CreateDefaultModel();
            dataStatistic.DoubleValue = data.DoubleValue;
            dataStatistic.CommandDataId = data.CommandDataId;
            dataStatistic.DataChannel = data.DataChannel;
            dataStatistic.DeviceId = data.DeviceId;
            dataStatistic.ProjectId = data.ProjectId;
            dataStatistic.Type = type;
            dataStatistic.UpdateTime = endDate;

            ProcessInvoke.GetInstance<DataStatisticsProcess>().StoreDataStatistic(dataStatistic);
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

            _produceDatas = new List<CommandData>();
            using (var context = new RepositoryDbContext())
            {
                foreach (var data in AppConfig.CommandDatas)
                {
                    _produceDatas.Add(context.Set<CommandData>().First(obj => obj.DataName == data));
                }
            }

            using (var context = new RepositoryDbContext())
            {
                _hotelGuids = context.Set<HotelRestaurant>().ToList();
            }

            _produceEndDayHour = DateTime.Now.AddHours(-1).GetCurrentHour();
            _produceEndDay = DateTime.Now.AddDays(-1).GetToday();


            Log($"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】系统初始化完成。");
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
