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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        [Required]
        [Display(Name = "数据类型ID")]
        public virtual Guid CommandDataId { get; set; }

        [Display(Name = "数据类型")]
        [ForeignKey("CommandDataId")]
        public virtual CommandData CommandData { get; set; }

        [Display(Name = "数据来源通道号")]
        public virtual ushort DataChannel { get; set; } = 0;

        [Display(Name = "来源工地ID")]
        public virtual Guid? ProjectId { get; set; }

        [Display(Name = "来源工地")]
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [NotMapped]
        [Display(Name = "数据名称")]
        public virtual string DataName => CommandData.DataName;

        [NotMapped]
        [Display(Name = "数据来源设备")]
        public virtual Device Device { get; set; }

        [NotMapped]
        [Display(Name = "数据来源设备ID")]
        public virtual Guid DeviceId { get; set; }

        [Display(Name = "浮点数据值")]
        public virtual double? DoubleValue { get; set; }

        [Display(Name = "布尔数据值")]
        public virtual bool? BooleanValue { get; set; }

        [Display(Name = "整型数据值")]
        public virtual int? IntegerValue { get; set; }

        [Required]
        [Display(Name = "数据更新时间")]
        public virtual DateTime UpdateTime { get; set; }

        [Required]
        [Display(Name = "统计类型")]
        public StatisticsType Type { get; set; }
    }
}
