using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 设备维护状态
    /// </summary>
    public enum DeviceMaintenanceStatus
    {
        [Display(Name = "很脏")]
        Dirty,

        [Display(Name = "一般")]
        Normal,

        [Display(Name = "干净")]
        Clean
    }
}
