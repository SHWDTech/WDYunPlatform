using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Coding;
using SHWDTech.Platform.ProtocolCoding.Enums;

namespace SHWDTech.Platform.ProtocolCoding.Authentication
{
    public class AuthenticationService
    {
        private static Protocol AuthenticationProtocol => ProtocolInfoManager.GetProtocolByName(ProtocolNames.Classic);

        public static AuthResult DeviceAuthcation(byte[] bytes)
        {
            var package = ProtocolEncoding.DecodeProtocol(bytes, AuthenticationProtocol);



            return null;
        }
    }
}
