using System;
using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Model;

namespace TestConsole
{
    class RecordTransfer
    {
        public static void StartTransfer()
        {
            var db = new RepositoryDbContext();
            var index = 2572197 - 1;
            var max = db.ProtocolDatas.OrderByDescending(obj => obj.Id).First().Id;
            while (index < max)
            {
                index++;
                Console.WriteLine($@"Current Index:{index}");
                var pdata = db.ProtocolDatas.FirstOrDefault(obj => obj.Id == index);
                if (pdata == null) continue;
                var hotel = db.RestaurantDevices.FirstOrDefault(obj => obj.Identity == pdata.DeviceIdentity).Hotel;
                var monitorDatas = db.MonitorDatas.Where(obj => obj.ProjectIdentity == hotel.Identity && obj.DeviceIdentity == pdata.DeviceIdentity && obj.ProtocolDataId == index).ToList();
                if (monitorDatas.Count <= 0) continue;
                Console.WriteLine($@"Transfer Start,ProtocolId: {index}");
                var record = new LampblackRecord
                {
                    ProjectIdentity = monitorDatas[0].ProjectIdentity,
                    DeviceIdentity = monitorDatas[0].DeviceIdentity,
                    ProtocolId = index,
                    //CleanerSwitch = monitorDatas.FirstOrDefault(d => d.CommandDataId == Guid.Parse("15802959-D25B-42AD-BE50-5B48DCE4039A"))?.BooleanValue.Value ?? false,
                    //CleanerCurrent = (int)monitorDatas.FirstOrDefault(d => d.CommandDataId == Guid.Parse("EEE9EC55-7E84-4176-BB90-C13962352BC2"))?.DoubleValue.Value,
                    //FanSwitch = monitorDatas.FirstOrDefault(d => d.CommandDataId == Guid.Parse("ADCE87E7-AEF2-4548-AA1E-FB435B72834F"))?.BooleanValue.Value ?? false,
                    //FanCurrent = (int)monitorDatas.FirstOrDefault(d => d.CommandDataId == Guid.Parse("01323F2C-70C9-4073-A58C-77F10C819F9C"))?.DoubleValue.Value,
                    //LampblackIn = (int)monitorDatas.FirstOrDefault(d => d.CommandDataId == Guid.Parse("F15B955E-AF42-44A5-A547-E1E2E7CDAC1D"))?.DoubleValue.Value,
                    //LampblackOut = (int)monitorDatas.FirstOrDefault(d => d.CommandDataId == Guid.Parse("D0E478AE-836A-45EB-BA93-32FDF1CBEE61"))?.DoubleValue.Value,
                    RecordDateTime = monitorDatas[0].UpdateTime,
                    DomainId = pdata.DomainId
                };

                var cc =
                    monitorDatas.FirstOrDefault(
                        d => d.CommandDataId == Guid.Parse("EEE9EC55-7E84-4176-BB90-C13962352BC2"));
                if (cc != null)
                {
                    record.CleanerCurrent = (int) cc.DoubleValue;
                    record.CleanerSwitch = record.CleanerCurrent > 4;
                }

                var fs =
                    monitorDatas.FirstOrDefault(
                        d => d.CommandDataId == Guid.Parse("ADCE87E7-AEF2-4548-AA1E-FB435B72834F"));
                if (fs != null)
                {
                    record.FanSwitch = fs.BooleanValue.Value;
                }

                var fc =
                    monitorDatas.FirstOrDefault(
                        d => d.CommandDataId == Guid.Parse("01323F2C-70C9-4073-A58C-77F10C819F9C"));
                if (fc != null)
                {
                    record.FanCurrent = (int) fc.DoubleValue;
                }

                var lmi =
                    monitorDatas.FirstOrDefault(
                        d => d.CommandDataId == Guid.Parse("F15B955E-AF42-44A5-A547-E1E2E7CDAC1D"));
                if (lmi != null)
                {
                    record.LampblackIn = (int) lmi.DoubleValue;
                }

                var lmo =
                    monitorDatas.FirstOrDefault(
                        d => d.CommandDataId == Guid.Parse("D0E478AE-836A-45EB-BA93-32FDF1CBEE61"));
                if (lmo != null)
                {
                    record.LampblackOut = (int)lmo.DoubleValue;
                }


                db.LampblackRecords.Add(record);
                try
                {
                    db.SaveChanges();
                    Console.WriteLine(@"Save Successed!");
                }
                catch
                {
                    Console.WriteLine(@"Save Failed!");
                }
            }
        }
    }
}
