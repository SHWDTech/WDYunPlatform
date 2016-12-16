using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface ICommandData
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
        /// 数据显示名称
        /// </summary>
        string DataDisplayName { get; set; }

        /// <summary>
        /// 数据转换类型
        /// </summary>
        string DataConvertType { get; set; }

        /// <summary>
        /// 数据值类型
        /// </summary>
        DataValueType DataValueType { get; set; }

        /// <summary>
        /// 数据标识
        /// </summary>
        byte DataFlag { get; set; }

        /// <summary>
        /// 所属指令
        /// </summary>
        ICollection<ProtocolCommand> Commands { get; set; }

        /// <summary>
        /// 数据有效性标志位索引值
        /// </summary>
        int ValidFlagIndex { get; set; }
    }
}
