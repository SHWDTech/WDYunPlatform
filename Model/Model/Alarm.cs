using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 报警信息
    /// </summary>
    [Serializable]
    public class Alarm : DataModelBase, IAlarm
    {
        [Required]
        [Display(Name = "报警信息来源设备ID")]
        public virtual Guid AlarmDeviceId { get; set; }

        [ForeignKey("AlarmDeviceId")]
        [Display(Name = "报警信息来源设备")]
        public virtual Device AlarmDevice { get; set; }

        [Required]
        [Display(Name = "报警信息值")]
        public virtual double AlarmValue { get; set; }

        [Required]
        [Display(Name = "报警信息类别ID")]
        public virtual Guid AlarmTypeId { get; set; }

        [Display(Name = "报警信息类别")]
        [ForeignKey("AlarmTypeId")]
        public virtual SysDictionary AlarmType { get; set; }

        [Required]
        [Display(Name = "报警信息更新时间")]
        public virtual DateTime UpdateTime { get; set; }
    }
}