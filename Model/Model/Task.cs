using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 任务
    /// </summary>
    [Serializable]
    public class Task : SysDomainModelBase, ITask
    {
        [Required]
        [Display(Name = "设备所属设备")]
        public Device TaskDevice { get; set; }

        [MaxLength(25)]
        [Display(Name = "任务编码")]
        public string TaskCode { get; set; }

        [Required]
        [Display(Name = "任务类型")]
        public int TaskType { get; set; }

        [Required]
        [Display(Name = "任务状态")]
        public int TaskStatus { get; set; }

        [Required]
        [Display(Name = "任务执行状态")]
        public int ExecuteStatus { get; set; }

        [Required]
        [Display(Name = "任务包含协议")]
        public List<ProtocolData> TaskProtocols { get; set; }

        [Required]
        [Display(Name = "任务生成时间")]
        public DateTime SetUpDateTime { get; set; }
    }
}