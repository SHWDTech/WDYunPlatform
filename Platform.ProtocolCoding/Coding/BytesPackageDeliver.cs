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
                }
            }

            lock (TempMonitorDatas)
            {
                TempMonitorDatas.AddRange(monitorDataList);
                LampblackRecord(package);
            }

            OnMonitorDataReceived();
        }

        private void LampblackRecord(IProtocolPackage<byte[]> package)
        {
            var dev = (RestaurantDevice)package.Device;
            var records = new List<LampblackRecord>();
            var current = 0;

            while (current <= dev.ChannelCount - 1)
            {
                var record = new LampblackRecord
                {
                    ProjectIdentity = dev.Project.Identity,
                    DeviceIdentity = dev.Identity,
                    ProtocolId = package.ProtocolData.Id,
                    CleanerCurrent = Convert.ToInt32(DecodeComponentDataByName($"CleanerCurrent-{current}", package)),
                    FanSwitch = Convert.ToBoolean(DecodeComponentDataByName($"FanSwitch-{current}", package)),
                    FanCurrent = Convert.ToInt32(DecodeComponentDataByName($"FanCurrent-{current}", package)),
                    LampblackIn = Convert.ToInt32(DecodeComponentDataByName($"LampblackIn-{current}", package)),
                    LampblackOut = Convert.ToInt32(DecodeComponentDataByName($"LampblackOut-{current}", package)),
                    RecordDateTime = DateTime.Now,
                    DomainId = package.Device.DomainId
                };
                record.CleanerSwitch = record.CleanerCurrent > 4;
                current++;
                records.Add(record);
            }
            SetStatusCache(package, records[0]);
            ProcessInvoke.Instance<ProtocolPackageProcess>().AddOrUpdateLampblackRecord(records);
        }

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
                    JsonConvert.SerializeObject(deviceCurrentStatus), TimeSpan.FromMinutes(2));
                RedisService.StringSetInQueue($"Hotel:CleanerCurrent:{package.Device.ProjectId}",
                    $"{record.CleanerCurrent}", TimeSpan.FromMinutes(2));
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
