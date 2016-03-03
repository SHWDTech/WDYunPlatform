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
        /// 协议所属设备
        /// </summary>
        Device Device { get; set; }

        /// <summary>
        /// 协议内容
        /// </summary>
        byte[] ProtocolContent { get; set; }

        /// <summary>
        /// 协议内容长度
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        Protocol Protocol { get; set; }

        /// <summary>
        /// 协议更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}