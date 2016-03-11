using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议
    /// </summary>
    [Serializable]
    public class Protocol : SysModelBase, IProtocol
    {
        //Required]
        [Display(Name = "协议应用领域")]
        public virtual SysDictionary Field { get; set; }

        //[Required]
        [Display(Name = "协议应用子领域")]
        public virtual SysDictionary SubField { get; set; }

        [Required]
        [Display(Name = "协议自定义段")]
        public string CustomerInfo { get; set; }

        [Required]
        [Display(Name = "协议版本号")]
        public string Version { get; set; }

        [Required]
        [Display(Name = "协议发布时间")]
        public DateTime ReleaseDateTime { get; set; }
    }
}