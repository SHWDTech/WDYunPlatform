using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 监测数据小时统计值模型
    /// </summary>
    public class DataStatistics : DataModelBase, IDataStatistics
    {
        [Required]
        [Display(Name = "数据类型ID")]
        public virtual Guid CommandDataId { get; set; }

        [Display(Name = "数据来源通道号")]
        public virtual short DataChannel { get; set; } = 0;

        [Display(Name = "来源工地ID")]
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 1)]
        public virtual long ProjectIdentity { get; set; }

        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 2)]
        [Display(Name = "数据来源设备ID")]
        public virtual long DeviceIdentity { get; set; }

        [Display(Name = "浮点数据值")]
        public virtual double? DoubleValue { get; set; }

        [Display(Name = "布尔数据值")]
        public virtual bool? BooleanValue { get; set; }

        [Display(Name = "整型数据值")]
        public virtual int? IntegerValue { get; set; }

        [Required]
        [Display(Name = "数据更新时间")]
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 3)]
        public virtual DateTime UpdateTime { get; set; }

        [Required]
        [Display(Name = "统计类型")]
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 0)]
        public StatisticsType Type { get; set; }
    }
}
