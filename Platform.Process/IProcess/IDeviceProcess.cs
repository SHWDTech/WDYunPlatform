using SHWDTech.Platform.Model.IModel;

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
    }
}
