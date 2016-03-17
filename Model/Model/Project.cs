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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "监测点编号")]
        public virtual int StatCode { get; set; }

        [Required]
        [Display(Name = "工程报建号")]
        [MaxLength(20)]
        public virtual string StatBJH { get; set; }

        [Display(Name = "项目外部编码")]
        [MaxLength(25)]
        public virtual string ProjectOutCode { get; set; }

        [Required]
        [Display(Name = "监测点名称")]
        [MaxLength(20)]
        public virtual string StatName { get; set; }

        [Required]
        [Display(Name = "项目负责人")]
        [MaxLength(20)]
        public virtual string ChargeMan { get; set; }

        [Display(Name = "负责人电话")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        public virtual string Telephone { get; set; }

        [Display(Name = "项目坐标经度")]
        [Range(-180, 180)]
        public virtual float Longitude { get; set; }

        [Display(Name = "项目坐标纬度")]
        [Range(-180, 180)]
        public virtual float Latitude { get; set; }

        [Required]
        [Display(Name = "施工单位")]
        [MaxLength(30)]
        public virtual string Department { get; set; }

        [Display(Name = "项目所属公司")]
        public virtual string Compnany { get; set; }

        [Display(Name = "项目地址")]
        [MaxLength(50)]
        public virtual string Address { get; set; }

        [Display(Name = "所属区县")]
        [MaxLength(20)]
        public virtual string Country { get; set; }

        [Display(Name = "项目所在街道")]
        [MaxLength(20)]
        public virtual string Street { get; set; }

        [Display(Name = "项目所属区域ID")]
        public virtual Guid DistrictId { get; set; }

        [Display(Name = "项目所属区域")]
        [ForeignKey("DistrictId")]
        public virtual SysDictionary District { get; set; }

        [Display(Name = "项目面积")]
        public virtual short Square { get; set; }

        [Display(Name = "工程开始时间")]
        public virtual DateTime ProStartDate { get; set; }

        [Display(Name = "施工进展情况")]
        [MaxLength(20)]
        public string Stage { get; set; }

        [Display(Name = "项目所属阶段ID")]
        public virtual Guid? ProjectStageId { get; set; }

        [Display(Name = "项目所属阶段")]
        public virtual SysDictionary ProjectStage { get; set; }

        [Display(Name = "项目开始时间")]
        public virtual DateTime StartDate { get; set; }

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