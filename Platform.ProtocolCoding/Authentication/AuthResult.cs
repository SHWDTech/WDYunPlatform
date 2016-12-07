using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.ProtocolCoding.Generics;

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
        /// 认证协议的协议包
        /// </summary>
        public IProtocolPackage Package { get; }

        /// <summary>
        /// 是否需要回复
        /// </summary>
        public bool NeedReply { get; }

        /// <summary>
        /// 设备回复协议字节流
        /// </summary>
        public byte[] ReplyBytes => Package.Finalized ? Package.GetBytes() : new byte[0];

        public AuthResult(AuthResultType type, IProtocolPackage package, IDevice device = null, bool needReply = false)
        {
            ResultType = type;
            AuthDevice = device;
            Package = package;
            NeedReply = needReply;
        }
    }
}
