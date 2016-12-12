using System.Dynamic;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public class AuthResult
    {
        /// <summary>
        /// 客户端认证结果
        /// </summary>
        public AuthenticationStatus ResultType { get; }

        /// <summary>
        /// 客户端认证数据源
        /// </summary>
        public IClientSource AuthedClientSource { get; }

        /// <summary>
        /// 认证协议的数据报
        /// </summary>
        public IProtocolPackage Package { get; }

        public AuthResult(AuthenticationStatus type, IProtocolPackage package, IClientSource clientSource)
        {
            ResultType = type;
            Package = package;
            AuthedClientSource = clientSource;
        }
    }
}
