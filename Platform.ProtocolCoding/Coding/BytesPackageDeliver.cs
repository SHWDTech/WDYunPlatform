﻿using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    public class BytesPackageDeliver : PackageDeliver<byte[]>
    {
        private readonly BytesDataConverter _dataConverter = new BytesDataConverter();

        /// <summary>
        /// 存储协议包携带的数据
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public void StoreData(IProtocolPackage<byte[]> package, IPackageSource source)
        {
            var monitorDataList = new List<MonitorData>();

            for (var i = 0; i < package.Command.CommandDatas.Count; i++)
            {
                var monitorData = new MonitorDataRepository().CreateDefaultModel();
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

                var monitorData = new MonitorDataRepository().CreateDefaultModel();
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
                    CommandDataId = new Guid("15802959-D25B-42AD-BE50-5B48DCE4039A"),
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

            lock (TempMonitorDatas)
            {
                TempMonitorDatas.AddRange(monitorDataList);
            }

            OnMonitorDataReceived();
        }

        /// <summary>
        /// 油烟系统报警信息处理
        /// </summary>
        /// <param name="package"></param>
        /// <param name="source"></param>
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
                var record = new AlarmRepository().CreateDefaultModel();
                record.AlarmType = AlarmType.Lampblack;
                record.AlarmCode = error;
                record.AlarmDeviceId = package.Device.Id;
                record.UpdateTime = package.ReceiveDateTime;
                record.DomainId = package.Device.DomainId;
                alarmList.Add(record);
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
        public void ProcessDataValidFlag(IProtocolPackage<byte[]> package, List<MonitorData> monitorDatas)
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
