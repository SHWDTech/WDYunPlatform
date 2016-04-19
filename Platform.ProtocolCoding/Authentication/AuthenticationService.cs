using System.Collections.Generic;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;

namespace SHWDTech.Platform.ProtocolCoding.Authentication
{
    /// <summary>
    /// 认证服务
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// 认证协议
        /// </summary>
        private static List<Protocol> AuthenticationProtocols => ProtocolInfoManager.GerProtocolsByField(ProtocolFieldNames.GeneralFunction);

        /// <summary>
        /// 设备认证
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static AuthResult DeviceAuthcation(byte[] buffer)
        {
            var package = ProtocolEncoding.Decode(buffer, AuthenticationProtocols);

            if (!package.Finalized)
            {
                return new AuthResult(AuthResultType.Faild, package);
            }

            var device = GetAuthedDevice(package);

            return device == null
                ? new AuthResult(AuthResultType.Faild, package)
                : new AuthResult(AuthResultType.Success, package, device);
        }

        /// <summary>
        /// 获取授权设备
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private static IDevice GetAuthedDevice(IProtocolPackage package)
        {
            if (package.Protocol.ProtocolName == ProtocolNames.Classic)
            {
                var nodeId = DataConvert.DecodeComponentData(package[StructureNames.NodeId]).ToString();
                return ProcessInvoke.GetInstance<DeviceProcess>().GetDeviceByNodeId(nodeId);
            }

            return null;
        }
    }
}