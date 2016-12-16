using System;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface IProtocolData
    {
        /// <summary>
        /// 所属业务
        /// </summary>
        string Business { get; set; }

        /// <summary>
        /// 设备NODEID
        /// </summary>
        string DeviceNodeId { get; set; }

        /// <summary>
        /// 协议内容
        /// </summary>
        byte[] ProtocolContent { get; set; }

        /// <summary>
        /// 协议内容长度
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// 所属协议ID
        /// </summary>
        Guid ProtocolId { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        Protocol Protocol { get; set; }

        /// <summary>
        /// 协议包拆包完成时间
        /// </summary>
        DateTime PackageDateTime { get; set; }

        /// <summary>
        /// 协议包数据更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}
