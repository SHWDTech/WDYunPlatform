using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 协议指令数据接口
    /// </summary>
    public interface ICommandData : ISysModel
    {
        /// <summary>
        /// 数据索引值
        /// </summary>
        int DataIndex { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        int DataLength { get; set; }

        /// <summary>
        /// 数据名称
        /// </summary>
        string DataName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// 指令ID
        /// </summary>
        Guid CommandId { get; set; }

        /// <summary>
        /// 所属指令
        /// </summary>
        ProtocolCommand Command { get; set; }
    }
}
