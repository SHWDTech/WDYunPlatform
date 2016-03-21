using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 设备授权接口
    /// </summary>
    public interface IDeviceAuthentication
    {
        /// <summary>
        /// 判断设备授权信息
        /// </summary>
        /// <param name="authBytes">请求授权字节流</param>
        /// <returns>是否授权成功</returns>
        IDevice DeviceAuthentication(byte[] authBytes);

        /// <summary>
        /// 确认设备授权信息
        /// </summary>
        /// <param name="authConfirmBytes"></param>
        /// <returns></returns>
        bool ConfirmDeviceAuthentication(byte[] authConfirmBytes);
    }
}
