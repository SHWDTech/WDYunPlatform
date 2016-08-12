using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 设备维护状态
    /// </summary>
    public enum DeviceMaintenanceStatus
    {
        [Display(Name = "很脏")]
        Dirty = 0,

        [Display(Name = "一般")]
        Normal = 1,

        [Display(Name = "干净")]
        Clean = 2
    }
}
