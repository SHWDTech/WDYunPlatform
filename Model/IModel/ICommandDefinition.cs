using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议指令定义接口
    /// </summary>
    public interface ICommandDefinition : ISysModel
    {
        /// <summary>
        /// 所属协议指令ID
        /// </summary>
        Guid CommandGuid { get; set; }

        /// <summary>
        /// 所属协议指令
        /// </summary>
        ProtocolCommand Command { get; set; }

        /// <summary>
        /// 对应结构名称
        /// </summary>
        string StructureName { get; set; }

        /// <summary>
        /// 定义内容
        /// </summary>
        byte[] ContentBytes { get; set; }
    }
}
