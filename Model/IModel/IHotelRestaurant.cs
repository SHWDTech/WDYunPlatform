using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 酒店饭店模型接口
    /// </summary>
    public interface IHotelRestaurant : IProject
    {
        /// <summary>
        /// 所属餐饮企业
        /// </summary>
        CateringCompany RaletedCompany { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        DateTime RegisterDateTime { get; set; }

        /// <summary>
        /// 电子邮件地址
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 所属区域ID
        /// </summary>
        Guid DistrictId { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        UserDictionary District { get; set; }

        /// <summary>
        /// 所在街道ID
        /// </summary>
        Guid StreetId { get; set; }

        /// <summary>
        /// 所在街道
        /// </summary>
        UserDictionary Street { get; set; }

        /// <summary>
        /// 详细地址ID
        /// </summary>
        Guid AddressId { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        UserDictionary Address { get; set; }

        /// <summary>
        /// 酒店营业状况
        /// </summary>
        HotelRestaurantStatus Status { get; set; }

        /// <summary>
        /// 营业开始时间
        /// </summary>
        DateTime OpeningDateTime { get; set; }

        /// <summary>
        /// 营业截止时间
        /// </summary>
        DateTime StopDateTIme { get; set; }

        /// <summary>
        /// 灶头数
        /// </summary>
        int CookStoveNumber { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        string AddressDetail { get; set; }
    }
}
