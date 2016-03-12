using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 项目
    /// </summary>
    [Serializable]
    public class Project : SysDomainModelBase, IProject
    {
        [Required]
        [Display(Name = "项目编码")]
        [MaxLength(25)]
        public virtual string ProjectCode { get; set; }

        [Display(Name = "项目外部编码")]
        [MaxLength(25)]
        public virtual string ProjectOutCode { get; set; }

        [Required]
        [Display(Name = "项目名称")]
        [MaxLength(25)]
        public virtual string ProjectName { get; set; }

        [Required]
        [Display(Name = "项目负责人")]
        [MaxLength(25)]
        public virtual string ChargeMan { get; set; }

        [Display(Name = "负责人电话")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25)]
        public virtual string Telephone { get; set; }

        [Display(Name = "项目坐标经度")]
        [Range(-180, 180)]
        public virtual float Longitude { get; set; }

        [Display(Name = "项目坐标纬度")]
        [Range(-180, 180)]
        public virtual float Latitude { get; set; }

        [Display(Name = "项目所属公司")]
        public virtual string Compnany { get; set; }

        [Display(Name = "项目地址")]
        [MaxLength(50)]
        public virtual string Address { get; set; }

        [Display(Name = "项目所在街道")]
        [MaxLength(200)]
        public virtual string Street { get; set; }

        [Display(Name = "项目所属区域ID")]
        public virtual Guid DistrictId { get; set; }

        [Display(Name = "项目所属区域")]
        [ForeignKey("DistrictId")]
        public virtual SysDictionary District { get; set; }

        [Display(Name = "项目面积")]
        public virtual short Square { get; set; }

        [Display(Name = "项目开始时间")]
        public virtual DateTime StartDate { get; set; }

        [Display(Name = "项目所属阶段ID")]
        public virtual Guid? StageId { get; set; }

        [Display(Name = "项目所属阶段")]
        [ForeignKey("StageId")]
        public virtual SysDictionary Stage { get; set; }

        [Display(Name = "项目类型ID")]
        public virtual Guid? TypeId { get; set; }

        [Display(Name = "项目类型")]
        [ForeignKey("TypeId")]
        public virtual SysDictionary Type { get; set; }

        [Display(Name = "项目报警类型ID")]
        public virtual Guid? AlarmTypeId { get; set; }

        [Display(Name = "项目报警类型")]
        [ForeignKey("AlarmTypeId")]
        public virtual SysDictionary AlarmType { get; set; }
    }
}