using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Photo : SysModelBase, IPhoto
    {
        [Key]
        [Display(Name = "照片ID")]
        public Guid PhotoId { get; set; }

        [Required]
        [Display(Name = "照片所属域")]
        public SysDomain PhotoDomain { get; set; }

        [Required]
        [Display(Name = "照片所属设备")]
        public Device PhtotDevice { get; set; }

        [Display(Name = "照片附加信息")]
        public string PhotoTag { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "照片地址")]
        public string PhotoUrl { get; set; }

        [Required]
        [Display(Name = "照片类型")]
        public int PhotoType { get; set; }
    }
}
