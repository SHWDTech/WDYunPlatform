using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 报警信息
    /// </summary>
    [Serializable]
    public class Alarm: DataModelBase, IAlarm
    {
        [Required]
        [Display(Name = "报警信息来源设备")]
        public Device Device { get; set; }

        [Required]
        [Display(Name = "报警信息值")]
        public double AlarmValue { get; set; }

        [Required]
        [Display(Name = "报警信息类别")]
        public int AlarmType { get; set; }

        [Required]
        [Display(Name = "报警信息更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}
