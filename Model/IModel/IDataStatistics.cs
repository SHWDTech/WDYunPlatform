﻿using System;
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
        /// 数据来源通道号
        /// </summary>
        short DataChannel { get; set; }

        /// <summary>
        /// 数据来源工地ID
        /// </summary>
        long ProjectIdentity { get; set; }

        /// <summary>
        /// 来源设备ID
        /// </summary>
        long DeviceIdentity { get; set; }

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
