using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.ProtocolCoding.Authentication
{
    /// <summary>
    /// 认证结果
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// 结果类型
        /// </summary>
        public AuthResultType ResultType { get; }

        /// <summary>
        /// 授权设备
        /// </summary>
        public IDevice AuthDevice { get; }

        /// <summary>
        /// 设备回复协议字节流
        /// </summary>
        public byte[] ReplyBytes { get; set; }

        public AuthResult(AuthResultType type, IDevice device = null)
        {
            ResultType = type;
            AuthDevice = device;
        }
    }
}
