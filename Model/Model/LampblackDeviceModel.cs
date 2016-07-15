using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 设备型号模型
    /// </summary>
    public class LampblackDeviceModel : SysDomainModelBase, ILampblackDeviceModel
    {
        [Required]
        [Display(Name = "型号名称")]
        [MaxLength(200)]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "失效")]
        public virtual int Fail { get; set; }

        [Required]
        [Display(Name = "较差")]
        public virtual int Worse { get; set; }

        [Required]
        [Display(Name = "合格")]
        public virtual int Qualified { get; set; }

        [Required]
        [Display(Name = "良好")]
        public virtual int Good { get; set; }
    }
}
