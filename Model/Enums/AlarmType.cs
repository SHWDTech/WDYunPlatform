using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 报警类型
    /// </summary>
    public enum AlarmType
    {
        /// <summary>
        /// 油烟浓度超标
        /// </summary>
        [Display(Name = "油烟浓度超标")]
        LampblackDensity,

        /// <summary>
        /// 净化器失效
        /// </summary>
        [Display(Name = "净化器失效")]
        LampblackCleaner
    }
}
