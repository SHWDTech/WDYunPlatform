using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.Utility;

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议包分发工具
    /// </summary>
    public static class PackageDeliver
    {
        /// <summary>
        /// 分发工具
        /// </summary>
        private static readonly Type Deliver;

        static PackageDeliver()
        {
            Deliver = typeof(PackageDeliver);
        }

        /// <summary>
        /// 检测数据临时集合
        /// </summary>
        private static readonly List<MonitorData> TempMonitorDatas = new List<MonitorData>();

        /// <summary>
        /// 协议包处理程序
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public static void Delive(IProtocolPackage package, IPackageSource source)
        {
            ParseProtocolData(package);
            DoDelive(package, source);
        }

        private static void DoDelive(IProtocolPackage package, IPackageSource source)
        {
            var deliverParams = package.DeliverParams;

            if (deliverParams.Count == 0) return;


            foreach (var deliverMethod in deliverParams.Select(param => Deliver.GetMethod(param)))
            {
                deliverMethod.Invoke(deliverMethod, new object[] { package, source });
            }
        }

        /// <summary>
        /// 将收到的协议包按原样回发
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public static void ReplyOriginal(IProtocolPackage package, IPackageSource source)
        {
            if (source.Type != PackageSourceType.CommunicationServer) return;

            source.Send(package.GetBytes());
        }

        /// <summary>
        /// 存储协议包携带的数据
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public static void StoreData(IProtocolPackage package, IPackageSource source)
        {
            var monitorDataList = new List<MonitorData>();

            for (var i = 0; i < package.Command.CommandDatas.Count; i++)
            {
                var monitorData = new MonitorDataRepository().CreateDefaultModel();
                monitorData.DomainId = package.Device.DomainId;

                var commandData = package.Command.CommandDatas.First(obj => obj.DataIndex == i);

                var temp = DataConvert.DecodeComponentData(package[commandData.DataName]);

                monitorData.DoubleValue = Convert.ToDouble(temp);
                monitorData.ProtocolDataId = package.ProtocolData.Id;
                monitorData.UpdateTime = DateTime.Now;
                monitorData.CommandDataId = commandData.Id;
                monitorData.ProjectId = package.Device.ProjectId;

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

        public static void Lampblack(IProtocolPackage package, IPackageSource source)
        {
            var monitorDataList = new List<MonitorData>();

            foreach (var dataComponent in package.DataComponents)
            {
                if (dataComponent.Value.CommandData.DataName == null ||
                    dataComponent.Value.CommandData.DataConvertType == ProtocolDataType.None) continue;
                var data = DataConvert.DecodeComponentData(dataComponent.Value);

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
                monitorData.ProjectId = package.Device.ProjectId;
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
                    ProjectId = package.Device.ProjectId,
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
        public static void LampblackAlarm(IProtocolPackage package, IPackageSource source)
        {
            var exception = package[ProtocolDataName.LampblackException];

            if (exception == null) return;

            var alarmList = new List<Alarm>();

            var flag = Globals.BytesToUint16(exception.ComponentBytes, 0, false);
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
        /// 检测数据接收事件
        /// </summary>
        private static void OnMonitorDataReceived()
        {
            lock (TempMonitorDatas)
            {
                while (TempMonitorDatas.Count > 0)
                {
                    var executeDatas = TempMonitorDatas.ToArray();
                    ProcessInvoke.Instance<ProtocolPackageProcess>().AddOrUpdateMonitorData(executeDatas);
                    TempMonitorDatas.Clear();
                }
            }
        }

        /// <summary>
        /// 存储协议包数据
        /// </summary>
        /// <param name="package">协议数据包</param>
        /// <returns>保存数据包相关信息</returns>
        public static void ParseProtocolData(IProtocolPackage package)
        {
            var protocolData = new ProtocolDataRepository().CreateDefaultModel();

            protocolData.DeviceId = package.Device.Id;
            protocolData.ProtocolId = package.Protocol.Id;
            protocolData.ProtocolTime = package.ReceiveDateTime;
            protocolData.UpdateTime = DateTime.Now;
            protocolData.DomainId = package.Device.DomainId;
            protocolData.ProtocolContent = package.GetBytes();
            protocolData.Length = protocolData.ProtocolContent.Length;

            package.ProtocolData = protocolData;
            ProcessInvoke.Instance<ProtocolPackageProcess>().AddOrUpdateProtocolData(protocolData);
        }

        /// <summary>
        /// 处理协议数据有效性标志位
        /// </summary>
        /// <param name="package">协议数据包</param>
        /// <param name="monitorDatas">监测数据</param>
        public static void ProcessDataValidFlag(IProtocolPackage package, List<MonitorData> monitorDatas)
        {
            var dataValidFlag = DataConvert.GetDataValidFlag(package[ProtocolDataName.DataValidFlag].ComponentBytes);

            foreach (var commandData in package.Command.CommandDatas)
            {
                var monitorData = monitorDatas.First(obj => obj.CommandDataId == commandData.Id);
                monitorData.DataIsValid = dataValidFlag[commandData.ValidFlagIndex];
            }
        }
    }
}
