using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 酒店饭店模型
    /// </summary>
    [Serializable]
    public class HotelRestaurant : Project, IHotelRestaurant
    {
        [Required]
        [MaxLength(200)]
        [Display(Name = "酒店编码")]
        public override string ProjectCode { get; set; }

        [Display(Name = "所属餐饮企业")]
        [ForeignKey("RaletedCompanyId")]
        public virtual CateringCompany RaletedCompany { get; set; }

        [Required]
        [Display(Name = "所属餐饮企业")]
        public virtual Guid RaletedCompanyId {get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "饭店（酒店）名称")]
        public override string ProjectName { get; set; }

        [Display(Name = "注册时间")]
        public virtual DateTime RegisterDateTime { get; set; }

        [Display(Name = "电子邮件")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [Display(Name = "区县")]
        [Required]
        public virtual Guid DistrictId { get; set; }

        [Display(Name = "区县")]
        [ForeignKey("DistrictId")]
        public virtual UserDictionary District { get; set; }

        [Required]
        [Display(Name = "街道")]
        public virtual Guid StreetId { get; set; }

        [Display(Name = "街道")]
        [ForeignKey("StreetId")]
        public virtual UserDictionary Street { get; set; }

        [Required]
        [Display(Name = "地址")]
        public virtual Guid AddressId { get; set; }

        [Display(Name = "地址")]
        [ForeignKey("AddressId")]
        public virtual UserDictionary Address { get; set; }

        [Display(Name = "酒店状态")]
        public virtual HotelRestaurantStatus Status { get; set; }

        [Display(Name = "营业开始时间")]
        public virtual DateTime OpeningDateTime { get; set; }

        [Display(Name = "营业截止时间")]
        public virtual DateTime StopDateTIme { get; set; }

        [Display(Name = "灶头数")]
        public virtual int CookStoveNumber { get; set; }

        [Display(Name = "详细地址")]
        public virtual string AddressDetail { get; set; }
    }
}
