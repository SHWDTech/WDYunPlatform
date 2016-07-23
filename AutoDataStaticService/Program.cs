using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility.ExtensionMethod;

namespace AutoDataStaticService
{
    class Program
    {
        static void Main()
        {
            if (!Init())
            {
                return;
            }

            var start = DateTime.Now.GetPreviousHour();
            var end = DateTime.Now.GetCurrentHour();

            List<Guid> hotels;
            using (var context = new RepositoryDbContext())
            {
                hotels = context.Set<HotelRestaurant>().Select(obj => obj.Id).ToList();
            }

            var cmdData = Guid.Parse("EEE9EC55-7E84-4176-BB90-C13962352BC2");
            foreach (var hotel in hotels)
            {
                var data = ProcessInvoke.GetInstance<MonitorDataProcess>()
                    .GetMinHotelMonitorData(obj => obj.UpdateTime > start && obj.UpdateTime < end && obj.ProjectId == hotel && obj.CommandDataId == cmdData);
                if (data != null)
                {
                    Console.WriteLine($"dataValue:{data.DoubleValue}.datatime{data.UpdateTime}");
                }

                var data2 = ProcessInvoke.GetInstance<MonitorDataProcess>()
                    .GetMaxHotelMonitorData(obj => obj.UpdateTime > start && obj.UpdateTime < end && obj.ProjectId == hotel && obj.CommandDataId == cmdData);
                if (data2 != null)
                {
                    Console.WriteLine($"dataValue:{data2.DoubleValue}.datatime{data2.UpdateTime}");
                }
            }

            Console.ReadKey();
        }

        static bool Init()
        {
            var serverUser = GeneralProcess.GetUserByLoginName(AppConfig.ServerAccount);

            if (serverUser == null)
            {
                Console.WriteLine("通信管理员账号信息错误，请检查配置！");
                return false;
            }

            ProcessInvoke.SetupGlobalRepositoryContext(serverUser, serverUser.Domain);

            return true;
        }
    }
}
