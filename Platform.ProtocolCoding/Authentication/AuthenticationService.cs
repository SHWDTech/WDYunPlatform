using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Generics;

namespace SHWDTech.Platform.ProtocolCoding.Authentication
{
    /// <summary>
    /// 认证服务
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// 设备认证
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static AuthResult DeviceAuthcation(byte[] buffer)
        {
            var package = ProtocolEncoder.Decode(buffer, ProtocolInfoManager.AllProtocols);

            if (!package.Finalized)
            {
                return new AuthResult(AuthResultType.DecodedFailed, package);
            }

            var device = GetAuthedDevice(package);

            return device == null
                ? new AuthResult(AuthResultType.DeviceNotRegisted, package)
                : new AuthResult(AuthResultType.Success, package, device, package.NeedReply);
        }

        /// <summary>
        /// 获取授权设备
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private static IDevice GetAuthedDevice(IProtocolPackage package)
            => ProcessInvoke.Instance<DeviceProcess>().GetDeviceByNodeId(package.DeviceNodeId, true);
    }
}