using SHWDTech.Platform.Model.Model;
using System;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议包模型接口
    /// </summary>
    public interface IProtocolData : IDataModel
    {
        /// <summary>
        /// 协议所属设备ID
        /// </summary>
        long DeviceIdentity { get; set; }

        /// <summary>
        /// 协议内容
        /// </summary>
        byte[] ProtocolContent { get; set; }

        /// <summary>
        /// 协议内容长度
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// 协议类型ID
        /// </summary>
        Guid ProtocolId { get; set; }

        /// <summary>
        /// 协议包组包完成时间
        /// </summary>
        DateTime ProtocolTime { get; set; }

        /// <summary>
        /// 协议更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}