using System.Collections.Generic;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 设备处理程序
    /// </summary>
    public class DeviceProcess : ProcessBase, IDeviceProcess
    {
        public IDevice GetDeviceByNodeId(string nodeId, bool isEnabled)
            => Repo<RestaurantDeviceRepository>().GetDeviceByNodeId(nodeId, isEnabled).FirstOrDefault();

        public IDevice GetDeviceByNodeId(string nodeId)
            => Repo<RestaurantDeviceRepository>().GetDeviceByNodeId(nodeId).FirstOrDefault();

        public List<Device> GetProjectDevices(long projectIdentity) => Repo<DeviceRepository>()
            .GetModels(d => d.Project.Identity == projectIdentity)
            .ToList();
    }
}
