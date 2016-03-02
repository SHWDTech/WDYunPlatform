using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 监测数据
    /// </summary>
    [Serializable]
    public class MonitorData :DataModelBase, IMonitorData
    {
        [Display(Name = "数据来源协议包")]
        public ProtocolData ProtocolData { get; set; }

        [Display(Name = "数据类型")]
        public int MonitorDataType { get; set; }

        [Display(Name = "数据值")]
        public double MonitorDataValue { get; set; }

        [Display(Name = "数据上传时间")]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "是否有效数据")]
        public bool DataIsValid { get; set; }
    }
}
