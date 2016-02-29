using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IProtocol : IDataModel
    {
        /// <summary>
        /// 协议所属域
        /// </summary>
        SysDomain ProtocolDomain { get; set; }

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
        int ProtocolType { get; set; }

        /// <summary>
        /// 协议版本
        /// </summary>
        int ProtocolVersion { get; set; }

        /// <summary>
        /// 协议更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}
