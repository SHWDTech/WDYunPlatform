using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
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
            Deliver = typeof (PackageDeliver);
        }

        /// <summary>
        /// 协议包处理程序
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public static void Delive(IProtocolPackage package, IPackageSource source)
        {
            var deliverParams = package.DeliverParams.Split(';');

            foreach (var deliverMethod in deliverParams.Select(param => Deliver.GetMethod(param)))
            {
                deliverMethod.Invoke(deliverMethod, new object[] {package, source });
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
            var process = ProcessInvoke.GetInstance<ProtocolDataProcess>();

            var protocolData = process.GetNewProtocolData();

            protocolData.Device = package.Device;
            protocolData.Protocol = package.Protocol;
            protocolData.ProtocolTime = package.ReceiveDateTime;
            protocolData.UpdateTime = DateTime.Now;
            protocolData.ProtocolContent = package.GetBytes();

            process.AddProtocolData(protocolData);


            var dataProcess = ProcessInvoke.GetInstance<MonitorDataProcess>();

            var dataValidFlag = (DataValidFlag)DataConvert.DecodeComponentData(package[ProtocolDataName.DataValidFlag]);

            var monitorDataList = new List<MonitorData>();

            for (var i = 0; i < package.Command.CommandDatas.Count; i++)
            {
                var monitorData = dataProcess.GetNewMonitorData();

                monitorData.DataIsValid = dataValidFlag[i];
                monitorData.MonitorDataValue =
                    (double)
                        DataConvert.DecodeComponentData(
                            package[package.Command.CommandDatas.First(obj => obj.DataIndex == i).DataName]);
                monitorData.ProtocolData = protocolData;
                monitorData.UpdateTime = DateTime.Now;

                monitorDataList.Add(monitorData);
            }

            dataProcess.AddOrUpdateMonitorData(monitorDataList);
        }
    }
}
