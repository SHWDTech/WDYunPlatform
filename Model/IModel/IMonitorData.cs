using System;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IMonitorData : IDataModel
    {
        /// <summary>
        /// 数据所属协议包
        /// </summary>
        Protocol Protocol { get; set; }

        /// <summary>
        /// 数据所属域
        /// </summary>
        SysDomain MonitorDataDomain { get; set; }

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
