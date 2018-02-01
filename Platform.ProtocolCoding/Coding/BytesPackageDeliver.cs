using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using WebViewModels.ViewDataModel;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    public class BytesPackageDeliver : PackageDeliver<byte[]>
    {
        private readonly BytesDataConverter _dataConverter = new BytesDataConverter();

        /// <summary>
        /// 存储协议包携带的数据
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">协议包来源</param>
        public void StoreData(IProtocolPackage<byte[]> package, IPackageSource source)
        {
            var monitorDataList = new List<MonitorData>();

            for (var i = 0; i < package.Command.CommandDatas.Count; i++)
            {
                using (var monitorDataRepository = new MonitorDataRepository())
                {
                    var monitorData = monitorDataRepository.CreateDefaultModel();
                    monitorData.DomainId = package.Device.DomainId;

                    var commandData = package.Command.CommandDatas.First(obj => obj.DataIndex == i);

                    var temp = _dataConverter.DecodeComponentData(package[commandData.DataName]);

                    monitorData.DoubleValue = Convert.ToDouble(temp);
                    monitorData.ProtocolDataId = package.ProtocolData.Id;
                    monitorData.UpdateTime = DateTime.Now;
                    monitorData.CommandDataId = commandData.Id;
                    monitorData.DeviceIdentity = package.Device.Identity;
                    monitorData.ProjectIdentity = package.Device.Project.Identity;

                    monitorDataList.Add(monitorData);
                }
            }

            if (package[ProtocolDataName.DataValidFlag] != null)
            {
                ProcessDataValidFlag(package, monitorDataList);
            }

            lock (TempMonitorDatas)
            {
                TempMonitorDatas.AddRange(monitorDataList);
            }

            OnMonitorDataReceived();
        }

        //解析完成后，把协议内的数据转换成数据库表记录。旧数据表，有部分业务用到。
        public void Lampblack(IProtocolPackage<byte[]> package, IPackageSource source)
        {
            var monitorDataList = new List<MonitorData>();

            foreach (var dataComponent in package.DataComponents)
            {
                if (dataComponent.Value.CommandData.DataName == null ||
                    dataComponent.Value.CommandData.DataConvertType == ProtocolDataType.None) continue;
                var data = _dataConverter.DecodeComponentData(dataComponent.Value);
                using (var monitorDataRepository = new MonitorDataRepository())
                {
                    var monitorData = monitorDataRepository.CreateDefaultModel();
                    monitorData.DomainId = package.Device.DomainId;

                    switch (dataComponent.Value.CommandData.DataValueType)
                    {
                        case DataValueType.Double:
                            monitorData.DoubleValue = Convert.ToDouble(data);
                            break;
                        case DataValueType.Integer:
                            monitorData.IntegerValue = Convert.ToInt32(data);
                            break;
                        case DataValueType.Boolean:
                            monitorData.BooleanValue = Convert.ToBoolean(data);
                            break;
                    }

                    monitorData.ProtocolDataId = package.ProtocolData.Id;
                    monitorData.UpdateTime = DateTime.Now;
                    monitorData.CommandDataId = dataComponent.Value.CommandData.Id;
                    monitorData.DeviceIdentity = package.Device.Identity;
                    monitorData.ProjectIdentity = package.Device.Project.Identity;
                    monitorData.DataIsValid = (dataComponent.Value.ValidFlag & 0x80) == 0;
                    monitorData.DataChannel = dataComponent.Value.ComponentChannel;

                    monitorDataList.Add(monitorData);

                    if (dataComponent.Value.CommandData.DataName != "CleanerCurrent") continue;
                    var cleanerSwitch = new MonitorData
                    {
                        DomainId = package.Device.DomainId,
                        ProtocolDataId = package.ProtocolData.Id,
                        UpdateTime = DateTime.Now,
                        CommandDataId = dataComponent.Value.CommandData.Id,
                        DeviceIdentity = package.Device.Identity,
                        ProjectIdentity = package.Device.Project.Identity,
                        DataIsValid = true,
                        DataChannel = dataComponent.Value.ComponentChannel
                    };
                    if (monitorData.DoubleValue > 4)
                    {
                        cleanerSwitch.BooleanValue = true;
                    }
                    monitorDataList.Add(cleanerSwitch);
                    if (package.Device.DomainId == Guid.Parse("C11B87A8-F4D7-4850-8000-C850953B2496"))
                    {
                        var fans = monitorDataList.Where(d => d.CommandDataId == Guid.Parse("01323F2C-70C9-4073-A58C-77F10C819F9C")).ToList();
                        foreach (var fan in fans)
                        {
                            var current = monitorDataList.FirstOrDefault(d =>
                                d.CommandDataId == Guid.Parse("EEE9EC55-7E84-4176-BB90-C13962352BC2") &&
                                d.DataChannel == fan.DataChannel);
                            if (current != null)
                            {
                                current.DoubleValue = Math.Round(fan.DoubleValue / 90.0f ?? 0, 2);
                            }
                            fan.DoubleValue = 0;
                        }

                        var switchs = monitorDataList.Where(d =>
                            d.CommandDataId == Guid.Parse("15802959-D25B-42AD-BE50-5B48DCE4039A")).ToList();
                        foreach (var @switch in switchs)
                        {
                            var current = monitorDataList.FirstOrDefault(d =>
                                d.CommandDataId == Guid.Parse("EEE9EC55-7E84-4176-BB90-C13962352BC2") &&
                                d.DataChannel == @switch.DataChannel);
                            if (current != null)
                            {
                                @switch.BooleanValue = current.DoubleValue > 4;
                            }
                        }
                    }
                }
            }

            lock (TempMonitorDatas)
            {
                TempMonitorDatas.AddRange(monitorDataList);
                LampblackRecord(package);
            }

            OnMonitorDataReceived();
        }

        //解析完成后，把协议内的数据转换成数据库表记录。新数据表，主要用这个表的数据。
        private void LampblackRecord(IProtocolPackage<byte[]> package)
        {
            var dev = (RestaurantDevice)package.Device;
            var records = new List<LampblackRecord>();
            var current = 0;

            while (dev.InUsingChannels.Contains(current + 1) && current < dev.ChannelCount)
            {
                var record = new LampblackRecord
                {
                    ProjectIdentity = dev.Project.Identity,
                    DeviceIdentity = dev.Identity,
                    ProtocolId = package.ProtocolData.Id,
                    CleanerCurrent = Convert.ToInt32(DecodeComponentDataByName($"CleanerCurrent-{current}", package)),
                    FanSwitch = Convert.ToBoolean(DecodeComponentDataByName($"FanSwitch-{current}", package)),
                    FanCurrent = Convert.ToInt32(DecodeComponentDataByName($"FanCurrent-{current}", package)),
                    LampblackIn = Convert.ToInt32(DecodeComponentDataByName($"LampblackInCon-{current}", package)),
                    LampblackOut = Convert.ToInt32(DecodeComponentDataByName($"LampblackOutCon-{current}", package)),
                    RecordDateTime = DateTime.Now,
                    Channel = current + 1,
                    DomainId = package.Device.DomainId
                };
                if (package.Device.DomainId == Guid.Parse("C11B87A8-F4D7-4850-8000-C850953B2496"))
                {
                    record.CleanerCurrent =
                        Math.Round(Convert.ToInt32(DecodeComponentDataByName($"FanCurrent-{current}", package)) / 90.0f, 2);
                    record.CleanerSwitch =
                        Convert.ToBoolean(DecodeComponentDataByName($"FanSwitch-{current}", package));
                    record.FanCurrent =
                        Convert.ToInt32(DecodeComponentDataByName($"CleanerCurrent-{current}", package));
                }
                record.CleanerSwitch = record.CleanerCurrent > 4;
                current++;
                records.Add(record);
            }
            SetStatusCache(package, records[0]);
            ProcessInvoke.Instance<ProtocolPackageProcess>().AddOrUpdateLampblackRecord(records);
        }

        //设置缓存数据，主页上取数据是从缓存里取的
        private static void SetStatusCache(IProtocolPackage<byte[]> package, LampblackRecord record)
        {
            try
            {
                var fanRunTimeRedisKey = RedisService.MakeSureStringGet($"Device:FanRunTime:{DateTime.Now:yyyy-MM-dd}:{package.Device.Id}");
                var fanRunTime = fanRunTimeRedisKey.HasValue ? long.Parse(fanRunTimeRedisKey.ToString()) : 0;
                fanRunTime += 600000000;
                RedisService.StringSetInQueue($"Device:FanRunTime:{DateTime.Now:yyyy-MM-dd}:{package.Device.Id}", $"{fanRunTime}", TimeSpan.FromDays(1));

                var cleanerRunTimeRedisKey = RedisService.MakeSureStringGet($"Device:CleanerRunTime:{DateTime.Now:yyyy-MM-dd}:{package.Device.Id}");
                var cleanerRunTime = cleanerRunTimeRedisKey.HasValue ? long.Parse(cleanerRunTimeRedisKey.ToString()) : 0;
                cleanerRunTime += 600000000;
                RedisService.StringSetInQueue($"Device:CleanerRunTime:{DateTime.Now:yyyy-MM-dd}:{package.Device.Id}", $"{cleanerRunTime}", TimeSpan.FromDays(1));

                var deviceCurrentStatus = new DeviceCurrentStatus
                {
                    FanCurrent = record.FanCurrent,
                    CleanerCurrent = record.CleanerCurrent,
                    CleanerSwitch = record.CleanerSwitch,
                    FanSwitch = record.FanSwitch,
                    LampblackIn = record.LampblackIn,
                    LampblackOut = record.LampblackOut,
                    FanRunTimeTicks = fanRunTime,
                    CleanerRunTimeTicks = cleanerRunTime,
                    UpdateTime = record.RecordDateTime.Ticks
                };
                RedisService.StringSetInQueue($"Device:DeviceCurrentStatus:{package.Device.Id}",
                    JsonConvert.SerializeObject(deviceCurrentStatus), TimeSpan.FromMinutes(4));
                RedisService.StringSetInQueue($"Hotel:CleanerCurrent:{package.Device.ProjectId}",
                    $"{record.CleanerCurrent}", TimeSpan.FromMinutes(4));
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("更新缓存失败！", ex);
            }
        }

        private object DecodeComponentDataByName(string name, IProtocolPackage<byte[]> package) 
            => package.DataComponents.ContainsKey(name) ? _dataConverter.DecodeComponentData(package.DataComponents[name]) : 0;

        /// <summary>
        /// 油烟系统报警信息处理
        /// </summary>
        /// <param name="package"></param>
        /// <param name="source">协议包来源</param>
        public static void LampblackAlarm(IProtocolPackage<byte[]> package, IPackageSource source)
        {
            var exception = package[ProtocolDataName.LampblackException];

            if (exception == null) return;

            var alarmList = new List<Alarm>();

            var flag = Globals.BytesToUint16(exception.ComponentContent, 0, false);
            for (var i = 0; i < 8; i++)
            {
                var error = (1 << i);

                if ((flag & error) == 0) continue;
                using (var alarmRepository = new AlarmRepository())
                {
                    var record = alarmRepository.CreateDefaultModel();
                    record.AlarmType = AlarmType.LampblackCleaner;
                    record.AlarmCode = error;
                    record.AlarmDeviceId = package.Device.Id;
                    record.UpdateTime = package.ReceiveDateTime;
                    record.DomainId = package.Device.DomainId;
                    alarmList.Add(record);
                }
            }

            if (alarmList.Count > 0)
            {
                ProcessInvoke.Instance<ProtocolPackageProcess>().AddOrUpdateAlarm(alarmList);
            }
        }

        /// <summary>
        /// 处理协议数据有效性标志位
        /// </summary>
        /// <param name="package">协议数据包</param>
        /// <param name="monitorDatas">监测数据</param>
        public static void ProcessDataValidFlag(IProtocolPackage<byte[]> package, List<MonitorData> monitorDatas)
        {
            var dataValidFlag = BytesDataConverter.GetDataValidFlag(package[ProtocolDataName.DataValidFlag].ComponentContent);

            foreach (var commandData in package.Command.CommandDatas)
            {
                var monitorData = monitorDatas.First(obj => obj.CommandDataId == commandData.Id);
                monitorData.DataIsValid = dataValidFlag[commandData.ValidFlagIndex];
            }
        }
    }
}
