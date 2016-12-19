using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 通信协议
    /// </summary>
    [Serializable]
    public class Protocol : GuidModel, IProtocol
    {
        [Required]
        [Display(Name = "协议名称")]
        public string ProtocolName { get; set; }

        [Required]
        [Display(Name = "协议处理模块")]
        public string ProtocolModule { get; set; }

        [Display(Name = "协议自定义段")]
        public virtual string CustomerInfo { get; set; }

        [Required]
        [Display(Name = "协议版本号")]
        public virtual string Version { get; set; }

        [Required]
        [Display(Name = "协议头")]
        public virtual byte[] Head { get; set; }

        [Required]
        [Display(Name = "协议尾")]
        public virtual byte[] Tail { get; set; }

        [Display(Name = "协议包含的结构")]
        public virtual ICollection<ProtocolStructure> ProtocolStructures { get; set; }

        [Display(Name = "协议包含的指令")]
        public virtual ICollection<ProtocolCommand> ProtocolCommands { get; set; }

        [Display(Name = "包含此协议的固件")]
        public virtual ICollection<Firmware> Firmwares { get; set; } = new List<Firmware>();

        [Required]
        [Display(Name = "协议发布时间")]
        public virtual DateTime ReleaseDateTime { get; set; }

        [Required]
        [Display(Name = "校验类型")]
        public virtual string CheckType { get; set; }

        public string GetIdString() => Id.ToString();
    }
}
