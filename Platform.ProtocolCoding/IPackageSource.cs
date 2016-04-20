using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;

namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 协议包数据源
    /// </summary>
    public interface IPackageSource
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        PackageSourceType Type { get; }

        /// <summary>
        /// 发送指令协议数据
        /// </summary>
        /// <param name="command">需要发送的指令</param>
        void Send(ProtocolCommand command);

        /// <summary>
        /// 发送字节流
        /// </summary>
        /// <param name="sendBytes"></param>
        void Send(byte[] sendBytes);
    }
}
