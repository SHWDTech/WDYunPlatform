using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 设备型号模型
    /// </summary>
    public class DeviceModel : SysDomainModelBase, IDeviceModel
    {
        [Required]
        [Display(Name = "型号名称")]
        [MaxLength(200)]
        public virtual string Name { get; set; }
    }
}
