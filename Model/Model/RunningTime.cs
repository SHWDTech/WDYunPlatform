using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    public class RunningTime : DataModelBase, IRunningTime
    {
        [Required]
        [Display(Name = "运行时间")]
        public virtual long RunningTimeTicks { get; set; }

        [Display(Name = "运行时间")]
        [NotMapped]
        public virtual TimeSpan RunningTimeSpan
        {
            get
            {
                return new TimeSpan(RunningTimeTicks);
            }
            set
            {
                RunningTimeTicks = value.Ticks;
            }
        }

        [Required]
        [Display(Name = "运行时间类型")]
        public virtual RunningTimeType Type { get; set; }

        [Display(Name = "相关酒店")]
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Required]
        [Display(Name = "相关酒店")]
        public virtual Guid ProjectId { get; set; }

        [Display(Name = "相关设备")]
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }

        [Required]
        [Display(Name = "相关设备")]
        public virtual Guid DeviceId { get; set; }

        [Required]
        [Display(Name = "相关设备")]
        public virtual DateTime UpdateTime { get; set; }
    }
}
