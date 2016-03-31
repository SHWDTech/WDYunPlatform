using System.Collections.Generic;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
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
        private static Protocol AuthenticationProtocol => ProtocolInfoManager.GetProtocolByName(ProtocolNames.Classic);

        public static AuthResult DeviceAuthcation(IList<byte> buffer)
        {
            var package = ProtocolEncoding.DecodeProtocol(buffer, AuthenticationProtocol);

            if (!package.Finalized || package.Command.CommandCategory != CommandCategory.HeartBeat)
            {
                return new AuthResult(AuthResultType.Faild, package);
            }

            var nodeId = DataConvert.DecodeComponentData(package[StructureNames.NodeId]).ToString();

            var device = ProcessInvoke.GetInstance<DeviceProcess>() .GetDeviceByNodeId(nodeId);

            if(device == null) return new AuthResult(AuthResultType.Faild, package);

            //var replyBytes = package.GetBytes();
        }
    }
}