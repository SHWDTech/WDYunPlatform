using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;

namespace SHWDTech.Platform.ProtocolCoding
{
    public class StringPackageDeliver : PackageDeliver<string>
    {
        private readonly StringDataConverter _dataConverter = new StringDataConverter();

        /// <summary>
        /// 存储协议包携带的数据
        /// </summary>
        /// <param name="package">接收到的协议包</param>
        /// <param name="source">接收数据源</param>
        public void StoreData(IProtocolPackage<string> package, IPackageSource source)
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
                monitorData.ProjectId = package.Device.ProjectId;

                monitorDataList.Add(monitorData);
            }
            lock (TempMonitorDatas)
            {
                TempMonitorDatas.AddRange(monitorDataList);
            }

            OnMonitorDataReceived();
        }
    }
}
