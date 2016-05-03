using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 任务
    /// </summary>
    [Serializable]
    public class CommandTask : SysDomainModelBase, ICommandTask
    {
        [Required]
        [Display(Name = "任务所属设备ID")]
        public virtual Guid TaskDeviceId { get; set; }

        [Display(Name = "任务所属设备")]
        [ForeignKey("TaskDeviceId")]
        public virtual Device TaskDevice { get; set; }

        [MaxLength(25)]
        [Display(Name = "任务编码")]
        public virtual string TaskCode { get; set; }

        [Required]
        [Display(Name = "任务类型")]
        public virtual int TaskType { get; set; }

        [Required]
        [Display(Name = "任务状态")]
        public virtual TaskStatus TaskStatus { get; set; }

        [Required]
        [Display(Name = "任务执行状态")]
        public virtual TaskExceteStatus ExecuteStatus { get; set; }

        [Required]
        [Display(Name = "任务包含协议")]
        public virtual ICollection<ProtocolData> TaskProtocols { get; set; } = new List<ProtocolData>();

        [Required]
        [Display(Name = "任务生成时间")]
        public virtual DateTime SetUpDateTime { get; set; }
    }
}