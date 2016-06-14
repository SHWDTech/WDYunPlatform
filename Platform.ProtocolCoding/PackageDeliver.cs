using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository;
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
        /// 协议包处理程序
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public static void Delive(IProtocolPackage package, IPackageSource source)
        {
            ParseProtocolData(package);
            try
            {
                DoDelive(package, source);
            }
            catch (NullReferenceException ex)
            {
                LogService.Instance.Debug("", ex);
            }
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

                monitorData.MonitorDataValue = Convert.ToDouble(temp);
                monitorData.ProtocolDataId = package.ProtocolData.Id;
                monitorData.UpdateTime = DateTime.Now;
                monitorData.CommandDataId = commandData.Id;
                monitorData.DataName = commandData.DataName;
                monitorData.ProjectId = package.Device.ProjectId;

                monitorDataList.Add(monitorData);
            }

            if (package[ProtocolDataName.DataValidFlag] != null)
            {
                ProcessDataValidFlag(package, monitorDataList);
            }

            var packageProcess = ProcessInvoke.GetInstance<ProtocolPackageProcess>();

            packageProcess.AddOrUpdateMonitorData(monitorDataList, package.ProtocolData);
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

            DbRepository.Repo<ProtocolDataRepository>().AddOrUpdate(protocolData);
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
