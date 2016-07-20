using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 项目模型
    /// </summary>
    [Serializable]
    public class Project : SysDomainModelBase, IProject
    {
        [Required]
        [Display(Name = "项目编码")]
        [MaxLength(200)]
        public virtual string ProjectCode { get; set; }

        [Required]
        [Display(Name = "项目名称")]
        [MaxLength(200)]
        public virtual string ProjectName { get; set; }

        [Display(Name = "负责人")]
        [MaxLength(200)]
        [Required]
        public virtual string ChargeMan { get; set; }

        [Display(Name = "联系电话")]
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public virtual string Telephone { get; set; }

        [Display(Name = "经度")]
        public virtual float? Longitude { get; set; }

        [Display(Name = "纬度")]
        public virtual float? Latitude { get; set; }

        [Display(Name = "备注")]
        [MaxLength(2000)]
        public virtual string Comment { get; set; }
    }
}
