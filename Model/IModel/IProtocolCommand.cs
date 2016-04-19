using System;
using System.Collections.Generic;
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
        /// 指令数据长度
        /// </summary>
        int CommandBytesLength { get; set; }

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
        /// 指令数据
        /// </summary>
        ICollection<CommandData> CommandDatas { get; set; }

        /// <summary>
        /// 指令处理参数相关配置
        /// </summary>
        ICollection<SysConfig> CommandDeliverParamConfigs { get; set; }

        /// <summary>
        /// 指令处理参数列表
        /// </summary>
        List<string> CommandDeliverParams { get; }
    }
}
