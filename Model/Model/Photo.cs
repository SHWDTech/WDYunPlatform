using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 图片
    /// </summary>
    [Serializable]
    public class Photo : SysDomainModelBase, IPhoto
    {
        [Required]
        [Display(Name = "照片所属设备")]
        public virtual Device PhtotDevice { get; set; }

        [Display(Name = "照片附加信息")]
        public virtual string PhotoTag { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "照片地址")]
        public virtual string PhotoUrl { get; set; }

        [Required]
        [Display(Name = "照片类型")]
        public virtual SysDictionary PhotoType { get; set; }
    }
}