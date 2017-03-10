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
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 0)]
        public virtual RunningTimeType Type { get; set; }

        [Required]
        [Display(Name = "相关酒店")]
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 1)]
        public virtual long ProjectIdentity { get; set; }

        [Required]
        [Display(Name = "相关设备")]
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 2)]
        public virtual long DeviceIdentity { get; set; }

        [Required]
        [Display(Name = "更新时间")]
        [Index("IX_Type_Project_Device_UpdateTime", IsClustered = true, Order = 3)]
        public virtual DateTime UpdateTime { get; set; }
    }
}
