using System;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface ICommandDefinition
    {
        /// <summary>
        /// 所属协议指令ID
        /// </summary>
        Guid CommandGuid { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        ProtocolCommand Command { get; set; }

        /// <summary>
        /// 对应协议结构名称
        /// </summary>
        string StructureName { get; set; }

        /// <summary>
        /// 默认字节
        /// </summary>
        byte[] ContentBytes { get; set; }
    }
}
