using System;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 监测数据模型接口
    /// </summary>
    public interface IMonitorData : IDataModel
    {
        /// <summary>
        /// 数据所属协议包ID
        /// </summary>
        long ProtocolDataId { get; set; }

        /// <summary>
        /// 数据类型ID
        /// </summary>
        Guid CommandDataId { get; set; }

        /// <summary>
        /// 数据来源通道号
        /// </summary>
        short DataChannel { get; set; }

        /// <summary>
        /// 来源工地ID
        /// </summary>
        long ProjectIdentity { get; set; }

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
        /// 数据上传时间
        /// </summary>
        DateTime UpdateTime { get; set; }

        /// <summary>
        /// 数据是否有效值
        /// </summary>
        bool DataIsValid { get; set; }
    }
}