using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 数据统计模型接口
    /// </summary>
    public interface IDataStatistics : IDataModel
    {
        /// <summary>
        /// 数据类型ID
        /// </summary>
        Guid CommandDataId { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        CommandData CommandData { get; set; }

        /// <summary>
        /// 数据来源通道号
        /// </summary>
        ushort DataChannel { get; set; }

        /// <summary>
        /// 数据来源工地ID
        /// </summary>
        Guid? ProjectId { get; set; }

        /// <summary>
        /// 数据来源工地
        /// </summary>
        Project Project { get; set; }

        /// <summary>
        /// 数据名称
        /// </summary>
        string DataName { get; }

        /// <summary>
        /// 数据来源设备
        /// </summary>
        Device Device { get; set; }

        /// <summary>
        /// 浮点数据值
        /// </summary>
        double? DoubleValue { get; set; }

        /// <summary>
        /// 布尔数据值
        /// </summary>
        bool? BooleanValue { get; set; }

        /// <summary>
        /// 整型数据值
        /// </summary>
        int? IntegerValue { get; set; }

        /// <summary>
        /// 数据更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }

        /// <summary>
        /// 统计数据类型
        /// </summary>
        StatisticsType Type { get; set; }
    }
}
