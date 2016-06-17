using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 餐饮企业模型
    /// </summary>
    [Serializable]
    public class Restaurant : SysDomainModelBase, IRestaurant
    {
        [MaxLength(200)]
        [Required]
        [Display(Name = "餐饮企业名称")]
        public virtual string RestaurantName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "企业编码")]
        public virtual string RestaurantCode { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "负责人")]
        public virtual string ChargeMan { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "联系电话")]
        public virtual string Telephone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "邮箱地址")]
        public virtual string Email { get; set; }

        [Display(Name = "详细地址")]
        public virtual string Address { get; set; }

        [NotMapped]
        [Display(Name = "注册时间")]
        public virtual DateTime DateTime => CreateDateTime;
    }
}
