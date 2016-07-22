using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;

namespace Platform.Process.Process
{
    /// <summary>
    /// 设备处理程序
    /// </summary>
    public class DeviceProcess : ProcessBase, IDeviceProcess
    {
        public IDevice GetDeviceByNodeId(string nodeId, bool isEnabled) 
            => Repo<DeviceRepository>().GetDeviceByNodeId(nodeId, isEnabled).FirstOrDefault();
    }
}
