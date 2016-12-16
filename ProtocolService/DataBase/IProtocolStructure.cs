using System;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface IProtocolStructure
    {
        /// <summary>
        /// 所属协议ID
        /// </summary>
        Guid ProtocolId { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        Protocol Protocol { get; set; }

        /// <summary>
        /// 协议段数据类型
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// 协议段名称
        /// </summary>
        string StructureName { get; set; }

        /// <summary>
        /// 协议段数据长度
        /// </summary>
        int StructureDataLength { get; set; }

        /// <summary>
        /// 协议段默认数据
        /// </summary>
        byte[] DefaultBytes { get; set; }
    }
}
