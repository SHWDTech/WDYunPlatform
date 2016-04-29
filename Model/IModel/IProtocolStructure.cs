using System;
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
        /// 协议段数据类型
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// 协议段名称
        /// </summary>
        string StructureName { get; set; }

        /// <summary>
        /// 协议段索引值
        /// </summary>
        int StructureIndex { get; set; }

        /// <summary>
        /// 协议段数据长度
        /// </summary>
        int StructureDataLength { get; set; }

        /// <summary>
        /// 协议结构默认值
        /// </summary>
        byte[] DefaultBytes { get; set; }
    }
}