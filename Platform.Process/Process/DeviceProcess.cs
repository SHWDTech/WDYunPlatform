using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;

namespace Platform.Process.Process
{
    /// <summary>
    /// 设备处理程序
    /// </summary>
    public class DeviceProcess : IDeviceProcess
    {
        public IDevice GetDeviceByNodeId(string nodeId)
            => DbRepository.Repo<DeviceRepository>().GetDeviceByNodeId(nodeId).FirstOrDefault();
    }
}
