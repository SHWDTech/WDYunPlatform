using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;
using SHWDTech.Platform.ProtocolCoding.MessageQueueModel;

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
        /// <param name="paramBytes">指令相关参数</param>
        void Send(ProtocolCommand command, List<CommandParam> paramBytes = null);

        /// <summary>
        /// 发送字节流
        /// </summary>
        /// <param name="sendBytes"></param>
        void Send(byte[] sendBytes);
    }
}
