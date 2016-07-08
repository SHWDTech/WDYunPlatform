using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议指令模型接口
    /// </summary>
    public interface IProtocolCommand : ISysModel
    {
        /// <summary>
        /// 指令类型
        /// </summary>
        byte[] CommandTypeCode { get; set; }

        /// <summary>
        /// 指令编码
        /// </summary>
        byte[] CommandCode { get; set; }

        /// <summary>
        /// 指令数据发送长度
        /// </summary>
        int SendBytesLength { get; set; }

        /// <summary>
        /// 指令数据接收长度
        /// </summary>
        int ReceiveBytesLength { get; set; }

        /// <summary>
        /// 指令数据接收最大长度
        /// </summary>
        int ReceiceMaxBytesLength { get; set; }

        /// <summary>
        /// 指令分类
        /// </summary>
        string CommandCategory { get; set; }

        /// <summary>
        /// 所属协议ID
        /// </summary>
        Guid ProtocolId { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        Protocol Protocol { get; set; }

        /// <summary>
        /// 数据段组合方式
        /// </summary>
        DataOrderType DataOrderType { get; set; }

        /// <summary>
        /// 指令数据
        /// </summary>
        ICollection<CommandData> CommandDatas { get; set; }

        /// <summary>
        /// 指令处理参数相关配置
        /// </summary>
        ICollection<SysConfig> CommandDeliverParamConfigs { get; set; }

        /// <summary>
        /// 指令相关定义
        /// </summary>
        ICollection<CommandDefinition> CommandDefinitions { get; set; }

        /// <summary>
        /// 指令处理参数列表
        /// </summary>
        List<string> CommandDeliverParams { get; }
    }
}
