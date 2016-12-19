using SHWDTech.Platform.ProtocolService.ProtocolEncoding;

namespace SHWDTech.Platform.ProtocolService
{
    public interface IClientSource
    {
        /// <summary>
        /// 数据源身份码
        /// </summary>
        string ClientIdentity { get; }

        /// <summary>
        /// 客户端NODEID
        /// </summary>
        string ClientNodeId { get; }

        /// <summary>
        /// 数据源所属业务
        /// </summary>
        string BusinessName { get; }

        /// <summary>
        /// 数据源对应解码器
        /// </summary>
        IProtocolEncoder ProtocolEncoder { get; set; }
    }
}
