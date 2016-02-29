using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class MonitorData :DataModelBase, IMonitorData
    {
        [Key]
        public long MonitorDataId { get; set; }

        [Display(Name = "数据来源协议包")]
        public Protocol Protocol { get; set; }

        [Display(Name = "数据所属域")]
        public SysDomain MonitorDataDomain { get; set; }

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
