using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.Generics;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议包分发工具
    /// </summary>
    public class PackageDeliver<T> : IPackageDeliver<T>
    {
        private Type _deliver;

        /// <summary>
        /// 分发工具
        /// </summary>
        private Type Deliver => _deliver ?? (_deliver = GetType());

        /// <summary>
        /// 检测数据临时集合
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        protected static readonly List<MonitorData> TempMonitorDatas = new List<MonitorData>();

        public void Delive(IProtocolPackage<T> package, IPackageSource source)
        {
            ParseProtocolData(package);
            DoDelive(package, source);
        }

        protected virtual void DoDelive(IProtocolPackage<T> package, IPackageSource source)
        {
            var deliverParams = package.DeliverParams;

            if (deliverParams.Count == 0) return;

            foreach (var deliverMethod in deliverParams.Select(param => Deliver.GetMethod(param)))
            {
                deliverMethod.Invoke(this, new object[] { package, source });
            }
        }

        /// <summary>
        /// 将收到的协议包按原样回发
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public void ReplyOriginal(IProtocolPackage<T> package, IPackageSource source)
        {
            if (source.Type != PackageSourceType.CommunicationServer) return;

            source.Send(package.GetBytes());
        }

        /// <summary>
        /// 检测数据接收事件
        /// </summary>
        protected static void OnMonitorDataReceived()
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
    }
}
