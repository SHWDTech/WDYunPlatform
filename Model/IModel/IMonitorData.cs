using SHWDTech.Platform.Model.Model;
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
        Guid ProtocolDataId { get; set; }

        /// <summary>
        /// 数据所属协议包
        /// </summary>
        ProtocolData ProtocolData { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        int MonitorDataType { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        double MonitorDataValue { get; set; }

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