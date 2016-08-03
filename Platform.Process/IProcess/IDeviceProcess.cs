using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 设备处理接口
    /// </summary>
    public interface IDeviceProcess : IProcessBase
    {
        /// <summary>
        /// 通过NodeId获取设备
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        IDevice GetDeviceByNodeId(string nodeId, bool isEnabled);

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        IList<Device> GetAllDevices();
    }
}
