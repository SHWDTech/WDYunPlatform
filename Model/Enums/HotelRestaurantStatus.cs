using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Enums
{
    /// <summary>
    /// 酒店（饭店）营业状态
    /// </summary>
    public enum HotelRestaurantStatus : byte
    {
        /// <summary>
        /// 营业中
        /// </summary>
        [Display(Name = "营业中")]
        Opening,

        /// <summary>
        /// 装修中
        /// </summary>
        [Display(Name = "装修中")]
        Decorating,

        /// <summary>
        /// 停业中
        /// </summary>
        [Display(Name = "停业中")]
        Stoped
    }
}
