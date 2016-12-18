
namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
{
    public class AuthResult
    {
        /// <summary>
        /// 客户端认证结果
        /// </summary>
        public AuthenticationStatus ResultType { get; set; } = AuthenticationStatus.AuthFailed;

        /// <summary>
        /// 客户端认证数据源
        /// </summary>
        public IClientSource AuthedClientSource => Package.ClientSource;

        /// <summary>
        /// 认证协议的数据报
        /// </summary>
        public IProtocolPackage Package { get; set; }
    }
}
