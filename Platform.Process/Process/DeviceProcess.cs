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
        /// <summary>
        /// 设备数据仓库
        /// </summary>
        private readonly DeviceRepository _repository = DbRepository.Repo<DeviceRepository>();

        public IDevice GetDeviceByNodeId(string nodeId)
            => _repository.GetDeviceByNodeId(nodeId).FirstOrDefault();
    }
}
