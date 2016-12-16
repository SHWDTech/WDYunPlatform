using System;
using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface IProtocolCommand
    {
        /// <summary>
        /// 指令类型编码
        /// </summary>
        byte[] CommandTypeCode { get; set; }

        /// <summary>
        /// 指令编码
        /// </summary>
        byte[] CommandCode { get; set; }

        /// <summary>
        /// 指令发送数据长度
        /// </summary>
        int SendBytesLength { get; set; }

        /// <summary>
        /// 指令接收数据长度
        /// </summary>
        int ReceiveBytesLength { get; set; }

        /// <summary>
        /// 指令接收数据最大长度
        /// </summary>
        int ReceiveMaxBytesLength { get; set; }

        /// <summary>
        /// 指令类型
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
        /// 指令包含的数据
        /// </summary>
        ICollection<CommandData> CommandDatas { get; set; }

        /// <summary>
        /// 指令处理参数字符串
        /// </summary>
        string DeliverParams { get; set; }

        /// <summary>
        /// 指令定义信息
        /// </summary>
        ICollection<CommandDefinition> CommandDefinitions { get; set; }

        /// <summary>
        /// 指令处理参数
        /// </summary>
        List<string> CommandDeliverParams { get; }
    }
}
