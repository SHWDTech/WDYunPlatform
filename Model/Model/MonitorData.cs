using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 监测数据
    /// </summary>
    [Serializable]
    public class MonitorData : DataModelBase, IMonitorData
    {
        [Required]
        [Display(Name = "数据来源协议包ID")]
        [Index("Ix_Project_Device_ProtocolData", Order = 2)]
        public virtual long ProtocolDataId { get; set; }

        [Required]
        [Index("Ix_Project_Device_ProtocolData", Order = 1)]
        public virtual long DeviceIdentity { get; set; }

        [Required]
        [Display(Name = "数据类型ID")]
        public virtual Guid CommandDataId { get; set; }

        [Display(Name = "数据来源通道号")]
        public virtual short DataChannel { get; set; } = 0;

        [Display(Name = "来源工地ID")]
        [Index("Ix_Project_Device_ProtocolData", Order = 0)]
        public virtual long ProjectIdentity { get; set; }

        [Display(Name = "浮点数据值")]
        public virtual double? DoubleValue { get; set; }

        [Display(Name = "布尔数据值")]
        public virtual bool? BooleanValue { get; set; }

        [Display(Name = "整型数据值")]
        public virtual int? IntegerValue { get; set; }

        [Required]
        [Display(Name = "数据上传时间")]
        public virtual DateTime UpdateTime { get; set; }

        [Display(Name = "是否有效数据")]
        public virtual bool DataIsValid { get; set; }
    }
}