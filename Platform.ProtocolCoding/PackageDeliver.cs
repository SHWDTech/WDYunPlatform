﻿using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;

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
                var monitorData = MonitorDataRepository.CreateDefaultModel();

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

            foreach (var commandData in package.Command.CommandDatas)
            {
                if (package[commandData.DataName] == null || package[commandData.DataName].DataType == ProtocolDataType.None) continue;

                var data = DataConvert.DecodeComponentData(package[commandData.DataName]);

                var monitorData = MonitorDataRepository.CreateDefaultModel();

                switch (commandData.DataValueType)
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
                monitorData.CommandDataId = commandData.Id;
                monitorData.ProjectId = package.Device.ProjectId;

                monitorDataList.Add(monitorData);
            }

            lock (TempMonitorDatas)
            {
                TempMonitorDatas.AddRange(monitorDataList);
            }

            OnMonitorDataReceived();
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
                    ProcessInvoke.GetInstance<ProtocolPackageProcess>().AddOrUpdateMonitorData(executeDatas);
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
            var protocolData = ProtocolDataRepository.CreateDefaultModel();

            protocolData.DeviceId = package.Device.Id;
            protocolData.ProtocolId = package.Protocol.Id;
            protocolData.ProtocolTime = package.ReceiveDateTime;
            protocolData.UpdateTime = DateTime.Now;
            protocolData.DomainId = RepositoryBase.CurrentDomain.Id;
            protocolData.ProtocolContent = package.GetBytes();
            protocolData.Length = protocolData.ProtocolContent.Length;

            package.ProtocolData = protocolData;
            ProcessInvoke.GetInstance<ProtocolPackageProcess>().AddOrUpdateProtocolData(protocolData);
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
