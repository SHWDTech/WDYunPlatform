using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 餐饮企业模型
    /// </summary>
    [Serializable]
    public class CateringCompany : SysDomainModelBase, ICateringCompany
    {
        [MaxLength(200)]
        [Required]
        [Display(Name = "餐饮企业名称")]
        public virtual string CompanyName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "企业编码")]
        public virtual string CompanyCode { get; set; }

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

        [Display(Name = "注册时间")]
        public virtual DateTime RegisterDateTime { get; set; }
    }
}
