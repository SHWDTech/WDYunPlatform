using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议结构接口
    /// </summary>
    public interface IProtocolStructure : ISysModel
    {
        /// <summary>
        /// 所属协议ID
        /// </summary>
        Guid ProtocolId { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        Protocol Procotol { get; set; }

        /// <summary>
        /// 所属协议父结构ID
        /// </summary>
        Guid? ParentStructureId { get; set; }

        /// <summary>
        /// 所属协议父结构
        /// </summary>
        ProtocolStructure ParentStructure { get; set; }

        /// <summary>
        /// 包含的协议子结构
        /// </summary>
        List<ProtocolStructure> SubStructures { get; set; }

        /// <summary>
        /// 协议段名称
        /// </summary>
        string ComponentName { get; set; }

        /// <summary>
        /// 协议段索引值
        /// </summary>
        int ComponentIndex { get; set; }

        /// <summary>
        /// 协议段长度
        /// </summary>
        int ComponentLength { get; set; }

        /// <summary>
        /// 协议段数据长度
        /// </summary>
        int ComponentDataLength { get; set; }
    }
}